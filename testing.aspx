<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testing.aspx.cs" Inherits="testing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <textarea id="text_area" runat="server" name="text_area" />
        <input type="button" name="build" id="build" runat="server" value="Compile the code" onserverclick="build_code" />
        <span id="show" runat="server"></span>
    </div>
    </form>
</body>
</html>
