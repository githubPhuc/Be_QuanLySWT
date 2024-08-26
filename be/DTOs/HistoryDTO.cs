namespace be.DTOs
{
    public class HistoryDTO
    {
        public int TestDetailId { get; set; }
        public string SubjectName { get; set; }
        public string Duration { get; set; }
        public string Topic { get; set; }   
        public float Score { get; set; }
        public int? TotalQuestion { get; set; }
        public int? AnswerRight { get; set; }
        public DateTime SubmitDate { get; set; }    
    }
}
