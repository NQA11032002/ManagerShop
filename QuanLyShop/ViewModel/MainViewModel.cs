using LiveCharts;
using LiveCharts.Wpf;
using QuanLyShop.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyShop.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private QUANLYSHOPEntities db;

        public static bool checkDongWindow { get; set; }

        private int _tongHoaDonThanhToanTrongNgay;
        public int tongHoaDonThanhToanTrongNgay { get => _tongHoaDonThanhToanTrongNgay; set { _tongHoaDonThanhToanTrongNgay = value; OnPropertyChanged(); } }

        private float _tongDoanhThuTrongNgay;
        public float tongDoanhThuTrongNgay { get => _tongDoanhThuTrongNgay; set { _tongDoanhThuTrongNgay = value; OnPropertyChanged(); } }

        private int _tongSanPhamBanTrongNgay;
        public int tongSanPhamBanTrongNgay { get => _tongSanPhamBanTrongNgay; set { _tongSanPhamBanTrongNgay = value; OnPropertyChanged(); } }
        public string chucVu { get; set; }
        public string Avatar { get; set; }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Format { get; set; }

        public ICommand miniWindow { get; set; }
        public ICommand closeWindow { get; set; }
        public ICommand MouseLeftButtonDowWindow { get; set; }
        public ICommand KhachHangWindow { get; set; }
        public ICommand SanPhamWindow { get; set; }
        public ICommand nhapHangWindow { get; set; }
        public ICommand hoaDonWindow { get; set; }
        public ICommand thongTinWindow { get; set; }
        public ICommand dangXuatWindow { get; set; }
        public ICommand thongKeWindow { get; set; }   
        public ICommand trangChuWindow { get; set; }
        public ICommand theloaiWindow { get; set; }
        public MainViewModel()
        {
            Avatar = DangNhapViewModel.AvatarLogin;

            SeriesCollection = new SeriesCollection();

            //thu nhỏ window
            miniWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.WindowState = WindowState.Minimized;
            });

            //đăng xuất
            dangXuatWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //gán biến check = fasle kiểm tra đăng xuất thì không dừng chương trình
                checkDongWindow = false;
                p.Close();
            });

            //đóng window
            closeWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //gán check = true kiểm tra dừng chương trình
                checkDongWindow = true;
                p.Close();
            });

            //duy chuyển uiwndow
            MouseLeftButtonDowWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });

            //form khách hàng
            KhachHangWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                KhachHangWindow kh = new KhachHangWindow();
                kh.ShowDialog();
                p.ShowDialog();
            });

            //form sản phẩm
            SanPhamWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                SanPhamWindow sp = new SanPhamWindow();
                sp.ShowDialog();
                p.ShowDialog();
            });

            //form nhập hàng
            nhapHangWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                NhapHangWindow nh = new NhapHangWindow();
                nh.ShowDialog();
                p.ShowDialog();
            });

            //form hóa đơn
            hoaDonWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                HoaDonWindow hd = new HoaDonWindow();
                hd.ShowDialog();
                p.ShowDialog();
            });

            //form thông tin tài khoản
            thongTinWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ThongTinWindow tt = new ThongTinWindow();
                tt.ShowDialog();
            });

            //reload trang chủ
            trangChuWindow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                tongHoaDon();
                tongDoanhThu();
                tongSanPham();
                doThiThongSo();
            });

            //form thống kê
            thongKeWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                ThongKeWindow tt = new ThongKeWindow();
                tt.ShowDialog();
                p.ShowDialog();
            });

            //form quản lý thể loại
            theloaiWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                TheLoaiWindow tl = new TheLoaiWindow();
                tl.ShowDialog();
                p.ShowDialog();
            });

            tongHoaDon();
            tongDoanhThu();
            tongSanPham();
            doThiThongSo();
        }
        //đồ thị
        public void doThiThongSo()
        {
            SeriesCollection.Clear();

            //thông số sản phẩm
            SeriesCollection.Add(new ColumnSeries() { Title = "Tổng Sản Phẩm", Values = new ChartValues<double> { tongSanPhamBanTrongNgay } });

            //tháng hiện tại
            Labels = new[] { DateTime.Now.Day.ToString() };

            //định dạng kiểu sau dấu , nhiêu số 
            Format = value => value.ToString();
        }

        //Tổng số hóa đơn trong ngày
        public void tongHoaDon()
        {
            tongHoaDonThanhToanTrongNgay = 0;
            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.HOADON where c.ngayDat.Year == DateTime.Now.Year && c.ngayDat.Month == DateTime.Now.Month && c.ngayDat.Day == DateTime.Now.Day && c.trangThai.Equals("Đã thanh toán") select c;

                tongHoaDonThanhToanTrongNgay = result.Count();
            }
        }

        //tong doanh thu
        public void tongDoanhThu()
        {
            tongDoanhThuTrongNgay = 0;
            using (db = new QUANLYSHOPEntities())
            {
                var result = from p in db.CTHD
                             join c in db.HOADON
                             on p.IDHD equals c.ID
                             where c.ngayDat.Year == DateTime.Now.Year && c.ngayDat.Month == DateTime.Now.Month && c.ngayDat.Day == DateTime.Now.Day && c.trangThai.Equals("Đã thanh toán")
                             select p;

                result.ToList().ForEach(p => tongDoanhThuTrongNgay = (float)(tongDoanhThuTrongNgay + p.thanhTien));
            }
        }

        //tổng sản phẩm
        public void tongSanPham()
        {
            tongSanPhamBanTrongNgay = 0;
            using (db = new QUANLYSHOPEntities())
            {
                var result = from p in db.CTHD
                             join c in db.HOADON
                             on p.IDHD equals c.ID
                             join s in db.SANPHAM
                             on p.IDSP equals s.ID
                             where c.ngayDat.Year == DateTime.Now.Year && c.ngayDat.Month == DateTime.Now.Month && c.ngayDat.Day == DateTime.Now.Day && c.trangThai.Equals("Đã thanh toán")
                             select p;

                result.ToList().ForEach(p => tongSanPhamBanTrongNgay = (tongSanPhamBanTrongNgay + p.soLuongDat).GetValueOrDefault());
            }
        }
    }
}
