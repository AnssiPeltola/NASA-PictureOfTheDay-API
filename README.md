# NASA-PictureOfTheDay-API

Ohjelmalla voidaan ladata NASAn päivän kuva tietokoneelle käyttäen NASAn APOD (Astronomy picture of the day) open APIa.

Ohjelma kysyy käyttäjältään haluaako ladata tämän päivän, eilisen vai satunnaisen päivän kuvan.
Ensin ohjelma lataa kuvan binaaridatan tietokoneelle .txt tiedostoon, josta se etsii kuvan URLin.
Kuvan tiedoissa on kaksi URLia, ensinmäinen on suurempi kokoinen kuva ja toinen pienempi kokoinen kuva.

Tähän versioon on kuvien lataus kansioksi määritetty käyttäjän "Anssi" työpöytä. Jos siis haluaa sitä itse kokeilla, joutuu sen määrittää uudelleen.

Parannuksina pitää vielä tehdä muutamille kohdille try-catch lausunnot.
