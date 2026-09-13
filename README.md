# Programování v .NET a C# - IW5, Organizační pokyny

---

## [FAQ - Frequently Asked Questions](https://github.com/nesfit/IW5/wiki)

---

# Rozvrh

## Přednášky

| Typ       | Místnost  | Čas            |
| --------- | --------- | -------------- |
| Přednáška | **D0207** | Po 16:00-17:50 |

+++

## Cvičení
Cvičení jsou demonstrační. Pokud možno, přineste si vlastní zařízení, na kterém se budete moct cvičení aktivně zúčastnit. Bude potřeba vývojové prostředí, ideálně Visual Studio 2026 / Rider / VSCode.

| Typ                             | Místnost  | Čas            |
| ------------------------------- | --------- | -------------- |
| Demonstrační cvičení | **D0207** | Po 18:00-19:50 |

+++

## Plán semestru

| Datum  | Typ | Vyučující       | Téma                                                                                    |
| ------ | --- | --------------- | --------------------------------------------------------------------------------------- |
| 14.09. | L01 | Roman Jašek     | [Úvod, představení předmětu, projektu, setup prostředí](./Lectures/Lecture_01/)         |
|        | E01 | Roman Jašek     | Minimal API, routing                                                                    |
| 21.09. | L02 | Michal Mrnuštík | [Inversion of Control, konfigurace, lokalizace](./Lectures/Lecture_02/)                 |
|        | E02 | Michal Mrnuštík | ASP&#46;NET Core Web API, controller, swagger, Postman, middleware                      |
| 28.09. |     |                 | **Státní svátek**                                                                       |
| 05.10. | L03 | Roman Jašek     | [Web - Blazor](./Lectures/Lecture_03/)                                                  |
|        | E03 | Roman Jašek     | Web - Blazor                                                                            |
| 12.10. | L04 | Roman Jašek     | [Web - Blazor](./Lectures/Lecture_04/)                                                  |
|        | E04 | Michal Tichý    | Testování, CI/CD, nasazování do Azure                                                   |
| 19.10. | L05 | Roman Jašek     | [Web - napojení na API](./Lectures/Lecture_05/)                                         |
|        | E05 | Roman Jašek     | Takhle napište projekt?!                                                                |
| Dle IS | P01 |                 | **Odevzdání první fáze projektu - API**                                                 |
| 26.10. | L06 | Roman Jašek     | [Identity Management - část 1](./Lectures/Lecture_06/)                                  |
| 02.11. | L07 | Roman Jašek     | [Identity Management - část 2](./Lectures/Lecture_07/)                                  |
| 09.11. | L08 | Maroš Janota    | [Web - frontend](./Lectures/Lecture_08/)                                                |
|        | E06 | Maroš Janota    | Web - frontend                                                                          |
| 16.11. | L09 | Roman Jašek     | [Identity Management overflow/Serverless](./Lectures/Lecture_09/)                       |
| 23.11. | L10 | Martin Dybal    | [Clean Architecture](./Lectures/Lecture_10/)                                            |
| 30.11. | L11 | Michal Tichý    | Clean Code                                                                              |
| 07.12. | L12 | Jan Pluskal     | .NET Aspire                                                                             |
| Dle IS | P02 |                 | **Obhajoby projektu**                                                                   |

LXY - přednáška | EXY - democvičení | P0X - projekt

---

# Výuka - bodové rozdělení

| Typ výuky | Maximální bodový zisk |
| --------- | --------------------- |
| Projekt   | 100                   |

---

# Projekt
* Zadání projektu: [Project/README.md](./Project/README.md)
* Projekt bude vypracovaný v 3-členném týmu.

| Fáze | Deadline                    | Obsah                                         | Body |
| ---- | --------------------------- | --------------------------------------------- | ---- |
| 1    | viz IS                      | API                                           | 50   |
| 2    | viz IS (den před obhajobou) | Web - finalizace aplikace a následná obhajoba | 50   |

* Při obhajobě:
  * musí být přítomni všichni členové týmu (výjimka je řádně omluvená nepřítomnost dle studijního řádu),
  * v případě nutnosti můžou být někteří členové připojeni online, za online připojení, komunikaci a případné technické potíže nese odpovědnost tým samotný (minimálně jeden člen týmu musí být fyzicky přítomný a řešit připojení),
  * obhajovat projekt bude náhodně vybraný člen týmu,
  * nemusíte chodit v obleku...,
  * očekává se prezentace Vaší aplikace, tj. její funkčnost dle zadání, a následuje technická rozprava nad zdrojovým kódem a otázky,
  * projekt musí bezpodmínečně obsahovat **Must have features!**


