using Stimline.Data.RunDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace ServiceCommon
{
    public class RunInfo
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public RunStatusEnum Status { get; set; }
        public string TimeZone { get; set; }
        public Toolstring Toolstring { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        public virtual string RunNumber { get; set; }
        public virtual Guid RunPlanId { get; set; }
    }
}
