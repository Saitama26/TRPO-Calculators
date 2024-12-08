using System.Data;
using System.Windows.Forms;


namespace TransportTask
{
    public partial class TransportProblemSolving : Form
    {
        Tuple<Control[,], Control[], Control[]> textBoxes { get; set; }
        Tuple<Control[,], Control[], Control[]> step2 { get; set; }
        Tuple<Control[,], Control[], Control[]> step3 { get; set; }
        Tuple<Control[,], Control[], Control[]> step4 { get; set; }
        Tuple<Control[,], Control[], Control[]> step5 { get; set; }
        bool Step1, Step2, Step3, Step4;
        int Rows, Columns;
        int ControlWidth;
        int[,] costs;
        int[] stocks, needs;
        int CountOfFilledCells;
        static bool check = true, IsOptimal = false, firstClick = true;
        int[,] allocation;
        int[,] costsControls;
        int minDeltaRow = -1, minDeltaCol = -1;

        public TransportProblemSolving()
        {
            InitializeComponent();
            Text = "Транспортная задача";
            StartPosition = FormStartPosition.CenterScreen;

            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;

            MinimumSize = new Size((int)(w / 1.5), (int)(h / 1.5));
            ControlWidth = (int)(Width * 0.05);
            Resize += Form1_Resize;

            InitializeTabControl();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            firstClick = true;
            for (int i = 0; i < 5; i++)
                DeleteBoxesFromPage(i + 1);

            Step1 = false;
            Step2 = false;
            Step3 = false;
            Step4 = false;

            textBoxes = InitializeAddComponents();
            var control = Controls.Find("tabControl", false)[0].Controls.Find("Page1", false)[0].Controls.Find("ResultOfCheck", false)[0];
            control.BackColor = BackColor;
            control.Text = "";
            Controls.Find("tabControl", false)[0].Controls.Find("Page2", false)[0].Controls.Find("Continue", false)[0].Text = "";

        }

