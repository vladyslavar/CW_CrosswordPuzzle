namespace CrosswordPuzzle
{
    partial class AnswersForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.answersTextBox = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mainPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(854, 531);
            this.panel2.TabIndex = 1;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.answersTextBox);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(854, 531);
            this.mainPanel.TabIndex = 4;
            // 
            // answersTextBox
            // 
            this.answersTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(194)))), ((int)(((byte)(190)))));
            this.answersTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.answersTextBox.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.answersTextBox.Location = new System.Drawing.Point(0, 0);
            this.answersTextBox.Multiline = true;
            this.answersTextBox.Name = "answersTextBox";
            this.answersTextBox.ReadOnly = true;
            this.answersTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.answersTextBox.Size = new System.Drawing.Size(854, 531);
            this.answersTextBox.TabIndex = 0;
            this.answersTextBox.TabStop = false;
            // 
            // AnswersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 531);
            this.Controls.Add(this.panel2);
            this.Name = "AnswersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnswersForm";
            this.panel2.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel2;
        private Panel mainPanel;
        private TextBox answersTextBox;
    }
}