﻿using System;
using System.Collections.Generic;
using EntertainmentDatabase.REST.API.Abstractions;
using EntertainmentDatabase.REST.API.Enums;
using EntertainmentDatabase.REST.ServiceBase.Generics.Abstractions;

namespace EntertainmentDatabase.REST.API.Entities
{
    public class Movie : IEntity, IConsumerMedia
    {
        public Movie()
        {
            this.ActorMovies = new List<ActorMovie>();
        }

        public Guid Id
        {
            get;
            set;
        }

        public byte[] RowVersion
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public ConsumerMediaType ConsumerMediaType
        {
            get;
            set;
        }

        public DateTime? ReleasedOn
        {
            get;
            set;
        }

        public ICollection<ActorMovie> ActorMovies
        {
            get;
            set;
        }
    }
}
