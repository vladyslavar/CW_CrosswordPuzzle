using CrosswordPuzzle.DataBase;
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
using static CrosswordPuzzle.ISizeView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CrosswordPuzzle
{
    public interface ISizeView
    {
        public delegate void PuzzleFormHandler(string theme, int size);
        public event PuzzleFormHandler SwitchToPuzzleFormEvent;
        public event EventHandler SwitchToMainFormEvent;

        public delegate void LoadDb();
        public LoadDb LoadDbHandler { get; set; }
        public delegate void CloseDb();
        public LoadDb CloseDbHandler { get; set; }
        
        public delegate int MaxSizeValue(int allPossibleWords);
        public MaxSizeValue MaxSizeValueHandler { get; set; }

        public void FormRun();
        public void FormShow(string theme, int size);
        public void FormHide(string theme, int size);
        public void FormHideNoArgs(object sender, EventArgs e);
    }
    public partial class SizeForm : Form, ISizeView
    {
        private string theme;
        private int possWords;
        private int minValue = 10;

        public LoadDb LoadDbHandler { get; set; }
        public LoadDb CloseDbHandler { get; set; }
        public MaxSizeValue MaxSizeValueHandler { get; set; }


        public event ISizeView.PuzzleFormHandler SwitchToPuzzleFormEvent;
        public event EventHandler SwitchToMainFormEvent;

        public SizeForm()
        {
            InitializeComponent();
            trackBar1.ValueChanged += new EventHandler(TrackBarStep);
            textBox1.TextChanged += new EventHandler(TextChangedSteps);
            panel1.Paint += new PaintEventHandler(DrawPanel);
            this.FormClosed += new FormClosedEventHandler(form_Closed);
            trackBar1.Minimum = minValue;
            SwitchToPuzzleFormEvent += FormHide;
            SwitchToMainFormEvent += FormHideNoArgs;
        }
        private void TrackBarMaker()
        {
            int availwords = MaxSizeValueHandler(possWords);
            if(availwords < 0)
            {
                trackBar1.Hide();
                startButton.Hide();
                textBox1.Hide();
                foreach(var el in this.Controls)
                {
                    if(el.GetType() == typeof(Label))
                    {
                        Label lbl = (Label)el;
                        int point = ((this.Height / 2 - trackBar1.Size.Height + 15)) + (trackBar1.Size.Height);
                        if (lbl.Location.Y == point ) this.Controls.Remove(lbl);
                    }
                }
            }
            else
            {
                

                trackBar1.TickFrequency = 5;

                int startX = this.Width / 2 - (trackBar1.Size.Width / 2);
                int startY = (this.Height / 2 - trackBar1.Size.Height + 15);
                trackBar1.Location = new Point(startX, startY);

                int intervals = 2;
                int divider = (availwords / trackBar1.TickFrequency) - intervals;
                
                if(divider == 0)
                {
                    this.Controls.Add(new Label()
                    {
                        Font = new System.Drawing.Font("Lucida Bright", 9),
                        Location = new Point(startX + (trackBar1.Size.Width / 2), startY + trackBar1.Size.Height),
                        Text = minValue.ToString(),
                        Width = trackBar1.Size.Width / 3
                    });
                    trackBar1.Maximum = minValue;
                    return;
                }

                int step = trackBar1.Size.Width / divider;
                int value = minValue;
                trackBar1.Value = minValue;
                textBox1.Text = minValue.ToString();


                for (int i = startX; i <= (startX + trackBar1.Size.Width); i += step)
                {
                    this.Controls.Add(new Label()
                    {
                        Font = new System.Drawing.Font("Lucida Bright", 9),
                        Location = new Point(i, startY + trackBar1.Size.Height),
                        Text = value.ToString(),
                        Width = step
                    });
                    trackBar1.Maximum = value;
                    value += trackBar1.TickFrequency;
                }
                trackBar1.Show();
                startButton.Show();
                textBox1.Show();
            }
        }
        private void TrackBarStep(object sender, EventArgs e)
        {
            
            if (trackBar1.Value % 5 != 0)
            {
                if ((trackBar1.Value - (trackBar1.Value % 5)) < 1) { trackBar1.Value = 1; }
                else trackBar1.Value = trackBar1.Value - (trackBar1.Value % 5);
            }
            textBox1.Text = trackBar1.Value.ToString();
        }
        private void TextChangedSteps(object sender, EventArgs e)
        {
            Regex regex = new Regex("^\\d+$");
            if(regex.IsMatch(textBox1.Text) && Convert.ToInt32(textBox1.Text) >= minValue)
            {
                
                int num = Convert.ToInt32(textBox1.Text);

                if (num <= trackBar1.Minimum)
                {
                    int min = trackBar1.Minimum;
                    trackBar1.Value = min;
                    textBox1.Text = min.ToString();
                    return;
                }
                if (num > trackBar1.Maximum)
                {
                    int max = trackBar1.Maximum;
                    trackBar1.Value = max;
                    textBox1.Text = max.ToString();
                    return;
                }
                if (num % 5 != 0)
                {
                    if ((num - (num % 5)) < 1)
                    {
                        trackBar1.Value = 1;
                        textBox1.Text = "1";
                    }
                    else
                    {
                        trackBar1.Value = num - (num % 5);
                        textBox1.Text = trackBar1.Value.ToString();
                    }
                }
                else
                {
                    trackBar1.Value = num;
                    textBox1.Text = trackBar1.Value.ToString();
                }
            }
            
        }
        
        private void DrawPanel(object sender, PaintEventArgs e)
        {
            themeLabel.Text = "Theme: " + theme;
            amountLabel.Text = "Words amount: " + possWords.ToString();

            SizeF themeSize = e.Graphics.MeasureString(themeLabel.Text, themeLabel.Font);
            SizeF amountSize = e.Graphics.MeasureString(amountLabel.Text, amountLabel.Font);

            SizeF size = themeSize;
            if (amountSize.Width > themeSize.Width) size = amountSize;

            panel1.Width = Convert.ToInt32(size.Width + 50);
            panel1.Height = Convert.ToInt32(themeSize.Height + amountSize.Height + 60);
            panel1.Location = new Point((this.Width / 2) - (panel1.Width / 2), label1.Bottom);

            int center = Convert.ToInt32((panel1.Width / 2) - (size.Width / 2));
            themeLabel.Location = new Point(center, Convert.ToInt32(((panel1.Height / 2) - themeSize.Height) / 2));
            amountLabel.Location = new Point(center, Convert.ToInt32((panel1.Height / 2) + themeSize.Height / 2));

        }
        void form_Closed(object sender, FormClosedEventArgs e)
        {
            CloseDbHandler();
            Application.Exit();
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            SwitchToMainFormEvent.Invoke(sender, e);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            // call event PuzzleFormEvevnt
            SwitchToPuzzleFormEvent.Invoke(this.theme, trackBar1.Value);
        }

        public void FormRun()
        {
            Application.Run(this);
        }

        public void FormShow(string theme, int size)
        {
            this.theme = theme;
            this.possWords = size;
            LoadDbHandler();
            TrackBarMaker();
            this.Show();

        }

        public void FormHide(string theme, int size)
        {
            this.Hide();
        }
        public void FormHideNoArgs(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
