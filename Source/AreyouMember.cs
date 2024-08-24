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

namespace Forms
{
    public partial class Are_you_Member : Form
    {
        public Are_you_Member()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MemberSignUp m = new MemberSignUp();
            m.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserSignup m = new UserSignup();
            m.Show();
            this.Close();
        }

        private void Are_you_Member_Load(object sender, EventArgs e)
        {

        }
    }
}
