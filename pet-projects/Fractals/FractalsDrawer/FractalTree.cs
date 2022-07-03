using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalsDrawer
{
    /// <summary>
    /// Класс с логикой построения фрактального дерева.
    /// </summary>
    internal class FractalTree : Fractal
    {
        /// <summary>
        /// Отношение длины отрезка к отрезку следующей итерации ( 0 - 1 ).
        /// </summary>
        public static double Ratio { get; set; }
        
        /// <summary>
        /// Угол наклона отрезка слева следующей итерации.
        /// </summary>
        public static int LeftAngle { get; set; }
        
        /// <summary>
        /// Угол наклона отрезка справа следующей итерации.
        /// </summary>
        public static int RightAngle { get; set; }
        
        /// <summary>
        /// Максимальная глубина рекурсии для фрактального дерева.
        /// </summary>
        public override int MaxRecursionDepth => 10;
        
        /// <summary>
        /// Метод для прорисовки фрактала с базовой логикой и обработкой исключений. (В данном случае - фрактального дерева).
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
                // х1 у1 - координаты конца отрезка начинающегося в х у.
                var x1 = x + len * Math.Sin(angle * Math.PI * 2 / 360.0);
                var y1 = y + len * Math.Cos(angle * Math.PI * 2 / 360.0);
                canvas.Children.Add(new Line 
                {
                    X1 = x, X2 = x1, Y1 = canvas.Height - y, Y2 = canvas.Height - y1, Stroke = ColorForGradient.SetGradientColor(RecursionDepth,RecursionDepth-recursionLevel), StrokeThickness = 0.5
                });
                if (recursionLevel > 1)
                {
                    // Рекурсивный вызов метода для прорисовки следующей итерации (левого и правого отрезка).
                    DrawFractal(canvas, x1, y1, len * Ratio, angle - LeftAngle, recursionLevel-1);
                    DrawFractal(canvas, x1, y1, len * Ratio, angle + RightAngle,recursionLevel-1);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
