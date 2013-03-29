// BitmapSprite.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Base class for all bitmap sprites - namely, AnimatedSprite,
//           and StaticSprite   

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SquirrelGE
{   public abstract class BitmapSprite
    {   
        /// <summary>
        /// The sprite will not be updatable when this flag is set.
        /// </summary>
        /// <remarks>
        /// If you are creating a tile based game where the sprite moves
        /// in increments, then this variable will make sure the sprite
        /// completes it's move/animation before chaning state.  
        /// This variable get's set upon movement, and clears after
        /// the sprite has moved pixelsPerMove</remarks>
        private bool isMovementLocked = false;

        private double x = 0;
        private double y = 0;
        private int speed = 0;
        private Direction direction = Direction.None;
        private Dictionary<Direction, Boolean> isMoveable =
            new Dictionary<Direction, Boolean>()
        {   { Direction.Up, false },
            { Direction.Right, false},
            { Direction.Down, false },
            { Direction.Left, false }
        };
        
        /// <summary>
        /// Multiplier to apply to the sprite image, affecting it's height
        /// </summary>
        private double sizeMultiplier = 1.0;

        /// <summary>
        /// How many pixels a sprite must move before allowing state change.
        /// Change this variable for tile based games.
        /// </summary>
        private int pixelsPerMove = 1;
        private double pixelsMoved = 0;

        /// <summary>
        /// The sprite sheet or tile sheet for this sprite. 
        /// </summary>
        private Bitmap masterImage;
        /// <summary>
        /// The cropped region of masterImage.  This will be what is drawn
        /// when rendering the sprite
        /// </summary>
        private Rectangle crop;
        /// <summary>
        /// The rectangle within crop that the sprite's x and y coords describe.
        /// </summary>
        /// <remarks>
        /// For simple sprites, the anchor will be the same as the crop.  
        /// However, sometimes the portion of the sprite you want to 
        /// interact with is a smaller portion of the sprite image.  
        /// That is what this variable describes.
        /// </remarks>
        private Rectangle anchor;
        private RotateFlipType rotateFlipType = 
            RotateFlipType.RotateNoneFlipNone;
        
        /// <summary>
        /// Created a BitmapSprite</summary>
        /// <param name="masterImage">
        /// The entire sprite image or the image that the sprite is contained
        /// in</param>
        /// <param name="crop">
        /// The cropped region of masterImage that the sprite will be drawn
        /// from.</param>
        protected BitmapSprite(Bitmap masterImage, Rectangle crop)
        {   this.masterImage = masterImage;
            this.crop = crop;
            anchor = new Rectangle(0, 0, crop.Width, crop.Height);
        }

        /// <summary>
        /// Created a BitmapSprite</summary>
        /// <param name="masterImage">
        /// The entire sprite image or the image that the sprite is contained
        /// in</param>
        protected BitmapSprite(Bitmap masterImage)
        {   this.masterImage = masterImage;
            crop = new Rectangle(0, 0, masterImage.Width, masterImage.Height);
            anchor = crop;
        }
        
        public double X
        {   get { return x; }
            set { x = value; }
        }        
        public double Y
        {   get { return y; }
            set { y = value; }
        }
        public Direction Direction
        {   get { return direction; }
            set
            {   if (!isMovementLocked) { direction = value; }
            }
        }
        public RotateFlipType RotateFlipType
        {   get { return rotateFlipType; }
            set
            {   if (!isMovementLocked) { rotateFlipType = value; }
            }
        }
        public int Speed
        {   get { return speed; }
            set 
            {   if (!isMovementLocked) { speed = value; }
            }
        }
        public double SizeMultiplier
        {
            get { return sizeMultiplier; }
            set { if (!isMovementLocked) sizeMultiplier = value; }
        }
        public int PixelsPerMove
        {   get { return pixelsPerMove; }
            set
            { if (!isMovementLocked) { pixelsPerMove = value; } 
            }
        }
        protected double PixelsMoved
        {   get { return pixelsMoved; }
            set
            {   pixelsMoved = value;
                if (pixelsMoved >= pixelsPerMove)
                {   pixelsMoved = 0;
                    isMovementLocked = false;

                    //
                    // Round x and y to the nearest multiple of pixelsPerMove
                    // Since x and y are a double type.
                    //

                    // Rounding x to the nearest multiple
                    int remainder = (int)x % pixelsPerMove;
                    if (remainder < pixelsPerMove / 2)
                    {   x = (int)x - remainder;
                    }
                    else
                    {   x = (int)x + (pixelsPerMove - remainder);
                    }

                    // Rounding y to the nearest multiple
                    remainder = (int)y % pixelsPerMove;
                    if (remainder < pixelsPerMove / 2)
                    {   y = (int)y - remainder;
                    }
                    else
                    {   y = (int)y + (pixelsPerMove - remainder);
                    }
                }
                else if (pixelsMoved != 0) { isMovementLocked = true; }
            }
        }

        /// <summary>
        /// Gets the in game position of the sprite's anchor rectangle.  The
        /// anchor rectangle is the rectangle that the user would most likely 
        /// want to interact with.
        /// </summary>
        public Rectangle GameAnchor
        {   get
            {   return new Rectangle((int)x,(int)y,
                    (int)(anchor.Width * sizeMultiplier), 
                    (int)(anchor.Height * sizeMultiplier));
            }
        }
        /// <summary>
        /// Gets the in game position of the sprite's bounding rectangle
        /// </summary>
        public Rectangle GameRectangle
        {   get
            {   return new Rectangle((int)(x - anchor.X),(int)(y - anchor.Y),
                    (int)(crop.Width * sizeMultiplier), 
                    (int)(crop.Height * sizeMultiplier));
            }
        }
        /// <summary>
        /// Gets or sets the sprite's anchor withing Crop
        /// </summary>
        public Rectangle Anchor
        {   get { return anchor; }
            set { anchor = value; }
        }
        /// <summary>
        /// Gets or sets the sprites crop region inside of MasterImage
        /// </summary>
        public Rectangle Crop
        {   get { return crop; }
            set { crop = value; }
        }
        protected bool IsMovementLocked
        {   get { return isMovementLocked; }
            set { isMovementLocked = value; }
        }
        internal Bitmap MasterImage
        {   get { return masterImage; }
        }

        public bool IsMoveable(Direction direction)
        {   return isMoveable[direction];
        }
        public void SetMoveable(Direction direction, bool value)
        {   isMoveable[direction] = value;
        }
        
        /// <summary>
        /// Moves a sprite based on the sprite's speed and direction
        /// </summary>
        /// <param name="elapsedSeconds">
        /// The time elapsed.  This is intended to be passed in from the game
        /// loop</param>
        /// <remarks>
        /// You must use this method to move your sprite if you want the
        /// sprite to not move during pixelsPerMove </remarks>
        public void Move(double elapsedSeconds)
        {   x += speed * elapsedSeconds * direction.XComponent;
            y += speed * elapsedSeconds * direction.YComponent;
            PixelsMoved += speed * elapsedSeconds;
        }

        // This is a hack to get something to work.  Phase out in future.
        public void ForceSpeed(int speed)
        {   this.speed = speed;
            pixelsMoved = pixelsPerMove;
        }
    }
}
