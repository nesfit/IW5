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

+++?code=/Lectures/Lecture_02/Models/Car.cs&lang=C#&title=Car
@[3-10]
@[5-6]
@[8]
@[9]
@[1-11]

[Code sample](/Lectures/Lecture_02/Models/Car.cs)

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
  * A language mechanism for **restricting direct access** to some of the **object's components**. [1][2]
  * A language construct that **facilitates the bundling of data with the methods** (or other functions) operating on that data. [3][4] 
OOP umožnuje sdružovat logicky související data a kód. 
zapouzdření (encapsulation)
dědičnost (inheritance)
polymorfismus (polymorphism).

Zapouzdření 
skrytí implementačních detailů
zvýšení modularity 
izolace nesouvisejících částí kódu

Dědičnost - pracuje s hierarchií pro zapouzdření, tedy nové objekty lze vytvářet jako potomky svých předků od nichž přebírají (dědí) vlastnosti a přidají vlastnosti nové


---

## References

[C# 7.0 in a Nutshell: The Definitive Reference](https://www.amazon.com/C-7-0-Nutshell-Definitive-Reference/dp/1491987650)

[C# Keywords](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/) 

[Data Types](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/types-and-variables)

[1]: Mitchell, John C. (2003). Concepts in programming languages. Cambridge University Press. p. 522. ISBN 0-521-78098-5.
[2]: Pierce, Benjamin (2002). Types and Programming Languages. MIT Press. p. 266. ISBN 0-262-16209-1.
[3]: Rogers, Wm. Paul (18 May 2001). "Encapsulation is not information hiding". JavaWorld.
[4]: Connolly, Thomas M.; Begg, Carolyn E. (2005). "Ch. 25: Introduction to Object DMBS § Object-oriented concepts". Database systems: a practical approach to design, implementation, and management (4th ed.). Pearson Education. p. 814. ISBN 0-321-21025-5.

And ... a lot of Google index images...
