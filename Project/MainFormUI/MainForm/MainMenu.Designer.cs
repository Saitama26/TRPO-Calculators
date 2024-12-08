namespace MainForm
{
    partial class MainMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            roundedPanel1 = new RoundedPanel();
            imageButton1 = new ImageButton();
            roundedPanel10 = new RoundedPanel();
            imageButton2 = new ImageButton();
            imageButton3 = new ImageButton();
            roundedPanel11 = new RoundedPanel();
            roundedPanel12 = new RoundedPanel();
            BackgroundPanel = new RoundedPanel();
            buttonSettings = new ImageButton();
            imageButton5 = new ImageButton();
            roundedLabelWithShadows1 = new RoundedLabelWithShadows();
            roundedPanel13 = new RoundedPanel();
            customButton6 = new CustomButton();
            customButton5 = new CustomButton();
            customButton4 = new CustomButton();
            customButton3 = new CustomButton();
            customButton2 = new CustomButton();
            shadowsSettings = new RoundedPanel();
            PanelOfButtonsNavigation = new RoundedPanel();
            roundedPanel4 = new RoundedPanel();
            roundedPanel6 = new RoundedPanel();
            roundedPanel1.SuspendLayout();
            BackgroundPanel.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // roundedPanel1
            // 
            roundedPanel1.AdditionalInfo = "BackgroundPanel";
            roundedPanel1.BackColor = Color.Gray;
            roundedPanel1.Controls.Add(imageButton1);
            roundedPanel1.Controls.Add(roundedPanel10);
            roundedPanel1.Controls.Add(imageButton2);
            roundedPanel1.Controls.Add(imageButton3);
            roundedPanel1.Controls.Add(roundedPanel11);
            roundedPanel1.Controls.Add(roundedPanel12);
            roundedPanel1.Location = new Point(11, 12);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.round1 = 30;
            roundedPanel1.Size = new Size(81, 244);
            roundedPanel1.TabIndex = 1;
            roundedPanel1.Tag = "BackgroundPanel";
            roundedPanel1.Transparency = 255;
            // 
            // imageButton1
            // 
            imageButton1.AdditionalInfo = "Buttons";
            imageButton1.BackgroundImageLayout = ImageLayout.Stretch;
            imageButton1.BorderStyle = BorderStyle.None;
            imageButton1.DrawBackColor = true;
            imageButton1.ForeColor = Color.White;
            imageButton1.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Assets\\GraphM.png");
            imageButton1.Location = new Point(11, 13);
            imageButton1.Name = "imageButton1";
            imageButton1.round1 = 40;
            imageButton1.Size = new Size(60, 60);
            imageButton1.TabIndex = 6;
            imageButton1.Tag = "Buttons";
            imageButton1.Text = "imageButton1";
            imageButton1.UseVisualStyleBackColor = false;
            imageButton1.Click += imageButton1_Click;
            // 
            // roundedPanel10
            // 
            roundedPanel10.AdditionalInfo = "Shadows";
            roundedPanel10.BackColor = Color.FromArgb(64, 64, 64);
            roundedPanel10.Location = new Point(12, 21);
            roundedPanel10.Name = "roundedPanel10";
            roundedPanel10.round1 = 40;
            roundedPanel10.Size = new Size(57, 55);
            roundedPanel10.TabIndex = 3;
            roundedPanel10.Tag = "Shadows";
            roundedPanel10.Transparency = 255;
            // 
            // imageButton2
            // 
            imageButton2.AdditionalInfo = "Buttons";
            imageButton2.BackgroundImageLayout = ImageLayout.Stretch;
            imageButton2.BorderStyle = BorderStyle.None;
            imageButton2.DrawBackColor = true;
            imageButton2.ForeColor = Color.White;
            imageButton2.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Assets\\SimplexM.png");
            imageButton2.Location = new Point(13, 88);
            imageButton2.Name = "imageButton2";
            imageButton2.round1 = 40;
            imageButton2.Size = new Size(60, 60);
            imageButton2.TabIndex = 7;
            imageButton2.Tag = "Buttons";
            imageButton2.Text = "imageButton2";
            imageButton2.UseVisualStyleBackColor = false;
            imageButton2.Click += imageButton2_Click;
            // 
            // imageButton3
            // 
            imageButton3.AdditionalInfo = "Buttons";
            imageButton3.BackgroundImageLayout = ImageLayout.Stretch;
            imageButton3.BorderStyle = BorderStyle.None;
            imageButton3.DrawBackColor = true;
            imageButton3.ForeColor = Color.White;
            imageButton3.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Assets\\TransportM.png");
            imageButton3.Location = new Point(12, 166);
            imageButton3.Name = "imageButton3";
            imageButton3.round1 = 40;
            imageButton3.Size = new Size(60, 60);
            imageButton3.TabIndex = 7;
            imageButton3.Tag = "Buttons";
            imageButton3.Text = "imageButton3";
            imageButton3.UseVisualStyleBackColor = false;
            imageButton3.Click += imageButton3_Click;
            // 
            // roundedPanel11
            // 
            roundedPanel11.AdditionalInfo = "Shadows";
            roundedPanel11.BackColor = Color.FromArgb(64, 64, 64);
            roundedPanel11.Location = new Point(14, 96);
            roundedPanel11.Name = "roundedPanel11";
            roundedPanel11.round1 = 40;
            roundedPanel11.Size = new Size(57, 55);
            roundedPanel11.TabIndex = 4;
            roundedPanel11.Tag = "Shadows";
            roundedPanel11.Transparency = 255;
            // 
            // roundedPanel12
            // 
            roundedPanel12.AdditionalInfo = "Shadows";
            roundedPanel12.BackColor = Color.FromArgb(64, 64, 64);
            roundedPanel12.Location = new Point(14, 174);
            roundedPanel12.Name = "roundedPanel12";
            roundedPanel12.round1 = 40;
            roundedPanel12.Size = new Size(57, 55);
            roundedPanel12.TabIndex = 5;
            roundedPanel12.Transparency = 255;
            // 
            // BackgroundPanel
            // 
            BackgroundPanel.AdditionalInfo = "BackgroundPanel";
            BackgroundPanel.BackColor = Color.Gray;
            BackgroundPanel.Controls.Add(buttonSettings);
            BackgroundPanel.Controls.Add(imageButton5);
            BackgroundPanel.Controls.Add(roundedLabelWithShadows1);
            BackgroundPanel.Controls.Add(roundedPanel13);
            BackgroundPanel.Controls.Add(customButton6);
            BackgroundPanel.Controls.Add(customButton5);
            BackgroundPanel.Controls.Add(customButton4);
            BackgroundPanel.Controls.Add(customButton3);
            BackgroundPanel.Controls.Add(customButton2);
            BackgroundPanel.Controls.Add(shadowsSettings);
            BackgroundPanel.Controls.Add(PanelOfButtonsNavigation);
            BackgroundPanel.Location = new Point(113, 12);
            BackgroundPanel.Name = "BackgroundPanel";
            BackgroundPanel.round1 = 5;
            BackgroundPanel.Size = new Size(527, 485);
            BackgroundPanel.TabIndex = 2;
            BackgroundPanel.Tag = "BackgroundPanel";
            BackgroundPanel.Transparency = 255;
            // 
            // buttonSettings
            // 
            buttonSettings.AdditionalInfo = "Buttons";
            buttonSettings.BackgroundImageLayout = ImageLayout.Stretch;
            buttonSettings.BorderStyle = BorderStyle.None;
            buttonSettings.DrawBackColor = true;
            buttonSettings.ForeColor = Color.White;
            buttonSettings.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Assets\\Settings.png");
            buttonSettings.Location = new Point(13, 415);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.round1 = 50;
            buttonSettings.Size = new Size(60, 60);
            buttonSettings.TabIndex = 8;
            buttonSettings.Tag = "Buttons";
            buttonSettings.Text = "imageButton4";
            buttonSettings.UseVisualStyleBackColor = false;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // imageButton5
            // 
            imageButton5.AdditionalInfo = "Buttons";
            imageButton5.BackgroundImageLayout = ImageLayout.Stretch;
            imageButton5.BorderStyle = BorderStyle.None;
            imageButton5.DrawBackColor = true;
            imageButton5.ForeColor = Color.White;
            imageButton5.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Assets\\cross.png");
            imageButton5.Location = new Point(483, 9);
            imageButton5.Name = "imageButton5";
            imageButton5.round1 = 100;
            imageButton5.Size = new Size(33, 33);
            imageButton5.TabIndex = 0;
            imageButton5.Tag = "Buttons";
            imageButton5.Text = "imageButton5";
            imageButton5.UseVisualStyleBackColor = false;
            imageButton5.Click += imageButton5_Click;
            // 
            // roundedLabelWithShadows1
            // 
            roundedLabelWithShadows1.AutoSize = true;
            roundedLabelWithShadows1.BackColor = Color.Gray;
            roundedLabelWithShadows1.Font = new Font("Tahoma", 32F);
            roundedLabelWithShadows1.ForeColor = Color.White;
            roundedLabelWithShadows1.Location = new Point(28, 19);
            roundedLabelWithShadows1.Name = "roundedLabelWithShadows1";
            roundedLabelWithShadows1.Round = 0;
            roundedLabelWithShadows1.ShadowColor = Color.Black;
            roundedLabelWithShadows1.ShadowOffsetX = 5;
            roundedLabelWithShadows1.ShadowOffsetY = 5;
            roundedLabelWithShadows1.ShadowsTransperency = 50;
            roundedLabelWithShadows1.Size = new Size(214, 52);
            roundedLabelWithShadows1.TabIndex = 13;
            roundedLabelWithShadows1.Text = "Обучение";
            // 
            // roundedPanel13
            // 
            roundedPanel13.AdditionalInfo = "Shadows";
            roundedPanel13.BackColor = Color.FromArgb(64, 64, 64);
            roundedPanel13.Location = new Point(483, 11);
            roundedPanel13.Name = "roundedPanel13";
            roundedPanel13.round1 = 100;
            roundedPanel13.Size = new Size(33, 33);
            roundedPanel13.TabIndex = 8;
            roundedPanel13.Tag = "Shadows";
            roundedPanel13.Transparency = 255;
            // 
            // customButton6
            // 
            customButton6.AdditionalInfo = "Buttons";
            customButton6.BackColor = Color.FromArgb(255, 128, 0);
            customButton6.BorderStyle = BorderStyle.None;
            customButton6.Color = Color.DarkGray;
            customButton6.Font = new Font("Tahoma", 16F);
            customButton6.ForeColor = Color.White;
            customButton6.Location = new Point(28, 341);
            customButton6.Name = "customButton6";
            customButton6.round1 = 30;
            customButton6.ShadowColor = Color.Black;
            customButton6.ShadowOffsetX = 3;
            customButton6.ShadowOffsetY = 3;
            customButton6.ShadowsTransperency = 100;
            customButton6.Size = new Size(468, 47);
            customButton6.TabIndex = 4;
            customButton6.Tag = "5 Вспомогательный раздел";
            customButton6.Text = "Вспомогательный раздел";
            customButton6.UseVisualStyleBackColor = false;
            customButton6.Click += SectionNavigationButtons;
            // 
            // customButton5
            // 
            customButton5.AdditionalInfo = "Buttons";
            customButton5.BackColor = Color.FromArgb(255, 128, 0);
            customButton5.BorderStyle = BorderStyle.None;
            customButton5.Color = Color.DarkGray;
            customButton5.Font = new Font("Tahoma", 16F);
            customButton5.ForeColor = Color.White;
            customButton5.Location = new Point(28, 277);
            customButton5.Name = "customButton5";
            customButton5.round1 = 30;
            customButton5.ShadowColor = Color.Black;
            customButton5.ShadowOffsetX = 3;
            customButton5.ShadowOffsetY = 3;
            customButton5.ShadowsTransperency = 100;
            customButton5.Size = new Size(468, 47);
            customButton5.TabIndex = 3;
            customButton5.Tag = "4 Контроль знаний";
            customButton5.Text = "Контроль знаний";
            customButton5.UseVisualStyleBackColor = false;
            customButton5.Click += SectionNavigationButtons;
            // 
            // customButton4
            // 
            customButton4.AdditionalInfo = "Buttons";
            customButton4.BackColor = Color.FromArgb(255, 128, 0);
            customButton4.BorderStyle = BorderStyle.None;
            customButton4.Color = Color.DarkGray;
            customButton4.Font = new Font("Tahoma", 16F);
            customButton4.ForeColor = Color.White;
            customButton4.Location = new Point(28, 215);
            customButton4.Name = "customButton4";
            customButton4.round1 = 30;
            customButton4.ShadowColor = Color.Black;
            customButton4.ShadowOffsetX = 3;
            customButton4.ShadowOffsetY = 3;
            customButton4.ShadowsTransperency = 100;
            customButton4.Size = new Size(468, 47);
            customButton4.TabIndex = 2;
            customButton4.Tag = "3 Практический раздел";
            customButton4.Text = "Практический раздел";
            customButton4.UseVisualStyleBackColor = false;
            customButton4.Click += SectionNavigationButtons;
            // 
            // customButton3
            // 
            customButton3.AdditionalInfo = "Buttons";
            customButton3.BackColor = Color.FromArgb(255, 128, 0);
            customButton3.BorderStyle = BorderStyle.None;
            customButton3.Color = Color.DarkGray;
            customButton3.Font = new Font("Tahoma", 16F);
            customButton3.ForeColor = Color.White;
            customButton3.Location = new Point(27, 153);
            customButton3.Name = "customButton3";
            customButton3.round1 = 30;
            customButton3.ShadowColor = Color.Black;
            customButton3.ShadowOffsetX = 3;
            customButton3.ShadowOffsetY = 3;
            customButton3.ShadowsTransperency = 100;
            customButton3.Size = new Size(468, 47);
            customButton3.TabIndex = 1;
            customButton3.Tag = "2 Теоретический раздел";
            customButton3.Text = "Теоретический раздел";
            customButton3.UseVisualStyleBackColor = false;
            customButton3.Click += SectionNavigationButtons;
            // 
            // customButton2
            // 
            customButton2.AdditionalInfo = "Buttons";
            customButton2.BackColor = Color.FromArgb(255, 128, 0);
            customButton2.BorderStyle = BorderStyle.None;
            customButton2.Color = Color.DarkGray;
            customButton2.Font = new Font("Tahoma", 16F);
            customButton2.ForeColor = Color.White;
            customButton2.Location = new Point(27, 95);
            customButton2.Name = "customButton2";
            customButton2.round1 = 30;
            customButton2.ShadowColor = Color.Black;
            customButton2.ShadowOffsetX = 3;
            customButton2.ShadowOffsetY = 3;
            customButton2.ShadowsTransperency = 100;
            customButton2.Size = new Size(468, 47);
            customButton2.TabIndex = 0;
            customButton2.Tag = "1 Нормативный раздел";
            customButton2.Text = "Нормативный раздел";
            customButton2.UseVisualStyleBackColor = false;
            customButton2.Click += SectionNavigationButtons;
            // 
            // shadowsSettings
            // 
            shadowsSettings.AdditionalInfo = "Shadows";
            shadowsSettings.BackColor = Color.FromArgb(64, 64, 64);
            shadowsSettings.Location = new Point(14, 423);
            shadowsSettings.Name = "shadowsSettings";
            shadowsSettings.round1 = 50;
            shadowsSettings.Size = new Size(57, 55);
            shadowsSettings.TabIndex = 7;
            shadowsSettings.Tag = "Shadows";
            shadowsSettings.Transparency = 255;
            // 
            // PanelOfButtonsNavigation
            // 
            PanelOfButtonsNavigation.AdditionalInfo = "NavigationPanel";
            PanelOfButtonsNavigation.BackColor = SystemColors.GrayText;
            PanelOfButtonsNavigation.BorderStyle = BorderStyle.FixedSingle;
            PanelOfButtonsNavigation.Location = new Point(14, 70);
            PanelOfButtonsNavigation.Name = "PanelOfButtonsNavigation";
            PanelOfButtonsNavigation.round1 = 5;
            PanelOfButtonsNavigation.Size = new Size(501, 341);
            PanelOfButtonsNavigation.TabIndex = 11;
            PanelOfButtonsNavigation.Tag = "NavigationPanel";
            PanelOfButtonsNavigation.Transparency = 255;
            PanelOfButtonsNavigation.Visible = false;
            // 
            // roundedPanel4
            // 
            roundedPanel4.AdditionalInfo = "Shadows";
            roundedPanel4.BackColor = Color.FromArgb(64, 64, 64);
            roundedPanel4.Location = new Point(114, 441);
            roundedPanel4.Name = "roundedPanel4";
            roundedPanel4.round1 = 50;
            roundedPanel4.Size = new Size(525, 60);
            roundedPanel4.TabIndex = 4;
            roundedPanel4.Tag = "Shadows";
            roundedPanel4.Transparency = 255;
            // 
            // roundedPanel6
            // 
            roundedPanel6.AdditionalInfo = "Shadows";
            roundedPanel6.BackColor = Color.FromArgb(64, 64, 64);
            roundedPanel6.Location = new Point(12, 217);
            roundedPanel6.Name = "roundedPanel6";
            roundedPanel6.round1 = 60;
            roundedPanel6.Size = new Size(79, 42);
            roundedPanel6.TabIndex = 5;
            roundedPanel6.Tag = "Shadows";
            roundedPanel6.Transparency = 255;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Blue;
            ClientSize = new Size(652, 550);
            Controls.Add(BackgroundPanel);
            Controls.Add(roundedPanel1);
            Controls.Add(roundedPanel4);
            Controls.Add(roundedPanel6);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainMenu";
            StartPosition = FormStartPosition.CenterScreen;
            TransparencyKey = Color.Blue;
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
            roundedPanel1.ResumeLayout(false);
            BackgroundPanel.ResumeLayout(false);
            BackgroundPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private RoundedPanel roundedPanel1;
        private RoundedPanel BackgroundPanel;
        private RoundedPanel roundedPanel4;
        private RoundedPanel roundedPanel6;
        private RoundedPanel roundedPanel10;
        private RoundedPanel roundedPanel11;
        private CustomButton customButton2;
        private CustomButton customButton6;
        private CustomButton customButton5;
        private CustomButton customButton4;
        private CustomButton customButton3;
        private RoundedPanel roundedPanel12;
        private RoundedPanel shadowsSettings;
        private ImageButton imageButton1;
        private ImageButton imageButton3;
        private ImageButton imageButton2;
        private ImageButton buttonSettings;
        private RoundedPanel roundedPanel13;
        private ImageButton imageButton5;
        private RoundedPanel PanelOfButtonsNavigation;
        private RoundedLabelWithShadows roundedLabelWithShadows1;
    }
}
