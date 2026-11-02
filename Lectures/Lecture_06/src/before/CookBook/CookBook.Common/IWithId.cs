using System;

namespace CookBook.Common
{
    public interface IWithId
    {
        Guid Id { get; init; }
    }
}
