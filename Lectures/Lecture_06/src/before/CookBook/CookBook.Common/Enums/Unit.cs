using System.Resources;
using CookBook.Common.Attributes;
using CookBook.Common.Resources;

namespace CookBook.Common.Enums
{
    public enum Unit
    {
        [UnitDescription(nameof(UnitResources.UnknownDescription))]
        Unknown = 0,

        [UnitDescription(nameof(UnitResources.KgDescription))]
        Kg = 1,

        [UnitDescription(nameof(UnitResources.LiterDescription))]
        L = 2,

        [UnitDescription(nameof(UnitResources.MlDescription))]
        Ml = 3,

        [UnitDescription(nameof(UnitResources.GDescription))]
        G = 4,

        [UnitDescription(nameof(UnitResources.PiecesDescription))]
        Pieces = 5,

        [UnitDescription(nameof(UnitResources.SpoonDescription))]
        Spoon = 6
    }

    public class UnitDescription : LocalizableDescriptionAttribute
    {
        public UnitDescription(string resourceName) : base(resourceName)
        {
        }

        protected override ResourceManager GetResource()
        {
            return UnitResources.ResourceManager;
        }
    }
}
