   M S B A M L   `   `   `  ���� U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf   projetSOGECIB   projetSOGECIB.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf       � Title$  
MainWindow��$	��600��$
��1000��.���� $��0,10,10.333,-0.333q�.@��� $��Courbe de taux��.���  � ColumnDefinitions �� $
��126* ��� $
��815* �.+�#   LiveCharts.Wpf.CartesianChart  &    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�
�������$��10,80,27.8,33.8q�$��2��   Series �� *SimuVM.Series+   AxisY    LiveCharts.Wpf.Axis     Title$ Valeurs��   LabelFormatter �� *SimuVM.YFormatter+   AxisX   $ 	Maturité��   Labels �� *SimuVM.Labels+�� $��Détermination du Swap��.���� $��50q�.+�� .+��� $��Valeur du nominal��$
��Left=�$	��Top=�$��0,0,0,0q��� $
��Left=�$��23��$	��100��	 �� TextWrapping$
	 Wrap=�$	��Top=�$��	150,0,0,0q����� *SimuVM.Nominal+
 �� Mode$
 TwoWay=��� $
��Left=�$	��Top=�$��0,20,0,0q����� *SimuVM.Nominal+�� $��Payeur de taux :��$��Center=�$	��Top=�$��0,0,0,0q�	�  	� 	GroupName$
 taux��$��Center=�$	��Top=�$��	150,0,0,0q� d� 	IsChecked �� *SimuVM.IsFix+$
 TwoWay=�.��Fixes	� $
 taux��$��Center=�$	��Top=�$��
172,20,0,0q�.��	Variables�� $��Center=�$	��Top=�$��0,40,0,0q����� *SimuVM.IsFix+�� $ ��Valeur du taux fixe en % :��$��Center=�$	��Top=�$��	500,0,0,0q��� $
	 Wrap=�$��Center=�$	��Top=�$��23��$	��100��$��	750,0,0,0q����� *SimuVM.TauxFixe+$
 TwoWay=��� $��Center=�$	��Top=�$��
500,20,0,0q����� *SimuVM.TauxFixe+� .+��� $��Echéance du swap : ��$
��Left=�$	��Top=�$��0,50,0,0q��� -   $��Dates��$
��Left=�$	��Top=�$	��200��$��25��$��
120,50,0,0q����� *SimuVM.ListMaturities+ �� SelectedItem �� *SimuVM.MaturitySwap+$
 TwoWay=����� .R���  �� Orientation$ 
Horizontal=�.+��� ���� *Name+(  "System.Windows.Controls.DatePicker  $��Center=�$	��Top=�$��	20,50,0,0q�   SelectedDate �� *SimuVM.MaturitySwap+$
 TwoWay=��� $ ��Fréquence de taux fixe : ��$��Center=�$	��Top=�$��
300,50,0,0q��� $��Center=�$��23��$	��100��$
	 Wrap=�$	��Top=�$��
530,50,0,0q����� *SimuVM.FrequenceFixe+$
 TwoWay=��� $��Center=�$��23��$	��100��$
	 Wrap=�$	��Top=�$��570,100,0,0q����� *SimuVM.FrequenceVariable+$
 TwoWay=��� $$��Fréquence de taux variable : ��$��Center=�$	��Top=�$��322,100,0,0q�� .+��� -   $��CalcSwap��$ ��Calculer la valeur du swap��$��100,100,0,0q�$
��Left=�$��Bottom=�$��25��$	��200������ *CalculCommand+�� $��500,100,0,0q�$
��Left=�$��Bottom=����� *SimuVM.ValeurSwap+