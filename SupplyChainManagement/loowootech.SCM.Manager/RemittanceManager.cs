using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class RemittanceManager:ManagerBase
    {
        public int Add(Remittance remittance)
        {
            if (IsRepeat(remittance.OrderId))
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
                var entity = db.Remittances.FirstOrDefault(e => e.OrderId == ID);
                return entity == null ? false : true;
            }
        }

        public void Save(Remittance data)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Remittances.FirstOrDefault(e => e.OrderId == data.OrderId);
                if (entity != null)
                {
                    db.Entry(entity).CurrentValues.SetValues(data);
                }
                else
                {
                    db.Remittances.Add(data);
                }
                db.SaveChanges();
            }
        }

        public Remittance GetModel(int orderId)
        {
            using (var db = GetDataContext())
            {
                return db.Remittances.FirstOrDefault(e => e.OrderId == orderId);
            }
        }
    }
}
