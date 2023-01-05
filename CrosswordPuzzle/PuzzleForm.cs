using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrosswordPuzzle.CrosswordLogic;
using CrosswordPuzzle.CrosswordLogic.Structs;
using CrosswordPuzzle.DataBase;
using CrosswordPuzzle.Presentors;
using static System.Windows.Forms.DataFormats;

namespace CrosswordPuzzle
{
    public interface IPuzzleView
    {
        public List<List<char>> _listToUse { get; }
        public List<Word> _words { get; }

        public event EventHandler SwitchToMainFormEvent;
        public delegate void AnswersFormHandler(Button button, List<Word> words);
        public event AnswersFormHandler SwitchToAnswersFormEvent;

        public delegate void LoadDb();
        public LoadDb LoadDbHandler { get; set; }
        public delegate void CloseDb();
        public LoadDb CloseDbHandler { get; set; }
        public delegate Dictionaries InitDictionariesDel(int size, string theme);
        public InitDictionariesDel InitDictionariesHandler { get; set; }
        public delegate TrimmedData SetCrosswordPuzzleDel(Dictionary<string, string> obligatoryDictionary, Dictionary<string, string> additionalDictionary);
        public SetCrosswordPuzzleDel SetCrosswordPuzzleHandler { get; set; }

        public void FormRun();
        public void FormShow(string theme, int size);
        public void FormHideNoArgs(object? sender, EventArgs e);
        
    }

    public partial class PuzzleForm : Form, IPuzzleView
    {
        private List<Letter> letterPossitions = new List<Letter>();
        public List<List<char>> listToUse;
        private List<Word> words;
        private int startTop;
        private int startLeft;

        private string theme;
        private int size;

        public event EventHandler SwitchToMainFormEvent;
        public event IPuzzleView.AnswersFormHandler SwitchToAnswersFormEvent;

        public List<List<char>> _listToUse { get { return listToUse; }}
        public List<Word> _words { get { return words; }}

        public IPuzzleView.InitDictionariesDel InitDictionariesHandler { get; set; }
        public IPuzzleView.LoadDb LoadDbHandler { get; set; }
        public IPuzzleView.LoadDb CloseDbHandler { get; set; }
        public IPuzzleView.SetCrosswordPuzzleDel SetCrosswordPuzzleHandler { get; set; }

        public PuzzleForm()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(form_Closed);
            this.SizeChanged += Form_SizeChanged;
            SwitchToMainFormEvent += FormHideNoArgs;
        }

