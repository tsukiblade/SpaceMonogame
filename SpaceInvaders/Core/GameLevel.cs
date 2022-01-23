using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Core
{
    public class GameLevel
    {
        public string LevelName { get; set; }
        public string FileName { get; set; }
        public List<Entity.Entity> Entities { get; set; }
    }
}
