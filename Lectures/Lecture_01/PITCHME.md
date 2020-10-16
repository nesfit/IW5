@snap[midpoint span-100]

# Introduction to C#, Visual Studio and .NET

@snapend

@snap[south-east span-40]

[ Jan Pluskal <ipluskal@fit.vutbr.cz> ]

@snapend
---

@snap[midpoint span-100]

## Introduction to Visual Studio

@snapend

---
@snap[north]
![](/Lectures/Lecture_01/Assets/img/VisualStudioLogo.png)
@snapend

* Integrated development environment (IDE)
* Feature-rich program that can be used for many aspects of software development:
  * editor
  * debugger
  * builder
  * completion tools
  * graphical designers
  * etc..
* [Free download](https://visualstudio.microsoft.com/vs/")
* [Installation guide](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2017)

+++
@snap[midpoint span-100]
![](/Lectures/Lecture_01/Assets/img/VisualStudioIde.jpg)
@snapend

+++
## Comparison of Visual Studio versions
|  Supported Features                | Community |  Professional | Enterprise |
| ---------------------------------- | --------- | ------------- | ---------- |
| Supported Usage scenarios          | ⚫⚫⚫◯  | ⚫⚫⚫⚫     | ⚫⚫⚫⚫   |
| Development Platform Support       | ⚫⚫⚫◯  | ⚫⚫⚫⚫     | ⚫⚫⚫⚫   |
| Integrated Development Environment | ⚫⚫⚫◯  | ⚫⚫⚫◯      | ⚫⚫⚫⚫  |
| Advanced Debugging and Diagnostics | ⚫⚫◯◯  | ⚫⚫◯◯      | ⚫⚫⚫⚫  |
| Testing Tools                      | ⚫◯◯◯  | ⚫◯◯◯       | ⚫⚫⚫⚫  |
| Cross-platform Development         | ⚫⚫◯◯  | ⚫⚫◯◯      | ⚫⚫⚫⚫  |
| Collaboration Tools and Features   | ⚫⚫⚫◯  | ⚫⚫⚫◯      | ⚫⚫⚫⚫  |

+++
## Only Enterprise features
* Code Map Debugger Integration
* .NET Memory Dump Analysis
* Test Case Management
* Code Coverage with tests
* IntelliTest
* ⋮

---
## Recommended extensions, services, and tools

* Extensions:
  * Resharper
  * Code metrices
  * Markdown Editor
  * Entity Framework Power tools
  * GitFlow
  * Mnemonic templates
* Tools & Services:
  * LinqPad
  * DotPeek
  * Source Tree
  * VSCode
  * Rider
  * Azure DevOps

@snap[east span-60]
![](/Lectures/Lecture_01/Assets/img/VSExtensions.PNG)
@snapend

+++
### [Resharper](https://www.jetbrains.com/resharper/)

@snap[west span-40]

Extends Visual Studio with code inspections. For most inspections provides quick-fixes to improve code in one way or another. Helps safely organize code and move it around the solution.

For more details see [features](https://www.jetbrains.com/resharper/features/).

@snapend

@snap[east span-50]
![](/Lectures/Lecture_01/Assets/img/reshaper_refactor_this.png)
@snapend

+++
### [Azure DevOps](https://azure.microsoft.com/en-us/services/devops/)
* Before Visual Studio Team Services.
* Cloud-hosted private Git repositories
* Agile planning
* Build management
* Test Plans

@snap[east span-50]
![Azure-DevOps](/Lectures/Lecture_01/Assets/img/Azure-DevOps2.png)
@snapend


+++
### [Code Metrices](https://marketplace.visualstudio.com/items?itemName=Elisha.CodeMetrices)
@snap[west span-50]
Visual Studio extension that helps to monitor the code complexity.
As you type, the method complexity "health" is updated, and the complexity is shown near the method.
@snapend

@snap[east span-50]
![Azure-DevOps](/Lectures/Lecture_01/Assets/img/code_metrices.jpg)
@snapend

+++
### [Mnemonic templates](https://github.com/JetBrains/mnemonics)
@snap[west span-50]

Templates for ReSharper that let you quickly generate code and data structures by typing in names.

@snapend

@snap[east span-50]
![](/Lectures/Lecture_01/Assets/img/resharperLiveTemplates_thumb.gif)
@snapend

+++
### [LinqPad](http://www.linqpad.net/)
@snap[west span-40]

LinqPad is not just for LINQ queries, but any C# expression, statement block or program.
Put an end to those hundreds of Visual Studio Console projects cluttering your source folder and join the revolution of LINQPad scripters and incremental developers.

@snapend

@snap[east span-60]
![](/Lectures/Lecture_01/Assets/img/linqpad-sql.gif)
@snapend

+++
### [DotPeek](https://www.jetbrains.com/decompiler/)
@snap[west span-40]

Tool based on ReSharper's bundled decompiler.
It can reliably decompile any .NET assembly into equivalent C# or IL code.

@snapend

@snap[east span-60]
![](/Lectures/Lecture_01/Assets/img/dotpeek.gif)
@snapend

+++
### [Markdown Editor](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor)
@snap[west span-40]

A full-featured Markdown editor with live preview and syntax highlighting.
Supports GitHub flavored Markdown.

@snapend

@snap[east span-60]
![](/Lectures/Lecture_01/Assets/img/markdown-editor.gif)
@snapend



+++
### [Entity Framework Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools)
@snap[west span-40]

Useful design-time utilities for EF 6/ EF Core, accessible through the Visual Studio Solution Explorer context menu when right-clicking on a file containing a derived DbContext class.

@snapend

@snap[east span-60]
![](/Lectures/Lecture_01/Assets/img/ef-powertool.PNG)
@snapend


+++
### [GitFlow](https://marketplace.visualstudio.com/items?itemName=vs-publisher-57624.GitFlowforVisualStudio2017)
@snap[west span-50]

Team Explorer extension integrates GitFlow into your development workflow. It lets you easily create and finish feature, release and hotfix branches right from Team Explorer.  For more details about git recommends [Pro Git book](https://git-scm.com/book/en/v2).

@snapend

@snap[east span-40]
![](/Lectures/Lecture_01/Assets/img/gitflow.png)
@snapend

---
## Why To Choose .NET?
@snap[north-east span-10]
![](/Lectures/Lecture_01/Assets/img/Overview_small.png)
@snapend

* Productivity
* Almost every platform
* Performance
* Security
* Large ecosystem
* Open source

@snap[east span-50]
![](/Lectures/Lecture_01/Assets/img/why-developers-choose-dot-net.png)
@snapend

+++
### Productivity
* To develop *high quality applications faster*
* Modern language constructs
  *  *Generics*
  *  Language Integrated Query (*LINQ*)
  *  *Asynchronous programming*
* Extensive class libraries - NuGet
* Common APIs
* *Multi-language* support

@snap[east span-50]
![](/Lectures/Lecture_01/Assets/img/dotneta.png)
@snapend

+++
### Almost every platform
* iOS
* Android
* Windows
* Windows server
* Linux
* Micro-services on cloud

@snap[east span-50]
![](/Lectures/Lecture_01/Assets/img/netcore3.jpg)
@snapend

+++
### Performance
* Applications provide better response times and require less computing power.
* Comparison of web application frameworks with tasks like
  * JSON serialization,
  * database access,
  * and server side template rendering.

@snap[south-east span-50]
![](/Lectures/Lecture_01/Assets/img/Performance.png)
[Source](https://www.techempower.com/benchmarks/#section=data-r16&hw=ph&test=plaintext)
@snapend

+++
### Security
* Immediate **security** benefits via its *managed runtime*
* Prevents *critical issues* like **buffer overflow**
* Patches are released with runtime

+++
### Large ecosystem
* Libraries from the [NuGet package manager](https://www.nuget.org/)
* Visual studio [marketplace](https://marketplace.visualstudio.com/)
* [Extensive partners network](https://vspartner.com/Directory)
* Community support, MVPs, ...

+++
### Open source
* [**.NET Foundation**](https://dotnetfoundation.org/)
* [.NET Core](https://github.com/dotnet/core)
* [.NET Runtime/CoreFX](https://github.com/dotnet/runtime)
* [ASP.NET Core](https://github.com/dotnet/aspnetcore)
* [.NET Standard](https://github.com/dotnet/standard)
* [EF Core](https://github.com/dotnet/efcore)
* [WPF](https://github.com/dotnet/wpf)
* [Reference Source - .NET Framework (readonly)](https://github.com/microsoft/referencesource)
* Independent, Innovative, Commercially-friendly
* Google, JetBrains, Red Hat, Samsung, Unity...

---
# .NET Platform
@snap[north-east span-10]
![](/Lectures/Lecture_01/Assets/img/Overview_small.png)
@snapend

* Language interoperability
* Architecture
* Common Language Runtime
* Benefits
* Garbage collector
* In The Nutshell
* The .NET family of frameworks

@snap[south-east]
[Source](https://blogs.msdn.microsoft.com/cesardelatorre/2016/06/27/net-core-1-0-net-framework-xamarin-the-whatand-when-to-use-it/)
@snapend

@snap[east span-70]
![](/Lectures/Lecture_01/Assets/img/Overview.png)
@snapend

+++
## Language interoperability
@snap[south-east span-70]
![](/Lectures/Lecture_01/Assets/img/Common_Language_Infrastructure.png)
@snapend


+++
## Architecture
@snap[midpoint span-100]
![](/Lectures/Lecture_01/Assets/img/dot_net_architecture.png)
@snapend

+++
## CLR - Common Language Runtime
* The virtual machine component of .NET
* Manages the execution
* Just-in-time compilation
* Similar to Java Virtual Machine

@snap[east span-50]
![](/Lectures/Lecture_01/Assets/img/dot_net_clr.png)
@snapend

+++
## CLR - Benefits
* Performance improvements - *JIT*
* Easy use of *components developed in other languages*
* Extensible *types defined in BCL*
* *Inheritance, interfaces, and overloading* for OOP
* [Free threading](https://stackoverflow.com/questions/3892259/difference-between-free-threaded-and-thread-safe) (MTA Apartment)
* Structured *exception handling*
* Custom *attributes*
* **Garbage collection**
* *Delegates* instead of function pointers

+++
### Garbage collector
* **Automated memory management** without need of programmer intervention
* Uses reachability from GC roots to identify *alive* objects
* *Three generations*

![](/Lectures/Lecture_01/Assets/img/gc_generations.png)


+++
## C# - In The Nutshell
@snap[midpoint span-90]
![](/Lectures/Lecture_01/Assets/img/Csh_in_nutshell_framework.png)
@snapend

+++
## Standard Libraries
|  Library                            | Namespaces                                                                                                                |
|-------------------------------------| ------------------------------------------------------------------------------------------------------------------------- |
| **Base Class Library**              | *System, System.Collections, System.Collections.Generic, System.Diagnostics, System.IO, System.Text, System.Threading...* |
| **Runtime Infrastructure Library**  | *System, System.Reflection, System.Runtime.CompilerServices, System.Runtime.InteropServices...*                           |
| **Network Library**                 | *System, System.Net, System.Net.Sockets...*                                                                               |
| **Reflection Library**              | *System.Globalization, System.Reflection...*                                                                              |
| **XML Library**                     | *System.Xml*                                                                                                              |
| ⋮                                   | ⋮                                                                                                                          |

+++
## The .NET family of frameworks
@snap[midpoint span-80]
![](/Lectures/Lecture_01/Assets/img/dot_net_libraries.png)
@snapend

---
# C# 101
@snap[north-east span-10]
![](/Lectures/Lecture_01/Assets/img/Overview_small.png)
@snapend

* Identifiers
* Keywords
* Literals
* Delimiters
* Operators
* Comments
* Datatypes
* Value Types

@snap[east span-40]
![](/Lectures/Lecture_01/Assets/img/keep-calm-and-focus-on-the-basics.png)
@snapend

+++
## C# is
* **multi-paradigm**;
* **strong typed**;
* **object oriented** (class-based);
* imperative, declarative;
* functional, generic;
* based on c++;
* programming language.

@snap[east span-40]
![](/Lectures/Lecture_01/Assets/img/csh.png)
@snapend

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/HelloWorld.cs&lang=C#&title=Hello World Sample
@[1]
@[3-4, 15]
@[5-6, 14]
@[7-8, 13]
@[9]
@[11]
@[12]
@[1-15]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/HelloWorld.cs)

+++
## Identifiers
* Name given to entities such as *variables*, *methods*, *classes*, etc.
* Tokens which uniquely identify elements
* `value` is a identifier:
  ```C#
  int value;
  ```
* **Reserved keywords** can not be used unless prefix `@` is added
  ```C#
  int @class;
  ```
+++
## Keywords
* **Reserved** words that have *special meaning*
* Meaning can not be changed
* **Can not be directly** used as *identifies*
* `long` is a keyword:
  ```C#
  long count;
  ```
* E.g. ```int, bool, if, for, class, false, public, break```
* [List of all Keywords](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/)

+++
## Contextual Keywords
* **Specific meaning** in a limited program *context*
* **Can be used** as *identifiers outside the context*
* E.g. ```var, await, async, where, set```
* [List of all Contextual Keywords](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/)

+++
## Literals
* Data inserted in a code
```C#
var hitchhikerConstant = 42;
var helloWorld = "Hello World";
var pi = 3.14159;
```

+++
## Delimiters
* Characters used to structure the code
* Curly braces `{`, `}`
  * Creates **code blocks**
  * Used to **impart a scope**
* Semicolon `;`
  * **Delimits statements**
  * Statement *can be written on multiple lines*.

```C#
Console.WriteLine
    (1 + 2 + 3 + 4 + 5);
```

+++
## Operators
  * Used to **combine multiple expressions**
  * E.g., `. () * + -`

```C#
var sum = 1 + 5 * (6 / 2);
```

+++
## Comments
* Line
  ```C#
  // line comment
  ```
* Block -  **Do not use block comments!!!**
  ```C#
  /* Comment can be split
  into multiple lines */
  ```
* Documentation
  ```C#
  /// <summary>
  /// Documents class, method...
  /// </summary>
  ```

---
## Datatypes
@snap[north-east span-10]
![](/Lectures/Lecture_01/Assets/img/Overview_small.png)
@snapend

* Instruct the compiler or interpreter how the programmer intends to use the data
* **Value type**
  * Variable directly **contains data**
  * **Have to be** assigned before accessing
  * Two variables, each have their copy of the data; *an operation on one variable* **DOES NOT** *affect the other*.
* **Reference types** (objects)
  * Variable **stores reference** to the data
  * **DO NOT have to be** assigned before accessing
  * IF two variables reference the same object; *operation on one variable* **DOES** *affect the object referenced by the other variable*.
* [Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/types-and-variables)

+++
## Value Types
@snap[north-east span-10]
![](/Lectures/Lecture_01/Assets/img/Overview_small.png)
@snapend
* **Simple Types**
  * Signed integral: `sbyte, short, int, long`
  * Unsigned integral: `byte, ushort, uint, ulong`
  * Unicode characters: `char`
  * IEEE floating point: `float, double`
  * High-precision decimal: `decimal`
  * Boolean: `bool`
* **Enum types**
  * User-defined types of the form `enum E {...}`
* **Struct types**
  * User-defined types of the form `struct S {...}`
* **Nullable value types** - become reference types
  * Extensions of all other value types with a `null` value
  * [Boxing/Unboxing](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types#boxing-and-unboxing)

+++
### Signed Integral
| Type   |    Size | Range                                                              |
| ------ | ------- | ------------------------------------------------------------------ |
| `sbyte`|  8 bits | range from -128 - 127                                              |
| `short`| 16 bits | range from -32,768 - 32,767                                        |
| `int`  | 32 bits | range from -2,147,483,648 - 2,147,483,647                          |
| `long` | 64 bits | range from –9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 |

+++
### Unsigned integral
|  Type   |    Size | Range                                      |
| ------- | ------- | ------------------------------------------ |
| `byte`  |  8 bits | range from 0 - 255                         |
| `ushort`| 16 bits | range from 0 - 65,535                      |
| `uint`  | 32 bits | range from 0 - 4,294,967,295               |
| `ulong` | 64 bits | range from 0 - 18,446,744,073,709,551,615  |

+++
### Floating point
|   Type   |    Size | Range                                                               |
| -------- | ------- | ------------------------------------------------------------------- |
| `float`  | 32 bits | range from $$1.5 × 10^{−45} - 3.4 × 10^{38}$$  7-digit precision    |
| `double` | 64 bits | range from $$5.0 × 10^{−324} - 1.7 × 10^{308}$$  15-digit precision |

+++
### Decimal
|    Type   |    Size  | Range                                                                                         |
| --------- | -------- | --------------------------------------------------------------------------------------------- |
| `decimal` | 128 bits | range is at least $$–7.9 × 10^{−28} - 7.9 × 10^{28}$$ with at least 28-digit precision range |


+++
### Literals notation
* Classical
  * E.g., `127`, `42`, etc...
* Hexadecimal
  * E.g., `0x7F`, `0x2A`, etc...
* Binary
  * E.g., '0B110010', '0b0010_0110_0000_0011', etc...
* Decimal
  * `'.'` character as a delimiter
  * `'e'` character as an exponent

+++
### Numerical data types specification
Using specific character as a suffix

```C#
 Console.WriteLine(1f.GetType());  // Float   (float)
 Console.WriteLine(1d.GetType());  // Double  (double)
 Console.WriteLine(1m.GetType());  // decimal (decimal)
 Console.WriteLine(1u.GetType());  // UInt32  (uint)
 Console.WriteLine(1L.GetType());  // Int64   (long)
 Console.WriteLine(1ul.GetType()); // UInt64  (ulong)
```

+++
### Numerical data types casting
* Transformation of **integral type** to **integral type**:
  * *implicit* when *target type* can accommodate the whole range of *source type*
  * *explicit* otherwise
* Transformation of **decimal type** to **decimal type**:
  * `float` can be *implicitly* casted to `double`
  * `double` has to be casted *explicitly* to `float`
* Transformation of **integral type** to **decimal type**:
  * Casting is *implicit*
* Transformation of **decimal type** to **integral type**:
  * Casting has to be *explicit*
    * Lost precision
    * Truncation can occur

---
## Arithmetic operations
* `+` addition
* `-` subtraction
* `*` multiplication
* `/` division
* `++` incrementation
* `--` decrementation

+++
### Byte, sbyte, short, ushort types
* 8 and 16 bits types do not have arithmetical operations
  * E.g., `byte, sbyte, short, ushort`
  * Compiler does implicitly cast to a large type `int, uint`
```C#
short x = 1, y = 1;
short z = x + y;    // Compile-time error
```
  * Solution is to do an explicit cast
```C#
short x = 1, y = 1;
short z = (short)(x + y); // OK
```

+++
### Numerical Overflow
* Overflow of integral types
```C#
int a = int.MinValue;
a--;
Console.WriteLine(a == int.MaxValue); // True
```
* Usage of `checked` keyword or compiler option **/checked+**
```C#
int a = int.MinValue;
var i = checked(a--); // throw OverflowException
Console.WriteLine(i == int.MaxValue);
```

+++
### Truncation and precision loss
* `float` and `double` are stored in *binary form*
  * which means only multiples of 2 are stored precisely
```C#
float f1 = 0.09f * 100f;
float f2 = 0.09f * 99.999999f;
Assert.False(f1>f2);
```
* `decimal` is stored in decimal form, but it still has a limited precision
```C#
decimal m = 1M  /  6M;                          // 0.1666666666666666666666666667M
double  d = 1.0 / 6.0;                          // 0.16666666666666666
decimal notQuiteWholeM = m + m + m + m + m + m; // 1.0000000000000000000000000002M
double  notQuiteWholeD = d + d + d + d + d + d; // 0.99999999999999989
Console.WriteLine(notQuiteWholeM == 1M);        // False
Console.WriteLine(notQuiteWholeD < 1.0);        // True
```

+++
## Bitwise operations
| Operator |   Meaning   |      Example     |   Result    |
| -------- | ----------- | ---------------- | ----------- |
|    `~`   |     Not     |         `~0xfU`  | 0xfffffffOU |
|    `&`   |     And     |   `0xf0 & 0x33`  | 0x30        |
|  <code>&#124;</code> |      Or     |<code>0xf0 &#x7c; 0x33</code> | 0xf3        |
|    `^`   |     Xor     | `0xff00 ^ 0x00ff`| 0xffff      |
|   `<<`   |  Left shift |  `0x20 << 2`     | 0x80        |
|   `>>`   | Right shift |  `0x20 >> 1`     | 0x10        |

+++
## Character type
* `System.Char`/`char`
* Literal is denoted by a single-quote, e.g., `'a'`
* Can be cast to integral type
  * *Implicit* cast to `ushort`
  * *Explicit* cast to others

+++
## Boolean type
* `System.Boolean`/`bool`
* Store logical values
  * `true` or `false`

```C#
sizeof(bool) == sizeof(uint8) == sizeof(sbyte)
```

* Nothing can be casted to `bool`
* Operators:
  * Equality `==`, `!=`
  * Conditional operators `&&`, `||`

```C#
public bool UseUmbrella(bool rainy, bool sunny, bool windy) {
  return !windy && (rainy || sunny);
}
```
* Often used for the *Lazy evaluation*
```C#
public static Foo Foo => _foo ?? (_foo = new Foo()); //later explained
```

+++
### Enum
* Used for enumerations
* **Do not use magic values!**
* Default size is `int`, can be changed to `byte`, `sbyte`, etc...

```C#
enum Foo {
  none,
  one,
  two,
  ten = 10
}
```

+++
### Struct
* Similar to a class type
* Unlike classes, *structs* are **value types** and do not typically require heap allocation
* Struct types **do not** support
  * User-specified *inheritance*
  * Struct types implicitly inherit from type `System.ValueType` that inherits `System.Object`

```C#
struct Foo
{
  string foo;
}
```

+++
## Nullable value types
* **Do not have to be assigned** *before they can be accessed*
* Because they are reference types, thus their `default` value is `null`
* For each non-nullable value type `T` there is a corresponding nullable value type `System.Nullable<T>`, `T?`
  * With the same value range as `T` + **additional value** - `null`

```C#
int  ten = 10;
int? one = 1;
int? canBeNull = null;
int  cannotBeNull = null;      // Compile-time error
```

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/NullableType.cs&lang=C#&title=Nullable Type Sample
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/NullableType.cs)


+++
##  Reference types
@snap[north-east span-10]
![](/Lectures/Lecture_01/Assets/img/Overview_small.png)
@snapend
* **Class types**
  * Ultimate base class of all other types: `object`
  * Unicode strings: `string`
  * User-defined types of the form `class C {...}`
* **Interface types**
  * User-defined types of the form `interface I {...}`
* **Array types**
  * Single- and multi-dimensional, e.g., `int[]` and `int[,]`
* **Delegate types**
  * User-defined types of the form `delegate int D(...)`
* **Generics**
  * Parameterized with other types `MyGenericType<T>`

+++
### Class
* **Data structure** that contains:
  * Data members - *fields*
  * Function members - *methods*, *properties*, *events*, *indexers*, *user-defined operators*, *instance constructors*, *static constructors*, *destructors*
* Supports
  * **Single, transitive, inheritance**
  * Polymorphism
* Extends and specializes base class/es

```C#
class @Class{}

class Foo
{
  string foo;
}
```

+++
### Interface
* Think about it as a **contract**
* Named set of *public function members*
* A **class** or **struct** that **implements an interface** *must provide implementations of the interface’s function members*
* An **interface can inherit** *from multiple base interfaces*, and a **class or struct can implement** *multiple interfaces*

```C#
interface IInterface
{
  string FirstName { get; }
  string LastName { get; }
  string GetFullName();
}
```


+++
### Delegate
* **References to methods** with a *particular parameter list* and *return type*
* Method can be treated as an entity that can be assigned to variable and passed as a parameter
* Analogous to **function type** provided by *functional languages*
  * They are also similar to the **concept of function pointers** found in other languages
  * Unlike function pointers, delegates are object-oriented and **type-safe**

```C#
public delegate int PerformCalculation(int x, int y);

class MyClass
{
  PerformCalculation PerformCalculation = Sum;

  void CallDelegate()
  {
    PerformCalculation(1, 2);
  }

  static int Sum(int x, int y) => x + y;
}
```

+++
### String
* `System.String` / `string`
* Represents *sequence of characters*
* **Reference** data type
* Always *immutable*
* Literal is denote by double-quotes. e.g., `"string value"`
* Verbatim string is denote by `@` prefix, e.g.,
```C#
@"Multi-line
string"
```

+++
#### String concatenation
* `+` operator
* Not all operands need to be strings
* Non-string operands get called `ToString()` method on them
```C#
string s = "a" + 5; // a5
```
* For multiple string concatenation operations avoid usage of `+`, use:
  * `System.Text.StringBuilder`
  * `s = System.String.Format("{0} times {1} = {2}", i, j, (i*j));`
  * `s = $"{i} times {j} = {i*j}";`

+++
### Array
* Represents *fixed length data structure of homogeneous items*
* Stored in a sequential block of memory
* *Do not have to be declared before it can be used*
* **Initialization**
  * Value types - default value
  * Reference types - `null`
* Access out of array range throws `IndexOutOfRangeException`
* Array types are constructed by following a type name with square brackets
  * `int[]` *single-dimensional* array of int
  * `int[,]` *two-dimensional* array of int (matrix)
  * `int[][]` is a *single-dimensional array of single-dimensional* array of int

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/Array.cs&lang=C#&title=Array Sample
@[11-21]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/Array.cs)

---
### Variables
* Has a specific **type**, which determines:
  * The **size** and layout of the memory
  * The **range of values** that can be stored within that memory
  * The **set of operations** that can be applied

+++
#### Variable types

<table>
<thead>
<tr>
<th>Type</th>
<th>Value</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>Non-nullable</strong> type</td>
<td><em><ul>
  <li>value of that </em>exact type</li>
</ul></td>
</tr>
<tr>
<td><strong>Nullable</strong>  type</td>
<td><ul>
<li>null<em> value </li>
<li>value of that </em>exact type</li>
</ul></td>
</tr>
<tr>
<td><strong>Object</strong></td>
<td><ul>
<li><em>null</em> reference</li>
<li>reference to an <em>object</em> of any reference type</li>
<li>reference to a <em>boxed value</em> of any value type</li>
</ul></td>
</tr>
<tr>
<td><strong>Class</strong> type</td>
<td><ul>
<li><em>null</em> reference</li>
<li>reference to an <em>instance of that class</em> type</li>
<li>reference to an instance of a class <em>derived</em> from that class type</li>
</ul></td>
</tr>
<tr>
<td>⋮</td>
<td>⋮</td>
</tr>
</tbody>
</table>

+++
#### Variable types
<table>
<thead>
<tr>
<th>Type</th>
<th>Value</th>
</tr>
</thead>
<tbody>
<td><strong>Interface</strong> type</td>
<td><ul>
<li><em>null</em> reference</li>
<li>reference to an <em>instance of a class</em> type that <em>implements</em> that interface type</li>
<li>reference to a <em>boxed</em> value of a value type that implements that interface type</li>
</ul></td>
</tr>
<tr>
<td><strong>Array</strong> type</td>
<td><ul>
<li><em>null</em> reference</li>
<li>reference to an <em>instance of that array</em> type</li>
<li>reference to an <em>instance of a compatible array</em> type</li>
</ul></td>
</tr>
<tr>
<td><strong>Delegate</strong> type</td>
<td><ul>
<li><em>null</em> reference</li>
<li>reference to an <em>instance of a compatible delegate</em> type</li>
</ul></td>
</tr>
</tbody>
</table>


+++
### Stack vs Heap
* **Stack**
  * Allocated block of memory for *local variables, parameters, return values*
* **Heap**
  * Storage for *reference data types, static variables*
  * Managed by the *Garbage Collector*
* Therefore:
  * **Local variable** has to be *assigned before reading*
  * **Method** has to be *called with all arguments*
  * **All other** values are initialized automatically

+++
### Default values
|    Type   | Default value  |
| --------- | -------------- |
| Reference | `null`         |
| Numerical | `0`            |
| Enums     | `0`            |
| Char      | `'\0'`         |
| Boolean   | `false`        |

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/DefaultValue.cs&lang=C#&title=Default Value Sample
@[12-18]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/DefaultValue.cs)

---
## Parameters
* Parameters can be passed to a method as:
  * **Value**
  * **Ref** reference
    * Variable **may** *be modified* by the called method
  * **In** reference
    * Variable **cannot** *be modified* by the called method
  * **Out** reference
    * Variable **must** *be assigned* by the called method
    * Variable **need not** *to be initialized* before the method call

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/ValueParameter.cs&lang=C#&title=Value Parameter Sample
@[9-26]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/ValueParameter.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/RefParameter.cs&lang=C#&title=Ref Parameter Sample
@[7-17]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/RefParameter.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/InParameter.cs&lang=C#&title=In Parameter Sample
@[7-19]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/InParameter.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/OutParameter.cs&lang=C#&title=Out Parameter Sample
@[7-24]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/OutParameter.cs)

+++
### Parameter with `params[]`
* Can be used only as the *last parameter* in a method signature
* Has to be declared as an array
* Used to pass multiple variables of the same type

```C#
int Sum(params int[] items)
{
  return items.Sum();
}
```

```C#
var one   = Sum(1);
var two   = Sum(1, 2);
var three = Sum(1, 2, 3);
```

+++
### Optional parameters
* Has a default value as a part of its definition
* If omitted, the *default value* is used

```C#
void Foo(int x = 2, int y = 3) { … }
```
```C#
Foo();
Foo(1);
Foo(1, 2);
```

+++
### Named parameters
* Usually used with method calls on methods with *multiple optional parameters*
* Reduce the number of *method overrides*

```C#
void Foo(int x = 2, int y = 3) { … }
```
```C#
Foo(y:4, x:4);
Foo(y: ++a, x: --a);
Foo(y: 1);
```

---
## Operators
* Types
  * *unary* e.g. `++x, sizeof(int), +x, (int)x`
  * *binary* e.g. `x + y`
  * *ternary* e.g. `(input > 0) ? "positive" : "negative"`
* *Binary* operators use **infix** notation, operator is in between operands
* **Primary expression**
  * Used to build the language
  * `Math.Log(1)` contains two primary operators `.` and `()`
* [List of all operators](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/operators)

+++
## Expressions
* Usually, **returns some value** after computation
* The simplest expression is *constant* or *variable*, e.g., `5`
* Expression can be combined using operators

```C#
5*4
```

* If you are not sure about priority, use '()'
```C#
(5*4)+1
```

+++
### Void expressions
* *Do not have value*
* Cannot be combined with other operators
* E.g., `{}, return, etc...`

```C#
Expression<Action> tree = () => Console.WriteLine("Hello");
Expression<Action> tree2 = () => { Console.WriteLine("Hello"); }; // Compile-time error
```

* [An expression may be classified as] "nothing". 
  * This occurs when the expression is an *invocation of a method with a return type of void*. 
  * An expression classified as nothing *is only valid in the context of a statement expression*.

+++
### Assigning expression
* E.g., `x=x+5`
* Can be part of another expression
```C#
y = 5 * (x = 2);
```
* Can be used to initialize multiple variables:
```C#
a = b = c = d = e = 0;
```
* Combination of operators
  * `x+=5`, the same meaning as `x=x+5`

+++
### Priority and assignment
* Priority is evaluated by the *priority of operators*
* *The same priority* operators are evaluated starting with *the most left one*
* Left-associative operators
  * `8/4/2` equals `(8/4)/2`
* Right-associative operators
```C#
x = y = 3;
```

---
## Statements - Selection
* Used to define a program control flow
* `if`
* `switch`
* Conditional (ternary) operand `?:`

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/If.cs&lang=C#&title=If Sample
@[10-17]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/If.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/Switch.cs&lang=C#&title=Switch Sample
@[13-30]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/Switch.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/Switch.cs&lang=C#&title=Switch Sample C# 8
@[45-64]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/Switch.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/TernaryOperand.cs&lang=C#&title=Ternary Operand Sample
@[10-12]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/TernaryOperand.cs)

+++
## Statements - Cycles
* `while`
* `do while`
* `for`
* `foreach`


+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/While.cs&lang=C#&title=While Sample
@[10-16]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/While.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/DoWhile.cs&lang=C#&title=Do While Sample
@[10-15]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/DoWhile.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/For.cs&lang=C#&title=For Sample
@[10-13]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/For.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/ForEach.cs&lang=C#&title=Foreach Sample
@[10-14]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/ForEach.cs)

+++
## Statements - Jump statements
* `break`
* `continue`
* `return`
* `throw`
* `goto`
  * usage leads to [Spaghetti code](https://en.wikipedia.org/wiki/Spaghetti_code)


+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/Break.cs&lang=C#&title=Break Sample
@[10-20]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/Break.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/Continue.cs&lang=C#&title=Continue Sample
@[10-17]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/Continue.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/Return.cs&lang=C#&title=Return Sample
@[8-12]
@[17-20]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/Return.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/Throw.cs&lang=C#&title=Throw Sample
@[8-18]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/Throw.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/Goto.cs&lang=C#&title=Goto Sample
@[10-17]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/Goto.cs)

+++
## Statements - other
* `using`
  * Encapsulates the use of a disposable resource
* `lock`
  * For *safe access* to the resource from the concurrent context
  * Simplification of a *Monitor synchronization primitive*

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/Using.cs&lang=C#&title=Using Sample
@[10-13]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/Using.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-1/Tests/Lock.cs&lang=C#&title=Lock Sample
@[11-22]
@[24-35]
[Code sample](/Lectures/Lecture_01/Assets/sln-1/Tests/Lock.cs)

---
## Namespaces
* *Groups classes and interfaces to named groups*
* Namespace `System.Security.Cryptography` contains class, e.g., RSA
* Usage of types from a given namespace, e.g.,
```C#
System.Security.Cryptography.RSA rsa = System.Security.Cryptography.RSA.Create();
```

* Directive `using`
```C#
using System.Security.Cryptography;
public class Namespaces
{
  public void Method()
  {
    RSA rsa = RSA.Create(); // Don't need fully qualified name
  }
}
```

+++
### Keyword `namespace`

```C#
namespace Outer.Middle.Inner
{
  class Class1 { ... }
  class Class2 { ... }
}
```

* Same as:

```C#
namespace Outer
{
  namespace Middle
  {
    namespace Inner
    {
      class Class1 { ... }
      class Class2 { ... }
    }
  }
}
```

+++
### Namespaces - rules
* Names declared in an outer scope are implicitly imported into inner one

```C#
namespace Outer
{
  namespace Middle
  {
    internal class Class1 { ... }

    namespace Inner
    {
      internal class Class2 : Class1 { ... }
    }
  }
}
```

+++
### Repetition of namespaces
* Namespace name can be repeated until a collision of names of inner types occurs
* The same namespace can be declared in multiple places

```C#
namespace Outer.Middle.Inner
{
  class Class1 {}
}
```
```C#
namespace Outer.Middle.Inner
{
  class Class2 { }
}
```

+++
### Inner `using` directives
* `using` can be used in an inner namespace to limit its scope

```C#
namespace N1
{
  class Class1 { }
}
namespace N2
{
  using N1;
  class Class2 : Class1 { }
}
namespace N2
{
  class Class3 : Class1 { } // Compile-time error
}
```

---
## References:

[C# 8.0 in a Nutshell: The Definitive Reference](https://www.amazon.com/C-8-0-Nutshell-Definitive-Reference/dp/1492051136)
[Visual Studio Documentation](https://docs.microsoft.com/en-us/visualstudio)
[Microsoft Visual Studio](https://visualstudio.microsoft.com)
[Microsoft](https://www.microsoft.com)
[Resharper](https://www.jetbrains.com/resharper)
[Wikipedia](https://en.wikipedia.org)
[Programiz](https://www.programiz.com)
[C# in depth](http://csharpindepth.com)

+++
## Refences to used images:
[Amazon books](https://www.amazon.com/)
[Welcome to the Visual Studio IDE](https://docs.microsoft.com/en-us/visualstudio/ide/visual-studio-ide?view=vs-2017)
[Why Choose .NET?](https://www.microsoft.com/net/platform/why-choose-dotnet)
[Wikipedia .Net Framework](https://en.wikipedia.org/wiki/.NET_Framework)
[CLR In Process](https://scottdorman.github.io/2008/11/10/clr-4.0-in-process-side-by-side-clr-hosting/)
[CodeProject Improve garbage collector performance](https://www.codeproject.com/Articles/39246/NET-Best-Practice-No-2-Improve-garbage-collector)
[C# 8.0 in a Nutshell](http://www.albahari.com/nutshell/)
[.NET Core, .NET Framework, Xamarin](https://blogs.msdn.microsoft.com/cesardelatorre/2016/06/27/net-core-1-0-net-framework-xamarin-the-whatand-when-to-use-it/)

+++

## Credits
* Michal Orlíček - for slides preparation

---

---?include=/Lectures/Lecture_01/Assets/csharp-version-history.md

---

@snap[north span-100]

# Object-oriented Programming and Advanced Constructs in C#

@snapend

@snap[midpoint span-100]
## OOP, Exceptions, Events, Delegates, Lambda Expressions and Generics
@snapend

@snap[south-east span-40]

[ Jan Pluskal <ipluskal@fit.vutbr.cz> ]

---
## Object Oriented Programming (OOP)
* First appearance in **SIMULA 67**
* Abstraction of real word
* Real object(dog) has some properties(**length, a color of coat, ...**) and an ability to do something(**bark, bite**)
* OOP Object interconnects data and behavior together
  * **Behavior** is described by **procedures** and **functions**, both called **methods** in OOP
  * Data is stored in object's **member variable(field)**
  * **Methods** and **fields** together create objects

+++?code=/Lectures/Lecture_01/Assets/sln-2/Examples/Dog.cs&lang=C#&title=OOP Sample - Ingnore details for now!
@[10-28]

<!-- [Code sample](/Lectures/Lecture_01/Assets/sln-2/Examples/Dog.cs) -->

+++
## Three Principles of OOP

@snap[north-east span-10]
![](/Lectures/GitPitch/assets/image/Overview_small.png)
@snapend

* OOP interconnects **data** and **logic**
  * **Encapsulation**
  * **Inheritance**
  * **Polymorphism**

+++
### Encapsulation
* Hides implementation details
* Improves modularity
* Definitions:
  * A language mechanism for **restricting direct access** to some of the **object's components**
  * A language construct that **facilitates the bundling of data with the methods**(or other functions) operating on that data

+++
#### Access Modifiers
* Used for limiting access to *implementation details*
* Ensures *encapsulation* and leads to safe code
* **If a modifier is omitted, the most restrictive one is used**

+++
| Modifier | Visibility |
|-|-|
|`public` | accessible *everywhere* |
|`private` | accessible only in the **same** *class* or *struct* |
|`protected` | accessible only in the **same** *class*, or in a *class* **that is derived** from the same class |
|`internal` | accessible in the **same assembly**, but not from another assemblies |
|`protected internal` | accessible in the **same assembly** in which it is declared, **or** from within a **derived** *class* in another assembly(*internal* **OR** *protected*) |
|`private protected` |  accessible **only within its declaring assembly**, in the **same** *class*, **and** in a *class* **that is derived** from the same *class*(*internal* **AND** *protected*) |


+++?code=/Lectures/Lecture_01/Assets/sln-2/Examples/Dog.cs&lang=C#&title=Encapsulation Sample
@[12,17-21]
[Code sample](/Lectures/Lecture_01/Assets/sln-2/Examples/Dog.cs)

+++
### Inheritance
* Create objects that are built upon existing objects
* Specify a new implementation to maintain the same behavior
* Reuse code and to independently extend original software via public classes
* An *inherited class* is called a **subclass** of its **parent class** or **superclass** or **base class**

+++
#### Identificators
  * `null` - a reference that  *points to nowhere*
  * `this` - a reference to a *current instance* of an object
  * `base` - a reference to a *subtype* of a *super class*

+++?code=/Lectures/Lecture_01/Assets/sln-2/Examples/Animal.cs&lang=C#&title=Inheritance Sample
[Code sample](/Lectures/Lecture_01/Assets/sln-2/Examples/Animal.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-2/Examples/Pet.cs&lang=C#&title=Inheritance Sample
[Code sample](/Lectures/Lecture_01/Assets/sln-2/Examples/Pet.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-2/Examples/Dog.cs&lang=C#&title=Inheritance Sample
@[10-28]
[Code sample](/Lectures/Lecture_01/Assets/sln-2/Examples/Dog.cs)

+++
#### Inheritance and Subtyping
* In some languages inheritance and subtyping are no different
* Generally in statically-typed class-based OO languages, such as(C++, C#, Java), whereas in others they differ
  * **subtyping** *establishes an is-a relationship*
  * **inheritance**:
    * only *reuses implementation and establishes* a **syntactic relationship**
    * not necessarily a **semantic relationship**, inheritance does not ensure behavioral subtyping
* **Subtyping** is also known as **interface inheritance** whereas inheritance, as defined here, is known as implementation inheritance or code inheritance
* Still, **inheritance** is a commonly used mechanism for establishing subtype relationships

+++
### Polymorphism
* Is the provision of a *single interface* to *entities of different types*
* A polymorphic type is one whose operations can also be applied to values of some other type, or types

+++
#### Polymorphism Types
* **Ad hoc polymorphism**:
  * **Function overloading**
  * *Function denotes different and potentially heterogeneous implementations* depending on a limited range of *individually specified types and combinations*
* **Parametric polymorphism**:
  * Code is written *without mention of any specific type* and thus *can be used transparently with any number of new types*
  * This is often known as **generics** in OOP, and *polymorphism* in functional programming
* **Subtyping**(*subtype polymorphism* or *inclusion polymorphism*):
  * Name denotes instances of many different classes related by some common *base* class

---
## Class
* The most common of *reference types*
* Think about it s a *construction plan for an object*
* **Encapsulates** *data* and *behavior*
  ```C#
  class Foo
  {
  }
  ```

+++
### Static/non-static
* `static` classes - **only one** instance for an **AppDomain**
  * *static members* belong to the **class** not the *object*
* **non-static** - classes are **instantiated** during program run
  * *non-static members* belongs to the *object*

+++
### Class may contain
| | |
|-|-|
|*Preceding* **the class keyword** |Attributes and class modifier |
|*Following* **the class Name**    |Generic type parameters, a base class, and interfaces|
|*Within* **the class body**       |Methods, properties, indexers, events, fields, constructors...|

+++
### Class Components
* **fields** – a *member variable*
* **properties** – *methods* that are accessed as if they were fields
* **constants** - *fields* or properties whose values are set at compile time and cannot be changed
* **methods** - named *procedures or functions*
* **events** - *notify* on object state changes
* **operators** - overloaded operators
* **indexers** - allow object to *be indexed as an array*
* **constructors** - **methods** that run initialization code
* **finalizer** - **method** called during object destruction
* **nested types** - *types declared within* a class scope

+++
### Field
* Variable that is a member of a *class* or *struct*
* Initialization is:
  * *Optional*
  * Non-initiated has a *default* value (`0, \0, null, false`)
  * Before a constructor call
    ```C#
    class Octopus
    {
      string name;
      public int Age = 10;
    }
    ```

+++
#### Field Modifiers
* `static`
* access - `public, internal, private, protected, protected internal, private protected`
* inheritance - `new`
* unsafe code - `unsafe`
* `readonly` - cannot be changed after construction
* threading - `volatile`

+++
### Method
* *Procedures* and *functions* are in OOP called *methods*
* Can access members of `class` or `struct`
* Can
  * accept parameters - *values*, *reference types*, `ref`, `in`
  * return result - in return type `return`, or `ref` or `out` parameters

+++
#### Method Modifiers
* `static`
* access - `public, internal, private, protected, protected internal, private protected`
* inheritance - `new, virtual, abstract, override, sealed, partial`
* unsafe code - `unsafe, extern`
* asynchronous - `async`

+++
#### Method Types
* Method contains only one expression
* Classical method:

```C#
int Foo(int x) { return x * 2; }
```

* Expression-bodied method:

```C#
int Foo(int x) => x * 2;
```

* Method with empty return type - `void`:

```C#
void Foo(int x) => Console.WriteLine(x);
```

+++
#### Method Signature
* Methods are declared in a *class* or *struct* by specifying:
  *  the **access level** such as public or private,
  *  **optional modifiers** such as abstract or sealed,
  *  the **return value**,
  *  the **name** of the method,
  *  and any method **parameters**.
* These parts together are the **signature of the method**.

* A **return type** *is not part of the signature* for the purposes of method **overloading**.
* A **return type** *is part of the signature* when determining the compatibility between a **delegate** and the method that it points to.

+++
#### Method Overloads
* **Return type** is not a part of the signature for overloading purpose
  ```C#
  void Foo(int x) {...}
  int  Foo(int x) {...} // Compile-time error
  ```
* Method overloads can have different return types
  ```C#
  int    Foo(int x) {...}
  double Foo(double x) {...} // OK
  ```

+++
#### Local Methods
* Defines a **method** *inside another method*
* Is visible only to the enclosing method
* Can access the local variables and parameters of the enclosing method
  ```C#
  void WriteCubes()
  {
    Console.WriteLine(Cube(3));
    Console.WriteLine(Cube(4));
    Console.WriteLine(Cube(5));

    int Cube(int value) => value * value * value;
  }
  ```

+++
### Property
* Similar to a *field*, but **encloses it with an access method**
* It is a safety mechanism that unifies *read* and *write* operations
* Hides *implementation details*

+++
#### Read-only and Calculated Property
* *Read-only* if it specifies only a `get` accessor
* *Write-only* if it specifies only a `set` accessor

```C#
private decimal _foo;

public decimal Foo1
{
  get { return _foo; }
}
public decimal Foo2
{
  private set { _foo = Math.Round(value, 2); }
}
```

+++
#### Get and Set Accessibility
* *Get* and *set* accessors can have different access levels
* Typical use:
  * `public` property
  * `internal` or `private` access modifier on the *setter*

```C#
private decimal _foo;

public decimal Foo
{
  get { return _foo; }
  private set { _foo = Math.Round(value, 2); }
}
```

+++
#### Property Modifiers
* `static`
* access - `public, internal, private, protected, protected internal, private protected`
* inheritance - `new, virtual, abstract, override, sealed`
* unsafe code - `unsafe, extern`

+++
#### Property Types
* Autogenerated property:

```C#
public string Foo {get; set;}
```

* Full property(with the backing field):

```C#
private string _foo;

public string Foo {
  get { return _foo; }
  set { _foo = value; }
}
```

+++
#### Expression-bodied Property
* With only get accessor:

```C#
public string Name => _name;
```

* With set accessor:

```C#
public string Name {
  get => return _name;
  set => _name = value;
}
```

+++
### Constructor
* Run initialization code on a **class** or **struct**
* Defined like a method
  * Method *name and return type* are reduced to the *name of the enclosing type*
* Constructors of the *base* class are accessible

+++?code=/Lectures/Lecture_01/Assets/sln-2/Examples/Panda.cs&lang=C#&title=Constructor Sample
[Code sample](/Lectures/Lecture_01/Assets/sln-2/Examples/Panda.cs)

+++
#### Implicit Parameterless Constructor
* `public`, *parameterless*
* Generated by C# compiler automatically
* If, and only if, **you do not define any other constructor**

+++
#### Constructor Modifiers
* Access - `public, internal, private, protected, protected internal`
* Unsafe code - `unsafe, extern`

+++
#### Constructor Overloading
* Type can have *multiple constructors*
* The same rules as method *overloading*
* Protects against code duplication and increases readability
* Keywords
  * `this` - refers to *this* type instance
  * `base` - refers to *base* class type instance

+++?code=/Lectures/Lecture_01/Assets/sln-2/Examples/WildCat.cs&lang=C#&title=Constructor Overloading Sample
[Code sample](/Lectures/Lecture_01/Assets/sln-2/Examples/WildCat.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-2/Examples/Cat.cs&lang=C#&title=Constructor Overloading Sample
[Code sample](/Lectures/Lecture_01/Assets/sln-2/Examples/Cat.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-2/Tests/Constructor.cs&lang=C#&title=Constructor Overloading Test
@[8-14]
[Code sample](/Lectures/Lecture_01/Assets/sln-2/Tests/Constructor.cs)

+++
### Deconstructors
* Opposite of a constructor
* From C# 7
* Deconstruction method must
  * Be called **Deconstruct**
  * Have one or more out parameters

+++?code=/Lectures/Lecture_01/Assets/sln-2/Examples/Rectangle.cs&lang=C#&title=Deconstructor Sample
[Code sample](/Lectures/Lecture_01/Assets/sln-2/Examples/Rectangle.cs)

+++
#### Call Deconstructor
```C#
var rect = new Rectangle(3, 4);
```

```C#
(float width, float height) = rect; // Deconstruction
Console.WriteLine(width + " " + height); // 3 4
```

or

```C#
float width, height;
rect.Deconstruct(out width, out height);
```

or

```C#
rect.Deconstruct(out var width, out var height);
```

or

```C#
(var width, var height) = rect;
```

or

```C#
var(width, height) = rect;
```

+++
### Finalizer
* Runs on an instance of an object when it is referenced no more before the garbage collection
* `override`s `System.Object`'s method `Finalize()`

```C#
protected override void Finalize() {
  // Cleanup code
  ...
  base.Finalize();
}
```

<div class="center">
or simply
</div>

```C#
class Dog {
  ~Dog() {
    // Cleanup code
  }
}
```


+++
### Abstract Class
* **Can never be instantiated**
* Only its concrete subclasses can be instantiated
* Cannot be `sealed`, it must be inheritable
* Is able to define `abstract` members:
  * Like `virtual` members, except they don’t provide a default implementation
  * Implementation must be provided by the **subclass** unless that **subclass** is also declared `abstract`

+++
### Abstract Class Example
```C#
public abstract class Asset
{
  // Note empty implementation
  public abstract decimal NetValue { get; }
}
```

```C#
public class Stock: Asset
{
  public long SharesOwned { get; set; }
  public decimal CurrentPrice { get; set; }

  /// Overriden, like a virtual method.
  public override decimal NetValue => CurrentPrice * SharesOwned;
}
```

+++
### Virtual
* Implementation can be overridden in subclasses
* To provide a specialized/concrete implementation
* Mechanism of **late binding**
* Virtual can be:
  * **Methods**
  * **Properties**
  * **Indexers**
  * **Events**

+++
### Type Compatibility
* Ease-up usage of *subtypes*, ergo *virtual methods*
* Compatibility of *types* of `class`, `struct` instances
* Determines which type references can be assigned into another type reference

+++
#### Up-cast
* Creates a *base* class reference from a *subclass* reference
* Only *members* provided by given *base* class can be accessed through up-casted reference

+++?code=/Lectures/Lecture_01/Assets/sln-2/Tests/UpCast.cs&lang=C#&title=Upcast Example
@[10-17]
[Code sample](/Lectures/Lecture_01/Assets/slnTests/UpCast.cs)

+++
#### Down-cast
* Creates a *subclass* reference from a *base* class reference
* It **fails**, if *base* class instance is not compatible with *inherited* one

+++?code=/Lectures/Lecture_01/Assets/sln-2/Tests/DownCast.cs&lang=C#&title=Downcast Example
@[11-17]
@[20-25]
[Code sample](/Lectures/Lecture_01/Assets/slnTests/DownCast.cs)

+++
#### Operator `as`
* Downcasts
* Returns `null`, if failed

+++?code=/Lectures/Lecture_01/Assets/sln-2/Tests/AsOperator.cs&lang=C#&title=AS Operator Example
@[9-14]
[Code sample](/Lectures/Lecture_01/Assets/slnTests/AsOperator.cs)

+++
#### Operator `is`
* Tests whether a reference conversion would succeed
* Usually before downcast

+++?code=/Lectures/Lecture_01/Assets/sln-2/Tests/IsOperator.cs&lang=C#&title=IS Operator Example
@[10-15]
[Code sample](/Lectures/Lecture_01/Assets/slnTests/IsOperator.cs)

+++?code=/Lectures/Lecture_01/Assets/sln-2/Tests/PatternMatching.cs&lang=C#&title=IS Pattern Matching Example
@[10-18]
@[21-28]
[Code sample](/Lectures/Lecture_01/Assets/slnTests/PatternMatching.cs)

+++
### Sealed
* Restricts
  * Inheritance of `class`
  * Overriding of *method*

```C#
class Animal { }
```

```C#
sealed class Cat: Animal { }
```

```C#
//Compile-time error
public class Kitten : Cat {}
```

+++
### System.Object
* Object(`System.Object`) is a common `base` class of all types
* Each type can be cast to `System.Object`
* `System.Object` methods:
  * `ToString()`
  * `Equals()`
  * `GetHashCode()`
  * `GetType()`
* To get type:
  * during *runtime* - `Object.GetType()`
  * during *compile time* - `typeof(object)`

+++
### Partial class/method
* Allows to split declaration across multiple files
* Each participant must have the `partial` declaration
* Typically used in WPF, Winforms
  * one file is auto-generated
  * one file is human edited

```C#
partial class PaymentForm // In auto-generated file
{
  // ...
  partial void ValidatePayment(decimal amount);

  public void InvokeValidation(){
    ValidatePayment(10);
  }
}
```

```C#
partial class PaymentForm // In hand-authored file
{
  // ...
  partial void ValidatePayment(decimal amount)
  {
    if(amount > 100)
    // ...
  }
}
```

---
## Struct
* Similar to a class, with the following key differences:
  * A `struct` is a **value type**, whereas a class is a **reference type**
  * A `struct` does not support inheritance(other than implicitly deriving from `System.ValueType`)
* Can have all the members as class, except:
  * A parameter-less constructor(is implicit)
  * Field initializers
  * A finalizer
  * Virtual or protected members
* Each constructor has to initialize all `struct`'s members
* Members cannot be initialized in `struct`'s declaration

+++
```C#
public readonly struct Point
{
  int x, y;
  public Point(int x, int y)
  {
    this.x = x;
    this.y = y;
  }
}

Point p1 = new Point(1, 1); // p1.x and p1.y will be 1
Point p2 = new Point(); // p2.x and p2.y will be 0
```

---
## Enums, Flags
* `enum` is a *value type*
  * creates an enumeration of named numerical values(int, 0,1...)
  * underlying type can be changed to `long`, `short`, `byte` 

* `enum` with the attribute `flags`
  * *single variable* may contain *multiple values*

```C#
enum HorseColor { Bay = 0, Palomino = 5, Chestnut = 10 }

HorseColor color = HorseColor.Bay;
int colorNumber  = (int) HorseColor.Chestnut;

HorseColor.TryParse("Chestnut", out HorseColor color);
```

```C#
[Flags] enum HorseType { None = 0, Racing = 1,
Breeding = 2, ForSausages = 4, Dead = 8 }

HorseType type  = HorseType.Racing | HorseType.Breeding;
          type |= HorseType.ForSausages ;
Console.WriteLine(type); //Racing, Breeding, ForSosages
```

---
## Interface
* Declares only *specification*, not *implementation* of its members
* All members are `public`
* `class` or `struct` can implement **multiple** `interface`s
* Implementation is provided by `class` or `struct` that implements particular `interface`
* `interface` can declare
  * **methods**
  * **properties**
  * **events**
  * **indexers**

```C#
public interface IEnumerator
{
  bool MoveNext();
  object Current { get; }
  void Reset();
}
```

+++
#### `interface` vs `abstract`

* Use *inheritance* for types that share implementation
* Use `interface` for types that have independent implementations
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

* Because animals might share some implementation of their taxonomy, it is possible to declare `Bird` and `Insect` as `abstract class`.
* But, their food intake and whether they fly or not might differ. It is best to declare these properties as `interfaces`, `IFlying` and `ICarnivore`.

+++
#### `class` vs `interface`
* `class` is considered to be a *type*
  * *Data* are stored in member variables
  * *Operations* are declared in methods
* `interface`
  * describes `class` members
  * behavior is defined in `class` that implements it
* *Multiple inheritance* is not supported
* *Multiple* `interface` *implementation* is supported

```C#
public interface IName {
  string Name {get;}
}
```

```C#
public class Pet: IName {
  public string Name { }
}
```

+++
#### Type Safety and Security
* **Strongly typed language**
  * *type* has to be known at *compile time*
* Support of Intellisense in Visual Studio
* Keyword `dynamic` overcomes type safety mechanisms, and type is resolved at *runtime*
* Benefits:
  * Elimination of type issues at *compile time*
  * Sandboxing protects object state against outer modifications

---
## Generics
* C# has two mechanisms for *reusable code across different types*
  * *Inheritance* - expresses reusability with a *base type*
  * *Generics* - express reusability with a *"template"* that contains "placeholder" types
    * *Type safe* code
    * *Reduce casting and boxing*

+++
### Non-generict *object* Stack
```C#
public class ObjectStack
{
  int position;
  object[] data = new object[100];
  public void Push(object obj) => data[position++] = obj;
  public object Pop() => data[--position];
}
```

```C#
ObjectStack stack = new ObjectStack();
stack.Push("s"); // Wrong type, but no error!
int i =(int)stack.Pop(); // Downcast - runtime error
```

+++
### Generics Types
* Declares type parameter/placeholder types to be filled in by the consumer of the generic type
  * i.e., `Stack<T>`, designed to stack instances of type `T`:

```C#
public class Stack<T>
{
  int position;
  T[] data = new T[100];
  public void Push(T obj) => data[position++] = obj;
  public T Pop() => data[--position];
}
```

usage:

```C#
var stack = new Stack<int>();
stack.Push(5);
stack.Push(10);
Assert.Equal(10,stack.Pop());
Assert.Equal(5,stack.Pop());
```

+++
### Generics Open/Close Types
* *Opened type* – `Stack<T>`
* *Closed type* – `Stack<int>`
  * During *runtime* all generics are of *closed type*

```C#
var stack = new Stack<T>(); // Compile-time error outside generic type or method
```


```C#
public class Stack<T>
{
 ...
 public Stack<T> Clone()
 {
 Stack<T> clone = new Stack<T>(); // Legal
 ...
 }
}
```

+++
### Why Generics
* **Reusable across different types**
  * i.e., we need a *stack* for multiple types, we can use:
    * **Generics**, or
    * Have a separate version, of the same class, for every encapsulated type, or
      * (e.i., `IntStack`, `StringStack` etc..)
    * Have *stack* that is generalized by using object:
      * ValueType requires boxing,
      * down-casting that cannot not be checked at compile time

+++
### Generic Methods
* Several basic algorithms can be implemented using *generic methods*.
* *Signature* of generic method contains generic type parameter.
* *Generic method* may contain multiple *generic parameters*

```C#
static void Swap<T>(ref T a, ref T b) {
  T temp = a;
  a = b;
  b = temp;
}
```

+++
### Generic Constraints
* Parameters can be restricted with:
  * `where T : <base class name>` - T must be or derive from the specified base class.
  * `where T : <interface name>` - T must be or implement the specified interface
  * `where T : class` - T must be a reference type
  * `where T : struct` - T must be a value type, not nullable
  * `where T : new()` - T must have a public parameterless constructor
  * `where T : U` - T must be or derive from the argument supplied for U
  * `where T : unmanaged` - T must not be reference type, and must not contain any reference type members at any level of nesting


---
## Covariance and Contravariance
* [Read more](https://docs.microsoft.com/en-us/dotnet/standard/generics/covariance-and-contravariance)

* **Covariance** allows use of more derived(more specific) type than originally specified.
  * You can assign an instance of `IEnumerable<Derived>` to a variable of type `IEnumerable<Base>`.

```C#
IEnumerable<Derived> d = new List<Derived>();
IEnumerable<Base> b = d;
```

* **Contravariance** allows a use less derived(less specific) type than initially specified.
  * You can assign an instance of `Action<Base>` to a variable of type `Action<Derived>`

```C#
Action<Base> b =(target) => { Console.WriteLine(target.GetType().Name); };
Action<Derived> d = b;
```

* **Invariance** use of only the same type as initially specified.
  * Invariant generic type parameter is neither **covariant** nor **contravariant**.
  * You **cannot** assign an instance of `List<Base>` to a variable of type `List<Derived>` or vice versa.

+++?code=/Lectures/Lecture_01/Assets/sln-2/Examples/CovarianceContravariance.cs&lang=C#&title=Covariance Contravariance Example
@[11]
@[27-30]
@[33-36]
@[11-24]
[Code sample](/Lectures/Lecture_01/Assets/sln-2/Examples/CovarianceContravariance.cs)

+++
## Boxing/Unboxing
* C#'s type system is unified such that a value of *any type can be treated as an `object`*.
* Every type in C# directly or indirectly derives from the `object` class type, and `object` is the ultimate *base class* of all types
* Values of reference types are treated as objects simply by viewing the values as type object
* Values of value types are treated as objects by performing **boxing** and **unboxing** operations

+++?code=/Lectures/Lecture_01/Assets/sln-2/Tests/Boxing.cs&lang=C#&title=Boxing Sample
@[8-16]
[Code sample](/Lectures/Lecture_01/Assets/slnTests/Boxing.cs)

---
## Exceptions
* C# has [*structured exception handling*](https://docs.microsoft.com/en-us/windows/desktop/debug/structured-exception-handling)
* Improves code readability
* `try` block
  * Must be followed by:
    * `catch` block
    * `finally` block
    * or both
* `catch` block
  * *Executes when an error occurs* in the `try` block
  * Has access to the *exception object that contains information about the error*
* `finally` block
  * *Executes always*, whether or not an error occurred

+++
### `try`, `catch`, `finally` example
```C#
try
{
 ... // exception may get thrown within execution of this block
}
catch(ExceptionA ex)
{
 ... // handle exception of type ExceptionA
}
catch(ExceptionB ex)
{
 ... // handle exception of type ExceptionB
}
finally
{
 ... // cleanup code - unmanaged resources
}
```

+++
### Exception Handling
* If exception is `throw`n in `try` statement:
  * Execution is passed to the compatible `catch` block
  * If the `catch` block successfully finishes
    * If present, Execution is passed to `finally` block
    * Execution moves to the next statement after the `try` statement
* If exception isn't in `try` statement, or is not caught by any `catch` block in the *callstack*:
  * the process is terminated and error message is displayed to the user

@snap[south-east]
[SOURCE](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/exceptions/)
@snapped

+++
### The `catch` block
* Specifies what type of exception to catch
  * This must either be `System.Exception` or a subclass of `System.Exception`
* Only one catch clause executes for a given exception
  *  More specific handler needs to be declared before general one

+++
#### Multiple `catch` Clauses Example

```C#
class Test
{
  static void Main(string[] args)
  {
    try
    {
      byte b = byte.Parse(args[0]);
      Console.WriteLine(b);
    }
    catch(IndexOutOfRangeException ex)
    {
      Console.WriteLine("Please provide at least one argument");
    }
    catch(FormatException ex)
    {
      Console.WriteLine("That's not a number!");
    }
    catch(OverflowException ex)
    {
      Console.WriteLine("You've given me more than a byte!");
    }
  }
}
```

+++
#### `catch` Simplify Examples
* Exception can be caught without specifying a variable
  ```C#
  catch(OverflowException) // no variable
  {
  ...
  }
  ```
* Furthermore, you can omit both the variable and the type
  ```C#
  catch { ... }
  ```
* Exception filters
  ```C#
  catch(WebException ex) when(ex.Status == WebExceptionStatus.Timeout)
  {
    ...
  }
  ```

+++
### The `finally` Block
* Executes always
  * Whether or not an exception is thrown
  * Whether or not the `try` block runs to completion
* Typically used to handle unmanaged resource

+++
#### The `finally` Block Example

```C#
static void ReadFile()
{
  StreamReader reader = null; // In System.IO namespace
  try
  {
    reader = File.OpenText("file.txt");
    if(reader.EndOfStream) return;
    Console.WriteLine(reader.ReadToEnd());
  }
  finally
  {
    if(reader != null) reader.Dispose();
  }
}
```

If object implements IDisposable, use `using` clause!

+++
### Throwing Exceptions Example
```C#
class Test
{
  static void Display(string name)
  {
    if(name == null)
      throw new ArgumentNullException(nameof(name));
    Console.WriteLine(name);
  }

  static void Main()
  {
    try { Display(null); }
    catch(ArgumentNullException ex)
    {
      Console.WriteLine("Caught the exception");
    }
  }
}
```

+++
### Rethrow Examples
* Rethrow the same exception

```C#
try { ... }
catch(Exception ex)
{
  // Log error
  ...
  throw; // Rethrow the same exception
}
```

* Rethrow a more specific exception

```C#
try
{
  ... // Parse a DateTime from XML element data
}
catch(FormatException ex)
{
  throw new XmlException("Invalid DateTime", ex);
}
```

+++
### Key Properties of `System.Exception`

* `StackTrace`
  * A string representing all the methods that are called from the origin of the exception to the catch block
* `Message`
  * A string with a description of the error
* `InnerException`
  * The inner exception(if any) that caused the outer exception
  * InnerException may have another InnerException

+++
### Common Exception Types
* `System.ArgumentException`
  * Thrown when a function is called with a bogus argument
* `System.ArgumentNullException`
  * Subclass of `ArgumentException`
  * Thrown when a function argument is(unexpectedly) null
* `System.ArgumentOutOfRangeException`
  * Subclass of `ArgumentException`
  * When a(usually numeric) argument is out of range(usually too big or too small)
* `System.InvalidOperationException`
  * Thrown when the state of an object is unsuitable for a method to successfully execute

+++
* `System.NotSupportedException`
  * Thrown to indicate that particular functionality is not supported
* `System.NotImplementedException`
  * Thrown to indicate that a function has not yet been implemented
* `System.ObjectDisposedException`
  * Thrown when the object, upon which the function is called, has been disposed
* `NullReferenceException`
  * The CLR throws this exception
  * Thrown when you attempt to access a member of an object whose value is null


---
## Delegates
* Is a type that represents references to methods with a particular *parameter list* and *return type*.
* When you instantiate a delegate, you can associate its instance with any method with a *compatible signature and return type*.

```C#
delegate int Transformer(int x);
```

is compatible with

```C#
static int Square(int x) => x * x;
```

+++
### Delegates Example
```C#
delegate int Transformer(int x);
...
class Test
{
  static void Main()
  {
    Transformer transformer = Square; // Create delegate instance
    int result = transformer(3);      // Invoke delegate
    Console.WriteLine(result);       // 9
  }
  static int Square(int x) => x * x;
}
```

+++
### Delegates Shorthand

The statement:

```C#
Transformer transformer = Square;
```

is equivalent:

```C#
Transformer transformer = new Transformer(Square);
```

The expression:

```C#
transformer(3)
```

is equivalent:

```C#
transformer.Invoke(3)
```

+++
#### Plug-in Methods with Delegates
```C#
public delegate int Transformer(int x);

class Util {
  public static void Transform(int[] values, Transformer transformer)
  {
    for(int i = 0; i < values.Length; i++)
    values[i] = transformer(values[i]);
  }
}

class Test {
  static void Main() {
    int[] values = { 1, 2, 3 };
    Util.Transform(values, Square); // Hook in the Square method
    foreach(int i in values)
      Console.Write(i + " "); // 1 4 9
  }

  static int Square(int x) => x * x;
}
```

The `Transform` method is a higher-order function(it’s a function that takes a function as an argument).

+++
### Multicast Delegates
* Delegate instance can reference a list of target methods
* The `+` and `+=` operators combine delegate instances
* The `-` and `-=` operators remove delegate instances
```C#
SomeDelegate d = SomeMethod1;
d += SomeMethod2;
```
* Invoking d will now call both `SomeMethod1` and `SomeMethod2`
* Delegates are invoked in the order they are added
* The caller receives the return value from the last method
  * Preceding methods return values are discarded

+++
#### Multicast Delegates Example - Invocation
```C#
public delegate void ProgressReporter(int percentComplete);

public class Util
{
  public static void HardWork(ProgressReporter progressReporter)
  {
    for(int i = 0; i < 10; i++)
    {
      progressReporter(i * 10); // Invoke delegate
      System.Threading.Thread.Sleep(100); // Simulate hard work
    }
  }
}
```

+++
#### Multicast Delegates Example - Declaration
```C#
class Test
{
  static void Main()
  {
    ProgressReporter progressReporter = WriteProgressToConsole;
    progressReporter += WriteProgressToFile;
    Util.HardWork(p);
  }

  static void WriteProgressToConsole(int percentComplete)
    => Console.WriteLine(percentComplete);

  static void WriteProgressToFile(int percentComplete)
    => System.IO.File.WriteAllText("progress.txt",
       percentComplete.ToString());
}
```

+++
### Instance Method/Target

* `Delegate.Method` - Gets the method represented by the delegate.
* `Delegate.Target` - Gets the class instance on which the current delegate invokes the instance method.

```C#
public delegate void ProgressReporter(int percentComplete);

class Program
{
  static void Main()
  {
    Foo foo = new Foo();
    ProgressReporter progressReporter = foo.InstanceProgress;
    progressReporter(99); // 99
    Console.WriteLine(progressReporter.Target == foo); // True
    Console.WriteLine(progressReporter.Method); // Void InstanceProgress(Int32)
  }
}

class Foo
{
  public void InstanceProgress(int percentComplete)
    => Console.WriteLine(percentComplete);
}
```


+++
#### `delegate` vs `interface`
* A problem that can be solved with a delegate can also be solved with an interface
* **Delegate design may be better** if:
  * The interface would define only a single method
  * Multicast capability is needed
  * The subscriber needs to implement the interface multiple times

+++
#### Delegate Compatibility
* All delegates are incompatible with one another

```C#
delegate void Delegate1();
delegate void Delegate2();
...
Delegate1 delegate1 = Method1;
Delegate2 delegate2 = delegate1; // Compile-time error
```

+++
#### Delegate Equality
* Delegates are equal if they reference the same methods in the same order
```C#
delegate void Delegate();
...
Delegate delegate1 = Method1;
Delegate delegate2 = Method1;
Console.WriteLine(delegate1 == delegate2); // True
```

---
## Events
* Construct that exposes the subset of delegate features required for the broadcaster/subscriber model
* [Read more](https://docs.microsoft.com/en-us/dotnet/csharp/distinguish-delegates-events)
```C#
public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

public class Broadcaster
{
  // Event declaration
  public event PriceChangedHandler PriceChanged;
}
```

+++
### Standard Event Pattern
* Used to provide consistency across Framework and user code
* Standard Event Pattern `EventArgs`
  * `System.EventArgs`
  * Predefined class with no members
  * Base class for conveying information for an event

```C#
public class PriceChangedEventArgs : System.EventArgs
{
  public readonly decimal LastPrice;
  public readonly decimal NewPrice;

  public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
  {
    LastPrice = lastPrice;
    NewPrice = newPrice;
  }
}
```

+++
#### Standard Event Pattern - Delegate
* name must end with `EventHandler`
* two arguments
  * the first a subclass of `object` *(broadcaster)*
  * the second a subclass of `EventArgs` *(extra information)*
* return type `void`
* .NET defines a generic delegate `System.EventHandler<T>`
  * can be used when an event doesn’t carry extra information

```C#
public delegate void EventHandler<TEventArgs>
(object source, TEventArgs e) where TEventArgs : EventArgs;
```

+++
#### Standard Event Pattern - Example
```C#
public class Stock
{
  ...
  public event EventHandler<PriceChangedEventArgs> PriceChanged;

  protected virtual void OnPriceChanged(PriceChangedEventArgs e)
  {
    PriceChanged?.Invoke(this, e);
  }
}
```

+++
### Event Modifiers
* `virtual`
* `override`
* `abstract` - the compiler will not generate the `add` and `remove` event accessor
* `sealed`
* `static`

---
## Lambda Expressions
* From C# 3.0
* *Anonymous function* written in place of a delegate instance
* Form **(parameters) => expression-or-statement-block**
  ```C#
  x => x * x;
  ```
* parameter `x`
* expression `x * x`
  ```C#
  x => { return x * x; };
  ```
* Parameter `x`
* Statement block `{ return x * x; }`

+++
### Lambda Expressions Usage Example
  ```C#
  delegate int Transformer(int i);
  ...
  Transformer sqr = x => x * x;
  Console.WriteLine(sqr(3)); // 9
  ```
* `x` corresponds to `i`
* `x * x` corresponds to return type `int`

+++
### Lambda Expressions Two Parameters Example

```C#
Func<string,string,int> totalLength = (s1, s2) => s1.Length + s2.Length;
int total = totalLength("hello", "world"); // 10;
```

* ```Func<T,TResult> Delegate```

```C#
public delegate TResult Func<in T,out TResult>(T arg);
```

+++
### Explicitly Specifying Lambda Parameter Types
* Compiler can usually infer the type contextually
* When it can't, you must specify the type explicitly:
  ```C#
  void Foo<T>(T x) {}
  void Bar<T>(Action<T> a) {}
  ...
  Bar((int x) => Foo(x));
  ```

+++
### Lambda Expression Capturing Outer Variables
* *Outer variables* referenced by a lambda expression are called *captured variables*
* *Lambda expression* that captures variables is called a *closure*
* *Captured variables*
  * Are evaluated when the delegate is *invoked*
  * Have their lifetime *extended* to that of the delegate

```C#
int factor = 5;
Func<int, int> multiplier = n => n * factor;
factor = 10;
Console.WriteLine(multiplier(3)); // 30
```

+++
### Extended Lifetime Example
```C#
static Func<int> Natural()
{
 int seed = 0;
 return() => seed++; // Returns a closure
}
```

```C#
static void Main()
{
 Func<int> natural = Natural();
 Console.WriteLine(natural()); // 0
 Console.WriteLine(natural()); // 1
}
```

+++
### Extended Lifetime Example
```C#
static Func<int> Natural()
{
 return() => { int seed = 0; return seed++; };
}
```

```C#
static void Main()
{
 Func<int> natural = Natural();
 Console.WriteLine(natural()); // 0
 Console.WriteLine(natural()); // 0
}
```

+++
### Lambda Expressions vs. Local Methods
* Local methods functionality overlaps with lambda expressions
* Local methods advantages:
  * Recursive without ugly hacks
  * Avoid the clutter of specifying a delegate type
  * Incurs slightly less overhead
* In many cases you *need* a delegate
  * i.e., a method with a delegate-typed parameter:

```C#
public void Foo(Func<int,bool> predicate) { ... }
```

---
## Tuples
* Simple way to store a set of values
* Safely return multiple values from a method without resorting to out parameters

```C#
static(string,int) GetPerson() =>("Bob", 23);

static void Main()
{
  (string,int) person = GetPerson(); // Could use 'var' here if we want
  Console.WriteLine(person.Item1);  // Bob
  Console.WriteLine(person.Item2);  // 23
}
```

+++
### Named Tuples Example
```C#
static(string Name, int Age) GetPerson() =>("Bob", 23);

static void Main()
{
  var person = GetPerson();
  Console.WriteLine(person.Name); // Bob
  Console.WriteLine(person.Age);  // 23
}
```

---
## References
[C# 8.0 in a Nutshell: The Definitive Reference](https://www.amazon.com/C-8-0-Nutshell-Definitive-Reference/dp/1492051136)
[Types and Programming Languages](https://www.amazon.com/Types-Programming-Languages-MIT-Press/dp/0262162091)
[Object-Oriented Analysis and Design with Applications](https://www.amazon.com/Object-Oriented-Analysis-Design-Applications-3rd/dp/020189551X)
[Database Systems: A Practical Approach to Design, Implementation and Management](https://www.amazon.com/Database-Systems-Practical-Implementation-Management/dp/0321210255)
[C# in Depth](https://www.amazon.com/C-Depth-3rd-Jon-Skeet/dp/161729134X)
[Microsoft documentation](https://docs.microsoft.com)

+++

## Credits
* Michal Orlíček - for slides preparation

