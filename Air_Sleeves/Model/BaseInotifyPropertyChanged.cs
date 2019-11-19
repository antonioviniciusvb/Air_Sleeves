using System.ComponentModel;
using System.Linq;
using System.Text;
using PropertyChanged;
using System.Threading.Tasks;
using Air_Sleeves.Util;
using System.Collections.Generic;
using System;

namespace Air_Sleeves.Model
{
    public class BaseInotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
