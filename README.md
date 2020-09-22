# IW5 Programming in .NET and C#, Organizational Details

---
# Aktuality k předmětu 
<!---  - **18.04.2019** | *Jan Pluskal* | [Registrace](http://goo.gl/mj3ODO) obhajoby projektů - autentizace xlogin00@vutbr.cz. Odevzdání projektu proveďte do WISu jako zip archív bez obj, bin, packages. V případě FEKTu není třeba odevzdávat. --->
  - **02.02.2020** | *Jan Pluskal* | [Visual Studio 2019 Enterprise](https://aka.ms/devtoolsforteaching) je nově dostupné v Azure Dev Tools for Teaching. Přihlášení je nutné s loginem z domény VUT, tj xlogin00@vutbr.cz
  - **02.02.2020** | *Jan Pluskal* | Při vytváření repozitáře respektujte schéma ze [zadání](/Project/README.md) *https://dev.azure.com/iw5-2020-fall-team0000/project*. Je nezbytně nutné použít Vaše účty z doménu *vutbr.cz*. Do Vašich repozitářů pro projekt přidejte účet **uciteliw5@vutbr.cz**. Pokud uděláte chybu a pouze nesedí url, dá se v nastavení změnit.
    * Pokud máte vytvořeno pod soukromými účty, je třeba vytvořit projekt znovu pod univerzitními a pushnout existující repozitář tak, aby Vám zůstala historie včetně správných časů commitů. 
    * Pokud bude kolize s existující organizací, použijte suffix *team0000-01*.
 
--- 
# Přednášky

| Fakulta |    Místnost |            Čas |
| ------- |-------------| -------------- |
| FIT     | G202       | Út 08:00-09:50 |


| Datum  |                                                                                            Téma přednášky | 
| ------ | ------------------------------------------------------------------------------------------------------- |
| 22. 9. | Úvod do jazyka C# a platformy .NET & úvod do objektově orientovaného programování |
| 29. 9. | ASP.NET Core Web API |
| 6. 10. | Inversion of Control, konfigurace, middleware, logging |
| 13. 10. | Jak psát testy |
| 20. 10. | Web - část 1 |
| 27. 10. | Web - část 2 |
| 3. 11. | Web - část 3 |
| 10. 11. | .NET Internals, Intermediate Language, Code Generation |
| 17. 11. | **Státní svátek** |
| 24. 11. | Softwarové architektury v C# |
| 31. 11. | Design Patterns v C# |
| 8. 12. | Angular frontend for .NET backend |
| 15. 12. | ??? |

--- 
# Cvičení 
Cvičení jsou demonstrační. Pokud možno, přineste si vlastní zařízení na kterém budete moct cvičení aktivně zúčastnit. Bude potřeba vývojové prostředí ideálně Visual Studio 2019 / Rider / VSCode.

| Typ                                  | Místnost | Čas            |
| ------------------------------------ |----------| -------------- |
| Demonstrační cvičení                 | G202    | Út 10:00-11:50 |

| Datum  |                                                                                            Téma cvičení | 
| ------ | ------------------------------------------------------------------------------------------------------- | 
| 22. 9. | C# tooling, VS - zalozeni projektu ASP.NET Core, zalozeni VSTS | 
| 29. 9. | Controller, endpoint, postman, curl, swagger | 
| 13. 10. | VS Test Explorer, Resharper, Code coverage, Live Tests, CI v VSTS (Demonstracne) a důraz na psaní testů |
| 20. 10. | Web - část 1 |
| 27. 10. | Web - část 2 |
| 3. 11. | Takhle napiste projekt? |

---
# Projekt
* Projekt bude vypracovaný v 3-členném týmu. 

| Fáze |               Deadline |                                   Obsah |
| ---- |------------------------| --------------------------------------- |
| 1    |                  1. 11. | API                                     |
| 2    |    Den před odevzdáním | Finalizace aplikace a následná obhajoba |

* Při obhajobě:
  * musí být přítomni všichni členové týmu,
  * obhajovat projekt bude náhodně vybraný člen týmu,
  * nemusíte chodit v obleku...,
  * projekt musí bezpodmínečně obsahovat **Must have features!**

--- 
# Nástroje použity ve cvičeních

| Nástroj  |  Typ   | Popis |
| -------- |  ------| -------|
|[Visual Studio 2019 Enterprise](https://aka.ms/devtoolsforteaching)| Samostatný program | Hlavní vývojové prostředí pro .Net |
|[Resharper](https://www.jetbrains.com/resharper/) | Doplněk | Nástroje na lepší produktivitu, refaktorování. Studentská licence je k dispozici zdarma [zde](https://www.jetbrains.com/student/) |
|[Code metrices](https://visualstudiogallery.msdn.microsoft.com/369d38e1-53d3-4f5c-9351-a0560162a6d9) | Doplněk | Zobrazování složitosti jednotlivých metod |
|[Postifx templates](https://github.com/controlflow/resharper-postfix) | Doplněk | Plynulé doplňování částí kódu bez nutnosti vracení se |

+++

| Nástroj  |  Typ   | Popis |
| -------- |  ------| -------|
|[Mnemonic Live Templates](https://github.com/JetBrains/mnemonics) | Doplněk | Doplňování částí kódu |
|[LinqPad](http://www.linqpad.net/) | Samostatný program  | Nástroj na přístup do databáze přes Linq, SQL… |
|[DotPeek](https://www.jetbrains.com/decompiler/) | Samostatný program  | Dekompilátor C# kódu |
|[MarkdownEditor](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor)| Doplněk| Handy Markdown editor for VS |
|[Entity Framework 6 Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EntityFramework6PowerToolsCommunityEdition)| Doplněk| View Entity Data Model|
|[OzCode](https://www.oz-code.com/)| Doplněk| Advanced debugging tools |
|[GitFlow](https://marketplace.visualstudio.com/items?itemName=vs-publisher-57624.GitFlowforVisualStudio2017)| Doplněk| GitFlow|

--- 
# Q&A

* Q: Slyšel jsem, že lze uznat místo projektu i bakalářskou práci nebo projekt do jiného předmětu napsaný v C#, je to pravda?
* A: Ano, ale projekt musí splňovat obecná kriteria (správný datový návrh, včetně dědičnosti a modifikátorů přístupu, SOLID a CleanCode) a musí být v C#. Je bezpodmínečně nutné upozornit na to cvičícího v prvním běhu cvičení.

---
# Výuka - bodové rozdělení

|          Typ výuky |     Maximální bodový zisk |
| ------------------ | ------------------------- |
| Počítačová cvičení |                        30 |
| Projekt            |                        70 |

--- 
# Vyučující
* [Martin Dybal](https://www.dybal.it/)
* [Roman Jašek]()
* [Tibor Jašek]()
* [Adam Jež]()
* [Michal Mrnušťík]()
* [Jan Pluskal](http://www.fit.vutbr.cz/~ipluskal/)
* [Jiří Pokorný]()
* [Patrik Švikruha]()

--- 
# Užitečné odkazy
* [WPF-Tutorial](https://wpf-tutorial.com/)
* [Entity framework tutorial](http://www.entityframeworktutorial.net/code-first/entity-framework-code-first.aspx)
* [R. C. Martin SOLID](https://youtu.be/TMuno5RZNeE?t=757) Bob Martin SOLID Principles of Object Oriented and Agile Design 
* [Resharper features](https://www.jetbrains.com/resharper/features/) and how to use them.
* [Pro Git book](https://git-scm.com/book/en/v2)

--- 
# Užitečná literatura
* [C# 7.0 in a Nutshell](http://www.albahari.com/nutshell/about.aspx), Ben Albahari, Joseph Albahari
* [Clean Code: A Handbook of Agile Software Craftsmanship](https://books.google.cz/books?id=hjEFCAAAQBAJ), Robert C. Martin
* [Agile Principles, Patterns, and Practices in C#](https://books.google.cz/books?id=hckt7v6g09oC), Robert C. Martin
* [C# 3.0 Design Patterns](https://books.google.cz/books?id=pD2XMZLGUAYC), Judith Bishop
