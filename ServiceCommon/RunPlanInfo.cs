using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCommon
{
    public class RunPlanInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int EstimatedDuration { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string RunTask { get; set; }
        public string UnitId { get; set; }
        public string WellName { get; set; }
        public Guid? WellboreId { get; set; }
    }
}
