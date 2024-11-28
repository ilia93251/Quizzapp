namespace AnswerClass;

public class AnswerClass
{
    public string A { get; set; } = "";
    public string B { get; set; } = "";
    public string C { get; set; } = "";
    public string D { get; set; } = "";
    public string CorrectAnswer { get; set; } = "";

    public void AnswerSaver()
    {
        Console.WriteLine("Enter 4 possible answers:");
        Console.Write("Answer A: ");
        A = Console.ReadLine() ?? "";
        Console.Write("Answer B: ");
        B = Console.ReadLine() ?? "";
        Console.Write("Answer C: ");
        C = Console.ReadLine() ?? "";
        Console.Write("Answer D: ");
        D = Console.ReadLine() ?? "";
        Console.WriteLine("Which answer is correct? (1, 2, 3, or 4)");
        int answer;
        while (!int.TryParse(Console.ReadLine(), out answer) || answer < 1 || answer > 4)
        {
            Console.WriteLine("Invalid choice. Please enter 1, 2, 3, or 4.");
        }

        switch (answer)
        {
            case 1:
                CorrectAnswer = "A";
                break;
            case 2:
                CorrectAnswer = "B";
                break;
            case 3:
                CorrectAnswer = "C";
                break;
            case 4:
                CorrectAnswer = "D";
                break;
        }
    }
}