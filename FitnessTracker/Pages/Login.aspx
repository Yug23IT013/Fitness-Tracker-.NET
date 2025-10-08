<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FitnessTracker.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Fitness Tracker</title>
    <link href="../CSS/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Fitness Tracker</h1>
                <p class="subtitle">Track your fitness journey</p>
            </div>
            
            <div class="auth-container">
                <h2>Login</h2>
                
                <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                    <div class="alert alert-danger" runat="server" id="divMessage">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                
                <div class="form-group">
                    <label for="txtEmail">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" required="true"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="txtPassword">Password:</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" required="true"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
                </div>
                
                <div class="text-center">
                    <p>Don't have an account? <a href="Register.aspx">Register here</a></p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
