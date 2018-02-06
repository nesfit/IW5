IW5 projekt
===========

### Důležité upozornění

Pro hodnocení projektu (a úspěšné absolvování předmětu) je nutno
dokončit **všechny 3 fáze projektu** a projekt **obhájit**. Pokud
projekt nebude při obhajobě obsahovat základní funkcionalitu uvedenou v
zadání, je možné celkové hodnocení snížit. **Nespokojíme se tedy
s nedokončeným projektem**. Tuhle poznámku sem dáváme proto, že se
v předchozích ročnících vyskytly týmy, které po dosáhnutí součtu 50 bodů
za předmět po 2. fázi rozhodly nedokončit projekt a poté byly nemile
překvapeni, když se po nich vyžadovala plná funkcionalita při obhajobě.
Dejte si na to tedy prosím pozor.

Zadání
------

Výsledkem projektu je program na správu fotografií. Do programu bude
možné přidávat fotografie. Přesné provedení přidávání fotografií bude
záležet na vás, můžete zvolit ruční přidávání fotografií ze souborů
v počítači, z webových url, nebo napojit program na nějaký externí zdroj
fotografií (např. Google Drive, OneDrive, DropBox, Picasa, Flickr,
Rajce…).

K jednotlivým fotografiím bude možné přidávat informace, které budou
poté použitelné při vyhledávání fotografií, jejich řazení a filtrování.
Některé informace lze zjistit z obrázku automaticky, není nutné je
vyplňovat uživatelem. Ke každému obrázku tak bude možné zadat minimálně:

-   *Název* – automaticky se předvyplní název souboru, ale dá se změnit

-   *Datum a čas pořízení*

-   *Formát* (jpg, png, gif…)

-   *Rozlišení*

-   *Poznámka*

Další významnou částí programu budou alba. Fotografie se budou dát
přidávat do alb, aby bylo možné je lépe organizovat a orientovat se v
nich.

Při fotografiích bude dále záležet na jejich obsahu. Bude možné určovat
co ve fotografiích je. Zejména bude možné označovat **osoby** a
**předměty** ve fotografiích. K fotografii tedy můžete přidat informaci
o tom, jaké osoby a předměty se na ní nachází.

**Osoba** v rámci fotografie má minimálně tyto atributy:

-   *Jméno*

-   *Příjmení*

-   *Poloha na fotografii*

**Předmět** v rámci fotografie má minimálně tyto atributy:

-   *Název*

-   *Poloha na fotografii*

Pod „předmětem“ si můžete představit například okno, židli, dům,
sklenici…

Program bude obsahovat minimálně tyto části – *seznam fotografií,*
*detail fotografie, seznam alb, detail alba, seznam osob, seznam
předmětů*.

Seznam fotografií

Seznam bude zobrazovat fotografie.

Bude zde možnost **řadit** fotografie dle *data pořízení a názvu.*

Seznam fotografií se bude dat **filtrovat** dle *data pořízení, formátu*
*a rozlišení*.

V sezname se bude dát **vyhledávat** minimálně dle *názvu fotografie*.

Detail fotografie

Dále se dá zobrazit detail jednotlivé fotografie se všemi informacemi o
ní. Zde se také dají **editovat** informace včetně výběru *osob a
předmětů*, které se na fotografii nachází.

### Seznam alb

Každé album má svůj *název*. V seznamu teda musíte **zobrazit**
minimálně *názvy alb*. Pěknou možností rozšíření **(za možné bonusové
body)** je např. zobrazení alb v seznamu s jejich „cover photo“, počtem
fotografií, které se v albu nachází…

### Detail alba

V detailu alba jsou **vidět** fotografie, které se v daném albu nachází.
Přidávání fotografií do alb je možné implementovat v detailu alba,
v seznamu alb nebo v detailu fotografie – zde necháme konkrétní
implementaci na vás, **musí být ale možné fotografie přidávat** do alb a
demonstrovat tuto funkcionalitu.

