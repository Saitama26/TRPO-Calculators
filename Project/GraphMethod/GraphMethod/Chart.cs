using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Data;

namespace GraphMethod
{
    class Chart
    {
        private PlotView plotView;
        private ModelForPlotViev plotModel;

        public Chart() { }
        public Chart(PlotView PlotView) => this.plotView = PlotView;

        public ModelForPlotViev CreateModel(string ModelsTitle) =>  this.plotModel = new ModelForPlotViev() { Title = ModelsTitle };

        public void DrawModel(ModelForPlotViev model) => plotView.Model = model.plotModel;

    }

    class ModelForPlotViev : PlotModel
    {
        public PlotModel plotModel { get; }
        int XMin, XMax, yMin, yMax;
        public ModelForPlotViev() { plotModel = new PlotModel(); }

        public void addAxisX(int Minimum = -30, int Maximum = 30, AxisPosition Position = AxisPosition.Bottom, bool PositionAtZeroCrossing = true, bool IsZoomEmable = true, bool IsPanEnabled = true)
        {
            XMin = Minimum;
            XMax = Maximum;

            plotModel.Axes.Add(new LinearAxis { Position = Position, Minimum = Minimum, Maximum = Maximum, PositionAtZeroCrossing = PositionAtZeroCrossing, IsZoomEnabled = IsZoomEmable, IsPanEnabled = IsPanEnabled});

            var xAxisLine = new LineSeries { Color = OxyColors.Black, StrokeThickness = 2 };
            xAxisLine.Points.Add(new OxyPlot.DataPoint(Minimum, 0));
            xAxisLine.Points.Add(new OxyPlot.DataPoint(Maximum, 0));
            plotModel.Series.Add(xAxisLine);
        }
        
        public void addAxisY(int Minimum = -30, int Maximum = 30, AxisPosition Position = AxisPosition.Left, bool PositionAtZeroCrossing = true, bool IsZoomEmable = true, bool IsPanEnabled = true) 
        {
            yMin = Minimum;
            yMax = Maximum;

            plotModel.Axes.Add(new LinearAxis { Position = Position, Minimum = Minimum, Maximum = Maximum, PositionAtZeroCrossing = PositionAtZeroCrossing, IsZoomEnabled = IsZoomEmable, IsPanEnabled = IsPanEnabled});
            var yAxisLine = new LineSeries { Color = OxyColors.Black, StrokeThickness = 2 };
            yAxisLine.Points.Add(new OxyPlot.DataPoint(0, Minimum));
            yAxisLine.Points.Add(new OxyPlot.DataPoint(0, Maximum));
            plotModel.Series.Add(yAxisLine);
        
        
        }

