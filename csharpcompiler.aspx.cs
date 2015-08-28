using System;
using System.CodeDom.Compiler; //using TempFileCollection class for temp file keeping or deleting
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;//for finding out words inside a string
using System.Security.Permissions;
using System.Security.AccessControl;
using System.Security.Principal;

[SerializableAttribute]
[PermissionSetAttribute(SecurityAction.InheritanceDemand, Name = "FullTrust")]//these three are permission for allowing to run the program
[PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]

public partial class csharpcompiler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        check_session();
        if (Convert.ToString(Session["active"]) == "active" && Convert.ToString(Session["name"]) != null)
            save.Style["visibility"] = "visible";
        show_files();
    }
    protected void show_files()
    {
        if (Convert.ToString(Session["active"]) == "active" && Convert.ToString(Session["name"]) != null)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/csharpfiles//" + Session["username"]));
            foreach (System.IO.FileInfo p in dir.GetFiles("*.cs"))
            {
                show.InnerHtml += "<a href=" + p.FullName + " style=\"text-decoration:none;\">" + "<img style=\"height:30px;width:30px;\" src=./chobi/file_icon.png />" + p.Name + "</a><br />";
            }
        }
    }
    protected void save_to_file(object sender, EventArgs e)
    {
        Directory.CreateDirectory(Server.MapPath("~/csharpfiles/" + "\\" + Session["username"] + "\\"));
        if (Convert.ToString(Session["active"]) == "active")
        {
            if (file_name.Value.Trim() == "")
                file_name.Value = "demo";
            if (File.Exists(Server.MapPath("~/csharpfiles/" +"\\" + Session["username"] + "\\" + file_name.Value.Trim() + ".cs")))
            {
                //alert_already_exists.InnerText = file_name.Value.Trim() + ".cs" + " already exists...";
                File.Delete(Server.MapPath("~/csharpfiles/" + "\\" + Session["username"] + "\\" + file_name.Value.Trim() + ".cs"));
            }
            else
                File.WriteAllText(Server.MapPath("~/csharpfiles/" + "\\" + Session["username"] + "\\" + file_name.Value.Trim() + ".cs"), input_area.Value.Trim());
        }
        show_files();
    }
    protected void loadfile(object sender, EventArgs e)
    {
        string name = (string)sender;
    }
    protected bool check_session()
    {
        if(Convert.ToString(Session.Mode)=="off")
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
    protected void build_code(object sender, EventArgs e) 
    {
        check_session();
        show_files();
        build_code(sender, e, "");
    }
    protected void build_code(object sender, EventArgs e, string s)
    {
        check_session();
        CodeDomProvider code_compiler = CodeDomProvider.CreateProvider("CSharp");
        string output_name = "out.exe";
        string path = Server.MapPath("~/temp_path");
        

        CompilerParameters parameters = new CompilerParameters();
        
        parameters.GenerateExecutable = true;
        parameters.OutputAssembly = path+@"\"+output_name;

        parameters.TempFiles = new TempFileCollection(path, false);
        parameters.GenerateInMemory = true;

        string code = input_area.Value.Trim();
        CompilerResults compile_results = code_compiler.CompileAssemblyFromSource(parameters, code);
        compile_results.PathToAssembly = path;
        compile_results.TempFiles = new TempFileCollection(path, false);
        if(compile_results.Errors.Count>0)
        {
            foreach(CompilerError comperr in compile_results.Errors)
            {
                string output_text="";
                output_text=output_text+"Line number:"+comperr.Line+", Error Number: "+comperr.ErrorNumber+", Error:  " + comperr.ErrorText + Environment.NewLine;
                output_area.InnerText=output_text;
            }
        }
        else
        {
            output_area.InnerText = "Successfully Compiled. No errors";
            if (s=="run")
            {
                string path1 = Server.MapPath("~/cfiles//" + Session["username"] + "test_input.txt");
                string subline;
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
                    catch(IndexOutOfRangeException re)
                    {
                        alert_already_exists.InnerText = "Your arguments array range is out of range. Please Check it";
                    }
                }
                myProcessStartInfo.FileName = path + @"\" + output_name;
                myProcessStartInfo.UseShellExecute=false;
                myProcessStartInfo.RedirectStandardOutput = true;
                myProcessStartInfo.RedirectStandardInput = true;

                myProcess.StartInfo = myProcessStartInfo;
                myProcess.Start();
                
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

                StreamReader myStreamReaderOutput = myProcess.StandardOutput;
                string myString;
                myString = myStreamReaderOutput.ReadToEnd();
                myProcess.WaitForExit();
                myProcess.Close();
                output_area.InnerText = myString;
            }
        }
    }
    protected void run_Click(object sender, EventArgs e)
    {
        build_code(sender, e,"run");
    }
}