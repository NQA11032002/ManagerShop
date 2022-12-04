using LiveCharts;
using LiveCharts.Wpf;
using QuanLyShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyShop.ViewModel
{
    class HoaDonViewModel : BaseViewModel
    {
        #region phương thức
        public QUANLYSHOPEntities db;

        private ObservableCollection<HOADON> _listHoaDon;
        public ObservableCollection<HOADON> ListHoaDon { get => _listHoaDon; set { _listHoaDon = value; OnPropertyChanged(); } }

        private ObservableCollection<CTHD> _listCTHD;
        public ObservableCollection<CTHD> ListCTHD { get => _listCTHD; set { _listCTHD = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACHHANG> _listKH;
        public ObservableCollection<KHACHHANG> ListKH { get => _listKH; set { _listKH = value; OnPropertyChanged(); } }

        private ObservableCollection<NHANVIEN> _listNV;
        public ObservableCollection<NHANVIEN> ListNV { get => _listNV; set { _listNV = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _listSP;
        public ObservableCollection<SANPHAM> ListSP { get => _listSP; set { _listSP = value; OnPropertyChanged(); } }

        private int _iD;
        public int ID { get => _iD; set { _iD = value; OnPropertyChanged(); } }

        private int _iDSP;
        public int IDSP { get => _iDSP; set { _iDSP = value; OnPropertyChanged(); } }

        private int _iDCTHD;
        public int IDCTHD { get => _iDCTHD; set { _iDCTHD = value; OnPropertyChanged(); } }

        private int? _iDNV;
        public int? IDNV { get => _iDNV; set { _iDNV = value; OnPropertyChanged(); } }

        private int? _iDKH;
        public int? IDKH { get => _iDKH; set { _iDKH = value; OnPropertyChanged(); } }

        private DateTime _ngayDat;
        public DateTime NgayDat { get => _ngayDat; set { _ngayDat = value; OnPropertyChanged(); } }

        private int? _soLuong;
        public int? SoLuong { get => _soLuong; set { _soLuong = value; OnPropertyChanged(); } }

        private float _tongTien;
        public float TongTien { get => _tongTien; set { _tongTien = value; OnPropertyChanged(); } }

        private float _thanhTien;
        public float ThanhTien { get => _thanhTien; set { _thanhTien = value; OnPropertyChanged(); } }

        private float _donGia;
        public float DonGia { get => _donGia; set { _donGia = value; OnPropertyChanged(); } }

        private int _iDHDThem;
        public int IDHDThem { get => _iDHDThem; set { _iDHDThem = value; OnPropertyChanged(); } }

        public string tenNV { get; set; }
        public string tenKH { get; set; }
        public string tenSP { get; set; }
        public int TimKiemHD { get; set; }
        public bool checkPhanTrangHD { get; set; }
        public string statusHD { get; set; }


        //phân trang page hiện tại
        public int page { get; set; }

        //số dòng hiện trên 1 trang
        public int numerRecord { get; set; }

        //chọn hóa đơn lấy ra các thuộc tính

        private HOADON _selectedItem;
        public HOADON SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    ID = SelectedItem.ID;
                    IDNV = SelectedItem.IDNhanVien;
                    IDKH = SelectedItem.IDKhachHang;
                    NgayDat = SelectedItem.ngayDat;
                    tenNV = SelectedItem.NHANVIEN.hoTen;
                    tenKH = SelectedItem.KHACHHANG.hoTen;

                    TongTien = 0;
                    loadCTHD(ID);
                }
            }
        }

        //chọn chi tiêt hóa đơn lưu các giá trị
        private CTHD _selectedItemCTHD;
        public CTHD SelectedItemCTHD
        {
            get => _selectedItemCTHD;
            set
            {
                _selectedItemCTHD = value;
                OnPropertyChanged();

                if (SelectedItemCTHD != null)
                {
                    IDCTHD = SelectedItemCTHD.ID;
                }
            }
        }

        //chọn khách hàng lưu giá trị
        private KHACHHANG _selectedItemKH;
        public KHACHHANG SelectedItemKH
        {
            get => _selectedItemKH;
            set
            {
                _selectedItemKH = value;
                OnPropertyChanged();

                if (SelectedItemKH != null)
                    IDKH = SelectedItemKH.ID;

            }
        }

        //chọn sản phẩm lưu giá trị
        private SANPHAM _selectedItemSP;
        public SANPHAM SelectedItemSP
        {
            get => _selectedItemSP;
            set
            {
                _selectedItemSP = value;
                OnPropertyChanged();

                if (SelectedItemSP != null)
                {
                    ThanhTien = 0;
                    IDSP = SelectedItemSP.ID;
                    DonGia = float.Parse(SelectedItemSP.giaSP.ToString());

                    //thành tiền của số lượng sản phẩm mua
                    if (SoLuong != null)
                        ThanhTien = (float)(SoLuong * DonGia);
                }
            }
        }

        public ICommand closeWindow { get; set; }
        public ICommand PreviewMouseLeftButtonUp { get; set; }
        public ICommand xoaHD { get; set; }
        public ICommand xoaCT { get; set; }
        public ICommand themCTSP { get; set; }
        public ICommand themHD { get; set; }
        public ICommand timKiemID { get; set; }
        public ICommand xemHD { get; set; }
        public ICommand xemHDDaThanhToan { get; set; }
        public ICommand xemHDChuaThanhToan { get; set; }
        public ICommand taoHD { get; set; }
        public ICommand thanhToanHD { get; set; }
        public ICommand textChangedSoLuong { get; set; }
        public ICommand MouseLeftButtonDowWindow { get; set; }
        public ICommand xoaTatCaHD { get; set; }

        //phân trang trước
        public ICommand btTruoc { get; set; }

        //phân trang sau
        public ICommand btSau { get; set; }

        //phân trang đầu tiên
        public ICommand btDau { get; set; }

        //phân trang cuối
        public ICommand btCuoi { get; set; }

        #endregion

        public HoaDonViewModel()
        {
            ListHoaDon = new ObservableCollection<HOADON>();
            ListCTHD = new ObservableCollection<CTHD>();
            ListKH = new ObservableCollection<KHACHHANG>();
            ListSP = new ObservableCollection<SANPHAM>();
            ListNV = new ObservableCollection<NHANVIEN>();

            page = 0;
            numerRecord = 7;

            //bằng true phân trang theo load tất cả hoad đơn ngược lại load theo trạng thái hóa đơn
            checkPhanTrangHD = true;

            //duy chuyển window
            MouseLeftButtonDowWindow = new RelayCommand<Window>((p) => { return true; }, (p) => { p.DragMove(); });

            //đóng window
            closeWindow = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

            //click mở thông tin hóa đơn
            PreviewMouseLeftButtonUp = new RelayCommand<object>((p) => { return true; }, (p) => { ThongTinHD tt = new ThongTinHD(); tt.ShowDialog(); });

            //xóa hóa đơn
            xoaHD = new RelayCommand<object>((p) => { return true; }, (p) => { xoa(ID); loadHoaDon(page, numerRecord); IDHDThem = IDHDThem - 1; });

            //xóa tất cả hóa đơn
            xoaTatCaHD = new RelayCommand<object>((p) => true, (p) => { xoaToanBoHoaDon(); loadHoaDon(page, numerRecord); });

            //xóa chi tiết hóa đơn
            xoaCT = new RelayCommand<object>((p) => { return true; }, (p) => { xoaCTSP(IDCTHD); loadCTHD(ID); });

            //thêm hóa đơn
            themHD = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                themHoaDon();

                //lấy ra giá trị vừa thêm
                IDHDThem = idHoaDonMax();
            });

            //thêm chi tiết sản phẩm
            themCTSP = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                themCTHD(IDHDThem);

                //load sản phẩm chưa thanh toán
                loadCTHD(IDHDThem);
            });

            //tìm kiếm hóa đơn theo ID
            timKiemID = new RelayCommand<object>((p) => { return true; }, (p) => { timKiem(TimKiemHD); });

            //load hóa đơn
            xemHD = new RelayCommand<object>((p) => { return true; }, (p) => { loadHoaDon(page, numerRecord); checkPhanTrangHD = true; });

            //load hóa đơn đã thanh toán
            xemHDDaThanhToan = new RelayCommand<object>((p) => { return true; }, (p) => { loadHoaDon(page, numerRecord, "Đã Thanh Toán"); checkPhanTrangHD = false; statusHD = "Đã Thanh Toán"; });

            //load hóa đơn chưa thanh toán
            xemHDChuaThanhToan = new RelayCommand<object>((p) => { return true; }, (p) => { loadHoaDon(page, numerRecord, "Chưa Thanh Toán"); checkPhanTrangHD = false; statusHD = "Chưa Thanh Toán"; });

            //mở form quản lý hóa đơn
            taoHD = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListCTHD.Clear();

                loadSP();
                loadKH();

                //reset thông tin hóa đơn
                reset();

                QuanLyHD ql = new QuanLyHD();
                ql.ShowDialog();

                //sau khi đóng form quản lý load lại hóa đơn
                loadHoaDon(page, numerRecord);
            });

            thanhToanHD = new RelayCommand<object>((p) => { return true; }, (p) => { thanhToan(ID); });

            //khi thay đổi số lượng thành tiền tự động cập nhật
            textChangedSoLuong = new RelayCommand<object>((p) => { return true; }, (p) => { ThanhTien = (float)(SoLuong * DonGia); });

            //phân trang sau
            btTruoc = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if(checkPhanTrangHD)
                {
                    if (page > 0)
                    {
                        page--;
                        loadHoaDon(page, numerRecord);
                    }
                }    
                else
                {
                    if (page > 0)
                    {
                        page--;
                        loadHoaDon(page, numerRecord, statusHD);
                    }
                }
            });

            //phân trang trước
            btSau = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if(checkPhanTrangHD)
                {
                    //nếu trang hiện tại nhỏ hơn số trang thì cho phép sang trang tiếp
                    if (page < soTrang() - 1)
                    {
                        page++;
                        loadHoaDon(page, numerRecord);
                    }
                }
                else
                {
                    //nếu trang hiện tại nhỏ hơn số trang thì cho phép sang trang tiếp
                    if (page < soTrang(statusHD) - 1)
                    {
                        page++;
                        loadHoaDon(page, numerRecord,statusHD);
                    }
                }    
            });

            //phân trang Đầu
            btDau = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (checkPhanTrangHD)
                {
                    page = 0;
                    loadHoaDon(page, numerRecord);
                }
                else
                {
                    page = 0;
                    loadHoaDon(page, numerRecord, statusHD);
                }    
            });

            //phân trang Cuối
            btCuoi = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                //nếu trang hiện tại nhỏ hơn số trang thì cho phép sang trang tiếp
                if(checkPhanTrangHD)
                {
                    //tính số trang tất cả sản phẩm
                    page = soTrang() - 1;
                    loadHoaDon(page, numerRecord);
                }   
                else
                {
                    //tính số trang theo trạng thái
                    page = soTrang(statusHD) - 1;
                    loadHoaDon(page, numerRecord, statusHD);
                }

            });

            loadHoaDon(page,numerRecord);
            loadSP();

        }

        //load sản phẩm
        public void loadSP()
        {
            ListSP.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.SANPHAM select c;


                result.ToList().ForEach(p =>
                {
                    ListSP.Add(p);
                });
            }
        }

        //load khách hàng
        public void loadKH()
        {
            ListKH.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.KHACHHANG select c;

                result.ToList().ForEach(p =>
                {
                    ListKH.Add(p);
                });
            }
        }

        //load hóa đơn
        public void loadHoaDon(int page, int numerRecord)
        {
            ListNV.Clear();
            ListKH.Clear();
            ListHoaDon.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = db.HOADON.Join(db.KHACHHANG, hd => hd.IDKhachHang, kh => kh.ID, (hd, kh) => hd).Join(db.NHANVIEN, hd => hd.IDNhanVien, nv => nv.ID, (hd, nv) => hd).OrderBy(p => p.ID).Skip(page * numerRecord).Take(numerRecord).ToList();

                result.ForEach(p =>
                {
                    ListNV.Add(p.NHANVIEN);
                    ListKH.Add(p.KHACHHANG);
                    ListHoaDon.Add(p);
                });
            }
        }

        //load hóa đơn theo trạng thái
        public void loadHoaDon(int page, int numerRecord, string status)
        {
            ListNV.Clear();
            ListKH.Clear();
            ListHoaDon.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = db.HOADON.Join(db.KHACHHANG, hd => hd.IDKhachHang, kh => kh.ID, (hd, kh) => hd).Join(db.NHANVIEN, hd => hd.IDNhanVien, nv => nv.ID, (hd, nv) => hd).OrderBy(p => p.ID).Where(p => p.trangThai.Equals(status)).Skip(page * numerRecord).Take(numerRecord).ToList();

                result.ForEach(p =>
                {
                    ListNV.Add(p.NHANVIEN);
                    ListKH.Add(p.KHACHHANG);
                    ListHoaDon.Add(p);
                });
            }
        }

        //load CTHD sản phẩm theo trạng thái hóa đơn
        public void loadCTHD(int id)
        {
            ListCTHD.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = db.CTHD.Join(db.HOADON, ct => ct.IDHD, hd => hd.ID, (ct, hd) => ct)
                    .Join(db.SANPHAM, ct => ct.IDSP, sp => sp.ID, (ct, sp) => ct)
                    .Where(ct => ct.IDHD == id).ToList();

                result.ForEach(p =>
                {
                    ListSP.Add(p.SANPHAM);
                    ListCTHD.Add(p);

                    TongTien = float.Parse((TongTien + p.thanhTien).ToString());
                });
            }
        }

        //xóa hóa đơn
        public void xoa(int id)
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có thật sự muốn xóa hóa đơn này không?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (result == MessageBoxResult.OK)
                    {
                        //xóa các chi tiết hóa đơn
                        xoaCTHD(id);

                        //xóa hóa đơn
                        var hd = db.HOADON.Find(id);

                        db.HOADON.Remove(hd);
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show("Xóa hóa đơn không thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //xóa tất cả hóa đơn
        public void xoaToanBoHoaDon()
        {
            using (db = new QUANLYSHOPEntities())
            {

                MessageBoxResult result = MessageBox.Show("Bạn có thật sự muốn xóa tất cả hóa đơn", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    var resultQuery = from c in db.HOADON select c;

                    //lặp lấy ra từng hóa đơn
                    foreach (var item in resultQuery)
                    {
                        //xóa các chi tiết hóa đơn trong hóa đơn
                        xoaCTHD(item.ID);

                        //xóa hóa đơn
                        var hd = db.HOADON.Find(item.ID);
                        db.HOADON.Remove(hd);
                        db.SaveChanges();

                    }

                }

            }
        }

        //xóa tất cả chi tiết sản phẩm trong hóa đơn
        public void xoaCTHD(int id)
        {
            db = new QUANLYSHOPEntities();
            
            try
            {
                var hd = from c in db.CTHD
                         join p in db.HOADON
                         on c.IDHD equals p.ID
                         where c.IDHD == id
                         select c;

                hd.ToList().ForEach(p =>
                {
                    var result = db.CTHD.Find(p.ID);
                    db.CTHD.Remove(result);
                    db.SaveChanges();
                });
            }
            catch
            {
            }
        }

        //xóa chi tiết sản phẩm được chọn trong tạo hóa đơn
        public void xoaCTSP(int id)
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có thật sự muốn xóa sản phẩm này không?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (result == MessageBoxResult.OK)
                    {
                        var ct = db.CTHD.Find(id);

                        db.CTHD.Remove(ct);
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show("Xóa sản phẩm không thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //thêm hóa đơn
        public void themHoaDon()
        {
            ListHoaDon.Clear();
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    if (IDKH == null || IDNV == null)
                    {
                        MessageBox.Show("Không được để trống thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    HOADON hd = new HOADON() { IDKhachHang = IDKH, IDNhanVien = IDNV, ngayDat = NgayDat, trangThai = "Chưa thanh toán" };
                    db.HOADON.Add(hd);
                    db.SaveChanges();
                    ListHoaDon.Add(hd);

                    ID = hd.ID;
                }
                catch { };
            }
        }

        //tăng điểm tích lũy mỗi khi mua hàng
        public void capNhatDiemTichLuyKhachHang(int? id)
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    var kh = db.KHACHHANG.Find(id);
                    if (kh.diemTL == null)
                    {
                        kh.diemTL = 0;
                        kh.diemTL = kh.diemTL + 1;   
                    }
                    else
                    {
                        kh.diemTL = kh.diemTL + 1;
                    }

                    //cập nhật rank theo điểm tích lũy
                    if (kh.diemTL >= 0 && kh.diemTL <= 10)
                    {
                        kh.ranks = "Đồng";
                    }
                    else
                    {
                        if (kh.diemTL <= 20)
                            kh.ranks = "Bạc";
                        else
                            if (kh.diemTL <= 30)
                            kh.ranks = "Vàng";
                        else
                            if (kh.diemTL <= 40)
                            kh.ranks = "Kim Cương";
                        else
                            if(kh.diemTL > 40)
                                kh.ranks = "Cao Thủ";
                    }

                    db.SaveChanges();
                }
                catch { };
            }
        }

        //thêm chi tiết sản phẩm trong hóa đơn
        public void themCTHD(int id)
        {
            ListCTHD.Clear();
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    if (SelectedItemSP == null)
                    {
                        MessageBox.Show("Vui lòng chọn sản phẩm!!!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        if (SoLuong <= 0)
                        {
                            MessageBox.Show("Số lượng sản phẩm phải lớn hơn 0!!!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            CTHD ct = new CTHD() { IDHD = id, IDSP = IDSP, soLuongDat = SoLuong, thanhTien = ThanhTien };
                            db.CTHD.Add(ct);
                            db.SaveChanges();
                            ListCTHD.Add(ct);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Hóa đơn không tồn tại. Cần tạo hóa đơn trước!!!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                };
            }
        }

        //tìm hóa đơn theo id
        public void timKiem(int id)
        {
            ListHoaDon.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.HOADON
                             join k in db.KHACHHANG
                             on c.IDKhachHang equals k.ID
                             join n in db.NHANVIEN
                             on c.IDNhanVien equals n.ID
                             where c.ID == id
                             select new
                             {
                                 IDHD = c.ID,
                                 IDNV = c.IDNhanVien,
                                 IDKH = c.IDKhachHang,
                                 tenNV = n.hoTen,
                                 tenKH = k.hoTen,
                                 ngayDat = c.ngayDat,
                             };

                result.ToList().ForEach(p =>
                {
                    KHACHHANG kh = new KHACHHANG() { ID = p.IDKH.GetValueOrDefault(), hoTen = p.tenKH };
                    NHANVIEN nv = new NHANVIEN() { ID = p.IDNV.GetValueOrDefault(), hoTen = p.tenNV };
                    HOADON hd = new HOADON() { ID = p.IDHD, IDKhachHang = p.IDKH, IDNhanVien = p.IDNV, ngayDat = p.ngayDat, KHACHHANG = kh, NHANVIEN = nv };
                    ListHoaDon.Add(hd);
                });
            }
        }

        //thanh toán hóa đơn
        public void thanhToan(int id)
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    var checkHD = from ct in db.CTHD
                                  join hd in db.HOADON
                                  on ct.IDHD equals hd.ID
                                  where hd.ID == id
                                  select hd;

                    //Kiểm tra hóa đơn có sản phẩm hay không
                    if (checkHD.Count() > 0)
                    {
                        MessageBoxResult result = MessageBox.Show("Bạn muốn thanh toán hóa đơn này?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                        if (result == MessageBoxResult.OK)
                        {
                            var hd = db.HOADON.Find(id);
                            hd.trangThai = "Đã thanh toán";
                            db.SaveChanges();

                            //mỗi lần khách hàng mua hàng và được thanh toán hóa đơn tăng 1 điểm tích lũy
                            capNhatDiemTichLuyKhachHang(hd.IDKhachHang);

                            //thanh toán thành công thì xóa các chi tiết giá đơn
                            ListCTHD.Clear();
                        }
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Không thể thanh toán hóa đơn trống!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch { };
            }
        }

        //tìm ID của hóa đơn lớn nhất
        public int idHoaDonMax()
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    var result = (from c in db.HOADON select c.ID).Max();

                    return result;
                }
                catch
                {
                    return 0;
                };
            }
        }

        //tính số trang
        public int soTrang()
        {
            using (db = new QUANLYSHOPEntities())
            {
                int sumPage = db.HOADON.Count();

                if ((sumPage % numerRecord) == 0)
                    sumPage = sumPage / numerRecord;
                else
                    sumPage = (sumPage / numerRecord) + 1;

                return sumPage;
            }
        }

        //tính số trang theo trạng Thái
        public int soTrang(string status)
        {
            using (db = new QUANLYSHOPEntities())
            {
                int sumPage = db.HOADON.Where(p => p.trangThai.Equals(status)).Count();

                if((sumPage % numerRecord) == 0)
                    sumPage = sumPage / numerRecord;
                else
                    sumPage = (sumPage / numerRecord) + 1;


                return sumPage;
            }
        }

        //reset thông tin hóa đơn
        public void reset()
        {
            IDHDThem = 0;
            IDNV = DangNhapViewModel.IDLogin;
            tenNV = DangNhapViewModel.tenLogin;
            IDKH = null;
            SelectedItem = null;
            SelectedItemSP = null;
            SelectedItemKH = null;
            NgayDat = DateTime.Now;
            DonGia = 0;
            SoLuong = 0;
            ThanhTien = 0;
        }

    }
}
