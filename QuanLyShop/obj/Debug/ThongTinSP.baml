   M S B A M L   `   `   `  ????E  AQuanLyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:QuanLyShop
QuanLyShop  5   	       QuanLyShop.ThongTinSP   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:QuanLyShop     5   	   _ [MaterialDesignThemes.Wpf, Version=4.4.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   -   5          ?Name$  QuanLySPWindow??5   	    ? Title$ 
ThongTinSP??5
   	   $	??500??5   	   $	??730??6      MaterialDesignBody#???   5   	   $??Regular"?5   	   $??13(?5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?5   	      TextRenderingMode$
 Auto=?5   	    ? WindowStyle$
 None=?5   	    ? WindowStartupLocation$ CenterScreen=?5   	    ? 
ResizeMode$ NoResize=?5   	   
??????  ? AllowsTransparency .   spVM ? DataContext# [ 5   	   $??Myriad Pro Light)?5   	   .??5      #  MaterialDesignThemes.Wpf.Card  	   UniformCornerRadius$	 15Q?6   $??5q?63   .??5   
   ? 
 ? ColumnDefinitions
 5      ?? 5      6%   ?? 5      6%   .+?5"      ? $??Center=?6   $	??400??6.   $	??300??6;   .+?5#      ?? ??6   ?? *Image+ ?? UpdateSourceTrigger$ PropertyChanged=?6X   5$      ? 5&      $??1??6   $	??Top=?6#   .+?5'      '  !MaterialDesignThemes.Wpf.PackIcon  $??30??5(   !   $??30??5)   !   $??Right=?5*   !      Kind$ SubdirectoryArrowRight=?5+   !   ?? 5,       ? Cursor$
 Hand??5-      $??Right=?50      $??30??51      $??30??52      
?????? $??0q?54       $  MaterialDesignRaisedAccentButton#??[ 55      ??5.      ?? *closeWindow+??5/      ??  ?? ElementName$ QuanLySPWindow??55   Z   56      ?? 58      $??0 50 0 0q?6   $??1??6+   .+?59      ?? $??5q?6   $
??Bold"?6%   $??35(?67   $??Franklin Gothic Demi Cond)?6?   ??6F   ?? *TenSP+$ PropertyChanged=?6?   ? 5;      $??5q?6   .+?5<      ?? $	??350??6   $
??Bold"?6*   $??25(?6<   ??6J   ?? *GiaSP+$ PropertyChanged=? ?? StringFormat$ C?? ?? ConverterCulture$ nl-VN??6?   5=      ? 5?      $??5q?6   .+?5@      ?? $??Center=?6    $
??Bold"?6;   $??0 15 0 0q?6M   $??20(?6_   $??Loại sản phẩm: ??6m   6?   ?? 5A      $
??Bold"?5B       ?? SelectedValuePath$ ID??5D       ?? DisplayMemberPath$ tenTL??5F      $??15(?5G      $	??200??5H      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  Chọn loại sản phẩm??5J       & "MaterialDesignFloatingHintComboBox#??[ 5K      ??5C      ?? *ListTL+ ?? SelectedItem 5E      ?? *SelectedItemQLTL+ ?? SelectedIndex 5I      ?? *SelectedIndexQLTL+$ PropertyChanged=?5L      .  (System.Windows.Interactivity.Interaction   Triggers 5M      /  )System.Windows.Interactivity.EventTrigger  5N         	EventName$ SelectionChanged??6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5O      6  0System.Windows.Interactivity.InvokeCommandAction  66   5P      5R      ? 5T      $??5q?6   .+?5U      ?? $
??Bold"?6    $??20(?62   $??Số Lượng: ??6@   6T   ?? 5V      $	??240??6@   $
??Bold"?6L   $??20(?6^    ?? 
IsReadOnly 6   ?? *
IsReadOnly+??6l   ?? *	SoLuong+$ PropertyChanged=?6?   5W      ? 5Y      $??5q?6   .+?5Z      ?? $
??Bold"?6    $??20(?62   $??Kích cỡ: ??6@   6S   ?? 5[      $??	0 -15 0 0q?5]       $??15(?5^       $
??Bold"?5`       $	??260??5a        % !MaterialDesignFloatingHintTextBox#??[ 5b        5\       ?? *
IsReadOnly+??5_       ?? *KichCo+$ PropertyChanged=? ?? Mode$ TwoWay=?5b       5c      R? 5e      $??5q?6   .@?5f      Q? $
??Bold"?5g      $??20(?5h      $??Chi tiết sản phẩm??5i      .@?5j      ?? $??16(?6D    ?? TextWrapping$
 Wrap=?6R   $	??300??6f    6"   ?? *
IsReadOnly+??6r   ?? *MieuTa+$ PropertyChanged=?6?   5k      5l      ? 5n      $??Bottom=?6   .+?5o        $??35??6+   $??5q?67   ???#000$	 15Q?6T   .??5p      ?? 
???????$??0q?5s      $??35??5t      $??20(?5u      $??Cập Nhật??5v      #??[ 5w      $	??150??5x      ??5r      ?? *suaSP+??5y      Z? 5z      5{        5}      $??35??6+   $??5q?67   ???#000$	 15Q?6T   .??5~      ?? 
???????$??0q?5?      $??35??5?      $??20(?5?      $
??Xóa??5?      #??[ 5?      $	??150??5?      ??5?      ?? *xoaSP+??5?      Z? 5?      5?      5?      5?      5?      5?      5?      