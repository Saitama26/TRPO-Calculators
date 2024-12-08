using System.Drawing.Drawing2D;

namespace MainForm
{
    public class RoundedPanel : Panel
    {
        public int Transparency { get; set; } = 255;
        public int round1 { get; set; } = 20;
        public string AdditionalInfo { get; set; } = "BackgroundPanel";


        public RoundedPanel() 
        { 
            SetStyle(ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(200, 60);
            BackColor = Color.Tomato;
            BorderStyle = BorderStyle.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width, Height);
            graph.DrawRectangle(new Pen(BackColor), rect);
            graph.FillRectangle(new SolidBrush(Color.FromArgb(Transparency, BackColor)), rect);

            double k = Math.Min(Width, Height);
            IntPtr ptr = WinAPIfunctions.RoundedControl(0, 0, Width, Height, (int)(k *round1/100), (int)(k * round1/100));
            Region = Region.FromHrgn(ptr);
            WinAPIfunctions.DeleteObject(ptr);

        }

    }
    
    public class CustomButton : Button 
    {
        
        StringFormat SF = new StringFormat();

        private BorderStyle borderStyle;
        public string AdditionalInfo { get; set; } = "Buttons";

        private bool MouseEntered = false;
        private bool MousePressed = false;

        public int round1 { get; set; } = 20;
        public Color Color { get; set; } = Color.Gray;
        public Color ShadowColor { get; set; } = Color.Gray;
        public int ShadowOffsetX { get; set; } = 2;
        public int ShadowOffsetY { get; set; } = 2;
        public int ShadowsTransperency { get; set; } = 100;
        public BorderStyle BorderStyle
        {
            get { return borderStyle; }
            set { borderStyle = value; Invalidate(); }
        }

        public CustomButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(100, 40);
            BackColor = Color.DarkGray;
            borderStyle = BorderStyle.None;
            ForeColor = Color.White;
                
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            g.DrawRectangle(new Pen(Color), rect);
            g.FillRectangle(new SolidBrush(Color), rect);

            Rectangle shadowRect = new Rectangle(rect.X + ShadowOffsetX, rect.Y + ShadowOffsetY, rect.Width, rect.Height);
            g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(ShadowsTransperency, ShadowColor)), shadowRect, SF);
            g.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);

            if (MouseEntered)
            {
                g.DrawRectangle(new Pen(Color.FromArgb(60, Color.White)), rect);
                g.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.White)), rect);
            }

            if (MousePressed)
            {
                g.DrawRectangle(new Pen(Color.FromArgb(30, Color.White)), rect);
                g.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.White)), rect);
            }

            if (borderStyle != BorderStyle.None) 
            { 
                using (Pen pen = new Pen(Color.Black)) 
                    { g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1); }
            }

            double k = Math.Min(Width, Height);
            IntPtr ptr = WinAPIfunctions.RoundedControl(0, 0, Width, Height, (int)(k * round1 / 100), (int)(k * round1 / 100));
            Region = Region.FromHrgn(ptr);
            WinAPIfunctions.DeleteObject(ptr);

        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;
            Invalidate();

        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = false;
            Invalidate();

            OnClick(PaintEventArgs.Empty);
        }

    }

    public class ImageButton : Button
    {
        private BorderStyle borderStyle;
        public ContentAlignment imageAlign = ContentAlignment.MiddleCenter;
        private ImageLayout BackGroundImageLayout = ImageLayout.Stretch;
        public Color BackColor { get; set; } = Color.Gray;

        private bool MouseEntered = false;
        private bool MousePressed = false;
        public bool DrawBackColor { get; set; } = false;
        public string AdditionalInfo { get; set; } = "Buttons";

        private int Round1 = 20;

        public int round1 { get => Round1; set => Round1 = value; }

        public BorderStyle BorderStyle
        {
            get { return borderStyle; }
            set { borderStyle = value; Invalidate(); }
        }

        public new ContentAlignment ImageAlign { get => imageAlign; set { imageAlign = value; Invalidate(); } }

        public ImageButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(100, 40);
            
            BackColor = Color.DarkGray;
            borderStyle = BorderStyle.None;
            ForeColor = Color.White;
            Text = "";

            ImageAlign = imageAlign;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            g.SmoothingMode = SmoothingMode.HighQuality;

            g.Clear(Parent.BackColor);
            
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (DrawBackColor)
            {
                g.DrawRectangle(new Pen(BackColor), rect);
                g.FillRectangle(new SolidBrush(BackColor), rect);
            }
            if (!MouseEntered)
            {
                Cursor = Cursors.Hand;
            }
            if (Image != null) g.DrawImage(Image, rect);
            if (BackgroundImage != null) g.DrawImage(BackgroundImage, rect);


            double k = Math.Min(Width, Height);
            IntPtr ptr = WinAPIfunctions.RoundedControl(0, 0, Width, Height, (int)(k * Round1 / 100), (int)(k * Round1 / 100));
            Region = Region.FromHrgn(ptr);
            WinAPIfunctions.DeleteObject(ptr);
        }


        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;
            Invalidate();

        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = false;
            Invalidate();

            OnClick(PaintEventArgs.Empty);
        }



    }

    public class RoundedLabelWithShadows : Label
    {
        private StringFormat SF = new StringFormat();

        public int Round { get; set; }
        public Color ShadowColor { get; set; } = Color.Black;
        public int ShadowOffsetX { get; set; } = 2;
        public int ShadowOffsetY { get; set; } = 2;
        public int ShadowsTransperency { get; set; } = 100;
        
        public RoundedLabelWithShadows() 
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            g.DrawRectangle(new Pen(Parent.BackColor), rect);
            g.FillRectangle(new SolidBrush(BackColor), rect);

            Rectangle shadowRect = new Rectangle(rect.X + ShadowOffsetX, rect.Y + ShadowOffsetY, rect.Width, rect.Height);
            g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(ShadowsTransperency, ShadowColor)), shadowRect, SF);
            g.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);

            double k = Math.Min(Width, Height);
            IntPtr ptr = WinAPIfunctions.RoundedControl(0, 0, Width, Height, (int)(k * Round / 100), (int)(k * Round / 100));
            Region = Region.FromHrgn(ptr);
            WinAPIfunctions.DeleteObject(ptr);
        }
    }

}


