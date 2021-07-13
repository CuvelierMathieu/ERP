using ERP.UI.Converters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace ERP.UITest.Converters
{
    [TestFixture]
    public class ListToSumConverterTest
    {
        public static object[] DoubleList = new object[]
        {
            new List<double>
            {
                1.2,
                3.4,
                5.6,
                7.89
            },
        };

        public static object[] IntList = new object[]
        {
            new List<int>
            {
                1,
                3,
                5,
                7
            },
        };

        public static object[] DecimalList = new object[]
        {
            new List<decimal>
            {
                1.2m,
                3.4m,
                5.6m,
                7.89m
            },
        };

        private static readonly List<double> DefaultValue = (List<double>)DoubleList[0];
        private static readonly Type DefaultTargetType = typeof(double);
        private const string DefaultParameter = null;
        private static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

        private ListToSumConverter _converter;

        [SetUp]
        public void InitializeConverter()
        {
            _converter = new();
        }

        [Test]
        [TestCase("")]
        [TestCase(1)]
        [TestCase(true)]
        public void ThrowsArgumentExceptionIfValueIsNotAIEnumerable(object value)
        {
            Assert.Throws<ArgumentException>(() => _converter.Convert(value, DefaultTargetType, DefaultParameter, DefaultCulture));
        }

        [Test]
        public void ReturnsZeroIfValueIsNull()
        {
            object actual = _converter.Convert(null, DefaultTargetType, DefaultParameter, DefaultCulture);
            double expected = 0;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(nameof(DoubleList))]
        public void ReturnsExpectedSum(List<double> valuesToBeAdded)
        {
            double expected = valuesToBeAdded.Sum();
            object actual = _converter.Convert(valuesToBeAdded, DefaultTargetType, DefaultParameter, DefaultCulture);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(nameof(IntList))]
        public void ReturnsExpectedSum(List<int> valuesToBeAdded)
        {
            int expected = valuesToBeAdded.Sum();
            object actual = _converter.Convert(valuesToBeAdded, DefaultTargetType, DefaultParameter, DefaultCulture);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(nameof(DecimalList))]
        public void ReturnsExpectedSum(List<decimal> valuesToBeAdded)
        {
            decimal expected = valuesToBeAdded.Sum();
            object actual = _converter.Convert(valuesToBeAdded, DefaultTargetType, DefaultParameter, DefaultCulture);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(nameof(DecimalList))]
        public void ReturnsDoubleIfAsked(List<decimal> valuesToBeAdded)
        {
            object actual = _converter.Convert(valuesToBeAdded, typeof(double), DefaultParameter, DefaultCulture);

            Assert.IsTrue(actual is double, $"Actual value type is {actual.GetType()}");
        }

        [Test]
        [TestCaseSource(nameof(IntList))]
        public void ReturnsDoubleIfAsked(List<int> valuesToBeAdded)
        {
            object actual = _converter.Convert(valuesToBeAdded, typeof(double), DefaultParameter, DefaultCulture);

            Assert.IsTrue(actual is double, $"Actual value type is {actual.GetType()}");
        }

        [Test]
        [TestCaseSource(nameof(DoubleList))]
        public void ReturnsDecimalIfAsked(List<double> valuesToBeAdded)
        {
            object actual = _converter.Convert(valuesToBeAdded, typeof(decimal), DefaultParameter, DefaultCulture);

            Assert.IsTrue(actual is decimal, $"Actual value type is {actual.GetType()}");
        }

        [Test]
        [TestCaseSource(nameof(IntList))]
        public void ReturnsDecimalIfAsked(List<int> valuesToBeAdded)
        {
            object actual = _converter.Convert(valuesToBeAdded, typeof(decimal), DefaultParameter, DefaultCulture);

            Assert.IsTrue(actual is decimal, $"Actual value type is {actual.GetType()}");
        }

        [Test]
        [TestCaseSource(nameof(DecimalList))]
        public void ReturnsIntIfAsked(List<decimal> valuesToBeAdded)
        {
            object actual = _converter.Convert(valuesToBeAdded, typeof(int), DefaultParameter, DefaultCulture);

            Assert.IsTrue(actual is int, $"Actual value type is {actual.GetType()}");
        }

        [Test]
        [TestCaseSource(nameof(DoubleList))]
        public void ReturnsIntIfAsked(List<double> valuesToBeAdded)
        {
            object actual = _converter.Convert(valuesToBeAdded, typeof(int), DefaultParameter, DefaultCulture);

            Assert.IsTrue(actual is int, $"Actual value type is {actual.GetType()}");
        }

        [Test]
        [TestCaseSource(nameof(DoubleList))]
        public void ReturnsStringIfAsked(List<double> valuesToBeAdded)
        {
            object actual = _converter.Convert(valuesToBeAdded, typeof(string), DefaultParameter, DefaultCulture);

            Assert.IsTrue(actual is string, $"Actual value type is {actual.GetType()}");
        }

        [Test]
        public void RoundsDownIfTargetTypeIsIntAndFirstDecimalDigitIsLesserThan5()
        {
            List<double> value = new() { 3.49 };
            int expected = 3;

            object actual = _converter.Convert(value, typeof(int), DefaultParameter, DefaultCulture);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RoundsUpIfTargetTypeIsIntAndFirstDecimalDigitIs5OrGreater()
        {
            List<double> value = new() { 3.5 };
            int expected = 4;

            object actual = _converter.Convert(value, typeof(int), DefaultParameter, DefaultCulture);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void ThrowAnArgumentExceptionIfValueIsAComplexObjectAndNoParameterWasGiven(string parameter)
        {
            List<ComplexObjectForTestingListToSumConverter> list = new()
            {
                new() { Value = 3.14 }
            };

            Assert.Throws<ArgumentException>(() => _converter.Convert(list, DefaultTargetType, parameter, DefaultCulture));
        }

        [Test]
        public void ReturnsExpectedResultForComplexObjectsList()
        {
            List<ComplexObjectForTestingListToSumConverter> list = DefaultValue
                .Select(d => new ComplexObjectForTestingListToSumConverter() { Value = d })
                .ToList();

            double expected = list.Sum(o => o.Value);

            object actual = _converter.Convert(list, DefaultTargetType, nameof(ComplexObjectForTestingListToSumConverter.Value), DefaultCulture);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConverterOnAComplexObjectListTakesMaxTenTimesMoreTimeThanLinq()
        {
            List<ComplexObjectForTestingListToSumConverter> list = DefaultValue
                .Select(d => new ComplexObjectForTestingListToSumConverter() { Value = d })
                .ToList();

            Stopwatch stopwatch = new();

            stopwatch.Start();
            double a = list.Sum(o => o.Value);
            stopwatch.Stop();

            long elapsedTimeForLinq = stopwatch.ElapsedTicks;
            long maxAcceptableTime = elapsedTimeForLinq * 10;

            stopwatch.Restart();
            object b = _converter.Convert(list, DefaultTargetType, nameof(ComplexObjectForTestingListToSumConverter.Value), DefaultCulture);
            stopwatch.Stop();

            long elapsedTimeForConverter = stopwatch.ElapsedTicks;

            Assert.LessOrEqual(elapsedTimeForConverter, maxAcceptableTime);
        }
    }

    public class ComplexObjectForTestingListToSumConverter
    {
        public double Value { get; set; }
    }
}
