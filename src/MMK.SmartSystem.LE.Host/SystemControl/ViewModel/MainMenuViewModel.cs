﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MMK.SmartSystem.LE.Host.SystemControl.ViewModel
{
    public class MainMenuViewModel : ViewModelBase
    {

        public bool IsLoad { get; set; }


        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        public string Page { get; set; }

        public Type PageType { get; set; }

        public ICommand OpenCommand
        {
            get
            {
                return new RelayCommand<MainMenuViewModel>((s) =>
                {
                    Messenger.Default.Send(s.PageType);
                });
            }
        }
    }
}
