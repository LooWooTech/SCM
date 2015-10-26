﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    /// <summary>
    /// 库存表
    /// </summary>
    [Table("inventorys")]
    public class Inventory
    {
        public Inventory()
        {
            CreateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Number { get; set; }
        public int InfoID { get; set; }
        public InfoType InfoType { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrderID { get; set; }
    }

    public enum InfoType
    {
        Component,
        Product
    }
}
