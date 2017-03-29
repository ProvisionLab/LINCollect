namespace Web.Models.DTO
{
    public class NAnswerModel
    {
        public int Id { get; set; }
        public int NQuestionId { get; set; }
        public int OrderId { get; set; }
        public bool IsDefault { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}