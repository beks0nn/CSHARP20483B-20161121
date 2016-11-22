namespace Lab4
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
            this.buttonInvokeDelegate = new System.Windows.Forms.Button();
            this.buttonAddDelA = new System.Windows.Forms.Button();
            this.buttonDeleteDelA = new System.Windows.Forms.Button();
            this.buttonAddDelB = new System.Windows.Forms.Button();
            this.buttonDeleteDelB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonInvokeDelegate
            // 
            this.buttonInvokeDelegate.Location = new System.Drawing.Point(76, 217);
            this.buttonInvokeDelegate.Name = "buttonInvokeDelegate";
            this.buttonInvokeDelegate.Size = new System.Drawing.Size(141, 23);
            this.buttonInvokeDelegate.TabIndex = 0;
            this.buttonInvokeDelegate.Text = "Invoke All";
            this.buttonInvokeDelegate.UseVisualStyleBackColor = true;
            this.buttonInvokeDelegate.Click += new System.EventHandler(this.buttonInvokeDelegate_Click);
            // 
            // buttonAddDelA
            // 
            this.buttonAddDelA.Location = new System.Drawing.Point(12, 33);
            this.buttonAddDelA.Name = "buttonAddDelA";
            this.buttonAddDelA.Size = new System.Drawing.Size(135, 23);
            this.buttonAddDelA.TabIndex = 5;
            this.buttonAddDelA.Text = "Add Delegate A";
            this.buttonAddDelA.Click += new System.EventHandler(this.buttonAddDelA_Click);
            // 
            // buttonDeleteDelA
            // 
            this.buttonDeleteDelA.Location = new System.Drawing.Point(12, 62);
            this.buttonDeleteDelA.Name = "buttonDeleteDelA";
            this.buttonDeleteDelA.Size = new System.Drawing.Size(135, 23);
            this.buttonDeleteDelA.TabIndex = 2;
            this.buttonDeleteDelA.Text = "Delete Delegate A";
            this.buttonDeleteDelA.UseVisualStyleBackColor = true;
            this.buttonDeleteDelA.Click += new System.EventHandler(this.buttonDeleteDelA_Click);
            // 
            // buttonAddDelB
            // 
            this.buttonAddDelB.Location = new System.Drawing.Point(153, 33);
            this.buttonAddDelB.Name = "buttonAddDelB";
            this.buttonAddDelB.Size = new System.Drawing.Size(135, 23);
            this.buttonAddDelB.TabIndex = 3;
            this.buttonAddDelB.Text = "Add Delegate B";
            this.buttonAddDelB.UseVisualStyleBackColor = true;
            this.buttonAddDelB.Click += new System.EventHandler(this.buttonAddDelB_Click);
            // 
            // buttonDeleteDelB
            // 
            this.buttonDeleteDelB.Location = new System.Drawing.Point(153, 62);
            this.buttonDeleteDelB.Name = "buttonDeleteDelB";
            this.buttonDeleteDelB.Size = new System.Drawing.Size(135, 23);
            this.buttonDeleteDelB.TabIndex = 4;
            this.buttonDeleteDelB.Text = "Delete Delegate B";
            this.buttonDeleteDelB.UseVisualStyleBackColor = true;
            this.buttonDeleteDelB.Click += new System.EventHandler(this.buttonDeleteDelB_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 261);
            this.Controls.Add(this.buttonDeleteDelB);
            this.Controls.Add(this.buttonAddDelB);
            this.Controls.Add(this.buttonDeleteDelA);
            this.Controls.Add(this.buttonAddDelA);
            this.Controls.Add(this.buttonInvokeDelegate);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonInvokeDelegate;
        private System.Windows.Forms.Button buttonAddDelA;
        private System.Windows.Forms.Button buttonDeleteDelA;
        private System.Windows.Forms.Button buttonAddDelB;
        private System.Windows.Forms.Button buttonDeleteDelB;
    }
}