        public void DrawLines(List<Tuple<float, float, float, string>> constraints)
        {
            foreach (var constraint in constraints)
            {
                Random rnd = new Random();
                OxyColor color = OxyColor.FromArgb(255, (byte)rnd.Next(1, 255), (byte)rnd.Next(1, 255), (byte)rnd.Next(1, 255));

                LineSeries series = new LineSeries
                {
                    Color = color,
                };

                float x1 = constraint.Item1;
                float x2 = constraint.Item2;
                float num = constraint.Item3;
                string sign = constraint.Item4;

                DataPoint startPoint, endPoint;



                if (x1 == 0)
                {
                    startPoint = new DataPoint(XMin, num / x2);
                    endPoint = new DataPoint(XMax, num / x2);
                }
                else if (x2 == 0)
                {
                    startPoint = new DataPoint(num / x1, yMin);
                    endPoint = new DataPoint(num / x1, yMax);
                }
                else
                {
                    double yAtXMin = (num - x1 * XMin) / x2;
                    double yAtXMax = (num - x1 * XMax) / x2;

                    double xAtYMin = (num - x2 * yMin) / x1;
                    double xAtYMax = (num - x2 * yMax) / x1;

                    List<DataPoint> points = new List<DataPoint>();

                    if (yAtXMin >= yMin && yAtXMin <= yMax)
                        points.Add(new DataPoint(XMin, yAtXMin));
                    if (yAtXMax >= yMin && yAtXMax <= yMax)
                        points.Add(new DataPoint(XMax, yAtXMax));
                    if (xAtYMin >= XMin && xAtYMin <= XMax)
                        points.Add(new DataPoint(xAtYMin, yMin));
                    if (xAtYMax >= XMin && xAtYMax <= XMax)
                        points.Add(new DataPoint(xAtYMax, yMax));

                    if (points.Count >= 2)
                    {
                        startPoint = points[0];
                        endPoint = points[^1];
                    }
                    else
                    {
                        continue; // Если точки не найдены, пропускаем ограничение
                    }
                }

                series.Points.Add(startPoint);
                series.Points.Add(endPoint);

                //AddPerpendicularTicks(series, startPoint, endPoint, x1, x2, num, sign);

                // Добавление подписи к линии
                string equation = $"{x1}x1 + {x2}x2 {sign} {num}";
                var textAnnotation = new TextAnnotation
                {
                    Text = equation,
                    TextColor = color,
                    TextPosition = startPoint,
                    FontSize = 12,
                    TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center,
                    TextVerticalAlignment = VerticalAlignment.Top,
                    StrokeThickness = 0
                };
                plotModel.Annotations.Add(textAnnotation);

                plotModel.Series.Add(series);
            }
        }

        public void DrawLineGradient(double x, double y)
        {
            var lineGradient = new LineSeries()
            {
                LineStyle = LineStyle.Solid,
                Color = OxyColors.Black,
                StrokeThickness = 2
            };

            lineGradient.Points.Add(new DataPoint(0, 0));
            lineGradient.Points.Add(new DataPoint(x, y));

            plotModel.Series.Add(lineGradient);

            // Добавление треугольника для указания направления
            var arrowHead = new PolygonAnnotation()
            {
                Fill = OxyColors.Black,
                Stroke = OxyColors.Black,
                StrokeThickness = 0.1
            };

            double arrowSize = 0.5; // Размер стрелки
            double angle = Math.Atan2(y, x);
            double arrowX1 = x - arrowSize * Math.Cos(angle - Math.PI / 6);
            double arrowY1 = y - arrowSize * Math.Sin(angle - Math.PI / 6);
            double arrowX2 = x - arrowSize * Math.Cos(angle + Math.PI / 6);
            double arrowY2 = y - arrowSize * Math.Sin(angle + Math.PI / 6);

            arrowHead.Points.Add(new DataPoint(x, y));
            arrowHead.Points.Add(new DataPoint(arrowX1, arrowY1));
            arrowHead.Points.Add(new DataPoint(arrowX2, arrowY2));
            arrowHead.Points.Add(new DataPoint(x, y)); // Закрываем треугольник

            plotModel.Annotations.Add(arrowHead);
        }

