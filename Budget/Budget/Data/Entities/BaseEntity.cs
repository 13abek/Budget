using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Data.Entities
{
    public class BaseEntity
    {
        public int id { get; set; }
        public bool Status { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
