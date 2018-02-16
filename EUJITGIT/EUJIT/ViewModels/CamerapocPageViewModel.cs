using System;
using System.ComponentModel;
using EUJIT.Interface;

namespace EUJIT.ViewModels
{
    public class CamerapocPageViewModel : INotifyPropertyChanged
    {

        public INavigationService navigationService;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
        public CamerapocPageViewModel()
        {
        }

        public CamerapocPageViewModel(INavigationService navService, Object a) 
        {
            navigationService = navService;
        }

    }
}
