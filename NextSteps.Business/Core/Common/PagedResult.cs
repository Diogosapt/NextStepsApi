using System.Collections.Generic;

namespace NextSteps.Business.Core.Common
{
    public record PagedResult<Entity>
    {
        public int Page { get; init; }

        public int PageSize { get; init; }

        public int Total { get; init; }

        public IEnumerable<Entity> Results { get; init; }
    }
}