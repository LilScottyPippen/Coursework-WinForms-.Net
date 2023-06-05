namespace HTML5
{
    partial class Profile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gradientPanel1 = new HTML5.GradientPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.gradientPanel3 = new HTML5.GradientPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.labelMistakesCount = new System.Windows.Forms.Label();
            this.labelTestEnded = new System.Windows.Forms.Label();
            this.labelEndCount = new System.Windows.Forms.Label();
            this.gradientPanel2 = new HTML5.GradientPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.circularProgressBar1 = new HTML5.CircularProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxAccount = new System.Windows.Forms.PictureBox();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.gradientPanel3.SuspendLayout();
            this.gradientPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackColor = System.Drawing.Color.White;
            this.gradientPanel1.BorderRadius = 30;
            this.gradientPanel1.Controls.Add(this.pictureBox2);
            this.gradientPanel1.Controls.Add(this.gradientPanel3);
            this.gradientPanel1.Controls.Add(this.gradientPanel2);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.ForeColor = System.Drawing.Color.Black;
            this.gradientPanel1.GradientAngle = 90F;
            this.gradientPanel1.GradientBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(213)))), ((int)(((byte)(93)))));
            this.gradientPanel1.GradientTopColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.gradientPanel1.Location = new System.Drawing.Point(36, 122);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(946, 356);
            this.gradientPanel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::HTML5.Properties.Resources._3212a7f3c30793e7881ea6fdb8556bcd_transformed;
            this.pictureBox2.Location = new System.Drawing.Point(893, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // gradientPanel3
            // 
            this.gradientPanel3.BackColor = System.Drawing.Color.White;
            this.gradientPanel3.BorderRadius = 50;
            this.gradientPanel3.Controls.Add(this.label3);
            this.gradientPanel3.Controls.Add(this.labelMistakesCount);
            this.gradientPanel3.Controls.Add(this.labelTestEnded);
            this.gradientPanel3.Controls.Add(this.labelEndCount);
            this.gradientPanel3.ForeColor = System.Drawing.Color.Black;
            this.gradientPanel3.GradientAngle = 90F;
            this.gradientPanel3.GradientBottomColor = System.Drawing.Color.White;
            this.gradientPanel3.GradientTopColor = System.Drawing.Color.White;
            this.gradientPanel3.Location = new System.Drawing.Point(423, 115);
            this.gradientPanel3.Name = "gradientPanel3";
            this.gradientPanel3.Size = new System.Drawing.Size(379, 200);
            this.gradientPanel3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(224, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 35);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mistakes";
            // 
            // labelMistakesCount
            // 
            this.labelMistakesCount.AutoSize = true;
            this.labelMistakesCount.Font = new System.Drawing.Font("Open Sans", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMistakesCount.Location = new System.Drawing.Point(254, 46);
            this.labelMistakesCount.Name = "labelMistakesCount";
            this.labelMistakesCount.Size = new System.Drawing.Size(57, 69);
            this.labelMistakesCount.TabIndex = 2;
            this.labelMistakesCount.Text = "3";
            // 
            // labelTestEnded
            // 
            this.labelTestEnded.AutoSize = true;
            this.labelTestEnded.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTestEnded.Location = new System.Drawing.Point(34, 115);
            this.labelTestEnded.Name = "labelTestEnded";
            this.labelTestEnded.Size = new System.Drawing.Size(145, 35);
            this.labelTestEnded.TabIndex = 1;
            this.labelTestEnded.Text = "Test ended";
            // 
            // labelEndCount
            // 
            this.labelEndCount.AutoSize = true;
            this.labelEndCount.Font = new System.Drawing.Font("Open Sans", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEndCount.Location = new System.Drawing.Point(78, 46);
            this.labelEndCount.Name = "labelEndCount";
            this.labelEndCount.Size = new System.Drawing.Size(57, 69);
            this.labelEndCount.TabIndex = 0;
            this.labelEndCount.Text = "5";
            // 
            // gradientPanel2
            // 
            this.gradientPanel2.BackColor = System.Drawing.Color.White;
            this.gradientPanel2.BorderRadius = 50;
            this.gradientPanel2.Controls.Add(this.label2);
            this.gradientPanel2.Controls.Add(this.circularProgressBar1);
            this.gradientPanel2.ForeColor = System.Drawing.Color.Black;
            this.gradientPanel2.GradientAngle = 90F;
            this.gradientPanel2.GradientBottomColor = System.Drawing.Color.White;
            this.gradientPanel2.GradientTopColor = System.Drawing.Color.White;
            this.gradientPanel2.Location = new System.Drawing.Point(179, 115);
            this.gradientPanel2.Name = "gradientPanel2";
            this.gradientPanel2.Size = new System.Drawing.Size(200, 200);
            this.gradientPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(44, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 35);
            this.label2.TabIndex = 3;
            this.label2.Text = "Progress";
            // 
            // circularProgressBar1
            // 
            this.circularProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.circularProgressBar1.ForeColor = System.Drawing.Color.Black;
            this.circularProgressBar1.Location = new System.Drawing.Point(50, 42);
            this.circularProgressBar1.Maximum = 100;
            this.circularProgressBar1.Minimum = 0;
            this.circularProgressBar1.Name = "circularProgressBar1";
            this.circularProgressBar1.Padding = new System.Windows.Forms.Padding(10);
            this.circularProgressBar1.ProgressColor = System.Drawing.Color.Black;
            this.circularProgressBar1.Size = new System.Drawing.Size(100, 100);
            this.circularProgressBar1.TabIndex = 0;
            this.circularProgressBar1.TextColor = System.Drawing.Color.Black;
            this.circularProgressBar1.Value = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Open Sans", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(204, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // pictureBoxAccount
            // 
            this.pictureBoxAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.pictureBoxAccount.Location = new System.Drawing.Point(84, 50);
            this.pictureBoxAccount.Name = "pictureBoxAccount";
            this.pictureBoxAccount.Size = new System.Drawing.Size(150, 150);
            this.pictureBoxAccount.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAccount.TabIndex = 7;
            this.pictureBoxAccount.TabStop = false;
            // 
            // Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1019, 561);
            this.Controls.Add(this.pictureBoxAccount);
            this.Controls.Add(this.gradientPanel1);
            this.Name = "Profile";
            this.Text = "Profile";
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.gradientPanel3.ResumeLayout(false);
            this.gradientPanel3.PerformLayout();
            this.gradientPanel2.ResumeLayout(false);
            this.gradientPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAccount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GradientPanel gradientPanel1;
        private System.Windows.Forms.PictureBox pictureBoxAccount;
        private System.Windows.Forms.Label label1;
        private GradientPanel gradientPanel2;
        private CircularProgressBar circularProgressBar1;
        private GradientPanel gradientPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelMistakesCount;
        private System.Windows.Forms.Label labelTestEnded;
        private System.Windows.Forms.Label labelEndCount;
    }
}