---

# Nástroje použity v přednáškách a cvičeních

| Nástroj                                                                                            | Typ                | Popis                                                                                                                             |
| -------------------------------------------------------------------------------------------------- | ------------------ | --------------------------------------------------------------------------------------------------------------------------------- |
| [Visual Studio Enterprise 2026](https://aka.ms/devtoolsforteaching)                                | IDE                | Vývojové prostředí pro .Net                                                                                                       |
| [Resharper](https://www.jetbrains.com/resharper/)                                                  | Doplněk VS         | Nástroje na lepší produktivitu, refaktorování. Studentská licence je k dispozici zdarma [zde](https://www.jetbrains.com/student/) |
| [Jetbrains Rider](https://www.jetbrains.com/rider/)                                                | IDE                | Vývojové prostředí pro .Net                                                                                                       |
| [Postman](https://www.postman.com/)                                                                | Samostatný program | Nástroj na provolávání Web API                                                                                                    |
| [LinqPad](http://www.linqpad.net/)                                                                 | Samostatný program | Nástroj na přístup do databáze přes Linq, SQL…                                                                                    |
| [DotPeek](https://www.jetbrains.com/decompiler/)                                                   | Samostatný program | Dekompilátor C# kódu                                                                                                              |
| [ResXManager](https://github.com/dotnet/ResXResourceManager)                                       | Doplněk VS         | Práce s lokalizačními soubory                                                                                                     |
| [EF Core Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools) | Doplněk VS         | Přidává funkcionalitu k DbContext jako je např. generování ER diagramů.                                                           |

+++

```pwsh
winget install Microsoft.DotNet.SDK.10

winget install Microsoft.VisualStudio.Enterprise --override "--add Microsoft.VisualStudio.Workload.NetCrossPlat --add Microsoft.VisualStudio.Workload.Data --add Microsoft.VisualStudio.Workload.ManagedDesktop --add Microsoft.VisualStudio.Workload.NetWeb --add Microsoft.VisualStudio.Workload.Azure"

winget install JetBrains.Toolbox
winget install Postman.Postman
```

---

# Další zajímavé nástroje

| Nástroj                                                           | Typ               | Popis                                                     |
| ----------------------------------------------------------------- | ----------------- | --------------------------------------------------------- |
| [Roslynator](https://github.com/JosefPihrt/Roslynator)            | Analyzér, Doplněk | Open-source alternativa k Resharper postavená nad Roslyn. |
| [Mnemonic Live Templates](https://github.com/JetBrains/mnemonics) | Doplněk           | Doplňování částí kódu                                     |

---

# Vyučující
* [Martin Dybal](https://www.linkedin.com/in/martin-dybal) - [www](https://www.dybal.it/)
* [Maroš Janota](https://www.linkedin.com/in/marosjanota/)
* [Roman Jašek](https://www.linkedin.com/in/roman-jasek-16921839) - [e-mail](mailto:roman.jasek@riganti.cz)
* [Tibor Jašek](https://www.linkedin.com/in/tibor-jašek-717a5761)
* [Michal Mrnuštík](https://www.linkedin.com/in/michal-mrnušt%C3%ADk-31050b60/) - [e-mail](mailto:michal.mrnustik@outlook.com)
* [Jan Pluskal](https://www.linkedin.com/in/jan-pluskal-4a60b761/) - [FIT](https://www.fit.vut.cz/person/pluskal) - [e-mail](mailto:pluskal@vut.cz)
* [Miroslav Šafář](https://www.linkedin.com/in/miroslav-safar/) - [FIT](https://www.fit.vut.cz/person/isafar/.cs) - [e-mail](mailto:isafar@fit.vut.cz)
* [Michal Tichý](https://www.linkedin.com/in/tichy-michal/) - [e-mail](mailto:edu@tichymichal.net)

---

# Užitečné odkazy
* [ASP.NET Core documentation](https://learn.microsoft.com/aspnet/core/)
* [Blazor documentation](https://learn.microsoft.com/aspnet/core/blazor/)
* [Entity framework tutorial](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)
* [R. C. Martin SOLID](https://youtu.be/TMuno5RZNeE?t=757) Bob Martin SOLID Principles of Object Oriented and Agile Design
* [Resharper features](https://www.jetbrains.com/resharper/features/) and how to use them.
* [Pro Git book](https://git-scm.com/book/en/v2)

---

# Užitečná literatura
* [C# 12 in a Nutshell](https://www.albahari.com/nutshell/), Joseph Albahari
* [Clean Code: A Handbook of Agile Software Craftsmanship](https://books.google.cz/books?id=hjEFCAAAQBAJ), Robert C. Martin
* [Agile Principles, Patterns, and Practices in C#](https://books.google.cz/books?id=hckt7v6g09oC), Robert C. Martin
* [Clean Architecture: A Craftsman's Guide to Software Structure and Design](https://books.google.cz/books?id=uGE1DwAAQBAJ), Robert C. Martin
* [The Art of Unit Testing](https://books.google.cz/books?id=2GRRmgEACAAJ), Roy Osherove

---

# Užitečné zdroje

aneb co sledovat, pokud se chcete dozvědět víc

* [ICS](https://github.com/nesfit/ICS) - předmět, který je prerekvizitou IW5 a pokrývá základy C#, .NET, Entity Framework a MVVM

+++

## Co se děje v .NET a co se chystá
* [.NET Conf](https://www.dotnetconf.net/) - konference, kde se představuje nová verze .NET (jednou ročně - **listopad**)
* [Microsoft Build](https://build.microsoft.com/en-US/home) - největší konference pro vývojáře od Microsoftu, hromada novinek (jednou ročně - **květen**)
* [.NET Announcements](https://github.com/dotnet/announcements) - repozitář, kde se dá odebírat notifikace o novinkách v .NET (a ostatních .NET frameworcích - ASP.NET Core, EF, MAUI...)
* [.NET Blog](https://devblogs.microsoft.com/dotnet/) - oficiální blog .NET týmu
* [Themes of .NET](https://aka.ms/dotnet-product-roadmap) - .NET roadmap - podrobný přehled toho, na čem se pracuje a kdy se to plánuje

+++

## Blogy
* [Scott Hanselman](https://www.hanselman.com/blog/) - Různorodá témata, většinou zaměřená na Microsoft technologie. Autor je zkušený speaker z řad Microsoftu.
* [Steve Gordon](https://www.stevejgordon.co.uk/) - .NET, web development, cloud, low-level stuff...
* [Andrew Lock](https://andrewlock.net/) - ASP.NET Core do hloubky, middleware, konfigurace, DI
* [Jiří Činčura](https://www.tabsoverspaces.com/) - performance, Entity Framework, databáze, novinky v .NET...
* [Robert Haken](https://knowledge-base.havit.cz/) - Blazor, webový vývoj, performance

+++

## Twitter / X
* @davidfowl - **David Fowler**, jeden z hlavních lidí ve vývoji .NET a ASP .NET
* @DamianEdwards - **Damian Edwards**, jeden z hlavních lidí ve vývoji .NET a ASP .NET
* @MadsTorgersen - **Mads Torgersen**, hlavní člověk odpovědný za C#
* @JamesNK - **James Newton-King**, autor Newtonsoft.Json, pracuje na gRPC integraci v .NET, (de)serializace, performance...
* @jaredpar - **Jared Parsons**, pracuje na C# kompilátoru a návrhu jazyka
* @troyhunt - **Troy Hunt**, zaměření na bezpečnost, autor projektu https://haveibeenpwned.com
* @dotnetmeme - memes ze světa .NET (ne od Microsoftu)

+++

## Youtube
* [Nick Chapsas](https://www.youtube.com/@nickchapsas) - novinky v .NET, performance, webový vývoj
* [.NET Community Standups](https://www.youtube.com/playlist?list=PLdo4fOcmZ0oX-DBuRG4u58ZTAJgBAeQ-t) - veřejně dostupné streamy z meetingů mezi vývojáři v Microsoftu ohledně toho, co se aktuálně děje a na čem se pracuje (ASP.NET Core, Blazor, EF, Aspire...)

+++

## Podcasty
* [.NET Rocks](https://www.dotnetrocks.com/) - 2 hodně zkušení hostitelé - Richard Campbell a Carl Franklin, 1 host, různá témata (hlavně) z .NET světa
* [The ReadME Podcast](https://github.com/readme/podcast) - podcast GitHubu
