<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogWorkout.aspx.cs" Inherits="FitnessTracker.Pages.LogWorkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log Workout - Fitness Tracker</title>
    <link href="../CSS/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Fitness Tracker</h1>
                <p class="subtitle">Log your workout session</p>
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
            
            <div class="card">
                <h2>Log New Workout</h2>
                
                <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                    <div class="alert alert-success" runat="server" id="divMessage">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                
                <div class="form-group">
                    <label for="ddlActivity">Select Activity:</label>
                    <asp:DropDownList ID="ddlActivity" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged">
                        <asp:ListItem Value="" Text="Select an activity"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="form-group">
                    <label for="txtSessionDate">Workout Date:</label>
                    <asp:TextBox ID="txtSessionDate" runat="server" CssClass="form-control" TextMode="Date" required="true"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="txtDuration">Duration (minutes):</label>
                    <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control" TextMode="Number" min="1" required="true"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="txtCalories">Calories Burned (optional):</label>
                    <asp:TextBox ID="txtCalories" runat="server" CssClass="form-control" TextMode="Number" step="0.1"></asp:TextBox>
                    <small class="text-muted">If left empty, calories will be calculated automatically based on activity and duration.</small>
                </div>
                
                <div class="form-group">
                    <label for="txtNotes">Notes (optional):</label>
                    <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <asp:Button ID="btnLogWorkout" runat="server" Text="Log Workout" CssClass="btn btn-primary" OnClick="btnLogWorkout_Click" />
                    <a href="Dashboard.aspx" class="btn btn-secondary">Cancel</a>
                </div>
            </div>
            
            <!-- Activity Information -->
            <div class="card" id="activityInfo" runat="server" visible="false">
                <h3>Activity Information</h3>
                <div class="form-group">
                    <label>Activity:</label>
                    <asp:Label ID="lblActivityName" runat="server" CssClass="form-control" style="background: #f8f9fa; border: 1px solid #dee2e6;"></asp:Label>
                </div>
                <div class="form-group">
                    <label>Category:</label>
                    <asp:Label ID="lblActivityCategory" runat="server" CssClass="form-control" style="background: #f8f9fa; border: 1px solid #dee2e6;"></asp:Label>
                </div>
                <div class="form-group">
                    <label>Calories per minute:</label>
                    <asp:Label ID="lblCaloriesPerMinute" runat="server" CssClass="form-control" style="background: #f8f9fa; border: 1px solid #dee2e6;"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
