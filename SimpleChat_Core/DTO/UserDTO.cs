namespace SimpleChat_Core.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MessageDTO>? Messages { get; set; }
        public List<ChatDTO>? Chats { get; set; }
    }
}
