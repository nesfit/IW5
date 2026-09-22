# IW5 projekt

## Tl;dr
- Téma: webová aplikace pro správu domácího skladu/spíže.
- Povinné entity: Položka, Úložný prostor, Uživatel, Nákupní seznam.
- Povinné operace: CRUD nad všemi entitami, seznamy s filtrací, řazením a stránkováním, textové vyhledávání.
- Perzistence: databáze přes Entity Framework Core nebo in-memory úložiště.
- Architektura: více projektů a vrstev. Jediný projekt je neakceptovatelný.
- Týmy po 3 studentech, kód v Azure DevOps, nasazení do Azure.
- Fáze 1 (50 bodů): Web API s OpenAPI/Swagger, testy, CI + CD. Odevzdává se, hodnotí se poslední commit před deadlinem.
- Fáze 2 (50 bodů): Blazor WebAssembly frontend napojený na API, uživatelské role. Neodevzdává se, hodnotí se při obhajobě.
- Pro absolvování musí být obě fáze hodnoceny alespoň 1 bodem.

---

## Cíl
Cílem je vytvořit použitelnou a snadno rozšiřitelnou aplikaci, která splňuje požadavky zadání. Aplikace nesmí padat ani zamrzávat a chybný vstup uživatele hlásí validační hláškou.

Zadání ponechává volnost pro vlastní realizaci. Důraz je kladen na technické zpracování a kvalitu kódu, hodnotí se ale i použitelnost. Pište aplikaci tak, abyste ji sami chtěli používat. Chybějící funkcionalitu můžete doplnit, rozšíření zdokumentujte v `README.md` Vašeho repozitáře.

---

## Téma projektu
Aplikace slouží jako jednoduchá webová stránka pro správu domácích zásob. Jde o zásoby potravin i například čistících prostředků. Položky se můžou nacházet v různých úložných prostorech.
Zároveň je součástí aplikace i jednoduchých nákupních seznamů.

Uživatel vytváří položky, nastavuje kde jsou umístěny a může sledovat stav položek v jednotlivých úložných prostorech i v celé domácnosti. Zároveň si může naplánovat nákup docházejících položek přímo z aplikace.

Možná rozšíření:
- organizace/filtrování úložných prostor dle místností (lednice, mražák, skříňka - všechny můžou být v kuchyni)
- podpora pro víc domácností
- nahrávání obrázků
- notifikace na blížící se datum spotřeby

---

## Data a entity
Požadujeme minimálně následující položky. Zvažte, co je třeba ukládat a co lze dopočítat při dotazování.

### Položka
- Název
- Obrázek (postačí URL)
- Datum spotřeby
- [Doplňující popis]
- (Uživatel - autor)

### Úložný prostor
- Název 
- Obrázek (postačí URL)
- [Doplňující popis]
- (Položky)
- (Uživatel - autor)

### Uživatel
- Jméno
- Fotografie (postačí URL)
- Role

### Nákupní seznam
- Název
- Obchod
- [Doplňující popis]
- (Položky)
- (Uživatel - autor)

Poznámky:
- `()` označuje možné/doporučené vazby mezi entitami
- `[]` označuje volitelné položky

---

## Důležitá upozornění
- Pro absolvování předmětu musí být každá fáze hodnocena alespoň 1 bodem. Fázi 2 hodnotíme při obhajobě, a pokud aplikace nepředvede základní funkcionalitu dle zadání, je hodnocena 0 body. **Nespokojíme se s nedokončeným projektem.** V minulosti týmy po 50 bodech z fáze 1 přestaly pracovat a u obhajoby neuspěly.
- Uživatelské role se hodnotí až ve fázi 2. Ve fázi 1 API nemusí omezovat akce podle uživatele, entita Uživatel a její data ale musí existovat.

---

## Základní funkcionalita

### Pohledy
Pro každou entitu aplikace obsahuje:
- **Seznam** - stránkovaný přehled záznamů s filtrací a řazením, s odkazem na detail a na vytvoření nového záznamu.
- **Detail** - všechny informace o záznamu.
- **Vytvoření a editace** - buď na stránce detailu, nebo na samostatné stránce.
- **Smazání.**

Pokud pro některou entitu seznam nedává smysl (např. lekce bez vazby na uživatele), rozhodnutí zdůvodněte v `README.md`.

