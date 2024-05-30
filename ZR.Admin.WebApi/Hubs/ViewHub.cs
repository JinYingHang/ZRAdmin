using Infrastructure;
using Infrastructure.Constant;
using Infrastructure.Model;
using IPTools.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Quartz;
using ZR.Admin.WebApi.Controllers;
using ZR.Admin.WebApi.Controllers.Bview;
using ZR.Admin.WebApi.Extensions;
using ZR.Admin.WebApi.Framework;
using ZR.Model;
using ZR.Model.System;
using ZR.Service.System.IService;
using static Infrastructure.GlobalConstant;

namespace ZR.Admin.WebApi.Hubs
{

    public class ViewHub : Hub
    {
        private static readonly List<string> clientUsers = new();
        private static bool _isRunning = false;
        public static IHubCallerClients clients;
        public ViewHub()
        {
           
        }
        public static void RunSendViewData()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                Task.Run(() =>
                {
                    while (true)
                    {
                        if (clients != null)
                            clients.All.SendAsync("viewData", GlobalConstant.rows);
                        Thread.Sleep(3000);
                    }
                });
            }
        }
       

        public override Task OnConnectedAsync()
        {
            var result = new ApiResult(200, "success", "注册SignalR成功");
            Clients.All.SendAsync("ReceiveData", result);
            clients = Clients;
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {

            return base.OnDisconnectedAsync(exception);
        }
        /// <summary>
        ///此方法是被客户端调用的，如何理解此方法
        ///登录了两个用户A和B，当A需要发送实时消息给B，A会再客户端调用此方法
        ///然后此方法通过支持clientID，Clients[clientId].SendAsync 发送 特征 消息给B
        ///再B的页面中绑定 接受 特征 的推送消息如“msg”，“viewdata”等
        /// </summary>
        /// <returns></returns>

        [HubMethodName("SendMessage")]
        public async Task SendMessage()
        {
           await Clients.All.SendAsync("viewData", GlobalConstant.rows);
        }
        //private async void SendData(object state)
        //{


        //    // 向所有客户端发送数据
        //    await Clients.All.SendAsync("ReceiveData", "sss");
        //}
    }
}
