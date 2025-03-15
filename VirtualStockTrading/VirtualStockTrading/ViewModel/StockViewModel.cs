using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStockTrading.Model;
using VirtualStockTrading.Servies;

namespace VirtualStockTrading.ViewModel
{
    public class StockViewModel : INotifyPropertyChanged
    {

        private readonly StockService _stockService;
        private ObservableCollection<Stock> _stocks;

        public ObservableCollection<Stock> Stocks
        {
            get => _stocks;
            set { _stocks = value; OnPropertyChanged(nameof(Stocks)); }
        }

        public StockViewModel()
        {
            _stockService = new StockService();
            Stocks = new ObservableCollection<Stock>();
            LoadStockData();
        }
        public async void LoadStockData()
        {
            var stock = await _stockService.GetStocckAsync("GOOGL"); // 애플 주식 가져오기
            Stocks.Add(stock);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));





    }
}
