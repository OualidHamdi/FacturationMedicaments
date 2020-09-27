using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VARIABLE
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void médicamentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            foreach(Form f in Application.OpenForms)
            {
                if (f.Text == "Medicament")
                {
                    isOpen = true;
                    f.Focus();
                    break;
                }
            }
            if(isOpen == false)
            {
                Medicament f2 = new Medicament();
                f2.MdiParent = this;
                f2.Show();
            }
        }

        private void facturationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Facture")
                {
                    isOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (isOpen == false)
            {
                Facture f2 = new Facture();
                f2.MdiParent = this;
                f2.Show();
            }
        }
    }
}
