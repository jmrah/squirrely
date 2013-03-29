// AnimationFrame.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Simply encapsulates a sprite rectangle and its anchor rectangle

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SquirrelGE
{
    public class AnimationFrame
    {
        private Rectangle sourceRectangle;
        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; }
        }

        private Rectangle anchorRectangle;
        public Rectangle AnchorRectangle
        {
            get { return anchorRectangle; }
            set { anchorRectangle = value; }
        }

        public AnimationFrame(Rectangle sourceRectangle, Rectangle anchorRectangle)
        {
            this.sourceRectangle = sourceRectangle;
            this.anchorRectangle = anchorRectangle;
        }
    }
}
