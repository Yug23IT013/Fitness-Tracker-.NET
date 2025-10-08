using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FitnessTracker.App_Code.Models;

namespace FitnessTracker.App_Code
{
    public class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["FitnessTrackerConnection"].ConnectionString;

        #region User Operations
        public static User AuthenticateUser(string email, string password)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password AND IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserID = (int)reader["UserID"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Age = reader["Age"] as int?,
                                Gender = reader["Gender"].ToString(),
                                Height = reader["Height"] as decimal?,
                                Weight = reader["Weight"] as decimal?,
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                IsActive = (bool)reader["IsActive"]
                            };
                        }
                    }
                }
            }
            return user;
        }

        public static bool RegisterUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Users (FirstName, LastName, Email, Password, Age, Gender, Height, Weight) 
                                VALUES (@FirstName, @LastName, @Email, @Password, @Age, @Gender, @Height, @Weight)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Age", user.Age ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", user.Gender ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Height", user.Height ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Weight", user.Weight ?? (object)DBNull.Value);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool EmailExists(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }
        #endregion

        #region Activity Operations
        public static List<Activity> GetAllActivities()
        {
            List<Activity> activities = new List<Activity>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Activities WHERE IsActive = 1 ORDER BY ActivityName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            activities.Add(new Activity
                            {
                                ActivityID = (int)reader["ActivityID"],
                                ActivityName = reader["ActivityName"].ToString(),
                                CaloriesPerMinute = (decimal)reader["CaloriesPerMinute"],
                                Category = reader["Category"].ToString(),
                                IsActive = (bool)reader["IsActive"]
                            });
                        }
                    }
                }
            }
            return activities;
        }

        public static Activity GetActivityById(int activityId)
        {
            Activity activity = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Activities WHERE ActivityID = @ActivityID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityID", activityId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            activity = new Activity
                            {
                                ActivityID = (int)reader["ActivityID"],
                                ActivityName = reader["ActivityName"].ToString(),
                                CaloriesPerMinute = (decimal)reader["CaloriesPerMinute"],
                                Category = reader["Category"].ToString(),
                                IsActive = (bool)reader["IsActive"]
                            };
                        }
                    }
                }
            }
            return activity;
        }
        #endregion

        #region Workout Session Operations
        public static bool LogWorkoutSession(WorkoutSession session)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO WorkoutSessions (UserID, ActivityID, SessionDate, DurationMinutes, CaloriesBurned, Notes) 
                                VALUES (@UserID, @ActivityID, @SessionDate, @DurationMinutes, @CaloriesBurned, @Notes)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", session.UserID);
                    cmd.Parameters.AddWithValue("@ActivityID", session.ActivityID);
                    cmd.Parameters.AddWithValue("@SessionDate", session.SessionDate);
                    cmd.Parameters.AddWithValue("@DurationMinutes", session.DurationMinutes);
                    cmd.Parameters.AddWithValue("@CaloriesBurned", session.CaloriesBurned ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", session.Notes ?? (object)DBNull.Value);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static List<WorkoutSession> GetUserWorkoutSessions(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<WorkoutSession> sessions = new List<WorkoutSession>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT ws.*, a.ActivityName, a.Category, a.CaloriesPerMinute 
                                FROM WorkoutSessions ws 
                                INNER JOIN Activities a ON ws.ActivityID = a.ActivityID 
                                WHERE ws.UserID = @UserID";
                
                if (startDate.HasValue)
                    query += " AND ws.SessionDate >= @StartDate";
                if (endDate.HasValue)
                    query += " AND ws.SessionDate <= @EndDate";
                
                query += " ORDER BY ws.SessionDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    if (startDate.HasValue)
                        cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                    if (endDate.HasValue)
                        cmd.Parameters.AddWithValue("@EndDate", endDate.Value);
                    
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sessions.Add(new WorkoutSession
                            {
                                SessionID = (int)reader["SessionID"],
                                UserID = (int)reader["UserID"],
                                ActivityID = (int)reader["ActivityID"],
                                SessionDate = (DateTime)reader["SessionDate"],
                                DurationMinutes = (int)reader["DurationMinutes"],
                                CaloriesBurned = reader["CaloriesBurned"] as decimal?,
                                Notes = reader["Notes"].ToString(),
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                ActivityName = reader["ActivityName"].ToString(),
                                ActivityCategory = reader["Category"].ToString(),
                                ActivityCaloriesPerMinute = (decimal)reader["CaloriesPerMinute"]
                            });
                        }
                    }
                }
            }
            return sessions;
        }
        #endregion

        #region Goal Operations
        public static bool CreateGoal(Goal goal)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Goals (UserID, GoalType, TargetValue, CurrentValue, Unit, StartDate, TargetDate) 
                                VALUES (@UserID, @GoalType, @TargetValue, @CurrentValue, @Unit, @StartDate, @TargetDate)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", goal.UserID);
                    cmd.Parameters.AddWithValue("@GoalType", goal.GoalType);
                    cmd.Parameters.AddWithValue("@TargetValue", goal.TargetValue);
                    cmd.Parameters.AddWithValue("@CurrentValue", goal.CurrentValue);
                    cmd.Parameters.AddWithValue("@Unit", goal.Unit);
                    cmd.Parameters.AddWithValue("@StartDate", goal.StartDate);
                    cmd.Parameters.AddWithValue("@TargetDate", goal.TargetDate);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static List<Goal> GetUserGoals(int userId)
        {
            List<Goal> goals = new List<Goal>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Goals WHERE UserID = @UserID ORDER BY CreatedDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            goals.Add(new Goal
                            {
                                GoalID = (int)reader["GoalID"],
                                UserID = (int)reader["UserID"],
                                GoalType = reader["GoalType"].ToString(),
                                TargetValue = (decimal)reader["TargetValue"],
                                CurrentValue = (decimal)reader["CurrentValue"],
                                Unit = reader["Unit"].ToString(),
                                StartDate = (DateTime)reader["StartDate"],
                                TargetDate = (DateTime)reader["TargetDate"],
                                IsCompleted = (bool)reader["IsCompleted"],
                                CreatedDate = (DateTime)reader["CreatedDate"]
                            });
                        }
                    }
                }
            }
            return goals;
        }

        public static bool UpdateGoalProgress(int goalId, decimal currentValue)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Goals SET CurrentValue = @CurrentValue WHERE GoalID = @GoalID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CurrentValue", currentValue);
                    cmd.Parameters.AddWithValue("@GoalID", goalId);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Weight Log Operations
        public static bool LogWeight(WeightLog weightLog)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO WeightLog (UserID, Weight, LogDate, Notes) VALUES (@UserID, @Weight, @LogDate, @Notes)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", weightLog.UserID);
                    cmd.Parameters.AddWithValue("@Weight", weightLog.Weight);
                    cmd.Parameters.AddWithValue("@LogDate", weightLog.LogDate);
                    cmd.Parameters.AddWithValue("@Notes", weightLog.Notes ?? (object)DBNull.Value);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static List<WeightLog> GetUserWeightLogs(int userId)
        {
            List<WeightLog> logs = new List<WeightLog>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM WeightLog WHERE UserID = @UserID ORDER BY LogDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new WeightLog
                            {
                                WeightLogID = (int)reader["WeightLogID"],
                                UserID = (int)reader["UserID"],
                                Weight = (decimal)reader["Weight"],
                                LogDate = (DateTime)reader["LogDate"],
                                Notes = reader["Notes"].ToString()
                            });
                        }
                    }
                }
            }
            return logs;
        }
        #endregion

        #region Dashboard Statistics
        public static Dictionary<string, object> GetDashboardStats(int userId)
        {
            Dictionary<string, object> stats = new Dictionary<string, object>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                // Total workouts this month
                string workoutQuery = @"SELECT COUNT(*) FROM WorkoutSessions 
                                      WHERE UserID = @UserID 
                                      AND MONTH(SessionDate) = MONTH(GETDATE()) 
                                      AND YEAR(SessionDate) = YEAR(GETDATE())";
                using (SqlCommand cmd = new SqlCommand(workoutQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    stats["MonthlyWorkouts"] = (int)cmd.ExecuteScalar();
                }

                // Total calories burned this month
                string caloriesQuery = @"SELECT ISNULL(SUM(CaloriesBurned), 0) FROM WorkoutSessions 
                                       WHERE UserID = @UserID 
                                       AND MONTH(SessionDate) = MONTH(GETDATE()) 
                                       AND YEAR(SessionDate) = YEAR(GETDATE())";
                using (SqlCommand cmd = new SqlCommand(caloriesQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    stats["MonthlyCalories"] = (decimal)cmd.ExecuteScalar();
                }

                // Total workout time this month
                string timeQuery = @"SELECT ISNULL(SUM(DurationMinutes), 0) FROM WorkoutSessions 
                                   WHERE UserID = @UserID 
                                   AND MONTH(SessionDate) = MONTH(GETDATE()) 
                                   AND YEAR(SessionDate) = YEAR(GETDATE())";
                using (SqlCommand cmd = new SqlCommand(timeQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    stats["MonthlyMinutes"] = (int)cmd.ExecuteScalar();
                }

                // Active goals count
                string goalsQuery = "SELECT COUNT(*) FROM Goals WHERE UserID = @UserID AND IsCompleted = 0";
                using (SqlCommand cmd = new SqlCommand(goalsQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    stats["ActiveGoals"] = (int)cmd.ExecuteScalar();
                }
            }
            return stats;
        }
        #endregion
    }
}