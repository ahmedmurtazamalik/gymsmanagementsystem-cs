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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;

namespace Forms
{
    public partial class GymOwner : Form
    {
        public GymOwner()
        {
            InitializeComponent();

            string username = UserLogin.userName;

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string query1 = "SELECT users.username FROM member JOIN users ON member.memberid = users.userid JOIN GYM ON member.gymName = gym.gymName where gym.ownerID = (select userid from users where username = '" + username + "') AND isActive = 1";
            SqlCommand command = new SqlCommand(query1, conn);

            SqlDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["username"].ToString());
            }

            reader.Close();

            string query2 = "SELECT u.username FROM trainer t JOIN users u ON t.trainerid = u.userid JOIN trainer_gym tg ON t.trainerid = tg.trainerid JOIN GYM g ON tg.gymid = g.GymId WHERE g.OwnerID = (select userid from users where username = '" + username + "') AND u.isActive = 1";
            SqlCommand cm1 = new SqlCommand(query2, conn);
            SqlDataReader reader2 = cm1.ExecuteReader();
            comboBox2.Items.Clear();
            while (reader2.Read())
            {
                comboBox2.Items.Add(reader2["username"].ToString());
            }
            reader2.Close();

            string query = "select * from users where username = '" + username + "'";
            SqlCommand cm = new SqlCommand(query,conn);
            SqlDataReader rdr = cm.ExecuteReader();

            if (rdr.Read())
            {
                string contact = rdr["contact"].ToString();
                string fname = rdr["fname"].ToString();
                string lname = rdr["lname"].ToString();

                label32.Text = username;
                label33.Text = contact;
                label34.Text = fname;
                label35.Text = lname;
            }
            rdr.Close();

            //datagridview2 mei trainers usernames who applied to gymid same as iss owner ki id and have status pending
            string query3 = "SELECT tr.requestid, u.username AS trainer_name, g.gymName FROM trainer_request tr JOIN users u ON tr.trainerid = u.userid JOIN gym g ON tr.gymid = g.gymid  WHERE tr.status = 'pending' AND tr.gymid = (SELECT TOP 1 gymid FROM gym WHERE ownerid IN (SELECT userid FROM users WHERE username = '" + username + "'))";
            SqlDataAdapter adapter1 = new SqlDataAdapter(query3, conn);
            DataTable dt1 = new DataTable();
            adapter1.Fill(dt1);
            dataGridView2.DataSource = dt1;
            dataGridView2.ReadOnly = true;
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView2.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView2.RowHeadersVisible = false;

            string getowneridq = "select userid from users where username = '" + username + "'";
            SqlCommand getownerid = new SqlCommand(getowneridq, conn);
            object getowneridret = getownerid.ExecuteScalar();
            string ownerid = getowneridret.ToString();

            string query4 = "select gymName from gym where ownerID = " + ownerid;
            SqlCommand cm3 = new SqlCommand(query4, conn);
            SqlDataReader reader3 = cm3.ExecuteReader();
            comboBox16.Items.Clear();
            comboBox3.Items.Clear();
            while (reader3.Read())
            {
                comboBox16.Items.Add(reader3["gymName"].ToString());
                comboBox3.Items.Add(reader3["gymName"].ToString());
            }
            reader3.Close();

