# IW5 závěrečný dotazník po ročníku 2024/2025

Celkový počet studentů v předmětu: ??? <br>
Počet odpovědí v dotazníku: 20

## Přednášky

## Projekt

## Celkové hodnocení předmětu

## Textové odpovědi

### Nějaké další poznámky k přednáškám.
- Přednášky byly fajn, většinou jsem je sledoval zpětně online. Jediná výhrada asi je, že někdy se zašlo příliš rychle do detailu s ztratil jsem jakýsi “big picture” nadhled nad tim, co se vlastně teď probírá a k čemu bych to mohl využít.
- pacia sa mi zive priklady z praxe napr o odstraneni scrutoru pre startup performance
- Extrémne by sa hodila prednáška/cvičenie na setup automatického deployu do Azure (cez písaný skript nie cez webové GUI). Automatizovaný deploy do cloudových služieb sa nepreberá ale hlavne prakticky neukazuje na žiadnom povinnom predmete.
- Více podrobností o identity serveru na straně Webu (hlavně správa rolí). Více přednášek o využití umělý inteligence pro vývoj a setup takového prostředí (rady tipy a triky co dělat). Více podrobností o nasazování na Azure a co všechno Azure nabízí za servici, jaké možností nabízí, rady jak debugovat v produkci (zařadit do projektu logování v produkci).
- Přístup k výuce byl super, vyučující ochotně zodpovídali dotazy nejen ohledně probírané látky. Obecně se učili praktické věci, což se mi líbilo. 
- Nenapadá mě nic co vytknout, přednášky byly fajn
- Trochu viac pouzivat tabulu a hlavne na zaciatku si nakreslit kompletnu schemu architektury (server aj client a jednotlive vrstvy), poukazat co bude ine voci ICS. Podobne aj v ICS. Nakreslit to komplet po urovniach, ukazat kde co s cim komunikuje, repa, UoW.  Nasledne vzdy ked sa nieco prebera fyzicky ukazat teraz sme v tejto casti... Akoze na nejakom slide to urcite bolo, ale mat to fyzicky na ociach nonstop by bolo lepsie.
Roman hovoril, ze sa to snad dalsi rok zmeni, ale teda zjednotit DAL medzi ICS a IW5 (chybajuci UoW). Automappery nam prisli velmi zmatocne, dokonca niektore uplne nevyuzite. V ICS bol mapper z entity na entitu pouzivany pri update operaciach na urovni repositarov. Tu zrejme mali rovnaku ulohu plnit tie mappery v suboroch s entitami. Avsak nasledne v DAL.EF.Repositories/RepoBase je update napisany tak ze sa hadam ten mapper ani nepouzije?
Skoda ze predmet IIS sa nevyucuje o semester skor, REST sa tam prebera az nakonci a to ho uz clovek davno vie vdaka tomuto predmetu. Len tu je to trochu speedrun, ale tak s tym nie je moc co robit...
Neviem s akymi inymi temami sa medzirocne strieda zadanie, ale tieto formulare sme zhodnotili ako dost narocne pre zaciatocnikov a spalili sme vela casu navrhovanim vhodnej schemy, pricom stale neviem ci sme to urobili dobre z databazoveho pohladu. Pre zaciatocny projekt by bolo mozno vhodne nieco menej abstraktne, nejaky informacny system alebo tak. Pripadne mozno aspon na konci semestra zhodnotit ako to spravne malo byt navrhnute? Ako priradovat vyplnene dotazniky od uzivatelov k prazdnemu dotazniku? Mat na to rozne entity? Ako sa spravne mali riesit rozne moznosti odpovedi (textova, slidrova, checkboxova)
Inak skvely predmet aj ked dost tazky ak jediny podobny projek predtym bol ICS. Prve odovzdania preto dopadli horsie, cez Vianoce som si ale napisal treti takyto projekt a uz to davalo vsetko vacsi zmysel, do buducna to bude urcite este lepsie. Fakt spolu s ICS najlepsi predmet na skole, dufam, ze to tu neznelo moc kriticky, su to len detaily, vdaka celemu teamu vyucujucich, aj ked s horsou znamkou odnasam si z tohoto predmetu asi najviac za studium.

