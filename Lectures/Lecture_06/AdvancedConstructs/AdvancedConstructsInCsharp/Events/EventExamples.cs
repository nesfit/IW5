using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedConstructsInCsharp.Events
{
    [TestClass]
    public class EventExamples
    {
        private decimal newPrice;

        [TestMethod]
        public void EventListenerGenericHandlerExample()
        {
            Console.WriteLine("\r\nSeting up the stockmarket listeneer");
            var wallstreet = new GenericHandlerStockPriceBroadcaster(10);
            wallstreet.PriceChanged += this.WallStreetPriceChanged;

            wallstreet.WatchMarket();
            Assert.AreNotEqual(0, newPrice);
        }

        private void WallStreetPriceChanged(object sender, PriceChangedEventArgs e)
        {
            Console.WriteLine(" Recieved update from stock market. Price {0} => {1}", e.OldPrice, e.NewPrice);
            this.newPrice = e.NewPrice;
        }

        private List<string> propertiesChanged = new List<string>();

        [TestMethod]
        public void ConsumeINotifyPropertyChanged()
        {
            Console.WriteLine("\r\nSeting up the PropertyChanged listener");
            Customer customer = new Customer();
            customer.PropertyChanged += this.CustomerPropertyChanged;
            customer.FirstName = "John";
            customer.LastName = "Doo";
            Assert.AreEqual(4, propertiesChanged.Count); // because property setters fire two times
        }

        private void CustomerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("Property {0} changed on object", e.PropertyName);
            propertiesChanged.Add(e.PropertyName);
        }
    }
}
