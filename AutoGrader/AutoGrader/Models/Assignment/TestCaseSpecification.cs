namespace AutoGrader.Models.Assignment
{
    public class TestCaseSpecification
    {
        public TestCaseSpecification()
        {
        }

        public TestCaseSpecification(string Input, string Output)
        {
            this.Input = Input;
            this.ExpectedOutput = Output;
        }

        public int ID { get; set; }

        public string Input { get; set; }

        public string ExpectedOutput { get; set; }

        public string Feedback { get; set; }

        public int AssignmentId { get; set; }
    }
}
