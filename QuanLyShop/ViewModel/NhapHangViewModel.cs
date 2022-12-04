using Microsoft.Win32;
using QuanLyShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Windows.Documents;
using System.IO;

namespace QuanLyShop.ViewModel
{
    class NhapHangViewModel : BaseViewModel
    {
        #region thuộc tính

        public QUANLYSHOPEntities db;

        private ObservableCollection<NHAPHANG> _listNH;
        public ObservableCollection<NHAPHANG> ListNH { get => _listNH; set { _listNH = value; OnPropertyChanged(); } }

        private ObservableCollection<NHAPHANG> _listSelectedNH;
        public ObservableCollection<NHAPHANG> ListSelectedNH { get => _listSelectedNH; set { _listSelectedNH = value; OnPropertyChanged(); } }

        private ObservableCollection<NHAPHANG> _ListNHNhanVien;
        public ObservableCollection<NHAPHANG> ListNHNhanVien { get => _ListNHNhanVien; set { _ListNHNhanVien = value; OnPropertyChanged(); } }

        private ObservableCollection<NHANVIEN> _listNhanVien;
        public ObservableCollection<NHANVIEN> ListNhanVien { get => _listNhanVien; set { _listNhanVien = value; OnPropertyChanged(); } }

        private ObservableCollection<THELOAI> _listTheLoai;
        public ObservableCollection<THELOAI> ListTheLoai { get => _listTheLoai; set { _listTheLoai = value; OnPropertyChanged(); } }

        private int _iD;
        public int ID { get => _iD; set { _iD = value; OnPropertyChanged(); } }

        private int? _iDNV;
        public int? IDNV { get => _iDNV; set { _iDNV = value; OnPropertyChanged(); } }

        private string _maSP;
        public string MaSP { get => _maSP; set { _maSP = value; OnPropertyChanged(); } }

        private string _tenTL;
        public string TenTL { get => _tenTL; set { _tenTL = value; OnPropertyChanged(); } }

        private string _tenSP;
        public string TenSP { get => _tenSP; set { _tenSP = value; OnPropertyChanged(); } }

        private string _kichCo;
        public string kichCo { get => _kichCo; set { _kichCo = value; OnPropertyChanged(); } }

        private string _tenNV;
        public string TenNV { get => _tenNV; set { _tenNV = value; OnPropertyChanged(); } }

        private int? _soLuong;
        public int? SoLuong { get => _soLuong; set { _soLuong = value; OnPropertyChanged(); } }

        private float? _giaNhap;
        public float? GiaNhap { get => _giaNhap; set { _giaNhap = value; OnPropertyChanged(); } }

        private DateTime _ngayNhap;
        public DateTime NgayNhap { get => _ngayNhap; set { _ngayNhap = value; OnPropertyChanged(); } }

        private float? _tongTien;
        public float? TongTien { get => _tongTien; set { _tongTien = value; OnPropertyChanged(); } }

        private int _iDNVTimKiem;
        public int IDNVTimKiem { get => _iDNVTimKiem; set { _iDNVTimKiem = value; OnPropertyChanged(); } }


        //phân trang page hiện tại
        public int page { get; set; }

        //số dòng hiện trên 1 trang
        public int numerRecord { get; set; }

        //chọn thể loại
        private THELOAI _selectedValueTheLoai;
        public THELOAI SelectedValueTheLoai
        {
            get => _selectedValueTheLoai;
            set
            {
                _selectedValueTheLoai = value;
                OnPropertyChanged();

                if (SelectedValueTheLoai != null)
                    TenTL = SelectedValueTheLoai.tenTL;
            }
        }

