using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void build_code(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["active"]) == "active" && Convert.ToString(Session["name"]) != null)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/csharpfiles/subinoy/"));
            foreach (System.IO.FileInfo p in dir.GetFiles("*.*"))
            {
                show.InnerHtml ="<a href="+p.FullName+" style=\"text-decoration:none;\">"+"<img src=./chobi/file_icon.png/>"+ p.Name+"</a><br />";
            }
        }
    }
}