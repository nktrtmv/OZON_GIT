using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace FractalsDrawer
{
    /// <summary>
    /// Класс с логикой построения треугольника Серпинского.
    /// </summary>
    internal class SerpinskiTriangle : Fractal
    {
        /// <summary>
        /// Максимальная глубина рекурсии для треугольника Серпинского.
        /// </summary>
        public override int MaxRecursionDepth => 7;
        
        /// <summary>
        /// Метод для прорисовки фрактала с базовой логикой и обработкой исключений. (В данном случае - треугольника Серпинского).
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
                // Прорисовка первой итерации - первого треугольника вершиной наверх.
                canvas.Children.Add(new Polygon {
                    Stroke = ColorForGradient.SetGradientColor(RecursionDepth, RecursionDepth-recursionLevel), Points = {
                        new Point(x-len/2,y), new Point(x+len/2,y), new Point(x, y - len*Math.Sqrt(3)/2)}
                });
                // Прорисовка второй итерации - треугольника уже вершиной вниз.
                if (RecursionDepth > 1)
                    DrawInvertedTriangle(canvas, x,y-len*Math.Sqrt(3)/4, len/2, 
                        ColorForGradient.SetGradientColor(RecursionDepth,RecursionDepth-recursionLevel+1));
                // Рекурсивная прорисовка следующих итераций фрактала.
                if (RecursionDepth > 2)
                    DrawTriangles(canvas, x,y-len*Math.Sqrt(3)/4, len/2, recursionLevel-2);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Метод для прорисовки треугольника серпинского с помощью рекурсии.
        /// </summary>
        /// <param name="canvas">Канвас на котром рисуется треугольник.</param>
        /// <param name="x">Кооридината х середины верхней стороны треугольника предыдущей итерации.</param>
        /// <param name="y">Кооридината у середины верхней стороны треугольника предыдущей итерации.</param>
        /// <param name="len">Длина сторон треугольника предыдущей итерации.</param>
        /// <param name="recursionLevel">Текущая глубина рекурсии (уровень рекурсии).</param>
        private static void DrawTriangles(Canvas canvas, double x, double y, double len, int recursionLevel)
        {
            DrawInvertedTriangle(canvas, x, y - len*Math.Sqrt(3)/4, len/2,
                ColorForGradient.SetGradientColor(RecursionDepth,RecursionDepth-recursionLevel));
            DrawInvertedTriangle(canvas, x - len/2, y + len*Math.Sqrt(3)/4, len/2,
                ColorForGradient.SetGradientColor(RecursionDepth,RecursionDepth-recursionLevel));
            DrawInvertedTriangle(canvas, x + len/2, y + len*Math.Sqrt(3)/4, len/2,
                ColorForGradient.SetGradientColor(RecursionDepth,RecursionDepth-recursionLevel));
            // Рекурсивный вызов следующей итерации.
            if (recursionLevel > 1)
            {
                DrawTriangles(canvas, x, y - len*Math.Sqrt(3)/4, len/2, recursionLevel-1);
                DrawTriangles(canvas, x - len/2, y + len*Math.Sqrt(3)/4, len/2, recursionLevel-1);
                DrawTriangles(canvas, x + len/2, y + len*Math.Sqrt(3)/4, len/2, recursionLevel-1);
            }
        }

        /// <summary>
        /// Метод для прорисовки равностороннего треугольника с углом смотрящим вниз
        /// </summary>
        /// <param name="canvas">Канвас на котром рисуется треугольник.</param>
        /// <param name="x">Кооридината х середины верхней стороны треугольника.</param>
        /// <param name="y">Кооридината у середины верхней стороны треугольника.</param>
        /// <param name="len">Длина стороны треугольника.</param>
        /// <param name="color">Цвет линий треугольника при построении.</param>
        private static void DrawInvertedTriangle(Canvas canvas, double x, double y, double len, Brush color)
        {
            canvas.Children.Add(new Polygon {
                Stroke = color, StrokeThickness = 1, Points =
                {
                    new Point(x-len/2,y), new Point(x+len/2,y), new Point(x,y+len*Math.Sqrt(3)/2)
                }});
        }
    }
}
