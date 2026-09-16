using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Exam
{
    public class TrueFalseQuestions : Question
    {
        public TrueFalseQuestions(string header, string body, decimal mark, RightAnswer rightAnswer, Answer[] answers) : base(header, body, mark)
        {
            Answers = answers;
            Right_Answer = rightAnswer;

        }

        public override Answer[] Answers
        {
            get => base.Answers;
            set
            {

                if (value.Length != 2)
                {
                    throw new ArgumentException("True/False question must have exactly 2 answers.");
                }
                bool hasTrue = false;
                bool hasFalse = false;
                foreach (Answer answer in value)
                {
                    if (answer.AnswerText?.ToLower() == "true" && hasTrue == false)
                    { hasTrue = true; }
                    else if (answer.AnswerText?.ToLower() == "false" && hasFalse == false)
                    { hasFalse = true; }
                    else { throw new ArgumentException("Answers must be True or False."); }

                }
                if (!hasTrue || !hasFalse)
                {
                    throw new ArgumentException("Answers must contain True and False.");
                }

                base.Answers = value;
            }
        }
        public override RightAnswer Right_Answer
        {
            get => base.Right_Answer;
            set
            {


                if (value.AnswerText?.ToLower() != "true" &&
                    value.AnswerText?.ToLower() != "false")
                {
                    throw new ArgumentException("Right answer must be True or False.");
                }
                base.Right_Answer = value;


            }
        }



    }
}
