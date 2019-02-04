# IW5 projekt (Zadání z minulého roku - bude změněno!)

## Důležité upozornění

Pro hodnocení projektu (a úspěšné absolvování předmětu) je nutno
dokončit **všechny 3 fáze projektu** a projekt **obhájit**. Pokud
projekt nebude při obhajobě obsahovat základní funkcionalitu uvedenou v
zadání, bude hodnocen 0 body. **Nespokojíme se tedy
s nedokončeným projektem**. Tuhle poznámku sem dáváme proto, že se
v předchozích ročnících vyskytly týmy, které po dosáhnutí součtu 50 bodů
za předmět po 2. fázi rozhodly nedokončit projekt a poté byly nemile
překvapeni, když se po nich vyžadovala plná funkcionalita při obhajobě.
Dejte si na to tedy prosím pozor.


## Cíl
Cílem je vytvořit použitelnou a snadno rozšiřitelnou aplikaci, která splňuje požadavky zadání. Aplikace nemá padat nebo zamrzávat, pokud uživatel vyplní něco špatně, upozorní ho validační hláškou.

---

Zadání úmyslně není striktní, je Vám ponechána volnost, pro vlastní realizaci. Při hodnocení je kladen důraz na technické zpracování a kvalitu kódu, ale hodnotíme i na použitelnost a grafické zpracování aplikace. Pokud Vám přijde, že v zadání chybí nějaká funkcionalita, neváhejte ji doplnit. Pište aplikaci tak, aby jste ji sami chtěli používat.

---

## Zadání - Aplikace pro týmovou komunikaci

Výsledná aplikace má sloužit studentům k usnadnění komunikace na týmových projektech. Uživatel (Student) může být součástí více týmů. Členové týmu komunikují pomocí příspěvků a odpovědí na ně. Pro lepší představu si představte zjednodušenou Facebookovou skupinu. 

### Přihlášení
Uživatel se hlásí na základě emailu a hesla. **Hesla ukládejte v bezpečné podobě!**

### Seznam týmů
Po přihlášení vidí uživatel seznam svých týmů a může si je zobrazit. 

### Příspěvky týmu

Zde každý člen týmu vidí všechny příspěvky a odpovědi na ně a může přidávat příspěvek nebo komentář k jinému příspěvku. Ostatní uživatelé zde nemají přístup.

 Příspěvky v týmu jsou zobrazeny i s komentáři a seřazeny dle datumu vytvoření posledního komentáře, ne datumu zveřejnění příspěvku. Komentáře se řadí chronologicky.

 - Příspěvek č. 1
    - odpověď č. 1 na příspěvek č. 1
    - odpověď č. N na příspěvek č. 1
 - Příspěvek č. 2
    - odpověď č. 1 na příspěvek č. 2
    - odpověď č. N na příspěvek č. 2

Zobrazení příspěvku obsahuje zvýrazněný titulek, formátovaný text, autora a datum zveřějnění. Můžete přidat i vlastní rozšíření. Možnost odpovědět na příspěvek.

Odpověď na příspěvek obsahuje formátovaný text, autora a datum zveřějnění. Můžete přidat i rozšíření.

Při vytváření příspěvku nebo komentáře uživatel nevyplňuje datum zveřejnění ani autora, aplikace si je doplní sama. 

Pokud se aplikace neaktualizuje sama, obsahuje tlačítko pro aktualizaci příspěvků.

### Vyhledávání
Aplikace umožňuje vyhledávání v příspěvcích a komentářích.

### Přehled týmu
Zde uživatel vidí popis týmu a výpis jeho členů. Je zde možnost přidat do týmu další členy a nebo odstranit stávající. 

#### Přidání člena do týmu
Uživatel má na výběr ze všech uživatelů aplikace, kteří nejsou členy týmu. Má také možnost vytvořit nového uživatele. 

### Profil uživatele
Zde je vidět jméno uživatele, jeho poslední aktivita a jeho týmy.

## Spolupráce

Projekt řeší studenti v týmech. V každém týmu jsou **4 studenti**. *Tým
o méně studentech není přípustný.*

