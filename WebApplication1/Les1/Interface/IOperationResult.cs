namespace Les1.Interface
{
    public interface IOperationResult<TResult> : IOperation
    {
        TResult Result { get; }
    }
}
