using System.ComponentModel;

namespace AutoGrader.Models.ViewModels
{
    public class SubmissionInputViewModel
    {
        [DisplayName("Source Code")]
        public string SourceCode { get; set; }

        public string Language { get; set; }

        public int AssignmentId { get; set; }
    }
}