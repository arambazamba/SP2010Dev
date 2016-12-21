<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference InlineScript="True" Path="DummyService.asmx" />
            </Services>
        </asp:ScriptManager>
    
    </div>
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Enter your Number:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMyNumber" runat="server" ClientIDMode="Static"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Process"></asp:Label>
            </td>
            <td>
                <asp:HyperLink ID="hlProcess" runat="server" Text="Click to process"
                 OnClick="CallWebservice()" CssClass="ShowMouse"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Result:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblResult" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
