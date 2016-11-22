namespace Lab5
{
    partial class Form1
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
            this.buttonAddSong = new System.Windows.Forms.Button();
            this.buttonDeleteSong = new System.Windows.Forms.Button();
            this.textBoxSongTitle = new System.Windows.Forms.TextBox();
            this.textBoxArtist = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonPlaySong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonAddSong
            // 
            this.buttonAddSong.Location = new System.Drawing.Point(197, 25);
            this.buttonAddSong.Name = "buttonAddSong";
            this.buttonAddSong.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSong.TabIndex = 0;
            this.buttonAddSong.Text = "Add Song";
            this.buttonAddSong.UseVisualStyleBackColor = true;
            this.buttonAddSong.Click += new System.EventHandler(this.buttonAddSong_Click);
            // 
            // buttonDeleteSong
            // 
            this.buttonDeleteSong.Location = new System.Drawing.Point(197, 102);
            this.buttonDeleteSong.Name = "buttonDeleteSong";
            this.buttonDeleteSong.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteSong.TabIndex = 1;
            this.buttonDeleteSong.Text = "Delete Song";
            this.buttonDeleteSong.UseVisualStyleBackColor = true;
            this.buttonDeleteSong.Click += new System.EventHandler(this.buttonDeleteSong_Click);
            // 
            // textBoxSongTitle
            // 
            this.textBoxSongTitle.Location = new System.Drawing.Point(15, 64);
            this.textBoxSongTitle.Name = "textBoxSongTitle";
            this.textBoxSongTitle.Size = new System.Drawing.Size(100, 20);
            this.textBoxSongTitle.TabIndex = 2;
            // 
            // textBoxArtist
            // 
            this.textBoxArtist.Location = new System.Drawing.Point(15, 25);
            this.textBoxArtist.Name = "textBoxArtist";
            this.textBoxArtist.Size = new System.Drawing.Size(100, 20);
            this.textBoxArtist.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Artist";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "SongTitle";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 102);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(179, 134);
            this.listBox1.TabIndex = 6;
            // 
            // buttonPlaySong
            // 
            this.buttonPlaySong.Location = new System.Drawing.Point(197, 131);
            this.buttonPlaySong.Name = "buttonPlaySong";
            this.buttonPlaySong.Size = new System.Drawing.Size(75, 23);
            this.buttonPlaySong.TabIndex = 7;
            this.buttonPlaySong.Text = "Play Song";
            this.buttonPlaySong.UseVisualStyleBackColor = true;
            this.buttonPlaySong.Click += new System.EventHandler(this.buttonPlaySong_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.buttonPlaySong);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxArtist);
            this.Controls.Add(this.textBoxSongTitle);
            this.Controls.Add(this.buttonDeleteSong);
            this.Controls.Add(this.buttonAddSong);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddSong;
        private System.Windows.Forms.Button buttonDeleteSong;
        private System.Windows.Forms.TextBox textBoxSongTitle;
        private System.Windows.Forms.TextBox textBoxArtist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonPlaySong;
    }
}

