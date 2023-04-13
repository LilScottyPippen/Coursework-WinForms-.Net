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
    public partial class LessonTest : Form
    {
        public LessonTest()
        {
            InitializeComponent();

            int centerX = Screen.PrimaryScreen.WorkingArea.Width / 2;
            int centerY = Screen.PrimaryScreen.WorkingArea.Height / 2;

            int formX = this.Width / 2;
            int formY = this.Height / 2;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(centerX - formX, centerY - formY);

            /*ArtanPanel codePanel = new ArtanPanel();
            codePanel.BackColor = Color.Black;
            codePanel.GradientBottomColor = Color.FromArgb(45, 45, 45);
            codePanel.GradientTopColor = Color.FromArgb(45, 45, 45);
            Controls.Add(codePanel);*/
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void artanPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
