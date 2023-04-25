using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTML5
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void labelConfirm_Click(object sender, EventArgs e)
        {

            gradientPanel1.GradientBottomColor = Color.FromArgb(116, 209, 157);
            gradientPanel1.GradientTopColor = Color.FromArgb(116, 209, 157);

            gradientPanel2.GradientBottomColor = Color.White;
            gradientPanel2.GradientTopColor = Color.White;

            label1.Text = "Confirm Email";
            textBoxEmail.Text = "";
        }
    }
}
