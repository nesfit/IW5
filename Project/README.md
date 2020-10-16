# IW5 projekt

## Důležité upozornění
Pro hodnocení projektu (a úspěšné absolvování předmětu) je nutno dokončit **obě 2 fáze projektu** a projekt **obhájit**. Pokud projekt nebude při obhajobě obsahovat základní funkcionalitu uvedenou v zadání, bude hodnocen 0 body. **Nespokojíme se tedy s nedokončeným projektem**. Tuhle poznámku sem dáváme proto, že se v předchozích  ročnících vyskytly týmy, které po dosáhnutí součtu 50 bodů za předmět po 1. fázi rozhodly nedokončit projekt a poté byly nemile překvapeni, když se po nich vyžadovala plná funkcionalita při obhajobě. Dejte si na to tedy prosím pozor.

# Cíl
Cílem je vytvořit použitelnou a snadno rozšiřitelnou aplikaci, která splňuje požadavky zadání. Aplikace nemá padat nebo zamrzávat.

Zadání úmyslně není striktní, je Vám ponechána volnost, pro vlastní realizaci. Při hodnocení je kladen důraz na technické zpracování a kvalitu kódu, ale hodnotíme i použitelnost a grafické zpracování aplikace. Pokud Vám přijde, že v zadání chybí nějaká funkcionalita, neváhejte ji doplnit. Pište aplikaci tak, aby jste ji sami chtěli používat.

# Zadání - Webová aplikace e-shop
Výsledná aplikace má sloužit jako jednoduchá simulace e-shopu.

---
## Data
V rámci dat, se kterými se bude pracovat budeme požadovat minimálně následující data.

### Zboží
- Název
- Fotografie
- Textový popis
- Cena
- Hmotnost
- Počet kusů na skladě
- Kategorie
- Výrobce
- Hodnocení

### Kategorie
- Název

### Výrobce
- Název
- Textový popis
- Obrázek (logo)
- Země
- Seznam zboží

### Hodnocení
- Číselné hodnocení
- Textové hodnocení

---
## Funkcionalita
Webová aplikace bude obsahovat několik stránek pro zobrazování a zadávání dat.

V zadání není požadováno perzistentní uložení dat. To znamená, že když se aplikace restartuje, tak může o data přijít. Nicméně bude nutno data ukládat za běhu aplikace, aby bylo možno demonstrovat, že když se například pomocí aplikace přidá nové zboží, tak se toto zboží zobrazí v seznamu zboží (a podobně pro ostatní data).

I když není vysloveně požadováno perzistentní uložení dat, je doporučováno jeho implementace a za implementaci perzistentního uložení dat budou uděleny **bonusové body**. Způsob uložení (SQL databáze, NoSQL databáze, JSON, XML, CSV...) necháváme na vašem výběru. Pokud bude ale aplikace deklarovat, že data perzistentně uchovává, tak toto bude i otestováno.

Minimální rozsah, který je požadován v rámci projektu je popsán v této kapitole.

### Funkcionalita e-shopu
**Není nutno** implementovat celkovou funkcionalitu, kterou byste čekali od e-shopu. Pro začátek - neočekáváme, že se v e-shopu bude dát nakoupit. Není tedy nutno implementovat nákupní košík a podobné funkce. Co je vyžadováno v tomto zadání by se dalo charakterizovat spíše jako katalog zboží.
Nechceme po Vás, abyste vytvářeli jenom další a další stránky kvůli funkcionalitě. Tento předmět má být spíš o tom, abyste si vyzkoušeli hlavní koncepty. Když budete potřebovat přidat další funkcionalitu, tak ji dopíšete za použití stejných konceptů. Nicméně při tvorbě aplikace **mějte na paměti požadavek rozšiřitelnosti**. Myslete tedy na to, že tento _"katalog zboží"_ by měl být vytvořen tak, aby se z něj dal vytvořit i plně funkční e-shop. Dbejte na to při návrhu datových tříd i při tvorbě stránek.

### Téma e-shopu
Věc, kterou Vám v tomto zadání **nepředepisujeme** je téma e-shopu. Váš e-shop tedy může prodávat co uznáte za vhodné (myslete ale na to, že řešení budete obhajovat).

### Seznam zboží
Seznam bude obsahovat všechno zboží dostupné v aplikaci. Bude možno se z něj překliknout na detail zboží a na stránku pro přidání nového zboží. Na stránce se dá zboží filtrovat minimálně dle těchto parametrů:
   - Kategorie
   - Cena (minimální, maximální)
   - Hmotnost (minimální, maximální)
   - Výrobce
   - Hodnocení (minimální, maximální)
   - Je/není na skladě

### Detail zboží
Stránka zobrazuje detail jednoho kusu zboží se všemi informacemi o něm (viz kapitolu Data). Na stránce se také dá přidávat nové hodnocení zboží a zobrazuje se průměrná číselná hodnota hodnocení a textové popisy jednotlivých existujících hodnocení.

### Stránka pro editaci zboží
Stránka, která slouží na editaci zboží. Může se využít na vytvoření nového zboží nebo na editaci již existujícího. Bude obsahovat všechny informace o zboží včetně výrobce a kategorie (viz kapitola Data).

### Seznam výrobců
Stránka obsahuje všechny výrobce. Bude možno se z ní překliknout na detail výrobce a na stránku pro přidání nového výrobce.

### Detail výrobce
Detail výrobce - stránka zobrazuje všechny informace o konkrétním výrobci včetně seznamu zboží, které vyrábí (viz kapitola Data).

### Stránka pro editaci výrobce
Stránka, která slouží na editaci výrobce. Může se využít na vytvoření nového výrobce nebo na editaci existujícího. Bude obsahovat všechny informace o výrobci zboží, který je vyrábí (viz kapitola Data).

