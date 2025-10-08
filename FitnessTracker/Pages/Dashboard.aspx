<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="FitnessTracker.Pages.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard - Fitness Tracker</title>
    <link href="../CSS/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Fitness Tracker</h1>
                <p class="subtitle">Welcome back, <asp:Label ID="lblUserName" runat="server"></asp:Label>!</p>
            </div>
            
            <nav class="navbar">
                <ul>
                    <li><a href="Dashboard.aspx">Dashboard</a></li>
                    <li><a href="LogWorkout.aspx">Log Workout</a></li>
                    <li><a href="ViewWorkouts.aspx">View Workouts</a></li>
                    <li><a href="Goals.aspx">Goals</a></li>
                    <li><a href="Logout.aspx" onclick="return confirm('Are you sure you want to logout?')">Logout</a></li>
                </ul>
            </nav>
            
            <!-- Dashboard Stats -->
            <div class="stats-grid">
                <div class="stat-card">
                    <div class="stat-number"><asp:Label ID="lblMonthlyWorkouts" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Workouts This Month</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number"><asp:Label ID="lblMonthlyCalories" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Calories Burned</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number"><asp:Label ID="lblMonthlyMinutes" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Minutes Exercised</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number"><asp:Label ID="lblActiveGoals" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Active Goals</div>
                </div>
            </div>
            
            <!-- Recent Workouts -->
            <div class="card">
                <h2>Recent Workouts</h2>
                <asp:GridView ID="gvRecentWorkouts" runat="server" CssClass="table" AutoGenerateColumns="false" EmptyDataText="No recent workouts found.">
                    <Columns>
                        <asp:BoundField DataField="SessionDate" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy}" />
                        <asp:BoundField DataField="ActivityName" HeaderText="Activity" />
                        <asp:BoundField DataField="DurationMinutes" HeaderText="Duration (min)" />
                        <asp:BoundField DataField="CaloriesBurned" HeaderText="Calories" DataFormatString="{0:F0}" />
                    </Columns>
                </asp:GridView>
                <div class="text-center mt-2">
                    <a href="ViewWorkouts.aspx" class="btn btn-primary">View All Workouts</a>
                </div>
            </div>
            
            <!-- Active Goals -->
            <div class="card">
                <h2>Active Goals</h2>
                <asp:GridView ID="gvActiveGoals" runat="server" CssClass="table" AutoGenerateColumns="false" EmptyDataText="No active goals. <a href='Goals.aspx'>Create a goal</a> to get started!">
                    <Columns>
                        <asp:BoundField DataField="GoalType" HeaderText="Goal Type" />
                        <asp:BoundField DataField="CurrentValue" HeaderText="Current" DataFormatString="{0:F1}" />
                        <asp:BoundField DataField="TargetValue" HeaderText="Target" DataFormatString="{0:F1}" />
                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                        <asp:TemplateField HeaderText="Progress">
                            <ItemTemplate>
                                <div class="progress">
                                    <div class="progress-bar" style='<%# string.Concat("width: ", Eval("ProgressPercentage"), "%") %>'></div>
                                </div>
                                <small><%# Eval("ProgressPercentage", "{0:F1}") %>%</small>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DaysRemaining" HeaderText="Days Left" />
                    </Columns>
                </asp:GridView>
                <div class="text-center mt-2">
                    <a href="Goals.aspx" class="btn btn-primary">Manage Goals</a>
                </div>
            </div>
            
            <!-- Quick Actions -->
            <div class="card">
                <h2>Quick Actions</h2>
                <div style="display: flex; gap: 15px; flex-wrap: wrap; justify-content: center;">
                    <a href="LogWorkout.aspx" class="btn btn-success">Log New Workout</a>
                    <a href="Goals.aspx" class="btn btn-primary">Set New Goal</a>
                    <a href="ViewWorkouts.aspx" class="btn btn-secondary">View All Workouts</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
