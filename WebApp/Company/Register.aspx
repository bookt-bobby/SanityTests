<%@ Page Title="Register" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Register.aspx.vb" Inherits="RegisterCompany" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new company and user.  Once you register your company, you will be able to invite users to help manage your rest-service testing.</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">User name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                    CssClass="text-danger" ErrorMessage="The user name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
       </div>
       <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtCompanyName" CssClass="col-md-2 control-label">Company</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtCompanyName" CssClass="form-control" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCompanyName"
                    CssClass="text-danger" ErrorMessage="The company name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 control-label">Primary Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                    CssClass="text-danger" ErrorMessage="The company email field is required." />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail"
                    CssClass="text-danger" ErrorMessage="The email address is not valid." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            </div>
        </div>

         
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
</div>
</asp:Content>

