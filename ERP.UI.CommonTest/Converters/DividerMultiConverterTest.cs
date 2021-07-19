using ERP.UI.Common.Converters;
using NUnit.Framework;
using System;
using System.Globalization;

namespace ERP.UI.CommonTest.Converters
{
    [TestFixture]
    public class DividerMultiConverterTest
    {
        private static readonly Type DefaultTargetType = typeof(double);
        private static readonly object DefaultParameter = null;
        private static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

        private DividerMultiConverter converter;

        [SetUp]
        public void InitializeConverter()
        {
            converter = new();
        }

        [Test]
        public void ConvertANullArrayThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null, DefaultTargetType, DefaultParameter, DefaultCulture));
        }

        [Test]
        public void ConvertAnEmptyArrayThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => converter.Convert(Array.Empty<object>(), DefaultTargetType, DefaultParameter, DefaultCulture));
        }

        [Test]
        public void ConvertAnArrayNotContainingExactlyTwoValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => converter.Convert(new object[3], DefaultTargetType, DefaultParameter, DefaultCulture));
        }

        private static object[] InputsAndExpectedResults()
        {
            return new object[]
            {
                (new object[] { 10, null }, (double?)null),
                (new object[] { null, 2 }, (double?)null),
                (new object[] { 10, 2 }, (double?)5.0),
                (new object[] { 10.0, 2m }, (double?)5.0),
                (new object[] { 12f, "4" }, (double?)3.0),
                (new object[] { "5.0", "-2,0" }, (double?)-2.5),
            };
        }

        [Test]
        [TestCaseSource(nameof(InputsAndExpectedResults))]
        public void ReturnsExpectedDivisionResult((object[] values, double? expected) tuple)
        {
            object actual = converter.Convert(tuple.values, DefaultTargetType, DefaultParameter, DefaultCulture);

            Assert.AreEqual(tuple.expected, actual);
        }

        [Test]
        public void ReturnsAStringIfAsked()
        {
            object actual = converter.Convert(new object[2] { 80, 8 }, typeof(string), DefaultParameter, DefaultCulture);

            Assert.True(actual is string);
        }
    }
}
