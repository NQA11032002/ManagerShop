   M S B A M L   `   `   `  ????E  AQuanLyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:QuanLyShop
QuanLyShop  5   	       QuanLyShop.QuenMatKhau   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:QuanLyShop     5   	   _ [MaterialDesignThemes.Wpf, Version=4.4.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   -   5          ?Name$  quenMK??5   	    ? Title$ QuenMatKhau??5   	   $	??400??5   	   $	??650??6   
???   ?$??Regular"?5   	   $??13(?5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?5   	      TextRenderingMode$
 Auto=?5   	    ? WindowStyle$
 None=?5   	    ? WindowStartupLocation$ CenterScreen=?5   	    ? 
ResizeMode$ NoResize=?5   	   $??
ExtraBlack"?5   	   
??????  ? AllowsTransparency .    quenMkVM ? DataContext# [  5   	   
???   ?$??Myriad Pro Light)?5   	   .??5      #  MaterialDesignThemes.Wpf.Card  	   UniformCornerRadius$	 15Q?6   $??5q?63   .??5   
   ? 
 ? RowDefinitions
 5      ?? 5      $??60 ?6    6.   ?? 5       $??50 ?6    6.   ?? 5!      6"   .+?5$      ? $??0 0 10 0q?6   .+?5%      '  !MaterialDesignThemes.Wpf.PackIcon  $??30??5&   !   $??30??5'   !   $??Center=?5(   !   $??Right=?5)   !      Kind$ SubdirectoryArrowRight=?5*   !   ?? 5+       ? Cursor$
 Hand??5,      $??Right=?5/      $??30??50      $??30??51      
?????? $??0q?53       $  MaterialDesignRaisedAccentButton#??[ 54      ??5-      ?? *closeWindow+??5.      ??  ?? ElementName$ quenMK??54   Z   55      ? 57      $??1??6   .+?58      ?? $??Quên Mật Khẩu??6   $??30(?61   $??Center=?6?   6^   59      ?? 5;      $??0 20 0 0q?6   $??2??6+   .+?5<      ? $??Center=?6   .+?5=      ?? $??Center=?6    $??10 5 0 0q?6;   $??Tài khoản??6M   $??15(?6^   $
??Bold"?6l   6?   ?? 5>      $??22 5 0 0q?6   $	??230??6/   
???   ?$??1q?6O    ?? CornerRadius$ 5??6c   ._?5?      ?? $??15(?5@   '   $
??Bold"?5B   '   $	??210??5C   '   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Nhập tài khoản??5D   '    % !MaterialDesignFloatingHintTextBox#??[ 5E   '   ??5A   '   ?? *
TaiKhoan+ ?? UpdateSourceTrigger$ PropertyChanged=? ?? Mode$ TwoWay=?5E   '   5F      5G      ? 5I      $??Center=?6   $??0 10 0 0q?69   .+?5J      ?? $??Center=?6    $??10 5 0 0q?6;   $??Email??6M   $??15(?6Z   $
??Bold"?6h   6|   ?? 5K      $??50 5 0 0q?6   $	??230??6/   
???   ?$??1q?6O   $ 5??6c   ._?5L      ?? $??15(?5M   '   $
??Bold"?5O   '   $	??210??5P   '   $ Nhập tài khoản??5Q   '   #??[ 5R   '   ??5N   '   ?? *Email+$ PropertyChanged=?$ TwoWay=?5R   '   5S      5T        5V      $??Center=?6&   $	??150??6C   $??
10  20 0 0q?6O   ???#000$	 15Q?6u   .??5W      ?? $
 Hand??5X   $   
???????$??20(?5Z   $   $	??150??5[   $   $??Center=?5]   $   $??0q?5^   $   $??Xác Nhận??5_   $   #??[ 5`   $   ??5\   $   ?? *	sendEmail+??5a   $   Z? 5b      5c      5d      5e      5f      5g      