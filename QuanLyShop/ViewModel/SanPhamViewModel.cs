using Microsoft.Win32;
using QuanLyShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyShop.ViewModel
{
    class SanPhamViewModel : BaseViewModel
    {
        #region thuộc tính
        public QUANLYSHOPEntities db;

        private ObservableCollection<SANPHAM> listSP;
        public ObservableCollection<SANPHAM> ListSP { get => listSP; set => listSP = value; }

        private ObservableCollection<THELOAI> _listTL;
        public ObservableCollection<THELOAI> ListTL { get => _listTL; set { _listTL = value; OnPropertyChanged(); } }

        private int _iD;
        public int ID { get => _iD; set { _iD = value; OnPropertyChanged(); } }

        private int? _iDTheLoai;
        public int? IDTheLoai { get => _iDTheLoai; set { _iDTheLoai = value; OnPropertyChanged(); } }

        private string _tenTheLoai;
        public string TenTheLoai { get => _tenTheLoai; set { _tenTheLoai = value; OnPropertyChanged(); } }

        private string _tenSP;
        public string TenSP { get => _tenSP; set { _tenSP = value; OnPropertyChanged(); } }

        private float _giaSP;
        public float GiaSP { get => _giaSP; set { _giaSP = value; OnPropertyChanged(); } }

        private int _soLuong;
        public int SoLuong { get => _soLuong; set { _soLuong = value; OnPropertyChanged(); } }

        private string _kichCo;
        public string KichCo { get => _kichCo; set { _kichCo = value; OnPropertyChanged(); } }

        private string _mieuTa;
        public string MieuTa { get => _mieuTa; set { _mieuTa = value; OnPropertyChanged(); } }

        private string _image;
        public string Image { get => _image; set { _image = value; OnPropertyChanged(); } }

        private int _SelectedIndexSapXep;
        public int SelectedIndexSapXep { get => _SelectedIndexSapXep; set { _SelectedIndexSapXep = value; OnPropertyChanged(); } }

        private float _fromGia;
        public float FromGia { get => _fromGia; set { _fromGia = value; OnPropertyChanged(); } }

        private float _toGia;
        public float ToGia { get => _toGia; set { _toGia = value; OnPropertyChanged(); } }

        private string _tenTimkiem;
        public string TenTimkiem { get => _tenTimkiem; set { _tenTimkiem = value; OnPropertyChanged(); } }

        public int SelectedIndexQLTL { get; set; }


        //chọn thể loại ở form sản phẩm
        private THELOAI _selectedItemTL;
        public THELOAI SelectedItemTL
        {
            get => _selectedItemTL;
            set
            {
                _selectedItemTL = value;
                OnPropertyChanged();

                if (SelectedItemTL != null)
                {
                    IDTheLoai = SelectedItemTL.ID;
                }
            }
        }

        //chọn thể loại ở form quản lý
        private THELOAI _selectedItemQLTL;
        public THELOAI SelectedItemQLTL
        {
            get => _selectedItemQLTL;
            set
            {
                _selectedItemQLTL = value;
                OnPropertyChanged();

                if (SelectedItemQLTL != null)
                {
                    IDTheLoai = SelectedItemQLTL.ID;
                }
            }
        }


        //chọn sản phẩm 
        private SANPHAM _selectedItem;
        public SANPHAM SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    ID = SelectedItem.ID;
                    IDTheLoai = SelectedItem.IDLoai;
                    TenSP = SelectedItem.tenSP;
                    GiaSP = float.Parse(SelectedItem.giaSP.ToString());
                    SoLuong = int.Parse(SelectedItem.soLuong.ToString());
                    KichCo = SelectedItem.kichCo;
                    MieuTa = SelectedItem.mieuTaSP;
                    Image = SelectedItem.imageSP;

                    //tìm vị trí thể loại của sản phẩm hiển thị trên form thông tin sản phẩm
                    foreach (var item in ListTL)
                    {
                        if (item.ID == IDTheLoai)
                        {
                            // -1 vì combobox bắt đầu index từ 0. database bắt đầu 1
                            SelectedIndexQLTL = item.ID - 1;
                            break;
                        }
                    }

                    ThongTinSP tt = new ThongTinSP();
                    tt.ShowDialog();
                }
            }
        }

        //các thuộc tính bắt sự kiện 
        public ICommand MouseLeftButtonDowWindow { get; set; }
        public ICommand closeWindow { get; set; }
        public ICommand selectionChangedSapXep { get; set; }
        public ICommand SelectionChangedTL { get; set; }
        public ICommand locGiaSanPham { get; set; }
        public ICommand timKiem { get; set; }
        public ICommand themSP { get; set; }
        public ICommand suaSP { get; set; }
        public ICommand xoaSP { get; set; }
        public ICommand quanLySP { get; set; }
        public ICommand openFolder { get; set; }
        public ICommand PreviewMouseLeftButtonUp { get; set; }

        #endregion

        public SanPhamViewModel()
        {
            ListSP = new ObservableCollection<SANPHAM>();
            ListTL = new ObservableCollection<THELOAI>();

            //duy chuyển window
            MouseLeftButtonDowWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });

            //đóng window
            closeWindow = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

            //mở form quản lý sản phẩm
            quanLySP = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                reset();

                //load lại sản phẩm trước khi thêm sản phẩm mới
                loadSP();

                //hiển thị IDSP lớn nhất
                ID = iDSPMax() + 1;

                QuanLySP ql = new QuanLySP();
                ql.ShowDialog();

            });

            //lọc sản phẩm theo giá
            locGiaSanPham = new RelayCommand<SANPHAM>((p) => { if (ToGia > 0) return true; return false; }, (p) => { locGia(FromGia, ToGia, IDTheLoai); });

            //tìm kiếm sản phẩm
            timKiem = new RelayCommand<SANPHAM>((p) => { if (TenTimkiem != null) return true; return false; }, (p) => { timKiemSPTheoTen(TenTimkiem); });
            
            //thay đổi sắp xếp
            selectionChangedSapXep = new RelayCommand<SANPHAM>((p) => { return true; }, (p) =>
             {
                 ToGia = 0;
                 FromGia = 0;
                 TenTimkiem = null;

                 selectedIndexSapXep();
             });

            //thay đổi thể loại
            SelectionChangedTL = new RelayCommand<SANPHAM>((p) => { return true; }, (p) =>
            {
                ToGia = 0;
                FromGia = 0;
                TenTimkiem = null;

                //IDTL khác 1 thì load sản phẩm theo ID THỂ Loại ngược lại load tất cả sản phẩm
                if (IDTheLoai != 1)
                    loadSP(IDTheLoai);
                else
                    loadSP();

                selectedIndexSapXep();
            });

            //thêm sửa xóa sản phẩm
            themSP = new RelayCommand<Window>((p) => { return true; }, (p) => { themSanPham(); loadSP(); });

            suaSP = new RelayCommand<Window>((p) => { return true; }, (p) => { suaSanPham(); loadSP(); });

            xoaSP = new RelayCommand<Window>((p) => { return true; }, (p) => { xoaSanPham(); loadSP(); });

            //chọn ảnh 
            openFolder = new RelayCommand<object>((p) => { return true; }, (p) => { Image = loadPath(); });

            //mở form thông tin sản phẩm
            PreviewMouseLeftButtonUp = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ThongTinSP tt = new ThongTinSP();
                tt.ShowDialog();
            });

            loadSP();
            loadTheLoai();
        }

        //load sản phẩm
        public void loadSP(int? idTheLoai = null)
        {
            ListSP.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    if (idTheLoai == null)
                    {
                        var result = from c in db.SANPHAM select c;

                        result.ToList().ForEach(p => ListSP.Add(p));
                    }
                    else
                    {
                        var result = from c in db.SANPHAM where c.IDLoai == idTheLoai select c;

                        result.ToList().ForEach(p => ListSP.Add(p));
                    }
                }
                catch { };
            }
        }

        

        //load thể loại
        public void loadTheLoai()
        {
            ListTL.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.THELOAI select c;

                result.ToList().ForEach(p => ListTL.Add(p));
            }
        }

        

        //lộc ra giá
        public void locGia(float fromGia, float toGia, int? id = null)
        {
            ListSP.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                //lọc sản phẩm theo ID loại
                if (id != 1)
                {
                    var result = from c in db.SANPHAM where c.giaSP >= fromGia && c.giaSP <= toGia && c.IDLoai == id select c;

                    result.ToList().ForEach(p => ListSP.Add(p));
                }
                else //lọc tất cả
                {
                    var result = from c in db.SANPHAM where c.giaSP >= fromGia && c.giaSP <= toGia select c;

                    result.ToList().ForEach(p => ListSP.Add(p));
                }
            }
        }

        

        //tìm kiếm sản phẩm theo tên
        public void timKiemSPTheoTen(string tenSanPham)
        {
            ListSP.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.SANPHAM orderby c.giaSP ascending where c.tenSP.Equals(tenSanPham) select c;

                result.ToList().ForEach(p => ListSP.Add(p));
            }
        }

     
        
        //sắp xếp sản phẩm theo giá tăng
        public void sapXepSPTang(int? ID = null)
        {
            ListSP.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                //sắp xếp theo ID loại
                if (ID != null)
                {
                    var result = from c in db.SANPHAM orderby c.giaSP ascending where c.IDLoai == ID select c;

                    result.ToList().ForEach(p => ListSP.Add(p));
                }
                else //sắp xếp tất cả
                {
                    var result = from c in db.SANPHAM orderby c.giaSP ascending select c;

                    result.ToList().ForEach(p => ListSP.Add(p));
                }

            }
        }

      

        //sắp xếp sản phẩm theo giá giảm
        public void sapXepSPGiam(int? ID = null)
        {
            ListSP.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                //sắp xếp theo ID thể loại
                if (ID != null)
                {
                    var result = from c in db.SANPHAM orderby c.giaSP descending where c.IDLoai == ID select c;

                    result.ToList().ForEach(p => ListSP.Add(p));
                }
                else //sắp xếp tất cả
                {
                    var result = from c in db.SANPHAM orderby c.giaSP descending select c;

                    result.ToList().ForEach(p => ListSP.Add(p));
                }
            }
        }

        

        //thêm sản phẩm
        public void themSanPham()
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    if(TenSP == null)
                        MessageBox.Show("Tên sản phẩm không được để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

                    if (GiaSP <= 0)
                        MessageBox.Show("Giá bán sản phẩm phải lớn hơn 0", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

                    if (SoLuong <= 0)
                        MessageBox.Show("Số lượng sản phẩm phải lớn hơn 0", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);


                    if (checkSize(KichCo))
                    {
                        SANPHAM sp = new SANPHAM() { tenSP = TenSP, giaSP = GiaSP, soLuong = SoLuong, IDLoai = IDTheLoai.GetValueOrDefault(), kichCo = KichCo, imageSP = Image, mieuTaSP = MieuTa };
                        db.SANPHAM.Add(sp);
                        db.SaveChanges();
                        ListSP.Add(sp);
                        MessageBox.Show("Thêm sản phẩm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }    
                    else
                        MessageBox.Show("Kích cỡ bạn nhập không đúng!. Phải thuộc các kích cỡ {S, M, L, XL, XXL }", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch
                {
                    MessageBox.Show("Thêm sản phẩm không thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
       
        //sửa sản phẩm
        public void suaSanPham()
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    var sp = db.SANPHAM.Find(ID);
                    sp.tenSP = TenSP;
                    sp.giaSP = GiaSP;
                    sp.soLuong = SoLuong;
                    sp.IDLoai = IDTheLoai.GetValueOrDefault();
                    sp.kichCo = KichCo;
                    sp.imageSP = Image;
                    sp.mieuTaSP = MieuTa;

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật sản phẩm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Cập nhật sản phẩm không thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

       
        
        //xóa sản phẩm
        public void xoaSanPham()
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có muốn xóa sản phẩm này không?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (result == MessageBoxResult.OK)
                    {
                        var sp = db.SANPHAM.Find(SelectedItem.ID);
                        db.SANPHAM.Remove(sp);
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show("Có đơn đặt hàng sản phẩm này không thể xóa!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

     
        
        //Kiểm tra định dạng Size
        public bool checkSize(string size)
        {
            if(size != null)
            {
                if (size.Equals("S") || size.Equals("M") || size.Equals("L") || size.Equals("XL") || size.Equals("XXL") || 
                    size.Equals("s") || size.Equals("m") || size.Equals("l") || size.Equals("xl") || size.Equals("xxl"))
                    return true;
            }    

            return false;
        }   
        
        //chọn hình ảnh
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

       

        //Lấy ra IDSP cao nhất để hiển thị trên view
        public int iDSPMax()
        {
            using (db = new QUANLYSHOPEntities())
            {
                try
                {
                    var result = (from c in db.SANPHAM select c.ID).Max();

                    return result;
                }
                catch
                {
                    return 0;
                };
            }
        }

       

        //reset thông tin sản phẩm
        public void reset()
        {
            SelectedItemQLTL = null;
            TenSP = null;
            GiaSP = 0;
            KichCo = null;
            SoLuong = 0;
            Image = null;
            MieuTa = null;
        }

       

        //thay đổi index sắp xếp
        public void selectedIndexSapXep()
        {
            switch (SelectedIndexSapXep)
            {
                case 0:
                    if (IDTheLoai == 1)
                        loadSP();
                    else
                        loadSP(IDTheLoai);
                    break;
                case 1:
                    if (IDTheLoai == 1)
                        sapXepSPTang();
                    else
                        sapXepSPTang(IDTheLoai);
                    break;
                case 2:
                    if (IDTheLoai == 1)
                        sapXepSPGiam();
                    else
                        sapXepSPGiam(IDTheLoai);
                    break;
            }
        }
    }
}
