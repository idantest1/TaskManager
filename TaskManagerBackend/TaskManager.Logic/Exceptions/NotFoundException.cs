namespace TaskManager.Logic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(object id) : base($"Resource with id {id} not found") { }

        public NotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
