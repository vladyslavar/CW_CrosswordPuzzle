namespace CrosswordPuzzle
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.themeLabel = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.customizeButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // themeLabel
            // 
            this.themeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.themeLabel.Font = new System.Drawing.Font("Lucida Bright", 19.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.themeLabel.Location = new System.Drawing.Point(0, 0);
            this.themeLabel.Name = "themeLabel";
            this.themeLabel.Size = new System.Drawing.Size(1026, 95);
            this.themeLabel.TabIndex = 0;
            this.themeLabel.Text = "Choose a theme";
            this.themeLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(194)))), ((int)(((byte)(190)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.ColumnWidth = 55;
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.Font = new System.Drawing.Font("Lucida Bright", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 65;
            this.listBox1.Location = new System.Drawing.Point(226, 118);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(583, 325);
            this.listBox1.TabIndex = 1;
            this.listBox1.TabStop = false;
            // 
            // customizeButton
            // 
            this.customizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customizeButton.FlatAppearance.BorderColor = System.Drawing.Color.Olive;
            this.customizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customizeButton.Font = new System.Drawing.Font("Lucida Bright", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.customizeButton.Location = new System.Drawing.Point(379, 477);
            this.customizeButton.Name = "customizeButton";
            this.customizeButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.customizeButton.Size = new System.Drawing.Size(280, 38);
            this.customizeButton.TabIndex = 3;
            this.customizeButton.Text = "customize";
            this.customizeButton.UseVisualStyleBackColor = false;
            this.customizeButton.Click += new System.EventHandler(this.customizeButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.nextButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.nextButton.FlatAppearance.BorderSize = 2;
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextButton.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nextButton.Location = new System.Drawing.Point(379, 522);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(280, 48);
            this.nextButton.TabIndex = 2;
            this.nextButton.Text = "NEXT";
            this.nextButton.UseVisualStyleBackColor = false;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(194)))), ((int)(((byte)(190)))));
            this.ClientSize = new System.Drawing.Size(1026, 612);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.customizeButton);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.themeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CROSS WORLD";
            this.ResumeLayout(false);

        }

        #endregion

        private Label themeLabel;
        private ListBox listBox1;
        private Button customizeButton;
        private Button nextButton;
    }
}