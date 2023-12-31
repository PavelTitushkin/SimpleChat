﻿namespace SimpleChat_Db.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Chat Chat { get; set; }
        public int? ChatId { get; set; }
        public string Content { get; set; }
    }
}
