using DTS.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZR.Model.DTS.DTO
{
    public class TaskArgDto
    {
        public int actionId { get; set; }
        public int taskId { get; set; }
        
        public List<Param> args { get; set; }
    }
    public class Param
    {
        /// <summary>
        /// 执行目标 ip:appname:fundid
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 参数名 
        ///</summary>
        public string ParamCode { get; set; }
        /// <summary>
        /// 参数数据类型(前端验证，后端解析用)int/bool/double/float…(带给后端的valueType-string)array(带给后端的valueTypestring,string,string) 
        ///</summary>
        public string ParamType { get; set; }
        /// <summary>
        ///1. 固定值，直接取数据即可   2.ref#appname(必填).form().obj.propname  自己到内存中找对应的属性值
        ///</summary>
        public string ParamValue { get; set; }
        /// <summary>
        /// 出参/入参
        /// </summary>
        public string ActionType { get; set; }
        /// <summary>
        /// 映射，当前参数名对应前置任务的参数名
        ///</summary>
        public string ParamCodeMap { get; set; }
    }
}
