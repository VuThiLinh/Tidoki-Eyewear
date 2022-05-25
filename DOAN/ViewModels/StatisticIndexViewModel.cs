using System.Collections.Generic;

namespace DOAN.ViewModels
{
    public class StatisticIndexViewModel
    {
        public int? TotalProduct { get; set; }

        public int? TotalOrderSuccees { get; set; }

        public int? TotalOrderNew { get; set; }

        public int? TotalOrderCanel { get; set; }

        public List<ReportInfo> ReportInfo { get; set; }

        public List<StatisticCategory> StatisticCategory { get; set; }

        public List<StatisticManufacture> StatisticManufacture { get; set; }

        public List<StatisticMonthYear> StatisticMonthYear { get; set; }

    }
}