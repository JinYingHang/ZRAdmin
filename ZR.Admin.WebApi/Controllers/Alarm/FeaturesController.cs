using Microsoft.AspNetCore.Mvc;
using ZR.Service.System.IService;
using ZR.Service.Alarm.IService;
using ZR.Model;
using ZR.Admin.WebApi.Filters;
using Infrastructure.Attribute;
using Infrastructure.Enums;
using Infrastructure;
using ZR.Admin.WebApi.Extensions;
using ZR.Model.System;
using ZR.Service.Alarm;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ZR.Admin.WebApi.Controllers.Alarm
{
    [Verify] //此标签需要传入token才能调用
    [Route("alarm/features")]
    public class FeaturesController: BaseController
    {
        private readonly IFeaturesService FeaturesService;
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public FeaturesController(IFeaturesService featuresService)
        {
            FeaturesService = featuresService;
        }
        /// <summary>
        /// 查询需要分配的特征数据
        /// /system/user/list
        /// </summary>
        /// <returns></returns>
        [ActionPermissionFilter(Permission = "event:features:list")]
        [HttpGet("list")]
        public IActionResult List(PagerInfo pager,[FromQuery] string? feature)
        {
            var list = FeaturesService.SelectFeaturesList(pager,feature);

            return SUCCESS(list, TIME_FORMAT_FULL);
        }

        [HttpGet("rulesList")]
        public IActionResult RulesList(PagerInfo pager, [FromQuery] int featureId, [FromQuery] int selectType)
        {
            var list = FeaturesService.SelectRulesList(pager,featureId, selectType);

            return SUCCESS(list, TIME_FORMAT_FULL);
        }

        [HttpPut("addRule")]
        //表示权限
        [ActionPermissionFilter(Permission = "event:alarm:addRule")]
        //下面注释 表示 插入数据库操作日志，调用方法入参 出餐 以及时间
        [Log(Title = "新增规则", BusinessType = BusinessType.UPDATE)]
        public IActionResult Update([FromBody] Object param)
        {
            var obj = (JObject)JsonConvert.DeserializeObject<JObject>(param.ToString());
            int featureId = int.Parse(obj["featureId"].ToString());
            int converRuleId = int.Parse(obj["converRuleId"].ToString());
            int operateType = int.Parse(obj["operateType"].ToString());
            return ToResponse(FeaturesService.AddOrDelRole(featureId, converRuleId, operateType));
        }


    }
}
