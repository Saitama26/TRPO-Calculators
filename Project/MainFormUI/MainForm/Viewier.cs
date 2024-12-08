using AcroPDFLib;

namespace MainForm
{
    public partial class Viewier : Form
    {
        public Viewier()
        {
            InitializeComponent();

            Width = (int)(Screen.PrimaryScreen.Bounds.Width * 0.5);
            Height = (int)(Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.Bounds.Height * 0.1);
            StartPosition = FormStartPosition.CenterScreen;

            IntPtr ptr = WinAPIfunctions.RoundedControl(0, 0, Width, Height, (int)(Width * 0.02), (int)(Width * 0.02));
            Region = Region.FromHrgn(ptr);
            WinAPIfunctions.DeleteObject(ptr);

        }

        public void initializerOfAdditionalСomponents(string FileName, string Direction)
        {
            if (FileName.Contains(".pdf"))
            {
                AxAcroPDFLib.AxAcroPDF pdfViewer = new AxAcroPDFLib.AxAcroPDF();
                ((System.ComponentModel.ISupportInitialize)(pdfViewer)).BeginInit();
                Controls.Add(pdfViewer);
                ((System.ComponentModel.ISupportInitialize)(pdfViewer)).EndInit();

                pdfViewer.Width = Width - (int)(Width * 0.02);
                pdfViewer.Height = Height - (int)(Height * 0.02);
                pdfViewer.Location = new System.Drawing.Point((Width - pdfViewer.Width) / 2, (Height - pdfViewer.Height) / 2);
                pdfViewer.Dock = DockStyle.None;
                pdfViewer.BackColor = BackColor;
                pdfViewer.setShowToolbar(false);
                pdfViewer.LoadFile(Direction + FileName);
            }

            ImageButton GoBack = new ImageButton()
            {
                Image = Image.FromFile(MainMenu.BaseAppDirectory + "Assets\\cross.png"),
                Size = new Size((int)(Width * 0.05), (int)(Width * 0.05)),
                round1 = 10,
                Location = new Point((int)(Width * 0.02), (int)(Width * 0.02)),
                DrawBackColor = true,
                BackColor = Color.White,
                ImageAlign = ContentAlignment.MiddleCenter,
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            GoBack.Click += (sender, e) => Close();

            Controls.Add(GoBack);
            GoBack.BringToFront();
        }

        private void axAcropdf1_Enter(object sender, EventArgs e)
        {

        }
    }
}
