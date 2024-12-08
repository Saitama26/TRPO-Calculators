
using System.Windows.Forms;

namespace TransportTask
{
    public partial class TransportProblemSolving : Form
    {
        private int StartY;

        private Tuple<Control[,], Control[], Control[]> InitializeAddComponents()
        {
            int.TryParse(textBox1.Text, out int rows);
            int.TryParse(textBox2.Text, out int columns);

            if (rows > 10 || rows < 2) rows = 2;
            if (columns > 10 || columns < 2) columns = 2;

            Rows = rows;
            Columns = columns;

            Control[,] TextBoxes = CreateTransportTable(rows, columns, ControlWidth, 1);
            Control[] stocks = CreateStocks(rows, ControlWidth, 1);
            Control[] needs = CreateNeeds(columns, ControlWidth, 1);


            int ControlHeight = TextBoxes[0, 0].Height;
            int xSpacing = 1;
            int ySpacing = 1;
            int widthTextBoxes = Columns * ControlWidth + Columns;
            int heightTextBoxes = Rows * ControlHeight + Rows;

            var c = Controls.Find("tabControl", false)[0].Controls.Find("Page1", false)[0].Controls.Find("ShopsCount", false)[0];
            int startY = c.Location.Y + c.Height + (int)(Height * 0.03);
            StartY = startY;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    TextBoxes[i, j].Location = new Point((Width - widthTextBoxes) / 20 + ControlWidth * j + xSpacing * j,
                                                          startY + ControlHeight * i + ySpacing * i);
                }
            }


            int StartXNeeds = TextBoxes[0, 0].Location.X;
            int StartYNeeds = TextBoxes[Rows - 1, 0].Location.Y + ControlHeight + ySpacing * 5;
            for (int i = 0; i < columns; i++)
                needs[i].Location = new Point(StartXNeeds + ControlWidth * i + xSpacing * i,
                                              StartYNeeds);


            int StartXStocks = TextBoxes[0, Columns - 1].Location.X + ControlWidth + xSpacing * 5;
            int StartYStocks = TextBoxes[0, 0].Location.Y;
            for (int i = 0; i < rows; i++)
                stocks[i].Location = new Point(StartXStocks,
                                                StartYStocks + ControlHeight * i + ySpacing * i);

