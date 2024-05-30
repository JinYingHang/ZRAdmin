using Infrastructure.Attribute;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZR.Model;
using ZR.Service.Alarm.IService;
using ZR.Model.System;
using static System.Net.WebRequestMethods;
using Infrastructure;

namespace ZR.Service.Alarm
{
    [AppService(ServiceType = typeof(IFeaturesService), ServiceLifetime = LifeTime.Transient)]
    public class FeaturesService : BaseService<object>, IFeaturesService
    {
        public string AlarmIp = AppSettings.GetAppConfig<string>("AlarmServiceIpPort");
        public FeaturesService() { }
        public object SelectFeaturesList(PagerInfo pager, string? feature)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string str = "";
                    if (feature != null)
                        str += "&&feature=" + feature;
                    client.DefaultRequestHeaders.Add("accept", "text/plain");
                    client.DefaultRequestHeaders.Add("request-from", "swagger");
                    var response = client.GetAsync(AlarmIp + "/api/system/need-deal-converge?pageIndex=" +pager.PageNum+"&&pageSize="+pager.PageSize+ str).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        //必须转为对象前端Vue3可以遍历
                        JObject aa = (JObject)JsonConvert.DeserializeObject<JObject>(result)["data"];

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
        }

        public object SelectRulesList(PagerInfo pager,int featureId,int selectType)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    
                    client.DefaultRequestHeaders.Add("accept", "text/plain");
                    client.DefaultRequestHeaders.Add("request-from", "swagger");
                    string url= AlarmIp + "/api/system/converge-rules-by-feature-id?featureid=" + featureId + "&selecttype="+selectType+"&pageindex="+pager.PageNum+"&pagesize="+pager.PageSize;
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        //必须转为对象前端,Vue3可以遍历
                        JObject aa = (JObject)JsonConvert.DeserializeObject<JObject>(result)["data"];

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
        }
        public int AddOrDelRole(int featureId, int converRuleId,int operateType)
        {

            //var list = PostService.GetPages(predicate.ToExpression(), 
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("accept", "text/plain");
                client.DefaultRequestHeaders.Add("request-from", "swagger");


                var response = client.PostAsync(AlarmIp + "/api/system/operate-feature2-converge?featureid=" + featureId + "&convergeruleids=" + converRuleId+ "&operateType=" + operateType, null).Result;

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
