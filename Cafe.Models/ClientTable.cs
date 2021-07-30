﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    /// <summary>
    /// Відображає столик клієнта
    /// </summary>
    public class ClientTable
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [Column("Примечание",TypeName ="varchar(250)")]
        public string Description { get; set; }

        //[Column(TypeName ="decimal(10,2)")]
        public decimal Summ { get; set; }

        [MaxLength(100)]
        public string TestLenght { get; set; }

    }
}
