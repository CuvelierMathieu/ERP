using ERP.UI.Common.Converters;
using System;
using System.Windows;

namespace ERP.UI.Common
{
    public static class ThicknessExtensions
    {
        public static double GetPart(this Thickness thickness, ThicknessPart part)
        {
            return part switch
            {
                ThicknessPart.Left => thickness.Left,
                ThicknessPart.Top => thickness.Top,
                ThicknessPart.Right => thickness.Right,
                ThicknessPart.Bottom => thickness.Bottom,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
