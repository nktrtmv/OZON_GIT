   M S B A M L   `   `   `  ����  FractalsDrawer    FractalsDrawer.MainWindow   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          � Title$  FractalDrawer��5      $
��1408��5      $	��792��5	       � 
ResizeMode$ 	CanResize=�5
       � SizeToContent$ WidthAndHeight=�5         MaterialDesignBody#���   5        MaterialDesignPaper#���  5      $��Medium"�5       � WindowStyle$ SingleBorderWindow=�5      ��5      :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5       $��* �6   �� 5!      $��* �6   �� 5"      $��* �6   �� 5#      $��* �6   �� 5$      $��* �6   �� 5%      $��* �6   �� 5&      $��* �6   �� 5'      $��* �6   �� 5(      $��* �6   �� 5)      $��* �6   �� 5*      $��* �6   �� 5+      $��* �6   �� 5,      $��* �6   �� 5-      $��* �6   �� 5.      $��* �6   �� 5/      $��* �6   �� 50      $��* �6   �� 51      $��* �6   �� 52      $��* �6   �� 53      $��* �6   �� 54      $��* �6   �� 55      $��* �6   �� 56      $��* �6   �� 57      $��* �6   �� 58      $��* �6   �� 59      $��* �6   �� 5:      $��* �6   �� 5;      $��* �6   �� 5<      $��* �6   �� 5=      $��* �6   �� 5>      $��* �6   �� 5?      $��* �6   �� 5@      $��* �6   �� 5A      $��* �6   �� 5B      $��* �6   �� 5C      $��* �6   �� 5D      $��* �6   �� 5E      $��* �6    � RowDefinitions 5I   
   �� 5J      $��* �6   �� 5K      $��* �6   �� 5L      $��* �6   �� 5M      $��* �6   �� 5N      $��* �6   �� 5O      $��* �6   �� 5P      $��* �6   �� 5Q      $��* �6   �� 5R      $��* �6   �� 5S      $��* �6   �� 5T      $��* �6   �� 5U      $��* �6   �� 5V      $��* �6   �� 5W      $��* �6   �� 5X      $��* �6   �� 5Y      $��* �6   �� 5Z      $��* �6   �� 5[      $��* �6   �� 5\      $��* �6   �� 5]      $��* �6   �� 5^      $��* �6   �� 5_      $��* �6   �� 5`      $��* �6   �� 5a      $��* �6   �� 5b      $��* �6   �� 5c      $��* �6   �� 5d      $��* �6   .+�5g   
   �� -   5         ��Name$ TypeTextBlock��5h      $��0��5i      $��2��5j      $��0��5k      $��20��5l      $��Center=�5m      $��16(�5n       �� TextAlignment$ Center=�5o      �� 6%   $�Тип фрактала:��6)   6A   �� 5p   
   -   5        $ ScaleTextBlock��5u      $��0��5q      $��2��5r      $��20��5s      $��18��5t      $��Center=�5v      $��16(�5w      $ Center=�5x      �� 6%   $�Масштаб:��6)   6<   �� 5y   
   $��0��5z      $��2��5{      $��38��5|      $��10��5}      $��Center=�5~      $��16(�5      $ Center=�5�      �� 6%   $�Изображение:��6)   6@   �� 5�   
   -   5         ��Name$ FractalTreeButton��5�      $��2��5�      $��2��5�      $��0��5�      $��4��5�      $��13(�5�      +  %MaterialDesignThemes.Wpf.ButtonAssist	   CornerRadius$	 15��5�      $��Дерево��5�      �� 5�   
   -   5        $ CochCurveButton��5�      $��2��5�      $��2��5�      $��4��5�      $��4��5�      $��13(�5�      $	 15��5�      $��Кривая Коха��5�   +   �� 5�   
   -   5        $ SerpinskiCarpetButton��5�      $��2��5�      $��2��5�      $��8��5�      $��4��5�      $��13(�5�      $	 15��5�      $��
Ковер��5�   1   �� 5�   
   -   5        $ SerpinskiTriangleButton��5�      $��2��5�      $��2��5�      $��12��5�      $��4��5�      $��13(�5�      $	 15��5�      $��Треугольник��5�   3   �� 5�   
   -   5        $ CantorSetButton��5�      $��2��5�      $��2��5�      $��16��5�      $��4��5�      $	 15��5�      $��Множество��5�   +   �� 5�   
   -	   5        
 ��Name$
 ScaleSlider��5�      $��2��5�      $��2��5�      $��21��5�      $��17��5�      $��Center=�5�      5�      �� 5�   
   -
   5        $ 
SaveButton��5�      $��2��5�      $��2��5�      $��39��5�      $��8��5�      $��16(�5�      $	 15��5�      $��Сохранить��5�   &   �� 5�   
   $��4��5�      $��7��5�      $��0��5�      $��20��5�      $��16(�5�      $9��3Настройка цветов градиента:��6   A� 5�   
   -   5         A�Name$ StartColorCircle��5�      $��7��5�      $��1��5�       �� StrokeDashCap$ Round=�5�       �� StrokeStartLineCap$ Round=�5�       �� Stretch$ Uniform=�5�      $��Stretch=�5�      $��Stretch=�5�      
���   �5�      �� 5�   
   $��6��5�      $��4��5�      $��2��5�      $ Center=�5�      $��Bottom=�5�      $��R��5�      6   �� 5�   
   $��6��5�      $��10��5�      $��2��5�      $ Center=�5�      $��Bottom=�5�      $��G��5�      6   �� 5�   
   $��6��5�      $��16��5�      $��2��5�      $ Center=�5�      $��Bottom=�5�      $��B��5�      6   �� 5�   
   -   5        $
 StartRedSlider��5�      $��7��5�      $��3��5�      $��4��5�      $��Center=�5�       � Minimum$ 0Q�5�       � Maximum$	 255Q�5�      5�      �� 5�   
   -   5        $
 StartGreenSlider��5�      $��7��5�      $��9��5�      $��4��5�      $��Center=�5�      $ 0Q�5�      $	 255Q�5�      5�      �� 5�   
   -   5        $
 StartBlueSlider��5�      $��7��5      $��15��5     $��4��5     $��Center=�5     $ 0Q�5     $	 255Q�5     5     A� 5  
   -   5        $ FinalColorCircle��5	     $��9��5
     $��1��5     $ Round=�5     $ Round=�5     $ Uniform=�5     $��Stretch=�5     $��Stretch=�5     
���   �5     �� 5  
   -   5        $
 FinalRedSlider��5     $��9��5     $��3��5     $��4��5     $��Center=�5     $ 0Q�5     $	 255Q�5     5     �� 5  
   -   5        $
 FinalGreenSlider��5     $��9��5     $��9��5     $��4��5      $��Center=�5!     $ 0Q�5"     $	 255Q�5#     5$     �� 5%  
   -   5        $
 FinalBlueSlider��5&     $��9��5'     $��15��5(     $��4��5)     $��Center=�5*     $ 0Q�5+     $	 255Q�5,     5-     �� 5/  
   $��11��50     $��6��51     $��0��52     $��20��53     $��17(�54     $;��5Основные настройки фрактала:��6   �� 56  
   -   5        $ LineLengthTextBlock��57     $��13��58     $��1��59     $��6��5:     $��15(�5;     $��Bottom=�5<     �� 6)   $"�Длина отрезка: 1��6-   6H   �� 5=  
   -   5        $ RecursionDepthTextBlock��5>     $��15��5?     $��1��5@     $��6��5A     $��15(�5B     $��Bottom=�5C     �� 6)   $(�"Глубина рекурсии: 1��6-   6K   �� 5E  
   -   5        $
 LineLengthSlider��5F     $��13��5G     $��8��5H     $��11��5I     $��Bottom=�5J     $ 1Q�5K     $	 250Q�5L     5M     �� 5N  
   -   5        $
 RecursionDepthSlider��5O     $��15��5P     $��8��5Q     $��11��5R     $ 1Q�5S     $ 10Q�5T     $��Bottom=�5U     5V     �� 5X  
   -   5         ��Name$ TreeGroupBox��5Z     $��0��5Y     $��17��5[     $��8��5\     $��20��5]     $��17(�5^     ${�False��5_     $y�	Collapsed=�5`     $?��9Настройки фрактального дерева:��6$   �� 5b  
   -   5        $ TreeTextBlockRatio��5c     $��19��5d     $��1��5e     $��7��5f     $��15(�5g     ${�False��5h     $y�	Collapsed=�5i     $��Bottom=�5j     �� 6)   $"�Отношение длин:��6-   6G   �� 5k  
   -   5        $ TreeTextBlockLeftAngle��5l     $��21��5m     $��1��5n     $��7��5o     $��15(�5p     ${�False��5q     $y�	Collapsed=�5r     $��Bottom=�5s     �� 6)   $)�#Левый угол наклона:��6-   6K   �� 5t  
   -   5        $ TreeTextBlockRightAngle��5u     $��23��5v     $��1��5w     $��7��5x     $��15(�5y     ${�False��5z     $
 Left=�5{     $y�	Collapsed=�5|     $��Bottom=�5}     �� 6)   $+�%Правый угол наклона:��6-   6L   �� 5  
   -   5        $
 TreeRatioSlider��5�     $��19��5�     $��8��5�     $��11��5�     $��Bottom=�5�     ${�False��5�     $y�	Collapsed=�5�     $ 25Q�5�     $ 75Q�5�     5�     �� 5�  
   -   5        $
 TreeLeftAngleSlider��5�     $��21��5�     $��8��5�     $��11��5�     $��Bottom=�5�     ${�False��5�     $y�	Collapsed=�5�     $
 -179Q�5�     $	 179Q�5�     5�     �� 5�  
   -   5        $
 TreeRightAngleSlider��5�     $��23��5�     $��8��5�     $��11��5�     $��Bottom=�5�     ${�False��5�     $y�	Collapsed=�5�     $
 -179Q�5�     $	 179Q�5�     5�     �� 5�  
   -   5        $ CantorGroupBox��5�     $��0��5�     $��17��5�     $��8��5�     $��20��5�     $��17(�5�     ${�False��5�     $y�	Collapsed=�5�     $;��5Настройки множества кантора:��6$   �� 5�  
   -   5        $ CantorTextBlockHeight��5�     $��19��5�     $��1��5�     $��6��5�     $��15(�5�     ${�False��5�     $y�	Collapsed=�5�     $��Bottom=�5�     �� 6)   $"�Высота отрезка:��6-   6G   �� 5�  
   -    5        $ CantorTextBlockDistance��5�     $��21��5�     $��1��5�     $��6��5�     $��15(�5�     ${�False��5�     $y�	Collapsed=�5�     $��Bottom=�5�     �� 6)   $&� Расстояние между:��6-   6I   �� 5�  
   -!   5        $
 CantorHeightSlider��5�     $��19��5�     $��8��5�     $��11��5�     $��Bottom=�5�     ${�False��5�     $y�	Collapsed=�5�     $ 1Q�5�     $ 30Q�5�     5�     �� 5�  
   -"   5        $
 CantorDistanceSlider��5�     $��21��5�     $��8��5�     $��11��5�     $��Bottom=�5�     ${�False��5�     $y�	Collapsed=�5�     $ 1Q�5�     $ 25Q�5�     5�     �� 5�  
   -#   5        $��0��5�     $��25��5�     $��2��5�     $��20��5�     $��16(�5�     $��Center=�5�     $	 15��5�     $'��!Построить фрактал��5�  (   �� 5�  
   -$   5         ��Name$ ScrollViewer��5�     $��4��5�     $��20��5�     $��23��5�     $��28��5�     $
��Auto=�5�     $
��Auto=�5�     $��Bottom=�5�     $��Right=�5�     $��2q�5�     $��1q�5�     $y�Visible=�5�     .��5�     �� -%   5         ��Name$ Board��5�     z�5�     V� 5�     .~�5�     �� �� 5�     �� 5�     T� 5�     5�     5�     5�     5�     5�     