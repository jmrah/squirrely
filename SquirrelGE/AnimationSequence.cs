// AnimationSequence.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Provides a class to represent an AnimationSequence

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquirrelGE
{
    public class AnimationSequence
    {
        private static int totalObjects = 0;
        private static int currentId = -1;
        private static int lastId = -1;

        private List<AnimationFrame> animationFrames = new List<AnimationFrame>();
        private int nextFrame = 0;
        private int id = totalObjects++;

        public void AddFrame(AnimationFrame frame)
        {
            animationFrames.Add(frame);
        }

        public AnimationFrame GetNextFrame()
        {
            currentId = id;
            if (currentId != lastId)
            {
                nextFrame = 0;
                lastId = id;
            }

            AnimationFrame frame = animationFrames[nextFrame];
            nextFrame++;
            if (nextFrame >= animationFrames.Count)
            {
                nextFrame = 0;
            }
            return frame;
        }
    }
}
