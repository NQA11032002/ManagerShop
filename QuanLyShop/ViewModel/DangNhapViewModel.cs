using Microsoft.Win32;
using QuanLyShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyShop.ViewModel
{
    class DangNhapViewModel : BaseViewModel
    {
        private QUANLYSHOPEntities db;

        #region thuộc tính

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

        public string _avatar;
        public string Avatar { get => _avatar; set { _avatar = value; OnPropertyChanged(); } }

        private string _taiKhoan;
        public string TaiKhoan { get => _taiKhoan; set { _taiKhoan = value; OnPropertyChanged(); } }

        private string _matKhau;
        public string MatKhau { get => _matKhau; set { _matKhau = value; OnPropertyChanged(); } }

        private string _saveMatKhau;
        public string SaveMatKhau { get => _saveMatKhau; set { _saveMatKhau = value; OnPropertyChanged(); } }

        private static string _chucVu;
        public static string ChucVu { get => _chucVu; set { _chucVu = value; } }

        private int _selecteIndex;
        public int SelecteIndex { get => _selecteIndex; set { _selecteIndex = value; ; OnPropertyChanged(); } }

        public bool CheckGioiTinhNam { get; set; }
        public bool CheckGioiTinhNu { get; set; }

        public static int IDLogin { get; set; }
        public static string tenLogin { get; set; }
        public static string AvatarLogin { get; set; }

        public ICommand MouseLeftButtonDowWindow { get; set; }
        public ICommand miniWindow { get; set; }
        public ICommand closeWindow { get; set; }
        public ICommand dangNhap { get; set; }
        public ICommand dangKy { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand savePasswordChangedCommand { get; set; }
        public ICommand openFolder { get; set; }
        public ICommand themNV { get; set; }
        public ICommand checkedNam { get; set; }
        public ICommand checkedNu { get; set; }
        public ICommand selectionChanged { get; set; }
        public ICommand capNhatThongTiNCaNhan { get; set; }
        public ICommand doiMatKhau { get; set; }
        public ICommand nhanVienWindow { get; set; }

        public Visibility visibilityQL { get; set; }

        public bool IsReadOnly { get; set; }
        #endregion

        public DangNhapViewModel()
        {
            SelecteIndex = -1;

            //thu nhỏ window
            miniWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.WindowState = WindowState.Minimized;
            });

            //đóng window
            closeWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //duy chuyển window
            MouseLeftButtonDowWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });

            //nhập mật khẩu mới
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { MatKhau = p.Password; });

            //nhập lại mât khẩu
            savePasswordChangedCommand = new RelayCommand<PasswordBox>((p) => true, (p) => { SaveMatKhau = p.Password; });

            //đăng nhập
            dangNhap = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (checkTaiKhoanMatKhau(TaiKhoan, MatKhau))
                {
                    xetChucVu();
                    p.Hide();
                    MainWindow mn = new MainWindow();
                    mn.ShowDialog();

                    //nếu click nút tắt window thì dừng chương trình ngược lại đăng xuất hiển thị form đăng nhập
                    if (MainViewModel.checkDongWindow)
                        p.Close();
                    else
                        p.Show();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            //đăng ký
            dangKy = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                resetInfo();
                DangKyWindow dk = new DangKyWindow();
                dk.ShowDialog();
            });

            //lấ đường dẫn file avatar
            openFolder = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                Avatar = loadPath();
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

            //thêm nhân viên
            themNV = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        if (TaiKhoan != null && MatKhau != null)
                        {
                            var result = from c in db.NHANVIEN where c.tenDangNhap.Equals(TaiKhoan) select c;

                            //kết quả lớn hơn 0 thì tồn tại tài khoản
                            if (result.Count() > 0)
                            {
                                MessageBox.Show("Tài Khoản đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            else
                            {
                                //kiểm tra email đúng định dạng @
                                if (Email != null)
                                {
                                    if (checkEmail(Email) == false)
                                    {
                                        MessageBox.Show("Vui lòng bao gồm '@' trong địa chỉ email", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Vui lòng nhập Email liên hệ !!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    return;
                                }

                                //đăng ký tài khoản
                                var NV = new NHANVIEN() { hoTen = HoTen, gioiTinh = GioiTinh, ngaySinh = NgaySinh, SDT = SDT, email = Email, diaChi = DiaChi, avatar = Avatar, chucVu = ChucVu, tenDangNhap = TaiKhoan, matKhau = MatKhau };
                                db.NHANVIEN.Add(NV);
                                db.SaveChanges();
                                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                p.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng điền tài khoản và mật khẩu!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Đăng ký thất bại Vui lòng không không để trống thông tin!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            });

            //cập nhật thông tin cá nhân
            capNhatThongTiNCaNhan = new RelayCommand<NHANVIEN>((p) => { return true; }, (p) =>
            {
                using (db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        var nv = db.NHANVIEN.Find(IDLogin);
                        if (nv != null)
                        {
                            nv.hoTen = HoTen;
                            nv.gioiTinh = GioiTinh;
                            nv.ngaySinh = NgaySinh;
                            nv.SDT = SDT;
                            nv.email = Email;
                            nv.diaChi = DiaChi;
                            nv.avatar = Avatar;

                            db.SaveChanges();
                            MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Cập nhật thông tin thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            });

            //đổi mật khẩu
            doiMatKhau = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        var nv = db.NHANVIEN.Find(IDLogin);

                        if (MatKhau.Equals(SaveMatKhau))
                        {
                            nv.matKhau = MatKhau;

                            db.SaveChanges();
                            MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu nhập lại không đúng mật khẩu mới", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Đổi mật khẩu thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            });

            //mở fomr nhân viên
            nhanVienWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                NhanVienWindow nv = new NhanVienWindow();
                nv.ShowDialog();
                p.ShowDialog();
            });
        }

        #region phương thức

        //kiểm tra tài khoản mật khẩu
        public bool checkTaiKhoanMatKhau(string taiKhoan, string matKhau)
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    var result = from c in db.NHANVIEN where c.tenDangNhap.Equals(taiKhoan) && c.matKhau.Equals(matKhau) select c;

                    if (result.Count() > 0)
                    {
                        IDLogin = result.FirstOrDefault().ID;
                        tenLogin = result.FirstOrDefault().hoTen;
                        HoTen = result.FirstOrDefault().hoTen;
                        NgaySinh = result.FirstOrDefault().ngaySinh;
                        GioiTinh = result.FirstOrDefault().gioiTinh;

                        if (GioiTinh != null)
                        {
                            if (GioiTinh.Equals("Nam"))
                                CheckGioiTinhNam = true;
                            else
                                CheckGioiTinhNu = true;
                        }

                        Email = result.FirstOrDefault().email;
                        DiaChi = result.FirstOrDefault().diaChi;
                        SDT = result.FirstOrDefault().SDT;
                        ChucVu = result.FirstOrDefault().chucVu;
                        AvatarLogin = result.FirstOrDefault().avatar;

                        return true;
                    }
                    return false;
                }
                catch { return false; }
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

        //Chọn file hình ảnh
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

        //Kiểm tra chức vụ đăng nhập
        public void xetChucVu()
        {
            switch (DangNhapViewModel.ChucVu)
            {
                case "Quản Lý":
                    IsReadOnly = true;
                    visibilityQL = Visibility.Visible;
                    break;
                default:
                    IsReadOnly = false;
                    visibilityQL = Visibility.Hidden;
                    break;
            }
        }

        //reset các thông tin
        public void resetInfo()
        {
            TaiKhoan = null;
            MatKhau = null;
            HoTen = null;
            NgaySinh = null;
            GioiTinh = null;
            DiaChi = null;
            SDT = null;
            Email = null;
            Avatar = null;
            ChucVu = null;
        }

        #endregion
    }
}
