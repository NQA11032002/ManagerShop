using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyShop.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //cập nhật sự kiện
        public event PropertyChangedEventHandler PropertyChanged;

        //thuộc tính có thay đổi gọi sự kiện thay đổi
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _canExecute = canExecute;
            _execute = execute;
        }

        public RelayCommand(Func<object, bool> p)
        {
        }

        //có thể thực hiện chương trình
        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        //thực hiện chương trình
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        //sự kiện thay đổi
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
