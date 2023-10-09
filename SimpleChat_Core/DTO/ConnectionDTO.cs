namespace SimpleChat_Core.DTO
{
    public class ConnectionDTO
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public int UserId { get; set; }
        public ChatDTO? Chat { get; set; }
        public int? ChatId { get; set; }
    }
}
