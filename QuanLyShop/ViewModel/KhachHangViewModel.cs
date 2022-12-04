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

namespace QuanLyShop.ViewModel
{
    class KhachHangViewModel : BaseViewModel
    {
        #region thuộc tính
        public QUANLYSHOPEntities db;

        private int _iDTimKiem;
        public int IDTimKiem { get => _iDTimKiem; set { _iDTimKiem = value; OnPropertyChanged(); } }

        private int _iD;
        public int ID { get => _iD; set { _iD = value; OnPropertyChanged(); } }

        private string _hoTen;
        public string HoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }

        private string _gioiTinh;
        public string GioiTinh { get => _gioiTinh; set { _gioiTinh = value; OnPropertyChanged(); } }

        private string _diaChi;
        public string DiaChi { get => _diaChi; set { _diaChi = value; OnPropertyChanged(); } }

        private DateTime? _ngaySinh;
        public DateTime? NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }

        private string _sDT;
        public string SDT { get => _sDT; set { _sDT = value; OnPropertyChanged(); } }

        private string _email;
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }

        private string _avatar;
        public string Avatar { get => _avatar; set { _avatar = value; OnPropertyChanged(); } }

        private string _ranks;
        public string Ranks { get => _ranks; set { _ranks = value; OnPropertyChanged(); } }

        private int _selecteIndex;
        public int SelecteIndex { get => _selecteIndex; set { _selecteIndex = value; ; OnPropertyChanged(); } }

        public bool CheckGioiTinhNam { get; set; }
        public bool CheckGioiTinhNu { get; set; }
        public bool isEnabled { get; set; }

        //phân trang page hiện tại
        public int page { get; set; }

        //số dòng hiện trên 1 trang
        public int numerRecord { get; set; }


        private ObservableCollection<KHACHHANG> _listKH;
        public ObservableCollection<KHACHHANG> ListKH { get => _listKH; set { _listKH = value; OnPropertyChanged(); } }

        private KHACHHANG _selectedItem;
        public KHACHHANG SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    ID = SelectedItem.ID;
                    HoTen = SelectedItem.hoTen;
                    GioiTinh = SelectedItem.gioiTinh;
                    NgaySinh = SelectedItem.ngaySinh;
                    SDT = SelectedItem.SDT;
                    Email = SelectedItem.email;
                    Avatar = SelectedItem.avatar;
                    Ranks = SelectedItem.ranks;
                    DiaChi = SelectedItem.diaChi;

                    switch (Ranks)
                    {
                        case "Đồng":
                            SelecteIndex = 0;
                            break;
                        case "Bạc":
                            SelecteIndex = 1;
                            break;
                        case "Vàng":
                            SelecteIndex = 2;
                            break;
                        case "Kim Cương":
                            SelecteIndex = 3;
                            break;
                        case "Cao Thủ":
                            SelecteIndex = 4;
                            break;
                        default:
                            SelecteIndex = 5;
                            break;
                    }

                    if (GioiTinh.Equals("Nam"))
                        CheckGioiTinhNam = true;
                    else
                        CheckGioiTinhNam = false;

                    if (GioiTinh.Equals("Nữ"))
                        CheckGioiTinhNu = true;
                    else
                        CheckGioiTinhNu = false;

                    visibilityUpdateDelec = Visibility.Visible;
                    visibility = Visibility.Hidden;
                    isEnabled = false;
                }
            }
        }

        public ICommand MouseLeftButtonDowWindow { get; set; }
        public ICommand closeWindow { get; set; }
        public ICommand openQuanLy { get; set; }
        public ICommand timKiem { get; set; }
        public ICommand selectionChanged { get; set; }
        public ICommand xoaKH { get; set; }
        public ICommand openFolder { get; set; }
        public ICommand themKH { get; set; }
        public ICommand suaKH { get; set; }
        public ICommand checkedNam { get; set; }
        public ICommand checkedNu { get; set; }
        public ICommand passwordChanged { get; set; }
        public ICommand xemKH { get; set; }
        public ICommand PreviewMouseLeftButtonUp { get; set; }

        //phân trang trước
        public ICommand btTruoc { get; set; }

        //phân trang sau
        public ICommand btSau { get; set; }

        //phân trang đầu tiên
        public ICommand btDau { get; set; }

        //phân trang cuối
        public ICommand btCuoi { get; set; }

        public Visibility visibility { get; set; }
        public Visibility visibilityUpdateDelec { get; set; }
        #endregion

        #region phương thức
        public KhachHangViewModel()
        {
            page = 0;
            numerRecord = 7;

            visibility = Visibility.Hidden;

            //duy chuyển window
            MouseLeftButtonDowWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });

            //mở form quản lý
            openQuanLy = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                visibility = Visibility.Visible;
                visibilityUpdateDelec = Visibility.Hidden;
                isEnabled = true;

                ID = iDMax() + 1;
                HoTen = null;
                GioiTinh = null;
                DiaChi = null;
                NgaySinh = null;
                Avatar = null;
                SDT = null;
                Email = null;
                SelecteIndex = -1;
                Ranks = null;
                CheckGioiTinhNam = false;
                CheckGioiTinhNu = false;

                QuanLyKH wd = new QuanLyKH();
                wd.ShowDialog();
            });

            //đóng form
            closeWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //xem toàn bộ khách hàng
            xemKH = new RelayCommand<KHACHHANG>((p) => { return true; }, (p) =>
            {
                loadKhachHang(page, numerRecord);
            });

            //tìm kiếm khách hàng theo IDD
            timKiem = new RelayCommand<KHACHHANG>((p) => { return true; }, (p) =>
            {
                ListKH.Clear();
                using (db = new QUANLYSHOPEntities())
                {
                    var KH = db.KHACHHANG.Find(IDTimKiem);
                    ListKH.Add(KH);
                }
            });

            //xóa khách hàng
            xoaKH = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                using (db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        var KH = db.KHACHHANG.Find(ID);
                        MessageBoxResult result = MessageBox.Show("Bạn Có Thật Sự Muốn Xóa Khách Hàng Này Không?", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                        if (result == MessageBoxResult.OK)
                        {
                            db.KHACHHANG.Remove(KH);
                            db.SaveChanges();
                            loadKhachHang(page, numerRecord);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Xóa Khách Hàng Thất Bại! Vui lòng kiểm tra lại khách hàng đang có đơn hàng", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    };
                }
            });

            //thêm khách hàng
            themKH = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                using (db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        if (HoTen != null && GioiTinh != null && NgaySinh != null && DiaChi != null && SDT != null)
                        {
                            if (checkEmail(Email))
                            {
                                var kh = new KHACHHANG() { hoTen = HoTen, gioiTinh = GioiTinh, ngaySinh = NgaySinh, SDT = SDT, email = Email, diaChi = DiaChi, avatar = Avatar, ranks = Ranks };
                                db.KHACHHANG.Add(kh);
                                db.SaveChanges();
                                ListKH.Add(kh);
                            }
                            else
                                MessageBox.Show("Email không đúng định dạng! Vui lòng bao gồm @ trong email", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                            MessageBox.Show("Thông Tin Không Được Phép Trống", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch
                    {
                        MessageBox.Show("Thêm Khách Hàng Thất Bại", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    };
                }

            });

            //sửa thông tin khách hàng
            suaKH = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                using (db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        var kh = db.KHACHHANG.Find(ID);
                        if (kh != null)
                        {
                            kh.ID = ID;
                            kh.hoTen = HoTen;
                            kh.gioiTinh = GioiTinh;
                            kh.ngaySinh = NgaySinh;
                            kh.SDT = SDT;
                            kh.email = Email;
                            kh.diaChi = DiaChi;
                            kh.avatar = Avatar;
                            kh.ranks = Ranks;
                            db.SaveChanges();
                            loadKhachHang(page, numerRecord);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Sửa Khách Hàng Thất Bại", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    };
                }
            });

            //kiểm tra nam checkboxNam = true
            checkedNam = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                GioiTinh = "Nam";
            });

            //kiểm tra nư
            checkedNu = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                GioiTinh = "Nữ";
            });

            //chọn combobox hạng
            selectionChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                switch (SelecteIndex)
                {
                    case 0:
                        Ranks = "Đồng";
                        break;
                    case 1:
                        Ranks = "Bạc";
                        break;
                    case 2:
                        Ranks = "Vàng";
                        break;
                    case 3:
                        Ranks = "Kim Cương";
                        break;
                    case 4:
                        Ranks = "Cao Thủ";
                        break;
                    default:
                        Ranks = "Chưa Có Rank";
                        break;
                }
            });

            //mở form quản lý khách hàng
            PreviewMouseLeftButtonUp = new RelayCommand<string>((p) => { return true; }, (p) => { QuanLyKH wd = new QuanLyKH(); wd.ShowDialog(); });

            openFolder = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                Avatar = loadPath();
            });

            //phân trang trước
            btTruoc = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (page > 0)
                {
                    page--;
                    loadKhachHang(page, numerRecord);
                }
            });

            //phân trang sau
            btSau = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                //nếu trang hiện tại nhỏ hơn số trang thì cho phép sang trang tiếp
                if (page < soTrang() - 1)
                {
                    page++;
                    loadKhachHang(page, numerRecord);
                }
            });

            //phân trang Đầu
            btDau = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                page = 0;
                loadKhachHang(page, numerRecord);
            });

            //phân trang Cuối
            btCuoi = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                //nếu trang hiện tại nhỏ hơn số trang thì cho phép sang trang tiếp
                page = soTrang();
                loadKhachHang(page - 1, numerRecord);
            });

            loadKhachHang(page, numerRecord);
        }

        //tìm ID Phiếu cao nhất để hiển thị khi thêm phiếu
        public int iDMax()
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    var result = db.KHACHHANG.Select(p => p.ID).Max();

                    return result;
                }
                catch
                {
                    return 0;
                }
            }
        }

        //load khách hàng
        public void loadKhachHang(int page, int numerRecord)
        {
            using (db = new QUANLYSHOPEntities())
            {
                //skip bỏ qua các phần tử được chọn bắt đầu phần tử đầu tiên
                //Take lấy ra số phần tử là 7

                ListKH = new ObservableCollection<KHACHHANG>(db.KHACHHANG.OrderBy(p => p.ID).Skip(page * numerRecord).Take(numerRecord));
            }
        }

        //tính số trang
        public int soTrang()
        {
            using (db = new QUANLYSHOPEntities())
            {
                int sumPage = db.KHACHHANG.Count();

                //nếu số dòng chia số dòng hiển thị ra lẻ thì + 1 trang
                if ((sumPage % numerRecord) == 0)
                    sumPage = sumPage / numerRecord;
                else
                    sumPage = (sumPage / numerRecord) + 1;

                return sumPage;
            }
        }

        //kiểm tra định dạng email
        public bool checkEmail(string email)
        {
            for (int i = 0; i < email.Length; i++)
            {
                if (email[i].CompareTo('@') == 0)
                    return true;
            }
            return false;
        }

        //mở hộp thoại
        public string loadPath()
        {
            string url = "";

            OpenFileDialog file = new OpenFileDialog();

            if (file.ShowDialog() == true)
            {
                url = file.FileName;
            }
            else
            {
                MessageBox.Show("Đường Dẫn Không Hợp Lệ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return url;
        }

        #endregion
    }
}
