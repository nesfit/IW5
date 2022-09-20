# IW5 projekt

## Důležité upozornění
Pro hodnocení projektu (a úspěšné absolvování předmětu) je nutno dokončit **obě 2 fáze projektu** a projekt **obhájit**. Pokud projekt nebude při obhajobě obsahovat základní funkcionalitu uvedenou v zadání, bude hodnocen 0 body. **Nespokojíme se tedy s nedokončeným projektem**. Tuhle poznámku sem dáváme proto, že se v předchozích  ročnících vyskytly týmy, které po dosáhnutí součtu 50 bodů za předmět po 1. fázi rozhodly nedokončit projekt a poté byly nemile překvapeni, když se po nich vyžadovala plná funkcionalita při obhajobě. Dejte si na to tedy prosím pozor.

# Cíl
Cílem je vytvořit použitelnou a snadno rozšiřitelnou aplikaci, která splňuje požadavky zadání. Aplikace nemá padat nebo zamrzávat.

Zadání úmyslně není striktní, je Vám ponechána volnost, pro vlastní realizaci. Při hodnocení je kladen důraz na technické zpracování a kvalitu kódu, ale hodnotíme i použitelnost a grafické zpracování aplikace. Pokud Vám přijde, že v zadání chybí nějaká funkcionalita, neváhejte ji doplnit. Pište aplikaci tak, aby jste ji sami chtěli používat.

# Zadání - Webová aplikace pro donášku jídel
Výsledná aplikace má sloužit jako jednoduchá aplikace simulující webovou stránku pro donášku jídel.

---
## Data
V rámci dat, se kterými se bude pracovat budeme požadovat minimálně následující data.

### Restaurace
- Název
- Logo
- Textový popis
- Adresa
- Souřadnice na mapě

### Jídlo
- Název
- Fotografie
- Textový popis
- Cena
- Seznam alergenů

### Objednávka
- Jméno
- Adresa
- Požadovaný čas doručení
- Poznámka
- Stav objednávky
- Seznam a počet objednaných jídel i s možnými poznámkami

---
## Funkcionalita
Webová aplikace bude obsahovat několik stránek pro zobrazování a zadávání dat.

V zadání není požadováno perzistentní uložení dat. To znamená, že když se aplikace restartuje, tak může o data přijít. Nicméně bude nutno data ukládat za běhu aplikace, aby bylo možno demonstrovat, že když se například pomocí aplikace přidá nový záznam, tak se tento zobrazí v příslušném seznamu záznamů, dá se editovat, smazat atd.

Minimální rozsah, který je požadován v rámci projektu je popsán v této kapitole.

### Seznam restaurací
Seznam bude obsahovat všechny restaurace dostupné v aplikaci. Bude možno se z něj překliknout na detail restaurace a na pohled pro přidání nové restaurace.

### Detail restaurace
Zobrazuje detail restaurace se všemi informacemi o něm a se seznamem jídel.

### Editace restaurace
Stránka, která slouží na editaci restaurace. Může se využít na vytvoření nové restaurace nebo na editaci existující. Bude obsahovat všechny informace o restauraci.

### Seznam jídel
Seznam jídel v restauraci. Bude možno se překliknout na detail jídla a přidání nového jídla. Jídla se budou dát řadit minimálně dle ceny. A budou se dát filtrovat minimálně pomocí alergenů.

### Detail jídla
Zobrazuje detail jídla se všemi informacemi o něm.

### Editace jídla
Stránka, která slouží na editaci jídla. Může se využít na vytvoření nového jídla nebo na editaci existujícího. Bude obsahovat všechny informace o jídle.

### Seznam objednávek
Pohled obsahuje všechny objednávky v rámci systému. Bude možno se z něj překliknout na detail objednávky a na pohled pro přidání nové objednávky.

### Detail objednávky
Stránka zobrazuje všechny informace o konkrétní objednávce včetně jejího stavu.

### Editace objednávky
Stránka, která slouží na editaci objednávky. Může se využít na vytvoření nové objednávky nebo na editaci existující. Bude obsahovat všechny informace o objednávce.

### Stránka "Tržby restaurace"
Stránka zobrazí tržby pro vybranou restauraci - t.j. sumu z objednávek, které byly úspěšně doručeny a zaplaceny.

