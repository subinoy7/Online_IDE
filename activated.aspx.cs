using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class activated : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            string constr = ConfigurationManager.ConnectionStrings["login_connection_str"].ConnectionString;
            string pass = pwd.Value;
            string user_token = !string.IsNullOrEmpty(Request.QueryString["t"]) ? Request.QueryString["t"] : Guid.Empty.ToString();
            string activationCode = !string.IsNullOrEmpty(Request.QueryString["hklm_er"]) ? Request.QueryString["hklm_er"] : Guid.Empty.ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM [activation] WHERE [activatioin_code] = @activation_code",con))
                {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@activation_code", activationCode);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                        if (rowsAffected == 1)
                        {
                            activation_staus.InnerHtml = "Activation successful.";
                            string verify = string.Format("SELECT * FROM [sign_up] WHERE [username]='{0}' AND [password]='{1}'",username.Value.Trim(),pass);
                            SqlCommand verifycommand = new SqlCommand(verify,con);
                            con.Open();
                            SqlDataReader mydatareader = verifycommand.ExecuteReader();
                            if (!mydatareader.HasRows)
                                goto GET_OUT;
                            mydatareader.Close();
                            con.Close();
                            string login = "INSERT INTO [hackers_choice] ([tumi],[passwd]) VALUES(@tumi,@passwd)";
                            SqlCommand mycommand = new SqlCommand(login,con);
                            mycommand.Parameters.AddWithValue("@tumi",user_token);
                            mycommand.Parameters.AddWithValue("@passwd",pass);
                            con.Open();
                            mydatareader = mycommand.ExecuteReader();
                            if (mydatareader.HasRows)
                            {
                                mydatareader.Close();
                                con.Close();
                                Server.Transfer("~/index.aspx?q=activated");
                            }
                        }
                        else
                        {
                            con.Close();
                            activation_staus.InnerHtml = "Invalid Activation code.";
                            Server.Transfer("~/index.aspx?q=unactivated");
                        }
                    GET_OUT:
                        string msg = "Please,Enter your valid username and password you enterd at registration";
                    show.InnerHtml = msg;
                }
            }
        }
}