<%@ Page Title="Create New Test" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="New.aspx.vb" Inherits="NewTest" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>


    <div class="form-horizontal">
        <h4>Create a new rest service test.</h4>
        <p class="text-success">
            <asp:Literal runat="server" ID="SuccessMessage" Visible="false" Text="Test successfully added.  Add a new test below, <a href='/tests/#/runTests'>run</a> or <a href='/tests/#/editTests'>edit tests</a>." />
        </p>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="TestName" CssClass="col-md-2 control-label">Test name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="TestName" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TestName"
                    CssClass="text-danger" ErrorMessage="The test name field is required." />
            </div>
        </div>
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Description" CssClass="col-md-2 control-label">Description</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Description"  TextMode="MultiLine" Rows="2" CssClass="form-control textarea-large"  />
                
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Url" CssClass="col-md-2 control-label">Url</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Url" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Url"
                    CssClass="text-danger" ErrorMessage="The url field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Property" CssClass="col-md-2 control-label">Property</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Property" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Property"
                    CssClass="text-danger" ErrorMessage="The property field is required." />
            </div>
        </div>
           <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Value" CssClass="col-md-2 control-label">Value</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Value" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Value"
                    CssClass="text-danger" ErrorMessage="The value field is required." />
            </div>
        </div> 
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Method" CssClass="col-md-2 control-label">Method</asp:Label>
             <a data-toggle="collapse" data-target="#divPost" id="togglePost"></a>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="Method"  CssClass="form-control" onchange="$('#togglePost').click();" >
                    <asp:ListItem>Get</asp:ListItem>
                    <asp:ListItem>Post</asp:ListItem>
                </asp:DropDownList>
                
            </div>
        </div>
        <div class="form-group collapse" id="divPost">
            <asp:Label runat="server" AssociatedControlID="PostData" CssClass="col-md-2 control-label">Post Data</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="PostData"  TextMode="MultiLine" Rows="3" CssClass="form-control textarea-large"  />
                
            </div>
        </div>
        <div class="form-group" style="display:none">
            <asp:Label runat="server" AssociatedControlID="Global" CssClass="col-md-2 control-label">Run Tests Sequentially?</asp:Label>
            <div class="col-md-10">
                <asp:CheckBox runat="server" ID="Global" Checked="true" />                
            </div>
        </div>



        
        <div class="form-group">
            <a data-toggle="collapse" data-target="#suite" class="col-md-2 control-label">
                 Advanced
            </a>

        </div>
        <div id="suite" class="collapse">
                    <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Suite" CssClass="col-md-2 control-label">Suite</asp:Label>
                <div class="col-md-10">
                    <asp:DropDownList runat="server" ID="Suite" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="sqlSuites" DataTextField="TestSuite" DataValueField="Id">
                    </asp:DropDownList>
                  
                </div>
        </div> 
         <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ReturnParamName" CssClass="col-md-2 control-label">Return Param Name</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="ReturnParamName"></asp:TextBox>
                </div>
        </div>
                  <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ReturnParamPath" CssClass="col-md-2 control-label">Return Param Name</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="ReturnParamPath"></asp:TextBox>
                </div>
        </div>  
        <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Order" CssClass="col-md-2 control-label">Order</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Order" Width="20"></asp:TextBox>
                </div>
        </div> 
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ContentType" CssClass="col-md-2 control-label">Content Type</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ContentType"  CssClass="form-control" >
                    <asp:ListItem Value="application/x-www-form-urlencoded">application/x-www-form-urlencoded</asp:ListItem>
                     <asp:ListItem Value="application/xml">application/xml</asp:ListItem>
                     <asp:ListItem Value=" application/json">application/json</asp:ListItem>
                </asp:DropDownList>               
            </div>
        </div>
             <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PauseFirst" CssClass="col-md-2 control-label">Pause before running?</asp:Label>
            <div class="col-md-10">
                <asp:CheckBox runat="server" ID="PauseFirst" />              
            </div>
        </div>


        </div>
   

        <asp:SqlDataSource ID="sqlSuites" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [Suite] WHERE ([CompanyId] = @CompanyId)">
            <SelectParameters>
                <asp:ProfileParameter Name="CompanyId" PropertyName="CompanyId" Type="String" />
      </SelectParameters>
        </asp:SqlDataSource>
       <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Create Test" CssClass="btn btn-default" OnClick="CreateTest_Click" />
            </div>
       </div>
    </div>
</asp:Content>

