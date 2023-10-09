namespace SimpleChat_Db.Entities
{
    public class Connection
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public int userId { get; set; }
        public Chat? Chat { get; set; }
        public int? ChatId { get; set; }
    }
}
