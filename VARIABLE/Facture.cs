using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace VARIABLE
{
    public partial class Facture : Form
    {
        ADO d = new ADO();
        float Prix = 0;
        public void RemlirCombo()
        {
           d.Connecter();
            d.cmd.CommandText = "Select NomMédicament from infoMedicament";
            d.cmd.Connection = d.con;
            d.dr = d.cmd.ExecuteReader();
            while (d.dr.Read())
            {
                comboBox1.Items.Add(d.dr[0]);
            }
            d.dr.Close();
            d.Deconnecter();
        }


        public Facture()
        {
            InitializeComponent();
        }



        private void label1_Click_1(object sender, EventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            RemlirCombo();
            comboBox1.SelectedText = "--select--";
            textBox2.ScrollBars = ScrollBars.Vertical;
            Rest.Text = "";
            PrixMedicament.Text = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Rest.Text = "";
            string  NomMedicament = comboBox1.SelectedItem.ToString();
            d.Connecter();

            d.cmd.CommandText = "Select NomMédicament,Prix,Info From infoMedicament  ";
            d.cmd.Connection = d.con;
            d.dr = d.cmd.ExecuteReader();
            while (d.dr.Read())
            {
                if (d.dr[0].ToString() == NomMedicament)
                {
                    Prix = float.Parse(d.dr[1].ToString());
                    PrixMedicament.Text = Prix.ToString();
                    textBox2.Text = d.dr[2].ToString();
                    break;
                }
            }
            d.dr.Close();
            d.Deconnecter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = "^[0-9]*(,[0-9]+)?$";

            if (textBox1.Text == "")
            {
                MessageBox.Show("Veuillez saisir le prix","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }else if (!Regex.IsMatch(textBox1.Text, pattern))
            {
                MessageBox.Show("Veuillez saisir le prix en chiffres 0-9", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {   
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Veuillez choisir un médicament d'abord", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else if (  (float.Parse(textBox1.Text) < Prix ) )
                {
                    MessageBox.Show("Le prix du médicament est supérieur à celui d'un client","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                else
                {
                    Rest.Text = (float.Parse(textBox1.Text) - Prix).ToString();
                }
            }
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string pattern = "^[0-9]*(,[0-9]+)?$";

            if (!Regex.IsMatch(textBox1.Text, pattern))
            {
                errorProvider1.SetError(this.textBox1, "Veuillez saisir le prix en chiffres 0-9");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Items.Clear();
                   RemlirCombo();
            comboBox1.ResetText();
            comboBox1.SelectedIndex = -1;
            comboBox1.SelectedText = "--select--";

            Rest.Text = "";
            PrixMedicament.Text = "";
            errorProvider1.Clear();

        }
    }
}
