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
    class TheLoaiViewModel : BaseViewModel
    {
        #region thuộc tính
        public QUANLYSHOPEntities db;

        private ObservableCollection<THELOAI> _listTL;
        public ObservableCollection<THELOAI> ListTL { get => _listTL; set { _listTL = value; OnPropertyChanged(); } }

        private int _iD;
        public int ID { get => _iD; set { _iD = value; OnPropertyChanged(); } }

        private string _tenTL;
        public string TenTL { get => _tenTL; set { _tenTL = value; OnPropertyChanged(); } }

        private THELOAI _selectedItem;
        public THELOAI SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                OnPropertyChanged();

                if(SelectedItem != null)
                {
                    ID = SelectedItem.ID;
                    TenTL = SelectedItem.tenTL;
                }    
            }
        }

        public ICommand closeWindow { get; set; }
        public ICommand themTL { get; set; }
        public ICommand suaTL { get; set; }
        public ICommand xoaTL { get; set; }

        #endregion

        #region phương thức
        public TheLoaiViewModel()
        {
            ListTL = new ObservableCollection<THELOAI>();

            //đóng window
            closeWindow = new RelayCommand<Window>((p) => { return true; },(p)=>
            {
                p.Close();
            });

            //thêm thể loại
            themTL = new RelayCommand<object>((p) => { return true; }, (p) => { 
                using(db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        var tl = new THELOAI() { tenTL = TenTL = TenTL };

                        db.THELOAI.Add(tl);
                        db.SaveChanges();
                        ListTL.Add(tl);
                        
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("Thêm thể loại không thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }    
            });

            //sửa thể loại
            suaTL = new RelayCommand<object>((p) => { return true; }, (p) => {
                using (db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        var tl = db.THELOAI.Find(ID);
                        
                        if(tl != null)
                        {
                            tl.tenTL = TenTL;
                            db.SaveChanges();
                            loadTL();
                            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Sửa thể loại không thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });

            //xóa thể loại
            xoaTL = new RelayCommand<object>((p) => { return true; }, (p) => {
                using (db = new QUANLYSHOPEntities())
                {
                    try
                    {
                        MessageBoxResult result = MessageBox.Show("Bạn có thật sự muốn xóa thể loại này không? ", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if(result == MessageBoxResult.Yes)
                        {
                            var tl = db.THELOAI.Find(ID);
                            db.THELOAI.Remove(tl);
                            db.SaveChanges();
                            MessageBox.Show("Xóa thể loại thành công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            loadTL();
                            reset();
                        }    

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Sửa thể loại không thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });

            loadTL();

        }

        public void loadTL()
        {
            ListTL.Clear();

            using(db = new QUANLYSHOPEntities())
            {
                var result = from c in db.THELOAI select c;

                result.ToList().ForEach(p =>
                {
                    ListTL.Add(p);
                });
            }    
        }
        public void reset()
        {
            ID = 0;
            TenTL = null;
        }

        #endregion
    }
}
