# BattleShip
Konsolowa implementacja gry w statki.

Program jest konsolową symulacją gry w statki dla dwóch graczy. Plansza do gry posiada wymiary 10x10. Statki są ustawiane losowo.

Po uruchomieniu użytkownik jest proszony o podanie nazwy dla obu graczy. Przeprowadzona zostaje prosta walidacja, która sprawdza, czy nazwa nie jest pusta.
Następnie tworzony jest obiekt klasy Player dla obu graczy oraz odpowiednia instancja klasy BoardGame dla każdego z nich.
W momencie utworzenia obiektu BoardGame następuje wypełnienie odpowiednich plansz- jedna służąca do wyświetlenia na konsoli, druga, niewidoczna dla gracza,
zawierająca statusy wszystkich pól.
Plansza zawierająca statusy, będąca głównym narzędziem do działania programu, zostaje również wypełniona statkami na losowych miejscach.
Podczas wypełniania planszy statkami następuje sprawdzenie, czy dane pole mieści się w dopuszczalnym przedziale, nie jest obsadzone przez inny statek
oraz czy w jego najbliższym sąsiedztwie(+1,-1 w każdym kierunku) nie znajduje się inny statek. Każdy statek jest obiektem klasy Ship, 
który przechowywany jest w liście, będącej z kolei elementem klasy GameBoard. 

Grę rozpoczyna Gracz 1. Proszony jest o podanie współrzędnych X oraz Y. Współrzędne te są sprawdzane czy są wartościami numerycznymi oraz czy nie wykraczają poza
dopuszczalny obszar gry. Jeżeli pojawi się problem, użytkownik zostanie o tym poinformowany.
W przypadku podania poprawnych wartości następuje wykonanie strzału na pole o współrzędnych X oraz Y. Sprawdzony zostanie status pola o podanych współrzędnych
i w zależności od tego statusu zostanie wykonana odpowiednia czynność- trafienie, pudło, zatopienie statku. 
Jeżeli użytkownik ponownie wybierze pole, które wcześniej zostało już oznaczone jako pudło, lub trafienie- zostanie o tym poinformowany.

W momencie trafienia w statek sprawdzane jest czy statek został zatopiony lub trafiona została jego część. W przypadku zatopienia statku, pola wokół niego 
zostaną wypełnione statusem "pudło". Sprawdzana zostaje również ilość pozostałych statków przeciwnika. Jeżeli jest ona równa 0, wtedy odpowedni gracz zostaje 
zwycięzcą gry, a na erkanie pojawia się stosowny komunikat.

Gracz 1 wykonuje atak tak długo, aż nie nie trafi w pole oznaczone jako "pudło". W takim przypadku swoją rundę rozpoczyna gracz 2.
Cała gra toczy się do momentu, aż jeden z graczy nie zostanie pozbawiony wszystkich statków. 

W ciągu gry na ekranie wyświetlana jest wizualna plansza obu graczy, informacje o pozostałych statkach, zarówno z podziałem na kategorie oraz łączną ich ilość, 
a także informacja o tym, który gracz aktualnie wykonuje swój strzał.
