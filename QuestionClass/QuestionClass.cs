namespace QuestionClass;
using AnswerClass;

public class QuestionClass
{
    public string Question { get; set; }
    public AnswerClass Answer { get; set; }

    public QuestionClass()
    {
        Question = "";
        Answer = new AnswerClass(); // Ensure Answer is always initialized
    }

    public void QAsaver()
    {
        Console.WriteLine("Enter the question:");
        Question = Console.ReadLine() ?? ""; // Avoid null values
        Answer.AnswerSaver();
    }
}