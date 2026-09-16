using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Exam
{
    public class PracticalExam : Exam
    {
        public PracticalExam(int time, int numberOfQuestions, Subject subject) : base(time, numberOfQuestions, subject)
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
                        q is not MCQQuestion)
                    {
                        throw new ArgumentException(
                            "Practical Exam accepts only  MCQ questions.");
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
                    Console.WriteLine($"Right Answer : {q.Right_Answer.AnswerText}");

                    hasQuestions = true;
                }

            }
            if (!hasQuestions)
            {
                Console.WriteLine("No questions available.");
            }

        }


        public override Question this[int index]
        {
            get => base[index];
            set
            {

                if (value is not MCQQuestion)
                {
                    throw new ArgumentException(
                        "practical Exam accepts only MCQ questions.");
                }

                base[index] = value;

            }
        }
    }

}
