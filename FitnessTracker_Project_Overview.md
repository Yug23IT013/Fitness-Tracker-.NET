# Fitness Tracker Web Application - Project Overview

## Project Information
- **Project Name**: Fitness Tracker
- **Technology Stack**: ASP.NET Web Forms (.NET Framework 4.7.2)
- **Database**: SQL Server
- **Frontend**: HTML, CSS, Bootstrap
- **Development Environment**: Visual Studio

## Project Description
The Fitness Tracker is a comprehensive web application designed to help users monitor and manage their fitness activities, set goals, and track their progress over time. The application provides an intuitive interface for logging workouts, viewing statistics, and maintaining fitness goals.

## Key Features

### 1. User Management
- **User Registration**: New users can create accounts with personal information including name, email, age, gender, height, and weight
- **User Authentication**: Secure login system with email and password validation
- **Profile Management**: Users can manage their personal information and account settings

### 2. Workout Tracking
- **Activity Library**: Pre-defined activities with calorie burn rates per minute
- **Workout Logging**: Users can log workouts with:
  - Activity selection
  - Duration tracking
  - Automatic calorie calculation
  - Optional notes
- **Workout History**: View all past workouts with filtering and sorting capabilities

### 3. Goal Setting & Tracking
- **Goal Creation**: Users can set various types of fitness goals
- **Progress Monitoring**: Real-time tracking of goal progress with visual indicators
- **Goal Management**: Update, complete, or modify existing goals

### 4. Dashboard & Analytics
- **Monthly Statistics**: Overview of workouts, calories burned, and exercise time
- **Recent Activity**: Quick view of latest workouts
- **Active Goals**: Progress tracking for current fitness goals
- **Quick Actions**: Easy access to common features

### 5. Weight Logging
- **Weight Tracking**: Log weight measurements over time
- **Progress Visualization**: Track weight changes and trends

## Technical Architecture

### Database Design
The application uses a well-structured relational database with the following main entities:

- **Users**: Store user account and profile information
- **Activities**: Predefined exercise activities with calorie rates
- **WorkoutSessions**: Individual workout records with duration and calories
- **Goals**: User-defined fitness objectives with progress tracking
- **WeightLog**: Weight measurement history

### Application Structure

#### Backend Components
- **Models**: Data models representing database entities
  - User.cs
  - Activity.cs
  - WorkoutSession.cs
  - Goal.cs
  - WeightLog.cs

- **Data Access Layer**: DatabaseHelper.cs provides centralized database operations
  - User authentication and management
  - Workout session operations
  - Goal management
  - Statistics and reporting

#### Frontend Pages
- **Login.aspx**: User authentication interface
- **Register.aspx**: New user registration
- **Dashboard.aspx**: Main user dashboard with statistics and quick actions
- **LogWorkout.aspx**: Workout entry form
- **ViewWorkouts.aspx**: Workout history and management
- **Goals.aspx**: Goal setting and progress tracking

#### Master Pages & Navigation
- **Site.Master**: Common layout and navigation structure
- **Site.Mobile.Master**: Mobile-optimized layout
- Responsive Bootstrap-based navigation

## User Experience Flow

### New User Journey
1. User visits the application
2. Registers for a new account with personal details
3. Logs in to access the dashboard
4. Explores available activities and logs first workout
5. Sets fitness goals for motivation
6. Regularly logs workouts and tracks progress

### Regular User Flow
1. Login to dashboard
2. View monthly statistics and recent activity
3. Log new workouts as they occur
4. Monitor goal progress
5. Review workout history and trends

## Key Benefits

### For Users
- **Easy Tracking**: Simple interface for logging workouts
- **Motivation**: Goal setting and progress visualization
- **Insights**: Statistical overview of fitness activities
- **Accessibility**: Web-based access from any device

### For Developers
- **Maintainable Code**: Clean separation of concerns
- **Scalable Architecture**: Well-structured database design
- **Extensible**: Easy to add new features and activities
- **Secure**: Proper user authentication and data protection

## Technical Specifications

### Development Environment
- **Framework**: .NET Framework 4.7.2
- **Web Technology**: ASP.NET Web Forms
- **Database**: SQL Server with ADO.NET
- **Frontend**: HTML5, CSS3, Bootstrap
- **IDE**: Visual Studio

### Security Features
- Password-based authentication
- Session management
- Input validation
- SQL injection prevention through parameterized queries

### Performance Considerations
- Database indexing for optimal query performance
- Efficient data retrieval with filtered queries
- Responsive design for various devices

## Future Enhancements (Potential)
- Exercise video tutorials
- Social features and workout sharing
- Advanced reporting and analytics
- Mobile application
- Integration with fitness devices
- Nutrition tracking
- Exercise recommendations based on goals

## Deployment Requirements
- IIS Server for hosting
- SQL Server database
- .NET Framework 4.7.2 runtime
- Modern web browser support

---

*This document provides a high-level overview of the Fitness Tracker application. For detailed technical specifications, database schemas, or implementation details, please refer to the source code and database documentation.*

## Screenshots Section
*[Screenshots will be added here to demonstrate the user interface and key features]*

1. Login Page
2. Dashboard Overview
3. Workout Logging Interface
4. Goals Management
5. Workout History View