        public void DrawRangeOfAcceptableValues(List<Tuple<float, float, float, string>>? constraints, string condition1, string condition2, bool DrawPoints = false)
        {
            List<Tuple<float, float>> intersections = FindIntersections(constraints ?? throw new ArgumentNullException());

            foreach (var constraint in constraints)
            {
                var points = FindIntersectionWithAxes(constraint.Item1, constraint.Item2, constraint.Item3, constraint.Item4);
                intersections.AddRange(points);

            }
            intersections.Add(new Tuple<float, float>(0, 0));
            //foreach (var intersection in intersections) { ScatterSeries scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerStrokeThickness = 2 }; scatterSeries.Points.Add(new ScatterPoint(intersection.Item1, intersection.Item2)); model.Series.Add(scatterSeries); }

            var filteredIntersections = FilteringIntersection(constraints, intersections, condition1, condition2);
            //FillIntersectionArea(model, filteredIntersections);
            if (filteredIntersections.Count > 2)
            {
                if (DrawPoints)
                    foreach (var intersection in filteredIntersections)
                    {
                        ScatterSeries scatterSeries = new ScatterSeries
                        {
                            MarkerType = MarkerType.Circle,
                            MarkerStrokeThickness = 1
                        };
                        scatterSeries.Points.Add(new ScatterPoint(intersection.Item1, intersection.Item2));
                        plotModel.Series.Add(scatterSeries);
                    }

                var center = new DataPoint(filteredIntersections.Average(p => p.Item1), filteredIntersections.Average(p => p.Item2));
                var sortedIntersections = filteredIntersections.OrderBy(p => Math.Atan2(p.Item2 - center.Y, p.Item1 - center.X)).ToList();

                var polygon = new PolygonAnnotation
                {
                    Fill = OxyColor.FromAColor(50, OxyColors.Green),
                    StrokeThickness = 0
                };

                foreach (var intersection in sortedIntersections)
                    polygon.Points.Add(new DataPoint(intersection.Item1, intersection.Item2));

                if (sortedIntersections.Count > 0)
                    polygon.Points.Add(new DataPoint(sortedIntersections[0].Item1, sortedIntersections[0].Item2));

                plotModel.Annotations.Add(polygon);
            }
        }

        public void DrawLevelLines(double gradientX, double gradientY, List<Tuple<float, float, float, string>> constraints, string MaxOrMin, string condition1, string condition2, out double x1, out double x2, (float, float)point)
        {
            x1 = 0; x2 = 0;

            List<Tuple<float, float>> intersections = FindIntersections(constraints);

            foreach (var constraint in constraints)
            {
                var AddPoints = FindIntersectionWithAxes(constraint.Item1, constraint.Item2, constraint.Item3, constraint.Item4);
                intersections.AddRange(AddPoints);

            }
            intersections.Add(new Tuple<float, float>(0, 0));

            var filteredIntersections = FilteringIntersection(constraints, intersections, condition1, condition2);

            FindFirstAndLastIntersection(filteredIntersections, gradientX, gradientY, out var firstPoint, out var lastPoint);
            var points = new List<DataPoint>() 
            {
                new DataPoint(0,0), 
                (MaxOrMin == "Max") ? 
                    new DataPoint(lastPoint.Item1, lastPoint.Item2) :
                    new DataPoint(firstPoint.Item1, firstPoint.Item2),
                new DataPoint(gradientX, gradientY),
            };

            for (int i = 0; i < points.Count; i++)
            {
                LineSeries levelLine = new LineSeries
                {
                    LineStyle = LineStyle.LongDash,
                    StrokeThickness = 2,
                    Color = OxyColors.Black
                };

                double normalX = -gradientY;
                double normalY = gradientX;

                double offsetX = points[i].X + normalX;
                double offsetY = points[i].Y + normalY;

                levelLine.Points.Add(new DataPoint(offsetX - 5 * normalX, offsetY - 5 * normalY));
                levelLine.Points.Add(new DataPoint(offsetX + 5 * normalX, offsetY + 5 * normalY));

                if (i == 1)
                {
                    x1 = points[i].X;
                    x2 = points[i].Y;

                    var textAnnotation = new TextAnnotation
                    {
                        Text = "A",
                        TextColor = OxyColors.Purple,
                        TextPosition = new DataPoint(points[i].X, points[i].Y),
                        FontSize = 12,
                        TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center,
                        TextVerticalAlignment = VerticalAlignment.Top,
                        StrokeThickness = 0
                    };
                    plotModel.Annotations.Add(textAnnotation);
                } 

                if(point.Item1 != 0 || point.Item2 != 0)
                {
                    var textAnnotation = new TextAnnotation
                    {
                        Text = "B",
                        TextColor = OxyColors.Purple,
                        TextPosition = new DataPoint(point.Item1, point.Item2),
                        FontSize = 12,
                        TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center,
                        TextVerticalAlignment = VerticalAlignment.Top,
                        StrokeThickness = 0
                    };
                    plotModel.Annotations.Add(textAnnotation);
                }


                plotModel.Series.Add(levelLine);
            }

        }

