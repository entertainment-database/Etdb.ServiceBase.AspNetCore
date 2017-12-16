﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Etdb.ServiceBase.General.Abstractions.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public object Recent { get; set; }

        public ConcurrencyException(string message, object recent) : base(message)
        {
            this.Recent = recent;
        }
    }
}
