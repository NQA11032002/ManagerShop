using LiveCharts;
using LiveCharts.Defaults;
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
    class ThongKeViewModel : BaseViewModel
    {
        #region thuộc tính

        public QUANLYSHOPEntities db;

        private ObservableCollection<KHACHHANG> _listKH;
        public ObservableCollection<KHACHHANG> ListKH { get => _listKH; set { _listKH = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _listSP;
        public ObservableCollection<SANPHAM> ListSP { get => _listSP; set { _listSP = value; OnPropertyChanged(); } }

        //list tổng số lượng của sản phẩm bán chạy
        public List<int> listTongSoLuongSP { get; set; }

        private int _iDKH;
        public int IDKH { get => _iDKH; set { _iDKH = value; OnPropertyChanged(); } }

        private int _iDNV;
        public int IDNV { get => _iDNV; set { _iDNV = value; OnPropertyChanged(); } }

        private string _hoTenKH;
        public string HoTenKH { get => _hoTenKH; set { _hoTenKH = value; OnPropertyChanged(); } }

        private string _hoTenNV;
        public string HoTenNV { get => _hoTenNV; set { _hoTenNV = value; OnPropertyChanged(); } }

        private string _rank;
        public string Rank { get => _rank; set { _rank = value; OnPropertyChanged(); } }

        private float _doanhThuThangHienTai;
        public float DoanhThuThangHienTai { get => _doanhThuThangHienTai; set => _doanhThuThangHienTai = value; }

        public int soLuongAo { get; set; }
        public int soLuongQuan { get; set; }
        public int soLuongGiay { get; set; }
        public int soLuongBalo { get; set; }
        public int soLuongAoKhoac { get; set; }
        public int soLuongMu { get; set; }
        public int soLuongPhuKien { get; set; }

        public SeriesCollection SeriesCollectionSPBanRa { get; set; }
        public SeriesCollection SeriesCollectionSPBanChay { get; set; }

        public ICommand MouseLeftButtonDowWindow { get; set; }
        public ICommand closeWindow { get; set; }
        public ICommand xemTK { get; set; }

        //hiển thị icon tăng
        public Visibility ArrowUp { get; set; }

        //hiển thị icon giảm
        public Visibility ArrowDown { get; set; }
        #endregion

        #region phương thức
        public ThongKeViewModel()
        {
            //mặc đinh icon giảm ẩn
            ArrowDown = Visibility.Hidden;

            ListKH = new ObservableCollection<KHACHHANG>();
            ListSP = new ObservableCollection<SANPHAM>();
            listTongSoLuongSP = new List<int>();

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

            //xem thống kê
            xemTK = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadThongKe();
            });

            loadThongKe();
        }

        public void loadThongKe()
        {
            //load danh sách khách hàng
            danhSachKH();

            //thông tin khách hàng có điểm tích lũy cao nhất
            khachHangDiemTLMax();

            //thông tin doanh thu tháng hiện tại so với tháng ttruowsc
            dtThangHienTaiSoVoiThangTruoc();

            //thông tin viên năng động nhất
            nhanVienNangDongNhat();

            //top 3 sản phẩm bán hcyaj nhất
            topBaSanPhamBanChay();

            //tính số lượng sản phẩm bán ra theo từng loại
            soLuongAo = soLuongSanPhamBanRa("Áo");
            soLuongQuan = soLuongSanPhamBanRa("Quần");
            soLuongGiay = soLuongSanPhamBanRa("Giày");
            soLuongBalo = soLuongSanPhamBanRa("Ba Lô");
            soLuongAoKhoac = soLuongSanPhamBanRa("Áo Khoác");
            soLuongMu = soLuongSanPhamBanRa("Mũ");
            soLuongPhuKien = soLuongSanPhamBanRa("Phụ Kiện");

            //đồ thị số lượng sản phẩm được bán ra
            doThiThongSo();

            //đồ thị sản phẩm bán chạy nhất
            doThiSanPhamBanChayNhat();
        }

        public ObservableCollection<KHACHHANG> danhSachKH()
        {
            ListKH.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.KHACHHANG select c;

                result.ToList().ForEach(p => ListKH.Add(p));
            }

            return ListKH;
        }

        //tìm khách hàng có điểm tích lũy cao nhất
        public void khachHangDiemTLMax()
        {
            using(db = new QUANLYSHOPEntities())
            {
                //sắp xếp giảm dần theo điểm rồi lấy ra thằng đầu tiên
                var result = (from c in db.KHACHHANG
                              orderby c.diemTL descending
                              select c).Take(1);

                if(result != null)
                {
                    foreach (var item in result)
                    {
                        IDKH = item.ID;
                        HoTenKH = item.hoTen;
                        Rank = item.ranks;
                    }   
                }    
            }    
        }

        //Tính Doanh thu tháng trước
        public float dtThangTruoc()
        {
            float doanhThu = 0;
            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.CTHD
                             join p in db.HOADON
                             on c.IDHD equals p.ID
                             where p.ngayDat.Year == DateTime.Now.Year && p.ngayDat.Month == DateTime.Now.Month - 1 && p.trangThai.Equals("Đã thanh toán")
                             select c;

                result.ToList().ForEach(p => doanhThu = (float)(doanhThu + p.thanhTien));
            }

            return doanhThu;
        }

        //Tính doanh thu tháng hiện tại
        public float dtThangHienTai()
        {
            float doanhThu = 0;
            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.CTHD
                             join p in db.HOADON
                             on c.IDHD equals p.ID
                             where p.ngayDat.Year == DateTime.Now.Year && p.ngayDat.Month == DateTime.Now.Month && p.trangThai.Equals("Đã thanh toán")
                             select c;

                result.ToList().ForEach(p => doanhThu = (float)(doanhThu + p.thanhTien));
            }

            return doanhThu;
        }

        //tổng doanh thu tháng hiện tại so với tháng trước
        public void dtThangHienTaiSoVoiThangTruoc()
        {
            DoanhThuThangHienTai = dtThangHienTai() - dtThangTruoc();

            if (DoanhThuThangHienTai > 0)
            {
                ArrowUp = Visibility.Visible;
                ArrowDown = Visibility.Hidden;
            }
            else
            {
                ArrowUp = Visibility.Hidden;
                ArrowDown = Visibility.Visible;
            }
        }

        //Lấy ra ID nhân viên thanh toán hóa đơn nhiều nhất
        public int IDNhanVienNangDongNhat()
        {
            using (db = new QUANLYSHOPEntities())
            {
                var result = from c in db.HOADON
                             join p in db.NHANVIEN
                             on c.IDNhanVien equals p.ID
                             group c by c.IDNhanVien into temp
                             select new
                             {
                                 ID = temp.Key,
                                 Count = temp.Count(),
                             };

                int maxCount = result.Select(p => p.Count).FirstOrDefault();
                int id = 0;

                result.ToList().ForEach(p =>
                {
                    if (p.Count > maxCount)
                    {
                        maxCount = p.Count;
                        id = p.ID.GetValueOrDefault();
                    }
                });

                if (id > 0)
                    return id;
                return result.Select(p => p.ID).FirstOrDefault().GetValueOrDefault();
            }
        }

        //Tìm nhân viên năng động nhất
        public void nhanVienNangDongNhat()
        {
            using (db = new QUANLYSHOPEntities())
            {
                var nv = db.NHANVIEN.Find(IDNhanVienNangDongNhat());

                if(nv != null)
                {
                    IDNV = nv.ID;
                    HoTenNV = nv.hoTen;
                }    
            }
        }

        //tính tổng số lượng từng loại sản phẩm bán ra trong tháng
        public int soLuongSanPhamBanRa(string tenTL)
        {
            using (db = new QUANLYSHOPEntities())
            {
                int sum = 0;

                var result = from ct in db.CTHD
                             join hd in db.HOADON
                             on ct.IDHD equals hd.ID
                             join sp in db.SANPHAM
                             on ct.IDSP equals sp.ID
                             join tl in db.THELOAI
                             on sp.IDLoai equals tl.ID
                             where tl.tenTL.Equals(tenTL) && hd.trangThai.Equals("Đã thanh toán") && hd.ngayDat.Month == DateTime.Now.Month
                             group ct by ct.SANPHAM.THELOAI.ID into temp
                             select new
                             {
                                 soLuongDat = temp.Sum(p => p.soLuongDat),
                             };

                result.ToList().ForEach(p =>
                {
                    sum += p.soLuongDat.GetValueOrDefault();
                });

                return sum;
            }
        }

        //đồ thị thống số lượng từng loại sản phẩm được bán ra
        public void doThiThongSo()
        {
            SeriesCollectionSPBanRa = new SeriesCollection
            {
                new PieSeries
                {
                    //tiêu đề
                    Title = "Áo",

                    //số lượng áo bán ra
                    Values = new ChartValues<ObservableValue> { new ObservableValue(soLuongAo) },

                    //hiển thị trên view hay không
                    DataLabels = true,
                    FontSize = 30,

                },
                new PieSeries
                {
                    Title = "Quần",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(soLuongQuan) },
                    DataLabels = true,
                    FontSize = 30,
                },
                new PieSeries
                {
                    Title = "Giày",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(soLuongGiay) },
                    DataLabels = true,
                    FontSize = 30,
                },
                new PieSeries
                {
                    Title = "Ba Lô",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(soLuongBalo) },
                    DataLabels = true,
                    FontSize = 30,
                },
                new PieSeries
                {
                    Title = "Áo Khoác",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(soLuongAoKhoac) },
                    DataLabels = true,
                    FontSize = 30,
                },
                new PieSeries
                {
                    Title = "Mũ",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(soLuongMu) },
                    DataLabels = true,
                    FontSize = 30,
                },
                new PieSeries
                {
                    Title = "Phụ Kiện",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(soLuongPhuKien) },
                    DataLabels = true,
                    FontSize = 30,
                },
            };
        }

        //tìm top 3 sản phẩm bán chạy nhất
        public void topBaSanPhamBanChay()
        {
            ListSP.Clear();

            using (db = new QUANLYSHOPEntities())
            {
                //sắp xếp giảm dần theo tổng số lượng đặt rồi lấy ra 3 thằng đầu tiên
                var result = (from ct in db.CTHD
                              group ct by ct.IDSP into temp
                              orderby temp.Sum(p => p.soLuongDat) descending
                              select new
                              {
                                  sanPham = temp.ToList(),
                                  tongSP = temp.Sum(p => p.soLuongDat)
                              }).Take(3);

                result.ToList().ForEach(p =>
                {
                    //thêm tổng số lượng đặt sp
                    int sum = p.tongSP.GetValueOrDefault();
                    listTongSoLuongSP.Add(sum);

                    //kiểm tra sản phẩm có trong danh sách ListSP chưa
                    bool checkSP = false;

                    //thêm sản phẩm vào danh sách sản phẩm
                    p.sanPham.ForEach(q => 
                    {
                        if (ListSP == null)
                            ListSP.Add(q.SANPHAM);
                        else
                        {
                            foreach (var item in ListSP)
                            {
                                //nếu đã tồn tại sản phẩm có ID trong danh sách thì dừng duyệt
                                if (item.ID == q.SANPHAM.ID)
                                {
                                    checkSP = true;
                                    break;
                                }
                            }

                            //chưa có sản phẩm trong danh sách thì thêm sản phẩm vào ListSP
                            if (checkSP == false)
                                ListSP.Add(q.SANPHAM);
                        }    
                    });
                });
            }
        }

        //đồ thị ba sản phẩm bán chạy
        public void doThiSanPhamBanChayNhat()
        {
            if (ListSP.Count >= 3 && listTongSoLuongSP.Count >= 3)
            {
                SeriesCollectionSPBanChay = new SeriesCollection
                {
                    new PieSeries
                    {
                        //tiêu đề
                        Title = ListSP[0].tenSP,

                        //số lượng áo bán ra
                        Values = new ChartValues<ObservableValue> { new ObservableValue(listTongSoLuongSP[0]) },

                        //hiển thị trên view hay không
                        DataLabels = true,
                        FontSize = 30,
                    },
                    new PieSeries
                    {
                        //tiêu đề
                        Title = ListSP[1].tenSP,

                        //số lượng áo bán ra
                        Values = new ChartValues<ObservableValue> { new ObservableValue(listTongSoLuongSP[1]) },

                        //hiển thị trên view hay không
                        DataLabels = true,
                        FontSize = 30,
                    },
                    new PieSeries
                    {
                        //tiêu đề
                        Title = ListSP[2].tenSP,

                        //số lượng áo bán ra
                        Values = new ChartValues<ObservableValue> { new ObservableValue(listTongSoLuongSP[2]) },

                        //hiển thị trên view hay không
                        DataLabels = true,
                        FontSize = 30,
                    },
                };
            } 
        }

        #endregion
    }
}