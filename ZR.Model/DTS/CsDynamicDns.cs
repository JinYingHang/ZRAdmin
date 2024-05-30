using SqlSugar;
namespace DTS.Entities
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("cs_dynamic_dns")]
    public class CsDynamicDns
    {
        /// <summary>
        ///  
        /// 默认值: nextval('dts_seq'::regclass)
        ///</summary>
         [SugarColumn(ColumnName="id" ,IsPrimaryKey = true ,IsIdentity = true  )]
         public int Id { get; set; }
        /// <summary>
        /// 域名 
        ///</summary>
         [SugarColumn(ColumnName="domain_name"    )]
         public string DomainName { get; set; }
        /// <summary>
        /// 对应的ip 
        ///</summary>
         [SugarColumn(ColumnName="ip"    )]
         public string Ip { get; set; }
        /// <summary>
        /// 描述 
        ///</summary>
         [SugarColumn(ColumnName="comment"    )]
         public string Comment { get; set; }
    }
}
