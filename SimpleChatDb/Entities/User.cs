namespace SimpleChat_Db.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Message>? Messages { get; set; }
        public List<Connection>? Chats { get; set; }
    }
}
