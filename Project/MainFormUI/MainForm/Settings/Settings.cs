namespace MainForm.Settings
{
    public partial class Settings : Form
    {
        MainMenu Parent;

        public Settings(MainMenu parent)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            var ScreenBounds = Screen.PrimaryScreen.Bounds;
            Parent = parent;

            Size = new Size((int)(Parent.Width / 2), (int)(Parent.Height / 2));
            StartPosition = FormStartPosition.CenterScreen;

            FormBorderStyle = FormBorderStyle.None;

            BackColor = Color.FromArgb(255, 109, 109, 109);
            
            InitAdditionComponents();

            IntPtr ptr = WinAPIfunctions.RoundedControl(0, 0, Width, Height, 20, 20);
            Region = Region.FromHrgn(ptr);
            WinAPIfunctions.DeleteObject(ptr);
        }

        private void InitAdditionComponents()
        {
            ImageButton CloseSettings = new ImageButton()
            {
                Size = new Size((int)(Width * 0.08), (int)(Width * 0.08)),
                round1 = 100,
                ImageAlign = ContentAlignment.MiddleCenter,
                BackgroundImageLayout = ImageLayout.Stretch,
                BackgroundImage = Image.FromFile(MainMenu.BaseAppDirectory + "Assets\\cross.png"),
                DrawBackColor = true
            };
            
            CloseSettings.Click += (sender, e) => Close();

            RoundedLabelWithShadows FormName = new RoundedLabelWithShadows()
            {
                Text = "Настройки",
                BackColor = BackColor,
                Font = new Font("Tahoma", 24),
                AutoSize = true,
                ShadowOffsetX = 3,
                ShadowOffsetY = 3,
                ShadowsTransperency = 70
            };
            RoundedLabelWithShadows Theme = new RoundedLabelWithShadows()
            {
                Text = "Тема",
                BackColor = BackColor,
                Font = new Font("Tahoma", 14),
                AutoSize = true,
                ShadowOffsetX = 2,
                ShadowOffsetY = 2,
                ShadowsTransperency = 30
            };



            RadioButton GrayTheme = new RadioButton()
            {
                Text = "Светлая тема",
                Checked = true,
                Font = new Font("Tahoma", 10),
                AutoSize = true
            };

            RadioButton DarkTheme = new RadioButton()
            {
                Text = "Темная тема",
                Checked = false,
                Font = new Font("Tahoma", 10),
                AutoSize = true
            };
            GrayTheme.CheckedChanged += TurnColors;



            Controls.Add(CloseSettings);
            Controls.Add(FormName);
            Controls.Add(Theme);
            Controls.Add(GrayTheme);
            Controls.Add(DarkTheme);

            CloseSettings.Location = new Point((int)(Width - CloseSettings.Width - Width * 0.02), (int)(Height * 0.02));
            FormName.Location = new Point((Width - FormName.Width) / 2, (Height - FormName.Height) / 20);
            Theme.Location = new Point((Width - Theme.Width) / 20, (int)(Height - Theme.Height - FormName.Height - Height * 0.1) / 4);
            GrayTheme.Location = new Point((int)(Theme.Location.X + Width * 0.05), (int)(Theme.Location.Y + Theme.Height + Height * 0.02));
            DarkTheme.Location = new Point(GrayTheme.Location.X, (int)(GrayTheme.Location.Y + GrayTheme.Height + Height * 0.02));

        }

        private void TurnColors(object? sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked && (sender as RadioButton).Text == "Светлая тема")
            {
                foreach (var c in Parent.Controls)
                {
                    try
                    {

                        if (c != null)
                        {
                            if ((c as RoundedPanel).AdditionalInfo == "BackgroundPanel")
                            {
                                (c as RoundedPanel).BackColor = Color.Gray;

                                foreach (var cd in (c as RoundedPanel).Controls)
                                {
                                    if ((cd as CustomButton) != null)
                                    {
                                        (cd as CustomButton).Color = Color.DarkGray;
                                        (cd as CustomButton).BackColor = Color.DarkGray;
                                        MainMenu.DynamicButtonsColor = Color.Gray;
                                        continue;
                                    }
                                    else if ((cd as ImageButton) != null)
                                    {
                                        //(cd as ImageButton).Color = Color.Silver;
                                        (cd as ImageButton).BackColor = Color.Silver;
                                        MainMenu.DynamicButtonsColor = Color.Gray;
                                        continue;

                                    }

                                    if ((cd as RoundedLabelWithShadows) != null)
                                    {
                                        (cd as RoundedLabelWithShadows).BackColor = (c as RoundedPanel).BackColor;
                                        continue;
                                    }

                                    if ((cd as RoundedPanel) != null)
                                    {
                                        if ((cd as RoundedPanel).AdditionalInfo == "NavigationPanel"){
                                            (cd as RoundedPanel).BackColor = Color.FromArgb(109, 109, 109);
                                            continue;
                                        }

                                        if ((cd as RoundedPanel).AdditionalInfo == "Shadows")
                                        {
                                            (cd as RoundedPanel).BackColor = Color.FromArgb(64, 64, 64);
                                            continue;
                                        }
                                    }
                                }
                            }

                            else if ((c as RoundedPanel).AdditionalInfo == "Shadows")
                                (c as RoundedPanel).BackColor = Color.FromArgb(64, 64, 64);
                        }

                    }
                    catch (Exception) { }
                }

                BackColor = Color.FromArgb(255, 109, 109, 109);
                foreach (var c in Controls) {
                    try
                    {
                        if (c != null){
                            if ((c as RoundedLabelWithShadows) != null)
                            {
                                (c as RoundedLabelWithShadows).BackColor = BackColor;
                                (c as RoundedLabelWithShadows).ForeColor = Color.Black;
                                continue;
                            }
                            if ((c as RadioButton) != null){
                                (c as RadioButton).ForeColor = Color.White;
                                continue;
                            }
                            if ((c as ImageButton) != null)
                            {
                                //(c as ImageButton).Color = Color.DarkGray;
                                (c as ImageButton).BackColor = Color.DarkGray;
                            }

                        }
                    }
                    catch (Exception) { }
                }

            } else 
            {
                foreach (var c in Parent.Controls)
                {
                    try
                    {

                        if (c != null)
                        {
                            if ((c as RoundedPanel).AdditionalInfo == "BackgroundPanel")
                            {
                                (c as RoundedPanel).BackColor = Color.FromArgb(37, 40, 59);

                                foreach (var cd in (c as RoundedPanel).Controls)
                                {
                                    if ((cd as CustomButton) != null)
                                    {
                                        (cd as CustomButton).Color = Color.FromArgb(62, 84, 141);
                                        (cd as CustomButton).BackColor = Color.FromArgb(62, 84, 141);
                                        MainMenu.DynamicButtonsColor = Color.Gray;
                                        continue;
                                    }
                                    else if ((cd as ImageButton) != null)
                                    {
                                        //(cd as ImageButton).Color = Color.FromArgb(62, 84, 141);
                                        (cd as ImageButton).BackColor = Color.FromArgb(62, 84, 141);
                                        MainMenu.DynamicButtonsColor = Color.Gray;
                                        continue;

                                    }

                                    if ((cd as RoundedLabelWithShadows) != null)
                                    {
                                        (cd as RoundedLabelWithShadows).BackColor = (c as RoundedPanel).BackColor;
                                        continue;
                                    }

                                    if ((cd as RoundedPanel) != null)
                                    {
                                        if ((cd as RoundedPanel).AdditionalInfo == "NavigationPanel")
                                        {
                                            (cd as RoundedPanel).BackColor = Color.FromArgb(27, 30, 49);
                                            continue;
                                        }

                                        if ((cd as RoundedPanel).AdditionalInfo == "Shadows")
                                        {
                                            (cd as RoundedPanel).BackColor = Color.FromArgb(12, 13, 28);
                                            continue;
                                        }
                                    }
                                }
                            }

                            else if ((c as RoundedPanel).AdditionalInfo == "Shadows")
                                (c as RoundedPanel).BackColor = Color.FromArgb(12, 13, 28);
                        }

                    }
                    catch (Exception) { }
                }
                BackColor = Color.FromArgb(37, 40, 59);
                foreach (var c in Controls)
                {
                    try
                    {
                        if (c != null){
                            if ((c as RoundedLabelWithShadows) != null)
                            {
                                (c as RoundedLabelWithShadows).BackColor = BackColor;
                                (c as RoundedLabelWithShadows).ForeColor = Color.White;
                                continue;
                            }
                            if ((c as RadioButton) != null) {
                                (c as RadioButton).ForeColor = Color.White;
                                continue;
                            }
                            if ((c as ImageButton) != null)
                            {
                                //(c as ImageButton).Color = Color.FromArgb(62, 84, 141);
                                (c as ImageButton).BackColor = Color.FromArgb(62, 84, 141);
                                MainMenu.DynamicButtonsColor = Color.FromArgb(62, 84, 141);
                            }


                        }       
                    }
                    catch (Exception) { }
                }
                
            }
            Update();

        }
    }
}
