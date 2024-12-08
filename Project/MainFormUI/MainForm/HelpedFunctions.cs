namespace MainForm
{
    public partial class MainMenu : Form
    {
        private bool IsRoot() => Direction != AppDomain.CurrentDomain.BaseDirectory + @"Education\" ? false : true;

        private void ChangeDirection(ref string Direction)
        {
            var subDirections = Direction.Split('\\').ToList();
            subDirections.RemoveAt(subDirections.Count - 1);

            if (subDirections.Count > 1)
            {
                string newStr = "";

                for (int i = 0; i < subDirections.Count - 1; i++)
                    newStr += subDirections[i] + "\\";

                Direction = newStr;
            }
        }

        private string[] GetAllSubDirectories(string path)
            => Directory.Exists(path) ?
            Directory.GetDirectories(path, ".", SearchOption.TopDirectoryOnly) : throw new DirectoryNotFoundException(); 

        private string[] GetAllFilesInDir(string path) 
            => Directory.Exists(path) ? 
            Directory.GetFiles(path, ".", SearchOption.TopDirectoryOnly) : throw new DirectoryNotFoundException();

        private List<CustomButton> CreateSubButtons(List<string> dirs, Color ColorOfButtons)
        {
            List<CustomButton> listButtons = new List<CustomButton>();

            int index = 0;
            for (int i = 0; i < dirs.Count; i++)
            {
                if (!dirs[i].Contains(".doc") || Directory.Exists(dirs[i]))
                {
                    listButtons.Add(new CustomButton()
                    {
                        Size = new Size(460, 60),
                        Text = dirs[i].Split(@"\")[^1],
                        BackColor = ColorOfButtons,
                        Color = ColorOfButtons,
                        Font = new Font("Tahoma", 12),
                        ShadowColor = Color.Black,
                        ShadowsTransperency = 50,
                    });
                    if (!File.Exists(dirs[i]))
                        listButtons[index].Click += Next;
                    else listButtons[index].Click += OpenReader;
                    index++;
                }
            }

            return listButtons;
        }

        private void OpenReader(object? sender, EventArgs e)
        {
            Viewier viewier = new Viewier();
            viewier.initializerOfAdditionalСomponents(((CustomButton)sender).Text, Direction);
            viewier.Show();
        }

        private void Next(object? sender, EventArgs e)
        {
            
            Direction  += ((CustomButton)sender).Text + "\\";
            PanelOfButtonsNavigation.Controls.Clear();

            var dirsAndFiles = GetAllFilesInDir(Direction).ToList();
            dirsAndFiles.AddRange(GetAllFilesInDir(Direction).ToList());

            if (dirsAndFiles.Count > 0)
            {
                var buttons = CreateSubButtons(dirsAndFiles, DynamicButtonsColor);
                if (buttons.Count > 0)
                    DistributeButtons(PanelOfButtonsNavigation, buttons, 10);
            }

        }

        private void DistributeButtons(RoundedPanel panel, List<CustomButton> listButtons, int SpacingButton = 10)
        {
            int totalHeight = panel.ClientSize.Height; 
            int buttonHeight = listButtons[0].Height; 
            int totalButtonHeight = listButtons.Count * buttonHeight; 

            int currentY = SpacingButton;
            
            PanelOfButtonsNavigation.AutoScrollPosition = new Point(0);
            foreach (CustomButton button in listButtons)
            {
                panel.Controls.Add(button);
                button.Location = new Point((panel.ClientSize.Width - button.Width) / 2, currentY);
                currentY += buttonHeight + SpacingButton;
            }

            panel.AutoScrollMinSize = new Size(0, currentY + SpacingButton);

        }
    }
}
