//window.onload = check;
function selected() {
	var op = document.getElementById('college_name');
	var anchor_clg = document.getElementById('clg');
	if(op.value==op.options[0].value){
		anchor_clg.innerHTML="Please Select a college name";
		return;
	}
	else
		anchor_clg.innerHTML="";
	return;
}
function check() {
    var passwd = document.getElementById('pass');
    var userid = document.getElementById('userid').value;
    var repass = document.getElementById('repass').value;
    if(document.getElementById('firstname').value=="")
        document.getElementById('firstname_span').innerText = "The firstname can not be empty";
    else if(document.getElementById('lastname').value=="")
        document.getElementById('lastname_span').innerText = "The firstname can not be empty";
    else if(userid==null || userid=="")
    document.getElementById('userid_span').innerText="The username can not be null.";
    if (pass.size < 8) {
        document.getElementById('pass_span').innerText = "Password must contains 8 letters";
        return false;
    }
    else if (pass.size > 13) {
        document.getElementById('pass_span').innerText = "Password must contains less than 13 letters";
        return false;
    }
    else if (pass != repass) {
        document.getElementById('pass_span').innerText = "The password does not match";
        return false;
    }
    else {
        document.getElementById('pass_span').innerText = "";
        return false;
    }
}