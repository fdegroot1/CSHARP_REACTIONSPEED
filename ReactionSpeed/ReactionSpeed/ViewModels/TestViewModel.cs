using ReactionSpeed_Client.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReactionSpeed_Client.ViewModels
{
    class TestViewModel : ObserverableObject
    {
        private MainWindowViewModel MainViewModel { get; set; }

        public TestViewModel(MainWindowViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }
    }
}