### Měli byste nějaký nápad na téma zadání pro budoucí ročníky?
- Něco okolo jízdních řádů, něco jako jednoduchej idos
- Task manager, kalendář, sociální sit(obdoba fitstagram v IIS), účetní systém(správa faktur, účtenek, zaměstnanců), rezervační systémy - kino, divadlo, restaurace(+obsluha v rámci restaurace), svatební aplikace (správa hostů, formulář, galerie od uživatelů, notifikace), aplikace na různý tracking - posilovna, běh (Strava), lezení
- Na kurzu IIS jsme vytvářeli IS pro zvířecí útulky - bylo to velmi zajímavé a stáli jsme před výzvou, jak ho vylepšit. 
- Nahravanie suborov napr obrazkov do aplikacie aj zo zariadenia aj pomocou url. Url je prilis jednoduche, toto by malo byt povinne v kazdom projekte bez ohladu na temu. Implementacia AI kludne aj 3strany alebo vyuzitie Azure OpenAI na zakladne rozpoznavanie fotky, textu, zvuku...
- Online cestovný poriadok alebo evidencia lístkov/šalinkariet, napr. Brno MHD
- Plánovač dovolených. Uživatel by si mohl vytvořit dovolenou a v ní jednotlivé výlety, kde by se zase mohli vytvářet jednotlivé zastávky. 
- Něco jako e-learning, kde by byly různé kurzy a různí uživatele by viděli svoje zapsané kurzy. Každý kurz by měl fórum na diskuzi, kde by mohli chatovat.
- jednoduchý e-shop

### Co byste na projektu vylepšili/co se vám nelíbilo?
- Mi osobně téma přišlo trochu nudné - nebaví mě formuláře
- Zda je možné použít modernější technologie, jako je react?
- Identity management zvolený pre tento projekt je neskutočný overkill a zbytočná robota naviac.
- Skoda ze ste nestihli synchronizovat cook book z ICS a IW5
- Kladl bych větší důraz na identity hned od začátku, třeba my jsme měli v projektu správu uživatelů (stylem ukládání do databáze spolu s formuláři), ale pak jsme to už nestihli propojit s identity (kvůli problémům s identity 😅), takže jsme to na konci měli samostatně. Asi nás mohlo napadnout, že identity pak uživatele vyřeší, ale bylo by fajn to na začátku říct víc explicitně.

### Co byste na předmětu změnili/vylepšili?
- Možná ta zmiňovaná změna v projektu by byla zajímavá
- Osobne by som IW5 ponechal také aké je (s drobnými zmenami), ale ICS by sa hodilo úplne prekopať. Predmet sa síce volá seminár C#, ale po dokončení mám pocit že som sa C# teda moc nenaučil. Základy sú preletené na prvých pár prednáškach a celý predmet je vlastne jedno veľké hodenie do vody. V projekte je strašne veľa nových vecí, ktoré nie je možné prejsť za jeden semester, takže výsledok je že študenti síce projekt dokončia (kopírovaním cookbooku a štýlom pokus omyl), ale nikto absolútne netuší ako to vlastne funguje. Na konci som vedel ako robiť veci ktoré odo mňa boli vyžadované, ale C# bola pre mňa stále taká istá čierna skrinka ako na začiatku semestra. 
Myslím si že by bolo oveľa vhodnejšie, keby ICS mal napríklad 2 jednoduchšie projekty, ktoré by slúžili ako "úvod", aby študenti aj pochopili čo sa naozaj deje keď spustia program. IW5 by bolo zložitejšie pokračovanie, kde by už robili 3-vrstvú architektúru, podobnú tomu s čím sa stretnú mimo školy.
Taktiež ešte neoceňujem "Clean code" prístup v obidvoch predmetoch, keďže mne aj viacerým iným príde skôr ako "clean code = žiadne komentáre". Chápem že .NET "Clean Code" konvencie sú pravdepodobne na tento spôsob (ako som videl z viacerých príkladov na Microsoft stránkach, kde je veľa "samokomentujúceho" kódu), ale osobne si myslím že viac komentárov nie je vôbec na škodu, keďže študenti pravdepodobne nemajú dlhoročné skúsenosti s C#, a aj najkrajšie napísaný C# kód aplikácie 3-vrstvej architektúry je pre nich ako čítať hieroglyfy.
- Jan Pluskal by si mal zmeniť meno na "Jan Ostrý", keďže neučí C++ ale C#
- Zverejnenie aspon orientacnych informacii o deadlinoch v IS VUT aj na gite. Kazdy rok to podla mna byva rovnake +- tyzden
- Osobně bych tento předmět rozdělelil na 2 navazující předměty. Jeden by byl v zimním semestru a druhý v letním semestru. Jeden předmět bych zaměřil víc na web a pokročilejší témata o webu a druhý bych zaměřil na API, Identity server a pokročilejší témata o backendu a Azuru. S tím že bych udělal možnost pokračovat na tom stejném projektu ze zimního předmětu do letního. Podle mě je toho na jeden předmět a jeden semestr strašně moc a byl bych radši, kdyby projekt byl jednoduší a kratší na každý semestr, ale dohromady bych na tom přes 2 semestry mohl strávit víc času s tím že by zbylo víc času na probrání zajímavostí a detailů. 
- Zkusil bych první vytvořit web a pak až API