### Stránka "Vyhledávání"
Stránka, na které můžete použít textové vyhledávání napříč záznamy v aplikaci. Seznam všech nalezených záznamů se zobrazí na stránce a bude se dát překlikem dostat na detail daného záznamu (tedy například v případě týmu se odnaviguje na detail týmu). Textově se vyhledává minimálně v těchto atributech:
- Restaurace
   - Název
   - Textový popis
   - Adresa
- Jídlo
   - Název
   - Textový popis

---
## Správa projektu - Azure DevOps
Projekt řeší studenti v týmech. V každém týmu jsou **3 studenti**.

Při řešení projektu týmy využívají Azure DevOps a využívají GIT na sdílení kódu. Do svého projektu přidělte přístup vyučujícím; tj. do Vašeho týmového projektu si v části Members přidejte účet **uciteliw5@vutbr.cz**

Účet **uciteliw5@vutbr.cz** budou používat vyučující pro přístup k odevzdávaným souborům. Bez přidání tohoto účtu není možné přistoupit k vašemu projektu a tedy není možné jej ze strany vyučujících hodnotit.

Návod na přidání člena projektu můžete najít zde: *https://docs.microsoft.com/en-us/vsts/accounts/add-team-members-vs*

Z GITu *musí být viditelná postupná práce na projektu a spolupráce týmu*. Pokud uvidíme, že existuje malé množství nelogických a nepřeložitelných commitů tak nás bude zajímat, jak jste spolupracovali a může to vést na snížení bodového hodnocení. Organizaci pojmenujte **iw5-2022-team<0000>** dle Vašeho čísla týmu a projekt **project** tak, že výsledné URL pro přístup pro tento imaginární tým by bylo https://dev.azure.com/iw5-2022-team0000/project. Nezapomeňte nastavit **Work item process** template na **Scrum**.

## Architektura projektu

Ve výuce Vám ukazujeme nějakou strukturu organizace kódu do logických vrstev a projektů se zapojením návrhových vzorů. Pokoušíme se vysvětlit proč je vzorový projekt takhle organizovaný a proč jsou zvoleny jednotlivá rozhodnutí.

Budeme tedy i po Vás chtít logické rozvržení projektu. Můžete využít to, jak je organizovaný vzorový projekt probíraný na cvičeních a inspirovat se tímto uspořádáním (můžete ho mít stejné, za to Vám rozhodně body nestrhnem). Nebo můžete využít i vlastní uspořádání - v tom případě ale po Vás budeme chtít vysvěltit proč jste němu přistoupili a čím se jeho jednotlivé aspekty řídí.

V každém případě ale budeme chtít aby výsledné řešení obsahovalo víc projektů a vrstev. Snažíme se Vám na tomto projektu ukázat nějakou základní architekturu SW projektu, aby jste si odnesli i něco víc než jen to, že budete znát syntax jazyka C#. Na tenhle aspekt tedy rozhodně bude brán zřetel ve všech fázích hodnocení projektu.

## Nasazení do Azure

V rámci přednášek se budeme věnovat také nasazení celého řešení do prostředí Azure. Zkusíte si tedy nasadit všechny části Vašeho řešení a také automatizaci nasazování celého systému. Při pojmenování webů, databáze (pokud ji budete používat) a dalších částí, které budete vytvářet vycházejte z návodu, který máte k dispozici v rámci 1. přednášky. Také nezapomeňte přiřadit přístup k projektové části Azure pro učitelský účet (dle pokynů v 1. přednášce).

Schéma pojmenování věcí, které budete potřebovat založit v Azure je ukázána v prezentaci k 1. přednášce - prosím držte se tohoto schématu.

# Odevzdávání
Odevzdávání projektu má **2 fáze**. V každé fázi se hodnotí jiné vlastnosti projektu. Nicméně fáze na sebe navzájem následují a studenti pokračují v práci na svém kódu i po jeho odevzdání v rámci následující fáze.

