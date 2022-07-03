using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;


namespace FractalsDrawer
{
    /// <summary>
    /// Класс с логикой построения множества Кантора.
    /// </summary>
    internal class CantorSet : Fractal
    {
        /// <summary>
        /// Максимальная глубина рекурсии для множества Кантора.
        /// </summary>
        public override int MaxRecursionDepth => 9;
        
        /// <summary>
        /// Высота сегмента множества Кантора.
        /// </summary>
        public static int Height { get; set; }
        
        /// <summary>
        /// Вертикальное расстояние между сегментами множества Кантора.
        /// </summary>
        public static int Distance { get; set; }
        
        /// <summary>
        /// Метод для прорисовки фрактала с базовой логикой и обработкой исключений. (В данном случае - множества Кантора).
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
                // Первая линия, первая итерация.
                DrawSegment(canvas,x,y,len,recursionLevel);
                // Рекурсивный вызов метода для построения фрактала.
                if (RecursionDepth > 1)
                    DrawCantorSet(canvas,x,y,len,recursionLevel-1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Метод для рисования множества Кантора с помощью рекурсии.
        /// </summary>
        /// <param name="canvas">Канвас на котором рисуется множество Кантора.</param>
        /// <param name="x">Координата х середины верхней стороны прямоугольника (предыдущей итерации).</param>
        /// <param name="y">Координата у середины верхней стороны прямоугольника (предыдущей итерации).</param>
        /// <param name="len">Длина ширины сегмента (предыдущей итерации).</param>
        /// <param name="recursionLevel">Текущая глубина рекурсии (уровень рекурсии).</param>
        private static void DrawCantorSet(Canvas canvas, double x, double y, double len, int recursionLevel)
        {
            DrawSegment(canvas, x-len/3, y+Height+Distance, len/3, recursionLevel);
            DrawSegment(canvas, x+len/3, y+Height+Distance, len/3, recursionLevel);
            // Рекурсивный вызов создания множества Кантора.
            if (recursionLevel > 1)
            {
                DrawCantorSet(canvas, x-len/3, y+Height+Distance, len/3, recursionLevel-1);
                DrawCantorSet(canvas, x+len/3, y+Height+Distance, len/3, recursionLevel-1);
            }
        }

        /// <summary>
        /// Метод для рисования сегмента - прямоугольника.
        /// </summary>
        /// <param name="canvas">Канвас на котором рисуется сегмент множества Кантора.</param>
        /// <param name="x">Координата х середины верхней стороны прямоугольника.</param>
        /// <param name="y">Координата у середины верхней стороны прямоугольника.</param>
        /// <param name="len">Длина ширины сегмента.</param>
        /// <param name="recursionLevel">Текущая глубина рекурсии (уровень рекурсии).</param>
        private static void DrawSegment(Canvas canvas, double x, double y, double len, int recursionLevel)
        {
            canvas.Children.Add(new Polygon {
                Fill = ColorForGradient.SetGradientColor(RecursionDepth,RecursionDepth-recursionLevel), Points =
                {
                    new Point(x-len/2,y), new Point(x+len/2,y),
                    new Point(x+len/2,y+Height), new Point(x-len/2,y+Height)
                }});
        }
    }
}
