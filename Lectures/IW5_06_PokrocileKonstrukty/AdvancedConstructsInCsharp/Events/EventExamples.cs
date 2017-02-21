using System;
using System.ComponentModel;

namespace AdvancedConstructsInCsharp.Events
{
    public class EventExamples
    {
        public static void RunExamples()
        {
            var eventExamples = new EventExamples();
            Console.WriteLine("\r\n\r\n--- Event Examples ---");

            //Example 2 - Event definition and subscription
            eventExamples.EventListenerGenericHandlerExample();
            Console.ReadKey();

            //Example 3 - Consuming INotifyPropertyChanged
            eventExamples.ConsumeINotifyPropertyChanged();
            Console.ReadKey();

            Console.ReadKey();
        }

        public void EventListenerGenericHandlerExample()
        {
            Console.WriteLine("\r\nSeting up the stockmarket listeneer");
            var wallstreet = new GenericHandlerStockPriceBroadcaster(10);
            wallstreet.PriceChanged += this.WallStreetPriceChanged;

            wallstreet.WatchMarket();
        }

        private void WallStreetPriceChanged(object sender, PriceChangedEventArgs e)
        {
            Console.WriteLine(" Recieved update from stock market. Price {0} => {1}", e.OldPrice, e.NewPrice);
        }

        public void ConsumeINotifyPropertyChanged()
        {
            Console.WriteLine("\r\nSeting up the PropertyChanged listener");
            Customer customer = new Customer();
            customer.PropertyChanged += this.CustomerPropertyChanged;
            customer.FirstName = "John";
            customer.LastName = "Doo";
        }

        private void CustomerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("Property {0} changed on object", e.PropertyName);
        }
    }
}
