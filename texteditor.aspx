<%@ Page Language="C#" AutoEventWireup="true" CodeFile="texteditor.aspx.cs" Inherits="texteditor" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Online C# Compiler</title>
    <link rel="icon" type="image/png" href="./chobi/icon.png" />
    <link type="text/css" href="./style/foreditor.css" rel="stylesheet" />
    <script type="text/javascript" src="/jquery.hotkeys.js"></script>
</head>
<body>
    <a href="./index.aspx" realtion="nofollow" title="Home"><img src="./chobi/home_button.png" style="height: 50px; width: 140px;" alt="Back to home" /></a>
    <div id="text_edit_area" runat="server">
    <form id="editor_portion" name="editor_portion" runat="server">
        <textarea id="edit_here_area" name="edit_here_area" cols="70" rows="20" runat="server"></textarea><br />
        <asp:Button ID="text_submit_button" runat="server" Text="Submit asp Code >>" OnClick="text_submit_button_Click" /><br />
        <asp:Button ID="c_submit_button" runat="server" Text="Show the asp page >>" OnClick="c_code_compiling" />
    </form>
        <br />
    <div id="display_area" runat="server">
        <iframe name="display_here_area" id="display_here_area" width="900" height="200" runat="server" src="" ></iframe>
    </div>
    </div>
</body>
</html>
