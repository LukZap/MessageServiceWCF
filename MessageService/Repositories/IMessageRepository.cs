﻿using MessageService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.Repositories
{
    public interface IMessageRepository
    {
        Message Add(Message message);
    }
}
