﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMK.SmartSystem.LE.Host.ViewModel
{
    public class MainTranslateViewModel : ViewModelBase
    {
        public SystemTranslate Translate { get; set; }

        public MainTranslateViewModel()
        {
            Translate = SmartSystemLEConsts.SystemTranslateModel;
        }

    }
}