// Squirrel.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  The squirrel!!!!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquirrelGE;
using System.Drawing;

namespace SquirrelThePirate2
{
    class Squirrel : AnimatedSprite
    {   public const string UpAndStopped = "upAndStopped";
        public const string UpAndRunning = "upAndRunning";
        public const string RightAndStopped = "rightAndStopped";
        public const string RightAndRunning = "rightAndRunning";
        public const string LeftAndStopped = "leftAndStopped";
        public const string LeftAndRunning = "leftAndRunning";
        public const string DownAndStopped = "downAndStopped";
        public const string DownAndRunning = "downAndRunning";
        public const string Celebrating = "celebrating";

        public Squirrel(Bitmap masterImage)
            : base(masterImage)
        {   // 
            // The first rectangle is the bounding rectangle of the sprite in Nuts.png
            // The second rectangle is it's anchor rectangle.  The squirrel
            // is a different size in each animation, so there has to be a 
            // reference besides the top-left corner on where to draw the 
            // sprite
            //
            
            AnimationSequence upAndStopped = new AnimationSequence();
            upAndStopped.AddFrame(new AnimationFrame(new Rectangle(0, 12, 24, 36), new Rectangle(2, 9, 20, 20)));
            AnimationSequence upAndRunning = new AnimationSequence();
            upAndRunning.AddFrame(new AnimationFrame(new Rectangle(110, 7, 25, 39), new Rectangle(4, 14, 20, 20)));
            upAndRunning.AddFrame(new AnimationFrame(new Rectangle(59, 3, 18, 46), new Rectangle(-1, 18, 20, 20)));
            upAndRunning.AddFrame(new AnimationFrame(new Rectangle(28, 14, 24, 33), new Rectangle(2, 7, 20, 20)));

            AnimationSequence rightAndStopped = new AnimationSequence();
            rightAndStopped.AddFrame(new AnimationFrame(new Rectangle(1, 55, 35, 37), new Rectangle(10, 15, 20, 20)));
            AnimationSequence rightAndRunning = new AnimationSequence();
            rightAndRunning.AddFrame(new AnimationFrame(new Rectangle(51, 54, 41, 38), new Rectangle(20, 16, 20, 20)));
            rightAndRunning.AddFrame(new AnimationFrame(new Rectangle(93, 61, 49, 31), new Rectangle(22, 10, 20, 20)));
            rightAndRunning.AddFrame(new AnimationFrame(new Rectangle(146, 62, 44, 29), new Rectangle(19, 8, 20, 20)));

            AnimationSequence leftAndStopped = new AnimationSequence();
            leftAndStopped.AddFrame(new AnimationFrame(new Rectangle(1, 55, 35, 37), new Rectangle(5, 15, 20, 20)));
            AnimationSequence leftAndRunning = new AnimationSequence();
            leftAndRunning.AddFrame(new AnimationFrame(new Rectangle(51, 54, 41, 38), new Rectangle(1, 16, 20, 20)));
            leftAndRunning.AddFrame(new AnimationFrame(new Rectangle(93, 61, 49, 31), new Rectangle(7, 10, 20, 20)));
            leftAndRunning.AddFrame(new AnimationFrame(new Rectangle(146, 62, 44, 29), new Rectangle(5, 8, 20, 20)));

            AnimationSequence downAndStopped = new AnimationSequence();
            downAndStopped.AddFrame(new AnimationFrame(new Rectangle(147, 6, 30, 39), new Rectangle(2, 15, 20, 20)));
            AnimationSequence downAndRunning = new AnimationSequence();
            downAndRunning.AddFrame(new AnimationFrame(new Rectangle(256, 11, 22, 34), new Rectangle(1, 9, 20, 20)));
            downAndRunning.AddFrame(new AnimationFrame(new Rectangle(221, 0, 20, 50), new Rectangle(0, 20, 20, 20)));
            downAndRunning.AddFrame(new AnimationFrame(new Rectangle(184, 7, 22, 39), new Rectangle(1, 13, 20, 20)));

            AnimationSequence celebrating = new AnimationSequence();
            celebrating.AddFrame(new AnimationFrame(new Rectangle(205, 59, 30, 34), new Rectangle(2, 14, 20, 20)));
            celebrating.AddFrame(new AnimationFrame(new Rectangle(236, 58, 29, 35), new Rectangle(2, 15, 20, 20)));

            this.AddAnimationSequence(UpAndStopped, upAndStopped);
            this.AddAnimationSequence(UpAndRunning, upAndRunning);
            this.AddAnimationSequence(RightAndStopped, rightAndStopped);
            this.AddAnimationSequence(RightAndRunning, rightAndRunning);
            this.AddAnimationSequence(LeftAndStopped, leftAndStopped);
            this.AddAnimationSequence(LeftAndRunning, leftAndRunning);
            this.AddAnimationSequence(DownAndStopped, downAndStopped);
            this.AddAnimationSequence(DownAndRunning, downAndRunning);
            this.AddAnimationSequence(Celebrating, celebrating);

            this.Fps = 9;
            this.Speed = 0;
            this.Direction = Direction.Right;
            this.CurrentSequence = RightAndStopped;
            this.PixelsPerMove = 20; // 20 = the tile width/height in the game
        }
    }
}
