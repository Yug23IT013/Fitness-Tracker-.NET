<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewWorkouts.aspx.cs" Inherits="FitnessTracker.Pages.ViewWorkouts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Workouts - Fitness Tracker</title>
    <link href="../CSS/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Fitness Tracker</h1>
                <p class="subtitle">Your workout history</p>
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
            
            <!-- Filter Section -->
            <div class="card">
                <h2>Filter Workouts</h2>
                <div style="display: flex; gap: 15px; flex-wrap: wrap; align-items: end;">
                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label for="txtStartDate">Start Date:</label>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label for="txtEndDate">End Date:</label>
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn btn-primary" OnClick="btnFilter_Click" />
                        <asp:Button ID="btnClearFilter" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btnClearFilter_Click" />
                    </div>
                </div>
            </div>
            
            <!-- Workouts Grid -->
            <div class="card">
                <h2>Workout History</h2>
                <asp:GridView ID="gvWorkouts" runat="server" CssClass="table" AutoGenerateColumns="false" 
                    EmptyDataText="No workouts found. <a href='LogWorkout.aspx'>Log your first workout</a>!" 
                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvWorkouts_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="SessionDate" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy}" />
                        <asp:BoundField DataField="ActivityName" HeaderText="Activity" />
                        <asp:BoundField DataField="ActivityCategory" HeaderText="Category" />
                        <asp:BoundField DataField="DurationMinutes" HeaderText="Duration (min)" />
                        <asp:BoundField DataField="CaloriesBurned" HeaderText="Calories" DataFormatString="{0:F0}" />
                        <asp:BoundField DataField="Notes" HeaderText="Notes" />
                    </Columns>
                    <PagerStyle CssClass="pagination" />
                </asp:GridView>
            </div>
            
            <!-- Summary Stats -->
            <div class="stats-grid">
                <div class="stat-card">
                    <div class="stat-number"><asp:Label ID="lblTotalWorkouts" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Total Workouts</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number"><asp:Label ID="lblTotalCalories" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Total Calories</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number"><asp:Label ID="lblTotalMinutes" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Total Minutes</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number"><asp:Label ID="lblAvgDuration" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Avg Duration (min)</div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
