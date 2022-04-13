using Les1.Interface;
using System.Collections.Generic;

namespace Les1.DAL
{
    public class DebetCardCreateResponse : IOperation
    {
        public IReadOnlyList<IOperationFailure> Failures { get; }

        public bool Succeed { get; }
        public DebetCardCreateResponse(IReadOnlyList<IOperationFailure> failures, bool succeed)
        {
            Failures = failures;
            Succeed = succeed;
        }
    }
}
