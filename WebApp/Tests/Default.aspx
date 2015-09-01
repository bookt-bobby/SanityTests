<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <link href="/DataTable/DataTables-1.9.4/media/css/demo_table.css" rel="stylesheet" />
    <link href="http://marcoceppi.github.io/bootstrap-glyphicons/css/bootstrap.icon-large.min.css"  rel="stylesheet" /> 
     <h4 style="min-height:19px;"><span class="text-success" id="successMsg"></span></h4>
     <h4 style="min-height:19px;display:none"><span class="text-danger" id="errorMsg"></span></h4>
    <div ng-view></div>
</asp:Content>

