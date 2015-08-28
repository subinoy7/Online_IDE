<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activated.aspx.cs" Inherits="activated" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center><span id="activation_staus" name="activation_staus" runat="server">
        Just complete it for security purpose<br />
        <p>Username : <input type="text" runat="server" id="username" name ="username" /></p>
        <p>Password : <input type="password" runat="server" id="pwd" name ="pwd" /></p>
        <input type="submit" value="Activate" name="activate" id="activate" />
    </span>
        <div id="show" name="show" runat="server"></div></center>
    </div>
    </form>
</body>
</html>