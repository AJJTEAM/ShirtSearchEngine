using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionLine.CodingChallenge.Services
{
    public class ValidationService : IValidationService
    {
        public void ValidateSearchOptions(SearchOptions searchOptions)
        {
            if (searchOptions?.Colors == null || searchOptions.Sizes == null)
                throw new ArgumentException("searchOptions");
        }
    }
}