        public bool IsSystemOfConstraintsConsistent(List<Tuple<float, float, float, string>> constraints, string condition1, string condition2)
        {
            // Найти все пересечения линий ограничений
            List<Tuple<float, float>> intersections = FindIntersections(constraints);

            foreach (var constraint in constraints)
            {
                var addPoints = FindIntersectionWithAxes(constraint.Item1, constraint.Item2, constraint.Item3, constraint.Item4);
                intersections.AddRange(addPoints);
            }

            // Проверить, удовлетворяет ли хотя бы одна точка всем ограничениям
            int count = 0;
            foreach (var point in intersections)
            {
                if ((condition1 == ">=" ? point.Item1 >= 0 : point.Item1 <= 0) &&
                        (condition2 == ">=" ? point.Item2 >= 0 : point.Item2 <= 0) && SatisfiesConstraints(point, constraints))
                {
                    count++; // Найдена хотя бы одна точка, удовлетворяющая всем ограничениям
                }
            }

            if (count >= 2)
                return true;

            return false; // Не найдено ни одной точки, удовлетворяющей всем ограничениям
        }

        public bool HasMultipleSolutions(List<Tuple<float, float, float, string>> constraints, double gradientX, double gradientY, string MaxOrMin, string condition1, string condition2, out (float, float) point)
        {
            List<Tuple<float, float>> intersections = FindIntersections(constraints);

            foreach (var constraint in constraints)
            {
                var addPoints = FindIntersectionWithAxes(constraint.Item1, constraint.Item2, constraint.Item3, constraint.Item4);
                intersections.AddRange(addPoints);
            }
            intersections.Add(new Tuple<float, float>(0, 0));

            var filteredIntersections = FilteringIntersection(constraints, intersections, condition1, condition2);

            FindFirstAndLastIntersection(filteredIntersections, gradientX, gradientY, out var firstPoint, out var lastPoint);

            var targetPoint = (MaxOrMin == "Max") ? lastPoint : firstPoint;

            double levelSlope = -gradientX / gradientY;
            foreach (var constraint in constraints) 
            { 
                if (IsPointOnConstraintLine(constraint, targetPoint)) 
                { double constraintSlope; 
                    if (constraint.Item2 != 0) 
                    { 
                        constraintSlope = -constraint.Item1 / constraint.Item2; 
                    } else 
                    { 
                        constraintSlope = double.PositiveInfinity; 
                    } 
                    if (Math.Abs(levelSlope - constraintSlope) < 1e-6) 
                    {
                        foreach (var pointInSet in filteredIntersections) 
                        { 
                            if (pointInSet != targetPoint && IsPointOnConstraintLine(constraint, pointInSet)) 
                            {
                                point = (pointInSet.Item1, pointInSet.Item2); 
                                return true; 
                            }
                        }
                    }
                }
            }

            point = (0, 0);
            return false;
        }
        
        private bool IsPointOnConstraintLine(Tuple<float, float, float, string> constraint, Tuple<float, float> point)
        {
            // Проверка, лежит ли точка на линии ограничения
            double leftSide = constraint.Item1 * point.Item1 + constraint.Item2 * point.Item2;
            double rightSide = constraint.Item3;

            return Math.Abs(leftSide - rightSide) < 1e-6;
        }
        
        private List<Tuple<float, float>> FilteringIntersection(List<Tuple<float, float, float, string>> constraints, List<Tuple<float, float>> intersections, string condition1, string condition2) =>
            intersections.Where
                (
                    pt =>
                        (condition1 == ">=" ? pt.Item1 >= 0 : pt.Item1 <= 0) &&
                        (condition2 == ">=" ? pt.Item2 >= 0 : pt.Item2 <= 0) &&
                        SatisfiesConstraints(pt, constraints)
                ).ToList();

