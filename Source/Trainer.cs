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
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Forms
{
    public partial class Trainer : Form
    {
        public Trainer()
        {
            InitializeComponent();

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();
            string username = UserLogin.userName;

            string query = "select * from users where username = '" + username + "'";
            SqlCommand cm = new SqlCommand(query, conn);
            SqlDataReader rdr = cm.ExecuteReader();

            if (rdr.Read())
            {
                string contact = rdr["contact"].ToString();
                string fname = rdr["fname"].ToString();
                string lname = rdr["lname"].ToString();

                label3.Text = username;
                label8.Text = contact;
                label9.Text = fname;
                label10.Text = lname;
            }
            rdr.Close();

            string query4 = "select gymName from gym where isApproved = 1";
            SqlCommand command = new SqlCommand(query4, conn);
            SqlDataReader reader = command.ExecuteReader();
            comboBox8.Items.Clear();
            while (reader.Read())
            {
                comboBox8.Items.Add(reader["gymName"].ToString());
            }
            reader.Close();

            string query3 = "select appointmentid, trainerid, memberid, date from appointment where isAccepted = 1";
            SqlDataAdapter adapter1 = new SqlDataAdapter(query3, conn);
            DataTable dt1 = new DataTable();
            adapter1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            dataGridView1.ReadOnly = true;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView1.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView1.RowHeadersVisible = false;

            string query5 = "select appointment.appointmentid, users.username, appointment.trainerid, appointment.date from appointment join users on users.userid = appointment.memberid join member on member.memberid = users.userid join gym on gym.gymName = member.gymName where appointment.isAccepted = 0 AND gym.isApproved = 1";
            SqlDataAdapter adapter2 = new SqlDataAdapter(query5, conn);
            DataTable dt2 = new DataTable();
            adapter2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            dataGridView2.ReadOnly = true;
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 8); 
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8); 
            dataGridView2.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView2.RowHeadersVisible = false;

            //received in 3
            string traineridq = "select userid from users where username = '" + username + "'";
            SqlCommand tid = new SqlCommand(traineridq,conn);
            object tidqt = tid.ExecuteScalar();
            string trainerid = tidqt.ToString();
            string feedbackq = "select memberid, dateandtime, rating, review from feedback where trainerid = " + trainerid;
            SqlDataAdapter adapter5 = new SqlDataAdapter(feedbackq, conn);
            DataTable dt5 = new DataTable();
            adapter5.Fill(dt5);
            dataGridView3.DataSource = dt5;
            dataGridView3.ReadOnly = true;
            dataGridView3.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView3.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView3.RowHeadersVisible = false;


            string query6 = "select * from dietplan";
            SqlDataAdapter adapter3 = new SqlDataAdapter(query6, conn);
            DataTable dt3 = new DataTable();
            adapter3.Fill(dt3);
            dataGridView4.DataSource = dt3;
            dataGridView4.ReadOnly = true;
            dataGridView4.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView4.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView4.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView4.RowHeadersVisible = false;

            string query7 = "select * from workoutplan";
            SqlDataAdapter adapter4 = new SqlDataAdapter(query7, conn);
            DataTable dt4 = new DataTable();
            adapter4.Fill(dt4);
            dataGridView5.DataSource = dt4;
            dataGridView5.ReadOnly = true;
            dataGridView5.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView5.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView5.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView5.RowHeadersVisible = false;

            string query9 = "select allergenname from allergen";
            SqlCommand cmd2 = new SqlCommand(query9, conn);
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

            string query10 = "select [name] from exercise";
            SqlCommand cmd4 = new SqlCommand(query10, conn);
            SqlDataReader reader6 = cmd4.ExecuteReader();
            comboBox18.Items.Clear();
            comboBox20.Items.Clear();
            while (reader6.Read())
            {
                comboBox18.Items.Add(reader6["name"].ToString());
                comboBox20.Items.Add(reader6["name"].ToString());
            }
            reader6.Close();

            //only machine names of gym of trainer
            string query11 = "SELECT machine.name FROM machine JOIN gym ON machine.gymId = gym.gymId JOIN trainer_gym ON gym.gymid = trainer_gym.gymid JOIN users ON trainer_gym.trainerid = users.userid WHERE users.username = '" + username + "'";
            SqlCommand cmd5 = new SqlCommand(query11, conn);
            SqlDataReader reader7 = cmd5.ExecuteReader();
            comboBox17.Items.Clear();
            while (reader7.Read())
            {
                comboBox17.Items.Add(reader7["name"].ToString());
            }
            reader7.Close();

            string query12 = "select dietplanname from dietplan";
            SqlCommand cmd = new SqlCommand(query12, conn);
            SqlDataReader reader2 = cmd.ExecuteReader();
            comboBox15.Items.Clear();
            while (reader2.Read())
            {
                comboBox15.Items.Add(reader2["dietplanname"].ToString());
            }
            reader2.Close();

            string query13 = "select workoutplanname from workoutplan";
            SqlCommand cmd1 = new SqlCommand(query13, conn);
            SqlDataReader reader3 = cmd1.ExecuteReader();
            comboBox19.Items.Clear();
            while (reader3.Read())
            {
                comboBox19.Items.Add(reader3["workoutplanname"].ToString());
            }
            reader3.Close();

            string query14 = "SELECT u.username FROM member m JOIN users u ON m.memberid = u.userid JOIN gym g ON m.gymName = g.gymName WHERE u.isActive = 1 AND g.GymId = (SELECT distinct tg.gymid  FROM trainer_gym tg JOIN users u ON tg.trainerid = u.userid WHERE u.username = '" + username + "')";
            SqlCommand cm2 = new SqlCommand(query14, conn);
            SqlDataReader rdr1 = cm2.ExecuteReader();
            comboBox2.Items.Clear();
            while (rdr1.Read())
            {
                comboBox2.Items.Add(rdr1["username"].ToString());
            }


        }

        private void Member_Load(object sender, EventArgs e)
        {

        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Starter starter = new Starter();
            starter.Show();
            this.Close();
        }

        private void tabPage14_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox33_TextChanged(object sender, EventArgs e)
        {

        }

        private void label36_Click_1(object sender, EventArgs e)
        {

        }

        private void richTextBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage11_Click(object sender, EventArgs e)
        {

        }

        private void tabPage17_Click(object sender, EventArgs e)
        {

        }

        private void tabPage16_Click(object sender, EventArgs e)
        {

        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string username = UserLogin.userName;

            // Check if a gym is selected in the comboBox8
            if (comboBox8.SelectedItem == null)
            {
                MessageBox.Show("Please select a gym.");
                return;
            }

            string gymname = comboBox8.SelectedItem.ToString();

            // Proceed with the rest of the code to fetch trainerid and gymid
            // Insertion of trainer request
            string query = "select userid from users where username = '" + username + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            object queryreturn = cmd.ExecuteScalar();
            int trainerid = Int32.Parse(queryreturn.ToString());

            string query1 = "select gymid from gym where isApproved = 1 AND gymName = '" + gymname + "'";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            object queryreturn1 = cmd1.ExecuteScalar();
            int gymid = Int32.Parse(queryreturn1.ToString());

            string query2 = "INSERT INTO trainer_request (trainerid, gymid, status) VALUES (@trainerid, @gymid, @status)";
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            cmd2.Parameters.AddWithValue("@trainerid", trainerid);
            cmd2.Parameters.AddWithValue("@gymid", gymid);
            cmd2.Parameters.AddWithValue("@status", "Pending");
            cmd2.ExecuteNonQuery();
            MessageBox.Show("Application sent for approval.");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();
            string username = UserLogin.userName;

            string query1 = "select userid from users where username = '" + username + "'";
            SqlCommand cmd = new SqlCommand(query1, conn);
            object queryreturn = cmd.ExecuteScalar();
            int trainerid = Int32.Parse(queryreturn.ToString());

            // Check if appointmentid is provided and is a valid integer
            if (!int.TryParse(richTextBox1.Text, out int appointmentid))
            {
                MessageBox.Show("Please enter a valid appointment ID.");
                return;
            }

            // Check if the appointment exists in the appointment table
            string query2 = "select trainerid from appointment where appointmentid = @AppointmentID";
            SqlCommand sqlCommand = new SqlCommand(query2, conn);
            sqlCommand.Parameters.AddWithValue("@AppointmentID", appointmentid);
            object queryreturn1 = sqlCommand.ExecuteScalar();

            if (queryreturn1 == null || queryreturn1 == DBNull.Value)
            {
                MessageBox.Show("Appointment does not exist.");
                return;
            }

            int check = Convert.ToInt32(queryreturn1);

            if (check == trainerid)
            {
                string query = "update appointment set isAccepted = 1, status = 'Accepted' where appointmentid = @AppointmentID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@AppointmentID", appointmentid);
                command.ExecuteNonQuery();
                MessageBox.Show("Appointment accepted.");
            }
            else
            {
                MessageBox.Show("Please select your own appointments.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();
            string username = UserLogin.userName;

            string query1 = "select userid from users where username = '" + username + "'";
            SqlCommand cmd = new SqlCommand(query1, conn);
            object queryreturn = cmd.ExecuteScalar();
            int trainerid = Int32.Parse(queryreturn.ToString());

            // Check if appointmentid is provided and is a valid integer
            if (!int.TryParse(richTextBox1.Text, out int appointmentid))
            {
                MessageBox.Show("Please enter a valid appointment ID.");
                return;
            }

            // Check if the appointment exists in the appointment table
            string query2 = "select trainerid from appointment where appointmentid = @AppointmentID";
            SqlCommand sqlCommand = new SqlCommand(query2, conn);
            sqlCommand.Parameters.AddWithValue("@AppointmentID", appointmentid);
            object queryreturn1 = sqlCommand.ExecuteScalar();

            if (queryreturn1 == null || queryreturn1 == DBNull.Value)
            {
                MessageBox.Show("Appointment does not exist.");
                return;
            }

            int check = Convert.ToInt32(queryreturn1);

            if (check == trainerid)
            {
                string query = "update appointment set isAccepted = 0, status = 'Rejected' where appointmentid = @AppointmentID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@AppointmentID", appointmentid);
                command.ExecuteNonQuery();
                MessageBox.Show("Appointment rejected.");
            }
            else
            {
                MessageBox.Show("Please select your own appointments.");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            {
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
                conn.Open();

                string allergenName = richTextBox22.Text;

                // Check if the allergen name is empty or null
                if (string.IsNullOrWhiteSpace(allergenName))
                {
                    MessageBox.Show("Please enter an allergen name.");
                    return;
                }

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

            // Check if either allergen name or meal name is null or empty
            if (string.IsNullOrEmpty(allergenName) || string.IsNullOrEmpty(mealName))
            {
                MessageBox.Show("Please select both an allergen and a meal.");
                return;
            }

            // Proceed with the insertion if both names are provided
            string insertQuery = "INSERT INTO Meal_Allergen (MealName, AllergenName) VALUES (@MealName, @AllergenName)";
            SqlCommand insertQ = new SqlCommand(insertQuery, conn);
            insertQ.Parameters.AddWithValue("@MealName", mealName);
            insertQ.Parameters.AddWithValue("@AllergenName", allergenName);

            insertQ.ExecuteNonQuery();

            MessageBox.Show("Allergen added to meal.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string mealName = richTextBox7.Text;

            // Check if the meal name is empty
            if (string.IsNullOrWhiteSpace(mealName))
            {
                MessageBox.Show("Please enter a meal name.");
                return;
            }

            string username = UserLogin.userName;
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

        private void button23_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string mealName = comboBox16.SelectedItem?.ToString();
            string dietplanName = comboBox15.SelectedItem?.ToString();

            // Check if either meal name or diet plan name is null or empty
            if (string.IsNullOrEmpty(mealName) || string.IsNullOrEmpty(dietplanName))
            {
                MessageBox.Show("Please select both a meal and a diet plan.");
                return;
            }

            string getdietplanID = "SELECT dietplanid FROM dietplan WHERE dietplanname = @DietPlanName";
            SqlCommand cm = new SqlCommand(getdietplanID, conn);
            cm.Parameters.AddWithValue("@DietPlanName", dietplanName);
            object getdietplan = cm.ExecuteScalar();

            if (getdietplan == null)
            {
                MessageBox.Show("Invalid diet plan name.");
                return;
            }

            int dietplanid = Convert.ToInt32(getdietplan);

            string insertDietPlanMealQuery = "INSERT INTO DietPlan_Meal (DietPlanId, MealName) VALUES (@DietPlanID, @MealName)";
            SqlCommand insertDietPlanMealCommand = new SqlCommand(insertDietPlanMealQuery, conn);
            insertDietPlanMealCommand.Parameters.AddWithValue("@DietPlanID", dietplanid);
            insertDietPlanMealCommand.Parameters.AddWithValue("@MealName", mealName);

            insertDietPlanMealCommand.ExecuteNonQuery();

            MessageBox.Show("Meal added to the Plan.");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string exerciseName = richTextBox4.Text;
            string muscleGroupName = richTextBox28.Text;

            // Check if either exercise name or muscle group is null or empty
            if (string.IsNullOrEmpty(exerciseName) || string.IsNullOrEmpty(muscleGroupName))
            {
                MessageBox.Show("Please enter both the exercise name and muscle group.");
                return;
            }

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

        private void button17_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string username = UserLogin.userName;
            string exerciseName = comboBox18.SelectedItem?.ToString();
            string machineName = comboBox17.SelectedItem?.ToString();

            // Check if either machine name or exercise name is null or empty
            if (string.IsNullOrEmpty(machineName) || string.IsNullOrEmpty(exerciseName))
            {
                MessageBox.Show("Please select both the exercise and the machine.");
                return;
            }

            string getExerciseId = "SELECT exerciseid FROM exercise WHERE [name] = @ExerciseName";
            SqlCommand exerciseIdquery = new SqlCommand(getExerciseId, conn);
            exerciseIdquery.Parameters.AddWithValue("@ExerciseName", exerciseName);
            object queryreturn = exerciseIdquery.ExecuteScalar();

            string getMachineId = "SELECT machineid FROM machine WHERE [name] = @MachineName";
            SqlCommand machineIdquery = new SqlCommand(getMachineId, conn);
            machineIdquery.Parameters.AddWithValue("@MachineName", machineName);
            object queryreturn1 = machineIdquery.ExecuteScalar();

            if (queryreturn == null)
            {
                MessageBox.Show("The selected exercise does not exist.");
                return;
            }

            if (queryreturn1 == null)
            {
                MessageBox.Show("The selected machine does not exist.");
                return;
            }

            int exerciseid = Convert.ToInt32(queryreturn);
            int machineid = Convert.ToInt32(queryreturn1);

            string insertExerciseMachineQuery = "INSERT INTO Exercise_Machine (exerciseid, machineid) VALUES (@ExerciseId, @MachineID)";
            SqlCommand insertExerciseMachine = new SqlCommand(insertExerciseMachineQuery, conn);
            insertExerciseMachine.Parameters.AddWithValue("@ExerciseId", exerciseid);
            insertExerciseMachine.Parameters.AddWithValue("@MachineID", machineid);
            insertExerciseMachine.ExecuteNonQuery();

            MessageBox.Show("Machine Added to the Exercise.");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string workoutPlanName = comboBox19.SelectedItem?.ToString();
            string exerciseName = comboBox20.SelectedItem?.ToString();

            // Check if either exercise name or workout plan name is null or empty
            if (string.IsNullOrEmpty(exerciseName) || string.IsNullOrEmpty(workoutPlanName))
            {
                MessageBox.Show("Please select both the exercise and the workout plan.");
                return;
            }

            string getWorkoutPlanIdQuery = "SELECT workoutPlanID FROM workoutPlan WHERE workoutplanName = @WorkoutPlanName";
            SqlCommand workoutPlanIdCmd = new SqlCommand(getWorkoutPlanIdQuery, conn);
            workoutPlanIdCmd.Parameters.AddWithValue("@WorkoutPlanName", workoutPlanName);
            object queryreturn = workoutPlanIdCmd.ExecuteScalar();

            string getExerciseIdQuery = "SELECT exerciseid FROM exercise WHERE [name] = @ExerciseName";
            SqlCommand exerciseIdCmd = new SqlCommand(getExerciseIdQuery, conn);
            exerciseIdCmd.Parameters.AddWithValue("@ExerciseName", exerciseName);
            object queryreturn1 = exerciseIdCmd.ExecuteScalar();

            // Check if either exercise ID or workout plan ID is null
            if (queryreturn == null || queryreturn1 == null)
            {
                MessageBox.Show("Selected exercise or workout plan does not exist.");
                return;
            }

            int workoutplanid = Convert.ToInt32(queryreturn);
            int exerciseid = Convert.ToInt32(queryreturn1);

            string insertWorkoutExerciseQuery = "INSERT INTO Workout_Exercise (WorkoutPlanId, ExerciseId) VALUES (@WorkoutPlanId, @ExerciseId)";
            SqlCommand insertWorkoutExerciseCmd = new SqlCommand(insertWorkoutExerciseQuery, conn);
            insertWorkoutExerciseCmd.Parameters.AddWithValue("@WorkoutPlanId", workoutplanid);
            insertWorkoutExerciseCmd.Parameters.AddWithValue("@ExerciseId", exerciseid);
            insertWorkoutExerciseCmd.ExecuteNonQuery();

            MessageBox.Show("Exercise added to the Workout Plan.");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string memberName = comboBox2.SelectedItem?.ToString();
            DateTime dateTime = dateTimePicker1.Value;
            string username = UserLogin.userName;

            // Validate if trainer name and date time are not empty or null
            if (string.IsNullOrEmpty(memberName))
            {
                MessageBox.Show("Please select a member.");
                return;
            }

            if (dateTime == DateTime.MinValue)
            {
                MessageBox.Show("Please select a valid appointment date.");
                return;
            }

            string query = "select memberid from member join users on users.userid = member.memberid where username = '" + memberName + "'";
            SqlCommand cm = new SqlCommand(query, conn);
            object queryreturn = cm.ExecuteScalar();

            if (queryreturn == null)
            {
                MessageBox.Show("Invalid member.");
                return;
            }

            string memberid = queryreturn.ToString();

            string query1 = "select trainerid from trainer join users on users.userid = trainer.trainerid where username = '" + username + "'";
            SqlCommand cm1 = new SqlCommand(query1, conn);
            object queryreturn1 = cm1.ExecuteScalar();

            if (queryreturn1 == null || queryreturn1 == DBNull.Value)
            {
                MessageBox.Show("Invalid trainer.");
                return;
            }

            string trainerid = queryreturn1.ToString();

            string insertQuery = "INSERT INTO appointment (trainerid, memberid, isAccepted, date, status) VALUES (@trainerid, @memberid, 1, @dateTime, 'Pending')";
            SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
            insertCommand.Parameters.AddWithValue("@trainerid", trainerid);
            insertCommand.Parameters.AddWithValue("@memberid", memberid);
            insertCommand.Parameters.AddWithValue("@dateTime", dateTime);
            insertCommand.ExecuteNonQuery();
            MessageBox.Show("Appointment scheduled.");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string workoutplanname = richTextBox32.Text;
            int sets, reps;

            // Validate if workout plan name is empty or null
            if (string.IsNullOrWhiteSpace(workoutplanname))
            {
                MessageBox.Show("Please enter a workout plan name.");
                return;
            }

            // Validate if sets is a numeric value
            if (!int.TryParse(richTextBox30.Text, out sets))
            {
                MessageBox.Show("Please enter a valid numeric value for sets.");
                return;
            }

            // Validate if reps is a numeric value
            if (!int.TryParse(richTextBox29.Text, out reps))
            {
                MessageBox.Show("Please enter a valid numeric value for reps.");
                return;
            }

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

        private void button8_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            string dietplanname = richTextBox5.Text;
            string plantype = richTextBox10.Text;
            string purpose = richTextBox20.Text;
            int nutrivalue;

            // Validate if diet plan name is empty or null
            if (string.IsNullOrWhiteSpace(dietplanname))
            {
                MessageBox.Show("Please enter a diet plan name.");
                return;
            }

            // Validate if nutrivalue is a valid integer
            if (!int.TryParse(richTextBox9.Text, out nutrivalue))
            {
                MessageBox.Show("Please enter a valid numeric value for Nutritional Value.");
                return;
            }

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
    }
}
