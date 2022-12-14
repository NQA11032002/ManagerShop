//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyShop.Model
{
    using QuanLyShop.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class NHANVIEN : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.HOADON = new HashSet<HOADON>();
            this.NHAPHANG = new HashSet<NHAPHANG>();
        }

        private int _iD;
        public int ID { get => _iD; set { _iD = value; OnPropertyChanged(); } }

        private string _hoTen;
        public string hoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }

        private string _gioiTinh;
        public string gioiTinh { get => _gioiTinh; set { _gioiTinh = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _ngaySinh;
        public Nullable<System.DateTime> ngaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }

        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _email;
        public string email { get => _email; set { _email = value; OnPropertyChanged(); } }

        private string _diaChi;
        public string diaChi { get => _diaChi; set { _diaChi = value; OnPropertyChanged(); } }

        private string _tenDangNhap;
        public string tenDangNhap { get => _tenDangNhap; set { _tenDangNhap = value; OnPropertyChanged(); } }

        private string _matKhau;
        public string matKhau { get => _matKhau; set { _matKhau = value; OnPropertyChanged(); } }

        private string _avatar;
        public string avatar { get => _avatar; set { _avatar = value; OnPropertyChanged(); } }

        private string _chucVu;
        public string chucVu { get => _chucVu; set { _chucVu = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADON { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAPHANG> NHAPHANG { get; set; }
    }
}