Při řešení projektu týmy využívají Visual Studio Team Services (VSTS) a
využívají GIT na sdílení kódu. Do svého GIT repositáře přidělí přístup
vyučujícím (způsob bude vysvětlen v rámci 1. cvičení).

Z GITu *musí být viditelná postupná práce na projektu a spolupráce
týmu*. Pokud uvidíme, že existuje malé množství nelogických a
nepřeložitelných commitů tak nás bude zajímat, jak jste spolupracovali a
může to vést na snížení bodového hodnocení. Repozitář pojmenujte **iw5-2019-team<00>** dle Vašeho čísla týmu tak, že výsledné URL pro přístup pro tento imaginární tým by bylo https://iw5-2018-team00.visualstudio.com.

Do Vašeho týmového projektu na GIT si v části Members přidejte účet
**uciteliw5@vutbr.cz**

Tento účet budou používat vyučující pro přístup k odevzdávaným souborům.
Bez přidání tohoto účtu není možné přistoupit k vašemu projektu a tedy
není možné jej ze strany vyučujících hodnotit.

Návod na přidání člena projektu můžete najít zde:
*https://docs.microsoft.com/en-us/vsts/accounts/add-team-members-vs*

## Odevzdávání

Odevzdávání projektu má **3 fáze**. V každé fázi se hodnotí jiné
vlastnosti projektu. Nicméně fáze na sebe navzájem následují a studenti
pokračují v práci na svém kódu i po jeho odevzdání v rámci následující
fáze.

**Kontroluje se kód, který je nahrán v GIT** ve větvy `master`. Vždy se kontroluje
**poslední commit před časem odevzdávání** dané fáze projektu. Na
commity nahrány po času odevzdávání nebo v jiných větvích nebude brán zřetel.

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

-   využití abstrakce, zapouzdření, polymorfismu

-   verzování v GITu po logických částech

-   logické rozšíření datového návrhu nad rámec zadání
    (bonusové body)


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

### Bonusové body

Za projevenou iniciativu je možné získat bonusové body. Nápady na možná
rozšíření:

-   Příspěvky a komentáře budou moci obsahovat i obrázky, soubory a videa

-   U příspěvků a komentářů půjde označit členy týmu. Označení členové uvidí příspěvek zvýrazněně

-   Uživatel si bude moci zobrazit příspěvky u kterých byl označen napříč týmy

-   Přihlášení pomocí externích služeb (Google, Facebook, Microsoft)


-   -   -   -   -   -   

## Obhajoba

Obhajoby projektů budou probíhat v **posledních 2 týdnech** výuky
cvičení. Termíny obhajob budou vyhlášeny v průběhu semestru.

Na obhajobu se dostaví **celý tým**. Z členů týmu bude cvičícími vybrán
1 student, který obhajobu povede. Na obhajobu **není nutné** mít
prezentaci (powerpoint nebo pdf). Budete nám muset ukázat, jak funguje
váš kód, že je správně navržen. Připravte se na naše otázky
k funkcionalitě jednotlivých tříd a k důvodům jejich členění. Na
obhajobu bude mít tým 15-20 minut.

## Základní funkcionalita

**Pokud aplikace nesplňuje následující podmínky je projekt hodnocen 0 body!'**

Aplikace musí splňovat
 - Přihlašování
    - Bezpečné ukládání hesel
 - Seznam týmů
    - Zobrazení týmů přihlášeného uživatele
 - Zobrazení příspěvků týmu
    - Zobrazení všech příspěvků v týmu řazené dle poslední aktivity u příspěvku
    - Komentáře pod jednotlivými příspěvky
    - Texty příspěvku i komentáře jsou formátovány
    - Možnost vložení příspěvku s titulkem a formátovaným textem
    - Možnost komentovat příspěvek
 - Vytvoření uživatele
 - Správa členů týmu
    - Možnost přidat a odebrat člena

 

## Finální odevzdání

Po skončení obhajoby bude nutné ještě nahrát váš program do informačního
systému. **Bez nahraného programu nemůžeme týmu udělit bodové
hodnocení.**

Zdrojový kód očistěte o všechny nepotřebné soubory, které neverzujete
(adresáře obj, bin, packages... viz .gitignore).
