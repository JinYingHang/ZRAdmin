using SqlSugar;
using System;
using ZR.Model;

namespace DTS.Entities
{
    /// <summary>
    /// DTS
    ///</summary>
    [SugarTable("dts_task")]
    public class DtsTask 
    {
        /// <summary>
        /// 主键 
        /// 默认值: nextval('dts_seq'::regclass)
        ///</summary>
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 任务组id 
        ///</summary>
        [SugarColumn(ColumnName = "task_group_id")]
        public int? TaskGroupId { get; set; }
        /// <summary>
        /// 任务失败是否通知到企业微信 0否 1是 
        ///</summary>
        [SugarColumn(ColumnName = "notice_wx")]
        public int? NoticeWX { get; set; }
        /// <summary>
        /// action_ID，记录任务所需要的参数什么的 
        ///</summary>
        [SugarColumn(ColumnName = "action_id")]
        public int? ActionId { get; set; }
        /// <summary>
        /// 任务名称 
        ///</summary>
        [SugarColumn(ColumnName = "task_name")]
        public string TaskName { get; set; }
        /// <summary>
        /// 前置任务数 
        ///</summary>
        [SugarColumn(ColumnName = "front_count")]
        public int? FrontCount { get; set; }
        /// <summary>
        /// -未发送-已接受-执行中-已完成-异常 
        ///</summary>
        [SugarColumn(ColumnName = "status")]
        public string Status { get; set; }
        /// <summary>
        /// 启动时间 
        ///</summary>
        [SugarColumn(ColumnName = "execute_time")]
        public DateTime? ExecuteTime { get; set; }
        /// <summary>
        /// 超时时间(分钟)
        ///</summary>
        [SugarColumn(ColumnName = "out_time")]
        public int? OutTime { get; set; }
        /// <summary>
        /// 超时发送间隔(分钟)
        ///</summary>
        [SugarColumn(ColumnName = "out_time_send_interval")]
        public int? OutTimeSendInterval { get; set; }
        /// <summary>
        /// 哪个ip执行， 域名使用dns#开头
        ///</summary>
        [SugarColumn(ColumnName = "ip")]
        public string Ip { get; set; }
        /// <summary>
        /// 哪个appname执行 
        ///</summary>
        [SugarColumn(ColumnName = "appname")]
        public string Appname { get; set; }
        /// <summary>
        /// 哪个fundid执行 
        ///</summary>
        [SugarColumn(ColumnName = "fundid")]
        public string Fundid { get; set; }
        /// <summary>
        /// 执行时长 
        ///</summary>
        [SugarColumn(ColumnName = "execute_duration")]
        public string ExecuteDuration { get; set; }
        /// <summary>
        /// 开始时间 
        ///</summary>
        [SugarColumn(ColumnName = "begin_time")]
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间 
        ///</summary>
        [SugarColumn(ColumnName = "end_time")]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 允许失败次数 
        ///</summary>
        [SugarColumn(ColumnName = "allow_fail_count")]
        public string AllowFailCount { get; set; }
        /// <summary>
        /// 失败次数 
        ///</summary>
        [SugarColumn(ColumnName = "fill_count")]
        public string FillCount { get; set; }
        /// <summary>
        /// 备注
        ///</summary>
        [SugarColumn(ColumnName = "content")]
        public string Content { get; set; }
        ///// <summary>
        ///// 是否已经在客户端存在 0否 1是
        /////</summary>
        //[SugarColumn(ColumnName = "is_on_client")]
        //public int? IsClearOnClient { get; set; }
        /// <summary>
        /// 入参
        /// </summary>
        [SugarColumn(ColumnName = "in_arg")]
        public string InArg { get; set; }
        /// <summary>
        /// 出参
        /// </summary>
        [SugarColumn(ColumnName = "out_arg")]
        public string OutArg { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "sort")]
        public double? Sort { get; set; }
        /// <summary>
        /// 启动条件：
        ///1.为空：直接启动
        ///2.不为空：如果配置的值和前置任务完成后返回的状态中front_condition相同则启动，否则直接跳该任务以及后续所有分支
        /// </summary>
        [SugarColumn(ColumnName = "start_condition")]
        public string StartCondition { get; set; }
        /// <summary>
        /// 前置任务完成后带出的条件，用来确定当前任务执行还是跳过
        /// </summary>
        [SugarColumn(ColumnName = "front_condition")]
        public string FrontCondition { get; set; }
        /// <summary>
        /// 0实任务 1虚任务(虚任务用来创建管理)
        /// </summary>
        [SugarColumn(ColumnName = "task_type")]
        public int TaskType { get; set; }
        /// <summary>
        /// 对应虚任务的id，虚任务和其子任务1对多
        /// </summary>
        [SugarColumn(ColumnName = "virtual_id")]
        public int VirtualId { get; set; }

        public DtsTask Clone() {
            return new DtsTask {
                Id = this.Id,
                Ip = this.Ip,
                ActionId = this.ActionId,
                AllowFailCount = this.AllowFailCount,
                Appname = this.Appname,
                BeginTime = this.BeginTime,
                EndTime = this.EndTime,
                Content = this.Content,
                ExecuteDuration = this.ExecuteDuration,
                ExecuteTime = this.ExecuteTime,
                FillCount = this.FillCount,
                FrontCondition = this.FrontCondition,
                TaskType = this.TaskType,
                FrontCount = this.FrontCount,
                Fundid = this.Fundid,
                VirtualId = this.VirtualId,
                InArg = this.InArg,
                NoticeWX = this.NoticeWX,
                OutArg = this.OutArg,
                OutTime = this.OutTime,
                OutTimeSendInterval = this.OutTimeSendInterval,
                Sort = this.Sort,
                StartCondition = this.StartCondition,
                Status = this.Status,
                TaskGroupId = this.TaskGroupId,
                TaskName = this.TaskName
            };
        }
    }
}
