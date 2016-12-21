<%@ Assembly Name="ECMASample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9a14bee9bbacd1f0" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ECMASample.aspx.cs" Inherits="ECMASample.Layouts.ECMASample.ECMASample" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:ScriptLink ID="ScriptLink1" Name="SP.js" runat="server" OnDemand="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink2" Name="/ECMASample/ECMASample.aspx.js" runat="server" OnDemand="false" Localizable="false" />
    <SharePoint:FormDigest ID="FormDigest1" runat="server" />
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
   <dx:ASPxLabel ID="lblStatus" runat="server" Text="" ClientInstanceName="lblStatus" />
   <br />
    <asp:Label ID="Label1" runat="server" Text="Last Name"></asp:Label><dx:ASPxTextBox ID="txtName" runat="server" Width="170px" ClientInstanceName="LastName"  /> 
    <br />
    <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label><dx:ASPxTextBox ID="txtFirstnme" runat="server" Width="170px" ClientInstanceName="FirstName"  />    
   <br />
       <asp:Label ID="Label3" runat="server" Text="Filter"></asp:Label><dx:ASPxTextBox ID="txtFilter" runat="server" Width="170px" ClientInstanceName="Filter"  />    
   <br />

   <dx:ASPxButton ID="btnAction" runat="server" AutoPostBack="false" Text="Get Web Properties" ClientInstanceName="btnAction" ClientSideEvents-Click="GetWebProperties"  />         <br />
   <dx:ASPxButton ID="btnReadList" runat="server" AutoPostBack="false" Text="ReadList" ClientInstanceName="btnAction" ClientSideEvents-Click="ReadFromList"  /> 
   <br />
      <dx:ASPxButton ID="btnAdd" runat="server" AutoPostBack="false" Text="Add Contact" ClientInstanceName="btnAdd" ClientSideEvents-Click="AddContact"  /> 
    <br />
   <dx:ASPxGridView ID="gvContacts" runat="server" ClientInstanceName="gvContacts" KeyFieldName="ID"  SettingsBehavior-AllowFocusedRow="True" AutoGenerateColumns="false"> 
    <ClientSideEvents BeginCallback="DeleteRow" />
   <Columns>
        <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="0" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="FirstName" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn Caption=" " VisibleIndex="2">                   
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton Text="Delete">
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
        </dx:GridViewCommandColumn>
    </Columns> 
    </dx:ASPxGridView>
    <br />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
ECMASample
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
ECMASample
</asp:Content>
