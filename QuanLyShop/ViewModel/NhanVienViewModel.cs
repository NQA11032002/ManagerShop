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
    class NhanVienViewModel : BaseViewModel
    {
        #region thuộc tính

        public QUANLYSHOPEntities db;

        private ObservableCollection<NHANVIEN> _listNV;
        public ObservableCollection<NHANVIEN> ListNV { get => _listNV; set { _listNV = value; OnPropertyChanged(); } }

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

        private string _taiKhoan;
        public string TaiKhoan { get => _taiKhoan; set { _taiKhoan = value; OnPropertyChanged(); } }

        private string _matKhau;
        public string MatKhau { get => _matKhau; set { _matKhau = value; OnPropertyChanged(); } }

        private string _chucVu;
        public string ChucVu { get => _chucVu; set { _chucVu = value; OnPropertyChanged(); } }

        private int _selecteIndex;
        public int SelecteIndex { get => _selecteIndex; set { _selecteIndex = value; ; OnPropertyChanged(); } }

        public bool CheckGioiTinhNam { get; set; }
        public bool CheckGioiTinhNu { get; set; }

        //lấy ra các thuộc tính đã chọn

        private NHANVIEN _selectedItem;
        public NHANVIEN SelectedItem
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
                    DiaChi = SelectedItem.diaChi;
                    ChucVu = SelectedItem.chucVu;

                    if(ChucVu != null)
                    {
                        if (ChucVu.Equals("Quản Lý"))
                            SelecteIndex = 1;
                        else
                            SelecteIndex = 0;
                    }                 

                    if(GioiTinh != null)
                    {
                        if (GioiTinh.Equals("Nam"))
                            CheckGioiTinhNam = true;
                        else
                            CheckGioiTinhNam = false;

                        if (GioiTinh.Equals("Nữ"))
                            CheckGioiTinhNu = true;
                        else
                            CheckGioiTinhNu = false;
                    }
                }
            }
        }

        public ICommand MouseLeftButtonDowWindow { get; set; }
        public ICommand closeWindow { get; set; }
        public ICommand openQuanLy { get; set; }
        public ICommand timKiem { get; set; }
        public ICommand selectionChanged { get; set; }
        public ICommand openFolder { get; set; }
        public ICommand xoaNV { get; set; }
        public ICommand suaNV { get; set; }
        public ICommand checkedNam { get; set; }
        public ICommand checkedNu { get; set; }
        public ICommand passwordChanged { get; set; }
        public ICommand xemNV { get; set; }
        public ICommand PreviewMouseLeftButtonUp { get; set; }

        public Visibility visibilityUpdateDelec { get; set; }
        public Visibility visibility { get; set; }

        #endregion

        public NhanVienViewModel()
        {
            ListNV = new ObservableCollection<NHANVIEN>();

            //duy chuyển window
            MouseLeftButtonDowWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });

            //mở form đăng ký
            openQuanLy = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                DangKyWindow dk = new DangKyWindow();
                dk.ShowDialog();
                loadNV();
            });

            //đóng window
            closeWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //load nhân viên
            xemNV = new RelayCommand<KHACHHANG>((p) => { return true; }, (p) =>
            {
                loadNV();
            });

            //tim kiếm nhân viên
            timKiem = new RelayCommand<NHANVIEN>((p) => { return true; }, (p) =>
            {
                using (db = new QUANLYSHOPEntities())
                {
                    ListNV.Clear();
                    using (db = new QUANLYSHOPEntities())
                    {
                        var NV = db.NHANVIEN.Find(IDTimKiem);
                        ListNV.Add(NV);
                    }
                }
            });

            //xóa nhân viên
            xoaNV = new RelayCommand<KHACHHANG>((p) => { return true; }, (p) =>
            {
                using (db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        var NV = db.NHANVIEN.Find(ID);
                        MessageBoxResult result = MessageBox.Show("Bạn Có Thật Sự Muốn Xóa Nhân Viên Này Không?", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                        if (result == MessageBoxResult.OK)
                        {
                            db.NHANVIEN.Remove(NV);
                            db.SaveChanges();
                            loadNV();
                            MessageBox.Show("Xóa Thành Công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Xóa Nhân Viên Không Thành Công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    };
                }
            });

            //sửa nhân viên
            suaNV = new RelayCommand<NHANVIEN>((p) => { return true; }, (p) =>
            {
                using (db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        var NV = db.NHANVIEN.Find(ID);
                        if (NV != null)
                        {
                            NV.hoTen = HoTen;
                            NV.gioiTinh = GioiTinh;
                            NV.ngaySinh = NgaySinh;
                            NV.SDT = SDT;
                            NV.email = Email;
                            NV.diaChi = DiaChi;
                            NV.avatar = Avatar;
                            NV.chucVu = ChucVu;
                            db.SaveChanges();
                            loadNV();
                            MessageBox.Show("Sửa thông tin thành công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Sửa thông tin thất bại", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    };
                }
            });

            //kiểm tra nam
            checkedNam = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                GioiTinh = "Nam";
            });

            //kiểm tra nữ
            checkedNu = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                GioiTinh = "Nữ";
            });

            //chọn chức vụ
            selectionChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                switch (SelecteIndex)
                {
                    case 0:
                        ChucVu = "Nhân Viên";
                        break;
                    case 1:
                        ChucVu = "Quản Lý";
                        break;
                }
            });

            //mở file chọn đường dẫn avatar
            openFolder = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                Avatar = loadPath();
            });

            //mở form quản lý thông nhân viên
            PreviewMouseLeftButtonUp = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                QuanLyNV wd = new QuanLyNV();
                wd.ShowDialog();
            });

            loadNV();
        }

        #region phương thức

        public void loadNV()
        {
            ListNV.Clear();
            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.NHANVIEN select c;

                result.ToList().ForEach(p => ListNV.Add(p));
            }
        }
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
