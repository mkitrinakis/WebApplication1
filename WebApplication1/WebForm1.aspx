<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div id="divRoot">
    <form id="form1" runat="server">
    
    
    
        <table>
            <tr>
        <td><asp:Label ID="Label1" runat="server" Text="Label1"></asp:Label></td>
&nbsp;
        <td><asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox></td>
                </tr>
        <tr>
        <td><asp:Label ID="Label3" runat="server" Text="Label3"></asp:Label></td>
        <td><asp:ListBox ID="ListBox1" runat="server"></asp:ListBox></td>
        </tr>
            <tr>
                <td>
        <asp:Label ID="Label2" runat="server" ClientIDMode="Static" Text="Label2"></asp:Label></td>
<td><asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList></td>
                </tr>
        </table>
        <div id="div1">
        </div>
    </form>
        </div>
</body>
</html>


<script src="Scripts/jquery-1.10.2.min.js" > </script>
<script src="Scripts/jquery-1.10.2.intellisense.js"></script>

<script lang="javascipt">
    
    var textBox1 = $('#<%=TextBox1.ClientID%>');
    textBox1.css({
        background: "green",
        border: "3px red solid"
    });

    alert('4');
    //$("form input:text")
    //$("#div1").text("This is my message.....")
  
</script>