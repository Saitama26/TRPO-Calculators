namespace GraphMethod
{
    partial class SolvingGraphM
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
            dataGridView2 = new DataGridView();
            textBox1 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            plotView1 = new OxyPlot.WindowsForms.PlotView();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            panel1 = new Panel();
            label6 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label7 = new Label();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(76, 226);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(507, 150);
            dataGridView2.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(228, 128);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 21);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(73, 136);
            label1.Name = "label1";
            label1.Size = new Size(135, 13);
            label1.TabIndex = 3;
            label1.Text = "Количество ограничений";
            // 
            // button1
            // 
            button1.Location = new Point(412, 170);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(77, 397);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 7;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(508, 170);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 9;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // plotView1
            // 
            plotView1.Location = new Point(74, 51);
            plotView1.Name = "plotView1";
            plotView1.PanCursor = Cursors.Hand;
            plotView1.Size = new Size(75, 23);
            plotView1.TabIndex = 11;
            plotView1.Text = "plotView1";
            plotView1.Visible = false;
            plotView1.ZoomHorizontalCursor = Cursors.Hand;
            plotView1.ZoomRectangleCursor = Cursors.No;
            plotView1.ZoomVerticalCursor = Cursors.Hand;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(92, 164);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(38, 21);
            comboBox1.TabIndex = 12;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(188, 163);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(39, 21);
            comboBox2.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(72, 167);
            label2.Name = "label2";
            label2.Size = new Size(19, 13);
            label2.TabIndex = 14;
            label2.Text = "x1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(133, 167);
            label3.Name = "label3";
            label3.Size = new Size(13, 13);
            label3.TabIndex = 15;
            label3.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(169, 166);
            label4.Name = "label4";
            label4.Size = new Size(19, 13);
            label4.TabIndex = 16;
            label4.Text = "x2";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(228, 166);
            label5.Name = "label5";
            label5.Size = new Size(13, 13);
            label5.TabIndex = 17;
            label5.Text = "0";
            // 
            // panel1
            // 
            panel1.Controls.Add(plotView1);
            panel1.Location = new Point(577, 39);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 100);
            panel1.TabIndex = 18;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(0, 13);
            label6.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(64, 39);
            label7.Name = "label7";
            label7.Size = new Size(35, 13);
            label7.TabIndex = 19;
            label7.Text = "label7";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(64, 58);
            label8.Name = "label8";
            label8.Size = new Size(35, 13);
            label8.TabIndex = 20;
            label8.Text = "label8";
            // 
            // SolvingGraphM
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(panel1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(dataGridView2);
            Controls.Add(label6);
            Name = "SolvingGraphM";
            Text = "Графический метод";
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView2;
        private TextBox textBox1;
        private Label label1;
        private Button button1;
        private Button button2;
        private Button button3;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Panel panel1;
        private Label label6;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label7;
        private Label label8;
    }
}
