﻿using MMK.SmartSystem.Laser.Base.MachineProcessWork.UserControls.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MMK.SmartSystem.Laser.Base.MachineProcessWork.UserControls
{
    /// <summary>
    /// ProcessWorkOperateControl.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessWorkOperateControl : UserControl
    {
        private ProcessWorkOperateViewModel PWorkVM = new ProcessWorkOperateViewModel();
        public ProcessWorkOperateControl()
        {
            InitializeComponent();
            this.DataContext = PWorkVM;
        }
    }
}