            return new(TextBoxes, needs, stocks);
        }

        private Control[] CreateStocks(int rows, int width, int page)
        {
            Control[] stocks = new Control[rows];

            for (int i = 0; i < rows; i++)
            {

                TextBox textBox = CreateTextBox(width, Color.Yellow);
                stocks[i] = textBox;
                Controls.Find("tabControl", false)[0].Controls.Find($"Page{page}", false)[0].Controls.Add(textBox);
            }
            return stocks;
        }

        private Control[] CreateNeeds(int col, int width, int page)
        {
            Control[] needs = new Control[col];

            for (int i = 0; i < col; i++)
            {

                TextBox textBox = CreateTextBox(width, Color.Yellow);
                needs[i] = textBox;
                Controls.Find("tabControl", false)[0].Controls.Find($"Page{page}", false)[0].Controls.Add(textBox);
            }
            return needs;
        }

        private Control[,] CreateTransportTable(int rows, int cols, int width, int page, string Text = "")
        {
            Control[,] TextBoxes = new Control[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    TextBox textBox = CreateTextBox(width, BackColor, Text);
                    TextBoxes[i, j] = textBox;
                    Controls.Find("tabControl", false)[0].Controls.Find($"Page{page}", false)[0].Controls.Add(textBox);

                }
            }
            return TextBoxes;

        }

        private void AddRow(Control[,] costs, Control[] stocks, int newStockValue)
        {
            if (textBoxes.Item1.GetLength(0) < 10)
            {
                Control[,] newCosts = new Control[costs.GetLength(0) + 1, costs.GetLength(1)];
                Control[] newStocks = new Control[stocks.Length + 1];

                for (int i = 0; i < costs.GetLength(0); i++)
                {
                    for (int j = 0; j < costs.GetLength(1); j++)
                    {
                        newCosts[i, j] = costs[i, j];
                    }
                }


                for (int i = 0; i < costs.GetLength(1); i++)
                {
                    var textBox = CreateTextBox(ControlWidth, BackColor, "0");
                    newCosts[costs.GetLength(0), i] = textBox;
                    Controls.Find("tabControl", false)[0].Controls.Find("Page1", false)[0].Controls.Add(textBox);
                }
                Rows++;


                for (int i = 0; i < stocks.Length; i++)
                    newStocks[i] = stocks[i];


                var textbox = CreateTextBox(ControlWidth, Color.Yellow, newStockValue.ToString());
                newStocks[newStocks.Length - 1] = textbox;
                Controls.Find("tabControl", false)[0].Controls.Find("Page1", false)[0].Controls.Add(textbox);

                textBoxes = new(newCosts, textBoxes.Item2, newStocks);
                UpdateLocation(textBoxes, StartY);
            }
        }

        private void AddCol(Control[,] costs, Control[] Needs, int newNeedValue)
        {
            if (textBoxes.Item1.GetLength(1) < 10)
            {

                Control[,] newCosts = new Control[costs.GetLength(0), costs.GetLength(1) + 1];
                Control[] newNeeds = new Control[Needs.Length + 1];

                for (int i = 0; i < costs.GetLength(0); i++)
                {
                    for (int j = 0; j < costs.GetLength(1); j++)
                    {
                        newCosts[i, j] = costs[i, j];
                    }
                }


                for (int i = 0; i < costs.GetLength(0); i++)
                {
                    var textBox = CreateTextBox(ControlWidth, BackColor, "0");
                    newCosts[i, costs.GetLength(1)] = textBox;
                    Controls.Find("tabControl", false)[0].Controls.Find("Page1", false)[0].Controls.Add(textBox);
                }
                Columns++;

                for (int i = 0; i < Needs.Length; i++)
                    newNeeds[i] = Needs[i];

                var textbox = CreateTextBox(ControlWidth, Color.Yellow, newNeedValue.ToString());
                newNeeds[newNeeds.Length - 1] = textbox;
                Controls.Find("tabControl", false)[0].Controls.Find("Page1", false)[0].Controls.Add(textbox);


                textBoxes = new(newCosts, newNeeds, textBoxes.Item3);
                UpdateLocation(textBoxes, StartY);
            }
        }

        private TextBox CreateTextBox(int width, Color BackColor, string Text = "") {
            TextBox textBox = new TextBox()
            {
                Text = Text,
                Name = $"TransportTable",
            };
            textBox.Size = new Size(width, (textBox.Width));
            textBox.Font = new Font("Tahome", 12);
            textBox.Height = textBox.Width;
            textBox.BackColor = BackColor;

            return textBox;
        }

        private void UpdateLocation(Tuple<Control[,], Control[], Control[]> textBoxes, int StartY)
        {
            
            var combobox = Controls.Find("tabControl", false)[0].Controls.Find("Page2", false)[0].Controls.Find("NWAOrMinEl", false)[0];
            var button = Controls.Find("tabControl", false)[0].Controls.Find("Page2", false)[0].Controls.Find("FindPlan", false)[0];
            var label = Controls.Find("tabControl", false)[0].Controls.Find("Page2", false)[0].Controls.Find("textMinElORMNW", false)[0];

            if (textBoxes != null)
            {

                int ControlHeight = textBoxes.Item1[0, 0].Height;
                int xSpacing = 1;
                int ySpacing = 1;
                int widthTextBoxes = Columns * ControlWidth + Columns;
                int heightTextBoxes = Rows * ControlHeight + Rows;
                for (int i = 0; i < textBoxes.Item1.GetLength(0); i++)
                {
                    for (int j = 0; j < textBoxes.Item1.GetLength(1); j++)
                    {
                        textBoxes.Item1[i, j].Location = new Point((Width - widthTextBoxes) / 20 + ControlWidth * j + xSpacing * j,
                                                              StartY + ControlHeight * i + ySpacing * i);
                        textBoxes.Item1[i, j].Size = new Size(ControlWidth, ControlWidth);
                    }
                }


                int StartXNeeds = textBoxes.Item1[0, 0].Location.X;
                int StartYNeeds = textBoxes.Item1[Rows - 1, 0].Location.Y + ControlHeight + ySpacing * 5;
                for (int i = 0; i < Columns; i++)
                {
                    textBoxes.Item2[i].Location = new Point(StartXNeeds + ControlWidth * i + xSpacing * i,
                                                  StartYNeeds);
                    textBoxes.Item2[i].Size = new Size(ControlWidth, ControlWidth);
                }


                int StartXStocks = textBoxes.Item1[0, Columns - 1].Location.X + ControlWidth + xSpacing * 5;
                int StartYStocks = textBoxes.Item1[0, 0].Location.Y;
                for (int i = 0; i < Rows; i++)
                {
                    textBoxes.Item3[i].Location = new Point(StartXStocks,
                                                    StartYStocks + ControlHeight * i + ySpacing * i);
                    textBoxes.Item3[i].Size = new Size(ControlWidth, ControlWidth);
                }

                if (!Step2)
                {
                    combobox.Location = new Point(textBoxes.Item3[0].Location.X + textBoxes.Item3[0].Width + (int)(Width * 0.02), textBoxes.Item3[0].Location.Y);
                    button.Location = new Point(combobox.Location.X + combobox.Width + (int)(Width * 0.02), combobox.Location.Y);
                    Controls.Find("tabControl", false)[0].Controls.Find("Page2", false)[0].Controls.Find("Continue", false)[0].Location = new Point(combobox.Location.X, combobox.Location.Y + combobox.Height + (int)(Height * 0.02)); ;
                }
                if (Step2){

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
                    var labelD = Controls.Find("tabControl", false)[0].Controls.Find($"Page3", false)[0].Controls.Find("Degeneracy", false)[0];
                    labelD.Location = new Point(c2.Location.X, c2.Location.Y + c2.Height + (int)(Height * 0.02));
                }

                if (Step3)
                {
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
                    var labelD = Controls.Find("tabControl", false)[0].Controls.Find($"Page4", false)[0].Controls.Find("CostFirstPlan", false)[0];
                    labelD.Location = new Point(c2.Location.X, c2.Location.Y + c2.Height + (int)(Height * 0.02));

                }
                if (Step4)
                {
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
                    var labelD = Controls.Find("tabControl", false)[0].Controls.Find($"Page5", false)[0].Controls.Find("CostSecondPlan", false)[0];
                    labelD.Location = new Point(c2.Location.X, c2.Location.Y + c2.Height + (int)(Height * 0.02));



                }

            }
        }
        private int Sum(Control[] arr)
        {
            int sum = 0;
            foreach (Control c in arr)
                sum += int.Parse(c.Text);
            return sum;
        }





        //Step 2
    
    
        



    
    }
}
