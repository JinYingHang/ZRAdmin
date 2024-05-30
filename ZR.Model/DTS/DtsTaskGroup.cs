using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace DTS.Entities
{
    /// <summary>
    /// DTS
    ///</summary>
    [SugarTable("dts_task_group")]
    public class DtsTaskGroup
    {
        /// <summary>
        /// 主键自增 
        /// 默认值: nextval('dts_seq'::regclass)
        ///</summary>
         [SugarColumn(ColumnName="id" ,IsPrimaryKey = true ,IsIdentity = true  )]
         public int Id { get; set; }
        /// <summary>
        /// 通知人_用户id 
        ///</summary>
         [SugarColumn(ColumnName="user_id_notic"    )]
         public int? UserIdNotic { get; set; }
        /// <summary>
        /// 任务组名称 
        ///</summary>
         [SugarColumn(ColumnName="group_name"    )]
         public string GroupName { get; set; }
        /// <summary>
        /// 启动时间段 HH:ff-HH:ff/HH:ff-HH:ff-周期性任务的执行时间段 
        ///</summary>
         [SugarColumn(ColumnName="execute_time_ranges"    )]
         public string ExecuteTimeRanges { get; set; }
        /// <summary>
        /// 执行次数-记录任务执行过多少次 
        ///</summary>
         [SugarColumn(ColumnName="execute_counts"    )]
         public int? ExecuteCounts { get; set; }
        /// <summary>
        /// 是否周期执行 -0否 -1是 
        ///</summary>
         [SugarColumn(ColumnName="periodic_execution"    )]
         public int? PeriodicExecution { get; set; }
        /// <summary>
        /// 状态-待确认-待执行-执行中-执行成功-执行失败 
        ///</summary>
         [SugarColumn(ColumnName="status"    )]
         public string Status { get; set; }
        /// <summary>
        /// 执行周期
        ///</summary>
         [SugarColumn(ColumnName="execute_duration"    )]
         public string ExecuteDuration { get; set; }
        /// <summary>
        /// 备注说明 
        ///</summary>
         [SugarColumn(ColumnName="content"    )]
         public string Content { get; set; }
        /// <summary>
        /// 是否是模板 0否 1是 
        ///</summary>
         [SugarColumn(ColumnName="template"    )]
         public string Template { get; set; }
        /// <summary>
        /// 任务进度 
        ///</summary>
         [SugarColumn(ColumnName="task_progress"    )]
         public string TaskProgress { get; set; }
        /// <summary>
        /// 开始时间 
        ///</summary>
         [SugarColumn(ColumnName="begin_time"    )]
         public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间 
        ///</summary>
         [SugarColumn(ColumnName="end_time"    )]
         public DateTime? EndTime { get; set; }
        /// <summary>
        /// 版本，因周期任务只修改 不新增。所以用版本区分不同周期的任务.写入到日志表
        /// </summary>
        [SugarColumn(ColumnName = "versions")]
        public string Versions { get; set; }
        /// <summary>
        /// 是否通知企业微信 0否 1是
        /// </summary>
        [SugarColumn(ColumnName = "is_notic_wx")]
        public string IsNoticWx { get; set; }
        /// <summary>
        /// 是否透传参数 -0否 -1是
        /// </summary>
        [SugarColumn(ColumnName = "is_transfer_arg")]
        public int? IsTransferArg { get; set; }
    }
}
