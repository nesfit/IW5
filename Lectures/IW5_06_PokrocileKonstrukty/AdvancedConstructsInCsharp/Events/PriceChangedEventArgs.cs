using System;

namespace AdvancedConstructsInCsharp.Events
{
    /// <summary>
    /// This class is used to caryout the data connected to the event
    /// </summary>
    public class PriceChangedEventArgs : EventArgs
    {
        public decimal OldPrice { get; private set; }
        public decimal NewPrice { get; private set; }

        public PriceChangedEventArgs(decimal oldPrice, decimal newPrice)
        {
            this.OldPrice = oldPrice;
            this.NewPrice = newPrice;
        }
    }
}