### Seznamy osob a předmětů

V seznamu osob (a předmětů) budou zobrazeny všechny osoby (resp.
předměty), které jsou ve všech fotografiích. Bude možné v tomto seznamu
vyhledávat podle jména a přéjmení osoby (resp. názvu předmětu).
Minimálně se budou zobrazovat jména a příjmení osob (resp. názvy
předmětů). Stejně jako při seznamu alb je možné získat **bonusové body**
za implementaci lepšího zobrazení (např. „cover photo“, počet
fotografií, na kterých se daná osoba/předmět nachází…)

Po kliknutí na osobu (resp. předmět) se zobrazí všechny fotografie, na
kterých se nachází.

**Aplikace bude data ukládat do databáze. Data tedy musí zůstat
zachována i po ukončení a opětovném spuštění aplikace.**

Spolupráce
----------

Projekt řeší studenti v týmech. V každém týmu jsou **4 studenti**. *Tým
o méně studentech není přípustný.*

Při řešení projektu týmy využívají Visual Studio Team Services (VSTS) a
využívají GIT na sdílení kódu. Do svého GIT repositáře přidělí přístup
vyučujícím (způsob bude vysvětlen v rámci 1. cvičení).

Z GITu *musí být viditelná postupná práce na projektu a spolupráce
týmu*. Pokud uvidíme, že existuje malé množství nelogických a
nepřeložitelných commitů tak nás bude zajímat, jak jste spolupracovali a
může to vést na snížení bodového hodnocení.

Do Vašeho týmového projektu na GIT si v části Members přidejte účet
**uciteliw5@vutbr.cz**

Tento účet budou používat vyučující pro přístup k odevzdávaným souborům.
Bez přidání tohoto účtu není možné přistoupit k vašemu projektu a tedy
není možné jej ze strany vyučujících hodnotit.

Návod na přidání člena projektu můžete najít zde:
*https://docs.microsoft.com/en-us/vsts/accounts/add-team-members-vs*

Odevzdávání
-----------

Odevzdávání projektu má **3 fáze**. V každé fázi se hodnotí jiné
vlastnosti projektu. Nicméně fáze na sebe navzájem následují a studenti
pokračují v práci na svém kódu i po jeho odevzdání v rámci následující
fáze.

*Kontroluje se kód, který je nahrán v GIT.* Vždy se kontroluje
*poslední* *commit před časem odevzdávání* dané fáze projektu. Na
commity nahrány po času odevzdávání nebude brán zřetel.

Je silně doporučováno projekty v průběhu semestru konzultovat s
cvičícími, předejdete tak případným komplikacím při odevzdání.

### Fáze 1 – objektový návrh (10 bodů) – odevzdání 18. 3. 2018 23:59:59

V téhle fázi se zaměříme na *datový návrh*. Vyžaduje se po Vás, aby
datový návrh splňoval zadání a nevynechal žádnou podstatnou část.
Zamyslete se nad vazbami mezi jednotlivými entitami v datovém modelu.
V následující fázi budete entity nahrávat do databáze, takže myslete na
jejich propojení již v téhle fázi. V této fázi budeme chtít, abyste
**odevzdali kód**, kde budete mít *entitní třídy*, které budou obsahovat
všechny vlastnosti, které budete dále potřebovat a vazby mezi třídami.
**Nestačí tedy odevzdat diagram tříd, nebo nějakou jinou reprezentaci.**
Budeme požadovat kód v jazyce C\#.

Hodnotíme:

-   logický návrh tříd

-   využití dědičnosti, zapouzdření, polymorfismu

-   verzování v GITu po logických částech

-   -   logické rozšíření datového návrhu nad rámec zadání
    (bonusové body)

### 

### Fáze 2 – databáze a WPF backend (20 bodů) – 15. 4. 2018 23:59:59

