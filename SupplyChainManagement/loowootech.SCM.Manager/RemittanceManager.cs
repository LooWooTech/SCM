using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Manager
{
    public class RemittanceManager:ManagerBase
    {
        public int Add(Remittance remittance)
        {
            if (IsRepeat(remittance.SID))
            {
                throw new ArgumentException("多次提交汇款信息，请不要多次填写当前订单的汇款信息");
            }
            using (var db = GetDataContext())
            {
                db.Remittances.Add(remittance);
                db.SaveChanges();
                return remittance.ID;
            }
        }

        public bool IsRepeat(int ID)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Remittances.FirstOrDefault(e => e.SID == ID);
                return entity == null ? false : true;
            }
        }
    }
}
