namespace SimpleChat_Db.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string ChatName { get; set; }
        public int MainUserId { get; set; }
        public List<Connection>? Connections { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
