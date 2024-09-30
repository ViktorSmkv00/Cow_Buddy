using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cow_Buddy2._0
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            UserNameTb.Text = "";
            UserPassTb.Text = "";
        }

        MySqlConnection Con = new MySqlConnection("server=localhost;database=cow_buddy_db;uid=root;pwd=viktor123;");
        private void button1_Click(object sender, EventArgs e)
        {
            if (UserNameTb.Text == "" || UserPassTb.Text == "")
            {
                MessageBox.Show("Enter a username or password");
            }
            else
            {
                if (PositionCb.SelectedIndex > -1)
                {
                    if (PositionCb.SelectedItem.ToString() == "Admin")
                    {
                        if (UserNameTb.Text == "Admin" && UserPassTb.Text == "Admin")
                        {
                            Employees prod = new Employees();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are an Admin, enter the correct username and password");
                            UserNameTb.Text = "";
                            UserPassTb.Text = "";
                        }
                    }
                    else
                    {
                        try
                        {
                            Con.Open();
                            MySqlDataAdapter sda = new MySqlDataAdapter("Select count(*) from EmployeeTbl where EmpName='" + UserNameTb.Text + "' and EmpPass='" + UserPassTb.Text + "'", Con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows[0][0].ToString() == "1")
                            {
                                Cows cow = new Cows();
                                cow.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Incorect username or password. Please try again.");
                                UserNameTb.Text = "";
                                UserPassTb.Text = "";
                            }
                            Con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            Con.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Choose position");
                }
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
