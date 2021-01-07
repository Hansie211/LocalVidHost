# Films kijken vanaf de bank
## De basis:
Er is lokaal een server, die aangesloten is op een scherm ( de TV ), en heeft een video speler open staan. Andere apparaten kunnen via een website verbinding maken met de server. Zij krijgen dan een lijst te zien met films en series die op de server staan. Ze kunnen een film selecteren om af te spelen op het scherm van de server.

## Technieken:
* De website maakt gebruik van ASP.NET Core en Blazor
* Videospeler: HTML5 video element, die via Blazor/JS wordt bestuurd vanuit Google Chrome.
* Communicatie tussen de clients en de server: SignalR
* Database: EF Core met SQLite

Om mij een zo ruim mogelijke hardware keuze te geven voor de server, moet de software op Linux werken (Debian Buster, Linux kernel 5.4). Daarom moet de code worden geschreven in .NET Core. De webbrowser zal Google Chrome zijn, geen MS Edge. Om de database zo portable mogelijk te houden, gebruik ik SQLite.

## Vereisten:
* De video speler moet langdurig draaien en bereikbaar zijn. Bij bijvoorbeeld fouten in de film moet de applicatie niet afsluiten.
* De video speler pagina zelf mag alleen te bereiken zijn vanaf de server.
* Films moeten gestart / gepauzeerd / gewisseld kunnen worden.
* Films moeten kunnen worden doorgespoeld / vooruit gesprongen.
* Wijzigingen in het afspelen moeten live geupdate worden op alle clients. 
* Het volume moet aanpasbaar zijn.
* Ondertiteling moet kunnen worden aangezet.
* Catalogus van films moet kunnen worden gefilterd.
* Als een film wordt afgebroken voordat die klaar was, moet die hervat kunnen worden vanaf dat moment.
* De database moet kunnen worden beheerd. Films moeten kunnen worden toegevoegd en verwijderd, net als ondertiteling. 
* Nieuwe films moeten worden geconverteerd naar een web-geoptimaliseerd mp4 formaat.
* Film informatie ( titel, genre, gesproken taal, … ) moet ook aan te passen zijn.
* Films kunnen ook aflevering van een serie zijn. Dan hebben ze ook een seizoen en afleveringsnummer.
* Series moeten automatisch doorgaan met de volgende aflevering.
* Een overzicht van de beschikbare ruimte op de schijf om nieuwe films toe te voegen.

## Uitbreiding ideeën:
* Persoonlijke gebruikersprofielen met favorieten, en informatie over al geziene films.
* Streamen, zodat je een film op je telefoon direct kan afspelen op het grote scherm, zonder eerst volledig up te loaden.
* Films moeten het liefst ook kunnen worden geïmporteerd vanaf een USB-stick.
