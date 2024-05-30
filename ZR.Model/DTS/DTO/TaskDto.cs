using SqlSugar;
using System;

namespace ZR.Model.DTS.DTO
{
    public class TaskDto : PagerInfo
    {

        public string GroupName { get; set; }
        public string ThenTaskNames { get; set; }
        public string ActionCode { get; set; }
        public int Id { get; set; }
        public int? TaskGroupId { get; set; }
        public int? ActionId { get; set; }
        public string TaskName { get; set; }
        public int? FrontCount { get; set; }
        public string Status { get; set; }
        public DateTime? ExecuteTime { get; set; }
        public int? OutTime { get; set; }
        public int? OutTimeSendInterval { get; set; }
        public string Ip { get; set; }
        public string Appname { get; set; }
        public string Fundid { get; set; }
        public string ExecuteDuration { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string AllowFailCount { get; set; }
        public string FillCount { get; set; }
        public string Content { get; set; }
        public string InArg { get; set; }
        public string OutArg { get; set; }
        public string StartCondition { get; set; }
        public string FrontCondition { get; set; }
    }
}
