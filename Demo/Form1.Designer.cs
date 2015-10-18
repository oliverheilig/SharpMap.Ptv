namespace Demo
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxPOI = new System.Windows.Forms.CheckBox();
            this.checkBoxMM = new System.Windows.Forms.CheckBox();
            this.checkBoxFG = new System.Windows.Forms.CheckBox();
            this.checkBoxBG = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(745, 523);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxPOI);
            this.panel1.Controls.Add(this.checkBoxMM);
            this.panel1.Controls.Add(this.checkBoxFG);
            this.panel1.Controls.Add(this.checkBoxBG);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 523);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 95);
            this.panel1.TabIndex = 1;
            // 
            // checkBoxPOI
            // 
            this.checkBoxPOI.AutoSize = true;
            this.checkBoxPOI.Checked = true;
            this.checkBoxPOI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPOI.Location = new System.Drawing.Point(142, 52);
            this.checkBoxPOI.Name = "checkBoxPOI";
            this.checkBoxPOI.Size = new System.Drawing.Size(60, 21);
            this.checkBoxPOI.TabIndex = 7;
            this.checkBoxPOI.Text = "POIs";
            this.checkBoxPOI.UseVisualStyleBackColor = true;
            this.checkBoxPOI.CheckedChanged += new System.EventHandler(this.checkBoxPOI_CheckedChanged);
            // 
            // checkBoxMM
            // 
            this.checkBoxMM.AutoSize = true;
            this.checkBoxMM.Checked = true;
            this.checkBoxMM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMM.Location = new System.Drawing.Point(142, 25);
            this.checkBoxMM.Name = "checkBoxMM";
            this.checkBoxMM.Size = new System.Drawing.Size(100, 21);
            this.checkBoxMM.TabIndex = 6;
            this.checkBoxMM.Text = "Map&Market";
            this.checkBoxMM.UseVisualStyleBackColor = true;
            this.checkBoxMM.CheckedChanged += new System.EventHandler(this.ckechBoxMM_CheckedChanged);
            // 
            // checkBoxFG
            // 
            this.checkBoxFG.AutoSize = true;
            this.checkBoxFG.Checked = true;
            this.checkBoxFG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFG.Location = new System.Drawing.Point(19, 52);
            this.checkBoxFG.Name = "checkBoxFG";
            this.checkBoxFG.Size = new System.Drawing.Size(104, 21);
            this.checkBoxFG.TabIndex = 5;
            this.checkBoxFG.Text = "Foreground";
            this.checkBoxFG.UseVisualStyleBackColor = true;
            this.checkBoxFG.CheckedChanged += new System.EventHandler(this.checkBoxFG_CheckedChanged);
            // 
            // checkBoxBG
            // 
            this.checkBoxBG.AutoSize = true;
            this.checkBoxBG.Checked = true;
            this.checkBoxBG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBG.Location = new System.Drawing.Point(17, 25);
            this.checkBoxBG.Name = "checkBoxBG";
            this.checkBoxBG.Size = new System.Drawing.Size(106, 21);
            this.checkBoxBG.TabIndex = 4;
            this.checkBoxBG.Text = "Background";
            this.checkBoxBG.UseVisualStyleBackColor = true;
            this.checkBoxBG.CheckedChanged += new System.EventHandler(this.checkBoxBG_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "MM Filter";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "GID = \'08212\'",
            "EW_W / EW_M > 1",
            "AUSL / EW > 0.1",
            "EW_QKM > 250"});
            this.comboBox1.Location = new System.Drawing.Point(337, 39);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(283, 24);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(629, 20);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(337, 20);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(283, 22);
            this.textBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 618);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxPOI;
        private System.Windows.Forms.CheckBox checkBoxMM;
        private System.Windows.Forms.CheckBox checkBoxFG;
        private System.Windows.Forms.CheckBox checkBoxBG;
    }
}

