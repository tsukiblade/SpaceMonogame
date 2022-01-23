using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SpaceInvaders.Helpers
{
    public static class ConstructLevelValues
    {
        public class ValueObject
        {
            public float X { get; set; }
            public float Y { get; set; }
            public int ObjType { get; set; }
            public int DiffType { get; set; }
        }
        public static ValueObject GetValues(string str)
        {
            var obj = new ValueObject();
            ReadOnlySpan<char> span = str;
            int indexX = span.IndexOf('x');
            int indexY = span.IndexOf('y');
            int indexT = span.IndexOf('t');
            int indexD = span.IndexOf('d');
            if(indexX == -1 || indexY == -1 || indexT == -1)
            {
                throw new Exception("Invalid input.");
            }

            if(indexD == -1)
            {
                indexD = indexT + 2;
            }
            else
            {
                obj.DiffType = Convert.ToInt32(span[(indexD + 1)..].ToString());
            }

            obj.X = float.Parse(span[(indexX + 1)..indexY].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            obj.Y = float.Parse(span[(indexY + 1)..indexT].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            obj.ObjType = Convert.ToInt32(span[(indexT + 1)..indexD].ToString());

            return obj;
        }
    }
}
