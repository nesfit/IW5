# IW5 Programming in .NET and C#, Organizational Details

---
# Aktuality k předmětu

--- 
# Přednášky a demonstrační cvičení

Organizace kurzu

|                      |    Místnost |            Čas |
| -                    | -           | -              |
| Přednáška            | L314        | Po 14:00-15:50 |
| Demonstrační cvičení | L314        | Po 16:00-17:50 |

+++

| Datum   | Téma                                                                                                   |
| -       |--------------------------------------------------------------------------------------------------------|
| 15. 09. | 1. [Úvod, představení předmětu, projektu, setup prostředí](./Lectures/Lecture_01/) (Roman Jašek)       |
|         | **(cvičení)** Minimal API, routing (Roman Jašek)                                                       |
| 22. 09. | 2. [Inversion of Control, konfigurace, lokalizace](./Lectures/Lecture_02/) (Michal Mrnuštík)           |
|         | **(cvičení)** ASP&#46;NET Core Web API, controller, swagger, Postman, middleware (Michal Mrnuštík)     |
| 29. 09. | 3.[Web - Blazor](./Lectures/Lecture_03/) (Roman Jašek)                                                 |
|         | **(cvičení)** Web - Blazor (Roman Jašek)                                                               |
| 06. 10. | 4. [Web - Blazor](./Lectures/Lecture_04/) (Roman Jašek)                                                |
|         | **(cvičení)** Testování, CI/CD, nasazování do Azure (Michal Tichý)                                     |
| 13. 10. | 5. [Web - napojení na API](./Lectures/Lecture_05/) (Roman Jašek)                                       |
|         | **(cvičení)** Takhle napište projekt?! (Roman Jašek)                                                   |
| 20. 10. | 6. [Identity Management - část 1](./Lectures/Lecture_06/) (Roman Jašek)                                |
| 27. 10. | 7. [Identity Management - část 2](./Lectures/Lecture_07/) (Roman Jašek)                                |
| 03. 11. | 8. [Web - frontend](./Lectures/Lecture_08/) (Maroš Janota)                                             |
|         | **(cvičení)** Web - frontend (Maroš Janota)                                                            |
| 10. 11. | 9. [Identity Management overflow/Serverless](./Lectures/Lecture_09/) (Roman Jašek)                                               |
| 17. 11. | **STÁTNÍ SVÁTEK**                                                                                      |
| 24. 11. | 10. [Clean Architecture](./Lectures/Lecture_10/) (Martin Dybal)                                        |
| 01. 12. | 11. Clean Code (Michal Tichý)                                                                          |
| 08. 12. | 12. .NET Aspire (Jan Pluskal)                                                                          |

--- 

# Projekt
* Projekt bude vypracovaný v 3-členném týmu. 

| Fáze |               Deadline |                                   Obsah |
| ---- |------------------------| --------------------------------------- |
| 1    |                 viz IS | API                                     |
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
* Roman Jašek: [e-mail](mailto:roman.jasek@riganti.cz)
* Michal Mrnuštík: [e-mail](mailto:michal.mrnustik@outlook.com)
* Michal Tichý: [e-mail](mailto:edu@tichymichal.net)
* Martin Dybal: [www](https://www.dybal.it/)
* Maroš Janota
* Tibor Jašek
* Jan Pluskal
* Silvia Sojčáková

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
