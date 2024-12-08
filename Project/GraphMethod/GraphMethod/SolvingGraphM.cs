namespace GraphMethod
{
    public partial class SolvingGraphM : Form
    {
        Chart chart;
        ComboBox maxOrMin;
        ModelForPlotViev model1, model2, model3;
        bool IsM1 = true, IsM2 = false, IsM3 = false;
        string text1, text2, text3;
        Font font = new Font("Times New Roman", 14);

        public SolvingGraphM()
        {
            InitializeComponent();
            MinimumSize = new Size(700, 600);
            Size = MinimumSize;

            plotView1.Dock = DockStyle.Fill;

            panel1.Size = new Size(Width, (int)(Height * 0.8));
            panel1.SendToBack();
            panel1.BorderStyle = BorderStyle.None;


            label6.Size = new Size((int)(panel1.Width * 0.9), (int)(panel1.Height * 0.4));
            label6.Font = font;
            label6.TextAlign = ContentAlignment.TopLeft;
            label6.AutoSize = true;

            label7.AutoSize = true;
            label7.Text = "Целевая функция стремится к";
            label7.Font = font;
            label7.Location = new Point((int)(Width * 0.05), label7.Height);

            label8.Text = "F = ";
            label8.Font = font;
            label8.Location = new Point(label7.Location.X, label7.Location.Y + label7.Height + (int)(Height * 0.02));
            TextBox x1 = new TextBox() { Name = "x1", Size = new Size((int)(Width * 0.04), (int)(Height * 0.04)), Location = new Point(label8.Location.X + label8.Width, label8.Location.Y) };
            Label x1d = new Label() { Name = "x1d", Text = "x1 + ", AutoSize = true, Font = font, Location = new Point(x1.Location.X + x1.Width, x1.Location.Y), };
            TextBox x2 = new TextBox() { Name = "x2", Size = new Size((int)(Width * 0.04), (int)(Height * 0.04)), Location = new Point(x1d.Location.X + x1.Width + (int)(Width * 0.02), label8.Location.Y) };
            Label x2d = new Label() { Name = "x2d", Text = "x2", AutoSize = true, Font = font, Location = new Point(x2.Location.X + x2.Width, x1.Location.Y), };

            maxOrMin = new ComboBox() { Size = new Size((int)(Width * 0.1), (int)(Height * 0.04)), Location = new Point(label7.Location.X + label7.Width + (int)(Width * 0.02), label7.Location.Y) };
            maxOrMin.Items.AddRange(new string[] { "Max", "Min" });

            label1.Font = font;
            label1.Location = new Point(label8.Location.X, label8.Location.Y + label8.Height + (int)(Height * 0.02));
            textBox1.Size = maxOrMin.Size;
            textBox1.Location = new Point(label1.Location.X + label1.Width + (int)(Width * 0.02), label1.Location.Y);

            label2.Font = font;
            label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + (int)(Height * 0.05));
            comboBox1.Location = new Point(label2.Location.X + label2.Width, label2.Location.Y);
            label3.Font = font;
            label3.Location = new Point(comboBox1.Location.X + comboBox1.Width, comboBox1.Location.Y);

            label4.Font = font;
            label4.Location = new Point(label3.Location.X + label3.Width * 3, label3.Location.Y);
            comboBox2.Location = new Point(label4.Location.X + label4.Width, label4.Location.Y);
            label5.Font = font;
            label5.Location = new Point(comboBox2.Location.X + comboBox2.Width, label4.Location.Y);

            button1.Text = "Создать";
            button1.Font = font;
            button1.AutoSize = true;
            button1.Location = new Point(label5.Location.X + label5.Width * 3, label5.Location.Y);

            button3.Visible = false;

            dataGridView2.Size = new Size((int)(Width * 0.9), (int)(Height * 0.4));
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.Font = font;
            dataGridView2.Location = new Point((Width - dataGridView2.Width) / 2, button1.Location.Y + (int)(Width * 0.1));

            button2.Font = font;
            button2.Text = "Решить";
            button2.AutoSize = true;
            button2.Location = new Point(dataGridView2.Location.X, dataGridView2.Location.Y + dataGridView2.Height + (int)(Height * 0.05));

            chart = new Chart(plotView1);
            comboBox1.Items.AddRange(new string[] { ">=", "<=" });
            comboBox2.Items.AddRange(new string[] { ">=", "<=" });

            Controls.Add(maxOrMin);
            Controls.Add(x1);
            Controls.Add(x2);
            Controls.Add(x1d);
            Controls.Add(x2d);

            maxOrMin.BringToFront();
        }

        private void CreateChart()
        {
            model1 = chart.CreateModel("Графическое решение ЗЛП");
            model1.addAxisX(Minimum: -20, Maximum: 20);
            model1.addAxisY(Minimum: -20, Maximum: 20);


            model2 = chart.CreateModel("Графическое решение ЗЛП");
            model2.addAxisX(Minimum: -20, Maximum: 20);
            model2.addAxisY(Minimum: -20, Maximum: 20);


            model3 = chart.CreateModel("Графическое решение ЗЛП");
            model3.addAxisX(Minimum: -20, Maximum: 20);
            model3.addAxisY(Minimum: -20, Maximum: 20);


            string minOrMax = maxOrMin?.SelectedItem?.ToString() ?? "Min";
            string x1Condition = comboBox1?.SelectedItem?.ToString() ?? ">=";
            string x2Condition = comboBox2?.SelectedItem?.ToString() ?? ">=";

            var constraints = new List<Tuple<float, float, float, string>>();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;
                float x1 = float.TryParse(row.Cells[0].Value?.ToString(), out x1) ? x1 : 0;
                float x2 = float.TryParse(row.Cells[1].Value?.ToString(), out x2) ? x2 : 0;
                string sign = row.Cells[2].Value?.ToString() ?? ">=";
                float num = float.TryParse(row.Cells[3].Value?.ToString(), out num) ? num : 0;

                constraints.Add(new Tuple<float, float, float, string>(x1, x2, num, sign));
            }

            var X1 = Controls.Find("x1", false)[0].Text;
            var X2 = Controls.Find("x2", false)[0].Text;

            float x = 0, y = 0;

            x = float.TryParse(X1, out x) ? x : 1;
            y = float.TryParse(X2, out y) ? y : 1;

            model1.DrawLines(constraints);
            model2.DrawLines(constraints);
            model3.DrawLines(constraints);


            if (!model2.IsSystemOfConstraintsConsistent(constraints, x1Condition, x2Condition))
            {
                text1 = $"Система ограничений задачи несовместима";
                text2 = $"Система ограничений задачи несовместима";
                text3 = $"Система ограничений задачи несовместима";
            }
            else if (model2.HasMultipleSolutions(constraints, x, y, minOrMax, x1Condition, x2Condition, out var point))
            {
                model2.DrawRangeOfAcceptableValues(constraints, x1Condition, x2Condition);
                model3.DrawRangeOfAcceptableValues(constraints, x1Condition, x2Condition);
                text1 = $"Необходимо найти {((minOrMax == "Min") ? "минимальное" : "максимальное")} значение функции F = {(x == 0 ? "" : x.ToString() + "X1")} " +
               $"{(y == 0 ? "" : (y < 0 ? "- " + y.ToString() + "X2" : "+" + y.ToString() + "X2"))}" +
               $" -> {minOrMax} при системе ограничений:\n";
                for (int i = 0; i < constraints.Count; i++)
                {
                    text1 += $"{(constraints[i].Item1 == 0 ? "" : constraints[i].Item1 + "X1")} {(constraints[i].Item2 == 0 ? "" : (constraints[i].Item2 < 0 ? "- " + constraints[i].Item2 + "X2" : "+ " + constraints[i].Item2.ToString() + "X2"))} {constraints[i].Item4} {constraints[i].Item3} \t\t({i + 1})\n";
                }
                text1 += $"x1 {x1Condition} 0 \tx2 {x2Condition} 0\n";
                text1 += "Шаг N1. Построим область допустимых решений, т.е. решим графически систему неравенств. Для этого построим каждую прямую.";

                text2 = "Шаг N2. Границы области допустимых решений.\r\nПересечением полуплоскостей будет являться область, координаты точек которого удовлетворяют условию неравенствам системы ограничений задачи.\r\nОбозначим границы области многоугольника решений.";
                model3.DrawLineGradient(x, y);
                model3.DrawLevelLines(x, y, constraints, minOrMax, x1Condition, x2Condition, out double x1, out double x2, point);
                text3 = $"Функция принимает {((minOrMax == "Min") ? "минимальное" : "максимальное")} значение в любой точке на отрезка AB\n" +
                    $"F(x) = {x} * {x1:f3} + {y:f3} * {x2:f3} = {x * x1 + y * x2:f3}";

            }
            //else if (model2.IsObjectiveFunctionUnbounded(constraints, x, y, minOrMax, x1Condition, x2Condition))
            else if (!model2.HasMultipleSolutions(constraints, x, y, minOrMax, x1Condition, x2Condition, out point))
            {
                model2.DrawRangeOfAcceptableValues(constraints, x1Condition, x2Condition);
                model3.DrawRangeOfAcceptableValues(constraints, x1Condition, x2Condition);
                model3.DrawLineGradient(x, y);
                model3.DrawLevelLines(x, y, constraints, minOrMax, x1Condition, x2Condition, out double x1, out double x2, point);

                text1 = $"Необходимо найти {((minOrMax == "Min") ? "минимальное" : "максимальное")} значение функции F = {(x == 0 ? "" : x.ToString() + "X1")} " +
                $"{(y == 0 ? "" : (y < 0 ? "- " + y.ToString() + "X2" : "+" + y.ToString() + "X2"))}" +
                $" -> {minOrMax} при системе ограничений:\n";
                for (int i = 0; i < constraints.Count; i++)
                {
                    text1 += $"{(constraints[i].Item1 == 0 ? "" : constraints[i].Item1 + "X1")} {(constraints[i].Item2 == 0 ? "" : (constraints[i].Item2 < 0 ? "- " + constraints[i].Item2 + "X2" : "+ " + constraints[i].Item2.ToString() + "X2"))} {constraints[i].Item4} {constraints[i].Item3} \t\t({i + 1})\n";
                }
                text1 += $"x1 {x1Condition} 0 \tx2 {x2Condition} 0\n";
                text1 += "Шаг N1. Построим область допустимых решений, т.е. решим графически систему неравенств. Для этого построим каждую прямую.";

                text2 = "Шаг N2. Границы области допустимых решений.\r\nПересечением полуплоскостей будет являться область, координаты точек которого удовлетворяют условию неравенствам системы ограничений задачи.\r\nОбозначим границы области многоугольника решений.";


                text3 = $"Шаг N3. Рассмотрим целевую функцию задачи F = {(x == 0 ? "" : x.ToString() + "X1")} {(y == 0 ? "" : (y < 0 ? "- " + y.ToString() + "X2" : "+" + y.ToString() + "X2"))} → {minOrMax.ToLower()}.\n" +
                    $"Вектор-градиент, составленный из коэффициентов целевой функции, указывает направление максимизации F(X). Начало вектора – точка (0; 0), конец – точка ({x};{y}).\n" +
                    $"Прямая F(x) = const пересекает область в точке A, ее координатами являются ({x1:f3};{x2:f3})" +
                    $"Откуда найдем {(minOrMax == "Min" ? "минимальное" : "максимальное")} значение целевой функции\n" +
                    $"F(x) = {x} * {x1:f3} + {y:f3} * {x2:f3} = {x * x1 + y * x2:f3}";

            }
            else
            {
                model2.DrawRangeOfAcceptableValues(constraints, x1Condition, x2Condition);
                model3.DrawRangeOfAcceptableValues(constraints, x1Condition, x2Condition);
                text1 = $"Необходимо найти {((minOrMax == "Min") ? "минимальное" : "максимальное")} значение функции F = {(x == 0 ? "" : x.ToString() + "X1")} " +
               $"{(y == 0 ? "" : (y < 0 ? "- " + y.ToString() + "X2" : "+" + y.ToString() + "X2"))}" +
               $" -> {minOrMax} при системе ограничений:\n";
                for (int i = 0; i < constraints.Count; i++)
                {
                    text1 += $"{(constraints[i].Item1 == 0 ? "" : constraints[i].Item1 + "X1")} {(constraints[i].Item2 == 0 ? "" : (constraints[i].Item2 < 0 ? "- " + constraints[i].Item2 + "X2" : "+ " + constraints[i].Item2.ToString() + "X2"))} {constraints[i].Item4} {constraints[i].Item3} \t\t({i + 1})\n";
                }
                text1 += $"x1 {x1Condition} 0 \tx2 {x2Condition} 0\n";
                text1 += "Шаг N1. Построим область допустимых решений, т.е. решим графически систему неравенств. Для этого построим каждую прямую.";

                text2 = "Шаг N2. Границы области допустимых решений.\r\nПересечением полуплоскостей будет являться область, координаты точек которого удовлетворяют условию неравенствам системы ограничений задачи.\r\nОбозначим границы области многоугольника решений.";
                text3 = $"Функция неограничена сверху";

            }




            chart.DrawModel(model1);
            plotView1.Size = new Size((int)(panel1.Width * 0.5), (int)(panel1.Height * 0.5));
            plotView1.Location = new Point((panel1.Width - plotView1.Width) / 2, (Height - label6.Height - 20 - plotView1.Height) / 2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.Columns.Clear();

            for (int i = 0; i < 2; i++)
            {
                dataGridView2.Columns.Add($"X{i + 1}", $"X{i + 1}");
            }
            dataGridView2.Columns.Add("Sign", "Sign");
            dataGridView2.Columns.Add("Num", "Num");

            if (int.TryParse(textBox1.Text, out int rowCount))
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Rows.Add(rowCount);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Location = new Point(0, 0);

            panel1.Size = new Size(Width, (int)(Height * 0.7));

            InitializeAdditionComponents();

            CreateChart();
            label6.Text = text1;
            var control = Controls.Find("0", false)[0];
            label6.Location = new Point((Width - label6.Width) / 4, control.Height + control.Location.Y + label6.Height / 2); ;
            panel1.Location = new Point((Width - panel1.Width) / 2, label6.Location.Y + label6.Height + 10);
            hideAndShowComponents(false);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;

                for (int i = 0; i < dataGridView2.ColumnCount - 2; i++)
                    row.Cells[i].Value = rnd.Next(-20, 20);
                row.Cells[dataGridView2.ColumnCount - 2].Value = rnd.Next(1, 3) == 2 ? "<=" : ">=";
                row.Cells[dataGridView2.ColumnCount - 1].Value = rnd.Next(-20, 20);
            }
        }

        private void InitializeAdditionComponents()
        {
            Control[] buttons = new Control[3];
            for (int i = 0; i < 3; i++)
            {
                Button button = new Button() { Text = $"Шаг {i + 1}", Name = i.ToString() };
                button.Size = new Size((int)(Width * 0.1), (int)(Height * 0.04));
                buttons[i] = button;
                buttons[i].Click += ReplaceModel;
                buttons[i].Visible = false;
            }

            Button ResetButton = new Button()
            {
                Size = new Size((int)(Width * 0.03), (int)(Width * 0.03)),
                Visible = false,
                Name = "ResetButton",
                BackgroundImageLayout = ImageLayout.Stretch,
                ImageAlign = ContentAlignment.MiddleCenter,
                BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"Assets\reset.png"),

            };
            ResetButton.Click += Reset;


            int spacing = 10;
            int ButtonsWidts = buttons.Sum(b => b.Width) + (buttons.Length - 1) * spacing;
            int StartPos = (Width - ButtonsWidts) / 2;

            for (int i = 0; i < buttons.Length; i++)
            {

                buttons[i].Name = i.ToString();
                buttons[i].Location = new Point(StartPos + buttons[i].Width * i + spacing * i, (int)(Height * 0.01));
            };
            ResetButton.Location = new Point(Width - ResetButton.Width - spacing * 3, spacing);


            Controls.AddRange(buttons);
            Controls.Add(ResetButton);

            for (int i = 0; i < buttons.Length; i++)
                buttons[i].BringToFront();
            ResetButton.BringToFront();

        }

        private void Reset(object? sender, EventArgs e)
        {
            Size = MinimumSize;
            hideAndShowComponents(true);

            dataGridView2.Columns.Clear();

            var X1 = Controls.Find("x1", false)[0].Text = "";
            var X2 = Controls.Find("x2", false)[0].Text = "";


        }

        private void ReplaceModel(object? sender, EventArgs e)
        {
            var control = Controls.Find("0", false)[0];
            if (sender == null) return;
            if (((Button)sender).Name == "0" && !IsM1)
            {
                chart.DrawModel(model1);
                label6.Text = text1;
                IsM1 = true;
                IsM2 = false;
                IsM3 = false;
            }
            else if (((Button)sender).Name == "1" && !IsM2)
            {
                chart.DrawModel(model2);
                label6.Text = text2;
                IsM1 = false;
                IsM2 = true;
                IsM3 = false;
            }
            else if (((Button)sender).Name == "2" && !IsM3)
            {
                chart.DrawModel(model3);
                label6.Text = text3;
                IsM1 = false;
                IsM2 = false;
                IsM3 = true;
            }
            label6.Location = new Point((Width - label6.Width) / 4, control.Height + control.Location.Y + label6.Height / 2); ;
            panel1.Location = new Point((Width - panel1.Width) / 2, label6.Location.Y + label6.Height + 10);


        }

        private void hideAndShowComponents(bool hide)
        {
            if (hide)
            {
                Controls.Find("x1", false)[0].Visible = true;
                Controls.Find("x2", false)[0].Visible = true;
                Controls.Find("x1d", false)[0].Visible = true;
                Controls.Find("x2d", false)[0].Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                dataGridView2.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                textBox1.Visible = true;
                maxOrMin.Visible = true;

                plotView1.Visible = false;
                Controls.Find("ResetButton", false)[0].Visible = false;
                label6.Visible = false;
                foreach (var control in Controls)
                {
                    try
                    {
                        var name = ((Button)control).Name;
                        if ((name == "0") || (name == "1") || (name == "2"))
                            ((Button)control).Visible = false;
                    }
                    catch (Exception ex) { }
                }
            }
            else
            {
                Controls.Find("x1", false)[0].Visible = false;
                Controls.Find("x2", false)[0].Visible = false;
                Controls.Find("x1d", false)[0].Visible = false;
                Controls.Find("x2d", false)[0].Visible = false;

                label7.Visible = false;
                label8.Visible = false;

                dataGridView2.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                textBox1.Visible = false;
                maxOrMin.Visible = false;
                label6.Visible = true;

                plotView1.Visible = true;
                Controls.Find("ResetButton", false)[0].Visible = true;
                foreach (var control in Controls)
                {
                    try
                    {
                        var name = ((Button)control).Name;
                        if ((name == "0") || (name == "1") || (name == "2"))
                            ((Button)control).Visible = true;
                    }
                    catch (Exception ex) { }
                }

            }
        }


    }
}
