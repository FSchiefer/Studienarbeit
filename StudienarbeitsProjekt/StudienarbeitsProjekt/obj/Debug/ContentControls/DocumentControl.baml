   M S B A M L   `   `   `  ����P  LStudienarbeitsProjekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:StudienarbeitsProjekt.ContentControls%StudienarbeitsProjekt.ContentControls  5      U QPresentationUI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35[>clr-namespace:System.Windows.Documents;assembly=PresentationUISystem.Windows.Documents 5      ;    5StudienarbeitsProjekt.ContentControls.DocumentControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    ?u3clr-namespace:StudienarbeitsProjekt.ContentControls    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  6K   ]System_Windows_Documents>clr-namespace:System.Windows.Documents;assembly=PresentationUI  6   $	��200��5        � 	Resources  5         DocumentViewerStyle1 �� 
TargetType �� BasedOn �� Property �� Value �� 
ResourceId �� TypeInTargetAssembly;  5System.Windows.Documents.PresentationUIStyleResources �� 
TargetType-  'System.Windows.Input.KeyboardNavigation   TabNavigation	 � ColumnDefinitions
 � RowDefinitionsA  ;Microsoft.Surface.Presentation.Controls.SurfaceScrollViewer  Name �� PanningMode �� EndPoint �� 
StartPoint � Color � Offset��%)  &	        �� 5   
   " Z�6@    6-   Z� .�5	      ��  � ��# � ��6+   �� 5
       � ��# � ��6+   �� 5       �@�FocusVisualStyle 61   Z� �� 5       �@�ContextMenu 6,   C� *�� $" PUIDocumentViewerContextMenu��"  +�� 5       � �� 5      �� 5      " Z�6&   .R�5      �� #��z��6!   #��z��6M   |�.  ._�5      � #��z��6#   $ Local=�6M   	 5   "   �� 5   &   $��* �67   
 5   "   �� 5   &   $
��Auto �64   �� 5   &   $��* �64   �� 5   &   $
��Auto �64   .+�5   "     $ PART_ContentHost��68   $
 both=�6R   
���   �
���   ���. $��0��6�   #|�z|�6�   $
��Auto=�6�   ��. $��1��6  $��1��6   ]� 5   "   $��1��6,   .+�5   &   � $��Right=�67   #��� r�6N   �� 5   &   $��10��60   $y�Visible=�6<   $	��top=�6Q   ��5   *   �� 5   .   $	 0,1&�6B   $	 0,0&�6Q   ��5!   6   � � 5"   :   $ Transparent��6G   $ 1Q�6[   5#   7   5%   /   5'   '   5(   #   5)      5*      5+      5-      5.      5/      .��51      � 
 52   
   �� 53      $��30 �6   �� 54      .+�57   
   �� -   5         ��Name$ Title��57      $��0��6    ;  5Microsoft.Surface.Presentation.Controls.SurfaceButton  58   
   2  ,System.Windows.Markup.XmlAttributeProperties  XmlSpace preserve6�   -   5          Name$ Close��58   /   $��0��6>   $y�Hidden=�6K   
�������
���  ��$��Right=�6�   .��6�      X  6�   Z� 59   
   -   5         Z�Name$ myDocViewer��59      $��3q�6.   $
��Auto��6C   $
��Auto��6R   #���   6�   $��1��6�   5;      5<      