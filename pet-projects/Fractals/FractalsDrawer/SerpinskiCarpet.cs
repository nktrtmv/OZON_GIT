using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace FractalsDrawer
{
    /// <summary>
    /// Класс с логикой построения ковра Серпинского.
    /// </summary>
    internal class SerpinskiCarpet : Fractal
    {
        /// <summary>
        /// Максимальная глубина рекурсии для ковра Серпинского.
        /// </summary>
        public override int MaxRecursionDepth => 4;
        
        /// <summary>
        /// Метод для прорисовки фрактала с базовой логикой и обработкой исключений. (В данном случае - ковра Серпинского).
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
                // Серый квадрат - нулевая (первая итерация).
                DrawSquare(canvas,x,y,len, Brushes.Gray);
                // Первая итерация создания ковра (первый квадрат внутри серого).
                if (RecursionDepth > 0)
                    DrawSquare(canvas,x,y,len/3, ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth-recursionLevel));
                // Вызов рекурсивного метода для создания ковра.
                if (RecursionDepth > 1)
                    DrawCarpet(canvas, x, y, len/3, recursionLevel - 1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Рекурсивный метод для создания и прорисвоки ковра Серпинского.
        /// </summary>
        /// <param name="canvas">Канвас на котором рисуется ковер.</param>
        /// <param name="x">Координата х середины квадрата вокруг которого строится итерация фрактала.</param>
        /// <param name="y">Координата у середины квадрата вокруг которого строится итерация фрактала.</param>
        /// <param name="len">Длина квадрата вокруг которого строится итерация фрактала.</param>
        /// <param name="recursionLevel">Текущая глубина рекурсии (уровень рекурсии).</param>
        private static void DrawCarpet(Canvas canvas, double x, double y, double len, int recursionLevel)
        {
            // Прорисовка 8 квадратов вокруг данного.
            DrawSquare(canvas, x - len, y, len/3, ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth-recursionLevel));
            DrawSquare(canvas, x + len, y, len/3, ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth-recursionLevel));
            DrawSquare(canvas, x, y + len, len/3, ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth-recursionLevel));
            DrawSquare(canvas, x, y - len, len/3, ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth-recursionLevel));
            DrawSquare(canvas, x - len, y + len, len/3, ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth-recursionLevel));
            DrawSquare(canvas, x - len, y - len, len/3, ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth-recursionLevel));
            DrawSquare(canvas, x + len, y + len, len/3, ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth-recursionLevel));
            DrawSquare(canvas, x + len, y - len, len/3, ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth-recursionLevel));
            // Вызов рекурсии для прорисовки следующей итерации вокруг каждого из только что прорисованных квадратов.
            if (recursionLevel > 1)
            {
                DrawCarpet(canvas, x - len, y, len/3, recursionLevel-1);
                DrawCarpet(canvas, x + len, y, len/3, recursionLevel-1);
                DrawCarpet(canvas, x, y + len, len/3, recursionLevel-1);
                DrawCarpet(canvas, x, y - len, len/3, recursionLevel-1);
                DrawCarpet(canvas, x - len, y + len, len/3, recursionLevel-1);
                DrawCarpet(canvas, x - len, y - len, len/3, recursionLevel-1);
                DrawCarpet(canvas, x + len, y + len, len/3, recursionLevel-1);
                DrawCarpet(canvas, x + len, y - len, len/3, recursionLevel-1);
            }
        }

        /// <summary>
        /// Метод для прорисовки квадрата по заданным коориднатам и длине стороны.
        /// </summary>
        /// <param name="canvas">Канвас на котором рисуется квадрат.</param>
        /// <param name="x">Координата х середины квадрата.</param>
        /// <param name="y">Координата у середины квадрата.</param>
        /// <param name="len">Длина стороны квадрата.</param>
        /// <param name="color">Цвет заливки квадрата.</param>
        private static void DrawSquare(Canvas canvas, double x, double y, double len, Brush color)
        {
            canvas.Children.Add(new Polygon {
                Fill = color, Points =
                {
                    new Point(x-len/2,y+len/2), new Point(x+len/2,y+len/2),
                    new Point(x+len/2,y-len/2), new Point(x-len/2,y-len/2)
                }});
        }
    }
}
