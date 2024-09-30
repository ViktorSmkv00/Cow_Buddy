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
    public partial class MilkProduction : Form
    {
        MySqlConnection Con = new MySqlConnection("server=localhost;database=cow_buddy_db;uid=root;pwd=viktor123;");
        private int key = 0;
        public MilkProduction()
        {
            InitializeComponent();
            FillCowId();
            populate();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            
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

        private void MilkProduction_Load(object sender, EventArgs e)
        {

        }

       

        private void FillCowId()
        {
            Con.Open();
            MySqlCommand cmd = new MySqlCommand("select CowId from CowTbl", Con);
            MySqlDataReader Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CowId", typeof(int));
            dt.Load(Rdr);
            CowId.ValueMember = "CowId";
            CowId.DataSource = dt;
            Con.Close();
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from MilkTbl";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MilkDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Clear()
        {
            CowNameTb.Text = "";
            AmMilkTb.Text = "";
            NoonMilkTb.Text = "";
            PmMilkTb.Text = "";
            TotalMilkTb.Text = "";
            key = 0;
        }

        private void GetCowName()
        {
            Con.Open();
            string query = "select * from CowTbl where CowId=" + CowId.SelectedValue.ToString() + "";
            MySqlCommand cmd = new MySqlCommand(query, Con);
            DataTable dt = new DataTable();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CowNameTb.Text = dr["CowName"].ToString();
            }
            Con.Close();
        }

       

        private void PmTb_Leave(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(AmMilkTb.Text) + Convert.ToInt32(PmMilkTb.Text) + Convert.ToInt32(NoonMilkTb.Text);
            TotalMilkTb.Text = "" + total;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (CowId.SelectedIndex == -1 || CowNameTb.Text == "" || AmMilkTb.Text == "" || PmMilkTb.Text == "" || NoonMilkTb.Text == "" || TotalMilkTb.Text == "")
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into MilkTbl (CowId, CowName, AmMilk, NoonMilk, PmMilk, TotalMilk, DateProd) values(" + CowId.SelectedValue.ToString() + ",'" + CowNameTb.Text + "'," + AmMilkTb.Text + "," + NoonMilkTb.Text + "," + PmMilkTb.Text + "," + TotalMilkTb.Text + ",'" + Date.Value.Date.ToString("yyyy-MM-dd") + "')";
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
                    Con.Close();
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (CowId.SelectedIndex == -1 || CowNameTb.Text == "" || AmMilkTb.Text == "" || PmMilkTb.Text == "" || NoonMilkTb.Text == "" || TotalMilkTb.Text == "")
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update MilkTbl set CowName='" + CowNameTb.Text + "',AmMilk=" + AmMilkTb.Text + ",NoonMilk=" + NoonMilkTb.Text + ",PmMilk=" + PmMilkTb.Text + ",TotalMilk=" + TotalMilkTb.Text + ",DateProd='" + Date.Value.Date.ToString("yyyy-MM-dd") + "' where Mid=" + key + ";";
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
                    Con.Close();
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Избор на млечен продукт да бъде изтрит");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from MilkTbl where MId=" + key + ";";
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
                    Con.Close();
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void MilkDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CowId.SelectedValue = MilkDGV.SelectedRows[0].Cells[1].Value.ToString();
            CowNameTb.Text = MilkDGV.SelectedRows[0].Cells[2].Value.ToString();
            AmMilkTb.Text = MilkDGV.SelectedRows[0].Cells[3].Value.ToString();
            NoonMilkTb.Text = MilkDGV.SelectedRows[0].Cells[4].Value.ToString();
            PmMilkTb.Text = MilkDGV.SelectedRows[0].Cells[5].Value.ToString();
            TotalMilkTb.Text = MilkDGV.SelectedRows[0].Cells[6].Value.ToString();
            Date.Text = MilkDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (CowNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(MilkDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void CowId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowName();
        }

        private void PmMilkTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void PmMilkTb_Leave(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(AmMilkTb.Text) + Convert.ToInt32(PmMilkTb.Text) + Convert.ToInt32(NoonMilkTb.Text);
            TotalMilkTb.Text = "" + total;
        }
    }
}
