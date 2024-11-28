using System.Text.Json;
using AnswerClass;
using QuestionClass;
namespace QuizClass;


    public class QuizClass
    {
        public int QuizId { get; set; }
          public QuestionClass.QuestionClass Q1{get;set;} 
          public QuestionClass.QuestionClass Q2{get;set;}
         public QuestionClass.QuestionClass Q3{get;set;}
         public QuestionClass.QuestionClass Q4{get;set;}
         public QuestionClass.QuestionClass Q5 { get; set;}

         private string Filepath = "C:\\Users\\user\\RiderProjects\\PROJECT\\Quiz\\QuizInfo.json";

         public List<QuizClass> QuizList;

         public QuizClass()
         {
             Q1 = new QuestionClass.QuestionClass();
             Q2 = new QuestionClass.QuestionClass();
             Q3 = new QuestionClass.QuestionClass();
             Q4 = new QuestionClass.QuestionClass();
             Q5 = new QuestionClass.QuestionClass();
             QuizList = new List<QuizClass>();
         }

         public void SaveToFile()
         {
             string jsonString = JsonSerializer.Serialize(QuizList,new JsonSerializerOptions());
             
             File.WriteAllText(Filepath, jsonString);
             
             QuizList = new List<QuizClass>();
         }

         public void LoadFromFile()
         {
             string jsonString = File.ReadAllText(Filepath);
             
             if(jsonString != "")
                 QuizList = JsonSerializer.Deserialize<List<QuizClass>>(jsonString);
             else
             {
                 QuizList = new List<QuizClass>();
             }
         }
         

    }