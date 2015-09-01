<%@ Page Title="Invite a User to Sanity Tests" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Invite.aspx.vb" Inherits="Account_InviteUser" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Invite a User to <%= CompanyName%>.</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" /> <div class="form-group">
         <asp:Label runat="server" AssociatedControlID="txtName" CssClass="col-md-2 control-label">Display Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
                    CssClass="text-danger" ErrorMessage="The company email field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                    CssClass="text-danger" ErrorMessage="The company email field is required." />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail"
                    CssClass="text-danger" ErrorMessage="The email address is not valid." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            </div>
        </div>
         <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="InviteUser_Click" Text="Invite" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
    
</asp:Content>

