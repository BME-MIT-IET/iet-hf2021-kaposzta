# Continuous Integration

A Continuous Integration-hoz Github Actions-t használtunk. Mivel a projekt .NET Framework-öt követel meg, ezért egy Windows gépen build-eljülk és teszteljük.

![image-20210515144946998](C:\Users\Kristof\AppData\Roaming\Typora\typora-user-images\image-20210515144946998.png)

A Continuous Integration beüzemelése során feltűnt, hogy vannak tesztek, amik sikertelenül futnak le. A legnagyobb problémát az okozta, hogy egyes tesztek egyszer lefutottak sikeresen, egyszer sikertelenül anélkül, hogy a teszteken változtattunk volna. Miután jobban utána néztem nem találtam egyértelmű problémát, ami megmagyarázhatta volna, hogy mi okozhatta ezeket. Egy pár teszt hálózat hibák miatt nem futottak le, arra lehet gondolni, hogy a teszteket nem olyan teljesítményű gépen írtak, mint amit én használtam a teszthez és ez okozhatott olyan problémát, hogy a szerver egyszer nem volt képes kiszolgálni a kéréseket, egyszer képes volt.

![WebException](C:\Users\Kristof\Documents\GitHub\BME\IET\iet-hf2021-kaposzta\doc\Continuous Integration\WebException.png)

Ezek mellett voltak olyanok, amik nem megfelelően használták a saját szolgáltatásukat (Már deklarált változót használnak a teszteléshez, ami kivételhez vezetett).

Illetve olyanok, amik nem érvényes URI-ket használtak (lehetséges, hogy az eredeti projekt tesztelése óta váltak érvénytelenné).

![WrongURI](C:\Users\Kristof\Documents\GitHub\BME\IET\iet-hf2021-kaposzta\doc\Continuous Integration\WrongURI.png)