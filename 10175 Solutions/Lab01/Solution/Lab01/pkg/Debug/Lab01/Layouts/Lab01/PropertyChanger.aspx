<%@ Assembly Name="Lab01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9db9a9b92d10c21b" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropertyChanger.aspx.cs" Inherits="Lab01.Layouts.Lab01.PropertyChanger" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <h2>Properties:</h2>
    <asp:Label ID="objectName" runat="server" Text=""></asp:Label><br/><br/>
    <asp:Panel ID="webProperties" runat="server" Visible="false" BorderColor="Orange" BorderStyle="Dashed" BorderWidth="1">
        <asp:Label ID="WebLabel" runat="server" Text="Web Title"></asp:Label>
        <br/>
        <asp:TextBox ID="webTitle" runat="server" EnableViewState="true"></asp:TextBox>
        &nbsp;
        <asp:Button ID="webTitleUpdate" runat="server" Text="Update"/>
        &nbsp;
        <asp:Button ID="webCancel" runat="server" Text="Cancel" />
    </asp:Panel>
    <asp:Panel ID="listProperties" runat="server" Visible="false" BorderColor="Orange" BorderStyle="Dashed" BorderWidth="1">
        <asp:Label ID="ListLabel" runat="server" Text="List Properties"></asp:Label>
        <br/>
        <asp:CheckBox ID="listVersioning" runat="server" EnableViewState="true" Text="Enable Versioning" />
        <br/>
        <asp:CheckBox ID="listContentTypes" runat="server" EnableViewState="true" Text="Enable Content Types" />
        &nbsp;
        <asp:Button ID="listPropertiesUpdate" runat="server" Text="Update" />
        &nbsp;
        <asp:Button ID="listCancel" runat="server" Text="Cancel"/>
    </asp:Panel>

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Property Changer
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
My Property Editor
</asp:Content>
