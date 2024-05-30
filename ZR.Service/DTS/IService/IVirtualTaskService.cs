using DTS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZR.Service.DTS.IService
{
    public interface IVirtualTaskService
    {
        public bool AddOrUpdateVirtualTask(DtsTask newVirtualTask);
    }
}
