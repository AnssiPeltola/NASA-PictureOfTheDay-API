namespace NasaAPI
{
    public class RandomDay
    {
        // References:
        public int randomYear { get; set; }
        public int randomMonth { get; set; }
        public int randomDay { get; set; }
        // public string thisYear = DateTime.Parse(DateTime.Now.ToString()).Year.ToString();
        public int thisYearInt = Convert.ToInt32(DateTime.Parse(DateTime.Now.ToString()).Year.ToString());
        
        public RandomDay()
        {
            
        }

        // Generoi random päivän väliltä 1995-06-16 - Tämäpäivä
        public DateTime Next()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1995, 6, 16);
            int range = (DateTime.Today - start).Days;

            return start.AddDays(gen.Next(range));
        }

        
       /*  public int GetRandomYear()
        {
            Random rndmYear = new Random();
            randomYear = rndmYear.Next(1995, thisYearInt);
            return randomYear;
        }

        public int GetRandomMonth()
        {
            Random rndmMonth = new Random();
            randomYear = rndmMonth.Next(1, 13);
            return randomMonth;
        }

        public int GetRandomDay()
        {
            Random rndmDay = new Random();
            randomYear = rndmDay.Next(1, 31);
            return randomDay;
        } */
        

    }
}