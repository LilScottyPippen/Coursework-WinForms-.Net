namespace HTML5
{
    partial class Settings
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
            this.labelSettings = new System.Windows.Forms.Label();
            this.groupBoxAccount = new System.Windows.Forms.GroupBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.pictureBoxAccount = new System.Windows.Forms.PictureBox();
            this.UploadAvatar = new System.Windows.Forms.Button();
            this.groupBoxAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSettings
            // 
            this.labelSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSettings.Font = new System.Drawing.Font("Open Sans", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSettings.Location = new System.Drawing.Point(0, 0);
            this.labelSettings.Name = "labelSettings";
            this.labelSettings.Size = new System.Drawing.Size(338, 39);
            this.labelSettings.TabIndex = 0;
            this.labelSettings.Text = "Settings";
            this.labelSettings.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxAccount
            // 
            this.groupBoxAccount.Controls.Add(this.UploadAvatar);
            this.groupBoxAccount.Controls.Add(this.pictureBoxAccount);
            this.groupBoxAccount.Controls.Add(this.labelEmail);
            this.groupBoxAccount.Controls.Add(this.buttonLogout);
            this.groupBoxAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAccount.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxAccount.Location = new System.Drawing.Point(0, 39);
            this.groupBoxAccount.Name = "groupBoxAccount";
            this.groupBoxAccount.Size = new System.Drawing.Size(338, 232);
            this.groupBoxAccount.TabIndex = 1;
            this.groupBoxAccount.TabStop = false;
            this.groupBoxAccount.Text = "Account";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEmail.Location = new System.Drawing.Point(12, 176);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(43, 15);
            this.labelEmail.TabIndex = 1;
            this.labelEmail.Text = "Email: ";
            // 
            // buttonLogout
            // 
            this.buttonLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonLogout.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLogout.Location = new System.Drawing.Point(3, 199);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(332, 30);
            this.buttonLogout.TabIndex = 0;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // pictureBoxAccount
            // 
            this.pictureBoxAccount.Image = global::HTML5.Properties.Resources._126200705;
            this.pictureBoxAccount.Location = new System.Drawing.Point(12, 35);
            this.pictureBoxAccount.Name = "pictureBoxAccount";
            this.pictureBoxAccount.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxAccount.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAccount.TabIndex = 2;
            this.pictureBoxAccount.TabStop = false;
            // 
            // UploadAvatar
            // 
            this.UploadAvatar.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UploadAvatar.Location = new System.Drawing.Point(12, 141);
            this.UploadAvatar.Name = "UploadAvatar";
            this.UploadAvatar.Size = new System.Drawing.Size(108, 32);
            this.UploadAvatar.TabIndex = 3;
            this.UploadAvatar.Text = "Upload Avatar";
            this.UploadAvatar.UseVisualStyleBackColor = true;
            this.UploadAvatar.Click += new System.EventHandler(this.UploadAvatar_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(338, 450);
            this.Controls.Add(this.groupBoxAccount);
            this.Controls.Add(this.labelSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.groupBoxAccount.ResumeLayout(false);
            this.groupBoxAccount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAccount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelSettings;
        private System.Windows.Forms.GroupBox groupBoxAccount;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Button UploadAvatar;
        private System.Windows.Forms.PictureBox pictureBoxAccount;
    }
}