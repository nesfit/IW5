# IW5
# Programming in .NET and C# 
# L02 – OOP & C#, Entity Framework
<div class="right">
[ Jan Pluskal <ipluskal@fit.vutbr.cz> ]
</div>

---

## Project

* Team registration
  * FIT - in WIS
  * FEKT - [http://goo.gl/cBXSLd](http://goo.gl/cBXSLd)
  
* Assignment - [GitHub](https://github.com/FitIW/5/tree/master/Project) 
  * Notice **individual assignments**

---

## Agenda

* Object Oriented Programming in C#
* Three pillars of OOP
* Basic terminology
* Interface
* Struct
* Access modifiers
* Generics
* C#'s specifics compared to OOP
* C# versions
* Basics of Entity Framework

---

## Object Oriented Programming 
* First appearance in **SIMULA 67**
* Abstraction of real word 
* Real object (DOG) has some properties (**length, color of coat, ...**) and an ability to do something (**bark, bite**)
* OOP Object interconnects data and behavior together. 
  * **Behavior** is described by **procedures** and **functions**, both called in OOP as **methods**. 
  * Data is stored in object's **member variable (field)**.
  * **Methods** and **members** together creates objects.

+++?code=/Lectures/Lecture_02/OOP/Car.cs&lang=C#&title=Car
@[3-10]
@[5-6]
@[8]
@[9]
@[1-11]

[Code sample](/Lectures/Lecture_02/OOP/Car.cs)

+++

### Three pillars of OOP

* OOP interconnects **data** and **logic**
* Principles:
  * Encapsulation
  * Inheritance
  * Polymorphism

+++

#### Encapsulation
* Hides implementation details
* Improves modulatiry
* Definitions:
  * A language mechanism for **restricting direct access** to some of the **object's components**. <sup>[1](#1)</sup> <sup>[2](#2)</sup>
  * A language construct that **facilitates the bundling of data with the methods** (or other functions) operating on that data. <sup>[3](#3)</sup> <sup>[4](#4)</sup> 

+++

#### Inheritance

* Allows programmers to: 
  * create classes that are built upon existing classes,
  * to specify a new implementation to maintain the same behaviour,
  * to reuse code and to independently extend original software via public classes.

* An *inherited class* is called a **subclass** of its *parent class* or **super class**. 

+++

* Inheritance should not be confused with subtyping. <sup>[6](#6)</sup> <sup>[8](#8)</sup>
  * In some languages inheritance and subtyping agree, generally in statically-typed class-based OO languages, such as C++, C#, Java, and Scala.), whereas in others they differ;
  * **subtyping** *establishes an is-a relationship*, 
  * **inheritance**
    * only *reuses implementation and establishes* a **syntactic relationship**,
    * not necessarily a **semantic relationship**, inheritance does not ensure behavioral subtyping.
    
 * To distinguish these concepts, **subtyping** is also known as *interface inheritance*,
 whereas inheritance as defined here is known as implementation inheritance or code inheritance.<sup>[9](#9)</sup>
 Still, inheritance is a commonly used mechanism for establishing subtype relationships.<sup>[10](#10)</sup>

+++?code=/Lectures/Lecture_02/OOP/Animals/Animal.cs&lang=C#&title=Animal

+++?code=/Lectures/Lecture_02/OOP/Animals/WildDog.cs&lang=C#&title=WildDog

+++

#### Polymorphism
* From Greek πολύς, polys, "many, much" and μορφή, morphē, "form, shape" <sup>[5](#5)</sup> 
* Is the provision of a *single interface* to *entities of different types*. 
* A polymorphic type is one whose operations can also be applied to values of some other type, or types. <sup>[6](#6)</sup> 
* There are several fundamentally different kinds of polymorphism:
  * **Ad hoc polymorphism**: 
    * **Function overloading**
    * When a *function denotes different and potentially heterogeneous implementations* depending on a limited range of *individually specified types and combinations*.    
  * **Parametric polymorphism**: 
    * When code is written *without mention of any specific type* and thus *can be used transparently with any number of new types*. 
    * This is often known as **generics** or *generic programming* in OOP, and *polymorphism* in functional programming. 
  * **Subtyping**, also called *subtype polymorphism* or *inclusion polymorphism*
    * When a name denotes instances of many different classes related by some common superclass.<sup>[7](#7)</sup>

---

### Basic terminology
* Types in C#:
  * **class** - construction plan for an object
  * **enum** - enum data type as known from other languages
  * **interface** - mechanism to allow *subtype polymorphism*
  * **struct** - value type, alternation to class, do *not allow inheritance*, only *subtyping*
* **instance** - concrete object, instance of a *class*
**field** – a member variable inside a class
**property** – an accessor for a field
**method** - named procedure of function, encapsulated in a class
* identificators
  * **null** - a reference that  *points to nowhere*
  * **this** - a reference to a *current instance* of an object
  * **base** - a reference to a *subtype* of a *supper class*

+++

#### Access modifiers
* **private** - visible only *inside of class**
* **protected** - visible only *inside of class*, and all *inherited types*
* **public** - visible from *everywhere**
* **internal** – visible only inside a *same assembly*, or *friendly assembly*
* **protected internal** – visible only inside a *same assembly*, or *friendly assembly*, only for *inherited types*

* Modifiers are used for limitting access to *implementation details* of a class.
* Therefore, they ensures *encapsulation* and leads to safe code. 
* If modifier is ommited, the most restrictive one is used.
* **Use them!!!**

+++

#### Class
* The most common *reference type*
* A construction of custom type, combining
  *  fields 
  *  properties 
  *  methods
  *  events

+++

* The class is a *blueprint*, not an instance
* Encapsulates *data* and *behavior*
* `static` - only *one* instance for program run
* *non static* - classes are instanciated during program run

* Before:
  * *attributes and class modifiers*
* Behind:
  * *generic type parameters, a base class, and interfaces**
* In brackets:
  * *class members - methods, properties, indexers, events, fields, constructors, overloaded operators, nested types, and a finalizer**

```C#
//The simpliest class
class SimpliestClass { }
```

+++

#### Fields
* *Variable* that is a `class` or `struct` member
* `readonly` cannot be changed after `class` or `struct` construction
* Initialization
  * Optional,
  * Noninitializatialized has a *default* value (0, \0, null, false)
  * *Before a constructor call*
* Modifiers:
  * `static`
  * access - `public, internal, private, protected, internal protected`
  * inheritance - `new`
  * unsafe code - `unsafe`
  * `readonly`
  * threading - `volatile`
  * 
+++?code=/Lectures/Lecture_02/OOP/Animals/UnknownCat.cs&lang=C#&title=UnknownCat

+++

#### Methods

* *Procedures* and *functions* are in OOP called *methods*
* Can access members of `class` or `struct`.
* Can
  * *accept parameters* - *values*, *reference types*, `ref`
  * *return result* - in return type (`return`), or `ref` or `out` parameters

* Modifiers:
  * `static`
  * access - `public, internal, private, protected, internal protected`
  * inheritance - `new, virtual, abstract, override, sealed, partial`
  * unsafe code - `unsafe, extern`
  * asynchronous - `async`

+++

#### Expression-bodied method
* Since C# 6
* Method contains only one expression.

* Classical method:
```C#
int Foo(int x) { return x * 2; }
```

* Expression-bodied metod:
```C#
int Foo(int x) => x * 2;
```

* Method can have an empty return type (`void`)
```C#
void Foo(int x) => Console.WriteLine(x);
```

+++

#### Method overload/override
* `class` or `interface` can **override** methods 
  * to use the same name, but different implementation
* `class` or `interface` can **overload** methods
  * to use the same name, but different implementation and different parameters
 
* Methods **overload** - signature needs to be different
```C#
void Foo (int x) {...}
void Foo (double x) {...}
void Foo (ref double x) {...} // OK so far
void Foo (out double x) {...} // Compile-time error, CLR
```

+++

* Return type is not a part of the signature
```C#
void Foo (int x) {...}
int  Foo (int x) {...} // Compile-time error
```

* Method overloads can have different return types
```C#
int    Foo (int x) {...}
double Foo (double x) {...} // OK
```

+++

#### Properties
* It is simmilar to a *fild*, but it enclosis it with access methods.
* It is a safety mechanism that unifies *read* and *write* operations.
* Hides *implementation details*.

* Autogenerated property:
```C#
public string Name {get; private set;}
```

* Property with backing field:
```C#
private string _name;
public string Name {
  get { return _name; }
  private set { _name = value; }
}
```

+++

* Property expression-bodied:
```C#
public string Name => $"{Magic.GetCatName()} Cat";
```

* Property with lazy initialization:
```C#
private string _name;_
public new string Name {
  get { return _name ?? (_name = $"{Magic.GetCatName()} Cat"); }
  private set { _name = value; }
}
```

---

### Constructor
* Launches type's, i.e., `class` or `struct`, initialization
* Defined as a **method** *without return type* with the same name as the constructed type
* If `class` is derived from *super class*, constructors of *super class* are accessible.
* Type can have multiple constructors

* *Parameterless constructor* is created automatically unless other *constructor* is defined, than it needs to be declared manually.

```C#
var alik = new Dog("Alík");
```

* Modifiers:
  * access - `public, internal, private, protected, internal protected
  * unsafe code - `unsafe, extern`

+++?code=/Lectures/Lecture_02/OOP/Animals/Dog.cs&lang=C#&title=Dog

+++

#### Constructor overloading
* Type, i.e., `class` or `struct` can have multiple constructors
* The same rules as *method overloading* are applied
* Protects against *code duplication* and increases *readability*

* Keywords
  * `this` - refers to *this* type instance 
  * `base` - refers to *super class* type instance

+++?code=/Lectures/Lecture_02/OOP/Animals/Cat.cs&lang=C#&title=Cat

+++?code=/Lectures/Lecture_02/OOP/ConstructorInitializationSample.cs&lang=C#&title=Constructor initialization demo

---

### Abstract class
* `class` declared as `abstract` cannot be instanciated, only its *descendant* can.
* *Descendant* has to implement all `abstract` members, or has to be `abstract` him-self.
* `abstract` members are similar to `virtual` members but do not provide *default implmentation*
* `abstract class` cannot be `sealed`, thus must be possible to *inherit* from it.

+++?code=/Lectures/Lecture_02/OOP/Animals/Animal.cs&lang=C#&title=Animal

+++?code=/Lectures/Lecture_02/OOP/Animals/WildDog.cs&lang=C#&title=WildDog

+++

#### Type compatibilty
* Easeup usage of *subtypes*, ergo *virtual methods*.
* Compatibility of *types* of `class`, `struct` instances.
* Determines, to which type reference can be assigned reference of another type.
* **upcast**
  * To type of *super class* can be assigned all instances of its descendants.
  * Only *members* provided by given *super class* can be accessed throught upcasted reference.
* **downcast**
  * Creates reference of *inherited* `class` from *super class* instance.
  * It fails, it *super class* instance is not compatible with *inherited* one.

+++?code=/Lectures/Lecture_02/OOP/CastSample.cs&lang=C#&title=CastSample

+++

#### Polymorfism, virtual metods 
* *Inheritance* and *subtyping*
* Because of *type compatibility*, 
  * we can create multiple instances of concrete implementations,
  * assign selected to *super class* reference,
  * and achive different beavior for the same method call throught the *super class* reference.

```C#
public class Animal
{
  public virtual void Draw {…}
}
public class Dog : Animal
{
  public override void Draw {…}
}
public class Cat : Animal
{
  public override void Draw {…}
}
```

---

#### Virtuals
* Virtual can be
  * methods
  * properties
  * indexers
  * events
  
* Activation uses mechanism of *late binding* which chooses the appropriate implementation during *runtime*
* **late binding** vs **early binding**

#### Operators IS/AS
* Operator `is`
  * tests type compatibility, thus determines wheather `class` inherits a *super class* or `class` implements `interface`.
  * usually before downcast

```C#
var wildDog = new WildDog();
Console.WriteLine(a is Dog ? 
	                "wildDog is a Dog" :
	                "wildDog is not a Dog");
```

+++

* Operator `as`
    * *downcast*s instance
    * in case that *types are incompatible*, `InvalidCastException` is not raised, but result is `null`.

```C#
var wildDog = new WildDog();
var dog = wildDog as Dog; //dog == null 
Console.WriteLine(dog != null ? 
	                "dog is a Dog" : 
	                "dog is not a Dog");
```

+++

```C#
WildDog wildDog = new WildDog();
if (wildDog is Dog dog)
   Console.WriteLine($"dog is a Dog, named {dog.Name}");
```

+++?code=/Lectures/Lecture_02/OOP/CastSample.cs&lang=C#&title=CastSample

+++

#### Sealed class

* Keyword `sealed` restricts 
  * inheritance of `class`
  * override of *method*

```C#
class Animal { }

sealed class Cat : Animal { }

//Compile-time error
public class Kitten : Cat {}
```

+++

#### Base keyword
* Members of *super class* can be access in overides using `base` keyword.

```C#
class Animal
{
  public virtual string Name {get;}
}

class Cat : Pet
{
  public override string Name => base.name + "Cat";
}
```

---

### System.Object
* object (`System.Object`) is a common *super class* of all types
* each type can be cast to `System.Object`
* `System.Object` methods:
  * ToString()
  * Equals()
  * GetHashCode()
  * GetType()
* To get instance type:
  * during *runtime* - `Object.GetType()`
  * during *compile time* - `typeof(object)`

+++

```C#
Dog d = new Dog();

Console.WriteLine(d.GetType().Name); // Returns “Dog”

Console.WriteLine(typeof(Dog).Name); // Returns “Dog”

Console.WriteLine(d.GetType() == typeof(Dog)); // Returns true
```

+++?code=/Lectures/Lecture_02/OOP/SystemObjectSample.cs&lang=C#&title=SystemObjectSample

---

### Finalizer
* runs on instance that is no more referenced before is garbage collecte
* simillar to *destructor* in C++
* `override`s `System.Object`'s method `Finalize()`.

```C#
protected override void Finalize() {
  ...
  base.Finalize();
}
```

```C#
class Dog {
  ~Dog()   {
    // Cleanup code
    ...
  }
}
```

---

### Partial classes and methods
* Using `partial`, a class can be split into multipe source files.
* Typical usage in WPF:
  * one file is *human edited*
  * one is *autogenerated*

* `partial` methods
  * the same principle as with the partial `class`

```C#
// Dog1Gen.cs - auto-generated
partial class Dog1Form {
  public Dog1Form() {
    this.Bark();
  }
  partial void Bark();
}
// Dog1Form.cs - hand-authored
partial class Dog1Form {
  partial void Bark() {
    Console.WriteLine("Bark");
  }
}
```

---

### Struct
* Simillar as `class` with differences:
    * `struct` is a *value type*, `class` is a *reference type*
    * `struct` inherits *System.ValueType*, `class` inherits *System.Object*
    * `struct` 
        * does not support *inheritance*
        * can have every member except *parameterless constructor, finalizer, virtual members*
        * each *constructor* has to initialize all `struct`'s members
        * cannot initialize members in declaration

```C#
public struct Point
{
  int x, y;
  public Point (int x, int y)
  {
    this.x = x; 
    this.y = y;
  }
}
...
Point p1 = new Point (1, 1); // p1.x and p1.y will be 1
Point p2 = new Point (); // p2.x and p2.y will be 0
```

---

### Enums, Flags 
* `enum` is a *value type*
  * creates an enumeration of named numerical values (int, 0,1...)
  * underlying type can be changed to `long`

* `enum` with the attribute `flags`
  * *single variable* may contain *multiple values*

```C#
private enum HorseColor { Siml = 0, Palomino = 5, Ryzak = 10 }

HorseColor color = HorseColor.Siml;
int colorNumber = (int)HorseColor.Ryzak;

HorseColor.TryParse("Ryzak", out HorseColor color);

[Flags] public enum HorseType { None = 0, Racing = 1, 
Breeding = 2, ForSosages = 4, Dead = 8 }

HorseType type = HorseType.Racing | HorseType.Breeding;
type |= HorseType.ForSosages;
Console.WriteLine(type); //Racing, Breeding, ForSosages
```

---

### Interface
* Declares only *specification*, not *implementation* of its members
* All members are `public`
* `class` or `struct` can implement multiple `interface`s
* Implementation is provided by `class` or `struct` that implementats particular `interface`
* `interface` can declare
  * methods
  * properties
  * events
  * indexers

```C#
// Defined in System.Collections
public interface IEnumerator
{
  bool MoveNext();
  object Current { get; }
  void Reset();
}
```

+++

#### `interface` vs `abstract class` ?

* Use *inheritance* for types that shares its implementation
* Use `interface` for types that have idependent implementations
* A `class`, or `struct` can implement multiple interfaces

```C#
abstract class Animal { }
abstract class Bird : Animal { }
abstract class Insect : Animal { }

interface IFlying {}
interface ICarnivore {}

// Concrete classes:
class Ostrich : Bird { }
class Eagle : Bird, IFlying, ICarnivore { }
class Bee : Insect, IFlying { }
class Flea : Insect, ICarnivore { }
```

* Bacause animals might share some implementation by their taxanomy, it is possible to declare `Bird` and `Insect` as `abstract class`.
* But, their food intake and wheather they fly or not might differs. It is best to declare these properties as `interfaces`, `IFlying` and `ICarnivore`.

---

### Reusable code
* C# mechanisms for useable code creation are:
  * inheritance
  * generics
  * composition
* Example:
  * Stack for different data types
    * *Hardcoded* data type leads to *code duplicatin*
    * Use of *System.Object* introduces *boxing*
    * The right way is *generic*

+++?code=/Lectures/Lecture_02/OOP/Generics/ObjectStack.cs&lang=C#&title=ObjectStack

+++?code=/Lectures/Lecture_02/OOP/Generics/NonGenericStackSample.cs&lang=C#&title=NonGenericStackSample

---

### Generics 
* *Inheritance* increases reusability of *base type*
* *Generics* allows use of templates
  * also, introduces *type safe code*, no more *casting* and *boxing*
* Generic `interface`
  * parameters can be restricted with:
    * where T : base-class
    * where T : interface 
    * where T : class 
    * where T : struct 
    * where T : new() 
    * where U : T
    * 
+++?code=/Lectures/Lecture_02/OOP/Generics/Stack.cs&lang=C#&title=ObjectStack

+++?code=/Lectures/Lecture_02/OOP/Generics/GenericStackSample.cs&lang=C#&title=NonGenericStackSample

+++

#### Generic methods
* Several basic algorithms can be implemented using *generic methods*.
* *Signature* of generic method contains generic type parameter.
* *Generic method* can contain multiple *generic parameters*

```C#
static void Swap<T> (ref T a, ref T b)
{
  T temp = a;
  a = b;
  b = temp;
}
```

Difference:
  * *opened type* – Stack<T>
  * *closed type* – Stack<int>
* during a *runtime* all generics are of *closed type*

---

### Covariance and Contravariance in Generics
* [Read more](https://docs.microsoft.com/en-us/dotnet/standard/generics/covariance-and-contravariance)
* **Covariance** allows a use of more derived (more specific) than originally specified.
  * You can assign an instance of `IEnumerable<Derived>` to a variable of type `IEnumerable<Base>`.  
* **Contravariance** allows a use less derived (less specific) than originally specified. 
  * You can assign an instance of `IEnumerable<Base>` to a variable of type `IEnumerable<Derived>`.

* **Invariance** use only of the same type as originally specified.
  *  Invariant generic type parameter is neither **covariant** nor **contravariant**.

---

### C# characteristics compared to OOP
* Unified typed system
* Classes and interfaces
* Properties, methods, events

+++

#### Unified typed system
* Type - encapsulates data and methods
* Shareing of base implementation
* E.g., `Object.ToString()` transform instance to a `string` representation
```C#
namespace System
{
  public class Object
  {
    public virtual string ToString() {}
    public virtual bool Equals(object obj) {} 
    public virtual int GetHashCode() {}
  }
}
```

+++

#### Classes and interfaces
* `class` is considered to be *type*
* *Data* are stored in member variables
* *Operations* are declared in methods

* `interface` 
  * describes `class` members
  * behavior is defined in `class` that implements it

* *Multiple inheritance* is not supported
* *Multiple* `interface` *implementation* is supported

```C#
 public interface IBoy {
    string Name {get;}
  }

  public class Boy: IBoy {
    public string Name { }
  }
```

+++

#### Class members
* Properties
  * encapsulates object state
  * e.g., color
* Methods
  * implements object behavior
  * e.g., SetButtonColor
* Event
  * notifies object change
  * e.g., ColorChanged

```C#
public class Button {
  public event EventHandler ColorChanged;
    
  public Color Color { get; set; }

  public void SetButtonColor(Color color) {
    Color = color;

    if (ColorChanged != null) {
      this.ColorChanged(this, EventArgs.Empty);
    }
  }
}
```

+++

#### Type Safety and Security
* *Strongly typed language*
  * *type* has to be known in *compile time*
* Support of Intellisence in Visual Studio
* keyword `dynamic` overcomes type safety mechanisms and type is resolved in *runtime*
* *benefits*
  * elimination of type issues in  *compile time*
  * *sandboxing* protects object state against outer modifications

```C#
Button button =  new Button();
var button = new Button();
Button button = new Color();
```

---

### New features in C# versions
#### C# 2.0
* Generics
  * `List<T> list = new List<T>();`
* Nullable types 
  * `Nullable<int> pocet = null;`
* Anonnymous methods
  * `p = delegate(string j) {Console.WriteLine(j); };`
* Iterator blocks	
  * `yield return;`
* Properties – getter and setter
  * `public Color Color { get{…} set{…} }`
* Partial types
```C#
public partial class TasksWindow { public int x = 1; }
public partial class TasksWindow { public int y = 1; }
public partial class TasksWindow { 
  public TasksWindow() {Console.WriteLine(x+y);}
}
```

+++

#### C# 3.0
* Expression trees
* Implicit local type – `var`	
  * `var cars = new List<Car>();`
* Lambda expressions
  * `(param)=>{Console.WriteLine(param);}`
* Extension metody
* Auto property
  * `public Color Color { get; set; }`
* LINQ
```C#
List<Car> cars = new List<Car>(); 
var redCars = cars
              .Where(c => c.Color == Color.Red)
              .Select(r => r.Name);
```

+++

#### C# 4.0
* Dynamic binding
* Optional parametes and named parameters
* Type variance - generic interfaces and delegates
* COM interoperability

+++

####  C# 5.0
* Support for asynchronous operations - `asycn` and `await` contextual keywords

+++

####  C# 6.0
* New compiler - **Roslyn**
* In Visual Studio 2015

+++

####  C# 7.0
* Out variables
```C#
public void PrintCoordinates(Point p)
{
    p.GetCoordinates(out int x, out int y);
    WriteLine($"({x}, {y})");
}
```
* Pattern matching
```C#
public void PrintStars(object o)
{
    if (o is null) return;     // constant pattern "null"
    if (!(o is int i)) return; // type pattern "int i"
    WriteLine(new string('*', i));
}
```

+++

* Switch statements with patterns
```C#
switch(shape)
{
    case Circle c:
        WriteLine($"circle with radius {c.Radius}");
        break;
    case Rectangle s when (s.Length == s.Height):
        WriteLine($"{s.Length} x {s.Height} square");
        break;
    case Rectangle r:
        WriteLine($"{r.Length} x {r.Height} rectangle");
        break;
    default:
        WriteLine("<unknown shape>");
        break;
    case null:
        throw new ArgumentNullException(nameof(shape));
}
```

+++

* Tuples
```C#
(string, string, string) LookupName(long id) // tuple return type
{
    ... // retrieve first, middle and last from data storage
    return (first, middle, last); // tuple literal
}
...
var names = LookupName(id);
WriteLine($"found {names.Item1} {names.Item3}.");
```

+++

* Deconstruction
```C#
(string first, string middle, string last) = LookupName(id1); // deconstructing declaration
WriteLine($"found {first} {last}.");
```

+++

* Local functions
```C#
public int Fibonacci(int x)
{
    if (x < 0) throw new ArgumentException("Less negativity please!", nameof(x));
    return Fib(x).current;

    (int current, int previous) Fib(int i)
    {
        if (i == 0) return (1, 0);
        var (p, pp) = Fib(i - 1);
        return (p + pp, p);
    }
}
```

+++

* Literal improvements
  * digit separator 
```C#
var d = 123_456;
var x = 0xAB_CD_EF;
```
  * binary literals
```C#
var b = 0b1010_1011_1100_1101_1110_1111;
```

+++

* Ref returns and locals
```C#
public ref int Find(int number, int[] numbers)
{
    for (int i = 0; i < numbers.Length; i++)
    {
        if (numbers[i] == number) 
        {
            return ref numbers[i]; // return the storage location, not the value
        }
    }
    throw new IndexOutOfRangeException($"{nameof(number)} not found");
}

int[] array = { 1, 15, -39, 0, 7, 14, -12 };
ref int place = ref Find(7, array); // aliases 7's place in the array
place = 9; // replaces 7 with 9 in the array
WriteLine(array[4]); // prints 9
```

+++

* Throw expressions
```C#
class Person
{
    public string Name { get; }
    public Person(string name) => Name = name ?? throw new ArgumentNullException(name);
    public string GetFirstName()
    {
        var parts = Name.Split(" ");
        return (parts.Length > 0) ? parts[0] : throw new InvalidOperationException("No name!");
    }
    public string GetLastName() => throw new NotImplementedException();
}
```

---

## Entity Framework Demo

---

## References

<a name="1">1</a>: Mitchell, John C. (2003). Concepts in programming languages. Cambridge University Press. p. 522. ISBN 0-521-78098-5.

<a name="2">2</a>: Pierce, Benjamin (2002). Types and Programming Languages. MIT Press. p. 266. ISBN 0-262-16209-1.

<a name="3">3</a>: Rogers, Wm. Paul (18 May 2001). "Encapsulation is not information hiding". JavaWorld.

<a name="4">4</a>: Connolly, Thomas M.; Begg, Carolyn E. (2005). "Ch. 25: Introduction to Object DMBS § Object-oriented concepts". Database systems: a practical approach to design, implementation, and management (4th ed.). Pearson Education. p. 814. ISBN 0-321-21025-5.

<a name="5">5</a>: Bjarne Stroustrup (February 19, 2007). "Bjarne Stroustrup's C++ Glossary". polymorphism – providing a single interface to entities of different types.

<a name="6">6</a>: Cardelli, Luca; Wegner, Peter (December 1985). "On understanding types, data abstraction, and polymorphism" (PDF). ACM Computing Surveys. New York, NY, USA: ACM. 17 (4): 471–523. doi:10.1145/6041.6042. ISSN 0360-0300.: "Polymorphic types are types whose operations are applicable to values of more than one type."

<a name="7">7</a>: Booch, et al 2007 Object-Oriented Analysis and Design with Applications. Addison-Wesley.

<a name="8">8</a>: Cook, William R.; Hill, Walter; Canning, Peter S. (1990). Inheritance is not subtyping. Proc. 17th ACM SIGPLAN-SIGACT Symp. on Principles of Programming Languages (POPL). pp. 125–135. CiteSeerX 10.1.1.102.8635 Freely accessible. doi:10.1145/96709.96721. ISBN 0-89791-343-4.

<a name="9">9</a>: Tempero, Ewan; Yang, Hong Yul; Noble, James (2013). What programmers do with inheritance in Java (PDF). ECOOP 2013–Object-Oriented Programming. pp. 577–601.

<a name="10">10</a>: Mike Mintz, Robert Ekendahl (2006). Hardware Verification with C++: A Practitioner’s Handbook. United States of America: Springer. p. 22. ISBN 0-387-25543-5.

[C# 7.0 in a Nutshell: The Definitive Reference](https://www.amazon.com/C-7-0-Nutshell-Definitive-Reference/dp/1491987650)

And ... a lot of Google index images...
