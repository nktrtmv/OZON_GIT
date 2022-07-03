using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FractalsDrawer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml.
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Безпараметрический конструктор - инициализация окна и установка необходимых свойств.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ScaleSlider.Maximum = 7.5;
            ScaleSlider.Minimum = 0.75;
            Board.Height = this.Height; // * 21 / 27;
            ScrollViewer.Height = this.Height * 21 / 27;
            Board.Width = this.Width; // * 27 / 48;
            ScrollViewer.Width = this.Width * 27 / 48;
        }

        /// <summary>
        /// Тип выбранного фрактала: 1 - Фрактальное дерево; 2 - Кривая Коха; 3 - Ковер Серпинского; 4 - Треугольник
        /// Серпинского; 5 - Множество Кантора.
        /// </summary>
        private int TypeOfFractal { get; set; }
        
        /// <summary>
        /// Булевое свойство - создан фрактал в текущей момент или канвас пуст.
        /// </summary>
        private bool IsCreated { get; set; }

        /// <summary>
        /// Обработчик события изменения размеров окна. При изменении размеров окна перерисовывается фрактал!
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Board.Height = this.Height * 21 / 27;
            ScrollViewer.Height = this.Height * 21 / 27;
            Board.Width = this.Width * 27 / 48;
            ScrollViewer.Width = this.Width * 27 / 48;
            if (TypeOfFractal != 0 && IsCreated)
                DrawButton_OnClick(sender,e);
        }

        /// <summary>
        /// Метод для изменения внешнего вида окна в зависимости от выбранного фрактала (изменение вида настроек фрактала).
        /// </summary>
        private void ChangeAdditionalSettingsVisibility()
        {
            if (TypeOfFractal == 1)
            {
                ShowFractalTreeSettings();
                HideCantorSetSettings();
            }
            else if (TypeOfFractal == 5)
            {
                HideFractalTreeSettings();
                ShowCantorSetSettings();
            }
            else
            {
                HideCantorSetSettings();
                HideFractalTreeSettings();
            }
        }

        /// <summary>
        /// Метод, показывающий настройки фрактального дерева.
        /// </summary>
        private void ShowFractalTreeSettings()
        {
            TreeGroupBox.Visibility = Visibility.Visible;
            TreeTextBlockRatio.Visibility = Visibility.Visible;
            TreeTextBlockLeftAngle.Visibility = Visibility.Visible;
            TreeTextBlockRightAngle.Visibility = Visibility.Visible;
            TreeRatioSlider.Visibility = Visibility.Visible;
            TreeLeftAngleSlider.Visibility = Visibility.Visible;
            TreeRightAngleSlider.Visibility = Visibility.Visible;

            TreeGroupBox.IsEnabled = true;
            TreeTextBlockRatio.IsEnabled = true;
            TreeTextBlockLeftAngle.IsEnabled = true;
            TreeTextBlockRightAngle.IsEnabled = true;
            TreeRatioSlider.IsEnabled = true;
            TreeLeftAngleSlider.IsEnabled = true;
            TreeRightAngleSlider.IsEnabled = true;
        }

        /// <summary>
        /// Метод, скрывающий настройки фрактального дерева.
        /// </summary>
        private void HideFractalTreeSettings()
        {
            TreeGroupBox.Visibility = Visibility.Collapsed;
            TreeTextBlockRatio.Visibility = Visibility.Collapsed;
            TreeTextBlockLeftAngle.Visibility = Visibility.Collapsed;
            TreeTextBlockRightAngle.Visibility = Visibility.Collapsed;
            TreeRatioSlider.Visibility = Visibility.Collapsed;
            TreeLeftAngleSlider.Visibility = Visibility.Collapsed;
            TreeRightAngleSlider.Visibility = Visibility.Collapsed;

            TreeGroupBox.IsEnabled = false;
            TreeTextBlockRatio.IsEnabled = false;
            TreeTextBlockLeftAngle.IsEnabled = false;
            TreeTextBlockRightAngle.IsEnabled = false;
            TreeRatioSlider.IsEnabled = false;
            TreeLeftAngleSlider.IsEnabled = false;
            TreeRightAngleSlider.IsEnabled = false;
        }

        /// <summary>
        /// Метод, показывающий настройки множества Кантора.
        /// </summary>
        private void ShowCantorSetSettings()
        {
            CantorGroupBox.Visibility = Visibility.Visible;
            CantorTextBlockHeight.Visibility = Visibility.Visible;
            CantorTextBlockDistance.Visibility = Visibility.Visible;
            CantorHeightSlider.Visibility = Visibility.Visible;
            CantorDistanceSlider.Visibility = Visibility.Visible;
            
            CantorGroupBox.IsEnabled = true;
            CantorTextBlockHeight.IsEnabled = true;
            CantorTextBlockDistance.IsEnabled = true;
            CantorHeightSlider.IsEnabled = true;
            CantorDistanceSlider.IsEnabled = true;
        }
        
        /// <summary>
        /// Метод, скрывающий настройки множества Кантора.
        /// </summary>
        private void HideCantorSetSettings()
        {
            CantorGroupBox.Visibility = Visibility.Collapsed;
            CantorTextBlockHeight.Visibility = Visibility.Collapsed;
            CantorTextBlockDistance.Visibility = Visibility.Collapsed;
            CantorHeightSlider.Visibility = Visibility.Collapsed;
            CantorDistanceSlider.Visibility = Visibility.Collapsed;
            
            CantorGroupBox.IsEnabled = false;
            CantorTextBlockHeight.IsEnabled = false;
            CantorTextBlockDistance.IsEnabled = false;
            CantorHeightSlider.IsEnabled = false;
            CantorDistanceSlider.IsEnabled = false;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Фрактальное дерево".
        /// Выбор фрактального дерева в качестве текущего фрактала.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void FractalTreeButton_Click(object sender, RoutedEventArgs e)
        {
            TypeOfFractal = 1;
            IsCreated = false;
            Board.Children.Clear();
            ChangeAdditionalSettingsVisibility();
            RecursionDepthSlider.Maximum = new FractalTree().MaxRecursionDepth;
            LineLengthSlider.Maximum = 150;
            TypeTextBlock.Text = "Тип фрактала: Фрактальное дерево";
            TreeTextBlockLeftAngle.Text = $"Левый угол наклона: 0\u00B0";
            TreeTextBlockRightAngle.Text = $"Правый угол наклона: 0\u00B0";
            TreeRatioSlider.Value = 0.15;
            TreeLeftAngleSlider.Value = 0;
            TreeRightAngleSlider.Value = 0;
            ScaleSlider.Value = 0.75;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Кривая Коха". Выбор кривой Коха в качестве текущего фрактала.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void CochCurveButton_Click(object sender, RoutedEventArgs e)
        {
            TypeOfFractal = 2;
            Board.Children.Clear();
            ChangeAdditionalSettingsVisibility();
            IsCreated = false;
            LineLengthSlider.Maximum = 250;
            RecursionDepthSlider.Maximum = new CochCurve().MaxRecursionDepth;
            TypeTextBlock.Text = "Тип фрактала: Кривая Коха";
            ScaleSlider.Value = 0.75;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Ковер Серпинского".
        /// Выбор ковра Серпинского в качестве текущего фрактала.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void SerpinskiCarpetButton_Click(object sender, RoutedEventArgs e)
        {
            TypeOfFractal = 3;
            Board.Children.Clear();
            ChangeAdditionalSettingsVisibility();
            IsCreated = false;
            RecursionDepthSlider.Maximum = new SerpinskiCarpet().MaxRecursionDepth;
            LineLengthSlider.Maximum = 600;
            TypeTextBlock.Text = "Тип фрактала: Ковер Серпинского";
            ScaleSlider.Value = 0.75;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Треугольник Серпинского".
        /// Выбор треугольника Серпинского в качестве текущего фрактала.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void SerpinskiTriangleButton_Click(object sender, RoutedEventArgs e)
        {
            TypeOfFractal = 4;
            Board.Children.Clear();
            ChangeAdditionalSettingsVisibility();
            IsCreated = false;
            LineLengthSlider.Maximum = 600;
            RecursionDepthSlider.Maximum = new SerpinskiTriangle().MaxRecursionDepth;
            TypeTextBlock.Text = "Тип фрактала: Треугольник Серпинского";
            ScaleSlider.Value = 0.75;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Множество Кантора".
        /// Выбор множества Кантора в качестве текущего фрактала.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void CantorSetButton_Click(object sender, RoutedEventArgs e)
        {
            TypeOfFractal = 5;
            Board.Children.Clear();
            ChangeAdditionalSettingsVisibility();
            IsCreated = false;
            RecursionDepthSlider.Maximum = new CantorSet().MaxRecursionDepth;
            LineLengthSlider.Maximum = 700;
            TypeTextBlock.Text = "Тип фрактала: Множество Кантора";
            CantorDistanceSlider.Value = 1;
            CantorHeightSlider.Value = 1;
            ScaleSlider.Value = 0.75;
        }
        
        /// <summary>
        /// Обработчик события изменения значения слайдера масштаба - изменяется масштаб канваса.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void ScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScaleTextBlock.Text = $"Масштаб: x{ScaleSlider.Value:F2}";
            Board.LayoutTransform = new ScaleTransform(ScaleSlider.Value, ScaleSlider.Value);
            ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.ScrollableWidth / 2);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.ScrollableHeight / 2);
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Сохранить" - сохранение канваса в формате PNG, если фрактал создан.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TypeOfFractal == 0)
                {
                    MessageBox.Show("Вы не выбрали какой фрактал построить.\n" +
                                    "Выберите фрактал и настройте его, тогда можно будет сохранить!");
                }
                else if (IsCreated == false)
                {
                    MessageBox.Show("Вы не построили фрактал.\n" +
                                    "Постройте фрактал и можно будет сохранить!");
                }
                else
                {
                    SaveImage(Board);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Метод для изменения цвета эллипсов показывающий выбранный слайдерами цвет.
        /// </summary>
        /// <param name="a">Целочисленный параметр показывающий цвет какого эллипса менять.</param>
        private void ChangeCircleColor(int a)
        {
            byte r, g, b;
            if (a == 1)
            {
                (r, g, b) = (Fractal.ColorForGradient.StartRedColor, Fractal.ColorForGradient.StartGreenColor,
                    Fractal.ColorForGradient.StartBlueColor);
                StartColorCircle.Fill = new SolidColorBrush(Color.FromArgb(255, r, g, b));
            }
            else
            {
                (r, g, b) = (Fractal.ColorForGradient.FinalRedColor, Fractal.ColorForGradient.FinalGreenColor,
                    Fractal.ColorForGradient.FinalBlueColor);
                FinalColorCircle.Fill = new SolidColorBrush(Color.FromArgb(255, r, g, b));
            }
        }
        
        /// <summary>
        /// Обработчик события изменения значения слайдера начального красного цвета.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void StartRedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Fractal.ColorForGradient.StartRedColor = (byte)Math.Round(StartRedSlider.Value);
            ChangeCircleColor(1);
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера начального зеленого цвета.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void StartGreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Fractal.ColorForGradient.StartGreenColor = (byte)Math.Round(StartGreenSlider.Value);
            ChangeCircleColor(1);
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера начального синего цвета.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void StartBlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Fractal.ColorForGradient.StartBlueColor = (byte)Math.Round(StartBlueSlider.Value);
            ChangeCircleColor(1);
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера конечного красного цвета.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void FinalRedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Fractal.ColorForGradient.FinalRedColor = (byte)Math.Round(FinalRedSlider.Value);
            ChangeCircleColor(2);
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера конечного зеленого цвета.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void FinalGreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Fractal.ColorForGradient.FinalGreenColor = (byte)Math.Round(FinalGreenSlider.Value);
            ChangeCircleColor(2);
        }
        
        /// <summary>
        /// Обработчик события изменения значения слайдера конечного синего цвета.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void FinalBlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Fractal.ColorForGradient.FinalBlueColor = (byte)Math.Round(FinalBlueSlider.Value);
            ChangeCircleColor(2);
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера глуюины рекурсии для фрактала.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void RecursionDepthSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Fractal.RecursionDepth = (int)Math.Round(RecursionDepthSlider.Value);
            RecursionDepthTextBlock.Text = $"Глубина рекурсии: {Fractal.RecursionDepth}";
            if (IsCreated)
                DrawButton_OnClick(sender,e);
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера длины начальной линии фрактала.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void LineLengthSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Fractal.LineLength = (int)Math.Round(LineLengthSlider.Value);
            LineLengthTextBlock.Text = $"Длина отрезка: {Fractal.LineLength}";
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера отношения отрезков двух итераций у фрактального дерева.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void TreeRatioSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FractalTree.Ratio = Math.Round(TreeRatioSlider.Value) / 100;
            TreeTextBlockRatio.Text = $"Отношение длин: {FractalTree.Ratio}";
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера угла левого отрезка фрактального дерева.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void TreeLeftAngleSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FractalTree.LeftAngle = (int)Math.Round(TreeLeftAngleSlider.Value);
            TreeTextBlockLeftAngle.Text = $"Левый угол наклона: {FractalTree.LeftAngle}\u00B0";
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера угла правого отрезка фрактального дерева.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void TreeRightAngleSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FractalTree.RightAngle = (int)Math.Round(TreeRightAngleSlider.Value);
            TreeTextBlockRightAngle.Text = $"Правый угол наклона: {FractalTree.RightAngle}\u00B0";
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Построить фрактал" - прорисовка и построение выбранного фрактала.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void DrawButton_OnClick(object sender, RoutedEventArgs e)
        {
            try {
                if (TypeOfFractal == 0) {
                    MessageBox.Show("Вы не выбрали какой фрактал построить.\n" +
                                    "Выберите фрактал и настройте его!");
                    return;
                }
                
                Board.Height = this.Height; // * 21 / 27;
                Board.Width = this.Width; //* 27 / 48;
                Board.Children.Clear();
                if (TypeOfFractal == 1) {
                    new FractalTree().DrawFractal(Board, Board.Width/2, Board.Height/4, Fractal.LineLength, 0, Fractal.RecursionDepth);
                    IsCreated = true;
                }
                else if (TypeOfFractal == 2) {
                    new CochCurve().DrawFractal(Board, Board.Width/2, Board.Height*2/3, Fractal.LineLength, 0, Fractal.RecursionDepth);
                    IsCreated = true;
                }
                else if (TypeOfFractal == 3) {
                    new SerpinskiCarpet().DrawFractal(Board, Board.Width/2, Board.Height/2, Fractal.LineLength, 0, Fractal.RecursionDepth);
                    IsCreated = true;
                }
                else if (TypeOfFractal == 4) {
                    new SerpinskiTriangle().DrawFractal(Board, Board.Width/2, Board.Height*5/6, Fractal.LineLength, 0, Fractal.RecursionDepth);
                    IsCreated = true;
                }
                else if (TypeOfFractal == 5) {
                    new CantorSet().DrawFractal(Board, Board.Width/2, Board.Height/4, Fractal.LineLength, 0, Fractal.RecursionDepth);
                    IsCreated = true;
                }
                ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.ScrollableWidth / 2);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера высоты сегмента множества Кантора.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void CantorHeightSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CantorSet.Height = (int) Math.Round(CantorHeightSlider.Value);
            CantorTextBlockHeight.Text = $"Высота отрезка: {CantorSet.Height}";
        }

        /// <summary>
        /// Обработчик события изменения значения слайдера вертикального расстояния между сегментами множества Кантора.
        /// </summary>
        /// <param name="sender">Объект, который инициализирует событие</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        private void CantorDistanceSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CantorSet.Distance = (int) Math.Round(CantorDistanceSlider.Value);
            CantorTextBlockDistance.Text = $"Расстояние между: {CantorSet.Distance}";
        }
        
        /// <summary>
        /// Метод для рендера канваса и сохранения его в формате png в папке "Save".
        /// </summary>
        /// <param name="canvas"></param>
        private static void SaveImage(Canvas canvas)
        {
            var rtb = new RenderTargetBitmap((int)canvas.Width*4,
                (int)canvas.Height*5, 600d, 600d, PixelFormats.Pbgra32);
            rtb.Render(canvas);

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var fs = System.IO.File.OpenWrite(@$"Save/fractal{DateTime.Now.ToString(" - dd.MM HH-mm-ss")}.png"))
            {
                pngEncoder.Save(fs);
            }
        }
    }
}