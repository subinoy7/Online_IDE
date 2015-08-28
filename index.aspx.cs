using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCrypt;
using System.Web.Security;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        check_session();
    }
    protected void check_session()
    {
        string tt = !string.IsNullOrEmpty(Request.QueryString["session"]) ? Request.QueryString["session"] : Guid.Empty.ToString();
        if (Convert.ToString(Session["username"]) != null)
        {
            if (Convert.ToString(Session["active"]) == "active")
            {
                b2.Style["visibility"] = "hidden";
                b.Style["visibility"] = "visible";
                b.InnerText = "Log Out";
                b1.Style["visibility"] = "hidden";
                bt.Style["visibility"] = "visible";
                bt.InnerText = Convert.ToString(Session["fullname"]);
            }
            if (tt.Substring(0, 4) == "time" && Convert.ToString(Session["active"]) == "")
                banner_msg.InnerText = "Session time expired or you have cleared the cookies";
        }
        else
        {
            bt.InnerText = "Hello, Guest";
        }
    }
    protected void log_out(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["active"]) == "active")
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            b2.Style["visibility"] = "visible";
            b.Style["visibility"] = "hidden";
            b.InnerText = "Log Me In";
            b1.Style["visibility"] = "visible";
            Response.Redirect("~/index.aspx?log_out=true");
        }
        else
        {
            Response.Redirect("~/index.aspx?session=timeout");
        }
    }
    protected void log_me_in(object sender, EventArgs e)
    {
        try
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hash = BCrypt.Net.BCrypt.HashPassword(pass.Value, salt);
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["login_connection_str"].ConnectionString;
            string query = string.Format("SELECT [hackers_choice].[tumi],[hackers_choice].[passwd],[sign_up].[firstname],[sign_up].[lastname] FROM [hackers_choice] INNER JOIN [sign_up] ON [hackers_choice].[tumi]=[sign_up].[username] WHERE [hackers_choice].[tumi]='{0}'", usrname.Value.Trim());
            SqlCommand mycommand = new SqlCommand(query, cn);
            cn.Open();
            SqlDataReader myreader = mycommand.ExecuteReader();
            if (myreader.HasRows)
            {
                myreader.Read();
                if (BCrypt.Net.BCrypt.Verify(pass.Value, string.Format("{0}", myreader[1])))
                {
                    log_in.Style["visibility"] = "hidden";
                    bool isPersistent = false;
                    string userData = "ApplicationSpecific data for this user.";
                    string u = BCrypt.Net.BCrypt.HashPassword("logged_in", salt);
                    string name = Convert.ToString(myreader[0]);
                    string fullname = Convert.ToString(myreader[2]) + " " + Convert.ToString(myreader[3]);
                    myreader.Close();
                    cn.Close();
                    Session["username"] = name;
                    Session["fullname"] = fullname;
                    Session["active"] = "active";
                    Session["handling"] = "true";
                    Session.Timeout = 60;
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddHours(1), isPersistent, userData, FormsAuthentication.FormsCookiePath);
                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);

                    // Create the cookie.
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    Server.Transfer("~/index.aspx?login=active");
                }
                else
                    login_invalid.InnerText = "Login username or password is invalid";
                cn.Close();
                myreader.Close();
            }
        }
        catch(Exception e1)
        {
            banner_msg.InnerText = "The server request is timed out";
        }
    }
}