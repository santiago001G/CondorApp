using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using CondorApp.Helpers;

namespace CondorApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IRepositorio Repositorio => DependencyService.Get<Repositorio>();

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
