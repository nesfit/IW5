using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AdvancedConstructsInCsharp
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
      get { return _stockPrice; }
      private set
      {
        var oldPrice = _stockPrice;
        _stockPrice = value;
        if (oldPrice != _stockPrice)
        {
          NotifyPriceChanged(oldPrice, _stockPrice);
        }
      }
    }

    protected BaseStockPriceBroadcaster(decimal initialPrice)
    {
      _stockPrice = initialPrice;
    }

    protected abstract void NotifyPriceChanged(decimal oldPrice, decimal newPrice);

    public void WatchMarket()
    {
      var rand = new Random();
      for (int i = 0; i < 5; i++)
      {
        StockPrice = 10 + rand.Next(-3, 3);
        Thread.Sleep(700);
      }
    }
  }

  #region Proper .NET event pattern
  /// <summary>
  /// This class is used to caryout the data connected to the event
  /// </summary>
  public class PriceChangedEventArgs : EventArgs
  {
    public decimal OldPrice { get; private set; }
    public decimal NewPrice { get; private set; }

    public PriceChangedEventArgs(decimal oldPrice, decimal newPrice)
    {
      OldPrice = oldPrice;
      NewPrice = newPrice;
    }
  }

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
      var handler = PriceChanged;
      if (handler != null)
      {
        handler(this, args);
      }
    }

    protected override void NotifyPriceChanged(decimal oldPrice, decimal newPrice)
    {
      OnPriceChanged(new PriceChangedEventArgs(oldPrice, newPrice));
    }
  }
  #endregion

  public class Customer : INotifyPropertyChanged
  {
    private string _firstName;
    private string _lastName;

    public event PropertyChangedEventHandler PropertyChanged;

    public string FirstName 
    { 
      get { return _firstName; }
      set
      {
        _firstName = value;
        OnPropertyChanged(); //Property name can be omitted  with [CallerMemberName] attribute
        OnPropertyChanged("FullName");
      } 
    }

    public string LastName
    {
      get { return _lastName; }
      set
      {
        _lastName = value;
        OnPropertyChanged();
        OnPropertyChanged("FullName");
      }
    }

    public string FullName
    {
      get { return FirstName + " " + LastName; }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }

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
      wallstreet.PriceChanged += WallStreetPriceChanged;
 
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
      customer.PropertyChanged += CustomerPropertyChanged;
      customer.FirstName = "John";
      customer.LastName = "Doo";
    }

    private void CustomerPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      Console.WriteLine("Property {0} changed on object", e.PropertyName);
    }

  }
}
