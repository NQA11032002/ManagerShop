   M S B A M L   `   `   `  ????E  AQuanLyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:QuanLyShop
QuanLyShop  5   	       QuanLyShop.QuanLyNV   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:QuanLyShop     5   	   _ [MaterialDesignThemes.Wpf, Version=4.4.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   -   5          ?Name$  nvQuanLy??5   	    ? Title$ QuanLyNV??5   	   $	??440??5   	   $	??680??6   
???   ?$??Regular"?5   	   $??13(?5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?5   	      TextRenderingMode$
 Auto=?5   	    ? WindowStyle$
 None=?5   	    ? WindowStartupLocation$ CenterScreen=?5   	    ? 
ResizeMode$ NoResize=?5   	   $??
ExtraBlack"?5   	   
??????  ? AllowsTransparency .    nvVM ? DataContext# [  5   	   
???   ?$??Myriad Pro Light)?5   	   .??5      #  MaterialDesignThemes.Wpf.Card  	   UniformCornerRadius$	 15Q?6   $??5q?63   .??5   
   ? 
 ? RowDefinitions
 5      ?? 5       $??40 ?6    6.   ?? 5!      6"   .+?5$      ? .+?5%      ?? $??Center=?6   $??THÔNG TIN NHÂN VIÊN??67   $??30(?6S   $??Center=?6a   $
??Bold"?6~   $??Arial Black)?6?   ??6?   Z? 6?   '  !MaterialDesignThemes.Wpf.PackIcon  5'      $??Center=?5(   !   $??30??5)   !   $??30??5*   !   $??Right=?5+   !      Kind$ SubdirectoryArrowRight=?5,   !   ?? 5-       ? Cursor$
 Hand??5.      $??Right=?51      $??30??52      $??30??53      
?????? $??0q?55       $  MaterialDesignRaisedAccentButton#??[ 56      ??5/      ?? *closeWindow+??50      ??  ?? ElementName$ nvQuanLy??56   N   57      ? 59      $??1??6    ? ColumnDefinitions 5:      ?? 5;      $	??175 ?6'   65   ?? 5<      6)   .+?5?      ?? $??Center=?6   $??	0 -30 0 0q?68   .+?5@        $	??160??6*   $	 15Q?67   $??10q?6P   .??5A      ?? ??6    ?? *Avatar+6<   5B        5D      ???#000$??10q?6=   $	 15Q?6I   .??5E      ?? -   5        $??btVePhimChuaChieu??5L      $
 Hand??5F      $??20(?5G      $	??150??5H      
???????$??Center=?5K      $??0q?5M      $??Chọn Ảnh??5N      #??[ 5O      ??5J      ?? *
openFolder+??5P      Z? 5Q      5R        5T      $	??150??6*   $??10q?67   ???#000$	 15Q?6U   .??5U      ?? $
 Hand??5V   $   
???????$??20(?5X   $   $	??150??5Y   $   $??Center=?5[   $   $??0q?5\   $   $
??Xóa??5]   $   #??[ 5^   $   ??5Z   $   ?? *xoaNV+??5_   $   Z? 5`      5a      5b      ?? 5d      $??1??6   .+?5e      ? .+?5f      ?? $??5q?6%   .+?5g      ?? $??10 5 0 0q?6(   $??ID Nhân Viên??6:   $??15(?6N   $
??Bold"?6\   6p   ?? 5h      $??0 5 0 0q?6%   $	??230??66   
???   ?$??1q?6V    ?? CornerRadius$ 15??6j   ._?5i   "   ?? -   5         ??Name$
 tbID??5m   '   {?.  $??15(?5k   '   $
??Bold"?5n   '   $	??210??5o   '   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ ID??5p   '    % !MaterialDesignFloatingHintTextBox#??[ 5q   '   ??5l   '   ?? *ID+ ?? UpdateSourceTrigger$ PropertyChanged=? ?? Mode$ TwoWay=?5q   '   5r      5s      ?? 5u      $??5q?6%   .+?5v      ?? $??10 5 0 0q?6(   $??	Họ Tên??6:   $??15(?6H   $
??Bold"?6V   6j   ?? 5w      $??0 5 0 0q?6%   $	??230??66   
???   ?$??1q?6V   $ 15??6j   ._?5x   "   ?? -   5        $ tbHoTen??5{   '   $??15(?5y   '   $
??Bold"?5|   '   $	??210??5}   '   $
 Name??5~   '   #??[ 5   '   ??5z   '   ?? *HoTen+$ PropertyChanged=?$ TwoWay=?5   '   5?      5?      ?? 5?      $??5q?6%   .+?5?      ?? $??10 5 0 0q?6(   $??Giới Tính??6:   $??15(?6K   $
??Bold"?6Y   6m   ?? 5?      $??40??6%   $??0 5 0 0q?62   $	??230??6C   
???   ?$??1q?6c   $ 15??6w   ._?5?   "   ? $??10 0 0 0q?6,   $??Center=?6>   .+?5?   &   	? $??15(?62   $	??Nam??6g    d? 	IsChecked 6@   ?? *CheckGioiTinhNam+.  (System.Windows.Interactivity.Interaction   Triggers 5?   *   /  )System.Windows.Interactivity.EventTrigger  5?   .      	EventName$ Checked??6>   .  (System.Windows.Interactivity.TriggerBase   Actions. 5?   2   6  0System.Windows.Interactivity.InvokeCommandAction     Command 6H   ?? *
checkedNam+6i   5?   /   5?   '   	? 5?   &   $??15(?62   $??10 0 0 0q?6f   $
??Nữ??6x    6@   ?? *CheckGioiTinhNu+ 5?   *     5?   .   $ Checked??6>   . 5?   2      6H   ?? *	checkedNu+6h   5?   /   5?   '   5?   #   5?      5?      ?? 5?      $??5q?6%   .+?5?      ?? $??10 5 0 0q?6(   $??
Ngày Sinh??6:   $??15(?6K   $
??Bold"?6Y   6m   ?? 5?      $??0 5 0 0q?6%   $	??230??66   
???   ?$??1q?6V   $ 15??6j   ._?5?   "   (	  "System.Windows.Controls.DatePicker	  $??15(?5?   '   $	??210??5?   '    	  SelectedDateFormat$ Short=?5?   '   $ 
Ngày Sinh??5?   '    ( $MaterialDesignFloatingHintDatePicker#??[ 5?   '    	  SelectedDate 5?   '   ?? *
NgaySinh+$ PropertyChanged=?$ TwoWay=?5?   #   5?      5?      ?? 5?      $??5q?6%   .+?5?      ?? $??10 5 0 0q?6(   $??Số Điện Thoại??6:   $??15(?6O   $
??Bold"?6]   6q   ?? 5?      $??0 5 0 0q?6%   $	??230??66   
???   ?$??1q?6V   $ 15??6j   ._?5?   "   ?? $??15(?5?   '   $
??Bold"?5?   '   $	??210??5?   '   $ Số Điện Thoại??5?   '   #??[ 5?   '   ??5?   '   ?? *SDT+$ PropertyChanged=?$ TwoWay=?5?   '   5?      5?      ?? 5?      $??5q?6%   .+?5?      ?? $??10 5 0 0q?6(   $??Email??6:   $??15(?6G   $
??Bold"?6U   6i   ?? 5?      $??0 5 0 0q?6%   $	??230??66   
???   ?$??1q?6V   $ 15??6j   ._?5?   "   ?? $??15(?5?   '   $
??Bold"?5?   '   $	??210??5?   '   $ Email??5?   '   #??[ 5?   '   ??5?   '   ?? *Email+$ PropertyChanged=?$ TwoWay=?5?   '   5?      5?      ?? 5?      $??5q?6%   .+?5?      ?? $??10 5 0 0q?6(   $??Địa Chỉ??6:   $??15(?6I   $
??Bold"?6W   6k   ?? 5?      $??0 5 0 0q?6%   $	??230??66   
???   ?$??1q?6V   $ 15??6j   ._?5?   "   ?? $??15(?5?   '   $
??Bold"?5?   '   $	??210??5?   '   $ Địa Chỉ??5?   '   #??[ 5?   '   ??5?   '   ?? *DiaChi+$ PropertyChanged=?$ TwoWay=?5?   '   5?      5?      ?? 5?      $??5q?6%   .+?5?      ?? $??10 5 0 0q?6(   $??Chức Vụ??6:   $??15(?6I   $
??Bold"?6W   6k   ?? 5?      $??0 5 0 0q?6%   $	??230??66   
???   ?$??1q?6V   $ 15??6j   ._?5?   "   ?? $??15(?5?   '   $	??210??5?   '   $ Chức Vụ??5?   '    & "MaterialDesignFloatingHintComboBox#??[ 5?   '    ?? SelectedIndex 5?   '   ?? *SelecteIndex+$ PropertyChanged=?$ TwoWay=? 5?   &     5?   *   $ SelectionChanged??6:   . 5?   .      6D   ?? *selectionChanged+6k   5?   +   .@?5?   &   ?? $??Nhân Viên??63   6I   ?? 5?   &   $??
Quản Lý??63   6G   5?   #   5?      5?      ? 5?      $??5q?6   .+?5?        $	??400??62   $??	50 10 5 5q?6?   ???#000$	 15Q?6d   .??5?   "   ?? $
 Hand??5?   $   
???????$??20(?5?   $   $	??400??5?   $   $??Center=?5?   $   $??0q?5?   $   $??Cập Nhật??5?   $   #??[ 5?   $   ??5?   $   ?? *suaNV+??5?   $   Z? 5?   #   5?      5?      5?      5?      5?      5?      5?      5?      