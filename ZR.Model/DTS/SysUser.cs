using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace DTS.Entities
{
    /// <summary>
    /// 用户信息表
    ///</summary>
    [SugarTable("sys_user")]
    public class SysUser
    {
        /// <summary>
        /// 用户ID 
        /// 默认值: nextval('sys_user_userid_seq'::regclass)
        ///</summary>
         [SugarColumn(ColumnName="userid" ,IsPrimaryKey = true ,IsIdentity = true  )]
         public long Userid { get; set; }
        /// <summary>
        /// 部门ID 
        ///</summary>
         [SugarColumn(ColumnName="deptid"    )]
         public long? Deptid { get; set; }
        /// <summary>
        /// 用户账号 
        ///</summary>
         [SugarColumn(ColumnName="username"    )]
         public string Username { get; set; }
        /// <summary>
        /// 用户昵称 
        ///</summary>
         [SugarColumn(ColumnName="nickname"    )]
         public string Nickname { get; set; }
        /// <summary>
        /// 用户类型（00系统用户） 
        ///</summary>
         [SugarColumn(ColumnName="usertype"    )]
         public string Usertype { get; set; }
        /// <summary>
        /// 用户邮箱 
        ///</summary>
         [SugarColumn(ColumnName="email"    )]
         public string Email { get; set; }
        /// <summary>
        /// 手机号码 
        ///</summary>
         [SugarColumn(ColumnName="phonenumber"    )]
         public string Phonenumber { get; set; }
        /// <summary>
        /// 用户性别（0男 1女 2未知） 
        ///</summary>
         [SugarColumn(ColumnName="sex"    )]
         public string Sex { get; set; }
        /// <summary>
        /// 头像地址 
        ///</summary>
         [SugarColumn(ColumnName="avatar"    )]
         public string Avatar { get; set; }
        /// <summary>
        /// 密码 
        ///</summary>
         [SugarColumn(ColumnName="password"    )]
         public string Password { get; set; }
        /// <summary>
        /// 帐号状态（0正常 1停用） 
        ///</summary>
         [SugarColumn(ColumnName="status"    )]
         public string Status { get; set; }
        /// <summary>
        /// 删除标志（0代表存在 2代表删除） 
        ///</summary>
         [SugarColumn(ColumnName="delflag"    )]
         public string Delflag { get; set; }
        /// <summary>
        /// 最后登录IP 
        ///</summary>
         [SugarColumn(ColumnName="loginip"    )]
         public string Loginip { get; set; }
        /// <summary>
        /// 最后登录时间 
        ///</summary>
         [SugarColumn(ColumnName="logindate"    )]
         public DateTime? Logindate { get; set; }
        /// <summary>
        /// 创建者 
        ///</summary>
         [SugarColumn(ColumnName="create_by"    )]
         public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间 
        ///</summary>
         [SugarColumn(ColumnName="create_time"    )]
         public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 更新者 
        ///</summary>
         [SugarColumn(ColumnName="update_by"    )]
         public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间 
        ///</summary>
         [SugarColumn(ColumnName="update_time"    )]
         public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 备注 
        ///</summary>
         [SugarColumn(ColumnName="remark"    )]
         public string Remark { get; set; }
    }
}
