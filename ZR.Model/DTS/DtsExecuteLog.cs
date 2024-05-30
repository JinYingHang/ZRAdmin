using SqlSugar;
using System;
namespace DTS.Entities
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("dts_execute_log")]
    public class DtsExecuteLog
    {
        /// <summary>
        ///  
        /// 默认值: nextval('dts_seq'::regclass)
        ///</summary>
        [SugarColumn(ColumnName = "id", IsIdentity = true,IsPrimaryKey = true)]
        public int Id { get; set; }
        /// <summary>
        /// 任务id 
        ///</summary>
        [SugarColumn(ColumnName = "task_id")]
        public int? TaskId { get; set; }
        /// <summary>
        /// 任务组id 
        ///</summary>
        [SugarColumn(ColumnName = "group_id")]
        public int? GroupId { get; set; }
        /// <summary>
        /// 任务组进度 
        ///</summary>
        [SugarColumn(ColumnName = "group_progress")]
        public string GroupProgress { get; set; }
        /// <summary>
        /// 任务组版本 
        ///</summary>
        [SugarColumn(ColumnName = "group_versions")]
        public string GroupVersions { get; set; }
        /// <summary>
        /// 任务状态 成功|失败... 
        ///</summary>
        [SugarColumn(ColumnName = "task_status")]
        public string TaskStatus { get; set; }
        /// <summary>
        /// 说明 
        ///</summary>
        [SugarColumn(ColumnName = "task_comments")]
        public string TaskComments { get; set; }
        /// <summary>
        /// 入参 
        ///</summary>
        [SugarColumn(ColumnName = "task_in_param")]
        public string TaskInParam { get; set; }
        /// <summary>
        /// 出参 
        ///</summary>
        [SugarColumn(ColumnName = "task_out_param")]
        public string TaskOutParam { get; set; }
        /// <summary>
        /// 开始时间 
        ///</summary>
        [SugarColumn(ColumnName = "task_begin_time")]
        public DateTime TaskBeginTime { get; set; }
        /// <summary>
        /// 结束时间 
        /// 默认值: CURRENT_TIMESTAMP
        ///</summary>
        [SugarColumn(ColumnName = "task_end_time")]
        public DateTime TaskEndTime { get; set; }
        /// <summary>
        /// 执行时间 
        ///</summary>
        [SugarColumn(ColumnName = "task_execution_time")]
        public string TaskExecutionTime { get; set; }
    }
}
