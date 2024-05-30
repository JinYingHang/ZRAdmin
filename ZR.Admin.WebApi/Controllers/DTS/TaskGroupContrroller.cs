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
    public class TaskGroupContrroller : BaseController
    {
        private readonly ITaskGroupService _groupService;
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public TaskGroupContrroller(ITaskGroupService dtsService) {
            _groupService = dtsService;
        }

        #region selectList

        [HttpGet("List")]
        public IActionResult SelectList([FromQuery] DtsTaskGroup taskGroup, [FromQuery] PagerInfo pager) {
            try {
                return SUCCESS(_groupService.SelectTaskGroupList(taskGroup, pager));
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("TaskList")]
        public IActionResult SelectTaskList([FromQuery] DtsTask task, [FromQuery] PagerInfo pager) {
            try {
                return SUCCESS(_groupService.SelectTaskList(task, pager));
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("ActionList")]
        public IActionResult SelectActionList([FromQuery] DtsActionBase action, PagerInfo page) {
            try {
                return SUCCESS(_groupService.SelectActionList(action, page));
            }
            catch (Exception) {
                throw;
            }
        }

        #endregion

        #region Get

        [HttpGet("getTaskbyId")]
        public IActionResult GetTaskbyId([FromQuery] DtsTask task) {
            try {
                return SUCCESS(_groupService.GetTaskbyId(task));
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("getActionByTaskId")]
        public IActionResult GetActionByTaskId([FromQuery] DtsTask task) {
            try {
                var re = _groupService.GetTaskbyId(task);
                if (re.ActionId != null) {
                    var reaction = _groupService.GetActionById(new DtsActionBase() { Id = (int)re.ActionId });
                    if (reaction!=null) {
                        return SUCCESS(reaction);
                    }
                    else {
                        return SUCCESS("Action为空");
                    }
                }
                else {
                    return SUCCESS("err");
                }
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("getParamByActionId")]
        public IActionResult GetParamByActionId([FromQuery] DtsParam db) {
            try {

                return SUCCESS(_groupService.GetParamByActionId(db));
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("getActionById")]
        public IActionResult GetActionById([FromQuery] DtsActionBase action) {
            try {
                return SUCCESS(_groupService.GetActionById(action));
            }
            catch (Exception) {
                throw;
            }
        }
        [HttpGet("getParamById")]
        public IActionResult GetParam([FromQuery] DtsParam param) {
            try {
                return SUCCESS(_groupService.GetParam(param));
            }
            catch (Exception) {
                throw;
            }
        }

        #endregion

        #region Query

        [HttpGet("QueryTasks")]
        public IActionResult QueryTasks([FromQuery] DtsTask task) {
            try {
                return SUCCESS(_groupService.QueryTasks(task));
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("queryThenTaskIds")]
        public IActionResult QueryThenTaskIds([FromQuery] DtsTask task) {
            try {
                return SUCCESS(_groupService.QueryThenTaskIds(task));
            }
            catch (Exception) {
                throw;
            }
        }
        [HttpGet("queryFrontTaskIds")]
        public IActionResult QueryFrontTaskIds([FromQuery] DtsTask task) {
            try {
                return SUCCESS(_groupService.QueryFrontTaskIds(task));
            }
            catch (Exception) {
                throw;
            }
        }

        #endregion

        #region  AddOrUpdate
        [HttpPost("UpdateTaskArg")]
        public int UpdateTaskArg([FromBody] TaskArgDto taskArg) {
            DtsTask task = _groupService.GetTaskbyId(new DtsTask() { Id = taskArg.taskId });
            List<Param> inParam = new List<Param>();
            List<Param> outParam = new List<Param>();
            if (taskArg.args != null && taskArg.args.Count > 0) {
                foreach (var arg in taskArg.args) {
                    if (arg.ActionType.Equals("出参")) {
                        outParam.Add(new Param {
                            Id=arg.Id,
                            ParamCode = arg.ParamCode,
                            ParamType = arg.ParamType,
                            ParamValue = arg.ParamValue,
                            ParamCodeMap = arg.ParamCodeMap,
                            ActionType = "出参"
                        });
                    }
                    else if (arg.ActionType.Equals("入参")) {
                        inParam.Add(new Param {
                            Id = arg.Id,
                            ParamCode = arg.ParamCode,
                            ParamType = arg.ParamType,
                            ParamValue = arg.ParamValue,
                            ParamCodeMap = arg.ParamCodeMap,
                            ActionType = "入参"
                        });
                    }
                }
                task.InArg = JsonConvert.SerializeObject(inParam);
                task.OutArg = JsonConvert.SerializeObject(outParam);
            }
            else {
                task.InArg = "";
                task.OutArg = "";
            }
            task.ActionId = taskArg.actionId;
            return _groupService.UpdateTask(task);
        }
        [HttpGet("UpdateTaskParam")]
        public IActionResult UpdateTaskParam([FromQuery] DtsTask arg) {
            try {
                DtsTask dtsTask = _groupService.GetTaskbyId(arg);
                dtsTask.ActionId = arg.ActionId;
                dtsTask.InArg = arg.InArg;
                dtsTask.OutArg = arg.OutArg;
                return SUCCESS(_groupService.UpdateTask(dtsTask));
            }
            catch (Exception) {
                throw;
            }
        }
        [HttpGet("UpdateArgBitch")]
        public IActionResult UpdateArgBitch([FromQuery] List<DtsParam> args) {
            try {
                return SUCCESS(_groupService.UpdateArgBitch(args));
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("AddOrUpdateAction")]
        public IActionResult AddOrUpdateAction([FromQuery] DtsActionBase action) {
            try {
                return SUCCESS(_groupService.AddOrUpdateAction(action));
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("addOrUpdate")]
        public IActionResult AddOrUpdateTaskGroup([FromQuery] DtsTaskGroup taskGroup) {
            try {
                return SUCCESS(_groupService.AddOrUpdateTaskGroup(taskGroup));
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpPost("AddOrUpdateTaskBatch")]
        public IActionResult AddOrUpdateTaskBatch([FromBody] DtsTask task) {
            try {
                //根据ip创建多个任务
                string[] ips = task.Ip.Split(',');

                foreach (string ip in ips) {
                    task.Ip = ip;
                    int id = _groupService.AddOrUpdateTask(task);
                    if (task.Sort == null && (id != 1 || id != 0)) {
                        task.Sort = task.Id;
                        _groupService.AddOrUpdateTask(task);
                    }
                }
                return SUCCESS("");
            }
            catch (Exception) {
                throw;
            }
        }
        [HttpPost("AddOrUpdateTask")]
        public IActionResult AddOrUpdateTask([FromBody] DtsTask task) {
            try {
                if (task==null) {
                    return SUCCESS("errr");
                }
                int id = _groupService.AddOrUpdateTask(task);
                if (task.Sort == null && (id != 1 || id != 0)) {
                    task.Sort = task.Id;
                    _groupService.AddOrUpdateTask(task);
                }
                return SUCCESS(id);
            }
            catch (Exception) {
                throw;
            }
        }
        [HttpGet("AddOrUpdateParam")]
        public IActionResult AddOrUpdateParam([FromQuery] DtsParam param) {
            try {
                return SUCCESS(_groupService.AddOrUpdateParam(param));
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpPost("AddOrUpdateThenTasks")]
        public IActionResult AddOrUpdateThenTasks([FromBody] ThenTasksDto thenTask) {
            try {
                DtsTaskToTask taskRelationDelete = new DtsTaskToTask() {
                    FrontDetailsTaskId = thenTask.FrontDetailsTaskId,
                    TaskGroupId = thenTask.TaskGroupId,
                };
                //1.根据任务组和id，清空关联关系
                _groupService.DelTaskRelation(taskRelationDelete);

                //2.重新建立关联关系
                foreach (var thenId in thenTask.ThenDetailsTaskIds) {
                    DtsTaskToTask taskRelationIns = new DtsTaskToTask() {
                        FrontDetailsTaskId = thenTask.FrontDetailsTaskId,
                        ThenDetailsTaskId = thenId,
                        TaskGroupId = thenTask.TaskGroupId,
                    };
                    _groupService.InsertTask2Task(taskRelationIns);
                }

                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }
        [HttpPost("AddOrUpdateFrontTasks")]
        public IActionResult AddOrUpdateFrontTasks([FromBody] ThenTasksDto frontTaskDto) {
            try {
                DtsTaskToTask taskRelationDelete = new() {
                    ThenDetailsTaskId = frontTaskDto.ThenDetailsTaskId,
                    TaskGroupId = frontTaskDto.TaskGroupId,
                };
                //1.根据任务组和id，清空关联关系
                _groupService.DelTaskRelation(taskRelationDelete);

                //2.重新建立关联关系
                foreach (var frontId in frontTaskDto.FrontDetailsTaskIds) {
                    DtsTaskToTask taskRelationIns = new DtsTaskToTask() {
                        FrontDetailsTaskId = frontId,
                        ThenDetailsTaskId = frontTaskDto.ThenDetailsTaskId,
                        TaskGroupId = frontTaskDto.TaskGroupId,
                    };
                    _groupService.InsertTask2Task(taskRelationIns);
                }

                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }



        #endregion

        #region Delete

        [HttpGet("delTask")]
        public IActionResult delTask(DtsTask dtsTask) {
            try {
                var task = _groupService.QueryTasks(dtsTask);
                if (task.Count != 0) {
                    _groupService.DelTaskRelation(new DtsTaskToTask { TaskGroupId = task[0].TaskGroupId, FrontDetailsTaskId = task[0].Id, ThenDetailsTaskId = task[0].Id });
                }
                int i = _groupService.DelTask(new DtsTask() { Id = dtsTask.Id });
                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("delTaskGroup")]
        public IActionResult delTaskGroup(DtsTaskGroup taskGroup) {
            try {
                //删除任务组
                int i = _groupService.delTaskGroup(new DtsTaskGroup() { Id = taskGroup.Id });
                //删除任务
                int del = _groupService.DelTask(new DtsTask() { TaskGroupId = taskGroup.Id });
                //删除关联关系
                int delT = _groupService.DelTaskRelation(new DtsTaskToTask() { TaskGroupId = taskGroup.Id });
                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("DelArg")]
        public IActionResult DelArg(DtsParam param) {
            try {
                int i = _groupService.DelParam(param);
                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("DelActionAndParam")]
        public IActionResult DelActionAndParam(DtsActionBase actionBase) {
            try {
                _groupService.DelActionAndParam(actionBase);
                _groupService.DelParam(new DtsParam() { ActionId = actionBase.Id });
                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }
        #endregion



        [HttpGet("resetTaskGroupById")]
        public IActionResult ResetTaskGroupById(DtsTaskGroup dtsTaskGroup) {
            try {
                var group = _groupService.QueryTaskGroup(dtsTaskGroup);
                if (group == null || group.Count < 1) {
                    return SUCCESS("未找到对应的任务组");
                }
           

                List<DtsTask> tasks = _groupService.QueryTasks(new DtsTask() { TaskGroupId = dtsTaskGroup.Id });
                foreach (DtsTask task in tasks) {
                    task.Status = "未发送";
                    task.FrontCount = -1;
                    _groupService.UpdateTask(task);
                }
                group[0].EndTime = DateTime.Now;
                group[0].Status = "待执行";
                group[0].TaskProgress = "0/0";
                _groupService.AddOrUpdateTaskGroup(group[0]);
                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }
        [HttpGet("ReStarTaskApi")]
        public IActionResult ReStarTaskApi(DtsTask dtsTask) {
            try {
                List<DtsTask> tasks = _groupService.QueryTasks(new DtsTask() { Id = dtsTask.Id });
                foreach (DtsTask task in tasks) {
                    task.Status = "未发送";
                    task.FrontCount = 0;
                    task.FrontCondition = string.Empty;
                    _groupService.UpdateTask(task);
                }
                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }
        [HttpGet("unboundTaskApi")]
        public IActionResult UnboundTaskApi(DtsTask dtsTask) {
            try {
                List<DtsTask> tasks = _groupService.QueryTasks(new DtsTask() { Id = dtsTask.Id });
                foreach (DtsTask task in tasks) {
                    task.TaskGroupId = null;
                    _groupService.UpdateTask(task);
                }
                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }
        [HttpGet("CopyTaskApi")]
        public IActionResult CopyTaskApi(DtsTask dtsTask) {
            try {
                //共有四个工作 1.复制task 2.复制action 3.复制params 4.task、action、params之间的关系绑定

                //找到要复制的任务
                DtsTask dtsTask1 = _groupService.GetTaskbyId(dtsTask);
                if (dtsTask1 == null) return SUCCESS("失败，找不到源任务");

                ////找到对应的Action
                //if (dtsTask1.ActionId != null) {
                //    DtsActionBase dtsActionBase = _groupService.GetActionById(new DtsActionBase() { Id = (int)dtsTask1.ActionId });
                //    //找到对应的params
                //    List<DtsParam> dtsParams = _groupService.GetParamByActionId(new DtsParam() { ActionId = dtsActionBase.Id });//等待复制的params
                //    //复制任务并获取新的actionID
                //    dtsActionBase.Id = -1;
                //    int actionId = _groupService.AddOrUpdateAction(dtsActionBase);
                //    //复制参数并绑定新的actionID
                //    foreach (var param in dtsParams) {
                //        param.Id = -1;
                //        param.ActionId = actionId;
                //        _groupService.AddOrUpdateParam(param);
                //    }
                //    //新任务绑定新的actionId
                //    dtsTask1.ActionId = actionId;
                //}

                //复制action
                dtsTask1.Id = -1;
                dtsTask1.Status = "未发送";
                if (dtsTask.TaskGroupId != null) {
                    dtsTask1.TaskGroupId = dtsTask.TaskGroupId;
                }
                int newTaskId = _groupService.AddOrUpdateTask(dtsTask1);
                return SUCCESS(newTaskId);
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpGet("copyTaskGroupApi")]
        public IActionResult CopyTaskGroup(DtsTaskGroup taskGroupArg) {
            try {
                // 1.复制taskGroup  2.复制任务 3.复制任务之间的关系
                DtsTaskGroup taskGroup = _groupService.QueryTaskGroup(taskGroupArg)[0];
                if (taskGroup == null) {
                    return SUCCESS("找不到任务组");
                }
                else {
                    Dictionary<int, int> taskRelations = new Dictionary<int, int>(); //k:oldId v:newId

                    //复制taskGroup
                    taskGroup.Id = -1;
                    taskGroup.Status = "待确认";
                    int newTaskGroupId = _groupService.AddOrUpdateTaskGroup(taskGroup);
                    //复制任务
                    List<DtsTask> tasks = _groupService.QueryTasks(new DtsTask() { TaskGroupId = taskGroupArg.Id });
                    foreach (DtsTask task in tasks) {
                        int oldId = task.Id;
                        task.TaskGroupId = newTaskGroupId;
                        ContentResult newIdObj = (ContentResult)CopyTaskApi(task);
                        //添加原任务和复制任务ip映射
                        var jo = JObject.Parse(newIdObj.Content.ToString());
                        int newId = int.Parse(jo["data"].ToString());
                        taskRelations.Add(oldId, newId);
                    }
                    //复制任务之间的关联关系
                    List<DtsTaskToTask> t2ts = _groupService.QueryTasksToTask(new DtsTaskToTask() { TaskGroupId = taskGroupArg.Id });
                    foreach (DtsTaskToTask t2t in t2ts) {
                        t2t.Id = -1;
                        t2t.FrontDetailsTaskId = taskRelations[(int)t2t.FrontDetailsTaskId];
                        t2t.ThenDetailsTaskId = taskRelations[(int)t2t.ThenDetailsTaskId];
                        t2t.TaskGroupId = newTaskGroupId;
                        int i = _groupService.InsertTask2Task(t2t);
                    }
                }
                return SUCCESS("成功");
            }
            catch (Exception) {
                throw;
            }
        }
        [HttpGet("StopTaskGroupApi")]
        public IActionResult StopTaskGroupApi(DtsTaskGroup taskGroupArg) {
            try {
                List<DtsTaskGroup> taskgroup = _groupService.QueryTaskGroup(new DtsTaskGroup { Id = taskGroupArg.Id });
                if (taskgroup.Count > 0) {
                    taskgroup[0].Status = "待确认";
                    _groupService.AddOrUpdateTaskGroup(taskgroup[0]);
                }
                List<DtsTask> tasks = _groupService.QueryTasks(new DtsTask() { TaskGroupId = taskGroupArg.Id });
                foreach (var task in tasks) {
                    task.FrontCount = -1;
                    task.Status = "未发送";
                    _groupService.AddOrUpdateTask(task);
                }
                return SUCCESS("成功");

            }
            catch (Exception) {
                throw;
            }
        }
    }
}
