using Microsoft.Xna.Framework;
using SpaceInvaders.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SpaceInvaders.Core
{
    public class LevelDirector
    {
        private ILevelBuilder _builder;

        public ILevelBuilder Builder
        {
            set => _builder = value;
        }

        public LevelDirector()
        {
        }

        public GameLevel ConstructLevel(string fileName)
        {
            //user fileName or smthing later
            string filePath = @"C:\Users\szl\Downloads\test.txt";
            IEnumerable<string> fileLines = File.ReadLines(filePath);
            foreach (string line in fileLines)
            {
                var values = ConstructLevelValues.GetValues(line);
                Vector2 vector; vector.X = values.X; vector.Y = values.Y;
                switch (values.ObjType)
                {
                    case 1:
                        _builder.BuildShip(vector, (DifficultyType)values.DiffType);
                        break;
                    case 2:
                        _builder.BuildAlien(vector, (DifficultyType)values.DiffType);
                        break;
                    case 3:
                        _builder.BuildObstacle(vector);
                        break;
                }
            }

            return _builder.GetCompleteLevel();
        }
    }
}
