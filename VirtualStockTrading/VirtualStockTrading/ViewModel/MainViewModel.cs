using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStockTrading.ViewModel
{
    public class MainViewModel
    {
        public StockViewModel StockVM { get; set; }

        public MainViewModel()
        {
            StockVM = new StockViewModel();
        }
    }
}
