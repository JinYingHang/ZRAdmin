using Infrastructure.Attribute;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZR.Model.System;
using ZR.Model;
using ZR.Service.Alarm.IService;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Infrastructure.Extensions;
using JinianNet.JNTemplate;
using Microsoft.Extensions.Logging;
using static System.Net.WebRequestMethods;
using Infrastructure;
using Infrastructure.Model;
using Org.BouncyCastle.Bcpg.Sig;

namespace ZR.Service.Alarm 
{
    [AppService(ServiceType = typeof(IAlarmService), ServiceLifetime = LifeTime.Transient)]
    public class AlarmService:BaseService<object>, IAlarmService
    {
        public string AlarmIp= AppSettings.GetAppConfig<string>("AlarmServiceIpPort");
        public AlarmService() { }

        public object GetAlarmDetail(PagerInfo pager, string eventId)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Add("accept", "text/plain");
                    client.DefaultRequestHeaders.Add("request-from", "swagger");

                    var response = client.GetAsync(AlarmIp+"/api/system/logs-by-event-id?eventid=" + eventId + "&&pageIndex=" + pager.PageNum + "&&pageSize=" + pager.PageSize).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        //必须转为对象前端Vue3可以遍历
                        JObject aa = (JObject)JsonConvert.DeserializeObject<JObject>(result)["data"]["data"];
                        //var list = PostService.GetPages(predicate.ToExpression(), pagerInfo, s => new { s.PostSort });
                        return aa;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        /// <summary>
        /// 根据条件分页查询用户列表
        /// </summary>
        /// <returns></returns>
        public object SelectAlarmList(PagerInfo pager, string? ips, string? appName, string? featureid, string? fundid, string? logLevel, string? status, string? eventType, string? dealperson,DateTime? BeginTime, DateTime? EndTime,string? feature)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("accept", "text/plain");
                    client.DefaultRequestHeaders.Add("request-from", "swagger");
                    string str = "";
                    if (ips != null)
                        str += "&ips=" + ips;
                    if (appName != null)
                        str += "&appnames=" + appName;
                    if (logLevel != null)
                        str += "&log_level=" + logLevel;
                    if (status != null)
                        str += "&status=" + status;
                    if (dealperson != null)
                        str += "&dealPersons=" + dealperson;
                    if (eventType != null)
                        str += "&eventtypes=" + eventType;
                    if (fundid != null)
                        str += "&fundid=" + fundid;
                    if (featureid != null)
                        str += "&featureid=" + featureid;
                    if (BeginTime != null)
                        str += "&BeginTime=" + BeginTime;
                    if (EndTime!= null)
                        str += "&EndTime=" + EndTime;
                    if (feature != null)
                        str += "&feature=" + feature;

                    var response = client.GetAsync(AlarmIp + "/api/system/monitor-datas?pageIndex=" +pager.PageNum+"&&pageSize="+pager.PageSize+str).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        //必须转为对象前端Vue3可以遍历
                        JObject aa = (JObject)JsonConvert.DeserializeObject<JObject>(result)["data"]["data"];
                        //var list = PostService.GetPages(predicate.ToExpression(), pagerInfo, s => new { s.PostSort });

                        return aa;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            //var query = Queryable()
            //    .LeftJoin<SysDept>((u, dept) => u.DeptId == dept.DeptId)
            //    .Where(exp.ToExpression())
            //    .Select((u, dept) => new SysUser
            //    {
            //        UserId = u.UserId.SelectAll(),
            //        DeptName = dept.DeptName,
            //    });
            return null;
        }

        public int UpdateEvent(string eventId,string dealstatus,string optName,string content )
        {
            //
           
            string str = "";
            str+= "&eventid=" + int.Parse(eventId);
            if (dealstatus!=null)
                str += "&dealstatus=" + int.Parse(dealstatus);
            if (optName!= null)
                str += "&dealperson=" + optName;
            if (content!= null)
                str += "&content=" + content;

            //var list = PostService.GetPages(predicate.ToExpression(), 
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("accept", "text/plain");
                client.DefaultRequestHeaders.Add("request-from", "swagger");
                string url= AlarmIp + "/api/system/event-deal-status?1=1" +str;

                var response = client.PutAsync(url,null).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    //必须转为对象前端Vue3可以遍历
                    JObject aa = (JObject)JsonConvert.DeserializeObject<JObject>(result);
                    if (aa["succeeded"].ToString() == "True")
                    {
                        return 1;
                    }
                    //var list = PostService.GetPages(predicate.ToExpression(), pagerInfo, s => new { s.PostSort });
                    else
                     return -1;
                }
                else
                {
                    return -1;
                }
            }
        }

    }
}
