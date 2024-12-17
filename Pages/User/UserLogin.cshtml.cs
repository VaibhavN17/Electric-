using Electric__.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Electric__.Pages.User
{
    public class UserLoginModel : PageModel
    {
        private readonly MongoDBService _mongoDBService;

        public UserLoginModel(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [BindProperty]
        public string LoginEmail { get; set; }

        [BindProperty]
        public string LoginPassword { get; set; }

        public IActionResult OnPostLogin()
        {
            var usersCollection = _mongoDBService.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("Email", LoginEmail),
                Builders<BsonDocument>.Filter.Eq("Password", LoginPassword) // Hash passwords in real apps!
            );

            var user = usersCollection.Find(filter).FirstOrDefault();

            if (user != null)
            {
                return RedirectToPage("/Index"); // Redirect to dashboard or homepage
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return Page();
        }
    }
}
