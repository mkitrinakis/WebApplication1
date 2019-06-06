<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testValidation.aspx.cs" Inherits="WebApplication1.testValidation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    

    <script type="text/javascript">
        function foo()
        {
            // alert('1'); 

            if (Page_ClientValidate()) {
                alert('passed validation')
                }
                else {
                alert('DID NOT pass...')
                }

            }

       

    </script>

    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lNow" runat="server"></asp:Label>
        <br />
        <br />

        <asp:Panel ID="panel1" runat="server">
            Label1 : <asp:TextBox runat="server" ID="tb1" /> 

        </asp:Panel>


        <asp:Button runat="server" ID="bSave" Text="Submit" OnClick="bSave_Click" /> 
        <br />
        <br />
        The result of the submit:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="L1" runat="server"></asp:Label>
        <br /> 

        and this is the client button 
        <input type="button" value="pressMe" onclick="foo()" /> 
    </div>
    </form>
</body>
</html>
