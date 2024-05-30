using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace DTS.Entities
{
    /// <summary>
    /// DTS
    ///</summary>
    [SugarTable("dts_task_to_task")]
    public class DtsTaskToTask
    {
        /// <summary>
        ///  
        /// 默认值: nextval('dts_seq'::regclass)
        ///</summary>
         [SugarColumn(ColumnName="id" ,IsPrimaryKey = true ,IsIdentity = true  )]
         public int Id { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="front_details_task_id"    )]
         public int? FrontDetailsTaskId { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="then_details_task_id"    )]
         public int? ThenDetailsTaskId { get; set; }
        /// <summary>
        /// 任务组id 
        ///</summary>
         [SugarColumn(ColumnName= "task_group_id")]
         public int? TaskGroupId { get; set; }
    }
}
