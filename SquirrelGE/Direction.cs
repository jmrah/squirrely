// Direction.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Represents up, down, left, right direction.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquirrelGE
{   public class Direction
    {   private readonly int xComponent;
        private readonly int yComponent;

        public static readonly Direction None;
        public static readonly Direction Up;
        public static readonly Direction Left;
        public static readonly Direction Down;
        public static readonly Direction Right;

        static Direction()
        {   None = new Direction(0, 0);
            Up = new Direction(0, -1);
            Left = new Direction(-1, 0);
            Down = new Direction(0, 1);
            Right = new Direction(1, 0);
        }
        private Direction(int xComponent, int yComponent)
        {   this.xComponent = xComponent;
            this.yComponent = yComponent;
        }

        public int XComponent
        {   get { return xComponent; }
        }
        public int YComponent
        {   get { return yComponent; }
        }
    }
}