            string query5 = "select gymName from gym where ownerID = " + ownerid + " AND isApproved = 1";
            SqlCommand cm4 = new SqlCommand(query5, conn);
            SqlDataReader reader4 = cm4.ExecuteReader();
            comboBox3.Items.Clear();
            while (reader4.Read())
            {
                comboBox3.Items.Add(reader4["gymName"].ToString());
            }
            reader4.Close();


        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Starter st = new Starter();
            st.Show();
            this.Close();
        }

        private void richTextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string username = UserLogin.userName;

            string name = richTextBox9.Text;
            string location = richTextBox3.Text;
            int members;

            // Check if gym name is null or empty
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a gym name.");
            }
            else
            {
                // Check if the gym already exists
                string checkQuery = "SELECT COUNT(*) FROM gym WHERE gymName = @gymName";
                SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                checkCommand.Parameters.AddWithValue("@gymName", name);
                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("A gym with the same name already exists. Please choose a different name.");
                }
                else
                {
                    string ownerid = "";
                    string query1 = "SELECT userid FROM users WHERE username = @username";
                    SqlCommand cm1 = new SqlCommand(query1, conn);
                    cm1.Parameters.AddWithValue("@username", username);

                    object query1return = cm1.ExecuteScalar();
                    if (query1return != null)
                    {
                        ownerid = query1return.ToString();
                    }

                    // Validate members input
                    if (int.TryParse(richTextBox2.Text, out members))
                    {
                        string query = "INSERT INTO gym (gymName, [Location], Members, isApproved, AdminID, OwnerID) " +
                                       "VALUES (@gymName, @Location, @Members, 0, NULL, @OwnerID)";
                        SqlCommand cm = new SqlCommand(query, conn);

                        cm.Parameters.AddWithValue("@gymName", name);
                        cm.Parameters.AddWithValue("@Location", location);
                        cm.Parameters.AddWithValue("@Members", members);
                        cm.Parameters.AddWithValue("@OwnerID", ownerid);
                        cm.ExecuteNonQuery();
                        MessageBox.Show("Gym sent for approval.");
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number for members.");
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string ownername = UserLogin.userName;
            string username = richTextBox12.Text;
            string password = richTextBox11.Text;
            string fname = richTextBox7.Text;
            string lname = richTextBox1.Text;
            string contact = richTextBox13.Text;
            string gymName = comboBox3.SelectedItem.ToString();
            // Check if the username is null or empty
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username for the trainer.");
            }
            else
            {
                // Check if the username already exists
                string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                checkCommand.Parameters.AddWithValue("@username", username);
                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("A trainer with the same username already exists. Please choose a different username.");
                }
                else
                {
                    string query1 = "SELECT userid FROM users WHERE username = '" + ownername + "'";
                    SqlCommand cm1 = new SqlCommand(query1, conn);

                    string ownerid = "";
                    object query1return = cm1.ExecuteScalar();
                    if (query1return != null)
                    {
                        ownerid = query1return.ToString();
                    }

                    SqlCommand cm = new SqlCommand("InsertNonMember", conn);
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.AddWithValue("@username", username);
                    cm.Parameters.AddWithValue("@password", password);
                    cm.Parameters.AddWithValue("@fname", fname);
                    cm.Parameters.AddWithValue("@lname", lname);
                    cm.Parameters.AddWithValue("@contact", contact);
                    cm.Parameters.AddWithValue("@usertype", "trainer");

                    cm.ExecuteNonQuery();

                    string query2 = "SELECT userid FROM users WHERE username = '" + username + "'";
                    SqlCommand cm2 = new SqlCommand(query2, conn);
                    object query2return = cm2.ExecuteScalar();
                    string trainerid = query2return.ToString();

                    SqlCommand updateTrainerCmd = new SqlCommand("UPDATE trainer SET ownerid = @ownerid WHERE trainerid = '" + trainerid + "'", conn);
                    updateTrainerCmd.Parameters.AddWithValue("@ownerid", ownerid);
                    updateTrainerCmd.ExecuteNonQuery();

                    string getgymidq = "select gymid from gym where gymName = '" + gymName + "'";
                    SqlCommand getgym = new SqlCommand(getgymidq, conn);
                    object getgymret = getgym.ExecuteScalar();
                    string gymid = getgymret.ToString();

                    DateTime currentDateTime = DateTime.Now;
                    string currentDateTimeString = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                    string trainergymquery = "INSERT INTO trainer_gym (trainerid, gymid, startdate) VALUES (@trainerid, @gymid, @datetime)";
                    SqlCommand updateTrainerGymCmd = new SqlCommand(trainergymquery, conn);
                    updateTrainerGymCmd.Parameters.AddWithValue("@trainerid", trainerid);
                    updateTrainerGymCmd.Parameters.AddWithValue("@gymid", gymid);
                    updateTrainerGymCmd.Parameters.AddWithValue("@datetime", currentDateTimeString);
                    updateTrainerGymCmd.ExecuteNonQuery();

                    MessageBox.Show("Trainer Added.");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            // Check if a member is selected in the comboBox1
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a member.");
                return;
            }

            string username = comboBox1.SelectedItem.ToString();

            string query2 = "select userid from users where username = '" + username + "'";
            SqlCommand cm2 = new SqlCommand(query2, conn);
            object query2return = cm2.ExecuteScalar();

            // Check if the memberid is null
            if (query2return == null)
            {
                MessageBox.Show("The selected member does not exist.");
                return;
            }

            string memberid = query2return.ToString();

            SqlCommand cm = new SqlCommand("SoftDeleteUser", conn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@userid", memberid);
            cm.ExecuteNonQuery();

            MessageBox.Show("Member removed.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            // Check if a member is selected in the comboBox1
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a trainer.");
                return;
            }


            string username = comboBox2.SelectedItem.ToString();

            string query2 = "select userid from users where username = '" + username + "'";
            SqlCommand cm2 = new SqlCommand(query2, conn);
            object query2return = cm2.ExecuteScalar();

            if (query2return == null)
            {
                MessageBox.Show("The selected trainer does not exist.");
                return;
            }

            string trainerid = query2return.ToString();

            SqlCommand cm = new SqlCommand("SoftDeleteUser", conn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@userid", trainerid);
            cm.ExecuteNonQuery();

            MessageBox.Show("Trainer removed.");
        }

        private void GymOwner_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string requestid = richTextBox4.Text;

            // Check if the requestid is null or empty
            if (string.IsNullOrEmpty(requestid))
            {
                MessageBox.Show("Please enter a request ID.");
            }
            else
            {
                // Check if the requestid exists in the DataGridView
                bool requestIdExists = false;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["requestid"].Value != null && row.Cells["requestid"].Value.ToString() == requestid)
                    {
                        requestIdExists = true;
                        break;
                    }
                }

                if (!requestIdExists)
                {
                    MessageBox.Show("The entered request ID is invalid.");
                }
                else
                {
                    // Update the trainer request status and insert data into the trainer_gym table
                    string updatequery = "UPDATE trainer_request SET status = 'Accepted' WHERE requestid = '" + requestid + "'";
                    SqlCommand cm = new SqlCommand(updatequery, conn);
                    cm.ExecuteNonQuery();

                    string traineridquery = "SELECT trainerid FROM trainer_request WHERE requestid = '" + requestid + "'";
                    string gymidquery = "SELECT gymid FROM trainer_request WHERE requestid = '" + requestid + "'";
                    SqlCommand cm1 = new SqlCommand(traineridquery, conn);
                    SqlCommand cm2 = new SqlCommand(gymidquery, conn);
                    object traineridResult = cm1.ExecuteScalar();
                    object gymidResult = cm2.ExecuteScalar();

                    // Check if trainerid or gymid is null
                    if (traineridResult == null || gymidResult == null)
                    {
                        MessageBox.Show("Trainer ID or Gym ID not found.");
                    }
                    else
                    {
                        string trainerid = traineridResult.ToString();
                        string gymid = gymidResult.ToString();
                        string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        string insertQuery = "INSERT INTO trainer_gym (trainerid, gymid, startdate) VALUES (@trainerid, @gymid, @startdate)";
                        SqlCommand cm3 = new SqlCommand(insertQuery, conn);
                        cm3.Parameters.AddWithValue("@trainerid", trainerid);
                        cm3.Parameters.AddWithValue("@gymid", gymid);
                        cm3.Parameters.AddWithValue("@startdate", currentDateTime);
                        cm3.ExecuteNonQuery();

                        MessageBox.Show("Trainer Approved!");
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string requestid = richTextBox4.Text;

            // Check if the requestid is null or empty
            if (string.IsNullOrEmpty(requestid))
            {
                MessageBox.Show("Please enter a request ID.");
            }
            else
            {
                // Check if the requestid exists in the DataGridView
                bool requestIdExists = false;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["requestid"].Value != null && row.Cells["requestid"].Value.ToString() == requestid)
                    {
                        requestIdExists = true;
                        break;
                    }
                }

                if (!requestIdExists)
                {
                    MessageBox.Show("Invalid request ID. Please enter a valid request ID.");
                }
                else
                {
                    // Update the trainer request status
                    string updatequery = "UPDATE trainer_request SET status = 'Rejected' WHERE requestid = '" + requestid + "'";
                    SqlCommand cm = new SqlCommand(updatequery, conn);
                    cm.ExecuteNonQuery();

                    MessageBox.Show("Trainer request rejected.");
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string machineName = richTextBox5.Text;
            string gymName = comboBox16.SelectedItem.ToString();

            string getgymidq = "select gymid from gym where gymName = '" + gymName + "'";
            SqlCommand getgym = new SqlCommand(getgymidq, conn);
            object getgymret = getgym.ExecuteScalar();
            string gymid = getgymret.ToString();  

            string insertMachineQuery = "INSERT INTO machine (name, gymId) VALUES (@name, @gymid)";
            SqlCommand insertMachine= new SqlCommand(insertMachineQuery, conn);
            insertMachine.Parameters.AddWithValue("@name", machineName);
            insertMachine.Parameters.AddWithValue("@gymid", gymid);
            insertMachine.ExecuteNonQuery();

            MessageBox.Show("Machine added to gym.");
        }
    }
}
