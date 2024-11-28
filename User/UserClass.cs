using System.Text.Json;

namespace User;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int Points { get; set; }
    public List<int> QuizIds { get; set; }
    public static int CurrentMaxId { get; set; } = 0;

    public List<User> UserList = new List<User>();
    private string Filepath = "C:\\Users\\user\\RiderProjects\\PROJECT\\User\\UserInfo.json";

    public User(string username, string password)
    {
        Id = ++CurrentMaxId;
        Username = username;
        Password = password;
        Points = 0;
        QuizIds = new List<int>();
    }

    public User()
    {
        Id = ++CurrentMaxId;
        Username = "";
        Password = "";
        Points = 0;
        QuizIds = new List<int>();
    }

    public void AddPoints(int points)
    {
        Points += points;
    }

    public void LoadFromFile()
    {
        if (File.Exists(Filepath))
        {
            string jsonString = File.ReadAllText(Filepath);
            if (!string.IsNullOrEmpty(jsonString))
            {
                UserList = JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
                if (UserList.Any())
                {
                    CurrentMaxId = UserList.Max(u => u.Id);
                }
            }
        }
        else
        {
            UserList = new List<User>();
        }
    }

    public void SaveToFile()
    {
        var jsonString = JsonSerializer.Serialize(UserList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(Filepath, jsonString);
    }
}