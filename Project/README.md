# IW5 projekt

## Důležité upozornění
Pro hodnocení projektu (a úspěšné absolvování předmětu) je nutno dokončit **obě 2 fáze projektu** a projekt **obhájit**. Pokud projekt nebude při obhajobě obsahovat základní funkcionalitu uvedenou v zadání, bude hodnocen 0 body. **Nespokojíme se tedy s nedokončeným projektem**. Tuhle poznámku sem dáváme proto, že se v předchozích  ročnících vyskytly týmy, které po dosáhnutí součtu 50 bodů za předmět po 1. fázi rozhodly nedokončit projekt a poté byly nemile překvapeni, když se po nich vyžadovala plná funkcionalita při obhajobě. Dejte si na to tedy prosím pozor.

# Cíl
Cílem je vytvořit použitelnou a snadno rozšiřitelnou aplikaci, která splňuje požadavky zadání. Aplikace nemá padat nebo zamrzávat.

Zadání úmyslně není striktní, je Vám ponechána volnost, pro vlastní realizaci. Při hodnocení je kladen důraz na technické zpracování a kvalitu kódu, ale hodnotíme i použitelnost a grafické zpracování aplikace. Pokud Vám přijde, že v zadání chybí nějaká funkcionalita, neváhejte ji doplnit. Pište aplikaci tak, aby jste ji sami chtěli používat.

# Zadání - Webová aplikace pro správu filmů
Výsledná aplikace má sloužit pro správu filmové kolekce. Pro zjednodušení si můžete představit, že vytváříte jednodušší verzi webu jako IMDB nebo ČSFD.

---
## Data
V rámci dat, se kterými se bude pracovat budeme požadovat minimálně následující data.

### Film
- Originální název
- Název česky
- Žánr
- Titulní fotografie
- Země původu
- Délka filmu
- Textový obsah filmu
- Seznam režisérů
- Seznam herců
- Seznam hodnocení filmu

### Osoba (herec/režisér)
- Jméno
- Příjmení
- Věk
- Fotografie
- Seznam filmů ve kterých hrál
- Seznam filmů, které režíroval

### Hodnocení filmu
- Číselné hodnocení
- Textové hodnocení

---
## Funkcionalita
Webová aplikace bude obsahovat několik stránek pro zobrazování a zadávání dat. 

V zadání není požadováno perzistentní uložení dat. To znamená, že když se aplikace restartuje, tak může o data přijít. Nicméně bude nutno data ukládat za běhu aplikace, aby bylo možno demonstrovat, že když se například pomocí aplikace přidá nový film, tak se tento film zobrazí v seznamu filmů (a podobně pro ostatní data).

I když není vysloveně požadováno perzistentní uložení dat, je doporučováno jeho implementace a za implementaci perzistentního uložení dat budou uděleny **bonusové body**. Způsob uložení (SQL databáze, NoSQL databáze, JSON, XML, CSV...) necháváme na vašem výběru. Pokud bude ale aplikace deklarovat, že data perzistentně uchovává, tak toto bude otestováno.

Minimální rozsah, který je požadován v rámci projektu je popsán v této kapitole.

### Seznam filmů
Seznam bude obsahovat všechny filmy dostupné v aplikaci. Bude možno se z něj překliknout na detail filmu a na stránku pro přidání nového filmu.

### Detail filmu
Stránka zobrazuje detail jednotlivého filmu se všemi informacemi o filmu (viz kapitolu Data). Na stránce se také dá přidávat nové hodnocení filmu a zobrazuje se průměrná číselná hodnota hodnocení a textové popisy jednotlivých existujících hodnocení.

### Stránka pro editaci filmu
Stránka, která slouží na editaci filmu. Může se využít na vytvoření nového filmu nebo na editaci existujícího filmu. Bude obsahovat všechny informace o filmu včetně výběru herců a režisérů (viz kapitola Data).

### Seznam osob (herců/režisérů)
Stránka obsahuje všechny osoby. Bude možno se z ní překliknout na detail osoby a na stránku pro přidání nové osoby

### Detail osoby
Detail osoby - stránka zobrazuje všechny informace o konkrétní osobě včetně seznamu filmů, ve kterých hrála a které režírovala (viz kapitola Data).

### Stránka pro editaci osoby
Stránka, která slouží na editaci osoby. Může se využít na vytvoření nové osoby nebo na editaci existující osoby. Bude obsahovat všechny informace o osobě včetně filmů, ve kterých hrála a které režírovala (viz kapitola Data).

### Stránka "Vyhledávání"
Stránka, na které můžete použít textové vyhledávání napříč záznamy v aplikaci. Seznam všech nalezených záznamů se zobrazí na stránce a bude se dát překlikem dostat na detail daného záznamu (v případě hodnocení se odnaviguje na detail filmu, který k hodnocení přislouchá). Textově se vyhledává minimálně v těchto atributech:
- Film
   - Originální název
   - Název česky
   - Země původu
   - Textový obsah filmu
- Osoba
   - Jméno
   - Pŕíjmení
- Hodnocení
   - Textové hodnocení

---
## Správa projektu - Azure DevOps
Projekt řeší studenti v týmech. V každém týmu jsou **3 studenti**.

Při řešení projektu týmy využívají Azure DevOps a využívají GIT na sdílení kódu. Do svého projektu přidělte přístup vyučujícím (způsob bude vysvětlen v rámci 1. cvičení); tj. do Vašeho týmového projektu si v části Members přidejte účet **uciteliw5@vutbr.cz**

