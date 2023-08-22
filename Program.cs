using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class Program
{
    static Random rnd = new Random();
    static DateTime today = DateTime.Now;
    static string pattern = @"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|jpeg|gif|png)";

    static async Task Main(string[] args)
    {
        string projectPath = Directory.GetCurrentDirectory();

        while (true)
        {
            // Display options to the user
            Console.WriteLine("Options:");
            Console.WriteLine("1. Today's picture");
            Console.WriteLine("2. Yesterday's picture");
            Console.WriteLine("3. Random day's picture");
            Console.WriteLine("0. Exit");
            string? choice = Console.ReadLine();

            // Process user's choice
            switch (choice)
            {
                case "1":
                    await DownloadAndSaveImage(today, projectPath);
                    break;

                case "2":
                    DateTime yesterday = today.AddDays(-1);
                    await DownloadAndSaveImage(yesterday, projectPath);
                    break;

                case "3":
                    await DownloadRandomDayImage(projectPath);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please choose again.");
                    break;
            }
        }
    }

    static async Task DownloadAndSaveImage(DateTime date, string projectPath)
    {
        try
        {
            // Get the URL of the image for the given date
            string imageUrl = await GetImageUrl(date);

            // Ask user to choose image quality
            string qualityChoice = ChooseImageQuality();

            // Determine whether to download HD or Standard quality
            string quality = qualityChoice == "1" ? "HD" : "Standard";

            if (imageUrl != null)
            {
                // Construct the path to save the image
                string savePath = GetSavePath(date, projectPath, quality, imageUrl);

                // Download the image from the URL and save it to the specified path
                await DownloadImage(imageUrl, savePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static async Task<string?> GetImageUrl(DateTime date)
    {
        string dateString = date.ToString("yyyy-MM-dd");
        string apiUrl = "https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date=" + dateString;

        using (HttpClient client = new HttpClient())
        {
            // Download and retrieve the response from the NASA API
            string response = await client.GetStringAsync(apiUrl);
            MatchCollection matches = Regex.Matches(response, pattern, RegexOptions.IgnoreCase);
            return matches.Count > 0 ? matches[0].Value : null;
        }
    }

    static string ChooseImageQuality()
    {
        // Prompt user to choose image quality
        Console.WriteLine("Choose image quality:");
        Console.WriteLine("1. HD");
        Console.WriteLine("2. Standard");
        return Console.ReadLine();
    }

    static string GetSavePath(DateTime date, string projectPath, string quality, string imageUrl)
    {
        // Construct the file name and save path for the downloaded image
        string fileName = $"{date.ToString("yyyy-MM-dd")}{quality}.jpg";
        string savePath = Path.Combine(projectPath, "Pictures", date.ToString("yyyy"), date.ToString("MM"), fileName);
        Directory.CreateDirectory(Path.GetDirectoryName(savePath)!); // Assert non-null
        return savePath;
    }

    static async Task DownloadImage(string url, string savePath)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] imageBytes = await client.GetByteArrayAsync(url);
                File.WriteAllBytes(savePath, imageBytes);
                Console.WriteLine("Image downloaded successfully: " + Path.GetFileName(savePath));
                Console.WriteLine("");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Image download failed: " + ex.Message);
        }
    }

    static async Task DownloadRandomDayImage(string projectPath)
    {
        // Generate a random date and download/save the image for that date
        DateTime randomDate = new DateTime(rnd.Next(1995, today.Year + 1), rnd.Next(1, 13), rnd.Next(1, 29));
        await DownloadAndSaveImage(randomDate, projectPath);
    }
}
