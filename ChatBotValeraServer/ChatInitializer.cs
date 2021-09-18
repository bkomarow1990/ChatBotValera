using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotValeraServer
{
    class ChatInitializer : CreateDatabaseIfNotExists<ChatContext>
    {
        protected override void Seed(ChatContext context)
        {

            Message hello = context.Messages.Add(new Message { MessageText = "Hello" });
            Answer hello_answer = context.Answers.Add(new Answer { AnswerText = "Hi" });
            context.Answers.Add(new Answer { AnswerText = "Salam!" });
            hello.Answers.Add(hello_answer);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
