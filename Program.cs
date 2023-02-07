namespace NasaAPI
{
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    public class Program
    {
        static void Main(string[] args)
        {
            RandomDay randomDay = new RandomDay();
            DateTime today = DateTime.Now;
            DateTime yesterday = DateTime.Today.AddDays(-1);
            string randomYearMonthDay;
            // (@"\b(?:https?://|www\.)\S+\b", RegexOptions.IgnoreCase);
            //                      (@"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?");
            // Regex urlRx = new Regex(@"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?", RegexOptions.IgnoreCase);
            //Regex urlRx = new Regex("(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?");
            Regex urlRx = new Regex(@"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?");
            List<string> list = new List<string>();

            while(true)
            {
            // https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date=YYYY-MM-DD
            Console.WriteLine("Jos haluat tämän päivän kuvan. Vastaa 1");
            Console.WriteLine("Jos haluat kuvan eiliseltä päivältä. Vastaa 2"); // YYYY-MM-DD
            Console.WriteLine("Jos haluat satunnaisen kuvan väliltä 16.6.1995 - " + today.ToString("dd.MM.yyyy") + ". Vastaa 3"); 
            Console.WriteLine("Jos haluat sulkea ohjelman. Vastaa 0");
            string? vastaus = Console.ReadLine();

            // (http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?

            if (vastaus == "1")
            {
                // string todayString = today.ToString("yyyy-MM-dd");
                //Console.WriteLine(today.ToString("yyyy-MM-dd"));

                using(WebClient client = new WebClient())
                {
                string s = client.DownloadString("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date="+ today.ToString("yyyy-MM-dd"));

                StreamWriter writer = new StreamWriter("nasa" + today.ToString("yyyy-MM-dd") + ".txt");
                writer.Write(s);
                writer.Close();
                }

                var matches = urlRx.Matches("nasa" + today.ToString("yyyy-MM-dd") + ".txt");

                foreach (var match in matches)
                {
                    Console.WriteLine(match);
                }
                
               /*  //string txt = "this is my url http://www.google.com and visit this website and this is my url http://www.yahoo.com";
                MatchCollection matches = urlRx.Matches("nasa" + today.ToString("yyyy-MM-dd") + ".txt");
                foreach (Match match in matches)
                {
                    list.Add(match.Value);
                } */

               /*  foreach (Match item in Regex.Matches("nasa" + today.ToString("yyyy-MM-dd") + ".txt", @"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?"))
                {
                    Console.WriteLine(item.Value);
                } */

                /* foreach (var link in list)
                {
                    Console.WriteLine(link);
                } */

              /*   string[] arr = Regex.Matches("nasa" + today.ToString("yyyy-MM-dd") + ".txt", @"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

                for (int i = 0; i < arr.Length; i++)
                {
                    System.Console.WriteLine(arr[i]);
                } */


            }   
            

            if (vastaus == "2")
            {
                // Console.WriteLine(yesterday.ToString("yyyy-MM-dd"));

                using(WebClient client = new WebClient())
                {
                string s = client.DownloadString("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date="+ yesterday.ToString("yyyy-MM-dd"));

                StreamWriter writer = new StreamWriter("nasa" + yesterday.ToString("yyyy-MM-dd") + ".txt");
                writer.Write(s);
                writer.Close();

                }
            }

            // 16.6.1995 - (today)
            // https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1
            if (vastaus == "3")
            {
                randomYearMonthDay = randomDay.Next().ToString("yyyy-MM-dd");
                // Console.WriteLine(randomDay.Next().ToString("yyyy-MM-dd"));
                // Console.WriteLine(randomYearMonthDay);

                using(WebClient client = new WebClient())
                {
                string s = client.DownloadString("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date="+ randomYearMonthDay);

                StreamWriter writer = new StreamWriter("nasa" + randomYearMonthDay + ".txt");
                writer.Write(s);
                writer.Close();

                }

            }

            if (vastaus == "0")
            {
                break;
            }

            }



        }

    }
}
