   M S B A M L   `   `   `  ????E  AQuanLyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:QuanLyShop
QuanLyShop  5   	       QuanLyShop.DangNhapWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:QuanLyShop     5   	   _ [MaterialDesignThemes.Wpf, Version=4.4.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   -   5          ?Name$  dnWindow??5   	    ? Title$ DangNhapWindow??5   	   $	??550??5   	   $	??950??6   
???   ?$??Regular"?5   	   $??13(?5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?5   	      TextRenderingMode$
 Auto=?5   	    ? WindowStyle$
 None=?5   	    ? WindowStartupLocation$ CenterScreen=?5   	    ? 
ResizeMode$ NoResize=?5   	   $??
ExtraBlack"?5   	   
??????  ? AllowsTransparency .    dnVM ? DataContext# [  5   	   
???   ?$??Myriad Pro Light)?5   	   .  (System.Windows.Interactivity.Interaction	   Triggers	 5      /  )System.Windows.Interactivity.EventTrigger  5   
   
   	EventName$
 MouseLeftButtonDown??6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   ?? *MouseLeftButtonDowWindow+   CommandParameter 5    $   ??  ?? ElementName$ dnWindow??6X   5!      .??5$      #  MaterialDesignThemes.Wpf.Card     UniformCornerRadius$ 15Q?6   $??5q?63   .??5%   
   ?  ? ColumnDefinitions 5&      ?? 5'      6%   ?? 5(      $	??330 ?6#   61   .+?5+      ??  ?? CornerRadius$ 	13 0 13 0??6   ._?5,      ? $??5q?6   .+?5-      ?? $??5 5 5 5q?6   $
??Fill=?6-   $/??)D:\QuanLyShop\QuanLyShop\Image\Logo\1.png??6<   6q   5.      5/      ?? 51      $??Center=?6   $	??330??62   ???	#FF181D29$??1??6V   $ 	0 13 13 0??6f   ._?52      ?  ? RowDefinitions 53      ?? 54      $??35 ?6(   66   ?? 55      6*   .+?58      ? .+?59      ? $??	265 0 0 0q?6   $??Center=?62   .+?5:      ??  K? ZIndex$ 1??5=   %   $??30??5>   %   $??30??5?   %    J? Opacity$ 0Q?5@   %   $??0q?5A   %    $  MaterialDesignRaisedAccentButton#??[ 5B   %   ??5;   %   ?? *
miniWindow+??5<   %   ?? $ dnWindow??5B   a   '  !MaterialDesignThemes.Wpf.PackIcon  5D      $??30??5E   %   $??30??5F   %   
???????$??Center=?5H   %      Kind$ WindowMinimize=?5I   %   5J      ? 5L      $??Center=?6   .+?5M      ?? $ 1??5P   %   $??30??5Q   %   $??30??5R   %   $??0q?5T   %   #??[ 5U   %   ??5N   %   ?? *closeWindow+??5O   %   ?? $ dnWindow????5S   %   Z? 5U   a     5V      $??30??5W   %   $??30??5X   %   
???????$??Center=?5Z   %   $ WindowClose=?5[   %   5]      5^      ?? 5`      $??1??6!   $??0 5 0 0q?6.   .+?5b      ?? 
???????$??30(?67   $??ĐĂNG NHẬP??6F   $??Center=?6W   6v   ?? 5d      $??5q?6%   .+?5e      ?? $??5q?6(   $??TÀI KHOẢN??63   
???????$??15(?6W   $
??Bold"?6e   6y   ?? 5g      $??5q?6%   $
??Left=?60   
???????$	??300??6^   $??1q?6j   $ 10??6~   ._?5h   "   ?? $??15(?5i   '   $
??Bold"?5k   '   $	??280??5l   '   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên Đăng Nhập??5m   '    % !MaterialDesignFloatingHintTextBox#??[ 5n   '   ??5j   '   ?? *
TaiKhoan+ ?? UpdateSourceTrigger$ PropertyChanged=? ?? Mode$ TwoWay=?5n   '   5o      5p      ?? 5r      $??5q?6%   .+?5s      ?? $??5q?6(   $??MẬT KHẨU??63   
???????$??15(?6V   $
??Bold"?6d   6x   ?? 5u      $??5q?6%   $
??Left=?60   
???????$	??300??6^   $??40??6j   $??1q?6v   $ 10??6?   ._?5v   "   G? -   5         G?Name$
 pass??5w   '   $??15(?5x   '   $
??Bold"?5y   '   $	??280??5z   '   $??32??5{   '   $??0 7 0 0q?5|   '   $ Mật Khẩu??5}   '   	 5~   &     5   *   $
 PasswordChanged??69   . 5?   .      5?   -   ?? *PasswordChangedCommand+ 5?   -   ?? $
 pass??6]   5?   +   5?   #   5?      5?      ? 5?      $??0 5 0 0q?6$   .+?5?        $??10q?63   $ 10Q?6?   .??5?   "   ??  ?? 	IsDefault . $??15(?5?   (   $	??150??5?   (   
???   ?$??Center=?5?   (   $??0q?5?   (   $??ĐĂNG NHẬP??5?   (   #??[ 5?   (   ??5?   (   ?? *
dangNhap+??5?   (   ?? $ dnWindow????5?   (   Z? 5?   #   5?        5?      $??10q?63   $ 10Q?6?   .??5?   "   ?? $??15(?5?   (   $	??140??5?   (   
???   ?$??Center=?5?   (   $??0q?5?   (   $??
ĐĂNG KÝ??5?   (   #??[ 5?   (   ??5?   (   ?? *dangKy+??5?   (   Z? 5?   #   5?      5?      5?      5?      5?      5?      5?      5?      