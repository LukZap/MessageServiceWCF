﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.Enums
{
    [DataContract]
    public enum ReturnCode
    {
        Success,

        ValidationError,

        InternalError
    }
}
