<%@ Page Title="Sanity Tests Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Free rest-service testing app</h1>
        <p class="lead">Sanity Tests' free web application allows you to automate tests for rest services 
            to find errors before your consumers do.</p>
        <p><a  class="btn btn-primary btn-large" href="/About"  aria-disabled="true" title="coming soon">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                By registering your company, your users will be able to share and re-use tests.
            </p>
            <p>
                <a class="btn btn-default" href="/company/register">Register&raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>See a Demo!</h2>
            <p>
                See a demonstration of the software below
            </p>
            <p>
                <a class="btn btn-default" aria-disabled="true" title="coming soon">Learn more &raquo;</a>
            </p>
        </div>
      
    </div>

</asp:Content>
