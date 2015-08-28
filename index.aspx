<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Compile Online</title>
    <link rel="icon" type="image/png" href="./chobi/icon.png" />
	<link rel="stylesheet" href="./style/my_style.css" type="text/css" />
    <script type="text/javascript" src="./myjquery/jquery.min.js"></script>
    <script src="./myjquery/jquery.min.1.7.1.js"></script>
	<script type="text/javascript">
	    window.onload = settime;
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
    <script>
        window.onload = f2;
        function f2(){
                if ($("fieldset").hasClass('changed'))
                {
                    $("fieldset").css('visibility', 'visible');
                    $("fieldset").toggle(1500);
                    $("fieldset").attr('class', 'not_checked');
                }
                else
                {
                    $("fieldset").toggle(2000);
                    $("fieldset").attr('class', 'changed');
                }
        }
    </script>
</head>
<body>
	<div id="whole_page">
		<div id="header">
			<h2 id="header_h2"><span id="project_name">Compile Online</span></h2><button id="bt" runat="server" class="admin_button" style="visibility:hidden;"/>
		</div>
        <span id="banner_msg" style="color: white; font-size: larger; position:absolute;" runat="server"></span>
		<div id="body_code">
			<span id="body_project_name">IDE for You</span>
		</div>
		<ul id="lists">
			<li><a href="#" id="inner_link">Home<img src="./chobi/Dropdown_Arrow.png" id="drop_down" /></a>
				<ul class="inner_home">
					<li><a href="about.html" relation="nofollow">About us</a></li>
					<li><a href="team.aspx" relation="nofollow">Team</a></li>
					<li><a href="contact.html" relation="nofollow">Contact us</a></li>
				</ul>
			</li>
			<li>Registration</li>
			<li>Tutorial</li>
			<li><a href="all_compilers.aspx" id="inner_link">Compilers</a></li>
			<li>Help</li>
		</ul>
        <div id="login_area">
			<fieldset id="login_frameset" style="visibility:hidden;" class="not_checked"><legend id="leg_end">Login</legend>
				<form id="login_form" method="post" runat="server">
					<p><label for="username">Username: </label>
					<input type="text" id="usrname" name="username" runat="server" placeholder="Enter you name" /><p>
					<label for="password">Password: </label>
					<input type="password" id="pass" name="pass" runat="server" placeholder="Password" /><input type="submit" id="log_in" name="log_in" runat="server" onserverclick="log_me_in" value="Login" />
				</form>
			</fieldset>
            <span id="login_invalid" runat="server" style="margin-left: 220px; color: wheat;position:absolute; margin-top :275px;font-family : Forte;font-style:italic;"></span>
		</div>
		<div id="buttons">
			<button id="b2" class="r1-button" type="button" runat="server" onclick="f2()" >Log Me In</button>
            <button id="b" class="r1-button" type="button" runat="server" style="visibility:hidden;" onserverclick="log_out" ></button>
			<a href="./signup.aspx" style="text-decoration:none;"><button id="b1" class="r2-submit" type="submit" runat="server" >Sign Up Free</button></a><br /><br /><br />
            <p id="heading" style="color:white;position:absolute;font-size:50px;">Compile and run your code online...</p>
		</div>
		<img src="./chobi/Untitled-2.png" title="Time goes on" id="clk_bg" alt="clock" />
		<span id="clock"></span>
	</div>
</body>
</html>