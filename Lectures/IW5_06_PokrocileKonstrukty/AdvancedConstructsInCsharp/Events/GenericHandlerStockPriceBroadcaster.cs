using System;

namespace AdvancedConstructsInCsharp.Events
{
    /// <summary>
    /// This is an actuall implementation of event patern 
    /// </summary>
    public class GenericHandlerStockPriceBroadcaster : BaseStockPriceBroadcaster
    {
        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public GenericHandlerStockPriceBroadcaster(decimal initialPrice)
            : base(initialPrice)
        {
        }

        protected virtual void OnPriceChanged(PriceChangedEventArgs args)
        {
            var handler = this.PriceChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        protected override void NotifyPriceChanged(decimal oldPrice, decimal newPrice)
        {
            this.OnPriceChanged(new PriceChangedEventArgs(oldPrice, newPrice));
        }
    }
}