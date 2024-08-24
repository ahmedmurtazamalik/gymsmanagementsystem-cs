using Member1;
using System;
using System.Collections.Generic;
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
    public partial class MemberSignUp : Form
    {
        public MemberSignUp()
        {
            InitializeComponent();

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            SqlCommand dietPlanCmd = new SqlCommand("SELECT dietplanName FROM dietplan", conn);
            SqlDataReader dietPlanReader = dietPlanCmd.ExecuteReader();
            while (dietPlanReader.Read())
            {
                string dietPlanName = dietPlanReader["dietplanName"].ToString();
                comboBox1.Items.Add(dietPlanName);
            }
            dietPlanReader.Close();

            SqlCommand workoutPlanCmd = new SqlCommand("SELECT workoutplanName FROM workoutPlan", conn);
            SqlDataReader workoutPlanReader = workoutPlanCmd.ExecuteReader();
            comboBox2.Items.Clear();
            while (workoutPlanReader.Read())
            {
                string workoutPlanName = workoutPlanReader["workoutplanName"].ToString();
                comboBox2.Items.Add(workoutPlanName);
            }
            workoutPlanReader.Close();

            SqlCommand gymNameCmd = new SqlCommand("SELECT gymName FROM GYM where isApproved = 1", conn);
            SqlDataReader gymNameReader = gymNameCmd.ExecuteReader();
            comboBox3.Items.Clear();
            while (gymNameReader.Read())
            {
                string gymName = gymNameReader["gymName"].ToString();
                comboBox3.Items.Add(gymName);
            }
            gymNameReader.Close();

        }

        private void MemberSignUp_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string username = richTextBox3.Text;
            string password = richTextBox2.Text;
            string fname = richTextBox5.Text;
            string lname = richTextBox6.Text;
            string contact = richTextBox7.Text;
            string dietplanname = comboBox1.SelectedItem.ToString();
            string workoutplanname = comboBox2.SelectedItem.ToString();
            string gymName = comboBox3.SelectedItem.ToString();
            string membership = richTextBox1.Text;

            string query1 = "SELECT dietplanid FROM dietplan WHERE dietplanname = '" + dietplanname + "'";
            SqlCommand cm1 = new SqlCommand(query1, conn);
            object query1return = cm1.ExecuteScalar();
            string dietplanid = query1return.ToString();

            string query2 = "SELECT workoutplanid FROM workoutplan WHERE workoutplanname = '" + workoutplanname + "'";
            SqlCommand cm2 = new SqlCommand(query2, conn);
            object query2return = cm2.ExecuteScalar();
            string workoutplanid = query2return.ToString();

            string query3 = "SELECT isActive FROM users WHERE username = '" + username + "'";
            SqlCommand cm3 = new SqlCommand(query3, conn);
            object isActive = cm3.ExecuteScalar();

            if (isActive != null && (bool)isActive == false)
            {
                string updateQuery = "UPDATE users SET isActive = 1 WHERE username = '" + username + "'";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.ExecuteNonQuery();
                MessageBox.Show("Returning member re-added.");
                UserLogin userLogin = new UserLogin();
                userLogin.Show();
                this.Close();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("InsertMember", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@dietplanid", dietplanid);
                cmd.Parameters.AddWithValue("@workoutplanid", workoutplanid);
                cmd.Parameters.AddWithValue("@membership", membership);
                cmd.Parameters.AddWithValue("@gymName", gymName);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Registration Successful!");

                UserLogin ul = new UserLogin();
                ul.Show();
                this.Close();
            }
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
