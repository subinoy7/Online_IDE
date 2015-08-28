<%@ Page Language="C#" AutoEventWireup="true" CodeFile="all_compilers.aspx.cs" Inherits="all_compilers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>Compile Online</title>
    <link rel="icon" type="image/png" href="./chobi/icon.png" />
	<link href="./style/all_cpp.css" rel="stylesheet" type="text/css" />
  	<script type="text/javascript">
  	    setInterval("settime()", 1000);
  	    function settime() {
  	        var dateTime = new Date();
  	        var hour = dateTime.getHours();
  	        var minute = dateTime.getMinutes();
  	        var second = dateTime.getSeconds();
  	        if (minute < 10)
  	            minute = "0" + minute;
  	        if (second < 10)
  	            second = "0" + second;
  	        var time = "" + hour + ":" + minute + ":" + second;
  	        document.getElementById("clock").innerHTML = time;
  	    }
  	</script>
</head>
<body background="./chobi/wallpaper21.jpg">
<div id="whole_page">
	<div class="flip3D">
		<div class="front"><img src="./chobi/c_logo.PNG"></img>
			<H3><span id="name">C-Compiler </span> </h3>
		</div>
		<div class="back"><h4><i>Click here on 'Start' button for compiling your C-Programing and take it flexible feature</i></h4>
						<a href="./ccompiler.aspx" rel="nofollow"><img id="img_id" src="./chobi/c_logo.PNG" alt="c_language" title="C Compiler" /></a></div>
	</div>
	<div class="flip3D">
		<div class="front"><img src="./chobi/asp.net_img.png"></img>
			<h3><span id="name">ASP.NET</span></h3></div>
		<div class="back"><h4><i>Click here on 'Start' button for compiling your ASP.NET and take it flexible feature</i></h4>
						<a  href="./texteditor.aspx" relation="nofollow"><img title="ASP.NET" id="img_id" src="./chobi/silver-apple-logo.png" /></a></div>
		</div>
	<div class="flip3D">
		<div class="front"><img src="./chobi/csharp.png"></img>
			<H3><span id="name"> C Sharp</span> </h3></div>
		<div class="back"><h4><i>Click here on 'Start' button for compiling your C# and take it flexible feature</i></h4>
						<a href="./csharpcompiler.aspx" realtion="nofollow"><img id="img_id" src="./chobi/csharp.png" title="C# compiler" alt="c#" /></a></div>
	</div>
<p></p>
<p></p>
<p></p>
<p></p>
	<span id="clock">
	</span>
</div>
</body>
</html>
