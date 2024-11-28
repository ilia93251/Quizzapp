using System.IO.Enumeration;
using System.Text.Json;

using System.Text.Json;

namespace PROJECT;
using QuizClass;
using  User;

internal class Program
{
   static User user;
    static void Main(string[] args)
    {
        int answer = 0;
        bool Endprocess1 = false;
        bool Endprocess2 = false;

        string Username = "";
        string Password = "";
        
        user = new User();
        user.LoadFromFile();

        QuizClass Quiz = new QuizClass();
        Quiz.LoadFromFile();
        Quiz.QuizId = Quiz.QuizList.Count + 1;

        while (!Endprocess1)
        {
            ShowMainMenu();
            if (!int.TryParse(Console.ReadLine(), out answer))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (answer)
            {
                case 1:
                    Console.WriteLine("Please enter a username:");
                    Username = Console.ReadLine();
                    Console.WriteLine("Please enter a password:");
                    Password = Console.ReadLine();
                    if (user.UserList.Any(x => x.Username == Username))
                    {
                        Console.WriteLine("Username is already taken.");
                        continue;
                    }
                    Console.WriteLine("Registration is successful.");
                    user.UserList.Add(new User(Username, Password));
                    user.SaveToFile();
                    user.Id = user.UserList.Count;
                    Endprocess1 = true;
                    break;

                case 2:
                    Console.WriteLine("Please enter a username:");
                    Username = Console.ReadLine();
                    Console.WriteLine("Please enter a password:");
                    Password = Console.ReadLine();
                    if (user.UserList.Any(x => x.Username == Username && x.Password == Password))
                    {
                        Console.WriteLine("Welcome back!");
                        Endprocess1 = true;
                    }
                    else
                        Console.WriteLine("Username or password is incorrect.");
                    break;

                case 3:
                    DisplayTop10Users(user);
                    break;

                case 4:
                    Endprocess1 = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }

        while (!Endprocess2)
        {
            ShowQuizMenu();
            if (!int.TryParse(Console.ReadLine(), out answer))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (answer)
            {
                case 1:
                    Console.WriteLine("Available Quizzes:");
                    foreach (var quiz in Quiz.QuizList)
                    {
                        Console.WriteLine($"Quiz ID: {quiz.QuizId}");
                    }
                    Console.Write("Enter quiz ID: ");
                    if (int.TryParse(Console.ReadLine(), out int quizId))
                    {
                        var selectedQuiz = Quiz.QuizList.FirstOrDefault(q => q.QuizId == quizId);
                        if (selectedQuiz != null)
                        {
                            int points = PlayQuizWithTimer(selectedQuiz);
                            Console.WriteLine($"You earned {points} points!");
                            user.AddPoints(points);
                            user.SaveToFile();
                        }
                        else
                            Console.WriteLine("Invalid quiz ID.");
                    }
                    else
                        Console.WriteLine("Invalid input.");
                    break;

                case 2:
                    Console.WriteLine("Please enter questions and answers for the quiz.");
                    Console.WriteLine("Enter question 1:");
                    Quiz.Q1.QAsaver();
                    Console.WriteLine("Enter question 2:");
                    Quiz.Q2.QAsaver();
                    Console.WriteLine("Enter question 3:");
                    Quiz.Q3.QAsaver();
                    Console.WriteLine("Enter question 4:");
                    Quiz.Q4.QAsaver();
                    Console.WriteLine("Enter question 5:");
                    Quiz.Q5.QAsaver();
                    Quiz.QuizList.Add(Quiz);
                    Quiz.SaveToFile();
                    Console.WriteLine("Quiz has been saved.");
                    break;

                case 3:
                    Console.WriteLine("Enter the quiz ID to remove:");
                    if (int.TryParse(Console.ReadLine(), out quizId))
                    {
                        var quizToRemove = Quiz.QuizList.FirstOrDefault(q => q.QuizId == quizId);
                        if (quizToRemove != null)
                        {
                            Quiz.QuizList.Remove(quizToRemove);
                            Quiz.SaveToFile();
                            Console.WriteLine("Quiz removed successfully.");
                        }
                        else
                            Console.WriteLine("Quiz ID not found.");
                    }
                    else
                        Console.WriteLine("Invalid input.");
                    break;

                case 4:
                    Console.WriteLine($"Username: {user.Username}");
                    Console.WriteLine($"Points: {user.Points}");
                    break;

                case 5:
                    Endprocess2 = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void ShowMainMenu()
    {
        Console.WriteLine("1: Register");
        Console.WriteLine("2: Login");
        Console.WriteLine("3: Show top 10 users");
        Console.WriteLine("4: Exit");
    }

    static void ShowQuizMenu()
    {
        Console.WriteLine("1: Play quiz");
        Console.WriteLine("2: Add quiz");
        Console.WriteLine("3: Remove quiz");
        Console.WriteLine("4: View your record");
        Console.WriteLine("5: Exit");
    }

    static void DisplayTop10Users(User user)
    {
        var topUsers = user.UserList.OrderByDescending(u => u.Points).Take(10);
        Console.WriteLine("Top 10 Users:");
        foreach (var u in topUsers)
        {
            Console.WriteLine($"Username: {u.Username}, Points: {u.Points}");
        }
    }

    static int PlayQuizWithTimer(QuizClass quiz)
    {
        int points = 0;
        var questions = new[] { quiz.Q1, quiz.Q2, quiz.Q3, quiz.Q4, quiz.Q5 };
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddMinutes(2);

        foreach (var q in questions)
        {
            if (DateTime.Now > endTime)
            {
                Console.WriteLine("Time's up! The quiz has ended.");
                break;
            }

            if (q?.Answer == null)
            {
                Console.WriteLine("Error: Question or Answer is not properly initialized.");
                continue;
            }

            Console.WriteLine(q.Question);
            Console.WriteLine($"A: {q.Answer.A}, B: {q.Answer.B}, C: {q.Answer.C}, D: {q.Answer.D}");
            Console.Write("Your answer: ");
            string userAnswer = Console.ReadLine();
            if (!string.IsNullOrEmpty(userAnswer) && userAnswer.ToUpper() == q.Answer.CorrectAnswer.ToUpper())
            {
                Console.WriteLine("Correct!");
                points += 20;
            }
            else
            {
                if (points <= 20) points = 0;
                else points -= 20;
                Console.WriteLine("Wrong answer.");
            }
            user.Points += points;
           
        }

        return points;
    }

}

