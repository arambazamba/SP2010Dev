<%@ Assembly Name="Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4ab0bb1d60ea2f51" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersistValues.ascx.cs" Inherits="Demo.CONTROLTEMPLATES.PersistValues" %>

<table style="width: 100%">
    <tr>
        <td><asp:Label ID="lUser" runat="server" Text="User"></asp:Label></td>
        <td><asp:TextBox ID="txtUser" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td><asp:Label ID="lPw" runat="server" Text="Passwort"></asp:Label></td>
        <td><asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:LinkButton ID="bSubmit" OnClick="SaveTextboxValue" runat="server">Speichern</asp:LinkButton></td>

    </tr>
</table>

