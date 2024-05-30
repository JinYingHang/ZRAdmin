using DTS.Entities;
using System.Collections.Generic;
using ZR.Model;
using ZR.Model.DTS.DTO;

namespace ZR.Service.DTS.IService
{
    public interface ITaskGroupService
    {
        /// <summary>
        /// 任务组列表
        /// </summary>
        public PagedInfo<DtsTaskGroup> SelectTaskGroupList(DtsTaskGroup dtsTaskGroup, Model.PagerInfo pager);
        /// <summary>
        /// 任务列表
        /// </summary>
        public PagedInfo<TaskDto> SelectTaskList(DtsTask dtsTaskGroup, Model.PagerInfo pager);
        /// <summary>
        /// action列表
        /// </summary>
        public PagedInfo<DtsActionBase> SelectActionList(DtsActionBase task, PagerInfo page);

        public List<DtsTaskGroup> QueryTaskGroup(DtsTaskGroup dtsTaskGroup);
        public List<int?> QueryThenTaskIds(DtsTask task);
        public List<int?> QueryFrontTaskIds(DtsTask task);

        public List<DtsTask> QueryTasks(DtsTask dt);
        public List<DtsTaskToTask> QueryTasksToTask(DtsTaskToTask dtTodt);
        public List<DtsParam> GetParam(DtsParam param);
        public List<DtsParam> GetParamByActionId(DtsParam ab);
        public DtsTask GetTaskbyId(DtsTask task);
        public DtsActionBase GetActionById(DtsActionBase ab);


        public int UpdateTask(DtsTask task);
        public int UpdateArgBitch(List<DtsParam> args);
        public int AddOrUpdateTaskGroup(DtsTaskGroup dtsParm);
        public int AddOrUpdateTask(DtsTask task);
        public int AddOrUpdateAction(DtsActionBase task);
        public int AddOrUpdateParam(DtsParam param);

        public int DelTask(DtsTask task );
        public int DelTaskRelation(DtsTaskToTask taskRelationDelete);
        public int DelParam(DtsParam param);

        public int InsertTask2Task(DtsTaskToTask taskRelationIns);
        public int DelActionAndParam(DtsActionBase actionBase);
        public int delTaskGroup(DtsTaskGroup dtsTaskGroup);
    }
}
