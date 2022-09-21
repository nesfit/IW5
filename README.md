# IW5 Programming in .NET and C#, Organizational Details

---
# Aktuality k předmětu
 2022-09-21 - Update k pojmenování týmů v Azure DevOps a pojmenování resources v Azure podle nového pojmenování týmů ve VUT IS.
 
--- 
# Přednášky a demonstrační cvičení

Organizace kurzu

|                      |    Místnost |            Čas |
| -                    | -           | -              |
| Přednáška            | D0206       | Ut 10:00-11:50 |
| Demonstrační cvičení | A112        | Ut 13:00-14:50 |

+++

| Datum   | Téma |
| -       | -    |
| 20. 09. | Úvod, představení předmětu, projektu, setup prostředí (Roman Jašek) |
|         | (cvičení) Minimal API v .NET 6, routing (Roman Jašek) |
| 27. 09. | Inversion of Control, konfigurace, lokalizace (Michal Mrnuštík) |
|         | **(cvičení)** ASP&#46;NET Core Web API, controller, swagger, Postman, middleware (Michal Mrnuštík) |
| 04. 10. | Web - Blazor (Roman Jašek)  |
|         | **(cvičení)** Web - Blazor (Roman Jašek) |
| 11. 10. | Web - Blazor (Roman Jašek) |
|         | **(cvičení)** Testování, CI/CD, nasazování do Azure (Michal Tichý) |
| 18. 10. | Web - frontend (Maroš Janota) |
|         | **(cvičení)** Web (Maroš Janota/Roman Jašek) |
| 25. 10. | Web - napojení na API (Michal Tichý) |
|         | **(cvičení)** Takhle napište projekt?! (Roman Jašek) |
| 01. 11. | Progressive Web Apps, Blazor MAUI (Roman Jašek) |
| 08. 11. | .NET MAUI (Roman Jašek) |
| 15. 11. | Clean Architecture (Martin Dybal) |
| 22. 12. | Architektura a struktura projektu pro enterprise aplikace (Roman Jašek) |
| 29. 11. | Vue.js (Patrik Švikruha) |
| 06. 12. | Unity (Vojtěch Brůža) |
| 13. 12. | Přednáška dle hlasování studentů |

--- 

# Projekt
* Projekt bude vypracovaný v 3-členném týmu. 

| Fáze |               Deadline |                                   Obsah |
| ---- |------------------------| --------------------------------------- |
| 1    |                viz WIS | API                                     |
| 2    |    Den před odevzdáním | Finalizace aplikace a následná obhajoba |

* Při obhajobě:
  * musí být přítomni všichni členové týmu,
  * obhajovat projekt bude náhodně vybraný člen týmu,
  * nemusíte chodit v obleku...,
  * projekt musí bezpodmínečně obsahovat **Must have features!**

--- 
# Nástroje použity v přednáškách a cvičeních

| Nástroj  |  Typ   | Popis |
| -------- |  ------| -------|
|[Visual Studio 2022](https://aka.ms/devtoolsforteaching)| Samostatný program | Hlavní vývojové prostředí pro .Net |
|[Visual Studio 2022 Preview](https://visualstudio.microsoft.com/vs/preview/)| Samostatný program | Preview verze následující verze Visual Studia |
|[Resharper](https://www.jetbrains.com/resharper/) | Doplněk | Nástroje na lepší produktivitu, refaktorování. Studentská licence je k dispozici zdarma [zde](https://www.jetbrains.com/student/) |
|[Postifx templates](https://github.com/controlflow/resharper-postfix) | Doplněk | Plynulé doplňování částí kódu bez nutnosti vracení se |
|[ResXManager](https://github.com/dotnet/ResXResourceManager) | Doplněk | Práce s lokalizačními soubory |


+++

| Nástroj  |  Typ   | Popis |
| -------- |  ------| -------|
|[Mnemonic Live Templates](https://github.com/JetBrains/mnemonics) | Doplněk | Doplňování částí kódu |
|[LinqPad](http://www.linqpad.net/) | Samostatný program  | Nástroj na přístup do databáze přes Linq, SQL… |
|[Postman](https://www.postman.com/) | Samostatný program  | Nástroj na provolávání Web API |
|[DotPeek](https://www.jetbrains.com/decompiler/) | Samostatný program  | Dekompilátor C# kódu |
|[MarkdownEditor](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor)| Doplněk| Handy Markdown editor for VS |

--- 
# Q&A

* Q: Slyšel jsem, že lze uznat místo projektu i bakalářskou práci nebo projekt do jiného předmětu napsaný v C#, je to pravda?
* A: Ano, ale projekt musí splňovat obecná kriteria (správný datový návrh, včetně dědičnosti a modifikátorů přístupu, SOLID a CleanCode) a musí být v C#. O tuto možnost žádejte indiviuálně po skončení přednášky.

---
# Výuka - bodové rozdělení

|          Typ výuky |     Maximální bodový zisk |
| ------------------ | ------------------------- |
| Projekt            |                       100 |

--- 
# Lidé podílející se na kurzu
* Roman Jašek: [e-mail](mailto:roman.jasek@hotmail.com)
* Michal Mrnuštík: [e-mail](mailto:michal.mrnustik@outlook.com)
* Michal Tichý: [e-mail](mailto:edu@tichymichal.net)
* Martin Dybal: [www](https://www.dybal.it/)
* Maroš Janota
* Vojtěch Brůža
* Jiří Pokorný
* Patrik Švikruha
* Tibor Jašek
* Silvia Sojčáková
* Jan Pluskal

--- 
# Užitečné odkazy
* [R. C. Martin SOLID](https://youtu.be/TMuno5RZNeE?t=757) Bob Martin SOLID Principles of Object Oriented and Agile Design 
* [Resharper features](https://www.jetbrains.com/resharper/features/) and how to use them.

--- 
# Užitečná literatura
* [C# 9.0 in a Nutshell](http://www.albahari.com/nutshell/), Ben Albahari, Joseph Albahari
* [Clean Code: A Handbook of Agile Software Craftsmanship](https://books.google.cz/books?id=hjEFCAAAQBAJ), Robert C. Martin
* [Agile Principles, Patterns, and Practices in C#](https://books.google.cz/books?id=hckt7v6g09oC), Robert C. Martin
* [.NET Blog](https://devblogs.microsoft.com/dotnet/)
