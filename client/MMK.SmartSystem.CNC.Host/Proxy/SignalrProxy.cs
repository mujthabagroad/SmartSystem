﻿using Microsoft.AspNetCore.SignalR.Client;
using MMK.SmartSystem.WebCommon.DeviceModel;
using MMK.SmartSystem.WebCommon.EventModel;
using MMK.SmartSystem.WebCommon.HubModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMK.SmartSystem.CNC.Host.Proxy
{
    public class SignalrProxy
    {
        public event Action<string> CncErrorEvent;
        public event Action<List<GroupEventData>> GetCncEventData;

        public event Action<HubReadWriterModel> GetClientReaderWriterEvent;
        public event Action<ProgramResovleDto> GetClientProgramResovleEvent;

        HubConnection connection;
        bool isExit = false;
        public SignalrProxy()
        {

            connection = new HubConnectionBuilder()
             .WithUrl($"{SmartSystemCNCHostConsts.ApiHost}/hubs-cncClientHub").WithAutomaticReconnect()
             .Build();
            initEvent();

        }
        public async Task Start()
        {
            try
            {
             
                await connection.StartAsync();
             
                CncErrorEvent?.Invoke($"服务器{SmartSystemCNCHostConsts.ApiHost}连接成功! {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

            }
            catch (Exception ex)
            {
                await Task.Delay(5000);
                CncErrorEvent?.Invoke(ex.Message + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                await Start();
            }
        }

        public async Task<T> SendAction<T>(string actionName, object message)
        {
            try
            {
                if (connection.State == HubConnectionState.Connected)
                {
                    return await connection.InvokeAsync<T>(actionName, message);

                }
                return default;
            }
            catch (Exception ex)
            {
                CncErrorEvent?.Invoke(ex.Message);
                return default;
            }


        }

        public async Task Close()
        {

            isExit = true;
            try
            {
                await connection.StopAsync();

            }
            catch (Exception ex)
            {

                CncErrorEvent?.Invoke(ex.Message);
            }


        }
        public void SendCncData(object cncEventDatas)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(cncEventDatas);
            try
            {
                //connection.InvokeAsync(SinglarCNCHubConsts.InitRefreshAction, json);
            }
            catch (Exception ex)
            {

                CncErrorEvent?.Invoke(ex.Message);

            }
        }
        private void initEvent()
        {
            connection.Closed += async (error) =>
            {
                await Task.Delay(5000);
                if (!isExit)
                {
                    await Start();

                }
            };

            connection.On<object>(SmartSystemCNCHostConsts.ClientGetCncEvent, (message) =>
            {
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GroupEventData>>(message.ToString());
               
                GetCncEventData?.Invoke(json);
            });

            connection.On<HubReadWriterModel>(SmartSystemCNCHostConsts.ClientReaderWriterEvent, (message) =>
            {
                GetClientReaderWriterEvent?.Invoke(message);
            });

            connection.On<ProgramResovleDto>(SmartSystemCNCHostConsts.ClientProgramRosolveEvent, (node) =>
            {
                GetClientProgramResovleEvent?.Invoke(node);
            });
        }
    }
}