Aplikace již nepracuje jen s daty uvedenými ve zdrojových souborech. Je
napojena na databázi a pracuje s ní. Vytvořte napojení datových tříd
pomocí Entity Frameworku na databázi.

V této fázi se od Vás již požaduje vytvoření WPF aplikace. Napište
backend aplikace (vytvoření View-Modelů), která bude napojena na Vámi
navrhnuté datové modely z 1. fáze a bude schopna načítat a ukládat data
do databáze.

**automatické testy**

Dbejte také kvality Vašeho kódu. Od této fáze se hodnotí i tenhle
atribut. Opravte si tedy předchozí kód dle zásad Clean Code a SOLID
probíraných na cvičeních a důsledně je dodržujte.

Hodnotíme:

-   využití **Entity Framework (EF) Code First** na vytvoření databáze
    z tříd navrhnutých ve fázi 1

-   návrh WPF aplikace dle návrhového vzoru **Model View
    ViewModel (MVVM)**

-   čistotu kódu

-   Testy

<!-- -->

-   -   -   

### Fáze 3 – WPF frontend, data binding (40 bodů) – 24 hodin před obhajobou

V poslední fázi vytváříte výslednou podobu Vaší aplikace. Budete zde
provazovat backend ve WPF aplikaci, který jste si připravili ve 2. fázi
s jednotlivými obrazovkami a zobrazením dat.

Vytvořte *View* k jednotlivým navrženým *View-Modelům*. Zamyslete se nad
tím, jakým způsobem je vhodné jednotlivá data zobrazovat.

Využijte *binding* v XAML kódu (vyvarujte se code-behind). Účelem není
jenom udělat aplikaci, která funguje, ale také aplikaci, která je
správně navržena a může být dále jednoduše upravitelná a rozšířitelná.
Dbejte tedy zásad probíraných ve cvičeních.

Za aplikace, jejichž vizuální návrh bude proveden dobře, a zároveň budou
plně funkční, budeme udělovat také bonusové body.

Hodnotíme:

-   funkčnost celé výsledné aplikace

-   vytvoření View k příslušným View-Modelům z fáze 2

-   zobrazení jednotlivých informací dle zadání – seznam, detail…

-   správné využití data bindingu v XAML

-   čistotu kódu

-   vytvoření dobře vypadající a plně funkční aplikace (bonusové body)

### 

### Bonusové body

Za projevenou iniciativu je možné získat bonusové body. Nápady na možná
rozšíření:

-   Napojení programu na online zdroj obrázků (Google Drive, OneDrive,
    DropBox, Picasa…)

-   Použití služby na získání informací o obrázku (např.
    osoby, předměty) – Microsoft Cognitive Services, Google Cloud Vision
    API…

-   Možnost zobrazení obrázku na mapě, práce s GPS polohou

-   Načtení kompletních EXIF informací z obrázku a zobrazení a využití v
    programu

<!-- -->

-   -   -   -   -   -   

### 

### Obhajoba

Obhajoby projektů budou probíhat v** posledních 2 týdnech** výuky
cvičení. Termíny obhajob budou vyhlášeny v průběhu semestru.

Na obhajobu se dostaví **celý tým**. Z členů týmu bude cvičícími vybrán
1 student, který obhajobu povede. Na obhajobu **není nutné** mít
prezentaci (powerpoint nebo pdf). Budete nám muset ukázat, jak funguje
váš kód, že je správně navržen. Připravte se na naše otázky
k funkcionalitě jednotlivých tříd a k důvodům jejich členění. Na
obhajobu bude mít tým 15-20 minut.

### Finální odevzdání

Po skončení obhajoby bude nutné ještě nahrát váš program do informačního
systému. **Bez nahraného programu nemůžeme týmu udělit bodové
hodnocení.**

Zdrojový kód očistěte o všechny nepotřebné soubory, které neverzujete
(adresáře obj, bin, packages... viz .gitignore).
