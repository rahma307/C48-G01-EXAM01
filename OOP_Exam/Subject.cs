namespace OOP_Exam
{
    public class Subject
    {

        #region Fields

        private int subjectId;
        private string subjectName = default!;
        private Exam exam = default!;

        public Subject(int subjectId, string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
        }


        #endregion
        public int SubjectId
        {
            get => subjectId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("subject id must be greater than 0");
                }
                subjectId = value;
            }
        }
        public string SubjectName
        {
            get => subjectName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("invalid subject name");
                    throw new ArgumentNullException("subject name can't be null or empty");
                }
                subjectName = value;
            }
        }

        public Exam Exam { get => exam; private set => exam = value; }

        public void CreateExam(ExamType examType, int time, int numberOfQuestions)
        {
            if (examType == ExamType.Final)
            {
                Exam = new FinalExam(time, numberOfQuestions, this);
            }
            else if (examType == ExamType.Practical)
            {
                Exam = new PracticalExam(time, numberOfQuestions, this);
            }
            else
            {
                throw new ArgumentException("Invalid exam type.");
            }
        }


    }
}