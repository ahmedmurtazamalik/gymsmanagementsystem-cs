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

namespace Forms
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            con.Open();
            string query1 = "SELECT gymName FROM GYM WHERE isApproved = 0";
            SqlCommand command = new SqlCommand(query1, con);
            SqlDataReader reader = command.ExecuteReader();
            
            comboBox5.Items.Clear();
            while (reader.Read()) {

                comboBox5.Items.Add(reader["gymName"].ToString());
            }
            reader.Close();
            string query2 = "SELECT gymName FROM GYM WHERE isApproved = 1";
            SqlCommand command2 = new SqlCommand(query2, con);
            SqlDataReader reader2 = command2.ExecuteReader();

            comboBox1.Items.Clear();
            while (reader2.Read())
            {

                comboBox1.Items.Add(reader2["gymName"].ToString());
            }
            reader2.Close();

            string query3 = "Select dietplan.dietplanName,meal.calories from dietplan join DietPlan_Meal on dietplan.dietplanId = DietPlan_Meal.DietPlanId join Meal on DietPlan_Meal.MealName = meal.MealName where Meal.MealName = 'Breakfast' and meal.Calories < 500";
            SqlDataAdapter adapter1 = new SqlDataAdapter(query3, con);
            DataTable dt1 = new DataTable();
            adapter1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            dataGridView1.ReadOnly = true;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView1.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView1.RowHeadersVisible = false;

            string query4 = "SELECT dp.dietplanId, dp.dietplanName,totalCarbs FROM DietPlan dp JOIN ( SELECT dietplanid, SUM(Carbs) AS TotalCarbs   FROM Meal m JOIN DietPlan_Meal dpm ON m.MealName = dpm.MealName  GROUP BY dietplanid) AS DietPlanTotalCarbs ON dp.dietplanId = DietPlanTotalCarbs.dietplanid WHERE TotalCarbs < 300;";
            SqlDataAdapter adapter2 = new SqlDataAdapter(query4, con);
            DataTable dt2 = new DataTable();
            adapter2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            dataGridView2.ReadOnly = true;
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView2.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView2.RowHeadersVisible = false;

            string query5 = "SELECT dp.dietplanId, dp.dietplanName FROM DietPlan dp WHERE NOT EXISTS ( SELECT 1  FROM Meal_Allergen ma  WHERE ma.MealName IN ( SELECT dpm.MealName  FROM DietPlan_Meal dpm  WHERE dpm.DietPlanId = dp.dietplanId)  AND ma.AllergenName = 'peanuts');";
            SqlDataAdapter adapter3 = new SqlDataAdapter(query5, con);
            DataTable dt3 = new DataTable();
            adapter3.Fill(dt3);
            dataGridView3.DataSource = dt3;
            dataGridView3.ReadOnly = true;
            dataGridView3.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView3.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView3.RowHeadersVisible = false;

            string query6 = "SELECT ma.AllergenName, COUNT(*) AS DietPlanCount FROM Meal_Allergen ma GROUP BY ma.AllergenName ORDER BY DietPlanCount DESC;";
            SqlDataAdapter adapter4 = new SqlDataAdapter(query6, con);
            DataTable dt4 = new DataTable();
            adapter4.Fill(dt4);
            dataGridView4.DataSource = dt4;
            dataGridView4.ReadOnly = true;
            dataGridView4.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView4.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView4.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView4.RowHeadersVisible = false;

            string query7 = "SELECT dp.dietplanName, AVG(m.Calories) AS AvgCaloriesPerMeal FROM DietPlan dp JOIN DietPlan_Meal dpm ON dp.dietplanId = dpm.DietPlanId JOIN Meal m ON dpm.MealName = m.MealName GROUP BY dp.dietplanName;";
            SqlDataAdapter adapter5 = new SqlDataAdapter(query7, con);
            DataTable dt5 = new DataTable();
            adapter5.Fill(dt5);
            dataGridView5.DataSource = dt5;
            dataGridView5.ReadOnly = true;
            dataGridView5.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView5.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView5.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView5.RowHeadersVisible = false;

            string query8 = "SELECT g.gymName FROM gym g JOIN trainer_request tr ON g.gymId = tr.gymId WHERE tr.status = 'Pending';";
            SqlDataAdapter adapter6 = new SqlDataAdapter(query8, con);
            DataTable dt6 = new DataTable();
            adapter6.Fill(dt6);
            dataGridView6.DataSource = dt6;
            dataGridView6.ReadOnly = true;
            dataGridView6.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView6.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView6.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView6.RowHeadersVisible = false;


            string query9 = "Select * from workoutplan";
            SqlDataAdapter adapter7 = new SqlDataAdapter(query9, con);
            DataTable dt7 = new DataTable();
            adapter7.Fill(dt7);
            dataGridView7.DataSource = dt7;
            dataGridView7.ReadOnly = true;
            dataGridView7.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView7.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView7.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView7.RowHeadersVisible = false;

            string query10 = "Select * from dietplan";
            SqlDataAdapter adapter8 = new SqlDataAdapter(query10, con);
            DataTable dt8 = new DataTable();
            adapter8.Fill(dt8);
            dataGridView8.DataSource = dt8;
            dataGridView8.ReadOnly = true;
            dataGridView8.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView8.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView8.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView8.RowHeadersVisible = false;

            string query11 = "SELECT gymName,members as memberCount FROM gym where members > 100";
            SqlDataAdapter adapter9 = new SqlDataAdapter(query11, con);
            DataTable dt9 = new DataTable();
            adapter9.Fill(dt9);
            dataGridView9.DataSource = dt9;
            dataGridView9.ReadOnly = true;
            dataGridView9.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView9.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView9.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView9.RowHeadersVisible = false;

            string query12 = "SELECT * FROM gym WHERE isApproved = 1;";
            SqlDataAdapter adapter10 = new SqlDataAdapter(query12, con);
            DataTable dt10 = new DataTable();
            adapter10.Fill(dt10);
            dataGridView10.DataSource = dt10;
            dataGridView10.ReadOnly = true;
            dataGridView10.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView10.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView10.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView10.RowHeadersVisible = false;

            string query13 = "SELECT fname,lname,userid,userType from users where isActive = 0";
            SqlDataAdapter adapter11 = new SqlDataAdapter(query13, con);
            DataTable dt11 = new DataTable();
            adapter11.Fill(dt11);
            dataGridView11.DataSource = dt11;
            dataGridView11.ReadOnly = true;
            dataGridView11.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView11.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView11.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView11.RowHeadersVisible = false;

            string query14 = "SELECT COUNT(*) AS PendingAppointments FROM appointment WHERE status = 'Pending';";
            SqlDataAdapter adapter12 = new SqlDataAdapter(query14, con);
            DataTable dt12 = new DataTable();
            adapter12.Fill(dt12);
            dataGridView12.DataSource = dt12;
            dataGridView12.ReadOnly = true;
            dataGridView12.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView12.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView12.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView12.RowHeadersVisible = false;

            string query15 = "SELECT t.trainerid, u.fname, u.lname FROM trainer t JOIN users u ON t.trainerid = u.userId WHERE t.trainerid NOT IN ( SELECT DISTINCT trainerid FROM feedback);";
            SqlDataAdapter adapter13 = new SqlDataAdapter(query15, con);
            DataTable dt13 = new DataTable();
            adapter13.Fill(dt13);
            dataGridView13.DataSource = dt13;
            dataGridView13.ReadOnly = true;
            dataGridView13.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView13.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView13.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8);
            dataGridView13.RowHeadersVisible = false;

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Starter st = new Starter();
            st.Show();
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            // Check if a gym is selected in the comboBox5
            if (comboBox5.SelectedItem == null)
            {
                MessageBox.Show("Please select a gym.");
                return;
            }

            string gymName = comboBox5.SelectedItem.ToString();
            string query3 = "UPDATE GYM SET isApproved = 1 WHERE gymName = @GymName";
            SqlCommand command3 = new SqlCommand(query3, conn);
            command3.Parameters.AddWithValue("@GymName", gymName);
            command3.ExecuteNonQuery();

            MessageBox.Show("Gym approved.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            // Check if a gym is selected in the comboBox1
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a gym.");
                return;
            }

            string gymName = comboBox1.SelectedItem.ToString();
            SqlCommand comm = new SqlCommand("RemoveGym", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@GymName", gymName);
            comm.ExecuteNonQuery();

            MessageBox.Show("Gym removed.");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView10_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Starter starter = new Starter();
            starter.Show();
            this.Close();
        }
    }
}
