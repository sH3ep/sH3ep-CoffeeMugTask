using System;

namespace CoffeeMugTask.Persistance.Repositories.Exceptions
{
    public class EntityValidationException:Exception
    {
        public EntityValidationException() : base() { }
        public EntityValidationException(string message) : base(message) { }
        public EntityValidationException(string message, System.Exception inner) : base(message, inner) { }
    }
}