        private void DeleteBoxesFromPage(int page)
        {
            var Boxes = Controls.Find("tabControl", false)[0].Controls.Find($"Page{page}", false)[0].Controls.Find("TransportTable", false);
            if (Boxes != null)
            {
                foreach (var box in Boxes)
                {
                    Controls.Find("tabControl", false)[0].Controls.Find($"Page{page}", false)[0].Controls.Remove(box);
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ControlWidth = (int)(Width * 0.05);
            var control = Controls.Find("tabControl", false)[0];
            control.Size = new Size((int)(Width * 0.9), (int)(Height * 0.85));
            control.Location = new Point((Width - control.Width) / 2, (Height - control.Height) / 2);
        }

        private void ResizeInnerComponents(object? sender, EventArgs e)
        {
            try
            {
                UpdateLocation(textBoxes, StartY);
                if (Step1)
                    UpdateLocation(step2, step2.Item1[0, 0].Location.Y);
                if (Step2)
                    UpdateLocation(step3, step3.Item1[0, 0].Location.Y);
                if (Step3)
                    UpdateLocation(step4, step4.Item1[0, 0].Location.Y);
                if (Step4)
                    UpdateLocation(step5, step5.Item1[0, 0].Location.Y);

            }
            catch (NullReferenceException) { }
        }

        private void FillTable(object? sender, EventArgs e)
        {
            try
            {
                Random random = new Random();

                // Заполнение массивов случайными значениями
                for (int i = 0; i < textBoxes.Item3.Length; i++)
                {
                    textBoxes.Item3[i].Text = random.Next(10, 100).ToString(); // Случайные значения для запасов (от 10 до 100)
                }

                for (int j = 0; j < textBoxes.Item2.Length; j++)
                {
                    textBoxes.Item2[j].Text = random.Next(10, 100).ToString(); // Случайные значения для потребностей (от 10 до 100)
                }

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        textBoxes.Item1[i, j].Text = random.Next(1, 50).ToString(); // Случайные значения для тарифов (от 1 до 50)
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Создай таблицу");
            }
        }

        private void CheckForBalance(object? sender, EventArgs e)
        {
            for (int i = 1; i < 5; i++)
                DeleteBoxesFromPage(i + 1);

            Step2 = false;
            Step3 = false;
            Step4 = false;


            var control = Controls.Find("tabControl", false)[0].Controls.Find("Page1", false)[0].Controls.Find("ResultOfCheck", false)[0];
            
            try
            {
            int NeedsSum = Sum(textBoxes.Item2);
            int StocksSum = Sum(textBoxes.Item3);

                if (NeedsSum == StocksSum)
                {
                    control.Location = new Point(textBoxes.Item2[0].Location.X, textBoxes.Item2[0].Location.Y + textBoxes.Item2[0].Height + (int)(Height * 0.02));
                    control.Text = "Go to the next step solving of problem";
                    control.BackColor = Color.LightGreen;

                    

                    needs = new int[textBoxes.Item2.Length];
                    for (int i = 0; i < needs.Length; i++)
                        needs[i] = int.Parse(textBoxes.Item2[i].Text);

                    stocks = new int[textBoxes.Item3.Length];
                    for (int i = 0; i < stocks.Length; i++)
                        stocks[i] = int.Parse(textBoxes.Item3[i].Text);

                    costs = new int[textBoxes.Item1.GetLength(0), textBoxes.Item1.GetLength(1)];
                    for (int i = 0; i < costs.GetLength(0); i++)
                        for (int j = 0; j < costs.GetLength(1); j++)
                            costs[i, j] = int.Parse(textBoxes.Item1[i, j].Text);

                    Step1 = true;
                    InitTwoStep();

                }
                else if (NeedsSum > StocksSum)
                {
                    AddRow(textBoxes.Item1, textBoxes.Item3, NeedsSum - StocksSum);
                    control.Location = new Point(textBoxes.Item2[0].Location.X, textBoxes.Item2[0].Location.Y + textBoxes.Item2[0].Height + (int)(Height * 0.02));
                    control.Text = "Check the balance again";

                }
                else
                {
                    AddCol(textBoxes.Item1, textBoxes.Item2, StocksSum - NeedsSum);
                    control.Location = new Point(textBoxes.Item2[0].Location.X, textBoxes.Item2[0].Location.Y + textBoxes.Item2[0].Height + (int)(Height * 0.02));
                    control.Text = "Check the balance again";
                }
            }catch(Exception)
            {
                Step2 = false;
                control.Text = "";
                control.BackColor = BackColor;
                MessageBox.Show("Заполните все поля!!!");
            }

        }

        private void InitTwoStep()
        {
            Control[] stocks2 = new Control[stocks.Length];
            Control[] needs2 = new Control[needs.Length];
            Control[,] costs2 = new Control[costs.GetLength(0), costs.GetLength(1)];

            for (int i = 0; i < stocks2.Length; i++)
            {
                var textBox = CreateTextBox(ControlWidth, Color.Yellow, stocks[i].ToString());
                textBox.ReadOnly = true;
                stocks2[i] = textBox;
                Controls.Find("tabControl", false)[0].Controls.Find($"Page2", false)[0].Controls.Add(stocks2[i]);
            }

            for (int i = 0; i < needs2.Length; i++)
            {
                var textBox = CreateTextBox(ControlWidth, Color.Yellow, needs[i].ToString());
                textBox.ReadOnly = true;
                needs2[i] = textBox;
                Controls.Find("tabControl", false)[0].Controls.Find($"Page2", false)[0].Controls.Add(needs2[i]);
            }


            for (int i = 0; i < costs2.GetLength(0); i++)
            {
                for (int j = 0; j < costs2.GetLength(1); j++)
                {
                    var textBox = CreateTextBox(ControlWidth, BackColor, "");
                    textBox.ReadOnly = true;
                    costs2[i, j] = textBox;
                    Controls.Find("tabControl", false)[0].Controls.Find($"Page2", false)[0].Controls.Add(costs2[i, j]);
                }
            }

            step2 = new(costs2, needs2, stocks2);

            UpdateLocation(step2, textBoxes.Item1[0, 0].Location.Y);

        }
        private void InitThreeStep()
        {
            Control[] stocks3 = new Control[stocks.Length];
            Control[] needs3 = new Control[needs.Length];
            Control[,] costs3 = new Control[costs.GetLength(0), costs.GetLength(1)];

            for (int i = 0; i < stocks3.Length; i++)
            {
                var textBox = CreateTextBox(ControlWidth, Color.Yellow, step2.Item3[i].Text);
                textBox.ReadOnly = true;
                stocks3[i] = textBox;
                Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Add(stocks3[i]);
            }

            for (int i = 0; i < needs3.Length; i++)
            {
                var textBox = CreateTextBox(ControlWidth, Color.Yellow, step2.Item2[i].Text);
                textBox.ReadOnly = true;
                needs3[i] = textBox;
                Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Add(needs3[i]);
            }


            for (int i = 0; i < costs3.GetLength(0); i++)
            {
                for (int j = 0; j < costs3.GetLength(1); j++)
                {
                    var textBox = CreateTextBox(ControlWidth, BackColor, step2.Item1[i, j].Text);
                    textBox.ReadOnly = true;
                    costs3[i, j] = textBox;
                    Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Add(costs3[i, j]);
                }
            }

            step3 = new(costs3, needs3, stocks3);

            UpdateLocation(step3, (int)(Height * 0.02));

            var c = Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Find("Description3", false)[0];
            c.Location = new Point
                (
                    step3.Item3[0].Location.X + step3.Item3[0].Width + (int)(Width * 0.02), step3.Item3[0].Location.Y
                );

            var c2 = Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Find("checkForDegenetacy", false)[0];
            c2.Location = new Point
                (
                c.Location.X + c.Width / 4, c.Location.Y + c.Height + (int)(Height * 0.02)
                );



        }
        private void InitFourStep()
        {
            int page = 4;
            Control[,] costs4 = CreateTransportTable(step3.Item1.GetLength(0), step3.Item1.GetLength(1), ControlWidth, page);
            Control[] stocks4 = CreateStocks(stocks.Length, ControlWidth, page);
            Control[] needs4 = CreateNeeds(needs.Length, ControlWidth, page);

            for (int i = 0; i < costs4.GetLength(0); i++)
            {
                for (int j = 0; j < costs4.GetLength(1); j++)
                {
                    if (step3.Item1[i, j].Text != "0" && step3.Item1[i, j].Text != "")
                        costs4[i, j].Text = step3.Item1[i, j].Text + "*" + costs[i, j].ToString();
                    else
                        costs4[i, j].Text = "";

                    (costs4[i, j] as TextBox).ReadOnly = true;
                }
            }

            for (int i = 0; i < stocks4.Length; i++)
                (stocks4[i] as TextBox).ReadOnly = true;

            for (int i = 0; i < needs4.Length; i++)
                (needs4[i] as TextBox).ReadOnly = true;

            step4 = new(costs4, needs4, stocks4);
            UpdateLocation(step4, step3.Item1[0, 0].Location.Y);

            var c = Controls.Find("tabControl", false)[0].Controls.Find($"Page4", false)[0].Controls.Find("Description4", false)[0];
            c.Location = new Point
            (
                step4.Item3[0].Location.X + step4.Item3[0].Width + (int)(Width * 0.02), step4.Item3[0].Location.Y
            );
            var c2 = Controls.Find("tabControl", false)[0].Controls.Find($"Page4", false)[0].Controls.Find("Calculate", false)[0];
            c2.Location = new Point
                (
                c.Location.X + c.Width / 4, c.Location.Y + c.Height + (int)(Height * 0.02)
                );


        }
        private void InitFiveStep()
        {
            int page = 5;
            Control[,] costs5 = CreateTransportTable(step3.Item1.GetLength(0), step3.Item1.GetLength(1), ControlWidth, page);
            Control[] stocks5 = CreateStocks(stocks.Length, ControlWidth, page);
            Control[] needs5 = CreateNeeds(needs.Length, ControlWidth, page);

            for (int i = 0; i < costs5.GetLength(0); i++)
                for (int j = 0; j < costs5.GetLength(1); j++)
                    (costs5[i, j] as TextBox).ReadOnly = true;

            for (int i = 0; i < stocks5.Length; i++)
                (stocks5[i] as TextBox).ReadOnly = true;

            for (int i = 0; i < needs5.Length; i++)
                (needs5[i] as TextBox).ReadOnly = true;

            step5 = new(costs5, needs5, stocks5);
            UpdateLocation(step5, step4.Item1[0, 0].Location.Y);

            var c = Controls.Find("tabControl", false)[0].Controls.Find($"Page5", false)[0].Controls.Find("Description5", false)[0];
            c.Location = new Point
            (
                step5.Item3[0].Location.X + step5.Item3[0].Width + (int)(Width * 0.02), step5.Item3[0].Location.Y
            );
            var c2 = Controls.Find("tabControl", false)[0].Controls.Find($"Page5", false)[0].Controls.Find("MakePlan", false)[0];
            c2.Location = new Point
                (
                c.Location.X + c.Width / 4, c.Location.Y + c.Height + (int)(Height * 0.02)
                );

        }


        private void SolveProblem(object? sender, EventArgs e)
        {
            Step3 = false;
            Step4 = false;

            if (Step1)
            {

                Controls.Find("tabControl", false)[0].Controls.Find($"Page4", false)[0].Controls.Find("CostFirstPlan", false)[0].Text = "";
                Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Find("Degeneracy", false)[0].Text = "";
                
                var button = Controls.Find("tabControl", false)[0].Controls.Find("Page2", false)[0].Controls.Find("FindPlan", false)[0];
                var comboBox = Controls.Find("tabControl", false)[0].Controls.Find($"Page2", false)[0].Controls.Find("NWAOrMinEl", false)[0];
                var labelContinue = Controls.Find("tabControl", false)[0].Controls.Find("Page2", false)[0].Controls.Find("Continue", false)[0];

                labelContinue.Text = "Первоначальный план найден.\n\nПереходи к следующему шагу.";
                comboBox.Location = new Point(textBoxes.Item3[0].Location.X + textBoxes.Item3[0].Width + (int)(Width * 0.02), textBoxes.Item3[0].Location.Y);
                button.Location = new Point(comboBox.Location.X + comboBox.Width + (int)(Width * 0.02), comboBox.Location.Y);
                labelContinue.Location = new Point(comboBox.Location.X, comboBox.Location.Y + comboBox.Height + (int)(Height * 0.02));

                for (int i = 0; i < step2.Item1.GetLength(0); i++)
                    for (int j = 0; j < step2.Item1.GetLength(1); j++)
                        step2.Item1[i, j].Text = ""; 

                if ((comboBox as ComboBox).SelectedItem == null)
                    (comboBox as ComboBox).SelectedItem = "Минимального элемента";

                DeleteBoxesFromPage(4);

                if ((comboBox as ComboBox)?.SelectedItem == "Минимального элемента")
                {
                    CountOfFilledCells = SolveByMinCost();
                    Step2 = true;
                }
                else
                {
                    CountOfFilledCells = SolveProblemNWA();
                    Step2 = true;
                }

                try
                {
                    var controls = Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Find("TransportTable", false);
                    foreach (var c in controls)
                        Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Remove(c);
                }
                catch (Exception ex)
                {
                }
                InitThreeStep();
            }
        }

        private int SolveProblemNWA()
        {
            Control[,] costControls = step2.Item1;
            Control[] needsControls = step2.Item2;
            Control[] stocksControls = step2.Item3;

            int rows = stocksControls.Length;
            int columns = needsControls.Length;

            int[,] allocation = new int[rows, columns];
            int[] remainingStocks = (int[])(stocks.Clone());
            int[] remainingNeeds = (int[])(needs.Clone());
            int[,] costs2 = (int[,])costs.Clone();

            int row = 0, col = 0;

            while (row < rows && col < columns)
            {
                int allocationAmount = Math.Min(remainingStocks[row], remainingNeeds[col]);
                allocation[row, col] = allocationAmount;
                remainingStocks[row] -= allocationAmount;
                remainingNeeds[col] -= allocationAmount;

                if (remainingStocks[row] == 0)
                {
                    row++;
                }
                else
                {
                    col++;
                }
            }

            // Запись результатов обратно в контролы
            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (allocation[i, j] == 0)
                        costControls[i, j].Text = "";
                    else
                    {
                        costControls[i, j].Text = allocation[i, j].ToString();
                        count++;
                    }
                }
            }

            for (int i = 0; i < rows; i++)
                step2.Item3[i].Text = remainingStocks[i].ToString();

            for (int j = 0; j < columns; j++)
                step2.Item2[j].Text = remainingNeeds[j].ToString();

            return count;
        }

