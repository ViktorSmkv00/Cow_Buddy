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
    public partial class Cows : Form
    {
        MySqlConnection Con = new MySqlConnection("server=localhost;database=cow_buddy_db;uid=root;pwd=viktor123;");
        private int age = 0;
        private int key = 0;
        public Cows()
        {
            InitializeComponent();
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

        private void panel3_Paint(object sender, PaintEventArgs e)
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

        private void Cows_Load(object sender, EventArgs e)
        {

        }

        private void populate()
        {
            try
            {
                Con.Open();
                string query = "SELECT * FROM CowTbl";
                MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                CowsDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void Clear()
        {
            CowNameTb.Text = "";
            EarTagTb.Text = "";
            ColorTb.Text = "";
            BreedTb.Text = "";
            WABTb.Text = "";
            AgeTb.Text = "";
            PastureTb.Text = "";
            key = 0;
        }

        private void SearchCow()
        {
            try
            {
                Con.Open();
                string query = "SELECT * FROM CowTbl WHERE CowName LIKE @search";
                MySqlDataAdapter sda = new MySqlDataAdapter(query, Con);
                sda.SelectCommand.Parameters.AddWithValue("@search", "%" + CowSearchTb.Text + "%");
                DataSet ds = new DataSet();
                sda.Fill(ds);
                CowsDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            if (CowNameTb.Text == "" || EarTagTb.Text == "" || ColorTb.Text == "" || BreedTb.Text == "" || WABTb.Text == "" || AgeTb.Text == "" || PastureTb.Text == "")
            {
                MessageBox.Show("Липсва информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "INSERT INTO CowTbl(CowName, EarTag, Color, Breed, Age, WeightAtBirth, Pasture) " +
                                   "VALUES(@CowName, @EarTag, @Color, @Breed, @Age, @WAB, @Pasture)";
                    MySqlCommand cmd = new MySqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@CowName", CowNameTb.Text);
                    cmd.Parameters.AddWithValue("@EarTag", EarTagTb.Text);
                    cmd.Parameters.AddWithValue("@Color", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@Breed", BreedTb.Text);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@WAB", WABTb.Text);
                    cmd.Parameters.AddWithValue("@Pasture", PastureTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запазване успешно");
                    populate();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (CowNameTb.Text == "" || EarTagTb.Text == "" || ColorTb.Text == "" || BreedTb.Text == "" || WABTb.Text == "" || AgeTb.Text == "" || PastureTb.Text == "")
            {
                MessageBox.Show("Липсваща информация");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "UPDATE CowTbl SET CowName=@CowName, EarTag=@EarTag, Color=@Color, Breed=@Breed, Age=@Age, " +
                                   "WeightAtBirth=@WAB, Pasture=@Pasture WHERE CowId=@key";
                    MySqlCommand cmd = new MySqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@CowName", CowNameTb.Text);
                    cmd.Parameters.AddWithValue("@EarTag", EarTagTb.Text);
                    cmd.Parameters.AddWithValue("@Color", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@Breed", BreedTb.Text);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@WAB", WABTb.Text);
                    cmd.Parameters.AddWithValue("@Pasture", PastureTb.Text);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Актуализиране успешно");
                    populate();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Избиране на крава за изтриване");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "DELETE FROM CowTbl WHERE CowId = @key";
                    MySqlCommand cmd = new MySqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Изтриване успешно");
                    populate();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void CowSearchTb_Click(object sender, EventArgs e)
        {

        }

        private void CowsDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            CowNameTb.Text = CowsDGV.SelectedRows[0].Cells[1].Value.ToString();
            EarTagTb.Text = CowsDGV.SelectedRows[0].Cells[2].Value.ToString();
            ColorTb.Text = CowsDGV.SelectedRows[0].Cells[3].Value.ToString();
            BreedTb.Text = CowsDGV.SelectedRows[0].Cells[4].Value.ToString();
            WABTb.Text = CowsDGV.SelectedRows[0].Cells[6].Value.ToString();
            PastureTb.Text = CowsDGV.SelectedRows[0].Cells[7].Value.ToString();
            key = Convert.ToInt32(CowsDGV.SelectedRows[0].Cells[0].Value.ToString());
            age = Convert.ToInt32(CowsDGV.SelectedRows[0].Cells[5].Value.ToString());
        }

        private void DOBDate_ValueChanged_1(object sender, EventArgs e)
        {
            age = Convert.ToInt32((DateTime.Today.Date - DOBDate.Value.Date).Days) / 365;
        }

        private void CowSearchTb_TextChanged(object sender, EventArgs e)
        {
            SearchCow();
        }

        private void DOBDate_MouseLeave(object sender, EventArgs e)
        {
            age = Convert.ToInt32((DateTime.Today.Date - DOBDate.Value.Date).Days) / 365;
            AgeTb.Text = "" + age;
        }
    }
}
    

