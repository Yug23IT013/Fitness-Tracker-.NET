<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goals.aspx.cs" Inherits="FitnessTracker.Pages.Goals" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Goals - Fitness Tracker</title>
    <link href="../CSS/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Fitness Tracker</h1>
                <p class="subtitle">Set and track your fitness goals</p>
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
            
            <!-- Create New Goal -->
            <div class="card">
                <h2>Create New Goal</h2>
                
                <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                    <div class="alert alert-success" runat="server" id="divMessage">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                
                <div style="display: flex; gap: 15px; flex-wrap: wrap;">
                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label for="txtGoalType">Goal Type:</label>
                        <asp:TextBox ID="txtGoalType" runat="server" CssClass="form-control" placeholder="e.g., Weight Loss, Muscle Gain" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label for="txtTargetValue">Target Value:</label>
                        <asp:TextBox ID="txtTargetValue" runat="server" CssClass="form-control" TextMode="Number" step="0.1" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label for="txtUnit">Unit:</label>
                        <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control" placeholder="e.g., kg, lbs, minutes" required="true"></asp:TextBox>
                    </div>
                </div>
                
                <div style="display: flex; gap: 15px; flex-wrap: wrap;">
                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label for="txtStartDate">Start Date:</label>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label for="txtTargetDate">Target Date:</label>
                        <asp:TextBox ID="txtTargetDate" runat="server" CssClass="form-control" TextMode="Date" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label for="txtCurrentValue">Current Value:</label>
                        <asp:TextBox ID="txtCurrentValue" runat="server" CssClass="form-control" TextMode="Number" step="0.1" value="0"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Button ID="btnCreateGoal" runat="server" Text="Create Goal" CssClass="btn btn-primary" OnClick="btnCreateGoal_Click" />
                </div>
            </div>
            
            <!-- Active Goals -->
            <div class="card">
                <h2>Active Goals</h2>
                <asp:GridView ID="gvActiveGoals" runat="server" CssClass="table" AutoGenerateColumns="false" 
                    EmptyDataText="No active goals. Create your first goal above!" 
                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvActiveGoals_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="GoalType" HeaderText="Goal Type" />
                        <asp:BoundField DataField="CurrentValue" HeaderText="Current" DataFormatString="{0:F1}" />
                        <asp:BoundField DataField="TargetValue" HeaderText="Target" DataFormatString="{0:F1}" />
                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                        <asp:TemplateField HeaderText="Progress">
                            <ItemTemplate>
                                <div class="progress">
                                    <asp:Panel ID="pnlProgress" runat="server" CssClass="progress-bar" Width='<%# System.Web.UI.WebControls.Unit.Percentage(Convert.ToDouble(Eval("ProgressPercentage"))) %>' />
                                </div>
                                <small><%# Eval("ProgressPercentage", "{0:F1}") %>%</small>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DaysRemaining" HeaderText="Days Left" />
                        <asp:TemplateField HeaderText="Update Progress">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUpdateValue" runat="server" CssClass="form-control" TextMode="Number" step="0.1" 
                                    style="width: 80px; display: inline-block;" placeholder="New value"></asp:TextBox>
                                <asp:Button ID="btnUpdateProgress" runat="server" Text="Update" CssClass="btn btn-sm btn-success" 
                                    CommandArgument='<%# Eval("GoalID") %>' OnClick="btnUpdateProgress_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination" />
                </asp:GridView>
            </div>
            
            <!-- Completed Goals -->
            <div class="card">
                <h2>Completed Goals</h2>
                <asp:GridView ID="gvCompletedGoals" runat="server" CssClass="table" AutoGenerateColumns="false" 
                    EmptyDataText="No completed goals yet. Keep working towards your goals!" 
                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvCompletedGoals_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="GoalType" HeaderText="Goal Type" />
                        <asp:BoundField DataField="CurrentValue" HeaderText="Achieved" DataFormatString="{0:F1}" />
                        <asp:BoundField DataField="TargetValue" HeaderText="Target" DataFormatString="{0:F1}" />
                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                        <asp:BoundField DataField="TargetDate" HeaderText="Completed On" DataFormatString="{0:MMM dd, yyyy}" />
                    </Columns>
                    <PagerStyle CssClass="pagination" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