        private void FindFirstAndLastIntersection(List<Tuple<float, float>> intersections, double gradientX, double gradientY, out Tuple<float, float> firstIntersection, out Tuple<float, float> lastIntersection)
        {

            try
            {
                // Сортируем точки пересечения по проекции на направление градиента
                intersections.Sort((a, b) =>
                {
                    double projectionA = a.Item1 * gradientX + a.Item2 * gradientY;
                    double projectionB = b.Item1 * gradientX + b.Item2 * gradientY;

                    return projectionA.CompareTo(projectionB);
                }
                );

                // Первая и последняя точки в отсортированном списке
                firstIntersection = intersections.First();
                lastIntersection = intersections.Last();
            }
            catch (Exception ex)
            {
                firstIntersection = new Tuple<float, float>(0f, 0f);
                lastIntersection = new Tuple<float, float>(0f, 0f);
            }
        }

        private List<Tuple<float, float>> FindIntersections(List<Tuple<float, float, float, string>> constraints)
        {
            var intersections = new List<Tuple<float, float>>(); for (int i = 0; i < constraints.Count; i++)
            {
                for (int j = i + 1; j < constraints.Count; j++)
                {
                    try
                    {
                        var intersection = SolveEquations(constraints[i].Item1, constraints[i].Item2, constraints[i].Item3, constraints[j].Item1, constraints[j].Item2, constraints[j].Item3);

                        if (intersection.Item1 >= -1 && intersection.Item2 >= -1)
                            intersections.Add(intersection);
                    }
                    catch (InvalidOperationException)
                    {

                    }
                }
            }
            return intersections;
        }

        private Tuple<float, float> SolveEquations(float a1, float b1, float c1, float a2, float b2, float c2)
        {
            float determinant = a1 * b2 - a2 * b1;

            if (determinant == 0)
            {
                throw new InvalidOperationException("Прямые параллельны и не пересекаются.");
            }

            float x = (c1 * b2 - c2 * b1) / determinant;
            float y = (a1 * c2 - a2 * c1) / determinant;

            return new Tuple<float, float>(x, y);
        }

        private List<Tuple<float, float>> FindIntersectionWithAxes(float x1, float x2, float num, string sign)
        {
            var points = new List<Tuple<float, float>>();

            // Проверка пересечения с осью X (Y = 0)
            if (x1 != 0)
            {
                float x = num / x1;
                if (x >= 0)
                {
                    bool isSatisfied = (sign == "<=" && x <= 20) || (sign == ">=" && x >= 0);
                    if (isSatisfied)
                    {
                        points.Add(new Tuple<float, float>(x, 0));
                    }
                }
            }

            // Проверка пересечения с осью Y (X = 0)
            if (x2 != 0)
            {
                float y = num / x2;
                if (y >= 0)
                {
                    bool isSatisfied = (sign == "<=" && y <= 10) || (sign == ">=" && y >= 0);
                    if (isSatisfied)
                    {
                        points.Add(new Tuple<float, float>(0, y));
                    }
                }
            }

            return points;
        }
        
        private bool SatisfiesConstraints(Tuple<float, float> point, List<Tuple<float, float, float, string>> constraints)
        {
            float x = point.Item1;
            float y = point.Item2;

            foreach (var constraint in constraints)
            {
                float a1 = constraint.Item1;
                float a2 = constraint.Item2;
                float b = constraint.Item3;
                string sign = constraint.Item4;

                if (!IsPointInRegion(x, y, a1, a2, b, sign))
                    return false;
            }

            return true;
        }

        private bool IsPointInRegion(float x, float y, float a1, float a2, float b, string sign, float epsilon = 2e-6f)
        {
            float lhs = a1 * x + a2 * y;

            if (sign == "<=")
            {
                return (lhs <= b + epsilon);
            }
            else if (sign == ">=")
            {
                return (lhs >= b - epsilon);
            }

            return false; // На случай, если знак неравенства не "<=" и не ">="
        }

    }


}
