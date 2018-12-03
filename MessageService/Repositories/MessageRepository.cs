using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageService.Entities;

namespace MessageService.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageDbContext _dbContext;

        public MessageRepository(MessageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Message Add(Message message)
        {
            var addedEntity = _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
            return message;
        }
    }
}
