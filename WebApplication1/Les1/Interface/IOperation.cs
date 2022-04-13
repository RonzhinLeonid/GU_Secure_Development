using System.Collections.Generic;

namespace Les1.Interface
{
    public interface IOperation
    {
        IReadOnlyList<IOperationFailure> Failures { get; }
        bool Succeed { get; }
    }
}
