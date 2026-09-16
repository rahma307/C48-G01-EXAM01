using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Exam
{
    public class FinalExam : Exam
    {
        public FinalExam(int time, int numberOfQuestions, Subject subject) : base(time, numberOfQuestions, subject)
        {
        }
        public override Question[] Questions
        {
            get => base.Questions;

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                foreach (Question q in value)
                {
                    if (q is not null &&
                        q is not TrueFalseQuestions &&
                        q is not MCQQuestion)
                    {
                        throw new ArgumentException(
                            "Final Exam accepts only True/False and MCQ questions.");
                    }
                }

                base.Questions = value;
            }
        }
        public override void ShowExam()
        {

            bool hasQuestions = false;
            foreach (Question q in Questions)
            {

                if (q is not null)
                {
                    string result = "";
                    Console.WriteLine($"{q.Header}\nQuestion :{q.Body}\n");
                    foreach (Answer answer in q.Answers)
                    {
                        result += $"- {answer?.AnswerText}\n";
                    }
                    Console.WriteLine($"Answers :\n {result}");
                    hasQuestions = true;
                }

            }
            if (!hasQuestions)
            {
                Console.WriteLine("No questions available.");
            }


            Console.WriteLine($"Grade : {Degree}");
        }


        public override Question this[int index]
        {
            get => base[index];
            set
            {

                if (value is not TrueFalseQuestions && value is not MCQQuestion)
                {
                    throw new ArgumentException(
                        "Final Exam accepts only True/False and MCQ questions.");
                }

                base[index] = value;

            }
        }


        public void ShowResult()
        {
            decimal grade = 0;
            foreach (Question question in Questions)
            {
                if (question.UserAnswer is not null && question.UserAnswer.AnswerId == question.Right_Answer.AnswerId)
                { grade += question.Mark; }
            }
            Console.WriteLine("\n========================");
            Console.WriteLine(" EXAM RESULT");
            Console.WriteLine("========================");
            Console.WriteLine($"Grade: {grade} / {Degree}");
            Console.WriteLine("\nCorrect Answers:");
            foreach (Question question in Questions)
            {
                Console.WriteLine($"Question: {question.Body}");
                Console.WriteLine($"Your Answer: {question.UserAnswer?.AnswerText}");
                Console.WriteLine($"Correct Answer: {question.Right_Answer.AnswerText}");
                Console.WriteLine();
            }
        }
    }
}
