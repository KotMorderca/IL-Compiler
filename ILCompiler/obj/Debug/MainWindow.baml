   M S B A M L   `   `   `  ����E  AILCompiler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ILCompiler
ILCompiler  5   	       ILCompiler.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ILCompiler     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$	 New��5
      6F     5   
   -   5        $
 Open��5      6H     5   
   -   5        $
 Save��5      6H     5   
   -   5        $ SaveAs��5      6L     5   
   -   5        $ Close��5      6I     5   
   -   5        $
 Undo��5      6H     5   
   -   5        $
 Redo��5      6H     5   
   -   5        $ Print��5      6D    J� InputBindings 5      �� 5   
   
 �� Key$ N��6    �� 	Modifiers$ Controlf�6    �� Command$	 New��61   6A   �� 5   
   $ O��6   $ Controlf�6   $
 Open��61   6B   �� 5   
   $ S��6   $ Controlf�6   $
 Save��61   6B   �� 5   
   $ S��6   $ Control+Shiftf�6   $ SaveAs��67   6J   �� 5   
   $ W��6   $ Controlf�6   $ Close��61   6C   �� 5   
   $ Z��6   $ Controlf�6   $
 Open��61   6B   �� 5   
   $ Y��6   $ Controlf�6   $
 Open��61   6B   �� 5   
   $ F5��6   $ Print��6   60   .��5!      �  � RowDefinitions 5"   
   �� 5#      $
��Auto �6   �� 5$      $
��40px �6   �� 5%      $��* �6   �� 5&      $��150px �6   .+�5(   
   o� $��0��6   .@�5)      m� $
��Plik��6   .@�5*      m� $��Nowy...��6    m� Command$	 New��6,   	 m� InputGestureText$	 Ctrl+N��6:   �� 5+      m� 5,      $��
Otwórz...��6   $
 Open��6.   $	 Ctrl+O��6=   �� 5-      m� 5.      $��Zapisz��6   $
 Save��6+   $	 Ctrl+S��6:   m� 5/      $��Zapisz jako...��6   $ SaveAs��63   $	 Ctrl+Shift+S��6D   �� 50      m� 51      $��Wyjście��6   $ Close��6,   $	 Ctrl+W��6<   52      m� 53      $��Edycja��6   .@�54      m� $��Cofnij��6   $
 Undo��6+   $	 Ctrl+Z��6:   m� 55      $��Ponów��6   $
 Redo��6*   $	 Ctrl+Y��69   56      57      � 58   
   $��1��6   
 � ColumnDefinitions
 59      �� 5:      $
��Auto �6#   �� 5;      $
��Auto �6#   �� 5<      $
��Auto �6#   .+�5>      �� $��0��6   $��Kompiluj��6%   $
��Auto��68   $��Print��6E   �� 5?      -	   5        $��dupcia��5?   (   $��2��6   6(   5@      ] YICSharpCode.AvalonEdit, Version=5.0.1.0, Culture=neutral, PublicKeyToken=9cc39be672370310'  !ICSharpCode.AvalonEdit.TextEditor  5A   
   ?
avalonEdit.http://icsharpcode.net/sharpdevelop/avalonedit  5C      -
   5        $��
textEditor��5D      $��2��5B      $��Consolas)�5E         SyntaxHighlightingM  GICSharpCode.AvalonEdit.Highlighting.HighlightingDefinitionTypeConverter$ C# 5F      $
��10pt(�5G      �� 5H   
   -   5        $��txt_boxConsole��5K      $��3��5I      $��150px��5J       �� 
IsReadOnly . $��Bottom=�5M      " �� VerticalScrollBarVisibility$
 Auto=�5N      5P      5Q      