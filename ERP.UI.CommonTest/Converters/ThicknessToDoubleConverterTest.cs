using ERP.UI.Common.Converters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace ERP.UI.CommonTest.Converters
{
    [TestFixture]
    internal class ThicknessToDoubleConverterTest
    {
        private static readonly Type DefaultTargetType = typeof(double);
        private static readonly object? DefaultParameter = null;
        private static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

        private static object[] UniformLengthsSource() => new object[]
        {
            new object[] { 10.0 },
            new object[] { -10.0 },
            new object[] { 3.14 },
            new object[] { 0.0 },
        };

        private static object[] UniformLengthsAndPartsSource()
        {
            List<object> source = new();

            foreach (object[] item in UniformLengthsSource())
                foreach (ThicknessPart part in GetAllParts())
                    source.Add(new object[] { item[0], part });

            return source.ToArray();
        }

        private static IEnumerable<ThicknessPart> GetAllParts()
        {
            foreach (ThicknessPart item in Enum.GetValues(typeof(ThicknessPart)))
                yield return item;
        }

        private static object[] NonUniformLengthsAndPartsSource()
        {
            List<object> result = new();

            foreach (object[] value in UniformLengthsAndPartsSource())
            {
                double length = (double)value[0];

                result.Add(new object[]
                { 
                    length++,
                    length++,
                    length++,
                    length++,
                    value[1],
                });
            }

            return result.ToArray();
        }

        [Test]
        [TestCaseSource(nameof(UniformLengthsAndPartsSource))]
        public void ConvertUniformThickness(double uniformLength, ThicknessPart thicknessPart)
        {
            Thickness thickness = new(uniformLength);

            double expected = uniformLength;
            double actual = ConvertWithANewConverter<double>(thickness, thicknessPart);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertANonThicknessReturnsAnUnsetValue()
        {
            ColumnSpaceDistribution value = ColumnSpaceDistribution.Between;

            var expected = DependencyProperty.UnsetValue;
            object actual = ConvertWithANewConverter<object>(value);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IgnoreTargetTypeAndReturnADouble()
        {
            Thickness thickness = new(12.1);

            ThicknessToDoubleConverter converter = new();
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
            object result = converter.Convert(thickness, typeof(int), DefaultParameter, DefaultCulture);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

            Assert.True(result is double);
        }

        [Test]
        [TestCaseSource(nameof(NonUniformLengthsAndPartsSource))]
        public void ConvertNonUniformThickness(double left, double top, double right, double bottom, ThicknessPart thicknessPart)
        {
            Thickness thickness = new(left, top, right, bottom);

            double expected = thicknessPart switch
            {
                ThicknessPart.Left => left,
                ThicknessPart.Top => top,
                ThicknessPart.Right => right,
                ThicknessPart.Bottom => bottom,
                _ => throw new NotImplementedException("Unhandled part"),
            };

            double actual = ConvertWithANewConverter<double>(thickness, thicknessPart);

            Assert.AreEqual(expected, actual);
        }

        private static T ConvertWithANewConverter<T>(object value)
        {
            ThicknessToDoubleConverter converter = new();
            return ConvertWithDefaultValues<T>(converter, value);
        }

        private static T ConvertWithANewConverter<T>(object value, ThicknessPart thicknessPart)
        {
            ThicknessToDoubleConverter converter = new()
            {
                ThicknessPart = thicknessPart,
            };
            return ConvertWithDefaultValues<T>(converter, value);
        }

        private static T ConvertWithDefaultValues<T>(ThicknessToDoubleConverter converter, object value)
        {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
            object result = converter.Convert(value, DefaultTargetType, DefaultParameter, DefaultCulture);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

            return (T)result;
        }
    }
}
