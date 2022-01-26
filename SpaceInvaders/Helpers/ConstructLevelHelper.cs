using System;
using System.Globalization;
using System.IO;
using SpaceInvaders.Constants;
using SpaceInvaders.Core;

namespace SpaceInvaders.Helpers
{
    public static class ConstructLevelHelper
    {
        public static ValueObject GetValues(string str)
        {
            var obj = new ValueObject();
            ReadOnlySpan<char> span = str;
            var indexX = span.IndexOf('x');
            var indexY = span.IndexOf('y');
            var indexT = span.IndexOf('t');
            var indexD = span.IndexOf('d');
            var indexS = span.IndexOf('s');
            if (indexX == -1 || indexY == -1 || indexT == -1) throw new Exception("Invalid input.");

            if (indexD == -1)
                indexD = indexT + 2;
            else
                obj.DiffType = Convert.ToInt32(span[(indexD + 1)..].ToString());

            obj.X = float.Parse(span[(indexX + 1)..indexY].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            obj.Y = float.Parse(span[(indexY + 1)..indexT].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            obj.ObjType = Convert.ToInt32(span[(indexT + 1)..indexD].ToString());

            return obj;
        }

        public static string GetNextLevelPath(bool sameLevel = false)
        {
            //record score and get next level if exists
            var directory = Paths.LevelsPath;
            //var gameManager = GameManager.Instance;
            var scoreData = new ScoreData
            {
                Score = PlayerContext.Instance.Score
            };
            GameManager.ScoreData.Add(scoreData);
            var currentLevel = sameLevel ? GameManager.Instance.CurrentLevel - 1 : GameManager.Instance.CurrentLevel++;

            var suffixExtension = ".txt";
            var currentLevelFilePath = Directory.GetCurrentDirectory() + directory + currentLevel + suffixExtension;

            if (!File.Exists(currentLevelFilePath)) return null;

            return currentLevelFilePath;
        }

        public class ValueObject
        {
            public float X { get; set; }
            public float Y { get; set; }
            public int ObjType { get; set; }
            public int DiffType { get; set; }
        }
    }
}