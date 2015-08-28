<%@ Page Language="C#" AutoEventWireup="true" CodeFile="signup.aspx.cs" Inherits="login" EnableEventValidation="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>Online C# Compiler</title>
    <link rel="icon" type="image/png" href="./chobi/icon.png" />
	<script type="text/javascript" src="./js/validation.js"></script>
	<link rel="stylesheet" href="./style/signup.css" type="text/css" />
    <script src="./myjquery/jquery.min.js" type="text/javascript"></script>
    <script src="myjquery/jquery.min.1.7.1.js"></script>
    <script type="text/javascript">
        $('#dob').datepicker("setDate", new Date(2008, 9, 03));
    </script>
</head>
<body background="./chobi/signupbackground.jpg">
		<p><center><h3 style="color: #ffd800;"><u><i>Please fill the following form to signup</i></u></h3></center></p><br />
	</div>
	<div id="formtype">
		<fieldset>
			<legend>Signup Form</legend>
			<div id="profile_image_block"><img id="profile_image" src="./style/profile.png" alt="profile_pic" width="100px" height="100px" /></div>
			<form autocomplete="on" name="loginform" runat="server">
				<label for="fname"><b>First Name</b></label>:  
				<asp:TextBox class="error" id="firstname" placeholder="Enter your first name" 
                    runat="server" /><span id="firstname_span" ></span><br /><br />
                <label for="lname"><b>Last Name</b></label>:  
				<asp:TextBox class="error" id="lastname" placeholder="Enter your last name" runat="server" /><span id="lastname_span" ></span><br /><br />
                <label for="username"><b>Username</b></label>:
                <input type="text" id="userid" name="userid" placeholder="User ID" runat="server" /><span id="userid_span" ></span><br/><br />
				<label for="email"><b>Email</b></label>:  
				<input type="text" id="e_mail" placeholder="Enter your e-mail" runat="server" /><span id="email_span" ></span><br/><br />
        		<label for="password"><b>Password</b></label>:  
        		<input type="password" runat="server" class="error" id="pass" placeholder="Password" /><br/><br />
        		<label for="password"><b>Retype Password</b></label>:  
        		<input type="password" runat="server" class="error" id="repeat_pass" placeholder="Retype Password" />
        		<span id="pass_span" ></span><br /><br />
                <label for="dob"><b>Your birthday</b></label>:
                <input type="text" id="dob" name="dob" runat="server"/>
                <input type="submit" onclick="return check();" id="submit" value="Sign Up" runat="server" onserverclick="submit_form" />
			</form>
		</fieldset><center>
        <span id="msg" name="msg" runat="server"></span></center>
    </div>
</body>
</html>