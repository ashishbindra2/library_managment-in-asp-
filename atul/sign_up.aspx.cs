using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace atul
{
    public partial class sign_up : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["signConnectionString"].ConnectionString);
                con.Open();
                String checkuser = "SELECT count(*) FROM userdata WHERE UserName='" + TextBox1.Text+ "'";
                SqlCommand com = new SqlCommand(checkuser,con);
                int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
                if(temp==1)
                {
                    Response.Write("user alradyExists");
                }
                con.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Guid newGUID = Guid.NewGuid();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["signConnectionString"].ConnectionString);
                con.Open();
                String insertQuery = "INSERT into userdata (ID,UserName,Email,Password,Country) values(@ID,@Uname,@Email,@Password,@Country)";
                SqlCommand com = new SqlCommand(insertQuery, con);
                com.Parameters.AddWithValue("@ID", newGUID.ToString());
                com.Parameters.AddWithValue("@Uname",TextBox1.Text);
                com.Parameters.AddWithValue("@Email",TextBox2.Text);
                com.Parameters.AddWithValue("@Password",TextBox3.Text);
                com.Parameters.AddWithValue("@Country", DropDownList1.SelectedItem.ToString());
                com.ExecuteNonQuery();
                Response.Redirect("admin.aspx");
                Response.Write("susessfull");
                con.Close();
            }
            catch(Exception ex) {
                Response.Write("Error:" + ex.ToString());
            }
        }
    }
}