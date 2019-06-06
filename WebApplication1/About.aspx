<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebApplication1.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>

    Literal : 
    <asp:Literal ID="l1" runat="server" /> 


    This is the Panel: 
    <asp:Panel ID="p1" runat="server" /> 


    and this is an html control: <input type="text" id="t1" value="xxx" /> 




   This is a server side button: <asp:Button ID="b1" runat="server" OnClick="b1_Click" /> 

    <br /> 

    Just a dummy label to show button is clicked <asp:Label ID="Label1" runat="server" /> 

</asp:Content>



