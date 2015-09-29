using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    public class ComponentFilter
    {
        public string Number { get; set; }

        public UnitType? UnitType { get; set; }

        public PageFilter Page { get; set; }
    }
}
