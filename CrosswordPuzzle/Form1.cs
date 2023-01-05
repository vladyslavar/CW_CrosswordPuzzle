using CrosswordPuzzle.DataBase;
using CrosswordPuzzle.Presentors;
using System.Collections.Generic;
using static CrosswordPuzzle.IMainFormView;

namespace CrosswordPuzzle
{
    public interface IMainFormView
    {
        public delegate void SizeFormHandler(string theme, int size);
        public event SizeFormHandler SwitchToSizeFormEvent;
        public event EventHandler SwitchToCustomizeFormEvent;

        public delegate void LoadDb();
        public LoadDb LoadDbHandler { get; set; }
        public delegate void CloseDb();
        public LoadDb CloseDbHandler { get; set; }

        public delegate List<string> GetCategoriesDel();
        public GetCategoriesDel GetCategoriesHandler { get; set; }

        public delegate ThemeSizeCount GetSizeDel(string theme);
        public GetSizeDel GetSizeHandler { get; set; }
        public void FormRun();
        public void FormShow();
        public void FormHide(string theme, int size);
        public void FormHideNoArgs(object sender, EventArgs e);
    }
    public partial class Form1 : Form, IMainFormView
    {
        public GetCategoriesDel GetCategoriesHandler { get; set; }
        public GetSizeDel GetSizeHandler { get; set; }
        public LoadDb LoadDbHandler { get; set; }
        public LoadDb CloseDbHandler { get; set; }

        public event IMainFormView.SizeFormHandler SwitchToSizeFormEvent;
        public event EventHandler SwitchToCustomizeFormEvent;

        public Form1()
        {
            InitializeComponent();

            this.listBox1.DrawItem += new DrawItemEventHandler(listBox_DrawItem);
            this.FormClosed += new FormClosedEventHandler(form_Closed);

            SwitchToSizeFormEvent += FormHide;
            SwitchToCustomizeFormEvent += FormHideNoArgs;
        }
        void GetCategoriesFromDB()
        {
            listBox1.Items.Clear();
            var categories = GetCategoriesHandler();
            foreach (var category in categories)
            {
                listBox1.Items.Add(category);
            }
        }
        void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox list = (ListBox)sender;

            
            if (e.Index > -1 && list.Items.Count > 0)
            {
                object item = list.Items[e.Index];

                Brush brush = new SolidBrush(e.ForeColor);
                Pen pen = new Pen(brush);
                SizeF size = e.Graphics.MeasureString(item.ToString(), e.Font);

                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(210, 214, 208)), e.Bounds.X + 11, e.Bounds.Y + 6, e.Bounds.Width - 21, e.Bounds.Height - 11);
                e.Graphics.DrawRectangle(pen, e.Bounds.X + 10, e.Bounds.Y + 5, e.Bounds.Width - 20, e.Bounds.Height - 10);
                e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds.Left + (e.Bounds.Width / 2 - size.Width / 2), e.Bounds.Top + (e.Bounds.Height / 2 - size.Height / 2));


                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.DrawRectangle(pen, e.Bounds.X + 10, e.Bounds.Y + 5, e.Bounds.Width - 20, e.Bounds.Height - 10);
                    e.Graphics.FillRectangle(new SolidBrush(Color.DarkOliveGreen), e.Bounds.X + 11, e.Bounds.Y + 6, e.Bounds.Width - 21, e.Bounds.Height - 11);
                    e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds.Left + (e.Bounds.Width / 2 - size.Width / 2), e.Bounds.Top + (e.Bounds.Height / 2 - size.Height / 2));
                }
            }
        }

        private void customizeButton_Click(object sender, EventArgs e)
        {
            SwitchToCustomizeFormEvent.Invoke(sender, e);
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            ThemeSizeCount themeSizeRet;
            if(listBox1.Items.Count > 0)
            {
                if (listBox1.SelectedItem == null)
                {
                    themeSizeRet = GetSizeHandler(listBox1.Items[0].ToString());
                }
                else themeSizeRet = GetSizeHandler(listBox1.SelectedItem.ToString());

                SwitchToSizeFormEvent.Invoke(themeSizeRet.Theme, themeSizeRet.Size);

            }

        }
        void form_Closed(object sender, FormClosedEventArgs e)
        {
            CloseDbHandler();
            Application.Exit();
        }

        public void FormRun()
        {
            LoadDbHandler();
            GetCategoriesFromDB();
            Application.Run(this);
        }
        public void FormShow()
        {
            this.Show();
            GetCategoriesFromDB();
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