        private int SolveByMinCost()
        {
            Control[,] costControls = step2.Item1;
            Control[] needsControls = step2.Item2;
            Control[] stocksControls = step2.Item3;

            int rows = stocksControls.Length;
            int columns = needsControls.Length;

            int[,] allocation = new int[rows, columns];
            int[] remainingStocks = (int[])(stocks.Clone());
            int[] remainingNeeds = (int[])(needs.Clone());
            int[,] costs2 = (int[,])costs.Clone();

            bool[,] allocated = new bool[rows, columns];

            while (true)
            {
                int minCost = int.MaxValue; int minRow = -1, minCol = -1;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (!allocated[i, j] && remainingStocks[i] > 0 && remainingNeeds[j] > 0)
                        {
                            int cost = costs2[i, j];
                            if (cost < minCost)
                            {
                                minCost = cost;
                                minRow = i;
                                minCol = j;
                            }
                        }
                    }
                }
                if (minRow == -1 || minCol == -1)
                    break;

                int allocationAmount = Math.Min(remainingStocks[minRow], remainingNeeds[minCol]);
                allocation[minRow, minCol] = allocationAmount;
                remainingStocks[minRow] -= allocationAmount;
                remainingNeeds[minCol] -= allocationAmount;
                allocated[minRow, minCol] = true;
            }

            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (allocation[i, j] > 0){
                        count++;
                        costControls[i, j].Text = allocation[i, j].ToString();
                    }
                }
            }

            for (int i = 0; i < rows; i++)
                step2.Item3[i].Text = remainingStocks[i].ToString();

            for (int j = 0; j < columns; j++)
                step2.Item2[j].Text = remainingNeeds[j].ToString();

            return count;
        }

        private void checkForDegenetacy(object? sender, EventArgs e)
        {
            if (Step2)
            {
                var label = Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Find("Degeneracy", false)[0];
                var c1 = Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Find("Description3", false)[0];
                var c2 = Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Find("checkForDegenetacy", false)[0];

                if (CountOfFilledCells == Rows + Columns - 1)
                {
                    (label as Label).Text = $"{CountOfFilledCells} = {Rows} + {Columns} - 1";
                    (label as Label).BackColor = Color.LightGreen;
                    (label as Label).ForeColor = Color.Black;
                    label.Location = new Point(c1.Location.X + label.Width / 4, c2.Location.Y + c2.Height + (int)(Height * 0.02));
                    Step3 = true;

                    try
                    {
                        var controls = Controls.Find("tabControl", false)[0].Controls.Find($"Page4", false)[0].Controls.Find("TransportTable", false);
                        foreach (var c in controls)
                            Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Remove(c);
                    }
                    catch (Exception ex) { }

                    InitFourStep();
                }
                else
                {
                    (label as Label).Text = $"{CountOfFilledCells} are not equal {Rows} + {Columns} - 1";
                    (label as Label).BackColor = Color.Red;
                    (label as Label).ForeColor = Color.White;
                }
            }
        }

        private void CalcCost(object? sender, EventArgs e)
        {
            firstClick = true;
            if (Step3)
            {
                var c = Controls.Find("tabControl", false)[0].Controls.Find($"Page4", false)[0].Controls.Find("Calculate", false)[0];
                
                var label = Controls.Find("tabControl", false)[0].Controls.Find($"Page4", false)[0].Controls.Find("CostFirstPlan", false)[0];
                int cost = 0;

                for (int i = 0; i < step4.Item1.GetLength(0); i++)
                {
                    for (int j = 0; j < step4.Item1.GetLength(1); j++)
                    {
                        if (step4.Item1[i, j].Text != "")
                        {
                            cost += Convert.ToInt32(new DataTable().Compute(step4.Item1[i, j].Text, null));
                            label.BackColor = Color.LightGreen;
                        }
                    }
                }

                label.Text = $"Total cost is {cost}";
                label.Location = new Point(c.Location.X, c.Location.Y + c.Height + (int)(Height * 0.02));

                Step4 = true;

                InitFiveStep();

            }
        }

        private async void MakePlan(Object sender, EventArgs e)
        {
            {
                if (Step4)
                {

                    if (firstClick)
                    {
                        Control[,] costControls = step3.Item1;
                        Control[] needsControls = step3.Item2;
                        Control[] stocksControls = step3.Item3;

                        Rows = stocksControls.Length;
                        Columns = needsControls.Length;
                        allocation = new int[Rows, Columns];
                        costsControls = new int[costControls.GetLength(0), costControls.GetLength(1)];

                        for (int i = 0; i < step3.Item1.GetLength(0); i++)
                        {
                            for (int j = 0; j < step3.Item1.GetLength(1); j++)
                            {
                                if (costControls[i, j].Text != "")
                                {
                                    allocation[i, j] = int.Parse(step3.Item1[i, j].Text);
                                }
                                else allocation[i, j] = 0;
                            }
                        }
                        firstClick = false;
                    }
                    for (int i = 0; i < allocation.GetLength(0); i++)
                        for (int j = 0; j < allocation.GetLength(1); j++)
                            if (allocation[i, j] > 0)
                                costsControls[i, j] = int.Parse(textBoxes.Item1[i, j].Text);



                    // Вычисляем потенциалы
                    double[] u = new double[Rows];
                        double[] v = new double[Columns];
                        CalculatePotentials(Rows, Columns, costsControls, u, v);

                        for (int i = 0; i < Rows; i++)
                            step5.Item3[i].Text = u[i].ToString();
                        for (int j = 0; j < Columns; j++)
                            step5.Item2[j].Text = v[j].ToString();

                        // Оценка небазисных переменных
                        double minDelta = double.MaxValue;

                        for (int i = 0; i < Rows; i++)
                        {
                            for (int j = 0; j < Columns; j++)
                            {
                                if (allocation[i, j] == 0)
                                {
                                    double delta = costs[i, j] - u[i] - v[j];
                                    step5.Item1[i, j].Text = delta.ToString();
                                    if (delta < minDelta)
                                    {
                                        minDelta = delta;
                                        minDeltaRow = i;
                                        minDeltaCol = j;
                                    }
                                }
                                else
                                {
                                    step5.Item1[i, j].Text = "0";
                                }
                            }
                        }


                        if (minDelta >= 0)
                            IsOptimal = true;

                    var label = Controls.Find("tabControl", false)[0].Controls.Find($"Page5", false)[0].Controls.Find("CostSecondPlan", false)[0];

                    if (IsOptimal)
                    {

                        int cost = 0;
                        for (int i = 0; i < Rows; i++)
                        {
                            for (int j = 0; j < Columns; j++)
                            {
                                step5.Item1[i, j].Text = allocation[i, j].ToString();
                                cost += allocation[i, j] * costs[i, j];
                            }
                        }


                        label.Text = $"Total cost is {cost}";
                    }
                    else
                    {
                        try
                        {
                            ImprovePlan(allocation, minDeltaRow, minDeltaCol);
                        }
                        catch (NullReferenceException)
                        {
                            MessageBox.Show("Не удалось улучшить план. :)");
                        }
                    }

                }
            };
        }

        private void CalculatePotentials(int rows, int columns, int[,] costs, double[] u, double[] v)
        {
            bool[] uDetermined = new bool[rows];
            bool[] vDetermined = new bool[columns];


            // начальное значение потенциала
            u[0] = 0;
            uDetermined[0] = true;
            bool changed;

            do
            {
                changed = false;
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++)
                    {
                        if (costs[i, j] != 0)
                        {
                            if (uDetermined[i] && !vDetermined[j])
                            {
                                v[j] = costs[i, j] - u[i];
                                vDetermined[j] = true;
                                changed = true;
                                step5.Item2[j].Text = v[j].ToString();
                            }
                            else if (!uDetermined[i] && vDetermined[j])
                            {
                                u[i] = costs[i, j] - v[j];
                                uDetermined[i] = true;
                                changed = true;
                                step5.Item3[i].Text = u[i].ToString();
                            }
                        }
                    }
                //} while (((count < (columns + rows - (rows + columns - 1) * 2)) && (tmp < columns * rows)));
            } while (changed);
        }

        private static void ImprovePlan(int[,] allocation, int startRow, int startCol)
        {
            int rows = allocation.GetLength(0);
            int columns = allocation.GetLength(1);

            // Найти цикл
            var cycle = FindCycle(allocation, startRow, startCol);

            // Перераспределить запасы и потребности вдоль цикла
            int minAllocation = int.MaxValue;
            for (int i = 1; i < cycle.Count; i += 2)
            {
                int row = cycle[i].Item1;
                int col = cycle[i].Item2;
                minAllocation = Math.Min(minAllocation, allocation[row, col]);
            }

            for (int i = 0; i < cycle.Count; i++)
            {
                int row = cycle[i].Item1;
                int col = cycle[i].Item2;
                if (i % 2 == 0)
                {
                    allocation[row, col] += minAllocation;
                }
                else
                {
                    allocation[row, col] -= minAllocation;
                }
            }
        }

        private static List<(int, int)> FindCycle(int[,] allocation, int startRow, int startCol)
        {
            int rows = allocation.GetLength(0);
            int columns = allocation.GetLength(1);

            var path = new List<(int, int)> { (startRow, startCol) };
            var visited = new HashSet<(int, int)>();
            var columnCount = new Dictionary<int, int>();
            var rowCount = new Dictionary<int, int>();
            if (!columnCount.ContainsKey(startCol)) { columnCount[startCol] = 0; }
            if (!rowCount.ContainsKey(startRow)) { rowCount[startRow] = 0; }
            columnCount[startCol]++;
            rowCount[startRow]++;

            //visited.Add((startRow, startCol));

            if (FindCycleHelper(allocation, startRow, startCol, path, visited, columnCount, rowCount))
            {
                return path;
            }

            return null;
        }

        private static bool FindCycleHelper(int[,] allocation, int row, int col, List<(int, int)> path, HashSet<(int, int)> visited, Dictionary<int, int> columnCount, Dictionary<int, int> rowCount)
        {
            int rows = allocation.GetLength(0);
            int columns = allocation.GetLength(1);

            for (int i = 0; i < rows; i++)
            {

                if (check && i != row && (allocation[i, col] != 0 || (i, col) == path[0]) && (!visited.Contains((i, col)) /*|| (i, col) == path[0]*/))
                {
                    //if (!columnCount.ContainsKey(col)) { columnCount[col] = 0; }
                    //if (!rowCount.ContainsKey(i)) { rowCount[i] = 0; }
                    //if (columnCount[col] >= 2 || (rowCount.ContainsKey(i) && rowCount[i] >= 2)) { continue; }

                    visited.Add((i, col));
                    path.Add((i, col));

                    //columnCount[col]--;
                    //rowCount[i]++;


                    check = !check;
                    if ((i, col) == path[0] || FindCycleHelper(allocation, i, col, path, visited, columnCount, rowCount))
                    {
                        return true;
                    }

                    path.RemoveAt(path.Count - 1);
                    visited.Remove((i, col));

                    //rowCount[i]--;
                    //columnCount[col]--;
                    check = !check;
                }
            }

            for (int j = 0; j < columns; j++)
            {
                if (!check && j != col && (allocation[row, j] != 0 || (row, j) == path[0]) && (!visited.Contains((row, j)) /*|| (row, j) == path[0]*/))
                {
                    //if (!rowCount.ContainsKey(row)) { rowCount[row] = 0; }
                    //if (!columnCount.ContainsKey(j)) { columnCount[j] = 0; }
                    //if (rowCount[row] >= 2 || (columnCount.ContainsKey(j) && columnCount[j] >= 2)) { continue; }

                    visited.Add((row, j));
                    path.Add((row, j));

                    //rowCount[row]++;
                    //columnCount[j]++;

                    check = !check;
                    if ((row, j) == path[0] || FindCycleHelper(allocation, row, j, path, visited, columnCount, rowCount))
                    {
                        return true;
                    }

                    path.RemoveAt(path.Count - 1);
                    visited.Remove((row, j));
                    //rowCount[row]--;
                    //columnCount[j]--;
                    check = !check;
                }
            }

            return false;
        }

        private void InitializeTabControl()
        {
            TabControl tabControl = new TabControl()
            {
                Name = "tabControl",
                Size = new Size((int)(Width * 0.95), (int)(Height * 0.95)),
            };
            for (int i = 0; i < 5; i++)
            {
                tabControl.Controls.Add(new TabPage()
                {
                    Text = $"Шаг {i + 1}",
                    Name = $"Page{i + 1}"
                }
                );
            }
            Controls.Add(tabControl);
            tabControl.Location = new Point((Width - tabControl.Width) / 2, (Height - tabControl.Height) / 2);

            Label CountShops = new Label() {
                Text = "Количество поставщиков",
                AutoSize = true,
                Font = new Font("Tahoma", 12)
            };

            Button fillButton = new Button()
            {
                Size = button1.Size,
                Location = new Point(button1.Location.X + button1.Width + 20, 0),
                Text = "Fill"
            };
            fillButton.Click += FillTable;
            fillButton.Visible = false;

            Label CountOfProvider = new Label()
            {
                Text = "Количество магазинов",
                AutoSize = true,
                Font = new Font("Tahoma", 12)
            };

            Button checkButton = new Button()
            {
                Text = "Check",
                Size = button1.Size,

            };
            checkButton.Click += CheckForBalance;

            Label resultOfCheck = new Label()
            {
                Text = "",
                Name = "ResultOfCheck",
                Font = new Font("Tahoma", 12),
                AutoSize = true,
                Size = new Size((int)(tabControl.Width * 0.9), (int)(Height - Height * 0.9)),
            };

            Button FindingInitialPlanNWA = new Button()
            {
                Name = "FindPlan",
                Size = button1.Size,
                Text = "Inizializing"
            };
            FindingInitialPlanNWA.Click += SolveProblem;

            Label NWAOrMInELText = new Label()
            {
                Text = "Нахождения первоначального опорного плана",
                Name = "textMinElORMNW",
                Font = new Font("Times New Roman", 18, FontStyle.Bold | FontStyle.Italic),
                AutoSize = true,
            };

            ComboBox NWAOrMinEl = new ComboBox()
            {
                Font = new Font("Tahoma", 12),
                Location = new Point(FindingInitialPlanNWA.Location.X + FindingInitialPlanNWA.Width + 20, 0),
                Name = "NWAOrMinEl"
            };

            Label LetsContinue = new Label()
            {
                Name = "Continue",
                Text = "",
                AutoSize = true,
                Font = new Font("Tahoma", 12, FontStyle.Italic)
            };

            Label DescriptionThreeStep = new Label()
            {
                AutoSize = true,
                Font = new Font("Tahoma", 12),
                Text = "Проверка на вырожденность\n\n\tN = m + n - 1\n\nN - кол-во заполненных клеток\nm и n - число столбцов и строк",
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "Description3",
                Location = new Point(-200, -200)
            };

            Button CheckingForDegeneracy = new Button()
            {
                Name = "checkForDegenetacy",
                AutoSize = true,
                Location = new Point(-200, -200),
                Text = "Check Non Emptiness",
            };
            CheckingForDegeneracy.Click += checkForDegenetacy;

            Label Degeneracy = new Label()
            {
                Text = "",
                Name = "Degeneracy",
                Font = new Font("Tahoma", 12),
                AutoSize = true,
            };

            Label DescriptionFourStep = new Label()
            {
                AutoSize = true,
                Name = "Description4",
                Font = new Font("Tahoma", 12, FontStyle.Bold),
                Text = "Полная стоимость доставки",
                Location = new Point(-200, -200),
            };

            Label CostFirstPlan = new Label()
            {
                Text = "",
                Name = "CostFirstPlan",
                Font = new Font("Tahoma", 12),
                AutoSize = true,
                Location = new Point(-200, -200)
            };

            Button CalculateCost = new Button()
            {
                Name = "Calculate",
                AutoSize = true,
                Font = new Font("Tahoma", 12),
                Text = "Calculate",
                Location = new Point(-200, -200),
            };
            CalculateCost.Click += CalcCost;

            Label DescriptionFiveStep = new Label()
            {
                AutoSize = true,
                Name = "Description5",
                Font = new Font("Tahoma", 12, FontStyle.Bold),
                Text = "Улучшение опорного плана",
                Location = new Point(-200, -200),
            };

            Button Start = new Button()
            {
                Name = "MakePlan",
                AutoSize = true,
                Location = new Point(-200, -200),
                Text = "Start"
            };
            Start.Click += MakePlan;

            Label CostSecondPlan = new Label()
            {
                Text = "",
                Name = "CostSecondPlan",
                Font = new Font("Tahoma", 12),
                AutoSize = true,
            };
            CostSecondPlan.Location = new Point((tabControl.Width - CostSecondPlan.Width) / 2, (int)((Height - CostSecondPlan.Height) / 1.5));

            NWAOrMinEl.Items.AddRange(new string[] { "Минимального элемента", "Северо-западного угла" });

            tabControl.Controls[0].Controls.Add(textBox1);
            tabControl.Controls[0].Controls.Add(CountShops);
            textBox1.Width = (int)(Width * 0.05);
            CountShops.Location = new Point((int)(Width * 0.02), (int)(Height * 0.02));
            textBox1.Location = new Point(CountShops.Location.X + CountShops.Width + (int)(Width * 0.02), CountShops.Location.Y);

            tabControl.Controls[0].Controls.Add(textBox2);
            tabControl.Controls[0].Controls.Add(CountOfProvider);
            textBox2.Width = textBox1.Width;
            CountOfProvider.Location = new Point(CountShops.Location.X, CountShops.Location.Y + CountShops.Height + (int)(Height * 0.02));
            textBox2.Location = new Point(textBox1.Location.X, CountOfProvider.Location.Y);


            tabControl.Controls[0].Controls.Add(button1);
            button1.Location = new Point(textBox2.Location.X + textBox2.Width + (int)(Width * 0.02), (textBox2.Location.Y - button1.Height) / 2);

            tabControl.Controls[0].Controls.Add(fillButton);
            tabControl.Controls[0].Controls.Add(checkButton);
            checkButton.Location = new Point(button1.Location.X + button1.Width + (int)(Width * 0.02), button1.Location.Y);

            tabControl.Controls[0].Controls.Add(resultOfCheck);



            tabControl.Controls[1].Controls.Add(NWAOrMInELText);
            tabControl.Controls[1].Controls.Add(NWAOrMinEl);
            NWAOrMInELText.Location =  new Point((tabControl.Width - NWAOrMInELText.Width) / 2, CountShops.Location.Y) /*CountShops.Location*/;
            NWAOrMinEl.Location = new Point(-200, -200);

            tabControl.Controls[1].Controls.Add(FindingInitialPlanNWA);
            FindingInitialPlanNWA.Location = new Point(-200, -200);
            tabControl.Controls[1].Controls.Add(LetsContinue);
            LetsContinue.Location = new Point(-200, -200);



            tabControl.Controls[2].Controls.Add(CheckingForDegeneracy);
            tabControl.Controls[2].Controls.Add(DescriptionThreeStep);
            tabControl.Controls[2].Controls.Add(Degeneracy);



            tabControl.Controls[3].Controls.Add(DescriptionFourStep);
            tabControl.Controls[3].Controls.Add(CostFirstPlan);
            tabControl.Controls[3].Controls.Add(CalculateCost);


            tabControl.Controls[4].Controls.Add(DescriptionFiveStep);
            tabControl.Controls[4].Controls.Add(Start);
            tabControl.Controls[4].Controls.Add(CostSecondPlan);

            tabControl.Size = new Size((int)(Width * 0.9), (int)(Height * 0.9));
            tabControl.Location = new Point((Width - tabControl.Width) / 2, (Height - textBox1.Height - tabControl.Height) / 2);
            tabControl.Resize += ResizeInnerComponents;

        }

        private void InitializeComponent()
        {
            SuspendLayout();

            textBox1 = new TextBox()
            {
                Size = new Size(100, 30),
            };

            textBox2 = new TextBox()
            {
                Name = "ShopsCount",
                Size = new Size(100, 30),
            };


            button1 = new Button()
            {
                Text = "Создать",
                AutoSize = true,
                Font = new Font("Tahoma", 12),
                Location = new Point(textBox2.Location.X + textBox2.Width + 20, 0)
            };
            button1.Click += button1_Click;

            Controls.Add(textBox1);
            Controls.Add(textBox2);
            Controls.Add(button1);


            ResumeLayout(false);
        }
    }
}