        void ButtonsDraw()
        {
            panel1.Width = bottomLeftPanel.Width - bottomRightPanel.Width;
            buttonDownload.Width = panel1.Width - (panel1.Width / 5);
            backButton.Width = (panel1.Width - (panel1.Width / 5)) / 2;
            buttonRestart.Width = (panel1.Width - (panel1.Width / 5))/2;
            buttonAnswear.Width = panel1.Width - (panel1.Width / 5);
            buttonDownload.Left = panel1.Width / 10;
            backButton.Left = panel1.Width / 10;
            buttonRestart.Left = backButton.Right;
            buttonAnswear.Left = panel1.Width / 10;
        }
        private void Form_SizeChanged(object? sender, EventArgs e)
        {
            ButtonsDraw();
        }
        private void MakePuzzle()
        {
            var retDicts = InitDictionariesHandler(size, theme);
            Dictionary<string, string> obligatoryDictionary = retDicts.obligatoryDictionary;
            Dictionary<string, string> additionalDictionary = retDicts.additionalDictionary;

            TrimmedData data = SetCrosswordPuzzleHandler(obligatoryDictionary, additionalDictionary);
            listToUse = data.array;
            words = data.words;
        }
        private void DrawPuzzle()
        {
            Font numFont = new Font("Lucida Bright", 8);
            Font font = new Font("Lucida Bright", 14);
            startTop = (this.mainLeftPanel.Height / 2) - ((font.Height + (font.Height / 3)) * listToUse.Count);
            startLeft = (this.mainLeftPanel.Width / 2) - ((font.Height + (font.Height / 3)) * listToUse[0].Count);

            if (startTop < 10) startTop = 40;
            if (startLeft < 10) startLeft = 40;

             foreach(var word in words)
            {
                int top = startTop + ((font.Height + (font.Height / 3)) * word.possition.X);
                int left = startLeft + ((font.Height + (font.Height / 3)) * word.possition.Y);
                if (word.horizontal)
                {
                    this.mainLeftPanel.Controls.Add(new Label()
                    {
                        Text = word.numering.ToString(),
                        Font = numFont,
                        ForeColor = Color.Black,
                        BackColor = Color.FromArgb(153, 158, 145),
                        Top = top + (font.Height / 3),
                        Left = left - (font.Height / 3) - numFont.Height,
                        Width = (font.Height / 3) + numFont.Height,

                        
                    });
                    foreach (var letter in word.word.ToCharArray())
                    {
                        letterPossitions.Add(new Letter(letter, new Possition(left, top)));
                        this.mainLeftPanel.Controls.Add(new System.Windows.Forms.TextBox()
                        {
                            TextAlign = HorizontalAlignment.Center,
                            MaxLength = 1,
                            Font = font,
                            Multiline = true,
                            Height = font.Height + (font.Height / 3),
                            Width = font.Height + (font.Height / 3),
                            Top = top,
                            Left = left,

                        });
                        left += font.Height + (font.Height / 3);
                    }
                }
                if(!word.horizontal)
                {
                    this.mainLeftPanel.Controls.Add(new Label()
                    {
                        Text = word.numering.ToString(),
                        Font = numFont,
                        ForeColor = Color.Black,
                        BackColor = Color.FromArgb(153, 158, 145),
                        Top = top - (font.Height / 3) - numFont.Height - 1,
                        Left = left + (font.Height / 3),
                        Width = font.Height + (font.Height / 3),
                    });
                    foreach (var letter in word.word.ToCharArray())
                    {
                        letterPossitions.Add(new Letter(letter, new Possition(left, top)));
                        this.mainLeftPanel.Controls.Add(new System.Windows.Forms.TextBox()
                        {
                            TextAlign = HorizontalAlignment.Center,
                            MaxLength = 1,
                            Font = font,
                            Multiline = true,
                            Height = font.Height + (font.Height / 3),
                            Width = font.Height + (font.Height / 3),
                            Top = top,
                            Left = left,

                        });
                        top += font.Height + (font.Height / 3);
                    }
                }
            }
            foreach (Control control in mainLeftPanel.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    control.TextChanged += Control_TextChanged;
                }
            }
        }

        private void Control_TextChanged(object? sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Regex regex = new Regex("[a-z]");

            if (!regex.Match(textBox.Text).Success)
            {
                textBox.Text = "";
            }
            else
            {
                int increment = 0;

                var font = new Font("Lucida Bright", 14);
                Font numFont = new Font("Lucida Bright", 8);
                int step = font.Height + (font.Height / 3);
                bool horizontal = false, vertical = false;

                List<Control> correctControls = new List<Control>();

                increment = step;
                foreach (var el in letterPossitions)
                {
                    if (el.possition.X == textBox.Location.X + increment && el.possition.Y == textBox.Location.Y) horizontal = true;
                    if (el.possition.X == textBox.Location.X - increment && el.possition.Y == textBox.Location.Y) horizontal = true;
                    if (el.possition.X == textBox.Location.X && el.possition.Y == textBox.Location.Y + increment) vertical = true;
                    if (el.possition.X == textBox.Location.X && el.possition.Y == textBox.Location.Y - increment) vertical = true;
                }

                if (vertical)
                {
                    increment = step;
                    Letter firstLetterInWord;
                    if (textBox.Text == "")
                        firstLetterInWord = new Letter('0', new Possition(textBox.Location.X, textBox.Location.Y));
                    else
                        firstLetterInWord = new Letter(Convert.ToChar(textBox.Text), new Possition(textBox.Location.X, textBox.Location.Y));

                    bool exists = false;
                    do
                    {
                        exists = false;
                        foreach (var el in letterPossitions)
                        {
                            if (el.possition.X == textBox.Location.X && el.possition.Y == textBox.Location.Y - increment)
                            {
                                exists = true;
                                firstLetterInWord = new Letter(el.letter, new Possition(el.possition.X, el.possition.Y));
                                increment += step;
                                break;
                            }
                        }
                    } while (exists);

                    foreach (var word in words)
                    {
                        int left = startLeft + ((font.Height + (font.Height / 3)) * word.possition.Y);
                        int top = startTop + ((font.Height + (font.Height / 3)) * word.possition.X);

                        if (firstLetterInWord.possition.X == left && firstLetterInWord.possition.Y == top)
                        {
                            var letters = word.word.ToCharArray();
                            bool correctWord = true;
                            foreach (var letter in word.word.ToCharArray())
                            {
                                foreach (Control control in mainLeftPanel.Controls)
                                {
                                    if (control.Location.X == firstLetterInWord.possition.X &&
                                        control.Location.Y == firstLetterInWord.possition.Y &&
                                        control.Text != letter.ToString())
                                    {
                                        correctWord = false;
                                        break;
                                    }
                                    if (control.Location.X == firstLetterInWord.possition.X &&
                                        control.Location.Y == firstLetterInWord.possition.Y &&
                                        control.Text == letter.ToString())
                                    {
                                        correctControls.Add(control);
                                        break;
                                    }
                                }
                                if (!correctWord) break;
                                else firstLetterInWord.possition.Y += step;
                            }
                            if (correctWord)
                            {
                                foreach (var c in correctControls)
                                {
                                    c.BackColorChanged += C_BackColorChanged;
                                    c.BackColor = Color.Tan;
                                }
                            }
                        }
                    }
                }

                if (horizontal)
                {
                    increment = step;
                    Letter firstLetterInWord;
                    if (textBox.Text == "")
                        firstLetterInWord = new Letter('0', new Possition(textBox.Location.X, textBox.Location.Y));
                    else
                        firstLetterInWord = new Letter(Convert.ToChar(textBox.Text), new Possition(textBox.Location.X, textBox.Location.Y));
                    bool exists = false;

                    do
                    {
                        exists = false;
                        foreach (var el in letterPossitions)
                        {
                            if (el.possition.X == textBox.Location.X - increment && el.possition.Y == textBox.Location.Y)
                            {
                                exists = true;
                                firstLetterInWord = new Letter(el.letter, new Possition(el.possition.X, el.possition.Y));
                                increment += step;
                                break;
                            }
                        }
                    } while (exists);

                    foreach (var word in words)
                    {
                        int left = startLeft + ((font.Height + (font.Height / 3)) * word.possition.Y);
                        int top = startTop + ((font.Height + (font.Height / 3)) * word.possition.X);

                        if (firstLetterInWord.possition.X == left && firstLetterInWord.possition.Y == top)
                        {
                            var letters = word.word.ToCharArray();
                            bool correctWord = true;
                            foreach (var letter in word.word.ToCharArray())
                            {
                                foreach (Control control in mainLeftPanel.Controls)
                                {
                                    if (control.Location.X == firstLetterInWord.possition.X &&
                                        control.Location.Y == firstLetterInWord.possition.Y &&
                                        control.Text != letter.ToString())
                                    {
                                        correctWord = false;
                                        break;
                                    }
                                    if (control.Location.X == firstLetterInWord.possition.X &&
                                        control.Location.Y == firstLetterInWord.possition.Y &&
                                        control.Text == letter.ToString())
                                    {
                                        correctControls.Add(control);
                                        break;
                                    }
                                }
                                if (!correctWord) break;
                                else firstLetterInWord.possition.X += step;
                            }
                            if (correctWord)
                            {
                                foreach (var c in correctControls)
                                {
                                    c.BackColorChanged += C_BackColorChanged;
                                    c.BackColor = Color.Tan;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void C_BackColorChanged(object? sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.ReadOnly = true;
        }
        private void AddQuestions()
        {
            textBox1.Text += "HORIZONTAL: " + Environment.NewLine;
            foreach(var word in words)
            {
                if(word.horizontal)
                {
                    textBox1.Text += word.numering.ToString() + ". " + word.meaning + Environment.NewLine;
                }
            }
            textBox1.Text += Environment.NewLine + "VERTICAL: " + Environment.NewLine;
            foreach (var word in words)
            {
                if (!word.horizontal)
                {
                    textBox1.Text += word.numering.ToString() + ". " + word.meaning + Environment.NewLine;
                }
            }
        }
        void form_Closed(object sender, FormClosedEventArgs e)
        {
            CloseDbHandler();
            Application.Exit();
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            PDFwritter pdfwritter = new PDFwritter(words, listToUse);
            pdfwritter.WritePDF();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            mainLeftPanel.Controls.Clear();
            textBox1.Text = "";
            MakePuzzle();
            DrawPuzzle();
            AddQuestions();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            mainLeftPanel.Controls.Clear();
            textBox1.Text = "";
            SwitchToMainFormEvent.Invoke(sender, e);
        }

        private void buttonAnswear_Click(object sender, EventArgs e)
        {
            
            Button clickedButton = (Button)sender;
            SwitchToAnswersFormEvent.Invoke(clickedButton, words);
        }

        public void FormRun()
        {
            Application.Run(this);
            /*
            LoadDbHandler();
            ButtonsDraw();
            MakePuzzle();
            DrawPuzzle();
            AddQuestions();*/
        }
        public void FormShow(string theme, int size)
        {
            this.size = size;
            this.theme = theme;
            LoadDbHandler();
            MakePuzzle();
            DrawPuzzle();
            AddQuestions();
            this.Show();
        }
        public void FormHideNoArgs(object? sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
