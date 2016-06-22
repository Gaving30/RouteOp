using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RouteOp
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                 
            }
        }

        protected void SubmitFormButton_Click(object sender, EventArgs e)
        {
            //Create variables by retrieving data from textboxes
            string strName = nameTextbox.Text.ToString();
            string strEmail = emailTextbox.Text.ToString();
            string strSubject = subjectTextBox.Text.ToString();
            string strComments = commentsTextbox.Text.ToString();
            string strFeedback = feedbackTextBox.Text.ToString();
            string strPostDate = DateTime.Now.ToString("dd-MM-yyyy");
            Boolean boolReplied = false;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO QueryTable(Name,eMail,Subject,Comments,PostDate,Replied,Feedback)" +
                " VALUES (@Name,@eMail,@Subject,@Comments,@PostDate,@Replied,@Feedback)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            SqlParameter ContactName = new SqlParameter("@Name", SqlDbType.VarChar, 50);
            ContactName.Value = strName.ToString();
            cmd.Parameters.Add(ContactName);
            SqlParameter ContactEmail = new SqlParameter("@eMail", SqlDbType.VarChar, 50);
            ContactEmail.Value = strEmail.ToString();
            cmd.Parameters.Add(ContactEmail);
            SqlParameter ContactSubject = new SqlParameter("@Subject", SqlDbType.VarChar, 100);
            ContactSubject.Value = strSubject.ToString();
            cmd.Parameters.Add(ContactSubject);
            SqlParameter ContactComments = new SqlParameter("@Comments", SqlDbType.VarChar, 200);
            ContactComments.Value = strComments.ToString();
            cmd.Parameters.Add(ContactComments);
            SqlParameter ContactPostDate = new SqlParameter("@PostDate", SqlDbType.DateTime);
            ContactPostDate.Value = strPostDate.ToString();
            cmd.Parameters.Add(ContactPostDate);
            SqlParameter ContactReplied = new SqlParameter("@Replied", DBNull.Value);
            ContactReplied.Value = boolReplied.ToString();
            cmd.Parameters.Add(ContactReplied);
            SqlParameter ContactFeedback = new SqlParameter("@Feedback", SqlDbType.VarChar, 200);
            ContactFeedback.Value = strFeedback.ToString();
            cmd.Parameters.Add(ContactFeedback);

            //Open - Run - Close Connection
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            //Let user know query was sent
            if (result > 0)
            {
                successLabel.Text = "Your Query Has Been Sent Successfully!";
            }

            //Clear all textboxes after storing details
            nameTextbox.Text = string.Empty;
            emailTextbox.Text = string.Empty;
            subjectTextBox.Text = string.Empty;
            commentsTextbox.Text = string.Empty;
            feedbackTextBox.Text = string.Empty;
        }
    }
}
