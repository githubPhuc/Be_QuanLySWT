namespace be.DTOs
{
    public class NewsDTO
    {
        public int? NewsId { get; set; } 
        public string? CategoryName { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? SubTitle { get; set; }   
        public string? Content { get; set; }    
        public DateTime? CreatedDay { get; set; }
        public string? Status { get; set; }      
        public int? AccountId { get; set; } 
    }

    public class FirstNews
    {
        public int? NewsId { get; set; }
        public string? CategoryName { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? SubTitle { get; set; }
        public string? Content { get; set; }
        public string? CreatedDay { get; set; }
    }
    public class DailyNews
    {
        public int? NewsId { get; set; }
        public string? CategoryName { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
    }

    public class OtherNews
    {
        public int? NewsId { get; set; }
        public string? CategoryName { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? SubTitle { get; set; }
    }

    public class NewDetail
    {
        public int? NewsId { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public string? CreateDate { get; set; }
        public string? CategoryName { get; set; } 
    }
}
