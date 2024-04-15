namespace Kreta.Shared.Responses
{
    public class ControllerResponse : ErrorStore
    {
        public bool IsSuccess => !HasError;
        public Guid Id { get; set; }
        public ControllerResponse() : base() { }
        public ControllerResponse(string errorString) : base()
        {
            Error = errorString;
        }
    }
}
