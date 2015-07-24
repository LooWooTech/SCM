using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Manager
{
    public class RateManager:ManagerBase
    {
        public int Add(Rate rate)
        {
            using (var db = GetDataContext())
            {
                db.Rates.Add(rate);
                db.SaveChanges();
            }
            return rate.ID;
        }


        public List<Rate> Get(int ID)
        {
            using (var db = GetDataContext())
            {
                return db.Rates.Where(e => e.SID == ID).ToList();
            }
        }

        public string GetJavaScriptContext(List<Rate> list,string FilePath)
        {
            string str = string.Empty;
            try
            {
                using (var reader = new StreamReader(FilePath))
                {
                    str = reader.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }
            int Count = list.Count;
            if(Count==0)
            {
                return str;
            }
            int Index = str.IndexOf("labels: [");
            StringBuilder SSB = new StringBuilder(str);
            StringBuilder Labelsb = new StringBuilder();
            StringBuilder datasb = new StringBuilder();
            for (var i = 0; i < Count; i++)
            {
                Labelsb.Append('"' + list[i].Time.ToString() + '"');
                datasb.Append(Math.Round(list[i].Price,4));
                if (i != (Count - 1))
                {
                    Labelsb.Append(',');
                    datasb.Append(',');
                }
            }
            SSB.Insert(Index + 9, Labelsb);
            Index = SSB.ToString().IndexOf("data:[");
            SSB.Insert(Index + 6, datasb);
            return SSB.ToString();


        }

    }
}
