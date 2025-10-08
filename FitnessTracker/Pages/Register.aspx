<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FitnessTracker.Pages.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register - Fitness Tracker</title>
    <link href="../CSS/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Fitness Tracker</h1>
                <p class="subtitle">Start your fitness journey today</p>
            </div>
            
            <div class="auth-container">
                <h2>Create Account</h2>
                
                <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                    <div class="alert alert-danger" runat="server" id="divMessage">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                
                <div class="form-group">
                    <label for="txtFirstName">First Name:</label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="txtLastName">Last Name:</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="txtEmail">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" required="true"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="txtPassword">Password:</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" required="true"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="txtConfirmPassword">Confirm Password:</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" required="true"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="txtAge">Age (Optional):</label>
                    <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="ddlGender">Gender (Optional):</label>
                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                        <asp:ListItem Value="" Text="Select Gender"></asp:ListItem>
                        <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                        <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                        <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="form-group">
                    <label for="txtHeight">Height in cm (Optional):</label>
                    <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" TextMode="Number" step="0.1"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="txtWeight">Weight in kg (Optional):</label>
                    <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control" TextMode="Number" step="0.1"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <asp:Button ID="btnRegister" runat="server" Text="Create Account" CssClass="btn btn-primary w-100" OnClick="btnRegister_Click" />
                </div>
                
                <div class="text-center">
                    <p>Already have an account? <a href="Login.aspx">Login here</a></p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
