using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Input;

namespace WpfApp1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _isEnglish = true;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string LanguageCommandLabel { get { return Properties.Resources.LanguageCommandLabel; } }
        public string NameLable { get { return Properties.Resources.NameLable; } }

        private string _name;
        public string Name {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private ICommand _command;
        public ICommand LanguageCommand {
            get { return _command ?? (_command = new DelegateCommand(ExecuteCommand, () => { return true; })); }
        }

        private void ExecuteCommand()
        {
            if (_isEnglish)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            }
            RefreshLables();
            _isEnglish = !_isEnglish;
        }

        private void RefreshLables()
        {
            OnPropertyChanged(nameof(LanguageCommandLabel));
            OnPropertyChanged(nameof(NameLable));
        }
    }
}