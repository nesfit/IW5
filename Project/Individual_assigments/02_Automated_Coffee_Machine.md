# Automated Coffee Machine
* Cílem je vytvořit HW prototyp a Webovou aplikaci, která zajistí automatizovanou přípravu kávy a účtování

## HW prototyp
* Ctečka RFID karet na ESP32 nebo Raspberry PI, Orange Pi Zero... 
* Ovládání automatického espressa pomocí GPIO pinů ... spuštění přípravy kávy
* Možné rozšíření o nastavení množství kávy a vody
* Zalogování akce uvaření kávy do webové aplikace

## Webová aplikace
* REST API pro komunikaci s HW prototypem
* Jednoduchá webová aplikace zobrazující uživatele a vypité kávy
* Možnost přiřazení identity uživatele k RFID identifikátorům - jeden uživatel může mít více RFID identifikátorů
* Administrativní rozhraní pro vytvoření vyúčtování za předchozí období - zadání množství spotřebované kávy, ceny
* Historie jednotlivých období, historie počtu vypitých káv pro uživatele a další statistiky
* Aplikace v ASP.NET Core

## Kontakt
* Pro více informací napište na ipluskal@fit.vutbr.cz nebo se zastavte kancelář C304.
