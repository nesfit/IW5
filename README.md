# Programování v .NET a C# IW5/MW5 
# Odkazy
 * [Fórum IW5](http://fitiw5-forum.northeurope.cloudapp.azure.com/index.php) Studentské fórum k předmětu IW5. Všechny Vaše dotazy směřujte jen zde! 
 * [Wiki predmětu z minulých let](http://www.fit.vutbr.cz/study/courses/IW1/public/info/doku.php?id=iw5) Dříve používaná Wiki k předmětu nahrazená GitHubem.
 * [FEKT](http://goo.gl/cBXSLd) Registrace týmu na projekt.
  

# Aktuality k předmětu
  - **13.02.2016** | *Jan Pluskal* | Registrace týmů na projekty pro [FEKT](http://goo.gl/cBXSLd).
  - **12.01.2016** | *Jan Pluskal* | Cvičení začnou v týdnu od 6.2.2017 (první týden semestru) značení týdne sudý/lichý je podle kalendáře (ISO 8601) - týden od 6.2.2016 je SUDÝ.

# Přednášky

| Fakulta | Místnost | Čas         |
| ------- |----------| ----------- |
| Fekt    | T010     | 13:00-14:50 |
| Fit     | D0206    | 16:00-17:50 |

# Cvičení 
Všechny cvičení se konají v APS, Učebna 2. Cvičení začnou v *týdnu od 6.2.2017* (první týden semestru) značení týdne sudý/lichý je podle kalendáře (ISO 8601) - týden od 6.2.2016 je SUDÝ.

| Fakulta  | Čas                |
| -------- | ------------------ |
| FIT      | St 12:00-13:50 S/L |
| FIT      | St 14:00-15:50 S/L |
| Fekt     | St 16:00-17:50 S/L |
| Fekt     | St 18:00-19:50 S/L |
| Fit      | Čt 9:00-10:50  S/L |
| Fit      | Čt 11:00-12:50 S/L |
| Fit      | Čt 13:00-14:50 S/L |

# Videa ze cvičení
 * [Implementace Calculator](https://youtu.be/vwnAV5BUaug) a pouziti klavesovych zkratek Resharperu.

# Nástroje použity ve cvičeních

| Nástroj  |  Typ   | Popis |
| -------- |  ------| -------|
| [Visual Studio 2015 FIT](https://e5.onthehub.com/WebStore/OfferingDetails.aspx?o=0e34bbfd-e242-e611-941e-b8ca3a5db7a1&pmv=00000000-0000-0000-0000-000000000000&ws=95f320d0-826f-e011-971f-0030487d8897&vsro=8) <br /> [Visual Studio 2015 FEKT](https://e5.onthehub.com/WebStore/OfferingDetails.aspx?o=0e34bbfd-e242-e611-941e-b8ca3a5db7a1&pmv=00000000-0000-0000-0000-000000000000&ws=7817c804-8b6f-e011-971f-0030487d8897&vsro=8)| Samostatný program | Hlavní vývojové prostředí pro .Net |
|[Resharper](https://www.jetbrains.com/resharper/) | Doplněk | Nástroje na lepší produktivitu, refaktorování. Studentská licence je k dispozici zdarma [zde](https://www.jetbrains.com/student/) |
|[Code metrices](https://visualstudiogallery.msdn.microsoft.com/369d38e1-53d3-4f5c-9351-a0560162a6d9) | Doplněk | Zobrazování složitosti jednotlivých metod |
|[Postifx templates](https://github.com/controlflow/resharper-postfix) | Doplněk | Plynulé doplňování částí kódu bez nutnosti vracení se |
|[Mnemonic Live Templates](https://github.com/JetBrains/mnemonics) | Doplněk | Doplňování částí kódu |
|[LinqPad](http://www.linqpad.net/) | Samostatný program  | Nástroj na přístup do databáze přes Linq, SQL… |
|[DotPeek](https://www.jetbrains.com/decompiler/) | Samostatný program  | Dekompilátor C# kódu |

# Učebny
**POZOR**, výuka probíhá v areálu vysokoškolských kolejí *Purkyňova 93* [Mapa](http://www.pocitacoveskoleni.cz/kontakt.html) v prvním patře bloku *B3*  v prostorách Školicího střediska IT Brno (ApS Brno s.r.o.). Pro přístup k učebnám zazvoňte na odpovídající zvonek vedle prosklených dveří přímo naproti schodišti. Učebny jsou na konci chodby po levé straně.

Označení učeben oproti značení FIT:
* B0302006 = Učebna 1
* B0302007 = Učebna 2 

# Q&A
* Q: Mohu přijít na cvičení i jindy, než mám zapsáno?
* A: Ano, přijít můžete na kterékoli cvičení v daném 14 denním bloku. Využívejte tuto možnost jen ve zvláštních případech. Do prezence se zapište na druhou stránku a uveďte do které skupiny patříte. Pokud bude na cvičení víc studentů než je počítačů, pak u počítačů mají přednost ti, kteří mají danou hodinu zapsanou. Vzhledem k tomu, že se výuka opakuje v sudém/lichém týdnu, tak pokud budete např. nemocní, můžete si cvičení nahradit v následujícím týdnu.
* Q: Slyšel jsem, že lze uznat místo projektu i bakalářskou práci nebo projekt do jiného předmětu napsaný v C#, je to pravda?
* A: Ano, ale projekt musí splňovat obecná kriteria (správný datový návrh, včetně dědičnosti a modifikátorů přistupu, SOLID a CleanCode) a musí být v C#. Je bezpodmínečně nutné upozornit na to cvičícího v prvním běhu cvičení.
* Q: Connection string pouzivany v cvicenich.
* ```<connectionStrings> 
      <add name="TasksContext" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=TasksDB;MultipleActiveResultSets=True;Integrated Security=True;" providerName="System.Data.SqlClient" />
      </connectionStrings>```
      
# Výuka - bodové rozdělení
|      Typ výuky     | Maximální bodový zisk |
| ------------------ | --------------------- |
| Počítačová cvičení |                    30 |
| Projekt            |                    70 |

# Užitečné odkazy
* [Entity framework tutorial](http://www.entityframeworktutorial.net/code-first/entity-framework-code-first.aspx)
* [R. C. Martin SOLID](https://youtu.be/TMuno5RZNeE?t=757) Bob Martin SOLID Principles of Object Oriented and Agile Design 

