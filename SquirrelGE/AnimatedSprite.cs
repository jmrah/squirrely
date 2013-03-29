// AnimatedSprite.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Provides a base class for an Animated Sprite

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SquirrelGE
{   public class AnimatedSprite : BitmapSprite
    {   private Dictionary<string, AnimationSequence> animationSequences = 
            new Dictionary<string, AnimationSequence>();
        private string currentSequence;
        private string lastSequence = "";
        private double totalElapsedSeconds = 0;
        private int fps = 1;

        public AnimatedSprite(Bitmap masterImage)
            : base(masterImage)
        {
        }
        
        public int Fps
        {   get { return fps; }
            set 
            { if (!this.IsMovementLocked) { fps = value; }
            }
        }
        public string CurrentSequence
        {
            get { return currentSequence; }
            set
            {   if (!this.IsMovementLocked) { currentSequence = value; }
            }
        }
    
        public void AddAnimationSequence(string sequenceKey, 
            AnimationSequence sequenceValue)
        {   animationSequences.Add(sequenceKey, sequenceValue);
        }

        public void Animate(double elapsedSeconds)
        {   totalElapsedSeconds += elapsedSeconds;

            if ((totalElapsedSeconds >= 1.0 / fps) || 
                (lastSequence != currentSequence))
            {   lastSequence = currentSequence;
                totalElapsedSeconds = 0;
                AnimationFrame frame = 
                    animationSequences[currentSequence].GetNextFrame();
                this.Crop = frame.SourceRectangle;
                this.Anchor = frame.AnchorRectangle;
            }
        }
    }
}
