using System;
using ZR.Model;


namespace ZR.Service.Alarm.IService
{
    public interface IAlarmService
    {
        public object SelectAlarmList(PagerInfo pager , string? ips, string? appName, string? featureid,string? fundid, string? logLevel, string? status, string? eventType, string? dealperson, DateTime? BeginTime,  DateTime? EndTime,string? feature);

        public object GetAlarmDetail(PagerInfo pager,string eventId);
        int UpdateEvent(string eventId, string dealstatus, string optName, string content);
    }
}
