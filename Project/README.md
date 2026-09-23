# IW5 projekt

## TL;DR
- Téma: webová aplikace pro správu domácích zásob.
- Povinné entity: Položka, Místo, Uživatel, Nákupní seznam.
- Povinné operace: CRUD nad všemi entitami, seznamy s filtrací, řazením a stránkováním, textové vyhledávání, přehled zásob po místech, přidání položky do nákupního seznamu.
- Perzistence: relační databáze přes Entity Framework Core (doporučujeme SQLite, pro zájemce Azure SQL Database). In-memory úložiště není povoleno.
- Architektura: více projektů a vrstev. Jediný projekt je neakceptovatelný.
- Platforma: .NET 10 (LTS).
- Týmy po 3 studentech, kód v Azure DevOps, nasazení do Azure.
- Fáze 1 (50 bodů): Web API s OpenAPI/Swagger, testy, CI + CD. Odevzdává se, hodnotí se poslední commit pushnutý do `main` před termínem.
- Fáze 2 (50 bodů): Blazor WebAssembly frontend napojený na API, uživatelské role vynucené v API. Neodevzdává se, hodnotí se při obhajobě.
- Pro absolvování musí být obě fáze hodnoceny alespoň 1 bodem.
- Do Azure DevOps přidejte účet **uciteliw5@vutbr.cz** (viz [Správa projektu](#správa-projektu--azure-devops)), jinak projekt nelze hodnotit.

---

## Cíl
Cílem je vytvořit použitelnou a snadno rozšiřitelnou aplikaci, která splňuje požadavky zadání. Aplikace nesmí padat ani zamrzávat a na chybný vstup uživatele musí upozornit validační hláškou.

Zadání ponechává volnost pro vlastní realizaci. Důraz je kladen na technické zpracování a kvalitu kódu, hodnotí se ale i použitelnost. Pište aplikaci tak, abyste ji sami chtěli používat. Chybějící funkcionalitu můžete doplnit, rozšíření zdokumentujte v `README.md` vašeho repozitáře.

---

## Téma projektu
Webová aplikace pro správu domácích zásob, například potravin nebo čisticích prostředků. Uživatel eviduje položky a místa, kde jsou uložené (lednice, spíž, skříňka…), a sleduje zásoby na jednotlivých místech i v celé domácnosti. Docházející položky může rovnou přidat do nákupního seznamu. Jak se téma promítá do povinné funkcionality, popisuje sekce [Zásoby a nákup](#zásoby-a-nákup).

Možná rozšíření:
- organizace/filtrování míst dle místností (lednice, mrazák, skříňka – všechny můžou být v kuchyni)
- podpora pro víc domácností
- nahrávání obrázků
- notifikace na blížící se datum spotřeby
- automatické určení docházejících položek (např. podle minimálního množství)

---

## Data a entity
Požadujeme minimálně následující údaje. Zvažte, co je třeba ukládat a co lze dopočítat při dotazování.

### Položka
- Název
- Obrázek (postačí URL)
- Množství
- [Datum spotřeby] (drogistické zboží jej obvykle nemá)
- Kategorie (potraviny, drogerie…)
- [Doplňující popis]
- (Uživatel – autor)

Položka představuje zásobu uloženou na některém místě. Zda stejné zboží na více místech nebo s různým datem spotřeby evidujete jako více položek, nebo zavedete samostatné entity (např. katalog zboží a zásoby), je na vás. Zvolený návrh popište v `README.md`.

### Místo
- Název
- Obrázek (postačí URL)
- [Doplňující popis]
- (Položky)
- (Uživatel – autor)

### Uživatel
- Jméno
- Fotografie (postačí URL)
- Role

### Nákupní seznam
- Název
- Obchod
- [Doplňující popis]
- (Položky)
- (Uživatel – autor)

Poznámky:
- `()` označuje povinné vazby mezi entitami. Bez nich nelze sledovat zásoby na místech, sestavit nákupní seznam ani uplatnit uživatelské role. Kardinalitu vazeb a způsob jejich realizace navrhněte sami.
- `[]` označuje volitelné údaje, které nemusí být vyplněné u každého záznamu.
- Vazba mezi nákupním seznamem a položkami může nést vlastní údaje (např. množství k nákupu, příznak „koupeno“). Taková vazební entita nemusí mít vlastní seznam ani detail.

---

## Důležitá upozornění
- Pro absolvování předmětu musí být každá fáze hodnocena alespoň 1 bodem. Fázi 2 hodnotíme při obhajobě, a pokud aplikace nepředvede základní funkcionalitu dle zadání, je hodnocena 0 body. **Nespokojíme se s nedokončeným projektem.** V minulosti se stalo, že týmy, které za fázi 1 získaly 50 bodů, přestaly na projektu pracovat a u obhajoby neuspěly.
- Uživatelské role se hodnotí až ve fázi 2. Ve fázi 1 API nemusí omezovat akce podle uživatele, entita Uživatel a její data ale musí existovat. Autora záznamu ukládejte už ve fázi 1 (ve fázi 1 jej lze předávat v požadavku, ve fázi 2 jej API určuje podle přihlášeného uživatele).

---

## Základní funkcionalita

### Pohledy
Pro každou entitu aplikace obsahuje:
- **Seznam** – stránkovaný přehled záznamů s filtrací a řazením, s odkazem na detail a na vytvoření nového záznamu.
- **Detail** – všechny informace o záznamu.
- **Vytvoření a editace** – buď na stránce detailu, nebo na samostatné stránce.
- **Smazání.**

Pokud pro některou entitu seznam nedává smysl, rozhodnutí zdůvodněte v `README.md`.

### Vyhledávání
Textové vyhledávání (stačí hledání podřetězce) minimálně v těchto údajích:
- Položka – název a doplňující popis
- Místo – název a doplňující popis
- Uživatel – jméno
- Nákupní seznam – název a doplňující popis

Doporučujeme, aby vyhledávání nerozlišovalo velká a malá písmena (pozor: v SQLite převádí funkce `lower()` na malá písmena jen znaky ASCII a operátor `LIKE` jen u nich ignoruje velikost písmen, takže „Šunka“ a „šunka“ bez dalších úprav nesplynou).

### Zásoby a nákup
Téma se v základní funkcionalitě promítá takto:
- Detail místa zobrazuje položky, které jsou na něm uložené, včetně jejich množství.
- Seznam položek slouží jako přehled zásob celé domácnosti a lze jej filtrovat podle místa.
- Položku lze přidat do nákupního seznamu, a to i do seznamu, který vytvořil jiný uživatel. Stačí, když to umožní stránka nákupního seznamu; akce přímo z detailu položky je vítaná.
- O tom, že položka dochází, rozhoduje uživatel. Automatické určování docházejících položek patří mezi možná rozšíření.

### Uživatelské role
Minimálně role **uživatel** a **administrátor**. Domácnost je sdílená.

Uživatel může:
- prohlížet všechny záznamy
- vytvářet položky, místa a nákupní seznamy
- u libovolné položky měnit množství a místo uložení; v libovolném nákupním seznamu přidávat a odebírat položky a měnit údaje vazby (např. množství k nákupu, příznak „koupeno“)
- ostatní údaje editovat a záznamy mazat jen u položek, míst a nákupních seznamů, které vytvořil
- upravit svůj profil (jméno, fotografii), nikoli však svou roli

Administrátor může:
- vytvářet, editovat a mazat libovolné položky, místa a nákupní seznamy
- vytvářet, editovat a mazat uživatele a měnit jejich role (při použití externího poskytovatele identit tím rozumíme založení či odstranění uživatelského profilu v aplikaci a přiřazení role)

Oprávnění musí vynucovat API, ne pouze web. Endpointy, které mění data, vyžadují přihlášeného uživatele: nepřihlášenému vracejí 401 Unauthorized, přihlášenému bez dostatečného oprávnění 403 Forbidden. Autora nového záznamu určuje API podle přihlášeného uživatele, ne podle údaje v požadavku. Samotné skrytí ovládacích prvků ve webu nestačí.

Přihlašování řešte tak, jak bude ukázáno v předmětu. V rámci tématu Identity management probereme externí poskytovatele identit (identity providers), lokální uživatelské účty i kombinaci obou přístupů. Zvolte alespoň jeden z nich a při obhajobě s ním předveďte práci s uživatelskými účty a rolemi.

### Perzistence
- Data ukládejte perzistentně: použijte Entity Framework Core (Code First, migrace) a relační databázi. Data musí přežít restart i nové nasazení aplikace.
- Doporučujeme SQLite. Soubor databáze v Azure App Service uložte mimo složku nasazené aplikace (např. do `/home/data`), jinak jej nové nasazení přepíše nebo nebude zapisovatelný.
- Zájemci mohou místo SQLite použít nativní databázi v Azure, např. Azure SQL Database.
- In-memory úložiště (vlastní kolekce v paměti ani poskytovatel EF Core InMemory) v aplikaci použít nelze. Nedoporučujeme ho ani v testech, protože nekontroluje integritu cizích klíčů; i pro testy použijte SQLite.
- API musí vracet správné chybové stavy a srozumitelné hlášky alespoň pro neexistující záznam (404), odkaz na neexistující entitu (400) a smazání záznamu, na který vedou vazby (409, nebo kaskádové mazání popsané v `README.md`). Kaskádové mazání nesmí odstranit položky, místa ani nákupní seznamy, které by uživatel podle [uživatelských rolí](#uživatelské-role) nesměl smazat sám; v takovém případě vraťte 409. Co se stane se záznamy smazaného uživatele (např. převedou se na administrátora), navrhněte sami a popište v `README.md`.
- Filtrace, řazení, vyhledávání a stránkování probíhají na serveru (API), ne ve webu. Provádějte je v databázovém dotazu (nad `IQueryable`), ne až po načtení celé tabulky do paměti.

---

## Architektura projektu
Ve výuce ukazujeme rozdělení kódu do logických vrstev a projektů s využitím návrhových vzorů a vysvětlujeme proč. Obdobné, logicky členěné rozvržení chceme i po vás. Uspořádání vzorového projektu ze cvičení můžete převzít bez ztráty bodů. Pokud uspořádání upravíte (např. jinak rozdělíte projekty nebo vrstvy), musíte umět změny zdůvodnit.

Řešení musí obsahovat více projektů a vrstev (např. API, BL, DAL, Web, Common). Řešení s jediným projektem není akceptovatelné. Architektura se hodnotí v obou fázích. Zásadně odlišnou architekturu (např. Clean Architecture, vertical slices) předem konzultujte.

---

## Odevzdávání
Projekt řešíte v týmech po **3 studentech**. Má 2 fáze, které na sebe navazují. Odevzdává se pouze **fáze 1**, a to pushnutím do větve `main` před termínem, který vyhlásíme v IS. **Fáze 2** se hodnotí při obhajobě.

Kontroluje se kód ve větvi `main` v Azure DevOps. Rozhoduje čas pushnutí do Azure Repos (*Repos → Pushes*), ne datum commitu uvedené v Gitu.
- Fáze 1 – poslední commit pushnutý do `main` před termínem odevzdání. Otagujte jej `review1` a tag pushněte (`git push origin review1`).
- Fáze 2 – poslední commit pushnutý do `main` do konce dne před obhajobou. Otagujte jej `final` a tag pushněte.
- Tagy pushněte nejpozději v termínu a poté je neměňte. Pokud tag chybí nebo ukazuje na jiný commit, hodnotíme commit určený podle času pushnutí. Kde dále píšeme `review1` nebo `final`, myslíme tím vždy takto určený hodnocený commit fáze 1, resp. fáze 2.
- Funkčnost a kvalitu kódu z pozdějších commitů a z jiných větví nehodnotíme; historii větví posuzujeme jen u týmové spolupráce.

Funkčnost API ve fázi 1 posuzujeme na commitu `review1`, ne na aktuálně nasazené verzi. CI a CD fáze 1 posuzujeme podle běhů pipeline (build, testy i nasazení) pro tento commit. Tyto běhy označte k trvalému uchování (u běhu *More actions → Retain*), jinak je retenční politika smaže. U obhajoby předvádíte verzi nasazenou v Azure z commitu `final`; po něm už do `main` nic nepushujte ani neslučujte, jinak CD nasadí novější verzi.

Je povoleno:
- použít libovolnou knihovnu z NuGetu,
- převzít kód z libovolného zdroje včetně nástrojů AI (ChatGPT, Copilot aj.) a ze vzorového projektu ze cvičení.

Není povoleno převzít kód z projektů ostatních týmů.

Pro převzatý kód platí:
- V `README.md` stručně popište, jak jste při řešení použili nástroje AI, a uveďte, odkud jste převzali další kód. Jednotlivé části kódu označovat nemusíte.
- Převzatému kódu musíte rozumět a umět jej u obhajoby vysvětlit.
- Ověřte, že převzatý kód ani použité knihovny neporušují licence.

Týmová spolupráce:
- Z Gitu musí být viditelná postupná práce a spolupráce týmu. Posuzujeme ji podle celé historie repozitáře v Azure DevOps včetně větví a pull requestů. Pokud slučujete přes squash, zdrojové větve nemažte (při dokončení pull requestu zrušte volbu *Delete … after merging*). Ke snížení hodnocení může vést malý počet commitů nebo nelogické commity v historii repozitáře a také commity s nepřeložitelným kódem ve větvi `main`; v rozpracovaných větvích nepřeložitelný kód nevadí.
- Doporučujeme Conventional Commits a promyšlenou strategii větvení v Gitu (viz [srovnání strategií větvení](https://medium.com/@sreekanth.thummala/choosing-the-right-git-branching-strategy-a-comparative-analysis-f5e635443423)).
- Pokud členové nepřispěli rovnoměrně, přidejte do kořene repozitáře soubor `ROZDELENI.txt` s jedním řádkem na člena ve tvaru `xlogin00: 40` (součet musí být 100). Body člena za fázi = body týmu za fázi × počet členů týmu × podíl / 100; součet za obě fáze je i tak nejvýše 100. Pro fázi 1 platí verze souboru v commitu `review1`, pro fázi 2 verze v commitu `final`. Pokud soubor chybí nebo je nesrozumitelný, počítáme s rovnoměrným rozdělením bodů. Při sporu mezi členy o rozdělení rozhodne vyučující podle historie Gitu a průběhu obhajoby.

Projekt v průběhu semestru konzultujte po přednášce nebo cvičení, předejdete tím komplikacím při odevzdání.

---

## Fáze 1 – API (50 bodů)
Vytvořte spustitelnou Web API službu se specifikací OpenAPI (verzi necháme na vás) a Swagger UI. Vzorové API bude ukázáno na přednáškách a cvičeních.

Požadavky:
- Endpointy pokrývající celou [základní funkcionalitu](#základní-funkcionalita): pro každou entitu seznam s filtrací, řazením a stránkováním, detail, vytvoření, úpravu, smazání a vyhledávání, dále funkce ze sekce [Zásoby a nákup](#zásoby-a-nákup).
- Perzistence přes Entity Framework Core s migracemi (alespoň InitialMigration) do relační databáze (viz [Perzistence](#perzistence)).
- Testy všech endpointů v rozsahu, který ověří správnost API; testy musí být spustitelné lokálně i v Azure DevOps.
- CI (build + testy) a CD s automatizovaným nasazením do Azure z Azure DevOps (viz [Nasazení do Azure](#nasazení-do-azure)).

Hodnotíme:
- logický návrh tříd a splnění funkcionality
- perzistenci dat a využití Entity Framework Core
- využití abstrakce, zapouzdření, polymorfismu
- validaci vstupů, řešení chybových stavů, správné návratové status kódy
- čistotu kódu
- verzování v Gitu po logických částech
- testy
- CI + CD do Azure
- rozšíření datového návrhu nad rámec zadání (bonusové body, přiznávají se až u obhajoby, viz [Bonusové body](#bonusové-body))

---

## Fáze 2 – Web a obhajoba (50 bodů)
Vytvořte Blazor WebAssembly aplikaci napojenou na API z fáze 1 a doplňte uživatelské role. Web čte a zapisuje data výhradně voláním API. Fázi uzavírá [obhajoba](#obhajoba).

Požadavky:
- Pohledy podle [základní funkcionality](#základní-funkcionalita) pro každou entitu a funkce ze sekce [Zásoby a nákup](#zásoby-a-nákup).
- Uživatelské role podle zadání, vynucené na straně API (viz [Uživatelské role](#uživatelské-role)).
- Nasazení webu do Azure vedle API.
- Oprava chyb a zapracování připomínek z hodnocení fáze 1.

Hodnotíme:
- zapracování připomínek z fáze 1
- funkčnost celé aplikace a zobrazení informací dle zadání
- práci s uživatelskými rolemi
- architekturu a členění řešení do vrstev a projektů
- čistotu kódu a validaci vstupů
- funkčnost testů a CI/CD
- grafické zpracování a UX nad rámec zadání a využití Scrumu (bonusové body, viz [Bonusové body](#bonusové-body))

---

## Obhajoba
Obhajoba uzavírá fázi 2 a její výsledek je hodnocením této fáze. Probíhá na konci semestru, termíny vyhlásíme v IS.

- Dostaví se **celý tým**. Člen, který se bez řádné omluvy dle studijního řádu nedostaví, je za fázi 2 hodnocen 0 body. Řádně omluvený člen obhajuje v náhradním termínu po domluvě s vyučujícím, a to nad stejným commitem `final` a stejnou nasazenou verzí jako zbytek týmu; do té doby nasazení neměňte a zdroje nemažte.
- Online účast části členů je možná jen po předchozí domluvě s vyučujícím; alespoň jeden člen je fyzicky přítomný. Za připojení a technické potíže odpovídá tým.
- Obhajobu vede člen týmu, kterého vyučující náhodně vybere ze všech zúčastněných členů, a to včetně těch, kteří jsou připojeni online.
- Prezentace není nutná. Předvedete funkčnost aplikace nasazené v Azure, následuje technická rozprava nad kódem: funkce jednotlivých tříd a důvody jejich členění.
- Fázi 2 hodnotíme jako tým, body mezi členy rozdělujeme podle `ROZDELENI.txt`. Členovi, který u obhajoby neprokáže porozumění kódu, můžeme hodnocení individuálně snížit.

---

## Správa projektu – Azure DevOps
Kód sdílejte v repozitáři Git v Azure DevOps.

- Organizaci pojmenujte `iw5-2026-team-xlogin00` (xlogin00 je login vedoucího týmu z názvu týmu ve VUT IS) a projekt `project`, tj. `https://dev.azure.com/iw5-2026-team-xlogin00/project`.
- Nastavte **Work item process** na **Scrum**.
- Nastavte CI tak, aby při pushnutí do libovolné větve proběhl build a testy. Nenechávejte to na poslední chvíli:
  - Bezplatný paralelní job na agentech hostovaných Microsoftem se v privátním projektu aktivuje až po propojení organizace s předplatným Azure (*Organization settings → Billing → Set up billing*). Předplatné musí být ve stejném Microsoft Entra ID jako organizace a Azure Free Trial nelze použít. Viz [Configure and pay for parallel jobs](https://learn.microsoft.com/en-us/azure/devops/pipelines/licensing/concurrent-jobs?view=azure-devops).
  - Pokud pipeline přesto hlásí „No hosted parallelism has been purchased or granted“, požádejte o bezplatný job [formulářem](https://aka.ms/azpipelines-parallelism-request). Vyřízení trvá několik pracovních dní.
  - Alternativou je vlastní (self-hosted) agent, jeden paralelní job je pro něj zdarma.
  - Nedostupnost agenta není důvodem k prodloužení termínu.

> :warning: **Velmi důležité upozornění**  
> Nejpozději do termínu odevzdání fáze 1 přidejte do organizace účet vyučujících **uciteliw5@vutbr.cz** (*Organization settings → Users → Add users*), a to jako posledního člena, s úrovní přístupu (*Access level*) **Stakeholder** a s přístupem do projektu `project`.  
> Zároveň jej přidejte do skupiny **Project Collection Administrators** (*Organization settings → Permissions*).  
> Bez splnění obou bodů není možné projekt hodnotit.

Návody a odkazy:
- [Přidání uživatelů do organizace a nastavení úrovně přístupu](https://learn.microsoft.com/en-us/azure/devops/organizations/accounts/add-organization-users?view=azure-devops)
- [Scrum workflow](https://learn.microsoft.com/en-us/azure/devops/boards/work-items/guidance/scrum-process-workflow?view=azure-devops)
- [Sprint burndown](https://learn.microsoft.com/en-us/azure/devops/report/dashboards/configure-sprint-burndown?view=azure-devops)
- [Vytvoření první pipeline](https://learn.microsoft.com/en-us/azure/devops/pipelines/create-first-pipeline?view=azure-devops)

---

## Nasazení do Azure
- Nasaďte všechny části řešení do Azure a nasazování automatizujte z Azure DevOps.
- Weby, databázi a další zdroje pojmenujte podle schématu z 1. přednášky.
- Ke všem zdrojům přiřaďte přístup účtu **uciteliw5@vutbr.cz** dle pokynů v 1. přednášce: ke zdrojům fáze 1 nejpozději do termínu jejího odevzdání, ke zdrojům vytvořeným později (např. pro web) hned po jejich vytvoření.
- Adresy nasazeného API (Swagger UI) a webu uveďte v `README.md` repozitáře.
- Nasazené API musí být dostupné od termínu odevzdání fáze 1 do zveřejnění jejího hodnocení, API i web pak v den obhajoby. Zdroje nemažte dříve, než bude hodnocení fáze 2 zapsané v IS. Předplatné a úrovně služeb volte podle pokynů v 1. přednášce tak, aby vám kredit vystačil do konce semestru.

---

## Konvence
- Identifikátory (názvy tříd, metod, proměnných apod.) volte anglicky; anglicky pište i komentáře.
- Používejte .NET 10 (LTS); verzi SDK doporučujeme zafixovat souborem `global.json` v kořeni repozitáře.
- Dodržujte zásady Clean Code.
- Používejte `.editorconfig` podle domluvy v týmu.

---

## Bonusové body
Bonusové body přiznáváme až u obhajoby. Připočítáváme je k fázi, ke které patří. Fáze tak může s bonusem přesáhnout 50 bodů, součet za obě fáze včetně bonusů je ale nejvýše 100. Bonusové body nenahrazují chybějící základní funkcionalitu.

Fáze 1:
- rozšíření datového návrhu nebo funkcionality nad rámec zadání (např. z výčtu „Možná rozšíření“ v sekci [Téma projektu](#téma-projektu)); uznáváme jen rozšíření kompletně implementované v API i ve webu a zdokumentované v `README.md` vašeho repozitáře

Fáze 2:
- grafické zpracování a UX nad rámec zadání
- využití Scrumu v Azure DevOps: sprinty pro jednotlivé fáze; práce rozdělená do work items typu PBI, Task a Bug; nástěnky (Boards); Burndown chart
