using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Model
{
    [Table("rates")]
    public class Rate
    {
        public Rate()
        {
            this.Time = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public double Price { get; set; }
        public int SID { get; set; }
    }
}
