using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VARIABLE
{
    public partial class Medicament : Form
    {
        ADO d = new ADO();

        public void RemplirGrid()
        {
            if (d.ds.Tables["Medicament"] != null)
            {
                d.ds.Tables["Medicament"].Clear();
            }
            d.dap = new SqlDataAdapter("select * from infoMedicament", d.con);
            d.dap.Fill(d.ds, "Medicament");
            dataGridView1.DataSource = d.ds.Tables["Medicament"];
        }


        public Medicament()
        {
            InitializeComponent();
        }

        private void Medicament_Load(object sender, EventArgs e)
        {

            RemplirGrid();
            textBox3.ScrollBars = ScrollBars.Vertical;

            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width = 147;
            dataGridView1.Columns[2].Width = 50;

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Supprimé";
            buttonColumn.Width = 70;
            buttonColumn.Name = "buttonColumn";
            buttonColumn.Text = "Supp";
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Insert(4, buttonColumn);



        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez vous quitter ?", "Confermation", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string pattern = "^[0-9]*(,[0-9]+)?$";

            if (!Regex.IsMatch(textBox2.Text, pattern))
            {
                errorProvider1.SetError(this.textBox2, "Veuillez saisir le prix en chiffres 0-9");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pattern = "^[0-9]*(,[0-9]+)?$";
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!Regex.IsMatch(textBox2.Text, pattern))
            {
                MessageBox.Show("Veuillez saisir le prix en chiffres 0-9", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                d.ligne = d.ds.Tables["Medicament"].NewRow();
                d.ligne[1] = textBox1.Text;
                d.ligne[2] = textBox2.Text;
                d.ligne[3] = textBox3.Text;

                for (int i = 0; i < d.ds.Tables["Medicament"].Rows.Count; i++)
                {
                    if (textBox1.Text == d.ds.Tables["Medicament"].Rows[i][0].ToString())
                    {
                        MessageBox.Show("Medicament déja existé");
                        return;
                    }
                }
                d.ds.Tables["Medicament"].Rows.Add(d.ligne);
                dataGridView1.DataSource = d.ds.Tables["Medicament"];
                d.bc = new SqlCommandBuilder(d.dap);
                d.dap.Update(d.ds, "Medicament");
                RemplirGrid();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

                MessageBox.Show("Le médicament a été ajouté avec succès", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

                if (MessageBox.Show("Voulez vous Supprimer ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    d.ds.Tables["Medicament"].Rows[dataGridView1.CurrentRow.Index].Delete();
                    d.bc = new SqlCommandBuilder(d.dap);
                    d.dap.Update(d.ds, "Medicament");
                    RemplirGrid();
                    MessageBox.Show("Le médicament a été supprimé avec succès", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            

        }
    }
}
