<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


</head>

    <script>
      function foo(fieldName, selectName) {
        var field = document.getElementById(fieldName);
        var fieldValue = field.value;
        var options = fieldValue.split(",");
        if (options != '') {
            var select = document.getElementById(selectName);
            alert('removing'); 
            select.innerHTML = "";
            alert('and now adding'); 
            for (var index in options) {
                select.add(new Option(options[index], options[index]));
            }
        }
    }
    </script>
<body>
    <form id="form1" runat="server">
    <div>
    <button id="action-button" type="button" >Click me to load info!</button>
        <button id="action-button2" type="button" >just a test!! ... </button>
<div id="info"></div>
        The text field: <input id="tbt1" type="text" />

        <br />
        And the select field: <select id="s1"><option>1</option>  <option>2</option> <option>3</option> </select>
        <br />
        And finally the button: <button type="button" id="bfoo" onclick="foo('tbt1', 's1')" > click the foo() </button>
        <br />
    </div>
    </form>

    <script src="Scripts/jquery-1.10.2.min.js" > </script>
<script src="Scripts/jquery-1.10.2.intellisense.js"></script>


<script>

  

    $('#action-button').click(function() {
   $.ajax({
       url: 'http://localhost:40464/WCFHello.svc/DoWork',
       type: 'GET',
      data: {
         format: 'json'
      },
      error: function (data, e) {
          if (data.status == 0) {
              alert('You are offline!!\n Please Check Your Network.');
          } else if (data.status == 404) {
              alert('Requested URL not found.');
          } else if (data.status == 500) {
              alert('Internel Server Error.');
          } else if (e == 'parsererror') {
              alert('Error.\nParsing JSON Request failed.');
          } else if (e == 'timeout') {
              alert('Request Time out.');
          } else {
              alert('Unknow Error.\n' + data.responseText);
          }
      },
      
  //    dataType: 'jsonp',
      success: function(data) {
          $('#tbt1').text = 'OK ? '
          alert('1211');
          alert(data);
        
          //alert(rsp.data.responseText + "-->");
          //alert(rsp.responseText + "-->"); 
          //$('#info').append(rsp.responseText + "-->" ); 
          // $('#info').append(" --> IT IS OK");
           // .append($description);
      }
     
   });
    });


    $('#action-button2').click(function () {
        alert('1');
        $('#tbt1').val = 'NOT OK ? '
    });
    </script>
</body>
</html>
