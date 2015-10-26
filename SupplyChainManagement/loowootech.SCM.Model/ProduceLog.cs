using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    [Table("produce_logs")]
    public class ProduceLog
    {
        public ProduceLog()
        {
            CreateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int ProductID { get; set; }

        public int ComponentID { get; set; }

        public int Number { get; set; }

        public int OrderID { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
