using System;
using System.Threading;

namespace AdvancedConstructsInCsharp.Events
{
    /// <summary>
    /// "Recieves" the stock price from market. 
    /// Notifies all listeners about the changes.
    /// </summary>
    public abstract class BaseStockPriceBroadcaster
    {
        private decimal _stockPrice;
        public decimal StockPrice
        {
            get { return this._stockPrice; }
            private set
            {
                var oldPrice = this._stockPrice;
                this._stockPrice = value;
                if (oldPrice != this._stockPrice)
                {
                    this.NotifyPriceChanged(oldPrice, this._stockPrice);
                }
            }
        }

        protected BaseStockPriceBroadcaster(decimal initialPrice)
        {
            this._stockPrice = initialPrice;
        }

        protected abstract void NotifyPriceChanged(decimal oldPrice, decimal newPrice);

        public void WatchMarket()
        {
            var rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                this.StockPrice = 10 + rand.Next(-3, 3);
                Thread.Sleep(700);
            }
        }
    }
}