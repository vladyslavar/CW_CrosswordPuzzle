using CrosswordPuzzle.DataBase;
using CrosswordPuzzle.DataBase.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace CrosswordPuzzle
{
    public interface ICustomizeFormView
    {
        // event on create new pazzle
        public event EventHandler SwitchToMainFormEvent;

        public delegate void LoadDb();
        public LoadDb LoadDbHandler { get; set; }
        public delegate void CloseDb();
        public LoadDb CloseDbHandler { get; set; }
        public delegate List<string> GetCategoriesDel();
        public GetCategoriesDel GetCategoriesHandler { get; set; }
        public delegate List<CrosswordPuzzle.DataBase.DBEntities.Word> GetQuestionsByThemeDel(string theme);
        public GetQuestionsByThemeDel GetQuestionsByThemeHandler { get; set; }
        public delegate void DeleteThemeByNameDel(string theme);
        public DeleteThemeByNameDel DeleteThemeByNameHandler { get; set; }
        public delegate void AddNewThemeDel(string name);
        public AddNewThemeDel AddNewThemeHandler { get; set; }

        public delegate List<string> GetSameWordsDel(string name, string meaning);
        public GetSameWordsDel GetSameWordsHandler { get; set; }
        public delegate void AddNewWordDel(string name, string meaning, string theme);
        public AddNewWordDel AddNewWordHandler { get; set; }
        public delegate void DeleteWordDel(object word);
        public DeleteWordDel DeleteWordHandler { get; set; }

        public void FormRun();
        public void FormShow();
        public void FormHide(object sender, EventArgs e);
    }
    public partial class CustomizeForm : Form, ICustomizeFormView
    {
        private Color activeColor = Color.FromArgb(89, 107, 60);
        private Color outColor = Color.FromArgb(173, 176, 167);

        public ICustomizeFormView.LoadDb LoadDbHandler { get; set; }
        public ICustomizeFormView.LoadDb CloseDbHandler { get; set; }
        public ICustomizeFormView.GetCategoriesDel GetCategoriesHandler { get; set; }
        public ICustomizeFormView.GetQuestionsByThemeDel GetQuestionsByThemeHandler { get; set; }
        public ICustomizeFormView.DeleteThemeByNameDel DeleteThemeByNameHandler { get; set; }
        public ICustomizeFormView.AddNewThemeDel AddNewThemeHandler { get; set; }
        public ICustomizeFormView.GetSameWordsDel GetSameWordsHandler { get; set; }
        public ICustomizeFormView.AddNewWordDel AddNewWordHandler { get; set; }
        public ICustomizeFormView.DeleteWordDel DeleteWordHandler { get; set; }

        public event EventHandler SwitchToMainFormEvent;

        public CustomizeForm()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(form_Closed);
            checkedListBox1.Click += checkedListBox_Click;
            checkedListBox2.Click += checkedListBox2_Click;
            checkedListBox3.Click += checkedListBox3_Click;

            textBoxNameQuestion.GotFocus += TextBox_GotFocus;
            textBoxNameQuestion.LostFocus += TextBoxNameQuestion_LostFocus;

            textBoxQuestion.GotFocus += TextBox_GotFocus;
            textBoxQuestion.LostFocus += TextBoxQuestion_LostFocus;

            SwitchToMainFormEvent += FormHide;
        }
        
        private void GetThemesFromDB()
        {
            if(checkedListBox1.Items.Count > 0) checkedListBox1.Items.Clear();
            if(checkedListBox2.Items.Count > 0) checkedListBox2.Items.Clear();

            var categories = GetCategoriesHandler();
            foreach (var category in categories)
            {
                checkedListBox1.Items.Add(category);
                checkedListBox2.Items.Add(category);
            }
        }
        private void GetQuestionsByThemesFromDB(CheckedItemCollection themes)
        {
            if (checkedListBox3.Items.Count > 0) checkedListBox3.Items.Clear();
            if (themes.Count > 0)
            {
                foreach(var theme in themes)
                {
                    var questions = GetQuestionsByThemeHandler(theme.ToString());
                    foreach (var question in questions)
                    {
                        checkedListBox3.Items.Add(question.Name + ": " + question.Meaning);
                    }
                }
            }
        }

        private void themesButton_Click(object sender, EventArgs e)
        {
            themesButton.BackColor = activeColor;
            questionsButton.BackColor = outColor;

            panelLeft.Show();
            panelRight.Hide();

            leftPanel_Draw();
        }
        private void leftPanel_Draw()
        {

            panelLeft.Location = new Point(backButton.Right - (backButton.Width / 2), themesButton.Bottom + 25);
            panelLeft.Width = this.Width - (2 * panelLeft.Left);

            buttonNewTheme.Location = new Point(0, 0);
            panel1.Location = new Point(0, buttonNewTheme.Height);
            panel1.Width = panelLeft.Width - (3 * (buttonAdd.Width / 2));
            buttonAdd.Location = new Point(panel1.Right + Convert.ToInt32(buttonAdd.Width * 0.20), panel1.Location.Y);

            buttonDeleteTheme.Location = new Point(0, panel1.Bottom + 30);
            checkedListBox1.Location = new Point(0, buttonDeleteTheme.Bottom);
            checkedListBox1.Width = panelLeft.Width - (3 * (buttonAdd.Width / 2));
            buttonDelete.Location = new Point(checkedListBox1.Right + Convert.ToInt32(buttonDelete.Width * 0.20),
                ((checkedListBox1.Top + checkedListBox1.Bottom) / 2) - (buttonDelete.Height / 2));
        }

        ////////////////////////
        private void questionsButton_Click(object sender, EventArgs e)
        {
            questionsButton.BackColor = activeColor;
            themesButton.BackColor = outColor;

            panelRight.Show();
            panelLeft.Hide();
            rightPanel_Draw();
        }

        private void rightPanel_Draw()
        {
            panelRight.Location = new Point(backButton.Right - (backButton.Width / 2), themesButton.Bottom + 25);
            panelRight.Width = this.Width - (2 * panelRight.Left);

            buttonChooseTheme.Location = new Point(0, 0);
            checkedListBox2.Location = new Point(0, buttonNewTheme.Height);
            checkedListBox2.Height = 200;
            checkedListBox2.Width = panelLeft.Width - (3 * (buttonAdd.Width / 2));
            buttonSelect.Hide();

            buttonNewQuestion.Location = new Point(0, checkedListBox2.Bottom + 30);
            panel2.Location = new Point(0, buttonNewQuestion.Bottom);
            panel3.Location = new Point(0, panel2.Bottom + 10);
            panel2.Width = panelLeft.Width - (3 * (buttonAdd.Width / 2));
            panel3.Width = panelLeft.Width - (3 * (buttonAdd.Width / 2));
            textBoxNameQuestion.Width = panelLeft.Width - ((buttonAdd.Width / 2));
            textBoxQuestion.Width = panelLeft.Width - ((buttonAdd.Width / 2));
            buttonRightAdd.Location = new Point(panel2.Right + Convert.ToInt32(buttonRightAdd.Width * 0.20), 
                ((panel2.Top + panel3.Bottom) / 2) - (buttonRightAdd.Height / 2));

            buttonDeleteQuestion.Location = new Point(0, panel3.Bottom + 30);
            checkedListBox3.Location = new Point(0, buttonDeleteQuestion.Bottom);
            checkedListBox3.Height = 200;
            checkedListBox3.Width = panelLeft.Width - (3 * (buttonAdd.Width / 2));
            buttonRightDelete.Location = new Point(checkedListBox3.Right + Convert.ToInt32(buttonRightDelete.Width * 0.20),
                ((checkedListBox3.Top + checkedListBox3.Bottom) / 2) - (buttonRightDelete.Height / 2));
            panel4.Height = 20;
            panel4.Location = new Point(0, checkedListBox3.Bottom);
        }
        private void TextBox_GotFocus(object? sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.ForeColor == Color.Gray)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }
        private void TextBoxNameQuestion_LostFocus(object? sender, EventArgs e)
        {
            if (textBoxNameQuestion.ForeColor == Color.Gray || textBoxNameQuestion.Text == "")
            {
                textBoxNameQuestion.Text = "word";
                textBoxNameQuestion.ForeColor = Color.Gray;
            }
        }
        private void TextBoxQuestion_LostFocus(object? sender, EventArgs e)
        {
            if (textBoxQuestion.ForeColor == Color.Gray || textBoxQuestion.Text == "")
            {
                textBoxQuestion.Text = "question";
                textBoxQuestion.ForeColor = Color.Gray;
            }
        }
        private void checkedListBox_Click(object sender, EventArgs e)
        {
            CheckedListBox checkedListBox = (CheckedListBox)sender;
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                if (checkedListBox.GetItemRectangle(i).Contains(checkedListBox.PointToClient(MousePosition)))
                {
                    switch (checkedListBox.GetItemCheckState(i))
                    {
                        case CheckState.Checked:
                            checkedListBox.SetItemCheckState(i, CheckState.Unchecked);
                            break;
                        case CheckState.Indeterminate:
                        case CheckState.Unchecked:
                            checkedListBox.SetItemCheckState(i, CheckState.Checked);
                            break;
                    }
                }
            }
        }

        private void checkedListBox2_Click(object sender, EventArgs e)
        {
            CheckedListBox checkedListBox = (CheckedListBox)sender;
            List<int> indexes = new List<int>();

            foreach (var item in checkedListBox.Items)
            {
                if(checkedListBox.GetItemCheckState(checkedListBox.Items.IndexOf(item)) == CheckState.Checked)
                {
                    indexes.Add(checkedListBox.Items.IndexOf(item));
                }
            }

            foreach (var i in indexes)
            {
                checkedListBox.SetItemCheckState(i, CheckState.Unchecked);
            }
            checkedListBox_Click(sender, e);
            GetQuestionsByThemesFromDB(checkedListBox.CheckedItems);
        }
        private void checkedListBox3_Click(object sender, EventArgs e)
        {
            CheckedListBox checkedListBox = (CheckedListBox)sender;
            checkedListBox_Click(sender, e);
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            SwitchToMainFormEvent.Invoke(sender, e);
        }
        void form_Closed(object sender, FormClosedEventArgs e)
        {
            CloseDbHandler();
            Application.Exit();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var checkedThemes = checkedListBox1.CheckedItems;
            foreach(var theme in checkedThemes)
            {
                DeleteThemeByNameHandler(theme.ToString());
            }
            GetThemesFromDB();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex("^\\s+$");
            bool isempty = regex.Match(textBox1.Text).Success;
            // add new category
            if (textBox1.Text != "" && !isempty)
            {
                AddNewThemeHandler(textBox1.Text);
            }
            textBox1.Text = "";
            // refresh categories;
            GetThemesFromDB();
        }

        private void buttonRightAdd_Click(object sender, EventArgs e)
        {
            //add new word
            DataBase.DBEntities.Word word;
            Regex regexN = new Regex("^[a-zA-Z]+$");
            Regex regexQ = new Regex("^\\s+$");
            bool isNameCorrect = regexN.Match(textBoxNameQuestion.Text).Success; 
            bool isQwstionEmpty = regexQ.Match(textBoxQuestion.Text).Success;

            if (textBoxNameQuestion.Text != "" && textBoxQuestion.Text != "" && isNameCorrect && !isQwstionEmpty)
            {
                //var sameWords = dBActions.GetWordByNameAndMeaning(textBoxNameQuestion.Text, textBoxQuestion.Text);
                var sameWords = GetSameWordsHandler(textBoxNameQuestion.Text, textBoxQuestion.Text);
                if (sameWords.Count() == 0 && checkedListBox2.CheckedItems.Count > 0)
                {
                    //get id By theme
                    AddNewWordHandler(textBoxNameQuestion.Text, textBoxQuestion.Text, checkedListBox2.CheckedItems[0].ToString());
                    textBoxNameQuestion.Text = "";
                    textBoxQuestion.Text = "";
                }
                else return;
            }
            GetQuestionsByThemesFromDB(checkedListBox2.CheckedItems);
            TextBoxNameQuestion_LostFocus(sender, e);
            TextBoxQuestion_LostFocus(sender, e);
        }

        private void buttonRightDelete_Click(object sender, EventArgs e)
        {
            var checkedWords = checkedListBox3.CheckedItems;
            foreach (var word in checkedWords)
            {
                DeleteWordHandler(word);
            }
            GetQuestionsByThemesFromDB(checkedListBox2.CheckedItems);
        }

        public void FormRun()
        {
            Application.Run(this);
        }

        public void FormShow()
        {
            LoadDbHandler();
            GetThemesFromDB();
            panelRight.Hide();
            leftPanel_Draw();
            this.Show();
        }

        public void FormHide(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
