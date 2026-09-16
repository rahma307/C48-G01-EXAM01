using System;


namespace OOP_Exam
{
    public abstract class Exam
    {
        #region fields

        private int time; //  time in minutes
        private int numberOfQuestions;
        private Question[] questions = Array.Empty<Question>();
        private Subject sub = default!;



        #endregion

        #region Constructor


        public Exam(int time, int numberOfQuestions, Subject subject)
        {
            Time = time;
            NumberOfQuestions = numberOfQuestions;
            Questions = new Question[NumberOfQuestions];
            Subject = subject;
        }

        #endregion

        #region Properties
        public int Time
        {
            get => time;
            set
            {
                if (value < 30 || value > 180)
                {
                    throw new ArgumentException("Exam time must be  greater than or equal 30 and less than or equal 180");

                }

                time = value;
            }
        }


        public int NumberOfQuestions
        {
            get => numberOfQuestions;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("number of questions must be greater than 0");
                numberOfQuestions = value;
            }
        }
        public virtual Question[] Questions
        {
            get => questions;
            set
            {
                if (value is null)
                { throw new ArgumentNullException(nameof(value)); }

                questions = value;
            }

        }

        public Subject Subject
        {
            get => sub;
            set
            {
                if (value is null)
                { throw new ArgumentNullException(nameof(value)); }

                sub = value;
            }
        }
        public virtual Question this[int index]
        {
            get
            {
                if (index < 0 || index >= Questions.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return Questions[index];
            }

            set
            {
                if (index < 0 || index >= Questions.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index)); ;
                }
                Questions[index] = value;
            }
        }

        public decimal Degree
        {
            get
            {
                decimal degree = 0;

                foreach (Question q in Questions)
                {
                    if (q is not null)
                    {
                        degree += q.Mark;
                    }
                }
                return degree;


            }
        }

        #endregion


        #region Methods

        public abstract void ShowExam();
        #endregion
    }
}