### Vyhledávání
Textové vyhledávání minimálně v těchto datech:
- Položka - název, [doplňující popis]
- Úložný prostor - název, [doplňující popis]
- Uživatel - jméno
- Nákupní seznam - název, [doplňující popis]

### Uživatelské role
Minimálně role **uživatel** a **administrátor**.

Uživatel může:
- vytvářet záznamy
- editovat a mazat záznamy, které vytvořil

Administrátor může:
- vytvářet, editovat a mazat libovolné záznamy
- vytvářet a mazat uživatele

Přihlašování řešte pomocí ASP.NET Core Identity tak, jak bude ukázáno v předmětu. Minimální požadavky na práci s uživateli budou vysvětleny v přednáškách zaměřených na téma Identity management.

### Perzistence
- Použijte Entity Framework Core (Code First, migrace) a relační databázi (např. SQL Server, Azure SQL, SQLite).
- Alternativně je povolené použití in-memory úložiště. V takovém případě je potřeba ošetřit stejné situace jaké můžou nastat při ukládání dat do databáze tak, aby API vracelo příslušné chybové stavy a srozumitelné chybové hlášky.
- Filtrace, řazení, vyhledávání a stránkování probíhají v databázi/in-memory úložišti na serveru (API), ne nad daty na straně klientské aplikace (Web).

---

## Architektura projektu
Ve výuce ukazujeme rozdělení kódu do logických vrstev a projektů s využitím návrhových vzorů a vysvětlujeme proč. Stejné rozvržení chceme i po Vás. Uspořádání vzorového projektu ze cvičení můžete převzít bez ztráty bodů; vlastní uspořádání musíte umět zdůvodnit.

Řešení musí obsahovat více projektů a vrstev (např. API, BL, DAL, Web, Common). Řešení s jediným projektem není akceptovatelné. Architektura se hodnotí v obou fázích. Jinou architekturu (např. Clean Architecture) předem konzultujte.

---

## Odevzdávání
Projekt řešíte v týmech po **3 studentech** a má 2 fáze, které na sebe navazují. Odevzdává se pouze **fáze 1**. **Fáze 2** se hodnotí při obhajobě podle stavu repozitáře.

Kontroluje se kód ve větvi `main`:
- Fáze 1 - poslední commit před časem odevzdání, otagujte jej `review1`.
- Fáze 2 - poslední commit do konce dne před obhajobou, otagujte jej `final`.
- Pozdější commity a jiné větve nebereme v potaz.

Je povoleno:
- použít libovolnou knihovnu z NuGet
- převzít kód z libovolného zdroje včetně LLM (ChatGPT, Copilot, ...), vyjma projektů ostatních týmů, pokud je řádně označen a zdroj uveden
- převzatému kódu musíte rozumět a umět jej u obhajoby vysvětlit; ověřte, že kód i knihovny neporušují licence

