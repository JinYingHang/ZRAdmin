using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZR.Model.DTS.DTO
{
    public class ThenTasksDto
    {       
        public List<int> FrontDetailsTaskIds { get; set; }
        public List<int> ThenDetailsTaskIds { get; set; }
        public int FrontDetailsTaskId { get; set; }
        public int ThenDetailsTaskId { get; set; }
        public int TaskGroupId { get; set; }

    }
}
