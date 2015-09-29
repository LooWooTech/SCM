using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Model
{
    public class EnterpriseFilter
    {
        public string Name { get; set; }

        public Business Business { get; set; }

        public PageFilter Page { get; set; }
    }
}
