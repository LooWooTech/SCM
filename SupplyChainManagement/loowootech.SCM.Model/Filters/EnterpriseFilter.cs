using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    public class EnterpriseFilter
    {
        public int[] Ids { get; set; }

        public string Name { get; set; }

        public Business? Business { get; set; }

        public PageFilter Page { get; set; }
    }
}
