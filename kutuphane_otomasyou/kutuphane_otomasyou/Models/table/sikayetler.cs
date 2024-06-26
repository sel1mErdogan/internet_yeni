﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kutuphane_otomasyou.Models.table
{
    [Table("sikayetler")]
    public class sikayetler

    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [StringLength(30), Required]
        public string ad { get; set; }


        [StringLength(30), Required]
        public string soyad { get; set; }


        [StringLength(50), Required]
        public string email { get; set; }

        [StringLength(500), Required]
        public string sikayet { get; set; }
    }
}