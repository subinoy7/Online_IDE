using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BCrypt.Net;

public partial class texteditor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void opencmd(string code)
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("~/gcc_compiler/bin/c_files");
        //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "gcc.exe";
        startInfo.Arguments = "gcc ./c_files/prac.c";
        process.StartInfo = startInfo;
    }
    protected void text_submit_button_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/files/aspfile.aspx");
        string code = edit_here_area.InnerText.Trim();
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        File.WriteAllText(@path, code);
        HtmlControl contentPanel1 = (HtmlControl)this.FindControl("display_here_area");
        if (contentPanel1 != null)
            contentPanel1.Attributes["src"] = "~/files/aspfile.aspx";
    }
    protected void c_code_compiling(object sender, EventArgs e)
    {

    }
}