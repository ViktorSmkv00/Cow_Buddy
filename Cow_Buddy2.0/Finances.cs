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
    public partial class Finances : Form
    {
        MySqlConnection Con = new MySqlConnection("server=localhost;database=cow_buddy_db;uid=root;pwd=viktor123;");
        public Finances()
        {
            InitializeComponent();
            populateExp();
            populateInc();
            FillEmpId();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MilkProduction Ob = new MilkProduction();
            Ob.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Cows Ob = new Cows();
            Ob.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            CowHealth Ob = new CowHealth();
            Ob.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Breedings Ob = new Breedings();
            Ob.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            MilkSales Ob = new MilkSales();
            Ob.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Login Ob = new Login();
            Ob.Show();
            this.Hide();
        }

        private void Finances_Load(object sender, EventArgs e)
        {

        }

        private void populateExp()
        {
            Con.Open();
            string query = "select * from ExpenditureTbl";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExpDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void populateInc()
        {
            Con.Open();
            string query = "select * from IncomeTbl";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            IncDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void FilterIncome()
        {
            Con.Open();
            string query = "select * from IncomeTbl where IncDate='" + IncomeDateFilter.Value.Date.ToString("yyyy-MM-dd") + "'";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            IncDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void FilterExp()
        {
            Con.Open();
            string query = "select * from ExpenditureTbl where ExpDate='" + ExpDateFilter.Value.Date.ToString("yyyy-MM-dd") + "'";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExpDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ClearExp()
        {
            ExpAmtTb.Text = "";
        }


        private void FillEmpId()
        {
            Con.Open();
            MySqlCommand cmd = new MySqlCommand("select EmpId from EmployeeTbl", Con);
            MySqlDataReader Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Load(Rdr);
            IdSlujitelCb.ValueMember = "EmpId";
            IdSlujitelCb.DataSource = dt;
            Con.Close();
        }

        private void ClearInc()
        {
            IncPurposeCb.SelectedIndex = -1;
            IncAmtTb.Text = "";
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (ExpPurposeCb.SelectedIndex == -1 || ExpAmtTb.Text == "" || IdSlujitelCb.SelectedIndex == -1)
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into ExpenditureTbl(ExpDate, ExpPurpose, ExpAmt, EmpId) values('" + ExpDate.Value.Date.ToString("yyyy-MM-dd") + "','" + ExpPurposeCb.SelectedItem.ToString() + "'," + ExpAmtTb.Text + "," + IdSlujitelCb.SelectedValue.ToString() + ")";
                    MySqlCommand cmd = new MySqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Успешно запазване на разход");

                    Con.Close();
                    populateExp();
                    ClearExp();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (IncPurposeCb.SelectedIndex == -1 || IncAmtTb.Text == "" || IdSlujitelCb.SelectedIndex == -1)
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into IncomeTbl(IncDate, IncPurpose, IncAmt, EmpId) values('" + IncomeDate.Value.Date.ToString("yyyy-MM-dd") + "','" + IncPurposeCb.SelectedItem.ToString() + "'," + IncAmtTb.Text + "," + IdSlujitelCb.SelectedValue.ToString() + ")";
                    MySqlCommand cmd = new MySqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Успешно запазване на приход");

                    Con.Close();
                    populateInc();
                    ClearInc();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {
            populateInc();
        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {
            populateExp();
        }

        private void ExpDateFilter_ValueChanged_1(object sender, EventArgs e)
        {
            FilterExp();
        }

        private void IncomeDateFilter_ValueChanged(object sender, EventArgs e)
        {
            FilterIncome();
        }
    }
}
