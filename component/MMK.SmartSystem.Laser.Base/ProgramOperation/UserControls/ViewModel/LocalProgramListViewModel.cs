﻿using Abp.Events.Bus;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MMK.SmartSystem.Common.EventDatas;
using MMK.SmartSystem.Laser.Base.ProgramOperation.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MMK.SmartSystem.Laser.Base.ProgramOperation.UserControls.ViewModel
{
    public class LocalProgramListViewModel:ViewModelBase
    {
        private ProgramViewModel _SelectedProgramViewModel;
        public ProgramViewModel SelectedProgramViewModel{
            get { return _SelectedProgramViewModel; }
            set{
                if (_SelectedProgramViewModel != value){
                    _SelectedProgramViewModel = value;
                    RaisePropertyChanged(() => SelectedProgramViewModel);
                }
            }
        }

        /// <summary>
        /// 原数据
        /// </summary>
        public List<ProgramViewModel> LocalProgramList { get; set; }

        /// <summary>
        /// 显示数据
        /// </summary>
        private ObservableCollection<ProgramViewModel> _ProgramList;
        public ObservableCollection<ProgramViewModel> ProgramList{
            get { return _ProgramList; }
            set{
                if (_ProgramList != value){
                    _ProgramList = value;
                    RaisePropertyChanged(() => ProgramList);
                }
            }
        }

        private ReadProgramFolderItemViewModel _ProgramFolderInfo;
        public ReadProgramFolderItemViewModel ProgramFolderInfo
        {
            get { return _ProgramFolderInfo; }
            set
            {
                if (_ProgramFolderInfo != value)
                {
                    _ProgramFolderInfo = value;
                    RaisePropertyChanged(() => ProgramFolderInfo);
                }
            }
        }

        public string Path { get; set; }

        private string _LocalPath;
        public string LocalPath
        {
            get { return _LocalPath; }
            set
            {
                if (_LocalPath != value)
                {
                    _LocalPath = value;
                    RaisePropertyChanged(() => LocalPath);
                }
            }
        }

        public LocalProgramListViewModel()
        {
            this.Path = @"C:\Users\wjj-yl\Desktop\测试用DXF";
            GetFileName();
            Messenger.Default.Register<SearchInfo>(this, (sInfo) => {
                this.ProgramList.Clear();
                if (string.IsNullOrEmpty(sInfo.Search))
                {
                    DataPaging(1);
                    return;
                }
                foreach (var item in this.LocalProgramList.Where(n => n.Name.Contains(sInfo.Search)))
                {
                    this.ProgramList.Add(item);
                }
            });
        }

        public void GetFileName()
        {
            if (Directory.Exists(this.Path)){
                DirectoryInfo root = new DirectoryInfo(this.Path);
                LocalProgramList = new List<ProgramViewModel>();
                foreach (FileInfo f in root.GetFiles())
                {
                    var program = new ProgramViewModel
                    {
                        Name = f.Name,
                        CreateTime = f.CreationTime.ToString(),
                        Size = (f.Length / 1024).ToString() + "KB"
                    };
                    this.LocalProgramList.Add(program);
                }
                DataPaging(1);
                this.LocalPath = this.Path;
                Messenger.Default.Send(new LocalProgramPath(this.Path));
            }
        }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageNumber = 10;
        public void DataPaging(int page)
        {
            int count = LocalProgramList.Count;
            int pageSize = 0;  
            if (count % PageNumber == 0)
            {
                pageSize = count / PageNumber;
            }
            else
            {
                pageSize = count / PageNumber + 1;
            }
            TotalPage = pageSize;
            CurrentPage = page;
            
            LastIsEnable = CurrentPage > 1;
            NextIsEnable = CurrentPage < TotalPage;
            
            this.ProgramList = new ObservableCollection<ProgramViewModel>(LocalProgramList.Take(PageNumber * CurrentPage).Skip(PageNumber * (CurrentPage - 1)).ToList());
        }

        private bool _NextIsEnable;
        public bool NextIsEnable
        {
            get { return _NextIsEnable; }
            set
            {
                if (_NextIsEnable != value)
                {
                    _NextIsEnable = value;
                    RaisePropertyChanged(() => NextIsEnable);
                }
            }
        }

        private bool _LastIsEnable;
        public bool LastIsEnable
        {
            get { return _LastIsEnable; }
            set
            {
                if (_LastIsEnable != value)
                {
                    _LastIsEnable = value;
                    RaisePropertyChanged(() => LastIsEnable);
                }
            }
        }

        public string ConnectId { get; set; }
        public ICommand UpLoadCommand
        {
            get{
                return new RelayCommand(() => {
                    if (this.SelectedProgramViewModel == null)
                    {
                        return;
                    }
                    var stream = FileToStream();
                    var fileHash = FileHashHelper.ComputeMD5(System.IO.Path.Combine(this.Path, this.SelectedProgramViewModel.Name));
                    Task.Factory.StartNew(new Action(() => {
                        EventBus.Default.TriggerAsync(new UpLoadProgramClientEventData
                        {
                            FileParameter = new Common.FileParameter(stream, this.SelectedProgramViewModel.Name),
                            ConnectId = ConnectId,
                            FileHashCode = fileHash
                        });
                    }));
                    new PopupWindow(new UpLoadLocalProgramControl(this.Path, this.ProgramFolderInfo), 900, 590, "上传本地程序").ShowDialog();
                });
            }
        }

        public ICommand LocalPathCommand{
            get{
                return new RelayCommand(() => {
                    System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
                    if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK){
                        this.Path = folderDialog.SelectedPath.Trim();
                        GetFileName();
                    }
                });
            }
        }

        public ICommand DeleteFileCommand{
            get{
                return new RelayCommand(() =>{
                    if (File.Exists(System.IO.Path.Combine(this.Path, this.SelectedProgramViewModel.Name))) { 
                        File.Delete(System.IO.Path.Combine(this.Path, this.SelectedProgramViewModel.Name));
                        GetFileName();
                    }
                });
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    new PopupWindow(new SearchControl(), 680, 240, "搜索本地程序").ShowDialog();
                });
            }
        }

        public ICommand OpenFileCommand
        {
            get{
                return new RelayCommand(() => {
                    System.Diagnostics.Process.Start(@"Notepad.exe", System.IO.Path.Combine(this.Path, this.SelectedProgramViewModel.Name));
                });
            }
        }

        public ICommand LastPageCommand
        {
            get
            {
                return new RelayCommand(() => {
                    if (CurrentPage > 1)
                    {
                        DataPaging(--CurrentPage);
                    }
                });
            }
        }

        public ICommand NextPageCommand
        {
            get
            {
                return new RelayCommand(() => {
                    if (CurrentPage < TotalPage)
                    {
                        DataPaging(++CurrentPage);
                    }
                });
            }
        }

        public Stream FileToStream()
        {
            using (var fileStream = new FileStream(System.IO.Path.Combine(this.Path, this.SelectedProgramViewModel.Name), FileMode.Open, FileAccess.Read, FileShare.Read)){

                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
                Stream stream = new MemoryStream(bytes);
                return stream;
            }
        }
    }
}
