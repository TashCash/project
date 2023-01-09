namespace FitnesClub.Forms
{
    partial class Splashscreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splashscreen));
            this.labelConn = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxGif = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGif)).BeginInit();
            this.SuspendLayout();
            // 
            // labelConn
            // 
            this.labelConn.AutoSize = true;
            this.labelConn.ForeColor = System.Drawing.Color.White;
            this.labelConn.Location = new System.Drawing.Point(44, 86);
            this.labelConn.Name = "labelConn";
            this.labelConn.Size = new System.Drawing.Size(110, 24);
            this.labelConn.TabIndex = 0;
            this.labelConn.Text = "Загружаем";
            // 
            // timerMain
            // 
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // pictureBoxGif
            // 
            this.pictureBoxGif.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxGif.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxGif.Image")));
            this.pictureBoxGif.InitialImage = null;
            this.pictureBoxGif.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxGif.Name = "pictureBoxGif";
            this.pictureBoxGif.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxGif.TabIndex = 1;
            this.pictureBoxGif.TabStop = false;
            // 
            // Splashscreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.labelConn);
            this.Controls.Add(this.pictureBoxGif);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Splashscreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splashscreen";
            this.Load += new System.EventHandler(this.Splashscreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGif)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelConn;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.PictureBox pictureBoxGif;
    }
}