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
    public partial class CowHealth : Form
    {
        MySqlConnection Con = new MySqlConnection("server=localhost;database=cow_buddy_db;uid=root;pwd=viktor123;");
        private int key = 0;
        public CowHealth()
        {
            InitializeComponent();
            FillCowId();
            populate();
        }

        private void FillCowId()
        {
            Con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT CowId FROM CowTbl", Con);
            MySqlDataReader Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CowId", typeof(int));
            dt.Load(Rdr);
            IdKravaCb.ValueMember = "CowId";
            IdKravaCb.DataSource = dt;
            Con.Close();
        }

        private void populate()
        {
            Con.Open();
            string query = "SELECT * FROM HealthTbl";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            HealthDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void GetCowName()
        {
            Con.Open();
            string query = "SELECT * FROM CowTbl WHERE CowId=" + IdKravaCb.SelectedValue.ToString();
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

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowName();
        }

        private void Clear()
        {
            CowNameTb.Text = "";
            EventTb.Text = "";
            CostTb.Text = "";
            DiagnoseTb.Text = "";
            VetNameTb.Text = "";
            TreatmentTb.Text = "";
            key = 0;
        }

        

        private void label15_Click(object sender, EventArgs e)
        {
            Finances Ob = new Finances();
            Ob.Show();
            this.Hide();
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

        private void CowHealth_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (IdKravaCb.SelectedIndex == -1 || CowNameTb.Text == "" || EventTb.Text == "" || CostTb.Text == "" || VetNameTb.Text == "" || DiagnoseTb.Text == "" || TreatmentTb.Text == "")
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "INSERT INTO HealthTbl VALUES(" + IdKravaCb.SelectedValue.ToString() + ",'" + CowNameTb.Text + "','" + Date.Value.Date.ToString("yyyy-MM-dd") + "','" + EventTb.Text + "','" + DiagnoseTb.Text + "','" + TreatmentTb.Text + "'," + CostTb.Text + ",'" + VetNameTb.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Здравословен проблем запазен");

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

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (IdKravaCb.SelectedIndex == -1 || CowNameTb.Text == "" || EventTb.Text == "" || CostTb.Text == "" || VetNameTb.Text == "" || DiagnoseTb.Text == "" || TreatmentTb.Text == "")
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "UPDATE HealthTbl SET CowId=" + IdKravaCb.SelectedValue.ToString() + ",cowname='" + CowNameTb.Text + "',RepDate='" + Date.Value.Date.ToString("yyyy-MM-dd") + "',Event='" + EventTb.Text + "',Diagnosis='" + DiagnoseTb.Text + "',Treatment='" + TreatmentTb.Text + "',Cost=" + CostTb.Text + ",VetName='" + VetNameTb.Text + "' WHERE RepId=" + key + ";";
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

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Избиране на доклад за изтриване");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "DELETE FROM HealthTbl WHERE RepId=" + key + ";";
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void HealthDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IdKravaCb.SelectedValue = HealthDGV.SelectedRows[0].Cells[1].Value.ToString();
            CowNameTb.Text = HealthDGV.SelectedRows[0].Cells[2].Value.ToString();
            Date.Text = HealthDGV.SelectedRows[0].Cells[3].Value.ToString();
            EventTb.Text = HealthDGV.SelectedRows[0].Cells[4].Value.ToString();
            DiagnoseTb.Text = HealthDGV.SelectedRows[0].Cells[5].Value.ToString();
            TreatmentTb.Text = HealthDGV.SelectedRows[0].Cells[6].Value.ToString();
            CostTb.Text = HealthDGV.SelectedRows[0].Cells[7].Value.ToString();
            VetNameTb.Text = HealthDGV.SelectedRows[0].Cells[8].Value.ToString();
            if (CowNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(HealthDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void IdKravaCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CowNameTb_Click(object sender, EventArgs e)
        {

        }

        private void IdKravaCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowName();
        }
    }
}
