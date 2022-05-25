using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN.ViewModels
{
    public class ReportInfo
    {
        public int iGroup { get; set; }
        public string Group { get; set; }
        public int? Count { get; set; }
        public decimal? Sum { get; set; }
        public decimal? SumImportPrice { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public double? Avg { get; set; }

        
    }
}