        //chọn phiếu nhập hàng
        private NHAPHANG _selectedItem;
        public NHAPHANG SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    TongTien = 0;
                    ID = SelectedItem.ID;
                    IDNV = SelectedItem.IDNhanVien;
                    MaSP = SelectedItem.maSanPham;
                    TenNV = SelectedItem.NHANVIEN.hoTen;
                    kichCo = SelectedItem.size;
                    TenTL = SelectedItem.tenTheLoai;
                    TenSP = SelectedItem.tenSanPham;
                    SoLuong = SelectedItem.soLuongNhap;
                    GiaNhap = float.Parse(SelectedItem.giaNhap.ToString());
                    NgayNhap = SelectedItem.ngayNhap;
                }
            }
        }

        //các thuộc tính bắt sự kiện
        public ICommand MouseLeftButtonDowWindow { get; set; }
        public ICommand closeWindow { get; set; }
        public ICommand xoaPhieuNhapHang { get; set; }
        public ICommand timKiem { get; set; }
        public ICommand themPhieu { get; set; }
        public ICommand themPhieuWindow { get; set; }
        public ICommand textChangedSoLuong { get; set; }
        public ICommand textChangedGiaNhap { get; set; }
        public ICommand PreviewMouseLeftButtonUp { get; set; }
        public ICommand xemPhieuNhap { get; set; }
        public ICommand xuatPhieuCommand { get; set; }

        //phân trang trước
        public ICommand btTruoc { get; set; }

        //phân trang sau
        public ICommand btSau { get; set; }

        //phân trang đầu tiên
        public ICommand btDau { get; set; }

        //phân trang cuối
        public ICommand btCuoi { get; set; }

        #endregion

        #region phương thức
        public NhapHangViewModel()
        {
            //khởi tạo các biến
            NgayNhap = DateTime.Now;
            SoLuong = 0;
            GiaNhap = 0;
            page = 0;
            numerRecord = 13;

            //khởi tạo danh sách
            ListNH = new ObservableCollection<NHAPHANG>();
            ListNHNhanVien = new ObservableCollection<NHAPHANG>();
            ListNhanVien = new ObservableCollection<NHANVIEN>();
            ListTheLoai = new ObservableCollection<THELOAI>();

            //mở form thêm phiếu
            themPhieuWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IDNV = DangNhapViewModel.IDLogin;
                TenNV = DangNhapViewModel.tenLogin;
                ID = iDMax() + 1;

                reset();

                QuanLyPN ql = new QuanLyPN();
                ql.ShowDialog();
            });

            //duy chuyển window
            MouseLeftButtonDowWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });

            //đóng window
            closeWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //xóa phiếu nhập hàng
            xoaPhieuNhapHang = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                xoaPhieu(ID);
                loadNhapHang(page, numerRecord); //load phiếu nhập hàng

                TongTien = 0;

                //load lại tổng tiền của nhân viên nhập
                foreach (var item in ListNHNhanVien)
                {
                    TongTien += float.Parse(item.tongTien.ToString());
                }
            });

            //thêm phiếu nhập hàng
            themPhieu = new RelayCommand<KHACHHANG>((p) => { return true; }, (p) =>
            {
                them();
                loadNhapHang(page, numerRecord);
            });

            //tìm kiếm phiếu nhập theo ID nhân viên
            timKiem = new RelayCommand<KHACHHANG>((p) => { return true; }, (p) => { timKiemPhieuNhap(IDNVTimKiem); });

            //Khi thay đổi số lượng tổng tiền tự tính và hiển thị
            textChangedSoLuong = new RelayCommand<object>((p) => { return true; }, (p) => { TongTien = 0; TongTien = SoLuong * GiaNhap; });

            //Khi thay đổi giá nhập tổng tiền tự tính và hiển thị
            textChangedGiaNhap = new RelayCommand<object>((p) => { return true; }, (p) => { TongTien = 0; TongTien = SoLuong * GiaNhap; });

            //chọn phiếu nhập mở form thông tin phiếu
            PreviewMouseLeftButtonUp = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                selectedPhieuNhap(NgayNhap, IDNV);

                ThongTinNH tt = new ThongTinNH();
                tt.ShowDialog();
            });

            //xem tất cả phiếu nhập
            xemPhieuNhap = new RelayCommand<KHACHHANG>((p) => { return true; }, (p) => { loadNhapHang(page, numerRecord); });

            //xuất file exe phiếu nhập
            xuatPhieuCommand = new RelayCommand<NHAPHANG>((p) => { return true; }, (p) => { xuatPhieu(); });

            //phân trang sau
            btTruoc = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (page > 0)
                {
                    page--;
                    loadNhapHang(page, numerRecord);
                }
            });

            //phân trang trước
            btSau = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                //nếu trang hiện tại nhỏ hơn số trang thì cho phép sang trang tiếp
                if (page < soTrang() - 1)
                {
                    page++;
                    loadNhapHang(page, numerRecord);
                }
            });

            //phân trang Đầu
            btDau = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                page = 0;
                loadNhapHang(page, numerRecord);
            });

            //phân trang Cuối
            btCuoi = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                //nếu trang hiện tại nhỏ hơn số trang thì cho phép sang trang tiếp
                page = soTrang();
                loadNhapHang(page - 1, numerRecord);
            });

            loadNhapHang(page, numerRecord);
            loadTheLoai();
        }

        //kiểm tra size có đúng định dạng không
        public bool checkSize(string size)
        {
            if (size != null)
            {
                if (size.Equals("S") || size.Equals("M") || size.Equals("L") || size.Equals("XL") || size.Equals("XXL") ||
                    size.Equals("s") || size.Equals("m") || size.Equals("l") || size.Equals("xl") || size.Equals("xxl"))
                    return true;
            }

            return false;
        }

        //tìm ID Phiếu cao nhất để hiển thị khi thêm phiếu
        public int iDMax()
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    var result = db.NHAPHANG.Select(p => p.ID).Max();

                    return result;
                }
                catch
                {
                    return 0;
                };
            }
        }

        //load thể loại
        public void loadTheLoai()
        {
            ListTheLoai.Clear();
            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.THELOAI select c;

                result.ToList().ForEach(p => { if (p.tenTL.Equals("Tất Cả") == false) ListTheLoai.Add(p); });
            }
        }

        //load nhập hàng
        public void loadNhapHang(int page, int numerRecord)
        {
            //clear để khi load lại không hiển thị trùng
            ListNH.Clear();
            ListNhanVien.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = db.NHAPHANG.Join(db.NHANVIEN, p => p.IDNhanVien, q => q.ID, (p, q) => p)
                    .OrderBy(p => p.ID)
                    .Skip(page * numerRecord)
                    .Take(numerRecord)
                    .ToList();

                result.ForEach(p =>
                {
                    ListNH.Add(p);
                    ListNhanVien.Add(p.NHANVIEN);
                });
            }
        }

        //tính số trang phiếu nhập
        public int soTrang()
        {
            using (db = new QUANLYSHOPEntities())
            {
                var sumPage = db.NHAPHANG.Join(db.NHANVIEN, p => p.IDNhanVien, q => q.ID, (p, q) => p).Count();

                if ((sumPage % numerRecord) == 0)
                    sumPage = sumPage / numerRecord;
                else
                    sumPage = (sumPage / numerRecord) + 1;

                return sumPage;
            }
        }

        //Chọn phiếu nhập
        public void selectedPhieuNhap(DateTime date, int? IDNV)
        {
            ListNH.Clear();
            ListNHNhanVien.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = db.NHAPHANG.Join(db.NHANVIEN, p => p.IDNhanVien, q => q.ID, (p, q) => p).Where(p => p.IDNhanVien == IDNV && p.ngayNhap.Day == date.Day && p.ngayNhap.Month == date.Month && p.ngayNhap.Year == date.Year).ToList();

                result.ForEach(p =>
                {
                    ListNH.Add(p);
                    ListNhanVien.Add(p.NHANVIEN);

                    TongTien = float.Parse((TongTien + p.tongTien).ToString());
                });
            }
        }

        //thêm phiếu
        public void them()
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    if (SoLuong == 0)
                    {
                        MessageBox.Show("Số lượng nhập phải lớn hơn 0", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (checkSize(kichCo) == false)
                    {
                        MessageBox.Show("Kích cỡ bạn nhập không đúng!. Phải thuộc các kích cỡ {S, M, L, XL, XXL }", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    NHAPHANG nh = new NHAPHANG() { IDNhanVien = IDNV, maSanPham = MaSP, tenSanPham = TenSP, tenTheLoai = TenTL, giaNhap = GiaNhap, ngayNhap = NgayNhap, size = kichCo, soLuongNhap = SoLuong, tongTien = TongTien };
                    db.NHAPHANG.Add(nh);
                    db.SaveChanges();
                    ListNH.Add(nh);
                    MessageBox.Show("Thêm Phiếu Nhập Hàng Thành Công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Thêm Phiếu Nhập Hàng Không Thành Công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //xóa phiếu
        public void xoaPhieu(int ID)
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Bạn Có Thật Sự Muốn Xóa Phiếu Nhập Hàng Này Không?", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (result == MessageBoxResult.OK)
                    {
                        var NH = db.NHAPHANG.Find(ID);
                        db.NHAPHANG.Remove(NH);
                        db.SaveChanges();
                        MessageBox.Show("Xóa Phiếu Nhập Hàng Thành Công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Xóa Phiếu Nhập Hàng Không Thành Công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //Tìm kiếm phiếu nhập hàng theo ID nhân viên
        public void timKiemPhieuNhap(int IDNVTimKiem)
        {
            ListNH.Clear();
            using (db = new QUANLYSHOPEntities())
            {
                ListNH.Clear();
                ListNHNhanVien.Clear();

                using (db = new QUANLYSHOPEntities())
                {
                    var result = db.NHAPHANG.Join(db.NHANVIEN, p => p.IDNhanVien, q => q.ID, (p, q) => p).Where(p => p.IDNhanVien == IDNVTimKiem).ToList();

                    result.ForEach(p =>
                    {
                        ListNH.Add(p);
                        ListNhanVien.Add(p.NHANVIEN);
                    });
                }
            }
        }

        //xuất file exe phiếu nhập
        public void xuatPhieu()
        {
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();

            // chỉ lọc ra các file có định dạng Excel
            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu tên đường dẫn
            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }

            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn không hợp lệ");
                return;
            }

            //Không phải doanh nghiệp tránh bản quyền
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage p = new ExcelPackage())
            {
                try
                {
                    // đặt tên người tạo file
                    p.Workbook.Properties.Author = "Nguyễn Quốc Anh";

                    // đặt tiêu đề cho file
                    p.Workbook.Properties.Title = "Báo cáo thống kê";

                    //Tạo một sheet để làm việc trên đó
                    p.Workbook.Worksheets.Add("CTDV");

                    // lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = p.Workbook.Worksheets[0];

                    // đặt tên cho sheet
                    ws.Name = "CTNH";
                    // fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 11;
                    // font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";

                    // Tạo danh sách các column header
                    string[] arrColumnHeader = { "Tên Nhân Viên", "Mã Hàng", "Số Lượng", "Giá NHập", "Ngày Nhập", "Tổng Tiền" };

                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    var countColHeader = arrColumnHeader.Count();

                    // merge gộp các column lại từ column 1 đến số column header
                    // đặt tên cho bảng excel
                    ws.Cells[1, 1].Value = "Thông Kê Nhập Hàng";
                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                    // in đậm
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                    // căn giữa
                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int colIndex = 1;
                    int rowIndex = 2;

                    //tạo các cột excel từ mảng cột đã tạo từ bên trên
                    foreach (var item in arrColumnHeader)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];

                        //set màu thành gray
                        var fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                        //căn chỉnh các border
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                        border.Top.Style =
                        border.Left.Style =
                        border.Right.Style = ExcelBorderStyle.Thin;

                        //gán giá trị
                        cell.Value = item;

                        colIndex++;
                    }

                    // lấy ra danh sách CTDV từ ItemSource của ListView
                    if (ListNH.Count > 0)
                    {
                        double? sum = 0.0d;

                        List<NHAPHANG> listCTNH = ListNH.Cast<NHAPHANG>().ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in listCTNH)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell
                            ws.Cells[rowIndex, colIndex++].Value = item.NHANVIEN.hoTen;

                            ws.Cells[rowIndex, colIndex++].Value = item.maSanPham;

                            ws.Cells[rowIndex, colIndex++].Value = item.soLuongNhap;

                            ws.Cells[rowIndex, colIndex++].Value = item.giaNhap;

                            ws.Cells[rowIndex, colIndex++].Value = item.ngayNhap.ToShortDateString();

                            ws.Cells[rowIndex, colIndex++].Value = item.tongTien;
                        }

                        //Lưu file lại
                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    MessageBox.Show("Xuất File Excel Thành Công!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Lỗi Xuất File!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        //reset thông tin phiếu
        public void reset()
        {
            MaSP = null;
            TenSP = null;
            SelectedValueTheLoai = null;
            SoLuong = 0;
            kichCo = null;
            GiaNhap = 0;
            NgayNhap = DateTime.Now;
            TongTien = 0;
        }
        #endregion
    }
}