### Stránka "Vyhledávání"
Stránka, na které můžete použít textové vyhledávání napříč záznamy v aplikaci. Seznam všech nalezených záznamů se zobrazí na stránce a bude se dát překlikem dostat na detail daného záznamu (v případě hodnocení se odnaviguje na detail zboží, který k hodnocení přísluší). Textově se vyhledává minimálně v těchto atributech:
- Zboží
   - Název
   - Textový popis
- Výrobce
   - Název
   - Textový popis
   - Země
- Hodnocení
   - Textové hodnocení

---
## Správa projektu - Azure DevOps
Projekt řeší studenti v týmech. V každém týmu jsou **3 studenti**.

Při řešení projektu týmy využívají Azure DevOps a využívají GIT na sdílení kódu. Do svého projektu přidělte přístup vyučujícím; tj. do Vašeho týmového projektu si v části Members přidejte účet **uciteliw5@vutbr.cz**

Účet **uciteliw5@vutbr.cz** budou používat vyučující pro přístup k odevzdávaným souborům. Bez přidání tohoto účtu není možné přistoupit k vašemu projektu a tedy není možné jej ze strany vyučujících hodnotit.

Návod na přidání člena projektu můžete najít zde: *https://docs.microsoft.com/en-us/vsts/accounts/add-team-members-vs*

Z GITu *musí být viditelná postupná práce na projektu a spolupráce týmu*. Pokud uvidíme, že existuje malé množství nelogických a nepřeložitelných commitů tak nás bude zajímat, jak jste spolupracovali a může to vést na snížení bodového hodnocení. Organizaci pojmenujte **iw5-2020Z-team<0000>** dle Vašeho čísla týmu a projekt **project** tak, že výsledné URL pro přístup pro tento imaginární tým by bylo https://dev.azure.com/iw5-2020Z-team0000/project. Nezapomeňte nastavit **Work item process** template na **Scrum**.

# Odevzdávání
Odevzdávání projektu má **2 fáze**. V každé fázi se hodnotí jiné vlastnosti projektu. Nicméně fáze na sebe navzájem následují a studenti pokračují v práci na svém kódu i po jeho odevzdání v rámci následující fáze.

**Kontroluje se kód, který je nahrán v GIT** ve větvi `master`. Vždy se kontroluje **poslední commit před časem odevzdávání** dané fáze projektu. Na commity nahrány po času odevzdávání nebo v jiných větvích nebude brán zřetel. Pokud commit, který máme hodnotit otagujete, např. `v1, v2`, usnadníte nám orietaci při hodnocení.

Je silně doporučováno projekty v průběhu semestru konzultovat po přednášce/cvičení, předejdete tak případným komplikacím při odevzdání.
 
---
### Fáze 1 – API (50 bodů)
V první fázi se zaměříme na vytvoření Web API služby. Výstupem tedy bude spustitelný projekt, který obsahuje Web API, poskytuje specifikaci ve standardu OpenAPI (výběr verze necháme na vás) a poskytuje přístup k API pomocí Swagger inspektoru. API obsahuje minimálně metody pro:
- Zboží
   - Získání seznamu všeho zboží
   - Získání detailu zboží
   - Vytvoření nového zboží
   - Upravení existujícího zboží
   - Smazání zboží
- Výrobce
   - Získání seznamu všech výrobců
   - Získání detailu výrobce
   - Vytvoření nového výrobce
   - Upravení existujícího výrobce
   - Smazání výrobce
- Hodnocení
   - Získání seznamu všech hodnocení pro konkrétní zboží
   - Získání detailu hodnocení
   - Vytvoření nového hodnocení
   - Upravení existujícího hodnocení
   - Smazání hodnocení
- Vyhledávání
   - Získání výsledků vyhledávání
Vzorové API, dle kterého se můžete inspirovat bude ukazováno na přednáškách/cvičeních.

V 1. fázi bude také požadováno pokrytí API testy. Minimálně musí být pokryty všechny API endpointy dostatečným počtem testů, aby se pomocí nich dala ověřit správnost funkcionality API. Počítáme tedy s tím, že budete mít vytvořeny testy, které můžeme u sebe spustit a tyto testy otestují správnost Vašeho řešení. To, jak psát testy bude ukázáno v rámci přednášek/cvičení.
Budeme tedy kontrolovat jak to, že máte napsány správné testy tak to, že aplikace funguje.

Hodnotíme:
- logický návrh tříd
- splnění funkcionality
- testy
- využití abstrakce, zapouzdření, polymorfismu
- čistotu kódu
- verzování v GITu po logických částech
- logické rozšíření datového návrhu nad rámec zadání (bonusové body)

---
### Fáze 2 - Web (50 bodů)
V druhé fázi se od vás bude požadovat vytvoření webové aplikace pomocí technologie Blazor. Webová aplikace bude napojena na API vytvořeno v první fázy projektu.

Hodnotíme:
- funkčnost celé výsledné aplikace
- zobrazení jednotlivých informací dle zadání – seznam, detail…
- čistotu kódu
- vytvoření dobře vypadající aplikace (bonusové body)

---
## Obhajoba
Obhajoby projektů budou probíhat v **posledním týdnu** semestru. Termíny obhajob budou vyhlášeny v průběhu semestru.

Na obhajobu se dostaví **celý tým**. Z členů týmu bude cvičícími vybrán 1 student, který obhajobu povede. Na obhajobu **není nutné** mít prezentaci (Powerpoint nebo PDF). Budete nám muset ukázat, jak funguje váš kód, že je správně navržen. Obhajoby budou probíhat osobně, nebo online dle aktuálních omezení v době obhajob.

Připravte se na naše otázky k funkcionalitě jednotlivých tříd a k důvodům jejich členění.
