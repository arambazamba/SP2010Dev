<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupMembershipWebpartUserControl.ascx.cs" Inherits="DemoWebParts.GroupMembershipWebpart.GroupMembershipWebpartUserControl" %>

<asp:GridView ID="gvUsers" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False" CellPadding="4" DataSourceID="odsUsers" 
    EnableModelValidation="True" ForeColor="#333333" GridLines="None" 
    PageSize="15">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="Benutzer" HeaderText="Benutzer" 
            SortExpression="Benutzer" >
        <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>

<asp:ObjectDataSource ID="odsUsers" runat="server" 
    SelectMethod="GetUserForSiteGroups" 
    OldValuesParameterFormatString="original_{0}" 
    TypeName="DemoWebParts.GroupMembershipWebpart.UserInfo">
   
</asp:ObjectDataSource>


