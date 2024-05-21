using kutuphane_otomasyou.Models.table.kisiler;
using kutuphane_otomasyou.Models.table.kitaplar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kutuphane_otomasyou.Models.table
{
    public class KisiKitapViewModel
    {
        public List<kisi> Kisiler { get; set; }
        public List<AlinanKitaplar> Kitaplar { get; set; }
    }

}