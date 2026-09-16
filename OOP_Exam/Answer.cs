using System.Reflection.PortableExecutable;

namespace OOP_Exam
{
    public class Answer
    {
        #region Fields
        private int answerId;
        private string answerText = default!;

        #endregion
        public Answer()
        {
            AnswerId = 0;
            AnswerText = "unkown";
        }
        public Answer(int answerId, string answerText)
        {
            AnswerId = answerId;
            AnswerText = answerText;
        }

        #region Properties
        public int AnswerId { get => answerId; set => answerId = value; }
        public string AnswerText
        {
            get => answerText;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("invalid Header");
                    throw new ArgumentNullException("Header can't be null or empty");
                }
                answerText = value;
            }
        }
        #endregion


        #region Methods
        public override string ToString()
        {
            return $"Answer ID: {AnswerId}, Answer Text: {AnswerText}";
        }


        #endregion



    }
}