namespace be.Repositories.Graden
{
    public class GetListGrade
    {
        public int GradeId { get; set; }

        public string? NameGrade { get; set; }

        public string? Status { get; set; }

        public int? UserCreated { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? UserUpdated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public bool? IsDelete { get; set; }

        public int? UserDelete { get; set; }

        public DateTime? DateDelete { get; set; }
    }
}
