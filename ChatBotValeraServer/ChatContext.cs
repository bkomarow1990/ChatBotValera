using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace ChatBotValeraServer
{
    public class ChatContext : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ChatBotValera.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public ChatContext()
            : base("name=ChatContext")
        {
            Database.SetInitializer(new ChatInitializer());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
    }

    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string MessageText { get; set; }

        //Nav
        public virtual ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
    }
    public class Answer
    {
        public int Id { get; set; }
        [Required]
        public string AnswerText { get; set; }

        //Nav
        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}