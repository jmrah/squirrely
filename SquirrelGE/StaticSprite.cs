// StaticSprite.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Base class for a static sprite.  Currently, provides
//           no extra functionality.  Perhaps in the future though.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SquirrelGE
{   public class StaticSprite : BitmapSprite
    {   public StaticSprite(Bitmap masterImage, Rectangle crop)
            : base(masterImage, crop)
        {
        }
    }
}
