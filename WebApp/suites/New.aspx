<%@ Page Title="New Suit - Create new test suite" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="New.aspx.vb" Inherits="Suites_Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>


    <div class="form-horizontal">
        <h4>Create a new Suite to group and order tests to model customer use-cases.</h4>
        <p class="text-success">
            <asp:Literal runat="server" ID="SuccessMessage" Visible="false" />
        </p>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="SuiteName" CssClass="col-md-2 control-label">Suite Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="SuiteName" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SuiteName"
                    CssClass="text-danger" ErrorMessage="The suite name field is required." />
            </div>
        </div>
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Description" CssClass="col-md-2 control-label">Description</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Description"  TextMode="MultiLine" Rows="3" CssClass="form-control textarea-large"  />
                
            </div>
        </div>
        <div class="form-group" title="Use this option when tests are dependent on each other">
            <asp:Label runat="server" AssociatedControlID="Sequential" CssClass="col-md-2 control-label">Run Tests Sequentially?</asp:Label>
            <div class="col-md-10">
                <asp:CheckBox runat="server" ID="Sequential" Checked="true" />                
            </div>
        </div>
       <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Create Suite" CssClass="btn btn-default" OnClick="CreateSuite_Click" />
            </div>
       </div>
    </div>
</asp:Content>

