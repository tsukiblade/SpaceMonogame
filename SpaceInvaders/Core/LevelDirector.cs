using System.IO;
using Microsoft.Xna.Framework;
using SpaceInvaders.Helpers;

namespace SpaceInvaders.Core
{
    public class LevelDirector
    {
        private ILevelBuilder _builder;

        public ILevelBuilder Builder
        {
            set => _builder = value;
        }

        public GameLevel ConstructLevel(string fileName)
        {
            //user fileName or smthing later
            var filePath = @"test.txt";
            var fileLines = File.ReadLines(fileName);
            foreach (var line in fileLines)
            {
                var values = ConstructLevelHelper.GetValues(line);
                Vector2 vector;
                vector.X = values.X;
                vector.Y = values.Y;
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