Účet **uciteliw5@vutbr.cz** budou používat vyučující pro přístup k odevzdávaným souborům. Bez přidání tohoto účtu není možné přistoupit k vašemu projektu a tedy není možné jej ze strany vyučujících hodnotit.

Návod na přidání člena projektu můžete najít zde: *https://docs.microsoft.com/en-us/vsts/accounts/add-team-members-vs*

Z GITu *musí být viditelná postupná práce na projektu a spolupráce týmu*. Pokud uvidíme, že existuje malé množství nelogických a nepřeložitelných commitů tak nás bude zajímat, jak jste spolupracovali a může to vést na snížení bodového hodnocení. Organizaci pojmenujte **iw5-2019-team<0000>** dle Vašeho čísla týmu a projekt **project** tak, že výsledné URL pro přístup pro tento imaginární tým by bylo https://dev.azure.com/iw5-2019-team0000/project. Nezapomeňte nastavit **Work item process** template na **Scrum**.


# Odevzdávání
Odevzdávání projektu má **2 fáze**. V každé fázi se hodnotí jiné vlastnosti projektu. Nicméně fáze na sebe navzájem následují a studenti pokračují v práci na svém kódu i po jeho odevzdání v rámci následující fáze.

**Kontroluje se kód, který je nahrán v GIT** ve větvi `master`. Vždy se kontroluje **poslední commit před časem odevzdávání** dané fáze projektu. Na commity nahrány po času odevzdávání nebo v jiných větvích nebude brán zřetel. Pokud commit, který máme hodnotit otagujete, např. `v1, v2`, usnadníte nám orietaci při hodnocení.

Je silně doporučováno projekty v průběhu semestru konzultovat po přednášce/cvičení, předejdete tak případným komplikacím při odevzdání.
 
---
### Fáze 1 – API (30 bodů)
V první fázi se zaměříme na vytvoření Web API služby. Výstupem tedy bude spustitelný projekt, který obsahuje Web API, poskytuje specifikaci ve standardu OpenAPI (výběr verze necháme na vás) a poskytuje přístup k API pomocí Swagger inspektoru. API obsahuuje mina  minimálně metody pro:
- Film
   - Získání seznamu všech filmů
   - Získání detailu filmu
   - Vytvoření nového filmu
   - Upravení existujícího filmu
   - Smazání filmu
- Osoba
   - Získání seznamu všech osob
   - Získání detailu osoby
   - Vytvoření nové osoby
   - Upravení existující osoby
   - Smazání osoby
- Hodnocení
   - Získání seznamu všech hodnocení napříč všemi filmy
   - Získání detailu hodnocení
   - Vytvoření nového hodnocení
   - Upravení existujícího hodnocení
   - Smazání hodnocení
- Vyhledávání
   - Získání výsledků vyhledávání
Vzorové API, dle kterého se můžete inspirovat najdete [zde](https) (vaše API se nemusí nutně držet formátu vzorového API, ale musí obsahovat výše zmíněné metody).

Hodnotíme:
- logický návrh tříd
- využití abstrakce, zapouzdření, polymorfismu
- verzování v GITu po logických částech
- logické rozšíření datového návrhu nad rámec zadání (bonusové body)
- čistotu kódu

---
### Fáze 2 - Web (40 bodů)
V druhé fázi se od vás bude požadovat vytvoření webové aplikace pomocí technologie ASP.Net Core MVC. Webová aplikace bude napojena na API vytvořeno v první fázy projektu. Pro napojení můžete použít Vaši implementaci, nebo můžete použít vzorovou implementaci, kterou dostanete po odevzdání první fáze projektu.

Hodnotíme:
- funkčnost celé výsledné aplikace
- zobrazení jednotlivých informací dle zadání – seznam, detail…
- čistotu kódu
- vytvoření dobře vypadající aplikace (bonusové body)


Pro získání bonusových bodů a také pro vyzkoušní si práce s *reálným* API můžete místo použití API z 1. fáze projektu využít API některé z existujících služeb, které se zabývají filmy. Pokud se rozhodnete použít API některé z těchto služeb, ve 2. fázi projektu nebude nutno dodržet přesnou funkcionalitu popsanou v zadání - rozsah ale bude minimálně v rozsahu tohoto zadání. Při výběru této varianty nás prosím kontaktujte a na konkrétním zadání se domluvíme.
API, k jednotlivým službám najdete zde (pokud víte ještě o jiné, tak dejte klidně vědět):
- [The Open Movie Database](http://www.omdbapi.com)
- [Trakt](https://trakt.docs.apiary.io)
- [Simkl](https://simkl.docs.apiary.io)
- IMDB má veřejně dostupné API, ale není zdokumentováno
- ČSFD nemá veřejně dostupné API :(

---
## Obhajoba
Obhajoby projektů budou probíhat v **posledních 2 týdnech** semestru. Termíny obhajob budou vyhlášeny v průběhu semestru.

Na obhajobu se dostaví **celý tým**. Z členů týmu bude cvičícími vybrán 1 student, který obhajobu povede. Na obhajobu **není nutné** mít prezentaci (Powerpoint nebo pdf). Budete nám muset ukázat, jak funguje váš kód, že je správně navržen. Připravte se na naše otázky
k funkcionalitě jednotlivých tříd a k důvodům jejich členění. Na obhajobu bude mít tým 10-15 minut.