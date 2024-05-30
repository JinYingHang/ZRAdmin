using DTS.Entities;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using ZR.Model;
using ZR.Model.DTS.DTO;
using ZR.Model.System.Dto;
using ZR.Repository;
using ZR.Service.DTS.IService;

namespace ZR.Service.DTS
{
    [AppService(ServiceType = typeof(ITaskGroupService), ServiceLifetime = LifeTime.Transient)]
    public class TaskGroupService : BaseService<DtsTaskGroup>, ITaskGroupService
    {
        public PagedInfo<DtsTaskGroup> SelectTaskGroupList(DtsTaskGroup dtg, PagerInfo pager) {
            try {
                //开始拼装查询条件
                var predicate = Expressionable.Create<DtsTaskGroup>();
                predicate = predicate.AndIF(dtg.Id != 0, it => it.Id == dtg.Id);
                predicate = predicate.AndIF(dtg.PeriodicExecution != null, it => it.PeriodicExecution == dtg.PeriodicExecution);
                predicate = predicate.AndIF(dtg.UserIdNotic != null, it => it.UserIdNotic == dtg.UserIdNotic);
                predicate = predicate.AndIF(dtg.GroupName.IfNotEmpty(), it => it.GroupName.Contains(dtg.GroupName));
                predicate = predicate.AndIF(dtg.Status.IfNotEmpty(), it => it.Status.Contains(dtg.Status));
                predicate = predicate.AndIF(dtg.Template.IfNotEmpty(), it => it.Template == dtg.Template);
                var query = Queryable().Where(predicate.ToExpression());
                return query.ToPage(pager);
            }
            catch (Exception) {
                throw;
            }
        }
        public PagedInfo<TaskDto> SelectTaskList(DtsTask dtsTask, PagerInfo pager) {
            try {
                //开始拼装查询条件
                var predicate = Expressionable.Create<DtsTask>();
                predicate = predicate.AndIF(dtsTask.TaskGroupId != null, (t => t.TaskGroupId == dtsTask.TaskGroupId));
                predicate = predicate.AndIF(dtsTask.TaskName != null, (t => t.TaskName.Contains( dtsTask.TaskName)));
                predicate = predicate.AndIF(dtsTask.Ip != null, (t => t.Ip.Contains( dtsTask.Ip)));
                predicate = predicate.AndIF(dtsTask.Appname != null, (t => t.Appname.Contains(dtsTask.Appname)));

                var sql = "	SELECT\n" +
                            "		tt.front_details_task_id as FrontTaskId,\n" +
                            "		string_agg ( t2.task_name, ', ' ) AS ThenTaskNames \n" +
                            "	FROM\n" +
                            "		dts_task_to_task AS tt\n" +
                            "		INNER JOIN dts_task AS t2 ON tt.then_details_task_id = t2.\"id\" \n" +
                            "	WHERE\n" +
                            "		t2.task_group_id = tt.task_group_id \n" +
                            "	GROUP BY\n" +
                            "		tt.front_details_task_id ";

                var query1 = Context.CopyNew().Queryable<DtsTask>()
                    .LeftJoin<DtsTaskGroup>((t, tg) => t.TaskGroupId == tg.Id)
                    .LeftJoin<DtsActionBase>((t, tg, ab) => t.ActionId == ab.Id)
                    .LeftJoin(Context.CopyNew().SqlQueryable<TasktoTaskDto>(sql), (t, tg, ab, d) => t.Id == d.FrontTaskId)
                    .Where(predicate.ToExpression())
                    .OrderBy((t, tg, ab, d) => t.Sort)
                    .Select<TaskDto>();


                return query1.ToPage(pager);
            }
            catch (Exception) {
                throw;
            }
        }
        public PagedInfo<DtsActionBase> SelectActionList(DtsActionBase action, PagerInfo pager) {
            //开始拼装查询条件
            var predicate = Expressionable.Create<DtsActionBase>();
            predicate = predicate.AndIF(action.ActionCode != null, (t => t.ActionCode.Contains(action.ActionCode)));
            predicate = predicate.AndIF(action.ActionName != null, (t => t.ActionName.Contains(action.ActionName)));
            predicate = predicate.AndIF(action.AppName != null, (t => t.AppName.Contains(action.AppName)));
            predicate = predicate.AndIF(action.Comments != null, (t => t.Comments.Contains(action.Comments)));
            predicate = predicate.AndIF(action.Id != 0, (t => t.Id==action.Id));

            var query = Context.CopyNew().Queryable<DtsActionBase>().Where(predicate.ToExpression());
            return query.ToPage(pager);
        }


