using System;
using System.Collections.Generic;
using System.Text;
using ConstructionLine.CodingChallenge.Services;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class ValidationServiceTests
    {
        [Test]
        public void Should_ThrowArgumentException_When_SearchOptionsAreNull()
        {
            var validationService = new ValidationService();

            Assert.Throws<ArgumentException>(() =>
            {
                validationService.ValidateSearchOptions(null);
            });
        }
        [Test]
        public void Should_ThrowArgumentException_When_ColorSearchOptionsAreNull()
        {
            var searchOptions = new SearchOptions()
            {
                Colors = null
            };
            var validationService = new ValidationService();

            Assert.Throws<ArgumentException>(() =>
            {
                validationService.ValidateSearchOptions(searchOptions);
            });
        }
        [Test]
        public void Should_ThrowArgumentException_When_SizeSearchOptionsAreNull()
        {
            var searchOptions = new SearchOptions()
            {
                Sizes = null
            };
            var validationService = new ValidationService();

            Assert.Throws<ArgumentException>(() =>
            {
                validationService.ValidateSearchOptions(searchOptions);
            });
        }
    }
}
