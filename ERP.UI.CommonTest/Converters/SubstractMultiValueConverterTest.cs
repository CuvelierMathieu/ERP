using ERP.UI.Common.Converters;
using NUnit.Framework;
using System;
using System.Globalization;

namespace ERP.UI.CommonTest.Converters
{
    [TestFixture]
    public class SubstractMultiValueConverterTest
    {
        private static readonly Type DefaultTargetType = typeof(double);
        private static readonly object? DefaultParameter = null;
        private static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

        private readonly SubstractMultiValueConverter converter = new();

        [Test]
        public void ConvertNullArrayThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null, DefaultTargetType, DefaultParameter, DefaultCulture));
        }

        [Test]
        public void ConvertEmptyArrayThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => converter.Convert(Array.Empty<object>(), DefaultTargetType, DefaultParameter, DefaultCulture));
        }

        [Test]
        public void ConvertArrayWithAnUniqueValueThrowsArgumentException()
        {
            object[] array = new object[1];

            Assert.Throws<ArgumentException>(() => converter.Convert(array, DefaultTargetType, DefaultParameter, DefaultCulture));
        }

        private static object[] InputsAndExpectedResults()
        {
            return new object[]
            {
                (new object[] { 10, 2, 3, 4 }, 1.0),
                (new object?[] { 10, null, 3, 4 }, 3.0),
                (new object?[] { null, 1, 3, 4 }, -8.0),
                (new object[] { 10.0, 2m, 3f, "4", "5.0", "1,0", }, -5.0),
            };
        }

        [Test]
        [TestCaseSource(nameof(InputsAndExpectedResults))]
        public void ReturnsExpectedResult((object[] values, double expected) tuple)
        {
            object actual = converter.Convert(tuple.values, DefaultTargetType, DefaultParameter, DefaultCulture);

            Assert.AreEqual(actual, tuple.expected);
        }
    }
}
