   M S B A M L   `   `   `  ����P  LStudienarbeitsProjekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:StudienarbeitsProjekt.ContentControls%StudienarbeitsProjekt.ContentControls  5      E    ?StudienarbeitsProjekt.ContentControls.ScatterOrientationControl   5      9u3clr-namespace:StudienarbeitsProjekt.ContentControls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  5      B   <StudienarbeitsProjekt.ContentControls.MovableScatterViewItem    	TouchDown$"  ScatterOrientation_TouchDown��6G      PreviewTouchDown$" ScatterOrientation_TouchDown��5      $��0q�5   :   .��5	      � 
����+�� � RowDefinitions 5
   
   �� 5      $��25 �6   �� 5      $��* �6   �� 5      $��30 �6   .+�5      �� $��0��6   
�������.��65   Control Panel6E   A  ;Microsoft.Surface.Presentation.Controls.SurfaceScrollViewer  5       �� PanningMode$
 Both=�5      $��1��64   .��5      �� $��Center=�5      $��Center=�6*   .+�5      @  :Microsoft.Surface.Presentation.Controls.SurfaceRadioButton  -   5          Name$ FreieOrientierung��5   ?   
������� d� 	IsChecked$
 TrueY�6~   .��5      �� $
��Wrap=�6$   $��Expanded&�68   Freie Orientierung5      5        5      -   5        $ RandOrientierung��5   ;   
�������.��5      �� $
��Wrap=�6    $��Expanded&�64    Orientierung anhand des Randes5       5!        5#   
   -   5        $ ScatterOrientierung��5#   3   
�������.��5$      �� $
��Wrap=�6$   $��Expanded&�68   %#Orientierung anhand dieses Fensters5&      5'        5(      -   5        $ TagOrientierung��5(   ?   
�������.��5)      �� $
��Wrap=�6$   $��Expanded&�68   Orientierung anhand des Tags5+      5,      5-      5.      =  7Microsoft.Surface.Presentation.Controls.SurfaceCheckBox  5/   
   -   5          Name$ Positionierung��5/   <   $��2��6   
�������.��50      �� $
��Wrap=�6$   $��Expanded&�68   Positionierung an Rand52      53      5?      5@      