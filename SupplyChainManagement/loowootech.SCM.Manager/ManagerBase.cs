using LoowooTech.SCM.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class ManagerBase
    {
        protected ManagerCore Core = ManagerCore.Instance;

        protected SCMContext GetDataContext()
        {
            var db = new SCMContext();
            db.Database.Connection.Open();
            return db;
        }
    }
}
