// Level.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Loads a level from file and provides access to it's Sprites

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SquirrelGE;
using System.Drawing;

namespace SquirrelThePirate2
{   class Level
    {   public List<BitmapSprite> WaterSprites = new List<BitmapSprite>();
        public List<BitmapSprite> ShoreSprites = new List<BitmapSprite>();
        public List<BitmapSprite> RockSprites = new List<BitmapSprite>();
        public List<BitmapSprite> SandSprites = new List<BitmapSprite>();
        public List<BitmapSprite> BushSprites = new List<BitmapSprite>();
        public List<BitmapSprite> AcornSprites = new List<BitmapSprite>();
        public List<BitmapSprite> TreeSprites = new List<BitmapSprite>();
        public BitmapSprite SandSprite;
        public BitmapSprite GrassSprite;
        public List<Point> BushCoords = new List<Point>();
        public Squirrel Squirrel = null;

        private const int TileHeight = 20;
        private const int TileWidth = 20;
        private const int SpriteSheetTileWidth = 4;

        // Simply create a sand sprite...a bit hacky.  there's probably a better way
        public Level()
        {   const int SandTilePos = 15;
            int spriteSheetY = (SandTilePos / SpriteSheetTileWidth) * 
                TileHeight + (SandTilePos / SpriteSheetTileWidth);
            int spriteSheetX = (SandTilePos % SpriteSheetTileWidth) * 
                TileWidth + (SandTilePos % SpriteSheetTileWidth);
            SandSprite =  new StaticSprite(GameTextures.TileMap, new System.Drawing.Rectangle(spriteSheetX, spriteSheetY, TileWidth, TileHeight));
            GrassSprite = new StaticSprite(GameTextures.SquirrelMap, new Rectangle(350, 109, 55, 55));
        }
        
        // Need i/o error handling in the future.
        public static Level GetLevel(string file)
        {   
            Level level = new Level();
            level.WaterSprites.Clear();
            level.ShoreSprites.Clear();
            level.RockSprites.Clear();
            level.SandSprites.Clear();
            level.BushSprites.Clear();
            level.AcornSprites.Clear();
            level.Squirrel = null;

            StreamReader levelReader = new StreamReader(file);
            int mapTilePositionCounter = 0;

            while (!levelReader.EndOfStream)
            {   string levelLine = levelReader.ReadLine();

                foreach (char asciiTile in levelLine.ToCharArray())
                {   int spriteSheetTile = 0;
                    switch (asciiTile)
                    {   case '/': spriteSheetTile = 0; break;
                        case '\\': spriteSheetTile = 1; break;
                        case '>': spriteSheetTile = 2; break;
                        case '<': spriteSheetTile = 3; break;
                        case '-': spriteSheetTile = 4; break;
                        case ']': spriteSheetTile = 5; break;
                        case '_': spriteSheetTile = 6; break;
                        case '[': spriteSheetTile = 7; break;
                        case '(': spriteSheetTile = 8; break;
                        case '{': spriteSheetTile = 9; break;
                        case '}': spriteSheetTile = 10; break;
                        case ')': spriteSheetTile = 11; break;
                        case '^': spriteSheetTile = 12; break;
                        case '#': spriteSheetTile = 13; break;
                        case '|': spriteSheetTile = 14; break;
                        case '.': spriteSheetTile = 15; break;
                        case '@': spriteSheetTile = 16; break;
                        case '*': spriteSheetTile = 13; break;
                        default: spriteSheetTile = 9999; break;
                    }

                    int spriteSheetPos = spriteSheetTile;
                    // There is a 1 pixel spacer between each til in the sprite sheet,
                    // which is accounted for with the addition expression
                    int spriteSheetY = (spriteSheetPos / SpriteSheetTileWidth) * 
                        TileHeight + (spriteSheetPos / SpriteSheetTileWidth);
                    int spriteSheetX = (spriteSheetPos % SpriteSheetTileWidth) * 
                        TileWidth + (spriteSheetPos % SpriteSheetTileWidth);
                    int mapPosX = ((mapTilePositionCounter % TileWidth) * TileWidth);
                    int mapPosY = (mapTilePositionCounter / TileHeight) * TileHeight;

                    BitmapSprite sprite = new StaticSprite(
                        GameTextures.TileMap,
                        new System.Drawing.Rectangle(spriteSheetX, spriteSheetY, TileWidth, TileHeight));
                    sprite.X = mapPosX;
                    sprite.Y = mapPosY;

                    if (spriteSheetPos < 12)
                    {   level.ShoreSprites.Add(sprite);
                    }
                    else if (spriteSheetPos == 12)
                    {   level.WaterSprites.Add(sprite);
                    }
                    else if (spriteSheetPos == 13)
                    {   level.BushSprites.Add(sprite);

                        if (asciiTile == '*')
                        {   const int AcornPos = 16;
                            int spriteSheetOverlapY = 
                                (AcornPos / SpriteSheetTileWidth) * 
                                TileHeight + (AcornPos / SpriteSheetTileWidth);
                            int spriteSheetOverlapX = 
                                (AcornPos % SpriteSheetTileWidth) * 
                                20 + (AcornPos % 4);
                            int overlapMapPosX = 
                                (mapTilePositionCounter % TileWidth) * TileWidth;
                            int overlapMapPosY = 
                                (mapTilePositionCounter / TileHeight) * TileHeight;
                            BitmapSprite acorn = 
                                new StaticSprite(GameTextures.TileMap,
                                    new System.Drawing.Rectangle(spriteSheetOverlapX, spriteSheetOverlapY, TileWidth, TileHeight));
                            acorn.X = overlapMapPosX;
                            acorn.Y = overlapMapPosY;
                            level.AcornSprites.Add(acorn);
                            acorn.PixelsPerMove = TileWidth;
                        }
                    }
                    else if (spriteSheetPos == 14)
                    {   level.RockSprites.Add(sprite);
                    }
                    else if (spriteSheetPos == 15)
                    {   level.SandSprites.Add(sprite);
                    }
                    else if (spriteSheetPos == 16)
                    {   level.AcornSprites.Add(sprite);
                        sprite.PixelsPerMove = TileWidth;
                    }
                    else if (asciiTile == '$')
                    {   level.Squirrel = new Squirrel(GameTextures.SquirrelMap);
                        level.Squirrel.X = mapPosX;
                        level.Squirrel.Y = mapPosY;
                    }
                    else if (asciiTile == 'T')
                    {
                        StaticSprite tree = new StaticSprite(GameTextures.SquirrelMap, new Rectangle(416, 129, 56, 64));
                        tree.Anchor = new Rectangle(18, 42, 20, 20);
                        tree.X = mapPosX;
                        tree.Y = mapPosY;
                        level.TreeSprites.Add(tree);
                    }
                    mapTilePositionCounter++;
                }
            }

            foreach (BitmapSprite bush in level.BushSprites)
            {   level.BushCoords.Add(new Point((int)bush.X, (int)bush.Y));
            }

            return level;
        }
    }
}
