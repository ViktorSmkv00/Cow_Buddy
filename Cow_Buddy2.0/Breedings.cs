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
    public partial class Breedings : Form
    {
        MySqlConnection Con = new MySqlConnection("server=localhost;database=cow_buddy_db;uid=root;pwd=viktor123;");
        private int key = 0;

        public Breedings()
        {
            InitializeComponent();
            populate();
            FillCowId();
        }

        private void FillCowId()
        {
            Con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT CowId FROM CowTbl", Con);
            MySqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CowId", typeof(int));
            dt.Load(Rdr);
            CowIdCb.ValueMember = "CowId";
            CowIdCb.DataSource = dt;
            Con.Close();
        }

        private void populate()
        {
            Con.Open();
            string query = "SELECT * FROM BreedTbl";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BreedDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void GetCowName()
        {
            Con.Open();
            string query = "SELECT * FROM CowTbl WHERE CowId=@CowId";
            MySqlCommand cmd = new MySqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@CowId", CowIdCb.SelectedValue);
            DataTable dt = new DataTable();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CowNameTb.Text = dr["CowName"].ToString();
                CowAgeTb.Text = dr["Age"].ToString();
            }
            Con.Close();
        }

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowName();
        }


        private void Breedings_Load(object sender, EventArgs e)
        {

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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Cows Ob = new Cows();
            Ob.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Избиране на доклад за развъждане да бъде изтрит");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "DELETE FROM BreedTbl WHERE BrId=@BrId";
                    MySqlCommand cmd = new MySqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@BrId", key);
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

        private void Clear()
        {
            CowNameTb.Text = ""; 
            RemarkTb.Text = "";  
            CowAgeTb.Text = "";  
            HeatDate.Value = DateTime.Now; 
            BreedDate.Value = DateTime.Now; 
            ExpDateCalve.Value = DateTime.Now; 
            DateCalved.Value = DateTime.Now; 
            CowIdCb.SelectedIndex = -1; 
            key = 0; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void CowIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCowName();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || RemarkTb.Text == "" || CowAgeTb.Text == "")
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "INSERT INTO BreedTbl (HeatDate, BreedDate, CowId, CowName, ExpDateCalve, DateCalved, CowAge, Remarks) VALUES (@HeatDate, @BreedDate, @CowId, @CowName, @ExpDateCalve, @DateCalved, @CowAge, @Remarks)";
                    MySqlCommand cmd = new MySqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@HeatDate", HeatDate.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@BreedDate", BreedDate.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@CowId", CowIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@CowName", CowNameTb.Text);
                    cmd.Parameters.AddWithValue("@ExpDateCalve", ExpDateCalve.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@DateCalved", DateCalved.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@CowAge", CowAgeTb.Text);
                    cmd.Parameters.AddWithValue("@Remarks", RemarkTb.Text);
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || RemarkTb.Text == "" || CowAgeTb.Text == "")
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "UPDATE BreedTbl SET HeatDate=@HeatDate, BreedDate=@BreedDate, CowId=@CowId, CowName=@CowName, ExpDateCalve=@ExpDateCalve, DateCalved=@DateCalved, CowAge=@CowAge, Remarks=@Remarks WHERE BrId=@BrId";
                    MySqlCommand cmd = new MySqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@HeatDate", HeatDate.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@BreedDate", BreedDate.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@CowId", CowIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@CowName", CowNameTb.Text);
                    cmd.Parameters.AddWithValue("@ExpDateCalve", ExpDateCalve.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@DateCalved", DateCalved.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@CowAge", CowAgeTb.Text);
                    cmd.Parameters.AddWithValue("@Remarks", RemarkTb.Text);
                    cmd.Parameters.AddWithValue("@BrId", key);
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

        private void BreedDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HeatDate.Text = BreedDGV.SelectedRows[0].Cells[1].Value.ToString();
            BreedDate.Text = BreedDGV.SelectedRows[0].Cells[2].Value.ToString();
            CowIdCb.SelectedValue = BreedDGV.SelectedRows[0].Cells[3].Value.ToString();
            CowNameTb.Text = BreedDGV.SelectedRows[0].Cells[4].Value.ToString();
            ExpDateCalve.Text = BreedDGV.SelectedRows[0].Cells[5].Value.ToString();
            DateCalved.Text = BreedDGV.SelectedRows[0].Cells[6].Value.ToString();
            CowAgeTb.Text = BreedDGV.SelectedRows[0].Cells[7].Value.ToString();
            RemarkTb.Text = BreedDGV.SelectedRows[0].Cells[8].Value.ToString();
            if (CowNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BreedDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
