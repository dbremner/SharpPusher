﻿using MVVMLib;
using SharpPusher.Services;
using SharpPusher.Services.PushServices;
using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace SharpPusher
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            ApiList = new ObservableCollection<Api>();
            ApiList.Add(new Blockr());
            ApiList.Add(new Smartbit());
            ApiList.Add(new BlockCypher());
            ApiList.Add(new BlockExplorer());
            ApiList.Add(new BlockchainInfo());

            BroadcastTxCommand = new BindableCommand(BroadcastTx, CanBroadcast);

            Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            versionString = string.Format("Version {0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);
        }


        private string versionString;
        public string VersionString
        {
            get { return versionString; }
        }


        private string rawTx;
        public string RawTx
        {
            get { return rawTx; }
            set
            {
                if (SetField(ref rawTx, value))
                {
                    BroadcastTxCommand.RaiseCanExecuteChanged();
                }
            }
        }


        public ObservableCollection<Api> ApiList { get; set; }


        private Api selectedApi;
        public Api SelectedApi
        {
            get { return selectedApi; }
            set
            {
                if (SetField(ref selectedApi, value))
                {
                    BroadcastTxCommand.RaiseCanExecuteChanged();
                }
            }
        }


        private bool isSending;
        public bool IsSending
        {
            get { return isSending; }
            set
            {
                if (SetField(ref isSending, value))
                {
                    BroadcastTxCommand.RaiseCanExecuteChanged();
                }
            }
        }



        public BindableCommand BroadcastTxCommand { get; private set; }
        private async void BroadcastTx()
        {
            IsSending = true;
            Errors = string.Empty;
            Status = "Broadcasting Transaction...";

            Response<string> resp = await SelectedApi.PushTx(RawTx);
            if (resp.HasErrors)
            {
                Errors = resp.GetErrors();
                Status = "Finished with error.";
            }
            else
            {
                Status = resp.Result;
            }
            IsSending = false;
        }
        private bool CanBroadcast()
        {
            if (!string.IsNullOrWhiteSpace(RawTx) && !IsSending && selectedApi != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}