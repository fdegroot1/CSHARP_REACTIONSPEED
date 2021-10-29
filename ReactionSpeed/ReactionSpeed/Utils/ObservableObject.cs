using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ReactionSpeed_Client.Utils
{
    public class ObserverableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}