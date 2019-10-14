﻿using GalaSoft.MvvmLight.Messaging;
using MMK.SmartSystem.Laser.Base.MachineProcess.UserControls.ViewModel;
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

namespace MMK.SmartSystem.Laser.Base.MachineProcess.UserControls
{
    /// <summary>
    /// InciseProcessControl.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessListControl : UserControl
    {
        public ProcessListViewModel pcListVewModel { get; set; }
        public ProcessListControl()
        {
            InitializeComponent();
            this.DataContext = pcListVewModel = new ProcessListViewModel();
        }

        private void ProcessDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
