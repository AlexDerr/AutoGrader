  namespace AutoGrader.Models.Submission
{
    public class SubmissionInput
    {
        public SubmissionInput()
        {
        }

        public int Id { get; set; }

        public string SourceCode { get; set; }

        public string Language { get; set; }
    }
}