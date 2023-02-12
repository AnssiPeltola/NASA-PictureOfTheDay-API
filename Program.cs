    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    public class Program
    {
        static void Main(string[] args)
        {
            // Referenssit:
            Random rnd = new Random();
            DateTime today = DateTime.Now;
            DateTime yesterday = DateTime.Today.AddDays(-1);
            int randomYear;
            int randomMonth;
            int randomDayy;
            string pattern = @"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|jpeg|gif|png)";
            string[] imageUrls = new string[2];
            int CurrentYear = Convert.ToInt32(today.Year);
            string projectPath = Directory.GetCurrentDirectory();

            // Ohjelma alkaa ja se on käynnissä kunnes käyttäjä antaa arvon "0"
            while(true)
            {
            Console.WriteLine("Jos haluat tämän päivän kuvan. Vastaa 1");
            Console.WriteLine("Jos haluat kuvan eiliseltä päivältä. Vastaa 2");
            Console.WriteLine("Jos haluat satunnaisen kuvan väliltä 16.6.1995 - " + today.ToString("dd.MM.yyyy") + ". Vastaa 3"); 
            Console.WriteLine("Jos haluat sulkea ohjelman. Vastaa 0");
            string? vastaus = Console.ReadLine();

            // Jos vastaa 1, annetaan käyttäjälle tämän päivän kuva(t)
            if (vastaus == "1")
            {
                try
                {
                // Lataa nettisivulta tekstit tiedostoon nasa<Tämäpäivä yyyy-MM-dd>.txt
                using(WebClient client = new WebClient())
                {
                string s = client.DownloadString("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date="+ today.ToString("yyyy-MM-dd"));

                // StreamWriter kirjoittaa tekstit tiedostoon Nasa<Tämäpäivä yyyy-MM-dd>.txt
                StreamWriter writer = new StreamWriter(projectPath + "/" + "txts" + "/" + "Nasa" + today.ToString("yyyy-MM-dd") + ".txt");
                writer.Write(s);
                writer.Close();
                }

                // Lukee tiedoston nasa<Tämäpäivä yyyy-MM-dd>.txt stringiin text
                string text = File.ReadAllText(projectPath + "/" + "txts" + "/" + "Nasa" + today.ToString("yyyy-MM-dd") + ".txt");

                // Tsekkaa kaikki matchit jotka löytää tekstistä, pattern regex on määritelty referensseissä
                MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

                // Jos matches on enemmän kuin 0 niin lisää array imageUrlsiin niin monta eri arvoa kuin matches löysi matcheja
                if (matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        imageUrls[i] = matches[i].Value;
                    }
                }
                }

                // Jos try:n {} sisällä oleva koodi ei mene läpi antaa se Error viestin ja palaa while loopin alkuun
                catch (WebException ex)
                {
                    Console.WriteLine("Jotain meni pieleen! Kokeile uudestaan! " + ex.Message);
                    continue;
                }

                // Printtaa kaikki imageUrls arrayn arvot. Koodia käytetty testaukseen
                // imageUrls.ToList().ForEach(i => Console.WriteLine(i.ToString()));

                // Kysyy käyttäjältä haluaako se ladata HD- vai normaalilaatuisen kuvan tietokoneelle.
                while (true)
                {
                    Console.WriteLine("Jos haluat HD-laatuisen kuvan. Vastaa 1");
                    Console.WriteLine("Jos haluat normaali-laatuisen kuvan. Vastaa 2");
                    string? mikaKuva = Console.ReadLine();

                    // Ladataan HD-laatuinen
                    if (mikaKuva == "1")
                    {
                    try
                    {
                    using (WebClient client = new WebClient())
                    {
                    // Tekee uuden kansion Kuvat\yyyy\MM
                    Directory.CreateDirectory(projectPath + "/" + "Kuvat" + "/" + today.ToString("yyyy") + "/" + today.ToString("MM"));

                    // Määritetään ladattavan tiedoston nimi ja muoto.
                    string fileName = today.ToString("yyyy-MM-dd") + "HD" + ".jpg";

                    // Määritetään kansion sijainti minne tiedosto ladataan
                    string savePath = Path.Combine(projectPath + "/" + "Kuvat" + "/" + today.ToString("yyyy") + "/" + today.ToString("MM"), fileName);

                    // Ladataan kuva imageUrls Arrayn indeksistä 0, koska APIssa HD-laatu on aina ensinmäisenä index on 0
                    client.DownloadFile(imageUrls[0], savePath);
                    Console.WriteLine("Kuva " + fileName + " ladattu onnistuneesti!");
                    Console.WriteLine("");

                    // Katkaisee while loopin
                    break;

                    }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("Kuvan lataaminen ei onnistunut! " + ex.Message);
                    }
                    }

                    // Ladataan normaalilaatuisena
                    if (mikaKuva == "2")
                    {
                    try
                    {
                    using (WebClient client = new WebClient())
                    {
                    // Tekee uuden kansion Kuvat\yyyy\MM
                    Directory.CreateDirectory(projectPath + "/" + "Kuvat" + "/" + today.ToString("yyyy") + "/" + today.ToString("MM"));

                    // Määritetään ladattavan tiedoston nimi ja muoto.
                    string fileName = today.ToString("yyyy-MM-dd") + ".jpg";

                    // Määritetään kansion sijainti minne tiedosto ladataan
                    string savePath = Path.Combine(projectPath + "/" + "Kuvat" + "/" + today.ToString("yyyy") + "/" + today.ToString("MM"), fileName);

                    // Ladataan kuva imageUrls Arrayn indeksistä 1, koska APIssa normaali-laatu on aina toisena index on 1
                    client.DownloadFile(imageUrls[1], savePath);
                    Console.WriteLine("Kuva " + fileName + " ladattu onnistuneesti!");
                    Console.WriteLine("");
                    
                    // Katkaisee while loopin
                    break;

                    }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("Kuvan lataaminen ei onnistunut! " + ex.Message);
                    }
                    }

                // Puhdistaa imageUrls arrayn, jolloin jos ohjelman pyöriessä ottaa toiselta päivältä kuvan niin ohjelma toimii.
                Array.Clear(imageUrls, 0, imageUrls.Length);
                }
            }
            
            // Jos vastaa 2, annetaan käyttäjälle tämän eilisen päivän kuva(t)
            if (vastaus == "2")
            {
                try
                {
                // Lataa nettisivulta tekstit tiedostoon nasa<Eilinenpäivä yyyy-MM-dd>.txt
                using(WebClient client = new WebClient())
                {
                string s = client.DownloadString("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date="+ yesterday.ToString("yyyy-MM-dd"));

                // StreamWriter kirjoittaa tekstit tiedostoon Nasa<Eilinenpäivä yyyy-MM-dd>.txt
                StreamWriter writer = new StreamWriter(projectPath + "/" + "txts" + "/" + "Nasa" + yesterday.ToString("yyyy-MM-dd") + ".txt");
                writer.Write(s);
                writer.Close();
                }

                // Lukee tiedoston nasa<Eilinenpäivä yyyy-MM-dd>.txt stringiin text
                string text = File.ReadAllText(projectPath + "/" + "txts" + "/" + "Nasa" + yesterday.ToString("yyyy-MM-dd") + ".txt");

                // Tsekkaa kaikki matchit jotka löytää tekstistä, pattern regex on määritelty referensseissä
                MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

                // Jos matches on enemmän kuin 0 niin lisää array imageUrlsiin niin monta eri arvoa kuin matches löysi matcheja
                if (matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        imageUrls[i] = matches[i].Value;
                    }
                }
                }

                // Jos try:n {} sisällä oleva koodi ei mene läpi antaa se Error viestin ja palaa while loopin alkuun
                catch (WebException ex)
                {
                    Console.WriteLine("Jotain meni pieleen! Kokeile uudestaan! " + ex.Message);
                    continue;
                }

                // Printtaa kaikki imageUrls arrayn arvot. Koodia käytetty testaukseen
                // imageUrls.ToList().ForEach(i => Console.WriteLine(i.ToString()));

                while (true)
                {
                    Console.WriteLine("Jos haluat HD-laatuisen kuvan. Vastaa 1");
                    Console.WriteLine("Jos haluat normaali-laatuisen kuvan. Vastaa 2");
                    string? mikaKuva = Console.ReadLine();

                    // Ladataan HD-laatuinen
                    if (mikaKuva == "1")
                    {
                    try
                    {
                    using (WebClient client = new WebClient())
                    {
                    // Tekee uuden kansion Kuvat\yyyy\MM
                    Directory.CreateDirectory(projectPath + "/" + "Kuvat" + "/" + yesterday.ToString("yyyy") + "/" + yesterday.ToString("MM"));

                    // Määritetään ladattavan tiedoston nimi ja muoto.
                    string fileName = yesterday.ToString("yyyy-MM-dd") + "HD" + ".jpg";

                    // Määritetään kansion sijainti minne tiedosto ladataan
                    string savePath = Path.Combine(projectPath + "/" + "Kuvat" + "/" + yesterday.ToString("yyyy") + "/" + yesterday.ToString("MM"), fileName);

                    // Ladataan kuva imageUrls Arrayn indeksistä 0, koska APIssa HD-laatu on aina ensinmäisenä index on 0
                    client.DownloadFile(imageUrls[0], savePath);
                    Console.WriteLine("Kuva " + fileName + " ladattu onnistuneesti!");
                    Console.WriteLine("");
    
                    // Katkaisee while loopin
                    break;
                    }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("Kuvan lataaminen ei onnistunut! " + ex.Message);
                    }
                    }

                    // Ladataan normaalilaatuisena
                    if (mikaKuva == "2")
                    {
                    try
                    {
                    using (WebClient client = new WebClient())
                    {
                    // Tekee uuden kansion Kuvat\yyyy\MM
                    Directory.CreateDirectory(projectPath + "/" + "Kuvat" + "/" + yesterday.ToString("yyyy") + "/" + yesterday.ToString("MM"));

                    // Määritetään ladattavan tiedoston nimi ja muoto.
                    string fileName = yesterday.ToString("yyyy-MM-dd") + ".jpg";

                    // Määritetään kansion sijainti minne tiedosto ladataan
                    string savePath = Path.Combine(projectPath + "/" + "Kuvat" + "/" + yesterday.ToString("yyyy") + "/" + yesterday.ToString("MM"), fileName);

                    // Ladataan kuva imageUrls Arrayn indeksistä 1, koska APIssa normaali-laatu on aina toisena index on 1
                    client.DownloadFile(imageUrls[1], savePath);
                    Console.WriteLine("Kuva " + fileName + " ladattu onnistuneesti!");
                    Console.WriteLine("");
                    
                    // Katkaisee while loopin
                    break;

                    }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("Kuvan lataaminen ei onnistunut! " + ex.Message);
                    }
                    }

                // Puhdistaa imageUrls arrayn, jolloin jos ohjelman pyöriessä ottaa toiselta päivältä kuvan niin ohjelma toimii.
                Array.Clear(imageUrls, 0, imageUrls.Length);
            }
            }
            
            // Jos vastaa 3, annetaan käyttäjälle satunnaisen päivän kuva(t)
            // Lisää try catch jos random antaa 01.01.1995 - 15.06-1995. Koska kuvia on vasta ajalta 16.6.1995 eteenpäin.
            if (vastaus == "3")
            {
                // Määritetään satunnainen vuosi, kuukausi ja päivä.
                randomYear = rnd.Next(1995, today.Year);
                randomMonth = rnd.Next(1,13);
                randomDayy = rnd.Next(1,32);

                try
                {
                // Lataa nettisivulta tekstit tiedostoon nasa<Tämäpäivä yyyy-MM-dd>.txt
                using(WebClient client = new WebClient())
                {
                string s = client.DownloadString("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date="+ randomYear.ToString() + "-" + randomMonth.ToString() + "-" + randomDayy.ToString());

                // StreamWriter kirjoittaa tekstit tiedostoon Nasa<Eilinenpäivä yyyy-MM-dd>.txt
                StreamWriter writer = new StreamWriter(projectPath + "/" + "txts" + "/" + "Nasa" + randomYear.ToString() + "-" + randomMonth.ToString() + "-" + randomDayy.ToString() + ".txt");
                writer.Write(s);
                writer.Close();
                }

                // Lukee tiedoston nasa<Satunnainenpäivä yyyy-MM-dd>.txt stringiin text
                string text = File.ReadAllText(projectPath + "/" + "txts" + "/" + "Nasa" + randomYear.ToString() + "-" + randomMonth.ToString() + "-" + randomDayy.ToString() + ".txt");
                
                // Tsekkaa kaikki matchit jotka löytää tekstistä, pattern regex on määritelty referensseissä
                MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
                
                // Jos matches on enemmän kuin 0 niin lisää array imageUrlsiin niin monta eri arvoa kuin matches löysi matcheja
                if (matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        imageUrls[i] = matches[i].Value;
                    }
                }
                }

                // Jos try:n {} sisällä oleva koodi ei mene läpi antaa se Error viestin ja palaa while loopin alkuun
                catch (WebException ex)
                {
                    Console.WriteLine("Jotain meni pieleen! Kokeile uudestaan! " + ex.Message);
                    continue;
                }
                
                // Printtaa kaikki imageUrls arrayn arvot. Koodia käytetty testaukseen
                // imageUrls.ToList().ForEach(i => Console.WriteLine(i.ToString()));

                while (true)
                {
                    Console.WriteLine("Sait satunnaisen päivän " + randomDayy.ToString() + "." + randomMonth.ToString() + "." + randomYear.ToString());
                    Console.WriteLine("Jos haluat HD-laatuisen kuvan. Vastaa 1");
                    Console.WriteLine("Jos haluat normaali-laatuisen kuvan. Vastaa 2");
                    string? mikaKuva = Console.ReadLine();

                    // Ladataan HD-laatuinen
                    if (mikaKuva == "1")
                    {
                    try
                    {
                    using (WebClient client = new WebClient())
                    {
                    // Tekee uuden kansion Kuvat\yyyy\MM
                    Directory.CreateDirectory(projectPath + "/" + "Kuvat" + "/" + randomYear.ToString() + "/" + randomMonth.ToString());

                    // Määritetään ladattavan tiedoston nimi ja muoto.
                    string fileName = randomYear.ToString() + "-" + randomMonth.ToString() + "-" + randomDayy.ToString() + "HD" + ".jpg";

                    // Määritetään kansion sijainti minne tiedosto ladataan
                    string savePath = Path.Combine(projectPath + "/" + "Kuvat" + "/" + randomYear.ToString() + "/" + randomMonth.ToString(), fileName);

                    // Ladataan kuva imageUrls Arrayn indeksistä 0, koska APIssa HD-laatu on aina ensinmäisenä index on 0
                    client.DownloadFile(imageUrls[0], savePath);
                    Console.WriteLine("Kuva " + fileName + " ladattu onnistuneesti!");
                    Console.WriteLine("");
                    
                    // Katkaistaan while loop
                    break;

                    }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("Kuvan lataaminen ei onnistunut! " + ex.Message);
                    }
                    }
                    

                    // Ladataan normaalilaatuisena
                    if (mikaKuva == "2")
                    {
                    try
                    {
                    using (WebClient client = new WebClient())
                    {
                    // Tekee uuden kansion Kuvat\yyyy\MM
                    Directory.CreateDirectory(projectPath + "/" + "Kuvat" + "/" + randomYear.ToString() + "/" + randomMonth.ToString(""));

                    // Määritetään ladattavan tiedoston nimi ja muoto.
                    string fileName = randomYear.ToString() + "-" + randomMonth.ToString() + "-" + randomDayy.ToString() + ".jpg";

                    // Määritetään kansion sijainti minne tiedosto ladataan
                    string savePath = Path.Combine(projectPath + "/" + "Kuvat" + "/" + randomYear.ToString() + "/" + randomMonth.ToString(), fileName);

                    // Ladataan kuva imageUrls Arrayn indeksistä 1, koska APIssa normaali-laatu on aina toisena index on 1
                    client.DownloadFile(imageUrls[1], savePath);
                    Console.WriteLine("Kuva " + fileName + " ladattu onnistuneesti!");
                    Console.WriteLine("");
            
                    // Katkaistaan while loop
                    break;

                    }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("Kuvan lataaminen ei onnistunut!" + ex.Message);
                    }
                    }

                    // Puhdistaa imageUrls arrayn, jolloin jos ohjelman pyöriessä ottaa toiselta päivältä kuvan niin ohjelma toimii.
                    Array.Clear(imageUrls, 0, imageUrls.Length);
                } 
            }

            // Jos käyttäjä antaa arvon "0" katkaistaan ohjelma
            if (vastaus == "0")
            {
                break;
            }

            }
        }
    }


