using System.ComponentModel;

namespace CookBook.Models
{
    public enum Unit
    {
        [Description("neznámý")]
        Unknown = 0,
        [Description("kg")]
        Kg = 1,
        [Description("l")]
        L = 2,
        [Description("ml")]
        Ml = 3,
        [Description("g")]
        G = 4,
        [Description("kusy")]
        Pieces = 5,
        [Description("lžíce")]
        Spoon = 6
    }
}