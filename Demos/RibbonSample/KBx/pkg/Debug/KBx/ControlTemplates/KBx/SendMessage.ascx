<%@ Assembly Name="KBx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9221b204671befc8" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendMessage.ascx.cs" Inherits="KBx.ControlTemplates.KBx.SendMessage" %>

<table bgcolor="#507CD1" 
    style="padding: 15px; width: 100%; margin: 0px 10px 0px 0px; color: #FFFFFF;">
    <tr>
        <td colspan="2">
            <h3>Send Message Form Dummy</h3>
        </td>
    </tr>
    <tr>
        <td width="70px">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td width="70px">
            <asp:Label ID="Label1" runat="server" Text="Subject:" ></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <asp:Label ID="Label2" runat="server" Text="Message" ></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox2" runat="server" Width="100%" Height="161px"></asp:TextBox>
        </td>
    </tr>
</table>


