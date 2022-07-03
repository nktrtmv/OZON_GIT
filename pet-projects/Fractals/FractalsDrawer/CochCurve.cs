using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;


namespace FractalsDrawer
{
    /// <summary>
    /// Класс с логикой построения кривой Коха.
    /// </summary>
    internal class CochCurve : Fractal
    {
        /// <summary>
        /// Максимальная глубина рекурсии для кривой Коха.
        /// </summary>
        public override int MaxRecursionDepth => 6;
        
        /// <summary>
        /// Метод для прорисовки фрактала с базовой логикой и обработкой исключений. (В данном случае - кривой Коха).
        /// </summary>
        /// <param name="canvas">Канвас на котором рисуется фрактал.</param>
        /// <param name="x">Точка по оси OX (используется для отчета откуда строить фрактал).</param>
        /// <param name="y">Точка по оси OY (используется для отчета откуда строить фрактал).</param>
        /// <param name="len">Длина прямой.</param>
        /// <param name="angle">Вспомогатенльная величина угла в градусах для построения фрактального дерева.</param>
        /// <param name="recursionLevel">Уровень рекурсии (номер итерации в рекурсии).</param>
        public override void DrawFractal(Canvas canvas, double x, double y, double len, int angle, int recursionLevel)
        {
            try
            {
                // Линия - первая итерация.
                var line = new Line 
                {
                    X1 = x - len * 3 / 2, X2 = x + len * 3 / 2, Y1 = y, Y2 = y, StrokeThickness = 2,
                    Stroke = ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth - recursionLevel)
                };
                canvas.Children.Add(line);
                // Если глубина рекурсии больше единицы, вызывается рекурсивный метод для прорисовки кривой Коха.
                if (recursionLevel > 1)
                    DrawCurve(canvas, recursionLevel - 1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        /// <summary>
        /// Метод, принимающий координаты начала и конца отрезка и создающий три равных отрехка по данным координатам.
        /// Если уточнить - берется линия и дробится на 3 равные части.
        /// </summary>
        /// <param name="x1">Координата x начала отрезка.</param>
        /// <param name="x2">Координата х конца отрезка.</param>
        /// <param name="y1">Координата у начала отрезка.</param>
        /// <param name="y2">Координата у конца отрезка.</param>
        /// <param name="color">Цвет создаваемых линий.</param>
        /// <returns>Кортеж из трех равных линий.</returns>
        private static (Line,Line,Line) GetThreeLines(double x1, double x2, double y1, double y2, Brush color)
        {
            var line1 = new Line
            {
                X1 = x1, X2 = x1 + (x2-x1)/3, Y1 = y1, Y2 = y1 + (y2-y1)/3, StrokeThickness = 1, Stroke = color
            };
            var line2 = new Line
            {
                X1 = x1 + (x2-x1)/3, X2 = x1 + 2*(x2-x1)/3, Y1 = y1 + (y2-y1)/3, Y2 = y1 + 2*(y2-y1)/3, StrokeThickness = 1,
                Stroke = color
            };
            var line3 = new Line
            {
                X1 = x1 + 2*(x2-x1)/3, X2 = x2, Y1 = y1 + 2*(y2-y1)/3, Y2 = y2, StrokeThickness = 1,
                Stroke = color
            };
            return (line1, line2, line3);
        }

        /// <summary>
        /// Метод для получения списка всех линиий на канвасе.
        /// </summary>
        /// <param name="canvas">Канвас на котором находятся линии.</param>
        /// <returns>Список линий на данном канвасе.</returns>
        private static List<Line> GetListOfLines(Canvas canvas)
        {
            var list = new List<Line>(canvas.Children.Count);
            foreach (Line l in canvas.Children)
            {
                list.Add(l);
            }

            return list;
        }
        
        /// <summary>
        /// Метод для создания угла (части равностороннего треугольника) для создания кривой Коха.
        /// </summary>
        /// <param name="line">Основание равностороннего треугольника.</param>
        /// <param name="len">Длина стороны равностороннего треугольника.</param>
        /// <param name="color">Цвет создаваемых линий.</param>
        /// <returns>Кортеж из двух линий создающих угол равностороннего треугольника.</returns>
        private static (Line,Line) GetEdge(Line line, double len, Brush color)
        {
            var x = (line.X2 + line.X1) / 2 + len / 2 * Math.Sqrt(3) * ((line.Y2-line.Y1) / len);
            var y = (line.Y2 + line.Y1) / 2 - len / 2 * Math.Sqrt(3) * ((line.X2-line.X1) / len);
            var line1 = new Line {X1 = line.X1, X2 = x, Y1 = line.Y1, Y2 = y, Stroke = color, StrokeThickness = 1};
            var line2 = new Line {X1 = x, X2 = line.X2, Y1 = y, Y2 = line.Y2, Stroke = color, StrokeThickness = 1};

            return (line1, line2);
        }

        /// <summary>
        /// Метод для получения длины отрезка.
        /// </summary>
        /// <param name="line">Линия, длину которой надо найти.</param>
        /// <returns>Длина линии.</returns>
        private static double GetLength(Line line) =>
            Math.Sqrt(Math.Pow(line.X1 - line.X2, 2) + Math.Pow(line.Y1 - line.Y2, 2));

        /// <summary>
        /// Метод для создания и прорисовки кривой Коха рекурсией.
        /// </summary>
        /// <param name="canvas">Канвас на котором рисуется кривая.</param>
        /// <param name="recursionLevel">Текущая глубина рекурсии (уровень рекурсии).</param>
        private void DrawCurve(Canvas canvas, int recursionLevel)
        {
            // Линии, которые в данный момент на канвасе.
            var oldLines = GetListOfLines(canvas);
            var newLines = new List<Line>();
            // По каждой линии на канвасе генерируется три линии которые образуют ее, далее создается угол (итерация).
            foreach (var line in oldLines)
            {
                var lines = GetThreeLines(line.X1, line.X2, line.Y1, line.Y2, line.Stroke);
                var edge = GetEdge(lines.Item2, GetLength(lines.Item2),
                    ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth - recursionLevel));
                newLines.Add(lines.Item1);
                newLines.Add(lines.Item3);
                newLines.Add(edge.Item1);
                newLines.Add(edge.Item2);
            }
            // Очищается текущий список линий на канвасе и обновляется снова созданными.
            canvas.Children.Clear();
            Array.ForEach(newLines.ToArray(), x => canvas.Children.Add(x));
            // Если уровень рекурсии больше единицы происходит рекурсивный вызов.
            if (recursionLevel > 1)
                DrawCurve(canvas, recursionLevel-1);
        }
    }
}