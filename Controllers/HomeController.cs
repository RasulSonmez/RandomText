using Microsoft.AspNetCore.Mvc;
using RandomText.Data;
using RandomText.Models;
using System.Diagnostics;
using System.Text;

namespace RandomText.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string searchString, string sortBy)
        {
            var randomTexts = _context.RandomLetters.AsQueryable();



            if (!string.IsNullOrEmpty(searchString))
            {
                randomTexts = randomTexts.Where(text => text.Words.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "words_asc":
                        randomTexts = randomTexts.OrderBy(text => text.Words);
                        break;
                    case "words_desc":
                        randomTexts = randomTexts.OrderByDescending(text => text.Words);
                        break;
                    default:
                        break;
                }
            }

            return View(randomTexts.ToList());

        }

        [HttpPost]
        public IActionResult RandomNumberGiveRandomText(int number)
        {
            Random random = new Random();

            List<RandomLetter> randomLetters = new List<RandomLetter>();

            for (int i = 0; i < 5; i++)
            {
                string randomLetter = GenerateRandomText(number, random);
                string countedText = GenerateCountedText(randomLetter);
                randomLetters.Add(new RandomLetter { Words = randomLetter, LetterOfCount = countedText });
            }


            _context.RandomLetters.AddRange(randomLetters);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private string GenerateRandomText(int length, Random random)
        {
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = alphabet[random.Next(alphabet.Length)];
            }

            return new string(result);
        }

        private string GenerateCountedText(string text)
        {
            StringBuilder countedText = new StringBuilder();

            char currentChar = text[0];
            int count = 1;

            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == currentChar)
                {
                    count++;
                }
                else
                {
                    countedText.Append(currentChar);
                    countedText.Append(count);
                    currentChar = text[i];
                    count = 1;
                }
            }

            countedText.Append(currentChar);
            countedText.Append(count);

            return countedText.ToString();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}