        public List<DtsTaskGroup> QueryTaskGroup(DtsTaskGroup dtsTaskGroup) {
            try {
                var predicate = Expressionable.Create<DtsTaskGroup>();
                predicate = predicate.AndIF(dtsTaskGroup.Id != 0, it => it.Id == dtsTaskGroup.Id);

                return Context.CopyNew().Queryable<DtsTaskGroup>().Where(predicate.ToExpression()).ToList();
            }
            catch (Exception) {

                throw;
            }
        }
        public List<int?> QueryThenTaskIds(DtsTask task) {
            try {
                var predicate = Expressionable.Create<DtsTaskToTask>();
                predicate = predicate.AndIF(task.Id != 0, it => it.FrontDetailsTaskId == task.Id);
                predicate = predicate.AndIF(task.TaskGroupId != 0, it => it.TaskGroupId == task.TaskGroupId);
                return Context.CopyNew().Queryable<DtsTaskToTask>().Where(predicate.ToExpression()).Select(x => x.ThenDetailsTaskId).ToList();
            }
            catch (Exception) {
                throw;
            }
        }
        public List<int?> QueryFrontTaskIds(DtsTask task) {
            try {
                var predicate = Expressionable.Create<DtsTaskToTask>();
                predicate = predicate.AndIF(task.Id != 0, it => it.ThenDetailsTaskId == task.Id);
                predicate = predicate.AndIF(task.TaskGroupId != 0, it => it.TaskGroupId == task.TaskGroupId);
                return Context.CopyNew().Queryable<DtsTaskToTask>().Where(predicate.ToExpression()).Select(x => x.FrontDetailsTaskId).ToList();
            }
            catch (Exception) {
                throw;
            };
        }
        public List<DtsTaskToTask> QueryTasksToTask(DtsTaskToTask dtTodt) {
            try {
                var predicate = Expressionable.Create<DtsTaskToTask>();
                predicate = predicate.AndIF(dtTodt.TaskGroupId != 0, it => it.TaskGroupId == dtTodt.TaskGroupId);
                return Context.CopyNew().Queryable<DtsTaskToTask>().Where(predicate.ToExpression()).ToList();
            }
            catch (Exception) {

                throw;
            }
        }


