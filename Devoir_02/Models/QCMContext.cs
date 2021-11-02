using Devoir_02.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devoir_02.Models
{
    public class QCMContext : DbContext
    {
        public QCMContext()
        {

        }

        public QCMContext(DbContextOptions<QCMContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuestionQuiz> QuestionQuizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionQuiz>().HasKey(t => new { t.QuestionID, t.QuizID });
            //Category
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, Description = "easy" },
                new Category { CategoryID = 2, Description = "medium" },
                new Category { CategoryID = 3, Description = "hard" }
                );

            //Questions
            modelBuilder.Entity<Question>().HasData(
                new Question { QuestionID = 1, Text = "Java is ...", Weight = 1, Type = "multiplechoice", CategoryID = 1 },
                new Question { QuestionID = 2, Text = "A Java class", Weight = 1, Type = "checkboxes", CategoryID = 2 },
                new Question { QuestionID = 3, Text = "What is Java inheritance?", Weight = 1, Type = "multiplechoice", CategoryID = 2 },
                new Question { QuestionID = 4, Text = "Polymorphism is the ability of an object to take on many forms.", Weight = 1, Type = "multiplechoice", CategoryID = 3 },
                new Question { QuestionID = 5, Text = "Local variables are declared in methods, constructors, or blocks.", Weight = 1, Type = "multiplechoice", CategoryID = 1 },
                new Question { QuestionID = 6, Text = "... stores a fixed-size sequential collection of elements of the same type?", Weight = 1, Type = "multiplechoice", CategoryID = 2 }
                );


            //Options
            modelBuilder.Entity<Option>().HasData(
                //options of question 1
                new Option { OptionID = 1, Text = "a coffee", IsRight = 0, QuestionID = 1 },
                new Option { OptionID = 2, Text = "a high-level programming language", IsRight = 1, QuestionID = 1 },
                new Option { OptionID = 3, Text = "a source code editor", IsRight = 0, QuestionID = 1 },
                //options of question 2
                new Option { OptionID = 4, Text = "is a template that describes the behavior that the object of its type support", IsRight = 1, QuestionID = 2 },
                new Option { OptionID = 5, Text = "can have any number of methods", IsRight = 1, QuestionID = 2 },
                //options of question 3
                new Option { OptionID = 6, Text = "the process where one class acquires the properties (methods and fields) of another.", IsRight = 1, QuestionID = 3 },
                new Option { OptionID = 7, Text = "a problem that arises during the execution of a program.", IsRight = 0, QuestionID = 3 },
                new Option { OptionID = 8, Text = "it mainly used to traverse collection of elements including arrays.", IsRight = 0, QuestionID = 3 },
                //options of question 4
                new Option { OptionID = 9, Text = "true", IsRight = 1, QuestionID = 4 },
                new Option { OptionID = 10, Text = "false", IsRight = 0, QuestionID = 4 },
                //options of question 5
                new Option { OptionID = 11, Text = "true", IsRight = 1, QuestionID = 5 },
                new Option { OptionID = 12, Text = "false", IsRight = 0, QuestionID = 5 },
                //options of question 6
                new Option { OptionID = 13, Text = "variables", IsRight = 0, QuestionID = 6 },
                new Option { OptionID = 14, Text = "arrays", IsRight = 1, QuestionID = 6 },
                new Option { OptionID = 15, Text = "methods", IsRight = 0, QuestionID = 6 }
                );
            //Quizzes
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz { QuizID = 1},
                new Quiz { QuizID = 2 }
                );
            //Link between quizzes and questions
            modelBuilder.Entity<QuestionQuiz>().HasData(
                //Questions in quiz 1
                new QuestionQuiz { QuestionID = 1, QuizID = 1},
                new QuestionQuiz { QuestionID = 2, QuizID = 1 },
                new QuestionQuiz { QuestionID = 4, QuizID = 1 },
                new QuestionQuiz { QuestionID = 6, QuizID = 1 },
                 //Questions in quiz 2
                new QuestionQuiz { QuestionID = 1, QuizID = 2 },
                new QuestionQuiz { QuestionID = 3, QuizID = 2 },
                new QuestionQuiz { QuestionID = 4, QuizID = 2 },
                new QuestionQuiz { QuestionID = 5, QuizID = 2 }
                );
            //Answers
            modelBuilder.Entity<Answer>().HasData(
                //Answer of quiz 1
                new Answer { AnswerID = 1, OptionID = 1, QuizID= 1},
                new Answer { AnswerID = 2, OptionID = 4, QuizID = 1 },
                new Answer { AnswerID = 3, OptionID = 5, QuizID = 1 },
                new Answer { AnswerID = 4, OptionID = 9, QuizID = 1 },
                new Answer { AnswerID = 5, OptionID = 14, QuizID = 1 },
                //Answer of quiz 2
                new Answer { AnswerID = 6, OptionID = 1, QuizID = 2 },
                new Answer { AnswerID = 7, OptionID = 7, QuizID = 2 },
                new Answer { AnswerID = 8, OptionID = 9, QuizID = 2 },
                new Answer { AnswerID = 9, OptionID = 11, QuizID = 2 }
                );




        }

    }
}
