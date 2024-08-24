using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Forms
{
    public partial class UserSignup : Form
    {
        public UserSignup()
        {
            InitializeComponent();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string username, password, fname, lname, contact, usertype;
            username = richTextBox3.Text;
            password = richTextBox4.Text;
            fname = richTextBox5.Text;
            lname = richTextBox6.Text;
            contact = richTextBox7.Text;
            usertype = richTextBox1.Text;

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string query1 = "select username from users where username = '" + username + "'";
            SqlCommand cm1 = new SqlCommand(query1, conn);
            object queryreturn1 = cm1.ExecuteScalar();

            if (queryreturn1 == null)
            {
                SqlCommand cmd = new SqlCommand("InsertNonMember", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@usertype", usertype);

                cmd.ExecuteNonQuery();

                string query = "select username from users where username = '" + username + "'";
                SqlCommand cm = new SqlCommand(query, conn);
                object queryreturn = cm.ExecuteScalar();

                if (queryreturn == null)
                {
                    MessageBox.Show("User Login failed. Please try again with different parameters.");
                }
                else
                {
                    MessageBox.Show("User added successfully!");
                    UserLogin userLogin = new UserLogin();
                    userLogin.Show();
                }
            }
            else
            {
                string query2 = "select usertype from users where username = '" + username + "'";
                SqlCommand cm2 = new SqlCommand(query2, conn);
                object queryreturn2 = cm2.ExecuteScalar();

                if (queryreturn2 == null)
                {
                    MessageBox.Show("User with this username already exists in database. Please try again with different username.");
                }
                else
                {
                    string query = "UPDATE users SET isActive = 1 WHERE username = '" + username + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Returning user re-added.");
                    UserLogin userLogin = new UserLogin();
                    userLogin.Show();
                    this.Close();
                }

            }
        }
    }
}
