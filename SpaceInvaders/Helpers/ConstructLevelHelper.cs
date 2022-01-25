﻿using SpaceInvaders.Constants;
using SpaceInvaders.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SpaceInvaders.Helpers
{
    public static class ConstructLevelHelper
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
            int indexS = span.IndexOf('s');
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

        public static string GetNextLevelPath(bool sameLevel = false)
        {
            //record score and get next level if exists
            string directory = Paths.LevelsPath;
            //var gameManager = GameManager.Instance;
            ScoreData scoreData = new ScoreData()
            {
                Score = PlayerContext.Instance.Score,
            };
            GameManager.ScoreData.Add(scoreData);
            int currentLevel = sameLevel ? GameManager.CurrentLevel - 1 : GameManager.CurrentLevel++;

            string suffixExtension = ".txt";
            string currentLevelFilePath = Directory.GetCurrentDirectory() + directory + currentLevel + suffixExtension;

            if (!File.Exists(currentLevelFilePath))
            {
                return null;
            }

            return currentLevelFilePath;
        }
    }
}
