using System;

namespace CoffeeMugTask.Persistance.Repositories.Exceptions
{
    public class EntityNotFoundException :Exception
    {
        public EntityNotFoundException() : base() { }
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(string message, System.Exception inner) : base(message, inner) { }
    }
}