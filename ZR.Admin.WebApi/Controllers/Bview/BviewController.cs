using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Drawing;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using static Infrastructure.GlobalConstant;

namespace ZR.Admin.WebApi.Controllers.Bview
{
    [Route("bview")]
    public class BviewController : BaseController
    {
       
      
        public class Columns
        {
            public string label { get; set; }
            public string prop { get; set; }
        }
        
        [AllowAnonymous]
        [HttpGet("getViewCol")]
        public IActionResult GetCol()
        {
            List<Columns> list=new List<Columns>();
            string cols = "ip,v10d,d0,h0,c0,n0,e0,d1d,d2d,d10d,h1d,h2d,h10d,c1d,c2d,c10d,n1d";
            foreach (var t in cols.Split(','))
            {
                Columns c=new Columns();
                c.label = t;
                
                c.prop = t;//props;  
                list.Add(c);    
            }
            return SUCCESS(list, TIME_FORMAT_FULL);
        }
        [AllowAnonymous]
        [HttpGet("getViewRow")]
        public IActionResult GetRow()
        {
            //主要是为了固定IP，生成IP

            //GlobalConstant.rows = new SelectList<Dictionary<string, Props>>();
            //for (int i = 0; i < 10; i++)
            //{
            //    Dictionary<string, Props> row =new Dictionary<string, Props>();
            //    byte[] addressBytes = new byte[4];
            //    Random random = new Random();
            //    random.NextBytes(addressBytes);
            //    IPAddress ipAddress = new IPAddress(addressBytes);
            //    Props props = new Props();
            //    props.value = ipAddress.ToString();
            //    props.name = ipAddress.ToString();
            //    row.Add("ip", props);
            //    rows.Add(row);
            //}
            return SUCCESS(GlobalConstant.rows, TIME_FORMAT_FULL);
        }
    }
}
