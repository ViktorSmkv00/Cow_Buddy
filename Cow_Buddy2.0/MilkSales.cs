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
    public partial class MilkSales : Form
    {
        MySqlConnection Con = new MySqlConnection("server=localhost;database=cow_buddy_db;uid=root;pwd=viktor123;");
        public MilkSales()
        {
            InitializeComponent();
            FillEmpId();
            populate();
        }

        private void label13_Click(object sender, EventArgs e)
        {

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

        private void label5_Click(object sender, EventArgs e)
        {
            Cows Ob = new Cows();
            Ob.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MilkProduction Ob = new MilkProduction();
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

        private void label15_Click(object sender, EventArgs e)
        {
            Finances Ob = new Finances();
            Ob.Show();
            this.Hide();
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

        private void MilkSales_Load(object sender, EventArgs e)
        {

        }

        

        private void FillEmpId()
        {
            Con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT EmpId FROM EmployeeTbl", Con);
            MySqlDataReader Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Load(Rdr);
            EmpIdCb.ValueMember = "EmpId";
            EmpIdCb.DataSource = dt;
            Con.Close();
        }


        private void populate()
        {
            Con.Open();
            string query = "SELECT * FROM MilkSalesTbl";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SalesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Clear()
        {
            LPriceTb.Text = "";
            QuantityTb.Text = "";
            ClientNameTb.Text = "";
            ClientPhoneTb.Text = "";
            AmountTb.Text = "";
        }

        private void SaveTransaction()
        {
            try
            {
                string Sales = "Sales";
                Con.Open();
                string Query = "INSERT INTO IncomeTbl (Date, Type, Amount, EmpId) VALUES ('" + Date.Value.Date.ToString("yyyy-MM-dd") + "','" + Sales + "'," + AmountTb.Text + "," + EmpIdCb.SelectedValue.ToString() + ")";
                MySqlCommand cmd = new MySqlCommand(Query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Успешно запазване на приход");
                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (EmpIdCb.SelectedIndex == -1 || LPriceTb.Text == "" || ClientNameTb.Text == "" || ClientPhoneTb.Text == "" || QuantityTb.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "INSERT INTO MilkSalesTbl (Date, LPrice, ClientName, ClientPhone, EmpId, Quantity, Amount) VALUES ('" + Date.Value.Date.ToString("yyyy-MM-dd") + "'," + LPriceTb.Text + ",'" + ClientNameTb.Text + "','" + ClientPhoneTb.Text + "'," + EmpIdCb.SelectedValue.ToString() + "," + QuantityTb.Text + "," + AmountTb.Text + ")";
                    MySqlCommand cmd = new MySqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Успешна продажба");
                    Con.Close();
                    populate();
                    SaveTransaction();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void QuantityTb_Leave_1(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(LPriceTb.Text) * Convert.ToInt32(QuantityTb.Text);
            AmountTb.Text = "" + total;
        }
    }
}
