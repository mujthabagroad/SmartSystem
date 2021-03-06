﻿using Abp.Dependency;
using Abp.Events.Bus;
using GalaSoft.MvvmLight.Messaging;
using MMK.SmartSystem.Common.Base;
using MMK.SmartSystem.Common.EventDatas;
using MMK.SmartSystem.Common.ViewModel;
using MMK.SmartSystem.Laser.Base.CustomControl;
using MMK.SmartSystem.Laser.Base.ProgramOperation.UserControls;
using MMK.SmartSystem.Laser.Base.ProgramOperation.UserControls.ViewModel;
using MMK.SmartSystem.Laser.Base.ProgramOperation.ViewModel;
using MMK.SmartSystem.WebCommon.HubModel;
using netDxf;
using netDxf.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

namespace MMK.SmartSystem.Laser.Base.ProgramOperation
{
    /// <summary>
    /// ProgramListPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProgramListPage : SignalrPage
    {
        private ProgramListViewModel programListViewModel { get; set; }
        public override bool IsRequestResponse => true;

        private List<IProgramNotice> programNotices = new List<IProgramNotice>();

        private ProgramApiService ProgramApi = new ProgramApiService();
        public ProgramListPage()
        {
            InitializeComponent();
            programNotices.Add(MyLocalProgramListControl);
            programNotices.Add(MyCNCProgramListControl);
            programNotices.Add(ProgramApi);
            programNotices.Add(MyCNCInfoControl);
            DataContext = programListViewModel = new ProgramListViewModel();

            programListViewModel.CNCPath = new CNCProgramPath(ProgramConfigConsts.CNCPath, "UserControl");

            MyCNCProgramListControl.RealReadWriterEvent += RealReadWriterEvent;
            MyLocalProgramListControl.RealReadWriterEvent += RealReadWriterEvent;
            ProgramApi.RealReadWriterEvent += RealReadWriterEvent;
            MyCNCInfoControl.RealReadWriterEvent += RealReadWriterEvent;
        }

        private void RealReadWriterEvent(HubReadWriterModel obj)
        {
            obj.ConnectId = CurrentConnectId;
            SendReaderWriter(obj);
        }

        protected override void PageSignlarLoaded()
        {
            MyLocalProgramListControl.lpViewModel.ConnectId = CurrentConnectId;

            MyCNCProgramListControl.Init();
            MyCNCInfoControl.Init();
            ProgramApi.Init(() =>
            {
                MyLocalProgramListControl.CheckedLocalProgram();
            });
        }
        protected override void SignalrProxyClient_HubReaderWriterResultEvent(HubReadWriterResultModel obj)
        {
            if (!obj.Success)
            {
                Messenger.Default.Send(new Common.ViewModel.NotifiactionModel()
                {
                    Title = "操作失败",
                    Content = $"失败信息：{obj.Error} {DateTime.Now}",
                    NotifiactionType = Common.ViewModel.EnumPromptType.Error
                });
                return;
            }
            foreach (var item in programNotices)
            {
                if (item.CanWork(obj))
                {
                    item.DoWork(obj);
                }
            }
        }

        public override List<object> GetResultViewModelMap()
        {
            return default;
        }

        public override void CncOnError(string message)
        {
        }
    }
}
