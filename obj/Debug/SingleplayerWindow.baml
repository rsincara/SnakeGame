   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	        WpfApp1.SingleplayerWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	   -   5          � Title$  SinglePlayer��5	   	   $	��450��6   $	��800��6+   ��5
      �� 5   
   -   5        
Backgr�� �� ImageSource$H BC:\Users\Bitard\source\repos\WpfApp1\bin\Debug\images\backgame.png��5   %   .��5      � .+�5   
   �� -   5         ��Name$ canvas��5      $
��Left=�6!   $	��450��6<   $	��Top=�6I   $	��800��6a    K� ZIndex$ 1��6m   �� 5   
   -   5        $ 
foodCanvas��5      $
��Left=�6%   $	��450��6@   $	��Top=�6M   $	��800��6e   $ 0��6q   � 5   
   -   5         �Name$ myGrid��5      $��Right=�6   $	��400��6;   $	��Top=�6H   $	��100��6`   $��0,2,0,0q�6l   .+�5      �� -   5         ��Name$ resultBlock��5      $
��Left=�6-   $��0,12,0,0q�6H   $
��Wrap=�6Z   $��Score: ��6n   $	��Top=�6}   $��29��6�   $��87��6�   $��20(�6�   $��Gadugi)�6�   �� 5      $
��Left=�6   $��	0,38,-7,0q�63   $
��Wrap=�6F   $��Best:��6Z   $	��Top=�6g   $��24��6   $	��107��6�   $��20(�6�   $��Rockwell)�6�   $
��Bold"�6�   �� 5      -   5        $ 	bestScore��5      $
��Left=�6+   $��	-4,55,0,0q�6F   $
��Wrap=�6Y   $��	TextBlock��6m   $	��Top=�6~   $	��101��6�    �� TextAlignment$ Center=�6�   $��20(�6�   $
��Bold"�6�   5      5      5      