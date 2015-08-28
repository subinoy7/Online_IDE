<%@ Page Language="C#" AutoEventWireup="true" CodeFile="csharpcompiler.aspx.cs" Inherits="csharpcompiler"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Online C# Compiler</title>
    <link rel="icon" type="image/png" href="./chobi/icon.png" />
    <script src="./myjquery/jquery.min.1.7.1.js"></script>
    <script src="./myjquery/jquery.caret.1.02.min.js"></script>
    <script src="src/ace.js" type="text/javascript" ></script>
    <script src="src/theme-cobalt.js" type="text/javascript"></script>
    <script src="src/mode-csharp.js" type="text/javascript" charset="utf-8"></script>
    <script src="./js/csharp_ace.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/validation.js"></script>
    <link rel="stylesheet" type="text/css" href="./style/compiler_style.css" />
</head>
<body background="./chobi/18.jpg">
    <a href="./index.aspx" realtion="nofollow" title="Home"><img src="./chobi/home_button.png" style="height: 50px; width: 150px;" alt="Back to home" /></a>
    <span id="list_span" name="list_span" runat="server"></span>
    <form id="form1" runat="server">
        <textarea id="input_area" name="input_area" autofocus="autofocus" cols="1" rows="1" runat="server" spellcheck="False"></textarea>
        <div id="editor" runat="server" style="position:absolute;width:600px;height:320px;margin-left:20px;border-radius:5px;"></div>
        <textarea id="output_area" name="output_area" style="margin-left:710px;" cols="70" rows="20" runat="server" readonly="readonly"></textarea>
        <br /><p style="margin-left:20px;">
        <label for="Input Value for your program: ">Input Value for your program: </label>
        <textarea style="width:400px;" name="input_value_area" placeholder="Enter inputs line by line and for arguments enter as example and check the checkbox. Example: 100 &quot;Hello World&quot;" id="input_value_area" runat="server" /><br />
        <input type="checkbox" id="cmd_arg" name="cmd_arg" runat="server" style="margin-left:200px;" /><span>Check for command line arguments</span>
        <br />
        <button name="build" id="build" runat="server" onserverclick="build_code">Compile the code</button>
        <button id="run" name="run" runat="server" onserverclick="run_Click">Run the code</button><br />
        <div id="save" style="visibility:hidden;margin-left:20px;" runat="server">
            <input type="text" id="file_name" placeholder="filename" name="file_name" runat="server" />&nbsp;.<img src="./chobi/ext-c.png" id="extension" />
            <button id="save_file" name="save_file" runat="server" onserverclick="save_to_file">Save</button>
        </div></p>
    </form>
    <span runat="server" id="show"></span>
    <span runat="server" id="alert_already_exists"></span>
    <span id="file_dir" runat="server"></span>
</body>
</html>
