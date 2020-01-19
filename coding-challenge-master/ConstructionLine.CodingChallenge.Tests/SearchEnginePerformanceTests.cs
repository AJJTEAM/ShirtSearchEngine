using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConstructionLine.CodingChallenge.Services;
using ConstructionLine.CodingChallenge.Tests.SampleData;
using Moq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEnginePerformanceTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;
        private SearchEngine _searchEngine;
        private Mock<IValidationService> _mockValidateService; 
        [SetUp]
        public void Setup()
        {
            _mockValidateService = new Mock<IValidationService>();
            var dataBuilder = new SampleDataBuilder(5000000);

            _shirts = dataBuilder.CreateShirts();

            _searchEngine = new SearchEngine(_shirts, _mockValidateService.Object);
        }


        [Test]
        public void PerformanceTest()
        {
            var sw = new Stopwatch();
            sw.Start();

            var options = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var results = _searchEngine.Search(options);

            sw.Stop();
            Console.WriteLine($"Test fixture finished in {sw.ElapsedMilliseconds} milliseconds");

            AssertResults(results.Shirts, options);
            AssertSizeCounts(_shirts, options, results.SizeCounts);
            AssertColorCounts(_shirts, options, results.ColorCounts);
        }
    }
}
