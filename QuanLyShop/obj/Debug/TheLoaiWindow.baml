   M S B A M L   `   `   `  ????E  AQuanLyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:QuanLyShop
QuanLyShop  5   	       QuanLyShop.TheLoaiWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:QuanLyShop     5   	   _ [MaterialDesignThemes.Wpf, Version=4.4.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   -   5          ?Name$  theLoai??5   	    ? Title$ TheLoaiWindow??5
   	   $	??570??5   	   $	??760??6   
???   ?$??Regular"?5   	   $??13(?5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?5   	      TextRenderingMode$
 Auto=?5   	    ? WindowStyle$
 None=?5   	    ? WindowStartupLocation$ CenterScreen=?5   	    ? 
ResizeMode$ NoResize=?5   	   $??
ExtraBlack"?5   	   
??????  ? AllowsTransparency .    tlVM ? DataContext# [  5   	   
???   ?$??Myriad Pro Light)?5   	   .??5      #  MaterialDesignThemes.Wpf.Card  	   UniformCornerRadius$	 15Q?6   $??5q?63   .??5   
   ? 
 ? RowDefinitions
 5      ?? 5      $??50 ?6    6.   ?? 5       $	??200 ?6    6/   ?? 5!      6#   .+?5$      ? .+?5%      '  !MaterialDesignThemes.Wpf.PackIcon  $??Center=?5&   !   $??30??5'   !   $??30??5(   !   $??Right=?5)   !      Kind$ SubdirectoryArrowRight=?5*   !   ?? 5+       ? Cursor$
 Hand??5,      $??Right=?5/      $??30??50      $??30??51      
?????? $??0q?53       $  MaterialDesignRaisedAccentButton#??[ 54      ??5-      ?? *closeWindow+??5.      ??  ?? ElementName$ theLoai??54   N   55      ? 57      $??1??6   .+?58      ?? .+?59      ? $??5q?6    $??Center=?6+   .+?5:      ?? $??Center=?6$   $??10 5 0 0q?6?   $??ID??6Q   $??15(?6[   $
??Bold"?6i   6}   ?? 5;      $??85 5 0 0q?6!   $	??400??63   
???   ?$??1q?6S    ?? CornerRadius$ 5??6g   ._?5<      ?? $??5q?5=   '   {?.  $??15(?5?   '   $
??Bold"?5A   '   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ ID??5B   '    % !MaterialDesignFloatingHintTextBox#??[ 5C   '   ??5@   '   ?? *ID+ ?? UpdateSourceTrigger$ PropertyChanged=? ?? Mode$ TwoWay=?5C   '   5D      5E      ? 5G      $??5q?6    $??Center=?6+   .+?5H      ?? $??10 5 0 0q?6$   $??Center=?66   $??TÊN THỂ LOẠI??6Q   $??15(?6e   $
??Bold"?6s   6?   ?? 5I      $??3 5 0 0q?6!   $	??400??62   
???   ?$??1q?6R   $ 5??6f   ._?5J      ?? $??5q?5K   '   $??15(?5L   '   $
??Bold"?5N   '   $ Nhập Tên Thể Loại??5O   '   #??[ 5P   '   ??5M   '   ?? *TenTL+$ PropertyChanged=?$ TwoWay=?5P   '   5Q      5R      ? 5T      $??Center=?6    .+?5U        $	??150??6.   $??100 10 10 10q?6:   ???#000$	 15Q?6b   .??5V      ?? $
 Hand??5W   $   
???????$??20(?5Y   $   $	??150??5Z   $   $??Center=?5\   $   $??0q?5]   $   $??Thêm??5^   $   #??[ 5_   $   ??5[   $   ?? *themTL+??5`   $   Z? 5a      5b        5d      $	??150??6.   $??10q?6;   ???#000$	 15Q?6Y   .??5e      ?? $
 Hand??5f   $   
???????$??20(?5h   $   $	??150??5i   $   $??Center=?5k   $   $??0q?5l   $   $??Sửa??5m   $   #??[ 5n   $   ??5j   $   ?? *suaTL+??5o   $   Z? 5p      5q        5s      $	??150??6.   $??10q?6;   ???#000$	 15Q?6Y   .??5t      ?? $	??150??5u   $   $
 Hand??5v   $   
???????$??20(?5x   $   $??Center=?5z   $   $??0q?5{   $   $
??Xóa??5|   $   #??[ 5}   $   ??5y   $   ?? *xoaTL+??5~   $   Z? 5      5?      5?      5?      5?      ?? 5?      $	??750??6   $??2??6#   $??Center=?60   $??Hidden=?6U   ??6?   ?? *ListTL+ ?? SelectedItem 6?   ?? *SelectedItem+$ PropertyChanged=? ?? View 5?      ?? 5?      .P?5?      ?? $??ID??6)    ?? DisplayMemberBinding 65   ?? *ID+6[   ?? 5?      $??Tên Thể Loại??6)    6?   ?? *tenTL+6h   5?      5?      5?      5?      5?      