        public DtsTask GetTaskbyId(DtsTask task) {
            try {

                var predicate = Expressionable.Create<DtsTask>();

                predicate = predicate.AndIF(task.Id != 0, it => it.Id == task.Id);

                return Context.CopyNew().Queryable<DtsTask>().Where(predicate.ToExpression()).First();
            }
            catch (Exception) {

                throw;
            }
        }
        public List<DtsTask> QueryTasks(DtsTask dts) {
            try {
                var predicate = Expressionable.Create<DtsTask>();
                predicate = predicate.AndIF(dts.TaskGroupId != null, it => it.TaskGroupId == dts.TaskGroupId);
                predicate = predicate.AndIF(dts.Id != 0, it => it.Id == dts.Id);

                var re = Context.CopyNew().Queryable<DtsTask>().OrderBy(x => x.Sort).Where(predicate.ToExpression()).ToList();
                return re;
            }
            catch (Exception) {

                throw;
            }
        }
        public DtsActionBase GetActionById(DtsActionBase action) {
            try {
                var predicate = Expressionable.Create<DtsActionBase>();
                predicate = predicate.AndIF(action.Id != 0, it => it.Id == action.Id);
              var a=  Context.CopyNew().Queryable<DtsActionBase>().Where(predicate.ToExpression()).First();
                return a;
            }
            catch (Exception) {
                throw;
            }
        }
        public List<DtsParam> GetParamByActionId(DtsParam dp) {
            try {
                var predicate = Expressionable.Create<DtsParam>();
                predicate = predicate.AndIF(dp.ActionId != 0, it => it.ActionId == dp.ActionId);

                var re = Context.CopyNew().Queryable<DtsParam>().Where(predicate.ToExpression()).ToList();
                return re;
            }
            catch (Exception) {
                throw;
            }
        }
        public List<DtsParam> GetParam(DtsParam param) {
            try {
                var predicate = Expressionable.Create<DtsParam>();
                predicate = predicate.AndIF(param.Id != 0, it => it.Id == param.Id);
                var re = Context.CopyNew().Queryable<DtsParam>().Where(predicate.ToExpression()).ToList();
                return re;
            }
            catch (Exception) {

                throw;
            }
        }

  
        public int UpdateTask(DtsTask dts) {
            try {
                return Context.CopyNew().Updateable(dts).ExecuteCommand();
            }
            catch (Exception) {
                throw;
            }
        }
        public int UpdateArgBitch(List<DtsParam> args) {
            try {
                return Context.CopyNew().Updateable(args).ExecuteCommand();
            }
            catch (Exception) {
                throw;
            }
        }
        public int AddOrUpdateTaskGroup(DtsTaskGroup group) {
            try {
                DtsTaskGroup dtsTaskGroup = Queryable().Where(x => x.Id == group.Id).ToArray().FirstOrDefault();
                if (dtsTaskGroup != null) {
                    //var config = new MapperConfiguration(cfg => {
                    //    cfg.CreateMap<DtsTaskGroup, DtsTaskGroup>();
                    //});
                    //var mapper = config.CreateMapper();
                    //mapper.Map(group, dtsTaskGroup);
                    //dtsTaskGroup.UserIdNotic = group.UserIdNotic ?? dtsTaskGroup.UserIdNotic;
                    //dtsTaskGroup.GroupName = group.GroupName ?? dtsTaskGroup.GroupName;
                    //dtsTaskGroup.ExecuteTimeRanges = group.ExecuteTimeRanges ??= dtsTaskGroup.ExecuteTimeRanges;
                    //dtsTaskGroup.ExecuteDuration = group.ExecuteDuration ?? dtsTaskGroup.ExecuteDuration;
                    //dtsTaskGroup.Template = group.Template ?? dtsTaskGroup.Template;
                    //dtsTaskGroup.ExecuteDuration = group.ExecuteDuration ?? dtsTaskGroup.ExecuteDuration;
                    //dtsTaskGroup.Content = group.Content ?? dtsTaskGroup.Content;
                    //dtsTaskGroup.Status = group.Status ?? dtsTaskGroup.Status;
                    //dtsTaskGroup.EndTime = group.EndTime ?? dtsTaskGroup.EndTime;
                    return Context.CopyNew().Updateable(group).ExecuteCommand();
                }
                else {
                    return Context.CopyNew().Insertable(group).ExecuteReturnIdentity();

                }
            }
            catch (Exception) {
                throw;
            }
        }
        public int AddOrUpdateTask(DtsTask dts) {
            try {
                DtsTask task = Context.CopyNew().Queryable<DtsTask>().Where(x => x.Id == dts.Id).ToArray().FirstOrDefault();
                if (task != null) {
                    //action.ActionId = dts.ActionId ?? action.ActionId;
                    //action.TaskGroupId = dts.TaskGroupId ?? action.TaskGroupId;
                    //action.AllowFailCount = dts.AllowFailCount ?? action.AllowFailCount;
                    //action.Appname = dts.Appname ?? action.Appname;
                    //action.Status = dts.Status ?? action.Status;
                    //action.Content = dts.Content ?? action.Content;
                    //action.EndTime = dts.EndTime ?? action.EndTime;
                    //action.ExecuteDuration = dts.ExecuteDuration ?? action.ExecuteDuration;
                    //action.ExecuteTime = dts.ExecuteTime ?? action.ExecuteTime;
                    //action.FrontCount = dts.FrontCount ?? action.FrontCount;
                    //action.Fundid = dts.Fundid ?? action.Fundid;
                    //action.FillCount = dts.FillCount ?? action.FillCount;
                    //action.Ip = dts.Ip ?? action.Ip;
                    //action.TaskName = dts.TaskName ?? action.TaskName;
                    //action.InArg = dts.InArg ?? action.InArg;
                    //action.Sort = dts.Sort ?? action.Sort;
                    //action.BeginTime = dts.BeginTime ?? action.BeginTime;
                    //action.OutTime = dts.OutTime ?? action.OutTime;
                    //action.OutTimeSendInterval = dts.OutTimeSendInterval ?? action.OutTimeSendInterval;
                    //if (!string.IsNullOrWhiteSpace(dts.InArg)) {
                    //    dts.InArg = dts.InArg.Replace("]", "").Replace("[", "");
                    //}


                    //if (!string.IsNullOrWhiteSpace(dts.OutArg)) {
                    //    dts.OutArg = dts.OutArg.Replace("]", "").Replace("[", "");
                    //}

                    return Context.CopyNew().Updateable(dts).ExecuteCommand();
                }
                else {
                    return Context.CopyNew().Insertable(dts).ExecuteReturnIdentity();
                }
            }
            catch (Exception) {
                throw;
            }
        }
        public int AddOrUpdateAction(DtsActionBase action) {
            try {
                DtsActionBase actionBase = Context.CopyNew().Queryable<DtsActionBase>().Where(x => x.Id == action.Id).First();
                if (actionBase != null) {
                    //actionBase.ActionCode = action.ActionCode ?? actionBase.ActionCode;
                    //actionBase.Status = action.Status ?? actionBase.Status;
                    //actionBase.ActionName = action.ActionName ?? actionBase.ActionName;
                    //actionBase.AppName = action.AppName ?? actionBase.AppName;
                    //actionBase.Comments = action.Comments ?? actionBase.Comments;
                    //actionBase.CreateTime = action.CreateTime ?? actionBase.CreateTime;
                    //actionBase.LastTime = action.LastTime ?? actionBase.LastTime;
                    return Context.CopyNew().Updateable(action).ExecuteCommand();
                }
                else {
                    return Context.CopyNew().Insertable(action).ExecuteReturnIdentity(); 
                }
            }
            catch (Exception) {
                throw;
            }
        }
        public int AddOrUpdateParam(DtsParam param) {
            try {
                DtsParam paramObj = Context.CopyNew().Queryable<DtsParam>().Where(x => x.Id == param.Id).First();
                if (paramObj != null) {
                    return Context.CopyNew().Updateable(param).ExecuteCommand();
                }
                else {
                    return Context.CopyNew().Insertable(param).ExecuteCommand();
                }
            }
            catch (Exception) {
                throw;
            }
        }


