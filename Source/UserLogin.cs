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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Member1;

namespace Forms
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        public static string userName { get; set; }

        private void MemberLogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string un = textBox1.Text;
            string enteredpw = textBox2.Text;
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BH7JLP5\\SQLEXPRESS01;Initial Catalog=project;Integrated Security=True");
            conn.Open();

            SqlCommand cm, cm2, cm3;
            string query = "select password from users where username = '" + un + "'";
            string query2 = "select usertype from users where username = '" + un + "'";
            string query3 = "select username from users where username = '" + un + "'";

            cm = new SqlCommand(query, conn);
            cm2 = new SqlCommand(query2, conn);
            cm3 = new SqlCommand(query3, conn);


            object queryreturn = cm.ExecuteScalar();
            object queryreturn2 = cm2.ExecuteScalar();
            object queryreturn3 = cm3.ExecuteScalar();

            string pw, type, uname;

            if (queryreturn == null)
            {
                pw = "";
            }
            else
            {
                pw = queryreturn.ToString();
            }
            if (queryreturn2 == null)
            {
                type = "";
            }
            else
            {
                type = queryreturn2.ToString();
            }
            if (queryreturn3 == null)
            {
                uname = "";
            }
            else
            {
                uname = queryreturn3.ToString();
            }

            if (uname == "")
            {
                MessageBox.Show("Invalid username/password.");
            }
            else if (enteredpw == "")
            {
                MessageBox.Show("Invalid username/password.");
            }
            else if (type == "member" && enteredpw == pw)
            {
                userName = uname;
                Member mem = new Member();
                mem.Show();
                this.Close();
            }
            else if (type == "admin" && enteredpw == pw) {
                userName = uname;
                Admin admin = new Admin();
                admin.Show();
                this.Close();
            }
            else if (type == "owner" && enteredpw == pw)
            {
                userName = uname;
                GymOwner owner = new GymOwner();
                owner.Show();
                this.Close();
            }
            else if (type == "trainer" && enteredpw == pw)
            {
                userName = uname;
                Trainer t = new Trainer();
                t.Show();
                this.Close();
            }
            else if (enteredpw != pw)
            {
                MessageBox.Show("Invalid username/password.");
            }



        }
    }
}
