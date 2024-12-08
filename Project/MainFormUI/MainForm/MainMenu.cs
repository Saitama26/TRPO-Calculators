using GraphMethod;
using SimplexMethod;
using TransportTask;

namespace MainForm
{
    public partial class MainMenu : Form
    {
        public static string BaseAppDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static bool IsGrayTheme = true;
        private bool Drag;
        private int MouseX;
        private int MouseY;
        private bool IsSettings = true;
        private string Direction = AppDomain.CurrentDomain.BaseDirectory + @"Education\";
        List<CustomButton> baseButtons = new List<CustomButton>();
        public static Color DynamicButtonsColor = Color.Gray;

        Settings.Settings FormSettings;
        SimplexMethod.MainScreen SimplexForm;
        GraphMethod.SolvingGraphM GraphM;
        TransportTask.TransportProblemSolving TransportProblemForm;
        bool TPIsRun = false, SIsRun = false, GIsRun = false;


        public MainMenu()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            IntPtr ptr = WinAPIfunctions.RoundedControl(0, 0, Width, Height, 20, 20);
            Region = Region.FromHrgn(ptr);
            WinAPIfunctions.DeleteObject(ptr);

            FormSettings = new Settings.Settings(this);
            TransportProblemForm = new TransportProblemSolving();


            baseButtons.Add(customButton2);
            baseButtons.Add(customButton3);
            baseButtons.Add(customButton4);
            baseButtons.Add(customButton5);
            baseButtons.Add(customButton6);
            PanelOfButtonsNavigation.AutoScroll = true;

            //LoadDecorFromJson.LoadDecorFromFile(FormSettings, this, BaseAppDirectory + "Decoration\\Decor.json", "LightTheme");
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - Left;
            MouseY = Cursor.Position.Y - Top;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Drag = false;
        }

        private void imageButton5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SectionNavigationButtons(object sender, EventArgs e)
        {
            IsSettings = false;

            buttonSettings.Image = Image.FromFile(BaseAppDirectory + "\\Assets" + "\\goBack.png");
            Direction += ((CustomButton)sender).Tag + "\\";

            foreach (var button in baseButtons)
                button.Visible = false;

            PanelOfButtonsNavigation.Visible = true;

            var dirsAndFiles = GetAllSubDirectories(Direction).ToList();
            dirsAndFiles.AddRange(GetAllFilesInDir(Direction).ToList());

            if (dirsAndFiles.Count > 0)
            {

                var buttons = CreateSubButtons(dirsAndFiles, DynamicButtonsColor);
                DistributeButtons(PanelOfButtonsNavigation, buttons, 10);

            }

        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (!IsSettings)
            {
                PanelOfButtonsNavigation.Controls.Clear();

                ChangeDirection(ref Direction);

                if (IsRoot())
                {
                    buttonSettings.Image = Image.FromFile(BaseAppDirectory + "\\Assets\\settings.png");

                    PanelOfButtonsNavigation.Visible = false;
                    PanelOfButtonsNavigation.AutoScrollPosition = new Point(0);

                    foreach (var button in baseButtons)
                        button.Visible = true;

                    IsSettings = true;
                }
                else
                {
                    PanelOfButtonsNavigation.Controls.Clear();
                    var dirsAndFiles = GetAllSubDirectories(Direction).ToList();
                    dirsAndFiles.AddRange(GetAllFilesInDir(Direction).ToList());

                    PanelOfButtonsNavigation.AutoScrollPosition = new Point(0);
                    if (dirsAndFiles.Count > 0)
                    {
                        DistributeButtons(PanelOfButtonsNavigation, CreateSubButtons(dirsAndFiles, DynamicButtonsColor), 10);
                    }
                }
            }
            else
            {
                FormSettings.ShowDialog();
            }
        }


        private void imageButton1_Click(object sender, EventArgs e)
        {
            if (!GIsRun)
            {
                GraphM = new SolvingGraphM();
                GraphM.Show();
                GIsRun = true;
            }
            else{
                GraphM.Close();
                GIsRun = false;
            }
        }

        private void imageButton2_Click(object sender, EventArgs e)
        { 
            if(!SIsRun)
            {
                SimplexForm = new MainScreen();
                SimplexForm.Show();
                SIsRun = true;
            }
            else
            {
                SimplexForm.Close();
                SIsRun = false;
            }
        }

        private void imageButton3_Click(object sender, EventArgs e)
        {
            if (!TPIsRun)
            {
                TransportProblemForm = new TransportProblemSolving();
                TransportProblemForm.Show();
                TPIsRun = true;
            }
            else
            {
                TransportProblemForm.Close();
                TPIsRun = false;
            }
        }
    }
}
