using Microsoft.AspNetCore.Mvc;
using ZR.Service.Alarm.IService;
using ZR.Model;
using ZR.Admin.WebApi.Filters;
using Infrastructure.Attribute;
using Infrastructure.Enums;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Infrastructure.Model;
using System.Collections;
using NLog.Filters;
using System.Web;

namespace ZR.Admin.WebApi.Controllers.Alarm
{
    /// <summary>
    /// 标签说明 [FromQuery]  为url中的条件字符串，必须一个个,也可以事先定义个类来接受，这样入参就不用很多了
    /// 
    /// 标签说明 [FromBody]  HTTP请求正文中的数据到 对象上
    /// </summary>

    [Verify] //此标签需要传入token才能调用
    [Route("alarm/search")]
    public class AlarmSearchController : BaseController
    {
        private readonly IAlarmService AlarmService;
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public AlarmSearchController(IAlarmService alarmService)
        {
            AlarmService = alarmService;
        }
        /// <summary>
        /// 查询警告信息
        /// /system/user/list
        /// </summary>
        /// <returns></returns>
        [ActionPermissionFilter(Permission = "event:alarm:list")]
        [HttpGet("list")]
        public IActionResult List([FromQuery] PagerInfo pager, [FromQuery] string? ips, [FromQuery] string? appName, [FromQuery] string? featureid, [FromQuery] string? fundid, [FromQuery] string? logLevel, [FromQuery] string? status, [FromQuery] string? eventType, [FromQuery] string? dealperson, [FromQuery] DateTime? BeginTime, [FromQuery] DateTime? EndTime, [FromQuery] string? feature)
        {
            var list = AlarmService.SelectAlarmList(pager, ips, appName, featureid, fundid, logLevel, status, eventType, dealperson, BeginTime, EndTime,feature);
            return SUCCESS(list, TIME_FORMAT_FULL);
        }

        /// <summary>
        /// 根据EventId 获取明细数据
        /// /system/user/list
        /// </summary>
        /// <returns></returns>
        [ActionPermissionFilter(Permission = "event:alarm:detail")]
        [HttpGet("detail")]
        public IActionResult GetDetail([FromQuery] PagerInfo pager, [FromQuery] int eventId)
        {
            logger.Info($"进入：{GetDetail},参数：{eventId}");
            var dd = eventId.ToString();
            var list = AlarmService.GetAlarmDetail(pager, eventId.ToString());

            return SUCCESS(list, TIME_FORMAT_FULL);
        }

        [HttpPut("edit")]
        //表示权限
        [ActionPermissionFilter(Permission = "event:alarm:edit")]
        //下面注释 表示 插入数据库操作日志，调用方法入参 出餐 以及时间
        [Log(Title = "完成事件", BusinessType = BusinessType.UPDATE)]
        public IActionResult Update([FromBody] Object param)
        {
            var obj = (JObject)JsonConvert.DeserializeObject<JObject>(param.ToString());
            ArrayList ids=new ArrayList();

            if (obj["eventId"].ToString().IndexOf(",") != -1)
            {
                var temp= obj["eventId"].ToArray();
                foreach (var t in temp)
                {
                    ids.Add(t.ToString());  
                }
            }
            else
            {
                var idArr = obj["eventId"].ToArray();
                if (idArr.Count()==0) {
                    ids.Add(obj["eventId"].ToString());
                }
                else {
                    ids.Add(idArr[0].ToString());
                }
            }


            string eventId = null, dealstatus = null, optName = null, content = null;
            string error_str = "";
            int flag = 0;
            ApiResult result = new ApiResult();
            result.Code = 200;

            foreach (var id in ids)
            {
                eventId = id.ToString();
                if (obj.ContainsKey("dealstatus"))
                    dealstatus = obj["dealstatus"].ToString();
                if (obj.ContainsKey("optName"))
                    optName = obj["optName"].ToString();
                if (obj.ContainsKey("content"))
                    content = obj["content"].ToString();

                flag = AlarmService.UpdateEvent(eventId, dealstatus, optName, content);
                if (flag != 1)
                {
                    result.Code = 405;
                    result.Msg += "eventId:" + eventId + " 修改失败";
                }
            }

            return ToResponse(result);
        }
    }
}
