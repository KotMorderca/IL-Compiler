   M S B A M L   `   `   `  ���� E  AILCompiler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ILCompiler
ILCompiler      ILCompiler.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:ILCompiler       � Title$  
MainWindow��$	��600��$	��800�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$	 New��  -   $
 Open��  -   $
 Save��  -   $ SaveAs��  -   $ Close��  -   $
 Undo��  -   $
 Redo��  -   $ Print�� J� InputBindings �� 
 �� Key$ N�� �� 	Modifiers$ Controlf� �� Command$	 New���� $ O��$ Controlf�$
 Open���� $ S��$ Controlf�$
 Save���� $ S��$ Control+Shiftf�$ SaveAs���� $ W��$ Controlf�$ Close���� $ Z��$ Controlf�$
 Open���� $ Y��$ Controlf�$
 Open���� $ F5��$ Print��.���  � RowDefinitions �� $
��Auto ��� $
��40px ��� $��* ��� $��150px �.+�o� $��0��.@�m� $
��Plik��.@�m� $��Nowy...�� m� Command$	 New��	 m� InputGestureText$	 Ctrl+N���� m� $��
Otwórz...��$
 Open��$	 Ctrl+O���� m� $��Zapisz��$
 Save��$	 Ctrl+S��m� $��Zapisz jako...��$ SaveAs��$	 Ctrl+Shift+S���� m� $��Wyjście��$ Close��$	 Ctrl+W��m� $��Edycja��.@�m� $��Cofnij��$
 Undo��$	 Ctrl+Z��m� $��Ponów��$
 Redo��$	 Ctrl+Y��� $��1��
 � ColumnDefinitions
 �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��Kompiluj��$
��Auto��$��Print���� -	   $��dupcia��$��2��] YICSharpCode.AvalonEdit, Version=5.0.1.0, Culture=neutral, PublicKeyToken=9cc39be672370310'  !ICSharpCode.AvalonEdit.TextEditor  ?
avalonEdit.http://icsharpcode.net/sharpdevelop/avalonedit  -
   $��
textEditor��$��2��$��Consolas)�   SyntaxHighlightingM  GICSharpCode.AvalonEdit.Highlighting.HighlightingDefinitionTypeConverter$ C# $
��10pt(��� -   $��txt_boxConsole��$��3��$��150px�� �� 
IsReadOnly . $��Bottom=�" �� VerticalScrollBarVisibility$
 Auto=