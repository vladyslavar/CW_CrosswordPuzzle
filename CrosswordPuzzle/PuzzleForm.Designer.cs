namespace CrosswordPuzzle
{
    partial class PuzzleForm
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
            this.backPanel = new System.Windows.Forms.Panel();
            this.backLeftPanel = new System.Windows.Forms.Panel();
            this.mainLeftPanel = new System.Windows.Forms.Panel();
            this.bottomLeftPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backButton = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.buttonAnswear = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.backRightPanel = new System.Windows.Forms.Panel();
            this.mainRightPanel = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bottomRightPanel = new System.Windows.Forms.Panel();
            this.upRightPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.backPanel.SuspendLayout();
            this.backLeftPanel.SuspendLayout();
            this.bottomLeftPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.backRightPanel.SuspendLayout();
            this.mainRightPanel.SuspendLayout();
            this.upRightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // backPanel
            // 
            this.backPanel.Controls.Add(this.backLeftPanel);
            this.backPanel.Controls.Add(this.backRightPanel);
            this.backPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backPanel.Location = new System.Drawing.Point(0, 0);
            this.backPanel.Name = "backPanel";
            this.backPanel.Size = new System.Drawing.Size(1119, 578);
            this.backPanel.TabIndex = 0;
            // 
            // backLeftPanel
            // 
            this.backLeftPanel.Controls.Add(this.mainLeftPanel);
            this.backLeftPanel.Controls.Add(this.bottomLeftPanel);
            this.backLeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.backLeftPanel.Name = "backLeftPanel";
            this.backLeftPanel.Size = new System.Drawing.Size(783, 578);
            this.backLeftPanel.TabIndex = 1;
            // 
            // mainLeftPanel
            // 
            this.mainLeftPanel.AutoScroll = true;
            this.mainLeftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(158)))), ((int)(((byte)(145)))));
            this.mainLeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.mainLeftPanel.Name = "mainLeftPanel";
            this.mainLeftPanel.Size = new System.Drawing.Size(783, 411);
            this.mainLeftPanel.TabIndex = 2;
            // 
            // bottomLeftPanel
            // 
            this.bottomLeftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(194)))), ((int)(((byte)(190)))));
            this.bottomLeftPanel.Controls.Add(this.panel1);
            this.bottomLeftPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomLeftPanel.Location = new System.Drawing.Point(0, 411);
            this.bottomLeftPanel.Name = "bottomLeftPanel";
            this.bottomLeftPanel.Size = new System.Drawing.Size(783, 167);
            this.bottomLeftPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.backButton);
            this.panel1.Controls.Add(this.buttonDownload);
            this.panel1.Controls.Add(this.buttonAnswear);
            this.panel1.Controls.Add(this.buttonRestart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(425, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 167);
            this.panel1.TabIndex = 3;
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(107)))), ((int)(((byte)(60)))));
            this.backButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(194)))), ((int)(((byte)(190)))));
            this.backButton.FlatAppearance.BorderSize = 2;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Lucida Bright", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.backButton.Location = new System.Drawing.Point(3, 53);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(175, 41);
            this.backButton.TabIndex = 3;
            this.backButton.Text = "back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(107)))), ((int)(((byte)(60)))));
            this.buttonDownload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(194)))), ((int)(((byte)(190)))));
            this.buttonDownload.FlatAppearance.BorderSize = 2;
            this.buttonDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDownload.Font = new System.Drawing.Font("Lucida Bright", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDownload.Location = new System.Drawing.Point(3, 6);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(350, 41);
            this.buttonDownload.TabIndex = 0;
            this.buttonDownload.Text = "download";
            this.buttonDownload.UseVisualStyleBackColor = false;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // buttonAnswear
            // 
            this.buttonAnswear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(107)))), ((int)(((byte)(60)))));
            this.buttonAnswear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(194)))), ((int)(((byte)(190)))));
            this.buttonAnswear.FlatAppearance.BorderSize = 2;
            this.buttonAnswear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnswear.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAnswear.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.buttonAnswear.Location = new System.Drawing.Point(3, 100);
            this.buttonAnswear.Name = "buttonAnswear";
            this.buttonAnswear.Size = new System.Drawing.Size(350, 55);
            this.buttonAnswear.TabIndex = 2;
            this.buttonAnswear.Text = "answears";
            this.buttonAnswear.UseVisualStyleBackColor = false;
            this.buttonAnswear.Click += new System.EventHandler(this.buttonAnswear_Click);
            // 
            // buttonRestart
            // 
            this.buttonRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(107)))), ((int)(((byte)(60)))));
            this.buttonRestart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(194)))), ((int)(((byte)(190)))));
            this.buttonRestart.FlatAppearance.BorderSize = 2;
            this.buttonRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRestart.Font = new System.Drawing.Font("Lucida Bright", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRestart.Location = new System.Drawing.Point(177, 53);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(175, 41);
            this.buttonRestart.TabIndex = 1;
            this.buttonRestart.Text = "restart";
            this.buttonRestart.UseVisualStyleBackColor = false;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // backRightPanel
            // 
            this.backRightPanel.Controls.Add(this.mainRightPanel);
            this.backRightPanel.Controls.Add(this.bottomRightPanel);
            this.backRightPanel.Controls.Add(this.upRightPanel);
            this.backRightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.backRightPanel.Location = new System.Drawing.Point(783, 0);
            this.backRightPanel.Name = "backRightPanel";
            this.backRightPanel.Size = new System.Drawing.Size(336, 578);
            this.backRightPanel.TabIndex = 0;
            // 
            // mainRightPanel
            // 
            this.mainRightPanel.Controls.Add(this.textBox1);
            this.mainRightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainRightPanel.Location = new System.Drawing.Point(0, 69);
            this.mainRightPanel.Name = "mainRightPanel";
            this.mainRightPanel.Size = new System.Drawing.Size(336, 342);
            this.mainRightPanel.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(179)))), ((int)(((byte)(162)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(336, 342);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            // 
            // bottomRightPanel
            // 
            this.bottomRightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(194)))), ((int)(((byte)(190)))));
            this.bottomRightPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomRightPanel.Location = new System.Drawing.Point(0, 411);
            this.bottomRightPanel.Name = "bottomRightPanel";
            this.bottomRightPanel.Size = new System.Drawing.Size(336, 167);
            this.bottomRightPanel.TabIndex = 1;
            // 
            // upRightPanel
            // 
            this.upRightPanel.Controls.Add(this.label1);
            this.upRightPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.upRightPanel.Location = new System.Drawing.Point(0, 0);
            this.upRightPanel.Name = "upRightPanel";
            this.upRightPanel.Size = new System.Drawing.Size(336, 69);
            this.upRightPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(179)))), ((int)(((byte)(162)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Lucida Bright", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "Questions";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PuzzleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 578);
            this.Controls.Add(this.backPanel);
            this.Name = "PuzzleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PuzzleForm";
            this.backPanel.ResumeLayout(false);
            this.backLeftPanel.ResumeLayout(false);
            this.bottomLeftPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.backRightPanel.ResumeLayout(false);
            this.mainRightPanel.ResumeLayout(false);
            this.mainRightPanel.PerformLayout();
            this.upRightPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel backPanel;
        private Panel backLeftPanel;
        private Panel mainLeftPanel;
        private Panel bottomLeftPanel;
        private Panel backRightPanel;
        private Panel mainRightPanel;
        private TextBox textBox1;
        private Panel bottomRightPanel;
        private Panel upRightPanel;
        private Label label1;
        private Button buttonAnswear;
        private Button buttonRestart;
        private Button buttonDownload;
        private Panel panel1;
        private Button backButton;
    }
}