using CrosswordPuzzle.CrosswordLogic.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrosswordPuzzle
{
    public interface IAnswersFormView
    {
        public Button activatebutton { get; }
        public List<Word> words { get; }

        public void FormRun();
        public void FormShow(Button button, List<Word> words);
        public void FormHide();
    }
    public partial class AnswersForm : Form, IAnswersFormView
    {
        public Button activatebutton { get; set; }
        public List<Word> words { get; set; }

        public AnswersForm(Button button, List<Word> words)
        {
            InitializeComponent();
        }
        private void SetAnswers()
        {
            answersTextBox.Text = "";
            answersTextBox.Text += "HORIZONTAL: " + Environment.NewLine;
            foreach (var word in words)
            {
                if (word.horizontal)
                {
                    answersTextBox.Text += word.numering.ToString() + ". " + word.word + Environment.NewLine;
                }
            }
            answersTextBox.Text += Environment.NewLine + "VERTICAL: " + Environment.NewLine;
            foreach (var word in words)
            {
                if (!word.horizontal)
                {
                    answersTextBox.Text += word.numering.ToString() + ". " + word.word + Environment.NewLine;
                }
            }
        }
        private void AnswearsForm_FormLostFocus(object? sender, EventArgs e)
        {
            this.Hide();
            activatebutton.Enabled = true;
        }
        private void AnswearsForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            this.Hide();
            activatebutton.Enabled = true;
        }

        public void FormRun()
        {
            Application.Run(this);
        }

        public void FormShow(Button button, List<Word> words)
        {
            this.Show();
            this.TopMost = true;
            this.FormClosed += AnswearsForm_FormClosed;
            this.LostFocus += AnswearsForm_FormLostFocus;
            this.activatebutton = button;
            this.words = words;
            if (words != null) SetAnswers();
        }

        public void FormHide()
        {
            this.Hide();
        }
    }
}
