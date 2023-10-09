namespace SimpleChat_Core.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public int UserId { get; set; }
        public ChatDTO Chat { get; set; }
        public int? ChatId { get; set; }
        public string Content { get; set; }
    }
}
