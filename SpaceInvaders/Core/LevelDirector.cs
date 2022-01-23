using System;
using System.Collections.Generic;
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

            //deserializacja pliku
            //uzycie metod buildera krok po kroku
            //foreach (var VARIABLE in fileName)
            //{
            //    _builder.BuildEnemyAlien();
            //}
            return _builder.GetCompleteLevel();
        }
    }
}
