using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Model
{
    /// <summary>
    /// 价格记录
    /// </summary>
    [Table("quotations")]
    public class Quotation
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public double Price { get; set; }
        public DateTime Time { get; set; }
        public int Number { get; set; }
        public int CID { get; set; }
    }
}
