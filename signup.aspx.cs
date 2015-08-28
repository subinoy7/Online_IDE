using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data; //for commandtype
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void submit_form(object sender, EventArgs e)
    {
        int userId = 1;
        string activationCode = Guid.NewGuid().ToString();
        string constr = ConfigurationManager.ConnectionStrings["login_connection_str"].ConnectionString;
        try
        {
            using (SqlConnection cn = new SqlConnection(constr))
            {
                string fname = firstname.Text.Trim();
                string lname = lastname.Text.Trim();
                string uid = userid.Value.Trim();
                string pwd = pass.Value;
                string mail = e_mail.Value.Trim();
                string check = string.Format("SELECT [username],[email] FROM [sign_up] WHERE [username]='{0}' OR [email]='{1}'", uid, mail);
                SqlCommand mycommand = new SqlCommand(check, cn);
                cn.Open();
                SqlDataReader myDataReader = mycommand.ExecuteReader();
                if (myDataReader.HasRows)
                {
                    msg.InnerText = "Duplicate username or email";
                    myDataReader.Close();
                    cn.Close();
                }
                else
                {
                    myDataReader.Close();
                    msg.InnerText = "";
                    string last = "SELECT TOP 1 id FROM [sign_up] ORDER BY [id] DESC";
                    SqlCommand mycommand1 = new SqlCommand(last, cn);
                    myDataReader = mycommand1.ExecuteReader();
                    if (myDataReader.HasRows)
                    {
                        myDataReader.Read();
                        userId = Convert.ToInt32(myDataReader["id"]) + 1;
                        myDataReader.Close();
                    }
                    string query = "INSERT INTO [dbo].[sign_up]([id], [firstname], [lastname], [username], [password], [email],[creation_date]) VALUES (@id,@fname, @lname, @uid, @pwd, @mail,GETDATE()) INSERT INTO [dbo].[activation] ([id],[activatioin_code]) VALUES (@act_id,@activation_code)";
                    myDataReader.Close();
                    SqlCommand mycommand2 = new SqlCommand(query, cn);
                    mycommand2.Parameters.AddWithValue("@act_id", userId);
                    mycommand2.Parameters.AddWithValue("@fname", fname);
                    mycommand2.Parameters.AddWithValue("@lname", lname);
                    mycommand2.Parameters.AddWithValue("@mail", mail);
                    mycommand2.Parameters.AddWithValue("@pwd", pwd);
                    mycommand2.Parameters.AddWithValue("@id", userId);
                    mycommand2.Parameters.AddWithValue("@uid", userid.Value.Trim());
                    mycommand2.Parameters.AddWithValue("@activation_code", activationCode);
                    if (mycommand2.ExecuteNonQuery() > 1)
                    {
                        SendActivationEmail(userId, activationCode);
                    }
                    cn.Close();
                    Server.Transfer("~/index.aspx");
                }
            }
        }
        catch (Exception er)
        {
            msg.InnerText = "There is some problem with traffic please try again...";
        }
    }
    private void SendActivationEmail(int userId, string activationCode)
    {
        using (MailMessage mm = new MailMessage("subinoyproject@gmail.com", e_mail.Value.Trim()))
        {
            mm.Subject = "Online Compiler Account Activation";
            string body = "Hello " + userid.Value.Trim() + ",";
            body += "<br /><br /><p>Hello user online user has approved your request."+
                 "Thanks for registration. We don't spam you.<br />Post your problems or compile your code online." +
                "</p>Please click the following link to activate your account";
            body += "<br /><a style='text-decoration:none;' href = '" + Request.Url.AbsoluteUri.Replace("activated.aspx","activated.aspx?hklm_er=" + activationCode+"&t="+userid.Value.Trim()) + "'>Click here to activate your account.</a>";
            body += "<br /><br />Thanks";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("your_email@gmail.com", "subinoyproject");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }
}