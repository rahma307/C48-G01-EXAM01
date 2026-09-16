namespace OOP_Exam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string subjectName = default!;
            int subjectId = 0;
            Subject sub = default!;
            int time = 0;
            int numberOfQuestions = 0;

            // create subject object
            while (true)
            {
                Console.WriteLine("please enter subject name");
                subjectName = Console.ReadLine()!;

                Console.Write("please enter Subject id");
                while (!int.TryParse(Console.ReadLine()!, out subjectId))
                {
                    Console.WriteLine("invalid input please enter number");
                }
                try
                {
                    sub = new Subject(subjectId, subjectName);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("please enter valid subject data again");
                }
            }

            // take exam type
            ExamType examType;

            while (true)
            {
                Console.Write("Enter exam type \n 1-Final \n 2-Practical: ");
                string input = Console.ReadLine()!;
                if (Enum.TryParse(input, out examType) &&
                    Enum.IsDefined(examType))
                {
                    break;
                }

                Console.WriteLine("Invalid exam type. Please enter Final or Practical.");
            }

            Console.WriteLine($"Selected Exam Type = {examType}");
            // create exam 

            while (true)
            {
                Console.Write("Please enter exam time: ");
                while (!int.TryParse(Console.ReadLine()!, out time))
                {
                    Console.WriteLine("invalid input please enter number");
                }
                Console.Write("Please enter number of questions: ");
                while (!int.TryParse(Console.ReadLine()!, out numberOfQuestions) || (examType == ExamType.Final && numberOfQuestions <= 1))
                {
                    Console.WriteLine("invalid input please enter number");
                }
                try
                {
                    sub.CreateExam(examType, time, numberOfQuestions);
                    Console.WriteLine(sub.SubjectId + sub.SubjectName);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
            // check type of exam 
            Console.WriteLine($"{sub.Exam.GetType().Name}");
            if (sub.Exam is FinalExam)
            {
                AddFinalExamQuestions(sub.Exam);
                sub.Exam.ShowExam();
            }
            else if (sub.Exam is PracticalExam)
            {
                AddPracticalExamQuestions(sub.Exam);
                sub.Exam.ShowExam();
            }

            Console.WriteLine("Do you want start exam (Y|N)");
            string start = Console.ReadLine()!;
            while (string.IsNullOrEmpty(start))
            {
                Console.WriteLine("please enter (Y|N)");
                start = Console.ReadLine()!;
            }
            if (start.ToLower() == "y")
            {
                StartExam(sub.Exam);
                if (sub.Exam is FinalExam final)
                {
                    final.ShowResult();
                }
            }
            else
            { Console.WriteLine("Exam not started."); }

        }


        static string GetQuestionType()
        {
            while (true)
            {
                Console.WriteLine("Choose question type:");
                Console.WriteLine("1- True/False");
                Console.WriteLine("2- MCQ");

                string questionType = Console.ReadLine()!;

                if (questionType == "1" || questionType == "2")
                {
                    return questionType;
                }

                Console.WriteLine("Invalid question type. Please choose 1 or 2.");
            }
        }

        static void AddFinalExamQuestions(Exam exam)
        {
            for (int i = 0; i < exam.NumberOfQuestions; i++)
            {
                string questionType = GetQuestionType();

                if (questionType == "1")
                {
                    while (true)
                    {
                        try
                        {
                            exam[i] = createTrueFalseQuestion();
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else if (questionType == "2")
                {
                    while (true)
                    {
                        try
                        {
                            exam[i] = createMcqQuestion();
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
        static void AddPracticalExamQuestions(Exam exam)
        {
            for (int i = 0; i < exam.NumberOfQuestions; i++)
            {
                while (true)
                {
                    try
                    {
                        exam[i] = createMcqQuestion();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        static TrueFalseQuestions createTrueFalseQuestion()
        {
            Console.WriteLine("please enter Question Header");
            string header = Console.ReadLine()!;
            Console.WriteLine("please enter Body Of Question");
            string body = Console.ReadLine()!;
            Console.WriteLine("please enter Question mark");
            decimal mark = 0;
            while (!decimal.TryParse(Console.ReadLine(), out mark))
            {
                Console.WriteLine("invalid mark please enter decimal number");
            }
            Answer[] answers =
            {
               new Answer(1, "True"),
               new Answer(2, "False")
             };

            RightAnswer rightAnswer = GetRightAnswer(answers);

            return new TrueFalseQuestions(header, body, mark, rightAnswer, answers);


        }


        static MCQQuestion createMcqQuestion()
        {

            Console.WriteLine("please enter Question Header");
            string header = Console.ReadLine()!;
            Console.WriteLine("please enter Body Of Question");
            string body = Console.ReadLine()!;
            Console.WriteLine("please enter Question mark");
            decimal mark = 0;
            while (!decimal.TryParse(Console.ReadLine(), out mark))
            {
                Console.WriteLine("invalid mark please enter decimal number");
            }

            Console.WriteLine("Please  enter number of answers");
            int answersnumber = 0;
            while (!int.TryParse(Console.ReadLine()!, out answersnumber) || answersnumber <= 0)
            {
                Console.WriteLine("Invalid number. Please enter a number greater than 0.");
            }

            Answer[] answers = GetAnswers(answersnumber);

            RightAnswer rightAnswer = GetRightAnswer(answers);
            return new MCQQuestion(header, body, mark, rightAnswer, answers);

        }

        static Answer[] GetAnswers(int numberOfAnswers)
        {
            Answer[] answers = new Answer[numberOfAnswers];

            for (int i = 0; i < numberOfAnswers; i++)
            {
                Console.Write($"Enter answer {i + 1}: ");
                string answerText = Console.ReadLine()!;

                while (string.IsNullOrEmpty(answerText))
                {
                    Console.WriteLine("Answer can't be empty.");
                    Console.Write($"Enter answer {i + 1}: ");
                    answerText = Console.ReadLine()!;
                }

                answers[i] = new Answer(i + 1, answerText);
            }

            return answers;
        }


        static RightAnswer GetRightAnswer(Answer[] answers)
        {
            Console.WriteLine("Please choose the right answer:");

            foreach (Answer answer in answers)
            {
                Console.WriteLine($"{answer.AnswerId}- {answer.AnswerText}");
            }

            int answerId;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out answerId))
                {
                    foreach (Answer answer in answers)
                    {
                        if (answer.AnswerId == answerId)
                        {
                            return new RightAnswer(
                                answer.AnswerId,
                                answer.AnswerText
                            );
                        }
                    }
                }

                Console.WriteLine("Invalid input. Please choose one of the available answers.");
            }
        }
        static void StartExam(Exam exam)
        {
            for (int i = 0; i < exam.NumberOfQuestions; i++)
            {
                Question question = exam[i];
                Console.WriteLine();
                Console.WriteLine($"Question {i + 1}");
                Console.WriteLine($"Header: {question.Header}");
                Console.WriteLine($"Question: {question.Body}");
                Console.WriteLine($"Mark: {question.Mark}");
                Console.WriteLine("Answers:");
                foreach (Answer answer in question.Answers)
                {
                    Console.WriteLine($"{answer.AnswerId}- {answer.AnswerText}");
                }
                int selectedAnswerId;
                while (true)
                {
                    Console.Write("Choose your answer: ");
                    if (int.TryParse(Console.ReadLine(), out selectedAnswerId))
                    {
                        Answer? selectedAnswer = null;
                        foreach (Answer answer in question.Answers)
                        {
                            if (answer.AnswerId == selectedAnswerId)
                            {
                                selectedAnswer = answer;
                                break;
                            }
                        }
                        if (selectedAnswer is not null)
                        {
                            question.UserAnswer = selectedAnswer;
                            break;
                        }
                    }
                    Console.WriteLine("Invalid answer. Please choose one of the available answers.");
                }
                Console.WriteLine();
            }

        }




    }
}

