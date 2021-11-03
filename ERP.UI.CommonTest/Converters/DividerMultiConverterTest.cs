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
        private static readonly object DefaultParameter = new();
        private static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

        private readonly DividerMultiConverter converter = new();

        [Test]
        public void ConvertANullArrayThrowsArgumentNullException()
        {
#nullable disable warnings
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null, DefaultTargetType, DefaultParameter, DefaultCulture));
#nullable restore warnings
        }

        [Test]
        public void ConvertAnEmptyArrayThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => converter.Convert(Array.Empty<object>(), DefaultTargetType, DefaultParameter, DefaultCulture));
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void ConvertAnArrayNotContainingExactlyTwoValuesThrowsArgumentException(int length)
        {
            Assert.Throws<ArgumentException>(() => converter.Convert(new object[length], DefaultTargetType, DefaultParameter, DefaultCulture));
        }

        [Test]
        public void DivideByNullReturnsDefaultObject()
        {
#nullable disable warnings
            object actual = converter.Convert(new object[2] { 10, null }, DefaultTargetType, DefaultParameter, DefaultCulture);
#nullable restore warnings

            AssertIsADefaultObject(actual);
        }

        private void AssertIsADefaultObject(object obj)
        {
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.GetType().BaseType);
        }

        [Test]
        public void DivideByEmptyStringReturnsDefaultObject()
        {
            object actual = converter.Convert(new object[2] { 10, string.Empty }, DefaultTargetType, DefaultParameter, DefaultCulture);

            AssertIsADefaultObject(actual);
        }

        [Test]
        public void DivideNullByNotNullReturnsDefaultObject()
        {
#nullable disable warnings
            object actual = converter.Convert(new object[2] { null, 2 }, DefaultTargetType, DefaultParameter, DefaultCulture);
#nullable restore warnings

            AssertIsADefaultObject(actual);
        }

        [Test]
        public void DivideWhiteSpaceStringByNotNullReturnsDefaultObject()
        {
#nullable disable warnings
            object actual = converter.Convert(new object[2] { "   ", 2 }, DefaultTargetType, DefaultParameter, DefaultCulture);
#nullable restore warnings

            AssertIsADefaultObject(actual);
        }

        private static object[] InputsAndExpectedResults()
        {
            return new object[]
            {
                (new object[] { 10, 2 }, (double?)5.0),
                (new object[] { 10.0, 5m }, (double?)2.0),
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
