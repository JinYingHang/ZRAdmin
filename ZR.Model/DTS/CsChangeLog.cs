using SqlSugar;
using System;
namespace DTS.Entities
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("cs_change_log")]
    public class CsChangeLog
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
         [SugarColumn(ColumnName="cm_resourece_table"    )]
         public string CmResoureceTable { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="cm_resourece_id"    )]
         public string CmResoureceId { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="cm_resourece_old_value"    )]
         public string CmResoureceOldValue { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="cm_resourece_new_value"    )]
         public string CmResoureceNewValue { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="change_type"    )]
         public string ChangeType { get; set; }
        /// <summary>
        ///  
        /// 默认值: CURRENT_TIMESTAMP
        ///</summary>
         [SugarColumn(ColumnName="change_time"    )]
         public DateTime ChangeTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="changed_by"    )]
         public string ChangedBy { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="change_details"    )]
         public string ChangeDetails { get; set; }
    }
}
