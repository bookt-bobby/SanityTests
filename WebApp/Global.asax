﻿<%@ Application Language="VB" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    Sub Application_Start(sender As Object, e As EventArgs)
        
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        
        If Not Roles.RoleExists("Admin") Then
            Roles.CreateRole("Admin")
        End If

    End Sub

</script>