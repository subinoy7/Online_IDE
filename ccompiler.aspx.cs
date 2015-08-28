using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


[SerializableAttribute]
[PermissionSetAttribute(SecurityAction.InheritanceDemand, Name = "FullTrust")]//these three are permission for allowing to run the program
[PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]

public partial class ccomplier : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        check_session();
        if (Convert.ToString(Session["active"]) == "active" && Convert.ToString(Session["name"]) != null)
            save.Style["visibility"] = "visible";
        show_files();
    }
    protected bool check_session()
    {
        if (Convert.ToString(Session.Mode) == "off")
        {
            show.InnerText = "Hello, Guest";
            return false;
        }
        string tt = !string.IsNullOrEmpty(Request.QueryString["session"]) ? Request.QueryString["session"] : Guid.Empty.ToString();
        if (Convert.ToString(Session["username"]) != null || Convert.ToString(Session["username"]) != "")
        {
            if (Convert.ToString(Session["active"]) == "active")
            {
                show.InnerText = Convert.ToString(Session["fullname"]);
            }
            if (tt.Substring(0, 4) == "time" && Session["active"] == "")
                show.InnerText = "Session time expired";
        }
        else
            show.InnerText = "Hello, Guest";
        if (Session["name"] != null && Convert.ToString(Session["active"]) == "active")
        {
            save.Style["visibility"] = "visible";
        }
        return false;
    }
    protected void save_to_file(object sender, EventArgs e)
    {
        Directory.CreateDirectory(Server.MapPath("~/cfiles/" + "\\" + Session["username"] + "\\"));
        if (Convert.ToString(Session["active"]) == "active")
        {
            if (file_name.Value.Trim() == "")
                file_name.Value = "demo";
            if (File.Exists(Server.MapPath("~/cfiles/" + "\\" + Session["username"] + "\\" + file_name.Value.Trim() + ".c")))
            {
                //alert_already_exists.InnerText = file_name.Value.Trim() + ".cs" + " already exists...";
                File.Delete(Server.MapPath("~/cfiles/" + "\\" + Session["username"] + "\\" + file_name.Value.Trim() + ".c"));
            }
            else
                File.WriteAllText(Server.MapPath("~/cfiles/" + "\\" + Session["username"] + "\\" + file_name.Value.Trim() + ".c"), input_area.Value.Trim());
        }
        show_files();
    }
    protected void show_files()
    {
        if (Convert.ToString(Session["active"]) == "active" && Convert.ToString(Session["name"]) != null)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/cfiles//" + Session["username"]));
            foreach (System.IO.FileInfo p in dir.GetFiles("*.c"))
            {
                show.InnerHtml = "<a href=" + p.FullName + " style=\"text-decoration:none;\">" + "<img style=\"height:30px;width:30px;\" src=./chobi/file_icon.png />" + p.Name + "</a><br />";
            }
        }
    }
    protected void build_code(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/cfiles/test.c");
        string code = input_area.InnerText.Trim();
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        File.WriteAllText(@path, code);
        build_code(sender, e, "");
    }
    protected void build_code(object sender, EventArgs e, string s)
    {
        string path = Server.MapPath("~/gcc_compiler/bin");
        string output_path = Server.MapPath("~/cfiles");
        string arg = " " + output_path + "\\test.c";
        Process compiling_process = new Process();
        ProcessStartInfo compiling_info = new ProcessStartInfo();
        compiling_info.FileName = path + "\\gcc.exe";
        compiling_info.UseShellExecute = false;
        compiling_info.WorkingDirectory = path;
        compiling_info.RedirectStandardOutput = true;
        compiling_info.RedirectStandardInput = true;
        compiling_info.RedirectStandardError = true;
        compiling_info.Arguments = arg;
        compiling_process.StartInfo = compiling_info;
        compiling_process.Start();
        compiling_process.WaitForExit();
        string error_msg = compiling_process.StandardError.ReadToEnd();
        string output_msg = compiling_process.StandardOutput.ReadToEnd();
        compiling_process.Close();
        if(output_msg=="" && error_msg=="")
            output_area.InnerText = "Compiled Successfully. No errors";
        else
            output_area.InnerText = error_msg;
        if(s=="run" && error_msg =="")
        {
            string path1 = Server.MapPath("~/cfiles//" + Session["username"] + "test_input.txt");

            if (File.Exists(path1))
            {
                File.Delete(path1);
            }
            File.WriteAllText(@path1, input_value_area.Value.Trim());
            Process myProcess = new Process();
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo();
            if (cmd_arg.Checked)
            {
                try
                {
                    string args = input_value_area.Value.Trim();
                    myProcessStartInfo.Arguments = args;
                }
                catch (IndexOutOfRangeException re)
                {
                    alert_already_exists.InnerText = "Your arguments array range is out of range. Please Check it";
                }
            }
            myProcessStartInfo.FileName = path + "\\a.exe";
            myProcessStartInfo.WorkingDirectory = path;
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.RedirectStandardError = true;
            myProcessStartInfo.RedirectStandardInput = true;
            myProcess.StartInfo = myProcessStartInfo;
            myProcess.Start();

            string subline;
            StreamWriter myStreamReaderInput = myProcess.StandardInput;
            myStreamReaderInput.AutoFlush = true;
            string input = input_value_area.Value.Trim();

            System.IO.StreamReader f = new System.IO.StreamReader(path1);
            while ((subline = f.ReadLine()) != null)
            {
                myStreamReaderInput.WriteLine(subline);
            }
            f.Close();
            myStreamReaderInput.WriteLine();

            myStreamReaderInput.Dispose();
            myStreamReaderInput.Close();

            myProcess.WaitForExit();
            output_msg = myProcess.StandardOutput.ReadToEnd();
            error_msg = myProcess.StandardError.ReadToEnd();
            myProcess.Close();
            if ((output_msg == "" && error_msg == "") || (output_msg != "" && error_msg == ""))
                output_area.InnerText = output_msg;
            else
                output_area.InnerText = output_msg +Environment.NewLine+"Error:"+ error_msg;
        }
    }
    public void run_Click(object sender, EventArgs e)
    {
        build_code(sender, e, "run");
    }
}