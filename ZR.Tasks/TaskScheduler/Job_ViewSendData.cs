using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZR.Service.System.IService;
using static Infrastructure.GlobalConstant;


namespace ZR.Tasks.TaskScheduler
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
            if (GlobalConstant.rows == null|| GlobalConstant.rows.Count==0)
            {
                GlobalConstant.rows = new List<Dictionary<string, Props>>();
                for (int i = 0; i < 10; i++)
                {
                    Dictionary<string, Props> row = new Dictionary<string, Props>();
                    byte[] addressBytes = new byte[4];
                    Random random = new Random();
                    random.NextBytes(addressBytes);
                    IPAddress ipAddress = new IPAddress(addressBytes);
                    Props props = new Props();
                    props.value = ipAddress.ToString();
                    props.name = ipAddress.ToString();
                    row.Add("ip", props);
                    rows.Add(row);
                }
            }


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
            Console.WriteLine("生成数据完成");
        }
    }
}
