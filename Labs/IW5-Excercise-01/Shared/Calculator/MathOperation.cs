// - Nastaveni projektu: output path, output type, target framework (kde a co je vystup)
// - Struktura aplikace: reference, Namespace, using, classes
// - try, catch
// - Debugging, F10,F11, vraceni provedene operace
//      - krokujte program, Okna: Watch, BreakPoints, conditional break point, call stack object browser
//      - vyskousejte System.Diagnostics.Debug.Write("");
// - vytvorte namespace IW5_cv01.Math v novem projektu typu knihovna, pridejte referenci
//      - v tomto projektu vytvorte funkce pro nasobeni, deleni, odcitani a stitani
// - vyuzitim tohoto suboru vytvorte jednoduchy kalkulator v konzoli
// - vyskousejte praci s parametry args, s kterymi byla aplikace spustena,
//      - (Project > Properties > Debug > Command line arguments)
// - Zkuste puzivat System.Math funkce
// - pozor na deleni nulou!


using System.ComponentModel;

namespace Calculator
{
    public enum MathOperation
        {
        [Description("+")]
        Addition,
        [Description("-")]
        Subtraction,
        [Description("*")]
        Multiplication,
        [Description("/")]
        Division
        }
}
