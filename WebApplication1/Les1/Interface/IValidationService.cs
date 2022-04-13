namespace Les1.Interface
{
    public interface IValidationService<TEntity> where TEntity : class
    {
        System.Collections.Generic.IReadOnlyList<IOperationFailure> ValidateEntity(TEntity item);
    }
}
