namespace IW5_OOP.Generics
{
    internal class TextValue<TText, TValue>
        where TText : class
        where TValue : struct
    {
        public TextValue(TText text, TValue value)
        {
            Text = text;
            Value = value;
        }

        public TText Text { get; set; }
        public TValue Value { get; set; }
    }
}