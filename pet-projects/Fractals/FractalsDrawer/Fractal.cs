using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace FractalsDrawer
{
    /// <summary>
    /// Базовый абстрактный класс для всех фракталов, в нем находятся основные настройки для фракталов и метод для прорисовки
    /// фрактала. Так же в данном классе находится класс ColorForGradient предназначенный для создания цветового градиента.
    /// </summary>
    abstract class Fractal
    {
        /// <summary>
        /// Класс предназначенный для создания цвкетового градиента у фракталов.
        /// </summary>
        internal static class ColorForGradient
        {
            /// <summary>
            /// Значение R в RGB для первой итерации создания фрактала.
            /// </summary>
            public static byte StartRedColor { get; set; }
            
            /// <summary>
            /// Значение G в RGB для первой итерации создания фрактала.
            /// </summary>
            public static byte StartGreenColor { get; set; }
            
            /// <summary>
            /// Значение B в RGB для первой итерации создания фрактала.
            /// </summary>
            public static byte StartBlueColor { get; set; }
            
            /// <summary>
            /// Значение R в RGB для последней итерации создания фрактала.
            /// </summary>
            public static byte FinalRedColor { get; set; }
            
            /// <summary>
            /// Значение G в RGB для последней итерации создания фрактала.
            /// </summary>
            public static byte FinalGreenColor { get; set; }
            
            /// <summary>
            /// Значение B в RGB для последней итерации создания фрактала.
            /// </summary>
            public static byte FinalBlueColor { get; set; }

            /// <summary>
            /// Метод генерирующий цвет на основе глубины рекурсии, уровня рекурсии и начального и конечногг цветов в RGB.
            /// </summary>
            /// <param name="recursionDepth">Глубина рекурсии фрактала.</param>
            /// <param name="recursionLevel">Уровень рекурсии (итерация рекурсии) при построении фрактала.</param>
            /// <returns>Цвет в RGB.</returns>
            public static Brush SetGradientColor(int recursionDepth, int recursionLevel)
            {
                if (recursionLevel == 0) // первая итерация.
                    return new SolidColorBrush(Color.FromRgb(StartRedColor, StartGreenColor, StartBlueColor));
                if (recursionLevel == recursionDepth - 1) // последняя итерация.
                    return new SolidColorBrush(Color.FromRgb(FinalRedColor, FinalGreenColor, FinalBlueColor));

                var r = StartRedColor + (int)((FinalRedColor - StartRedColor) * recursionLevel / (double) recursionDepth);
                var g = StartGreenColor + (int)((FinalGreenColor - StartGreenColor) * recursionLevel / (double) recursionDepth);
                var b = StartBlueColor + (int)((FinalBlueColor - StartBlueColor) * recursionLevel / (double) recursionDepth);
                return new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
            }
        }
        /// <summary>
        /// Максимальная глубина рекурсии для фрактала.
        /// </summary>
        public abstract int MaxRecursionDepth { get; }
        
        /// <summary>
        /// Максимальная длина линии в фрактале (длина линии в первой итерации).
        /// </summary>
        public static int LineLength { get; set; }
        
        /// <summary>
        /// Текущая глубина рекурсии.
        /// </summary>
        public static int RecursionDepth { get; set; }
        
        /// <summary>
        /// Метод для прорисовки фрактала с базовой логикой и обработкой исключений.
        /// </summary>
        /// <param name="canvas">Канвас на котором рисуется фрактал.</param>
        /// <param name="x">Точка по оси OX (используется для отчета откуда строить фрактал).</param>
        /// <param name="y">Точка по оси OY (используется для отчета откуда строить фрактал).</param>
        /// <param name="len">Длина прямой.</param>
        /// <param name="angle">Вспомогатенльная величина угла в градусах для построения фрактального дерева.</param>
        /// <param name="recursionLevel">Уровень рекурсии (номер итерации в рекурсии).</param>
        public abstract void DrawFractal(Canvas canvas, double x, double y, double len, int angle, int recursionLevel);
    }
}
