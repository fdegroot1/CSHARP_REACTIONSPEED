using GalaSoft.MvvmLight.Command;
//using ReactionSpeed_Client.Models;
using ReactionSpeed_Client.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;


namespace ReactionSpeed_Client.ViewModels
{
    class MainWindowViewModel : ObserverableObject
    {
        public ObserverableObject SelectedViewModel { get; set; }

        public ICommand TestViewCommand { get; set; }

        public MainWindowViewModel()
        {
            SelectedViewModel = new TestViewModel(this);
            TestViewCommand = new RelayCommand(() =>
            {
                SelectedViewModel = new TestViewModel(this);
            });

        }

    }
}
