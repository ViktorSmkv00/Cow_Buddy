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
    public partial class Employees : Form
    {
        MySqlConnection Con = new MySqlConnection("server=localhost;database=cow_buddy_db;uid=root;pwd=viktor123;");
        private int key = 0;
        public Employees()
        {
            InitializeComponent();
            populate();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Login Ob = new Login();
            Ob.Show();
            this.Hide();
        }

        private void Employees_Load(object sender, EventArgs e)
        {

        }

        private void populate()
        {
            Con.Open();
            string query = "select * from EmployeeTbl";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Clear()
        {
            EmpTelTb.Text = "";
            EmpNameTb.Text = "";
            AddressTb.Text = "";
            GenderCb.SelectedIndex = -1;
            key = 0;
            EmpPassTb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || GenderCb.SelectedIndex == -1 || EmpTelTb.Text == "" || AddressTb.Text == "" || EmpPassTb.Text == "")
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into EmployeeTbl(EmpName, EmpDOB, Gender, Phone, Address, EmpPass) values(N'" + EmpNameTb.Text + "','" + DOB.Value.Date.ToString("yyyy-MM-dd") + "',N'" + GenderCb.SelectedItem.ToString() + "','" + EmpTelTb.Text + "',N'" + AddressTb.Text + "','" + EmpPassTb.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запазване успешно");

                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || GenderCb.SelectedIndex == -1 || EmpTelTb.Text == "" || AddressTb.Text == "")
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update EmployeeTbl set EmpName=N'" + EmpNameTb.Text + "',EmpDOB='" + DOB.Value.Date.ToString("yyyy-MM-dd") + "',Gender=N'" + GenderCb.SelectedItem.ToString() + "',Phone='" + EmpTelTb.Text + "',Address=N'" + AddressTb.Text + "',EmpPass='" + EmpPassTb.Text + "' where Empid=" + key + ";";
                    MySqlCommand cmd = new MySqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Актуализиране успешно");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Избиране на служител да бъде изтрит");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from EmployeeTbl where EmpId=" + key + ";";
                    MySqlCommand cmd = new MySqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Изтриване успешно");

                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            DOB.Text = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenderCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpTelTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
            AddressTb.Text = EmployeeDGV.SelectedRows[0].Cells[5].Value.ToString();
            EmpPassTb.Text = EmployeeDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (EmpNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
