namespace _463613_Ryan_Kusnadi_ResponsiJuniorProject
{
    using Npgsql;
    using System.Data;
    using System.Linq.Expressions;
    using System.Xml.Linq;
    public partial class Form1 : Form
    {
        private NpgsqlConnection conn;
        string connstring = "Host=localhost;Port=2022;Username=postgres;Password=informatika;Database=463613_Ryan Kusnadi_Responsi";
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;
        private DataGridViewRow r;
        public string temp_id;
        public Form1()
        {
            InitializeComponent();

            conn = new NpgsqlConnection(connstring);
            reloadDataTable();
        }

        private void reloadDataTable()
        {
            try
            {
                conn.Open();
                dataGridView1.DataSource = null;
                sql = "select * from st_select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                dataGridView1.DataSource = dt;



                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                r = dataGridView1.Rows[e.RowIndex];
                tb_nama.Text = r.Cells["_nama"].Value.ToString();
                tb_dep.Text = r.Cells["_id_dep"].Value.ToString();
            }
        }
        private void btn_insert(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from st_insert(:_nama, :_id_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_nama", tb_nama.Text);
                switch (tb_dep.Text)
                {
                    case "HR":
                        temp_id = "1";
                        break;
                    case "ENG":
                        temp_id = "2";
                        break;
                    case "DEV":
                        temp_id = "3";
                        break;
                    case "PM":
                        temp_id = "4";
                        break;
                    case "FIN":
                        temp_id = "5";
                        break;
                    default:
                        MessageBox.Show("Department ID NOT FOUND, Will be automatically assigned to HR", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                cmd.Parameters.AddWithValue("_id_dep", temp_id);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Input Successful", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    reloadDataTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                reloadDataTable();
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            reloadDataTable();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Select Row to Update!", "Good!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    conn.Open();
                    sql = @"select * from st_update(:_id_karyawan,:_nama,:_id_dep)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id_karyawan", r.Cells["_id_karyawan"].Value.ToString());
                    cmd.Parameters.AddWithValue("_name", tb_nama.Text);
                    switch (tb_dep.Text)
                    {
                        case "HR":
                            temp_id = "1";
                            break;
                        case "ENG":
                            temp_id = "2";
                            break;
                        case "DEV":
                            temp_id = "3";
                            break;
                        case "PM":
                            temp_id = "4";
                            break;
                        case "FIN":
                            temp_id = "5";
                            break;
                        default:
                            MessageBox.Show("Department ID NOT FOUND, Will be automatically assigned to HR", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                    cmd.Parameters.AddWithValue("_id_dep", temp_id);


                    if ((int)cmd.ExecuteScalar() == 1)
                    {
                        MessageBox.Show("Update Successful", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        reloadDataTable();
                        r = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reloadDataTable();
                    conn.Close();
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("No Row Selected!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Delete Row?" + " ?", "Deletion Confirmed", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)

                try
                {
                    conn.Open();
                    sql = @"select * from st_delete(:_id_karyawan)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id_karyawan", r.Cells["_id_karyawan"].Value.ToString());
                    if ((int)cmd.ExecuteScalar() == 1)
                    {
                        MessageBox.Show("Deletion Successful!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        reloadDataTable();
                    }

                }
                catch (Exception ex)
                {
                    reloadDataTable();
                    MessageBox.Show("Error:" + ex.Message, "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
        }
    }
}