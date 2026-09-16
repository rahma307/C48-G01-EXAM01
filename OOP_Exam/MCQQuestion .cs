using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Exam
{
    public class MCQQuestion : Question
    {
        public MCQQuestion(string header, string body, decimal mark, RightAnswer rightAnswer, Answer[] answers) : base(header, body, mark, answers)
        {
            Right_Answer = rightAnswer;
        }
        public override RightAnswer Right_Answer
        {
            get => base.Right_Answer;

            set
            {
                bool incorrect = true;
                foreach (Answer item in Answers)
                {
                    if (item is not null)
                    {
                        if (value.AnswerId == item.AnswerId)
                        {
                            incorrect = false;
                        }
                    }
                }
                if (incorrect)
                {
                    throw new ArgumentException("Right answer must be one of the provided answers.");
                }
                base.Right_Answer = value;

            }
        }
    }
}
