# Continuous Integration

A Continuous Integration-hoz Github Actions-t használtunk. Mivel a projekt .NET Framework-öt követel meg, ezért egy Windows gépen build-eljülk és teszteljük.

![image-20210515144946998](C:\Users\Kristof\AppData\Roaming\Typora\typora-user-images\image-20210515144946998.png)

Az eredeti projektben lévő tesztek között voltak olyan tesztesetek, amik a hálózat viszonyok miatt néha sikeresen lefutottak, néha nem.

![WebException](C:\Users\Kristof\Documents\GitHub\BME\IET\iet-hf2021-kaposzta\doc\Continuous Integration\WebException.png)

Ezek mellett voltak olyanok, amik nem megfelelően használták a saját szolgáltatásukat (Már deklarált változót használnak a teszteléshez, ami kivételhez vezetett).

Illetve olyanok, amik nem érvényes URI-ket használtak (lehetséges, hogy az eredeti projekt tesztelése óta váltak érvénytelenné).

![WrongURI](C:\Users\Kristof\Documents\GitHub\BME\IET\iet-hf2021-kaposzta\doc\Continuous Integration\WrongURI.png)