Pokud se týmově rozhodnete, že všichni členové nepřispěli rovnoměrně k vypracování projektu. Přidejte do kořene repozitáře textový soubor s názvem ROZDELENI.txt, ve kterém uveďte loginy všech členů týmu a poměrné rozdělení bodů v procentech (struktura není pevně daná). V případě, že soubor nepřiložíte nebo nebude srozumitelný tak implicitně uvažujeme rovnoměrné rozdělení bodů. Pro rovnoměrné rozložení bodů tedy není nutné soubor přikládat.

**Kontroluje se kód, který je nahrán v GIT** ve větvi `master` nebo `main`. Vždy se kontroluje **poslední commit před časem odevzdávání** dané fáze projektu. Na commity nahrány po času odevzdávání nebo v jiných větvích nebude brán zřetel. Pokud commit, který máme hodnotit otagujete, např. `v1, v2`, usnadníte nám orietaci při hodnocení.

Je silně doporučováno projekty v průběhu semestru konzultovat po přednášce/cvičení, předejdete tak případným komplikacím při odevzdání.
 
---
### Fáze 1 – API (50 bodů)
V první fázi se zaměříme na vytvoření Web API služby. Výstupem tedy bude spustitelný projekt, který obsahuje Web API, poskytuje specifikaci ve standardu OpenAPI (výběr verze necháme na vás) a poskytuje přístup k API pomocí Swagger inspektoru. API obsahuje minimálně metody pro:
- Restaurace
   - Získání seznamu restaurací
   - Získání detailu restaurace
   - Vytvoření nové restaurace
   - Upravení existující restaurace
   - Smazání existující restaurace
- Jídlo
   - Získání seznamu všech jídel pro restauraci
   - Získání detailu jídla
   - Vytvoření nového jídla
   - Upravení existujícího jídla
   - Smazání existujícího jídla
- Objednávka
   - Získání seznamu všech objednávek pro restauraci
   - Získání detailu objednávky
   - Vytvoření nové objednávky
   - Upravení existující objednávky
   - Smazání existující objednávky
- Tržby
   - Získání seznamu tržeb pro jednotlivé restaurace
- Vyhledávání
   - Získání výsledků vyhledávání
Vzorové API, dle kterého se můžete inspirovat bude ukazováno na přednáškách/cvičeních.

V 1. fázi bude také požadováno pokrytí API testy. Minimálně musí být pokryty všechny API endpointy dostatečným počtem testů, aby se pomocí nich dala ověřit správnost funkcionality API.

Počítáme tedy s tím, že budete mít vytvořeny testy, které můžeme spustit jak lokálně tak v rámci Azure DevOps a tyto testy testují správnost Vašeho řešení. To, jak psát testy bude ukázáno v rámci přednášek/cvičení.

Budeme tedy kontrolovat jak to, že máte napsány správné testy tak to, že aplikace funguje.

Hodnotíme:
- logický návrh tříd
- splnění funkcionality
- využití abstrakce, zapouzdření, polymorfismu
- čistotu kódu
- verzování v GITu po logických částech
- testy
- automatizované nasazení do Azure (CI + CD) z Azure DevOps
- logické rozšíření datového návrhu nad rámec zadání (bonusové body)

---
### Fáze 2 - Web (50 bodů)
V druhé fázi se od vás bude požadovat vytvoření webové aplikace pomocí technologie Blazor. Webová aplikace bude napojena na API vytvořeno v první fázy projektu.

Hodnotíme:
- opravení chyb a zapracování připomínek, které jsme vám dali v rámci hodnocení fáze 1
- funkčnost celé výsledné aplikace
- zobrazení jednotlivých informací dle zadání – seznam, detail, vytváření, editace, mazání…
- čistotu kódu
- vytvoření dobře vypadající aplikace (bonusové body)

---
## Obhajoba
Obhajoby projektů budou probíhat v **posledním týdnu** semestru. Termíny obhajob budou vyhlášeny v průběhu semestru.

Na obhajobu se dostaví **celý tým**. Z členů týmu bude cvičícími vybrán 1 student, který obhajobu povede. Na obhajobu **není nutné** mít prezentaci (Powerpoint nebo PDF). Budete nám muset ukázat, jak funguje váš kód, že je správně navržen. Obhajoby budou probíhat osobně, nebo online dle aktuálních omezení v době obhajob.

Připravte se na naše otázky k funkcionalitě jednotlivých tříd a k důvodům jejich členění.
