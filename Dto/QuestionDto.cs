namespace JwtAuthApi.Dto
{
    public class QuestionDto
    {
        public string Title { get; set; }
        public List<AnswerDto> Answers { get; set;}
    }
}
