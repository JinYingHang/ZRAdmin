using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZR.Model;

namespace ZR.Service.Alarm.IService
{
    public interface IFeaturesService
    {
        public object SelectFeaturesList(PagerInfo pager, string? feature);

        public object SelectRulesList(PagerInfo pager,int featureId,int selectType);

        public int AddOrDelRole(int featureId, int converRuleId,int type);

    }
}