        public int DelTask(DtsTask task) {
            try {
                var predicate = Expressionable.Create<DtsTask>();
                predicate = predicate.AndIF(task.Id != 0, it => it.Id == task.Id);
                predicate = predicate.AndIF(task.TaskGroupId != null, it => it.TaskGroupId == task.TaskGroupId);
                return Context.CopyNew().Deleteable<DtsTask>().Where(predicate.ToExpression()).ExecuteCommand();
            }
            catch (Exception) {

                throw;
            }
        }
        public int DelTaskRelation(DtsTaskToTask taskRelationDelete) {
            try {
                var predicate = Expressionable.Create<DtsTaskToTask>();
                if (taskRelationDelete.FrontDetailsTaskId != null && taskRelationDelete.FrontDetailsTaskId != null&&taskRelationDelete.TaskGroupId!=null) {
                    return Context.CopyNew()
                        .Deleteable<DtsTaskToTask>()
                        .Where(x=>x.TaskGroupId== taskRelationDelete.TaskGroupId
                                                                     && (
                                                                            x.ThenDetailsTaskId==taskRelationDelete.ThenDetailsTaskId
                                                                          ||x.FrontDetailsTaskId==taskRelationDelete.FrontDetailsTaskId
                                                                          ))
                        .ExecuteCommand();
                }
                else {
                    predicate = predicate.AndIF(taskRelationDelete.ThenDetailsTaskId != null, it => it.ThenDetailsTaskId == taskRelationDelete.ThenDetailsTaskId);
                    predicate = predicate.AndIF(taskRelationDelete.FrontDetailsTaskId != null, it => it.FrontDetailsTaskId == taskRelationDelete.FrontDetailsTaskId);
                    predicate = predicate.AndIF(taskRelationDelete.TaskGroupId != null, it => it.TaskGroupId == taskRelationDelete.TaskGroupId);

                    return Context.CopyNew().Deleteable<DtsTaskToTask>()
                        .Where(predicate.ToExpression())
                        .ExecuteCommand();
                }
   

            }
            catch (Exception) {
                throw;
            }
        }
        public int DelParam(DtsParam param) {
            try {
                return Context.CopyNew().Deleteable(param).ExecuteCommand();
            }
            catch (Exception) {
                throw;
            }
        }
        public int DelActionAndParam(DtsActionBase actionBase) {
            try {
                Context.CopyNew().Deleteable<DtsParam>().Where(x=>x.ActionId==actionBase.Id).ExecuteCommand();
                return Context.CopyNew().Deleteable(actionBase).ExecuteCommand();
            }
            catch (Exception) {
                throw;
            }
        }
        public int delTaskGroup(DtsTaskGroup dtsTaskGroup) {
            try {
                return Context.CopyNew().Deleteable(dtsTaskGroup).ExecuteCommand();
            }
            catch (Exception) {

                throw;
            }
        }

        public int InsertTask2Task(DtsTaskToTask taskRelationIns) {
            try {
                return Context.CopyNew().Insertable(taskRelationIns).ExecuteCommand();
            }
            catch (Exception) {
                throw;
            }
        }


    }
}
