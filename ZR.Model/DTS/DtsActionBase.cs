using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace DTS.Entities
{
    /// <summary>
    /// DTS
    ///</summary>
    [SugarTable("dts_action_base")]
    public class DtsActionBase
    {
        /// <summary>
        ///  
        /// 默认值: nextval('dts_seq'::regclass)
        ///</summary>
         [SugarColumn(ColumnName="id" ,IsPrimaryKey = true ,IsIdentity = true  )]
         public int Id { get; set; }
        /// <summary>
        /// 任务标记 
        ///</summary>
         [SugarColumn(ColumnName="action_code"    )]
         public string ActionCode { get; set; }
        /// <summary>
        /// 任务名称 
        ///</summary>
         [SugarColumn(ColumnName="action_name"    )]
         public string ActionName { get; set; }
        /// <summary>
        /// 应用名称 
        ///</summary>
         [SugarColumn(ColumnName="app_name"    )]
         public string AppName { get; set; }
        /// <summary>
        /// 备注 
        ///</summary>
         [SugarColumn(ColumnName="comments"    )]
         public string Comments { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="status"    )]
         public string Status { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="create_time"    )]
         public DateTime? CreateTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="last_time"    )]
         public DateTime? LastTime { get; set; }
    }
}
