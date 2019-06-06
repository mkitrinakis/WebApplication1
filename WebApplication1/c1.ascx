<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="c1.ascx.cs" Inherits="WebApplication1.c1" %>

<br /> 
<br /> 
this is the ascx control <br /> 

html input: <input id="c1T1" value="Init value od c1T1" /> <br /> 

Server Control input: <asp:TextBox ID="sc1" runat="server"  />

and this is a javascript button: <input type="button" value="pressMe" onclick="foo()" />

<%= (5+2).ToString() %>
<br /> 
<br /> 
<br /> 

<script>
    function foo() {
        alert('start...');
        var fld = document.getElementById('<%=sc1.ClientID  %>');
        alert('fld.value');
        fld.value = 'put from gavascript value';
        alert('finished');
    }
</script>



