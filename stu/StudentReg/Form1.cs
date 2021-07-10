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

namespace StudentReg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            



        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-91DD8R4\\SQLEXPRESS; Initial Catalog=abc; User Id=supun ; Password=supun; ");
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        bool Mode = true;
        string sql;


        
        public void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                sql = "select* from student";
                cmd = new SqlCommand(sql, con);
                con.Open();

                read = cmd.ExecuteReader();

                drr = new SqlDataAdapter(sql, con);
                tblStudent.Rows.Clear();

                while (read.Read())
                {
                    tblStudent.Rows.Add(read[0], read[1], read[2], read[3], read[4]);
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void getID(string id)
        {
           sql =  "select* from student where id = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while(read.Read())
            {
                txtName.Text = read[1].ToString();
                txtNo.Text = read[4].ToString();
                txtCourse.Text = read[2].ToString(); 
                txtFee.Text = read[3].ToString();
            }
            con.Close();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string number = txtNo.Text;
            string course = txtCourse.Text;
            string fee = txtFee.Text;


            if(Mode == true)
            {
                sql = "insert into student(stname,telno,course,fee) values(@stname,@telno,@course,@fee)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@telno", number);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                MessageBox.Show("Record Added successfull");
                cmd.ExecuteNonQuery();


                txtName.Clear();
                txtNo.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();  

            } 
            else
            {
                id = tblStudent.CurrentRow.Cells[0].Value.ToString();
                sql = "update student set stname = @stname , telno = @telno , course = @course , fee = @fee where id =@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@telno", number);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record updated successfull");
                cmd.ExecuteNonQuery();


                txtName.Clear();
                txtNo.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();
                btnSave.Text = "Save";
                Mode = true;

            }
            con.Close();
        }

        private void comboCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCourse.Text = comboCourse.Text;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == tblStudent.Columns["Edit"].Index && e.RowIndex >=0)
            {
                Mode = false;
                id = tblStudent.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                btnSave.Text = "Edit";
            }
            else if(e.ColumnIndex == tblStudent.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = tblStudent.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from student where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Delete Successufull");
                con.Close();
            }
                       

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

            try
            {
                sql = "select* from student";
                cmd = new SqlCommand(sql, con);
                con.Open();

                read = cmd.ExecuteReader();

                drr = new SqlDataAdapter(sql, con);
                tblStudent.Rows.Clear();

                while (read.Read())
                {
                    tblStudent.Rows.Add(read[0], read[1], read[2], read[3], read[4]);
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtNo.Clear();
            txtCourse.Clear();
            txtFee.Clear();
            txtName.Focus();
            btnSave.Text = "Save";
            Mode = true;

        }
    }
}