Týmová spolupráce:
- Z GITu musí být viditelná postupná práce a spolupráce týmu. Malý počet commitů, nelogické commity nebo commity s nepřeložitelným kódem mohou vést ke snížení hodnocení.
- Doporučujeme Conventional Commits a ["GIT Branching strategy"](https://medium.com/@sreekanth.thummala/choosing-the-right-git-branching-strategy-a-comparative-analysis-f5e635443423).
- Pokud členové nepřispěli rovnoměrně, přidejte do kořene repozitáře soubor `ROZDELENI.txt` s loginy a poměrným rozdělením bodů v procentech. Bez souboru (nebo pokud je nesrozumitelný) bereme rovnoměrné rozdělení.

Projekt v průběhu semestru konzultujte po přednášce nebo cvičení, předejdete komplikacím při odevzdání.

---

## Fáze 1 – API (50 bodů)
Vytvořte spustitelnou Web API službu se specifikací OpenAPI (verzi necháme na Vás) a Swagger UI. Vzorové API bude ukázáno na přednáškách a cvičeních.

Požadavky:
- Endpointy pokrývající celou [Základní funkcionalitu](#základní-funkcionalita) pro každou entitu: seznam s filtrací, řazením a stránkováním, detail, vytvoření, úprava, smazání, vyhledávání, procvičování.
- Perzistence přes Entity Framework Core s migracemi (alespoň InitialMigration). Nebo perzistence pomocí in-memory storage.
- Testy všech endpointů v rozsahu, který ověří správnost API, spustitelné lokálně i v Azure DevOps.
- CI (build + testy) a CD s automatizovaným nasazením do Azure z Azure DevOps (viz [Nasazení do Azure](#nasazení-do-azure)).

Hodnotíme:
- logický návrh tříd a splnění funkcionality
- perzistenci dat a využití Entity Framework Core/in-memory storage
- využití abstrakce, zapouzdření, polymorfismu
- validaci vstupů, řešení chybových stavů, správné návratové status kódy 
- čistotu kódu
- verzování v GITu po logických částech
- testy
- CI + CD do Azure
- rozšíření datového návrhu nad rámec zadání (bonusové body, přiznávají se až u obhajoby, pokud je rozšíření kompletně implementováno)

---

## Fáze 2 – Web a obhajoba (50 bodů)
Vytvořte Blazor WebAssembly aplikaci napojenou na API z fáze 1 a doplňte uživatelské role. Fázi uzavírá [obhajoba](#obhajoba).

Požadavky:
- Pohledy dle [Základní funkcionality](#základní-funkcionalita) pro každou entitu.
- Uživatelské role dle zadání.
- Nasazení webu do Azure vedle API.
- Oprava chyb a připomínek z hodnocení fáze 1.

Hodnotíme:
- zapracování připomínek z fáze 1
- funkčnost celé aplikace a zobrazení informací dle zadání
- práci s uživatelskými rolemi
- čistotu kódu a validaci vstupů
- funkčnost testů a CI/CD
- grafické zpracování a UX nad rámec zadání (bonusové body)

---

## Obhajoba
Obhajoba uzavírá fázi 2 a její výsledek je hodnocením této fáze. Probíhá na konci semestru, termíny vyhlásíme v IS.

- Dostaví se **celý tým**; výjimkou je řádně omluvená nepřítomnost dle studijního řádu.
- Část členů může být v nutném případě online, alespoň jeden člen je fyzicky přítomný. Za připojení a technické potíže odpovídá tým.
- Obhajobu vede **náhodně vybraný člen týmu**.
- Prezentace není nutná. Předvedete funkčnost aplikace dle zadání, následuje technická rozprava nad kódem: funkce jednotlivých tříd a důvody jejich členění.

---

## Správa projektu – Azure DevOps
Kód sdílejte v GITu v Azure DevOps.

- Organizaci pojmenujte `iw5-2026-team-xlogin00` (xlogin00 je login vedoucího týmu z názvu týmu ve VUT IS) a projekt `project`, tj. `https://dev.azure.com/iw5-2026-team-xlogin00/project`.
- Nastavte **Work item process** na **Scrum**.
- Nastavte CI tak, aby při pushnutí do libovolné větve proběhl build a testy. Nenechávejte to na poslední chvíli, aktivace CI runnerů může vyžadovat formulář a trvat několik dní.

> :warning: **Velmi důležité upozornění**  
> Přidejte do projektu vyučující účet **uciteliw5@vutbr.cz**, a to jako posledního člena, s oprávněním **Stakeholder**.  
> Zároveň jej přidejte do **Project Collection Administrator** v nastavení organizace.  
> Bez splnění obou bodů není možné projekt hodnotit.

Návody a odkazy:
- [Přidání člena projektu](https://docs.microsoft.com/en-us/vsts/accounts/add-team-members-vs)
- [Scrum workflow](https://docs.microsoft.com/en-us/azure/devops/boards/work-items/guidance/scrum-process-workflow?view=azure-devops)
- [Burndown chart](https://docs.microsoft.com/en-us/azure/devops/report/sql-reports/sprint-burndown-scrum?view=azure-devops-2019&viewFallbackFrom=azure-devops-2019)
- [Azure Pipelines video](https://www.youtube.com/watch?v=yr6PJxfACNc)

---

## Nasazení do Azure
- Nasaďte všechny části řešení do Azure a nasazování automatizujte z Azure DevOps.
- Weby, databázi a další zdroje pojmenujte podle schématu z 1. přednášky.
- Ke všem zdrojům přiřaďte přístup účtu **uciteliw5@vutbr.cz** dle pokynů v 1. přednášce.

---

## Konvence
- Identifikátory, třídy a komentáře pojmenovávejte anglicky.
- Dodržujte zásady Clean Code.
- Používejte `.editorconfig` dle domluvy v týmu.

---

## Doporučení (bonusové body)
- Využijte Scrum v Azure DevOps: sprinty na jednotlivé fáze, práci rozdělenou na PBI, Tasks a Bugs, Boards a Burndown chart.
