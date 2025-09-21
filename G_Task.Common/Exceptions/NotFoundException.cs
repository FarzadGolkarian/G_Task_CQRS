namespace G_Task.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) was not found !") { }
        public NotFoundException(string message) : base(message) { }


    }
}
