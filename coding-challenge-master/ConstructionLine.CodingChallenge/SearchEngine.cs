using System;
using System.Collections.Generic;
using System.Linq;
using ConstructionLine.CodingChallenge.Services;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly IValidationService _validationService;    
        public SearchEngine(List<Shirt> shirts, IValidationService validationService)
        {
            _shirts = shirts;
            _validationService = validationService;
        }


        public SearchResults Search(SearchOptions options)
        {
            _validationService.ValidateSearchOptions(options);
            var shirts = FindShirtsNoLinq(options);
            var searchResults = new SearchResults
            {
                Shirts = shirts,
                ColorCounts = shirts.GroupBy(_=>_.Color).
                    Select(g => 
                        new ColorCount {Count = g.Count(), Color = g.Key}).ToList(),
                SizeCounts = shirts.GroupBy(_ => _.Size).
                    Select(g =>
                        new SizeCount { Count = g.Count(), Size = g.Key }).ToList()
            };
            
            foreach (var color in Color.All.Where(color => searchResults.ColorCounts.All(colorCount => colorCount.Color != color)))
                searchResults.ColorCounts.Add(new ColorCount { Color = color, Count = 0 });
            foreach (var size in Size.All.Where(size => searchResults.SizeCounts.All(sizeCount => sizeCount.Size != size)))
                searchResults.SizeCounts.Add(new SizeCount{ Size = size, Count = 0 });
            return searchResults;
        }

        private List<Shirt> FindShirts(SearchOptions options) => _shirts.Where(_ =>
            (!options.Colors.Any() || options.Colors.Any(color => color.Id == _.Color.Id))
            && (!options.Sizes.Any() || options.Sizes.Any(size => size.Id == _.Size.Id))).ToList();
        private List<Shirt> FindShirtsNoLinq(SearchOptions options)
        {
            var colors = options.Colors;
            var sizes = options.Sizes;
            var sizeIds = sizes.Select(s => s.Id).ToList();
            var colorIds = colors.Select(c => c.Id).ToList();
            var result = new List<Shirt>();
            for (var x = 0; x < _shirts.Count; x++)
            {
                var shirt = _shirts[x];
                if ((!colors.Any() || colorIds.Contains(shirt.Color.Id))
                    && (!sizes.Any() || sizeIds.Contains(shirt.Size.Id)))
                {
                    result.Add(shirt);
                }
            }
            return result;
        }
    }
}