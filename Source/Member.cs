using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Member1
{
    public partial class Member : Form
    {
        public Member()
        {
            InitializeComponent();
            string username = UserLogin.userName;
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string query = "select * from users where username = '" + username + "'";
            SqlCommand cm = new SqlCommand(query, conn);
            SqlDataReader rdr = cm.ExecuteReader();

            if (rdr.Read())
            {
                string contact = rdr["contact"].ToString();
                string fname = rdr["fname"].ToString();
                string lname = rdr["lname"].ToString();

                label39.Text = username;
                label59.Text = contact;
                label60.Text = fname;
                label61.Text = lname;
            }
            rdr.Close();

            string query1 = "select member.dietplanid, member.workoutplanid from member join users on users.userid = member.memberid where users.username = '" + username + "'";
            SqlCommand cm1 = new SqlCommand(query1, conn);
            SqlDataReader rdr1 = cm1.ExecuteReader();

            if (rdr1.Read())
            {
                string dietplanid = rdr1["dietplanid"].ToString();
                string workoutplanid = rdr1["workoutplanid"].ToString();

                string query2 = "select dietplanname, workoutplanname from dietplan, workoutPlan where dietplanid = " + dietplanid + "  AND workoutplanid = " + workoutplanid;
                
                SqlCommand cm2 = new SqlCommand(query2, conn);

                rdr1.Close();
                SqlDataReader rdr2 = cm2.ExecuteReader();

                if (rdr2.Read())
                {
                    string dietplanname = rdr2["dietplanname"].ToString();
                    string workoutplanname = rdr2["workoutplanname"].ToString();

                    richTextBox4.Text = dietplanname;
                    richTextBox3.Text = workoutplanname;
                }
                rdr2.Close();

            }

            string query3 = "select appointmentid, trainerid, memberid, date from appointment where isAccepted = 1";
            SqlDataAdapter adapter = new SqlDataAdapter(query3, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.ReadOnly = true;
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView2.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView2.RowHeadersVisible = false;

            string query4 = "SELECT u.username FROM users u WHERE u.isActive = 1 AND u.userid IN ( SELECT t.trainerid FROM trainer_gym tg JOIN trainer t ON tg.trainerid = t.trainerid JOIN users trainer_user ON t.trainerid = trainer_user.userid WHERE tg.gymid = ( SELECT g.GymId FROM gym g JOIN member m ON g.gymName = m.gymName JOIN users u2 ON m.memberid = u2.userid WHERE u2.username = '" + username + "'))";
            SqlCommand command = new SqlCommand(query4, conn);
            SqlDataReader reader = command.ExecuteReader();
            comboBox2.Items.Clear();
            comboBox1.Items.Clear();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader["username"].ToString());
                comboBox1.Items.Add(reader["username"].ToString());
            }

            reader.Close();

            string query5 = "select dietplanname from dietplan";
            SqlCommand cmd = new SqlCommand(query5, conn);
            SqlDataReader reader2 = cmd.ExecuteReader();
            comboBox6.Items.Clear();
            comboBox15.Items.Clear();
            while (reader2.Read())
            {
                comboBox6.Items.Add(reader2["dietplanname"].ToString());
                comboBox15.Items.Add(reader2["dietplanname"].ToString());
            }
            reader2.Close();

            string query6 = "select workoutplanname from workoutplan";
            SqlCommand cmd1 = new SqlCommand(query6, conn);
            SqlDataReader reader3 = cmd1.ExecuteReader();
            comboBox5.Items.Clear();
            comboBox19.Items.Clear(); 
            while (reader3.Read())
            {
                comboBox5.Items.Add(reader3["workoutplanname"].ToString());
                comboBox19.Items.Add(reader3["workoutplanname"].ToString());
            }
            reader3.Close();

            string query7 = "select allergenname from allergen";
            SqlCommand cmd2 = new SqlCommand(query7, conn);
            SqlDataReader reader4 = cmd2.ExecuteReader();
            comboBox14.Items.Clear();
            while (reader4.Read())
            {
                comboBox14.Items.Add(reader4["allergenname"].ToString());
            }
            reader4.Close();


            string query8 = "select mealname from meal";
            SqlCommand cmd3 = new SqlCommand(query8, conn);
            SqlDataReader reader5 = cmd3.ExecuteReader();
            comboBox13.Items.Clear();
            comboBox16.Items.Clear();
            while (reader5.Read())
            {
                comboBox13.Items.Add(reader5["mealname"].ToString());
                comboBox16.Items.Add(reader5["mealname"].ToString());
            }
            reader5.Close();

            string query9 = "select [name] from exercise";
            SqlCommand cmd4 = new SqlCommand(query9, conn);
            SqlDataReader reader6 = cmd4.ExecuteReader();
            comboBox18.Items.Clear();
            comboBox20.Items.Clear();
            while (reader6.Read())
            {
                comboBox18.Items.Add(reader6["name"].ToString());
                comboBox20.Items.Add(reader6["name"].ToString());
            }
            reader6.Close();

            string query10 = "select name from machine where gymid = (select gymid from gym where gymName = (select gymName from member join users on users.userid = member.memberid where username = '" + username + "'));";
            SqlCommand cmd5 = new SqlCommand(query10, conn);
            SqlDataReader reader7 = cmd5.ExecuteReader();
            comboBox17.Items.Clear();
            while (reader7.Read())
            {
                comboBox17.Items.Add(reader7["name"].ToString());
            }
            reader7.Close();

            string query11 = "select gymName from member join users on users.userid = member.memberid where username = '" + username + "'";
            SqlCommand cmd6 = new SqlCommand(query11, conn);
            object queryreturn11 = cmd6.ExecuteScalar();
            string gymName = queryreturn11.ToString();
            richTextBox11.Text = gymName;

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void tabPage15_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox26_TextChanged(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void tabPage13_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void label40_Click_1(object sender, EventArgs e)
        {

        }

        private void tabPage12_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void tabPage16_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click_1(object sender, EventArgs e)
        {

        }

        private void richTextBox33_TextChanged(object sender, EventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Starter starter = new Starter();   
            starter.Show();
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string username = UserLogin.userName;
            string workoutplanname = comboBox5.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(workoutplanname))
            {
                string getworkoutplanID = "SELECT workoutplanid FROM workoutplan WHERE workoutplanname = @workoutplanname";
                SqlCommand cm = new SqlCommand(getworkoutplanID, conn);
                cm.Parameters.AddWithValue("@workoutplanname", workoutplanname);
                object getworkoutplan = cm.ExecuteScalar();

                if (getworkoutplan != null)
                {
                    int workoutplanid = Convert.ToInt32(getworkoutplan);

                    string updateQuery = "UPDATE member SET workoutplanid = @workoutplanid WHERE memberid = (SELECT memberid FROM member JOIN users ON users.userid = member.memberid WHERE username = @username)";
                    SqlCommand cm1 = new SqlCommand(updateQuery, conn);
                    cm1.Parameters.AddWithValue("@workoutplanid", workoutplanid);
                    cm1.Parameters.AddWithValue("@username", username);
                    cm1.ExecuteNonQuery();

                    MessageBox.Show(workoutplanname + " has been selected as the workout plan.");
                }
                else
                {
                    MessageBox.Show("Selected workout plan does not exist.");
                }
            }
            else
            {
                MessageBox.Show("Please select a workout plan.");
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();
            string workoutplanname = richTextBox32.Text;
            int sets = Int32.Parse(richTextBox30.Text);
            int reps = Int32.Parse(richTextBox29.Text);

            // Check if the workout plan already exists
            string checkQuery = "SELECT COUNT(*) FROM workoutplan WHERE workoutplanName = @workoutplanname";
            SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
            checkCommand.Parameters.AddWithValue("@workoutplanname", workoutplanname);
            int count = (int)checkCommand.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("A workout plan with the same name already exists. Please choose a different name.");
            }
            else
            {
                // Insert the workout plan if it doesn't already exist
                string insertQuery = "INSERT INTO workoutplan (workoutplanName, [sets], reps) VALUES (@workoutplanname, @sets, @reps)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                insertCommand.Parameters.AddWithValue("@workoutplanname", workoutplanname);
                insertCommand.Parameters.AddWithValue("@sets", sets);
                insertCommand.Parameters.AddWithValue("@reps", reps);
                insertCommand.ExecuteNonQuery();
                MessageBox.Show("Workout plan created.");
            }
        }

        private void label62_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();
            string trainerName = comboBox2.SelectedItem.ToString();
            string dateTime = dateTimePicker1.Value.ToString();

            string username = UserLogin.userName;
            string query = "select memberid from member join users on users.userid = member.memberid where username = '" + username + "'";
            SqlCommand cm = new SqlCommand(query,conn);
            object queryreturn = cm.ExecuteScalar();
            string memberid = queryreturn.ToString();

            string query1 = "select trainerid from trainer join users on users.userid = trainer.trainerid where username = '" + trainerName + "'";
            SqlCommand cm1 = new SqlCommand(query1, conn);
            object queryreturn1 = cm1.ExecuteScalar();
            string trainerid = queryreturn1.ToString();

            string insertQuery = "INSERT INTO appointment (trainerid, memberid, isAccepted, date, status) VALUES (@trainerid, @memberid, 0, @dateTime, 'Pending')";
            SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
            insertCommand.Parameters.AddWithValue("@trainerid", trainerid);
            insertCommand.Parameters.AddWithValue("@memberid", memberid);
            insertCommand.Parameters.AddWithValue("@dateTime", dateTime);
            insertCommand.ExecuteNonQuery();
            MessageBox.Show("Sent for approval.");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string username = UserLogin.userName;
            string dietplanname = comboBox6.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(dietplanname))
            {
                string getdietplanID = "SELECT dietplanid FROM dietplan WHERE dietplanname = @dietplanname";
                SqlCommand cm = new SqlCommand(getdietplanID, conn);
                cm.Parameters.AddWithValue("@dietplanname", dietplanname);
                object getdietplan = cm.ExecuteScalar();

                if (getdietplan != null)
                {
                    int dietplanid = Convert.ToInt32(getdietplan);

                    string updateQuery = "UPDATE member SET dietplanid = @dietplanid WHERE memberid = (SELECT memberid FROM member JOIN users ON users.userid = member.memberid WHERE username = @username)";
                    SqlCommand cm1 = new SqlCommand(updateQuery, conn);
                    cm1.Parameters.AddWithValue("@dietplanid", dietplanid);
                    cm1.Parameters.AddWithValue("@username", username);
                    cm1.ExecuteNonQuery();

                    MessageBox.Show(dietplanname + " has been selected as the diet plan.");
                }
                else
                {
                    MessageBox.Show("Selected diet plan does not exist.");
                }
            }
            else
            {
                MessageBox.Show("Please select a diet plan.");
            }

        }

        private void button12_Click_1(object sender, EventArgs e)
        {

        }

        private void button14_Click_1(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {


        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string dietplanname = richTextBox5.Text;
            string plantype = richTextBox10.Text;
            string purpose = richTextBox20.Text;
            int nutrivalue = Int32.Parse(richTextBox9.Text);

            // Check if the diet plan already exists
            string checkQuery = "SELECT COUNT(*) FROM dietplan WHERE dietplanName = @dietplanname";
            SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
            checkCommand.Parameters.AddWithValue("@dietplanname", dietplanname);
            int count = (int)checkCommand.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("A diet plan with the same name already exists. Please choose a different name.");
            }
            else
            {
                // Insert the diet plan if it doesn't already exist
                string insertQuery = "INSERT INTO dietplan (dietplanName, nutriValue, [type], purpose) VALUES (@dietplanname, @nutrivalue, @type, @purpose)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                insertCommand.Parameters.AddWithValue("@dietplanname", dietplanname);
                insertCommand.Parameters.AddWithValue("@nutrivalue", nutrivalue);
                insertCommand.Parameters.AddWithValue("@type", plantype);
                insertCommand.Parameters.AddWithValue("@purpose", purpose);
                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Diet plan created.");
            }
        }

        private void button20_Click_1(object sender, EventArgs e)
        {

        }

        private void tabPage17_Click(object sender, EventArgs e)
        {

        }

        private void tabPage19_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string username = UserLogin.userName;
            string mealName = richTextBox7.Text;
            string portionsize = richTextBox6.Text;
            int fats = Int32.Parse(richTextBox2.Text);
            int proteins = Int32.Parse(richTextBox21.Text);
            int carbs = Int32.Parse(richTextBox13.Text);
            int calories = Int32.Parse(richTextBox8.Text);

            // Check if the meal already exists
            string checkQuery = "SELECT COUNT(*) FROM Meal WHERE MealName = @mealName";
            SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
            checkCommand.Parameters.AddWithValue("@mealName", mealName);
            int count = (int)checkCommand.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("A meal with the same name already exists. Please choose a different name.");
            }
            else
            {
                // Insert the meal if it doesn't already exist
                string insertQuery = "INSERT INTO Meal (MealName, PortionSize, Fats, Proteins, Carbs, Calories) VALUES (@mealName, @portionSize, @fats, @proteins, @carbs, @calories)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                insertCommand.Parameters.AddWithValue("@mealName", mealName);
                insertCommand.Parameters.AddWithValue("@portionSize", portionsize);
                insertCommand.Parameters.AddWithValue("@fats", fats);
                insertCommand.Parameters.AddWithValue("@proteins", proteins);
                insertCommand.Parameters.AddWithValue("@carbs", carbs);
                insertCommand.Parameters.AddWithValue("@calories", calories);
                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Meal created successfully.");
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string allergenName = richTextBox22.Text;

            // Check if the allergen name is empty or null
            if (string.IsNullOrEmpty(allergenName))
            {
                MessageBox.Show("Please enter an allergen name.");
            }
            else
            {
                // Check if the allergen already exists
                string checkQuery = "SELECT COUNT(*) FROM Allergen WHERE AllergenName = @AllergenName";
                SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                checkCommand.Parameters.AddWithValue("@AllergenName", allergenName);
                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("An allergen with the same name already exists. Please choose a different name.");
                }
                else
                {
                    // Insert the allergen if it doesn't already exist
                    string insertQuery = "INSERT INTO Allergen (AllergenName) VALUES (@AllergenName)";
                    SqlCommand insertQ = new SqlCommand(insertQuery, conn);
                    insertQ.Parameters.AddWithValue("@AllergenName", allergenName);
                    insertQ.ExecuteNonQuery();

                    MessageBox.Show("Allergen created.");
                }
            }



        }

        private void button21_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string allergenName = comboBox14.SelectedItem?.ToString();
            string mealName = comboBox13.SelectedItem?.ToString();

            // Check if allergen name or meal name is empty or null
            if (string.IsNullOrEmpty(allergenName) || string.IsNullOrEmpty(mealName))
            {
                MessageBox.Show("Please select both a meal and an allergen.");
            }
            else
            {
                string insertQuery = "INSERT INTO Meal_Allergen (MealName, AllergenName) VALUES (@MealName, @AllergenName)";
                SqlCommand insertQ = new SqlCommand(insertQuery, conn);
                insertQ.Parameters.AddWithValue("@MealName", mealName);
                insertQ.Parameters.AddWithValue("@AllergenName", allergenName);

                insertQ.ExecuteNonQuery();

                MessageBox.Show("Allergen added to meal.");
            }

        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string mealName = comboBox16.SelectedItem?.ToString();
            string dietplanName = comboBox15.SelectedItem?.ToString();

            // Check if meal name or diet plan name is empty or null
            if (string.IsNullOrEmpty(mealName) || string.IsNullOrEmpty(dietplanName))
            {
                MessageBox.Show("Please select both a meal and a diet plan.");
            }
            else
            {
                string getdietplanID = "select dietplanid from dietplan where dietplanname = '" + dietplanName + "'";
                SqlCommand cm = new SqlCommand(getdietplanID, conn);
                object getdietplan = cm.ExecuteScalar();

                // Check if diet plan exists
                if (getdietplan != null)
                {
                    int dietplanid = Int32.Parse(getdietplan.ToString());

                    string insertDietPlanMealQuery = "INSERT INTO DietPlan_Meal (DietPlanId, MealName) VALUES (@dietPlanID, @mealName)";
                    SqlCommand insertDietPlanMealCommand = new SqlCommand(insertDietPlanMealQuery, conn);
                    insertDietPlanMealCommand.Parameters.AddWithValue("@dietPlanID", dietplanid);
                    insertDietPlanMealCommand.Parameters.AddWithValue("@mealName", mealName);

                    insertDietPlanMealCommand.ExecuteNonQuery();

                    MessageBox.Show("Meal added to the Plan.");
                }
                else
                {
                    MessageBox.Show("Selected diet plan does not exist.");
                }
            }

        }

        private void button24_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string username = UserLogin.userName;
            string exerciseName = richTextBox31.Text;
            string muscleGroupName = richTextBox28.Text;

            // Check for empty or null values in exercise name and muscle group
            if (string.IsNullOrEmpty(exerciseName) || string.IsNullOrEmpty(muscleGroupName))
            {
                MessageBox.Show("Please fill in both exercise name and muscle group.");
            }
            else
            {
                // Check if the exercise already exists
                string checkQuery = "SELECT COUNT(*) FROM exercise WHERE [name] = @exercisename";
                SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                checkCommand.Parameters.AddWithValue("@exercisename", exerciseName);
                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("An exercise with the same name already exists. Please choose a different name.");
                }
                else
                {
                    // Insert the exercise if it doesn't already exist
                    string insertQuery = "INSERT INTO exercise (musclegroup, [name]) VALUES (@musclegroup, @exercisename)";
                    SqlCommand insertQ = new SqlCommand(insertQuery, conn);
                    insertQ.Parameters.AddWithValue("@musclegroup", muscleGroupName);
                    insertQ.Parameters.AddWithValue("@exercisename", exerciseName);
                    insertQ.ExecuteNonQuery();

                    MessageBox.Show("Exercise Created Successfully.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string username = UserLogin.userName;
            string exerciseName = comboBox18.SelectedItem?.ToString();
            string machineName = comboBox17.SelectedItem?.ToString();

            string subquery1 = "(select gymName from member join users on users.userid = member.memberid where username = '" + username + "')";
            SqlCommand sq1 = new SqlCommand(subquery1, conn);
            object sq1ret = sq1.ExecuteScalar();
            string gymName1 = sq1ret.ToString();

            string subquery2 = "select gymid from gym where gymName = '" + gymName1 + "'";
            SqlCommand sq2 = new SqlCommand(subquery2, conn);
            object sq2ret = sq2.ExecuteScalar();
            string gymid = sq2ret.ToString();

            // Check if exercise name or machine name is empty or null
            if (string.IsNullOrEmpty(exerciseName) || string.IsNullOrEmpty(machineName))
            {
                MessageBox.Show("Please select both an exercise and a machine.");
            }
            else
            {
                string getExerciseId = "select exerciseid from exercise where [name] ='" + exerciseName + "'";
                SqlCommand exerciseIdquery = new SqlCommand(getExerciseId, conn);
                object queryreturn = exerciseIdquery.ExecuteScalar();

                // Check if exercise exists
                if (queryreturn != null)
                {
                    int exerciseid = Int32.Parse(queryreturn.ToString());

                    string getMachineId = "select machineid from machine where gymid = " + gymid + " AND [name] = '" + machineName + "'";
                    SqlCommand machineIdquery = new SqlCommand(getMachineId, conn);
                    object queryreturn1 = machineIdquery.ExecuteScalar();

                    // Check if machine exists
                    if (queryreturn1 != null)
                    {
                        int machineid = Int32.Parse(queryreturn1.ToString());

                        string insertExerciseMachineQuery = "INSERT INTO Exercise_Machine (exerciseid, machineid) VALUES (@ExerciseId, @MachineID)";
                        SqlCommand insertExerciseMachine = new SqlCommand(insertExerciseMachineQuery, conn);
                        insertExerciseMachine.Parameters.AddWithValue("@ExerciseId", exerciseid);
                        insertExerciseMachine.Parameters.AddWithValue("@MachineID", machineid);
                        insertExerciseMachine.ExecuteNonQuery();

                        MessageBox.Show("Machine Added to the Exercise.");
                    }
                    else
                    {
                        MessageBox.Show("Selected machine does not exist.");
                    }
                }
                else
                {
                    MessageBox.Show("Selected exercise does not exist.");
                }
            }

        }

        private void button27_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string workoutPlanName = comboBox19.SelectedItem?.ToString();
            string exerciseName = comboBox20.SelectedItem?.ToString();

            // Check if workout plan name or exercise name is empty or null
            if (string.IsNullOrEmpty(workoutPlanName) || string.IsNullOrEmpty(exerciseName))
            {
                MessageBox.Show("Please select both a workout plan and an exercise.");
            }
            else
            {
                string getWorkoutPlanIdQuery = "SELECT workoutPlanID FROM workoutPlan WHERE workoutplanName = @WorkoutPlanName";
                SqlCommand workoutPlanIdCmd = new SqlCommand(getWorkoutPlanIdQuery, conn);
                workoutPlanIdCmd.Parameters.AddWithValue("@WorkoutPlanName", workoutPlanName);
                object queryreturn = workoutPlanIdCmd.ExecuteScalar();

                // Check if workout plan exists
                if (queryreturn != null)
                {
                    int workoutplanid = Int32.Parse(queryreturn.ToString());

                    string getExerciseIdQuery = "SELECT exerciseid FROM exercise WHERE [name] = @ExerciseName";
                    SqlCommand exerciseIdCmd = new SqlCommand(getExerciseIdQuery, conn);
                    exerciseIdCmd.Parameters.AddWithValue("@ExerciseName", exerciseName);
                    object queryreturn1 = exerciseIdCmd.ExecuteScalar();

                    // Check if exercise exists
                    if (queryreturn1 != null)
                    {
                        int exerciseid = Int32.Parse(queryreturn1.ToString());

                        string insertWorkoutExerciseQuery = "INSERT INTO Workout_Exercise (WorkoutPlanId, ExerciseId) VALUES (@WorkoutPlanId, @ExerciseId)";
                        SqlCommand insertWorkoutExerciseCmd = new SqlCommand(insertWorkoutExerciseQuery, conn);
                        insertWorkoutExerciseCmd.Parameters.AddWithValue("@WorkoutPlanId", workoutplanid);
                        insertWorkoutExerciseCmd.Parameters.AddWithValue("@ExerciseId", exerciseid);
                        insertWorkoutExerciseCmd.ExecuteNonQuery();

                        MessageBox.Show("Exercise added to the Workout Plan.");
                    }
                    else
                    {
                        MessageBox.Show("Selected exercise does not exist.");
                    }
                }
                else
                {
                    MessageBox.Show("Selected workout plan does not exist.");
                }
            }


        }

        private void comboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();
    
            string username = UserLogin.userName;
            string trainerName = comboBox1.SelectedItem.ToString();            
            int rating = Int32.Parse(numericUpDown1.Value.ToString());
            string review = richTextBox1.Text;

            string getTrainerid = "select userid from users where username = '" + trainerName + "'";
            SqlCommand cm = new SqlCommand(getTrainerid, conn);
            object queryreturn1 = cm.ExecuteScalar();
            int trainerid = Int32.Parse(queryreturn1.ToString());

            string getMemberid = "select userid from users where username = '" + username + "'";
            SqlCommand cm1 = new SqlCommand(getMemberid, conn);
            object queryreturn2 = cm1.ExecuteScalar();
            int memberid = Int32.Parse(queryreturn2.ToString());

            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string insertQuery = "INSERT INTO feedback (memberid, trainerid, dateandtime, rating, review) VALUES (@memberid, @trainerid, @dateandtime, @rating, @review)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
            insertCmd.Parameters.AddWithValue("@memberid", memberid);
            insertCmd.Parameters.AddWithValue("@trainerid", trainerid);
            insertCmd.Parameters.AddWithValue("@dateandtime", currentDateTime);
            insertCmd.Parameters.AddWithValue("@rating", rating);
            insertCmd.Parameters.AddWithValue("@review", review);
            insertCmd.ExecuteNonQuery();

            MessageBox.Show("Trainer Feedback submitted!");
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
