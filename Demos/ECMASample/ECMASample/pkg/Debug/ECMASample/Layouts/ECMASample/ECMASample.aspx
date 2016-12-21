<%@ Assembly Name="ECMASample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9a14bee9bbacd1f0" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ECMASample.aspx.cs" Inherits="ECMASample.Layouts.ECMASample.ECMASample" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:ScriptLink ID="ScriptLink1" Name="SP.js" runat="server" OnDemand="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink3" Name="/ECMASample/jquery-1.6.2.min.js" runat="server" OnDemand="false" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink2" Name="/ECMASample/ECMASample.aspx.js" runat="server" OnDemand="false" Localizable="false" />
    <SharePoint:FormDigest ID="FormDigest1" runat="server" />
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
   <br />
    <asp:Label ID="Label1" runat="server" Text="Last Name"></asp:Label>
    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label>
    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>

   <br />
    <asp:Label ID="Label3" runat="server" Text="Filter"></asp:Label>
    <asp:TextBox ID="txtFilter" runat="server"></asp:TextBox>

   <br />
    <asp:HyperLink ID="hlAction" Text="Get Web Properties" runat="server" OnClick="GetWebProperties()"></asp:HyperLink>
    <br />
    <asp:HyperLink ID="hlReadList" Text="ReadList" runat="server" OnClick="ReadFromList()"></asp:HyperLink>
    <br />
    <asp:HyperLink ID="hlAdd" Text="Add Contact" runat="server" OnClick="AddContact()"></asp:HyperLink>
    <br/>
    <asp:Label ID="lblStatus" runat="server" >Initial Status</asp:Label>

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
ECMASample
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
ECMASample
</asp:Content>
