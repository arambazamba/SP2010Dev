<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CopyTo.aspx.cs" Inherits="CopyTo2010.Layouts.CopyTo2010.CopyTo" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
   <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: top" width="850">
            <tr>
                <td colspan="2" valign="top" style="height: 40px">
                                <asp:LinkButton ID="lbConnect" runat="server" OnClick="PopulateTree">Connect</asp:LinkButton>
                                |
                                <asp:LinkButton ID="lbCopy" runat="server" OnClick="CopyItem">Copy</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 400px; background-color: antiquewhite;" valign="top">
                    <table border="0" cellspacing="10" width="100%">
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label8" runat="server" Text="Site Collection"></asp:Label></td>
                            <td valign="top">
                                <asp:TextBox ID="txtSiteCol" runat="server" Width="250px">http://chiron</asp:TextBox></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label6" runat="server" Text="List Item"></asp:Label></td>
                            <td valign="top">
                                <asp:Label ID="lblListItem" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label10" runat="server" Text="Selected List"></asp:Label></td>
                            <td valign="top">
                                <asp:Label ID="lblList" runat="server" Width="250px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top">
                                <asp:Label ID="lblStatus" runat="server" Width="350px"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 250px; background-color: antiquewhite;" valign="top">
                    <asp:TreeView ID="tvSite" runat="server" ImageSet="Simple" OnSelectedNodeChanged="NodeSelected" Width="250px">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
            </tr>
        </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Application Page
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
My Application Page
</asp:Content>
