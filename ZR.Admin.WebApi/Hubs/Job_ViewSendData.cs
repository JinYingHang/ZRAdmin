using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZR.Service.System.IService;
using ZR.Tasks;
using static Infrastructure.GlobalConstant;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ZR.Admin.WebApi.Hubs
{
    [AppService(ServiceType = typeof(Job_ViewSendData), ServiceLifetime = LifeTime.Scoped)]
    public class Job_ViewSendData : JobBase, IJob
    {
       

        public async Task Execute(IJobExecutionContext context)
        {
            await ExecuteJob(context, async () => await Run());
        }

        public async Task Run()
        {
            await Task.Delay(1);
            //TODO 业务逻辑
            string cols = "v10d,d0,h0,c0,n0,e0,d1d,d2d,d10d,h1d,h2d,h10d,c1d,c2d,c10d,n1d";
            foreach (var t in GlobalConstant.rows)
            {
                Random random = new Random();

                foreach (var c in cols.Split(','))
                {
                    Props props = new Props();
                    double percentage = random.NextDouble() * 100.0;
                    props.value = Math.Round(percentage, 2);
                    props.name = c;
                    if (double.Parse(props.value.ToString()) > 95)
                        props.color = "red";
                    else if (double.Parse(props.value.ToString()) > 90 && (double.Parse(props.value.ToString()) < 95))
                        props.color = "green";
                    else
                        props.color = null;

                    t[c] = props;
                }
            }
        }
    }
}
