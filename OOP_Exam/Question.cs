using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Exam
{
    public abstract class Question : ICloneable, IComparable<Question>
    {
        #region Fields
        private string header = default!;
        private string body = default!;
        private decimal mark;
        private RightAnswer rightAnswer = default!;
        private Answer[] answers = Array.Empty<Answer>();
        private Answer? userAnswer;
        #endregion

        protected Question(string header, string body, decimal mark)
        {
            Header = header;
            Body = body;
            Mark = mark;
        }

        protected Question(string header, string body, decimal mark, Answer[] answers)
        {
            Header = header;
            Body = body;
            Mark = mark;
            Answers = answers;
        }


        #region Properties
        public Answer? UserAnswer { get => userAnswer; set => userAnswer = value; }
        public string Header
        {
            get => header;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("invalid Header");
                    throw new ArgumentNullException("Header can't be null or empty");
                }
                header = value;
            }
        }

        public string Body
        {
            get => body;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("invalid Body");
                    throw new ArgumentNullException("Body can't be null or empty");
                }
                body = value;
            }
        }

        public decimal Mark
        {
            get => mark;
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("invalid mark");
                    throw new ArithmeticException("mark must be greater than 0");
                }
                mark = value;

            }
        }

        public virtual RightAnswer Right_Answer
        {
            get => rightAnswer;

            set
            {
                if (value is null)
                {
                    Console.WriteLine("invalid Header");
                    throw new ArgumentNullException("RightAnswer can't be null or empty");
                }
                rightAnswer = value;
            }

        }

        public virtual Answer[] Answers
        {
            get => answers;

            set
            {
                if (value is null)
                { throw new ArgumentNullException(nameof(value)); }

                answers = value;
            }

        }// retyrn Array

        public Answer this[int index]
        {
            get
            {

                if (index < 0 || index >= Answers.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return Answers[index];
            }
            set
            {
                if (index < 0 || index >= Answers.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index)); ;
                }
                Answers[index] = value;
            }
        }

        #endregion

        #region Methods

        public Question Deepcopy()
        {

            Question copy = (Question)MemberwiseClone();// shallow 
            copy.Answers = Answers is null ? Array.Empty<Answer>() : (Answer[])this.Answers.Clone();
            copy.Right_Answer = Right_Answer is null ? new RightAnswer() : new RightAnswer(Right_Answer.AnswerId, Right_Answer.AnswerText);

            return copy;

        }
        public object Clone()
        {
            return Deepcopy();
        }


        public int CompareTo(Question? other)
        {
            if (other is null)
            {
                throw new ArgumentNullException("the second operend is null");
            }

            return this.Mark.CompareTo(other.Mark);
        }

        public override string ToString()
        {
            string result = $"Header: {Header}\n";
            result += $"Question: {Body}\n";
            result += $"Mark: {Mark}\n";
            result += "Answers:\n";

            foreach (Answer answer in Answers)
            {
                result += $"- {answer.AnswerText}\n";
            }

            result += $"Right Answer: {Right_Answer.AnswerText}";

            return result;
        }




        #endregion
    }
}
