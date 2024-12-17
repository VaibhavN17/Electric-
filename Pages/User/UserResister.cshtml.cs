using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Electric__.Pages.User
{
    public class UserResisterModel : PageModel
    {
        [BindProperty]
        public RegisterInput Input { get; set; } = new RegisterInput();

        public string Message { get; set; }

        private readonly IMongoCollection<BsonDocument> _userCollection;

        public UserResisterModel()
        {
            // MongoDB connection setup
            const string connectionUri = "mongodb+srv://Vaibhav2005:<db_password>@vaibhav.uq5sc.mongodb.net/?retryWrites=true&w=majority&appName=Vaibhav";

            var client = new MongoClient(connectionUri);
            var database = client.GetDatabase("ElectricDB"); // Database name
            _userCollection = database.GetCollection<BsonDocument>("UserData"); // Collection name
        }

        public void OnGet()
        {
            // GET handler logic if needed
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Message = "Please fill in all required fields correctly.";
                return;
            }

            // Create a BSON document for user data
            var userDocument = new BsonDocument
            {
                { "Username", Input.Username },
                { "Email", Input.Email },
                { "Password", Input.Password },
                { "CreatedAt", DateTime.UtcNow }
            };

            // Insert the data into MongoDB
            try
            {
                _userCollection.InsertOne(userDocument);
                Message = "User registered successfully!";
            }
            catch (Exception ex)
            {
                Message = $"Error: {ex.Message}";
            }
        }

        // Input model for the form
        public class RegisterInput
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
