using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    public class OrderFilter
    {
        public PageFilter Page { get; set; }

        public OrderType? Type { get; set; }

        public State? State { get; set; }

        public int EnterpriseId { get; set; }
    }
}
