using DTS.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZR.Model;
using ZR.Model.DTS.DTO;
using ZR.Service.DTS.IService;

namespace ZR.Admin.WebApi.Controllers.DTS
{

    [Route("dts/taskGroup")]
    public class VirtualTaskContrroller : BaseController
    {
        private readonly IVirtualTaskService _virtualTaskService;
        private readonly ITaskGroupService _groupService;

        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public VirtualTaskContrroller(IVirtualTaskService virtualTaskService, ITaskGroupService groupService) {
            _virtualTaskService = virtualTaskService;
            _groupService = groupService;
        }

        /// <summary>
        /// 添加虚拟任务，创建子任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateVirtualTask")]
        public IActionResult AddOrUpdateVirtualTask([FromBody] DtsTask task) {
            try {
                if (task.TaskType != 1) {
                    return SUCCESS("不是虚拟任务，不处理");
                }
                var re = _virtualTaskService.AddOrUpdateVirtualTask(task);
                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }
    }
}
