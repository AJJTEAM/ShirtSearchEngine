using System;
using System.Collections.Generic;
using ConstructionLine.CodingChallenge.Services;
using Moq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        private  Mock<IValidationService> _mockValidateService;
        [SetUp]
        public void Setup()
        {
            _mockValidateService = new Mock<IValidationService>();
        }

        [Test]
        public void Should_ReturnResults_When_NoShirtsInStock()
        {
            var shirts = new List<Shirt>();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black },
                Sizes = new List<Size> { Size.Large }
            };
            var searchEngine = new SearchEngine(shirts, _mockValidateService.Object);
            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }
        [TestCaseSource(nameof(TestSearchOptions))]
        public void Should_ReturnResults_When_ValidSearchOptions(SearchOptions searchOptions)
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts, _mockValidateService.Object);
            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }
        [Test]
        public void Should_ThrowsArgumentException_When_SearchOptionsAreNull()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };
            _mockValidateService.Setup(t => t.ValidateSearchOptions(null)).Throws(new ArgumentException("searchOptions"));
            var searchEngine = new SearchEngine(shirts, _mockValidateService.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                searchEngine.Search(null);
            });
        }
        public static IEnumerable<object[]> TestSearchOptions = new List<object[]>
        {
            new object[] {new SearchOptions{Sizes = new List<Size> { Size.Large }}},
            new object[] {new SearchOptions{Colors = new List<Color> {Color.Yellow}}},
            new object[] {new SearchOptions{Colors = new List<Color> {Color.Red},Sizes = new List<Size> {Size.Small}}},
            new object[] {new SearchOptions{Colors = new List<Color> {Color.Red, Color.Black},Sizes = new List<Size> {Size.Medium}}},
            new object[] {new SearchOptions{Colors = new List<Color> {Color.Red},Sizes = new List<Size> {Size.Small, Size.Large}}},
            new object[] {new SearchOptions()}
        };

    }
}
