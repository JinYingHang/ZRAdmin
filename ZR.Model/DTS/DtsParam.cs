using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace DTS.Entities
{
    /// <summary>
    /// DTS
    ///</summary>
    [SugarTable("dts_param")]
    public class DtsParam
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
         [SugarColumn(ColumnName="action_id"    )]
         public int? ActionId { get; set; }
        /// <summary>
        /// 参数名 
        ///</summary>
         [SugarColumn(ColumnName="param_code"    )]
         public string ParamCode { get; set; }
        /// <summary>
        /// 参数数据类型(前端验证，后端解析用)int/bool/double/float…(带给后端的valueType-string)array(带给后端的valueTypestring,string,string) 
        ///</summary>
         [SugarColumn(ColumnName="param_type"    )]
         public string ParamType { get; set; }
        /// <summary>
        /// 默认值 
        ///</summary>
         [SugarColumn(ColumnName="default_value"    )]
         public string DefaultValue { get; set; }
        /// <summary>
        /// 选择值
        ///</summary>
        [SugarColumn(ColumnName = "select_value")]
        public string SelectValue { get; set; }
        /// <summary>
        /// 取值范围 
        ///</summary>
        [SugarColumn(ColumnName="value_range"    )]
         public string ValueRange { get; set; }
        /// <summary>
        /// 映射，当前参数名对应前置任务的参数名
        ///</summary>
        [SugarColumn(ColumnName= "param_code_map")]
         public string ParamCodeMap { get; set; }
        /// <summary>
        /// 组件类型(前端渲染用)-text（string）-select.单/多(默认值#真实值|显示值,真实值|显示值...)-以后可以留着拓展其他组件 
        ///</summary>
         [SugarColumn(ColumnName="module_type"    )]
         public string ModuleType { get; set; }
        /// <summary>
        /// 入参/出参 
        ///</summary>
         [SugarColumn(ColumnName="type"    )]
         public string Type { get; set; }
        /// <summary>
        /// 是否必须，非空验证 
        ///</summary>
         [SugarColumn(ColumnName="is_must"    )]
         public string IsMust { get; set; }
        /// <summary>
        /// 参数中文 
        ///</summary>
         [SugarColumn(ColumnName="param_name"    )]
         public string ParamName { get; set; }
    }
}
