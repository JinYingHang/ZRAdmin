using DTS.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZR.Service.DTS.IService;

namespace ZR.Service.DTS
{
    public class VirtualTaskService : BaseService<DtsTask>, IVirtualTaskService
    {
        public bool AddOrUpdateVirtualTask(DtsTask newVirtualTask) {
            try {
                //虚拟任务
                DtsTask oldVirtualTask = GetById(newVirtualTask.Id);
                if (oldVirtualTask != null) {
                    newVirtualTask.Id = oldVirtualTask.Id;
                    Updateable(newVirtualTask);
                }
                else {
                    int newId = Add(newVirtualTask);
                    newVirtualTask.Id = newId;
                }

                //1.根据虚任务的ip集合，判断是否要新增或者删除子任务
                List<string> newVirtualTaskips = newVirtualTask.Ip.Replace("，", ",").Split(',').ToList();
                List<string> oldVirtualTaskips = oldVirtualTask.Ip.Replace("，", ",").Split(',').ToList();
                List<string> addTaskips = newVirtualTaskips.Except(oldVirtualTaskips).ToList();
                List<string> delTaskips = oldVirtualTaskips.Except(newVirtualTaskips).ToList();
                //1.新增子任务
                foreach (var ip in addTaskips) {
                    //1.1如果有则直接删掉重新添加（理论不会有，保险）
                    DtsTask oldSonTask = Queryable().Where(task => task.VirtualId == newVirtualTask.Id && task.Ip == ip).First();
                    if (oldSonTask != null)
                        DeleteById(oldSonTask.VirtualId);
                    //1.2新增子任务
                    DtsTask sonTask = newVirtualTask.Clone();
                    sonTask.Ip = ip;
                    sonTask.VirtualId = newVirtualTask.Id;
                    int id = Add(sonTask);
                }
                //2.删除子任务
                foreach (var ip in delTaskips) {
                    //1.1如果有则直接删掉重新添加（理论不会有，保险）
                    DtsTask oldSonTask = Queryable().Where(task => task.VirtualId == newVirtualTask.Id && task.Ip == ip).First();
                    if (oldSonTask != null)
                        DeleteById(oldSonTask.VirtualId);
                }
                //3.更新子任务
                foreach (var ip in newVirtualTaskips) {
                    DtsTask oldSonTask = Queryable().Where(task => task.VirtualId == newVirtualTask.Id && task.Ip == ip).First();
                    if (oldSonTask != null) {
                        DtsTask sonTask = newVirtualTask.Clone();
                        sonTask.Id = oldSonTask.Id;
                        sonTask.Ip = oldSonTask.Ip;
                        Updateable(sonTask);
                    }
                }
                return true;
            }
            catch (Exception) {
                throw;
            }
        }


    }
}
