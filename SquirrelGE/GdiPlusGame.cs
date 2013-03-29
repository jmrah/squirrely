// GdiPlusGame.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Implementation of a Game that uses the Gdi+ graphics.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SquirrelGE
{   internal class GdiPlusGame : Game
    {   private Form form;
        private Graphics graphics;
        private PictureBox pictureBox;

        /// <summary>
        /// Creates a Gdi+ Game</summary>
        /// <param name="title">the title to appear on the window</param>
        /// <param name="width">Width of the client in pixels</param>
        /// <param name="height">Height of the client in pixels</param>
        /// <returns>A Game</returns>
        internal GdiPlusGame(string title, int width, int height)
        {   CreateForm(title, width, height);
            CreateGraphics(width, height);
            Input = new GameInput(form);    //quick hack i need to fix later
        }
        private void CreateForm(string title, int width, int height)
        {   form = new Form();
            form.ClientSize = new Size(width, height);
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.Text = title;
            form.MaximizeBox = false;
        }
        private void CreateGraphics(int width, int height)
        {   pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Black;
            pictureBox.Parent = form;
            pictureBox.Dock = DockStyle.Fill;

            Bitmap canvas = new Bitmap(width, height);
            pictureBox.Image = canvas;
            graphics = Graphics.FromImage(canvas);

            // the image is blurry without this
			graphics.InterpolationMode = 
                System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            // pixels are cut off without this
			graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
        }

        public override int Width
        {   get { return form.ClientRectangle.Width; }
        }
        public override int  Height
        {   get { return form.ClientRectangle.Height; }
        }

        /// <see cref="Game"/>
        public override void Run(GameStateManager gameStateManager)
        {   base.Run(gameStateManager);

            //
            // Application.Run() is a blocking method.  We need to hook into it's 
            // Shown event to start the GameLoop AFTER it is up and running.
            //

            form.Shown += new EventHandler(OnFormShown);   
            form.FormClosed += new FormClosedEventHandler(OnFormClosed);
            Application.Run(form);          
        }
        private void OnFormShown(Object sender, EventArgs e)
        {   GameLoop.Start();
        }
        private void OnFormClosed(Object sender, FormClosedEventArgs e)
        {   GameLoop.Stop();
        }

        //
        // Graphics Functions
        //

        /// <see cref="Game"/>
        public override void Clear(Color color)
        {
            graphics.Clear(color);
        }

        public override void TileSprite(BitmapSprite sprite)
        {
            Bitmap temp = new Bitmap(sprite.Crop.Width, sprite.Crop.Height);
            Graphics g = Graphics.FromImage(temp);
            g.DrawImage(
                sprite.MasterImage, 
                new Rectangle(0, 0, temp.Width, temp.Height),
                sprite.Crop,
                GraphicsUnit.Pixel);
            TextureBrush tileBrush = new TextureBrush(temp, System.Drawing.Drawing2D.WrapMode.Tile);
            graphics.FillRectangle(tileBrush, form.ClientRectangle);
        }

        public override void RenderSprite(BitmapSprite sprite)
        {   if (sprite.RotateFlipType == RotateFlipType.RotateNoneFlipNone)
            {   graphics.DrawImage(
                    sprite.MasterImage,
                    sprite.GameRectangle,
                    sprite.Crop,
                    GraphicsUnit.Pixel);
            }
            else
            {   Bitmap tempImage = new Bitmap(sprite.Crop.Width, sprite.Crop.Height);
                Graphics tempGraphics = Graphics.FromImage(tempImage);
                tempGraphics.DrawImage(sprite.MasterImage, 
                    new Rectangle(0, 0, tempImage.Width, tempImage.Height), 
                    sprite.Crop,
                    GraphicsUnit.Pixel);
                tempImage.RotateFlip(sprite.RotateFlipType);
                graphics.DrawImage(tempImage, sprite.GameRectangle);
                tempGraphics.Dispose();
            }
        }

        //
        // I think i will need to abstract these draw string methods a bit 
        // more.  They were thrown in last minute.
        //
        
		public override void DrawString(
			string text,
			Font font,
			Brush brush,
			PointF point
		)
		{
			graphics.DrawString(text, font, brush, point);
		}

        public override void DrawString(
            string s,
            Font font,
            Brush brush,
            RectangleF layoutRectangle,
            StringFormat format
        )
        {
            graphics.DrawString(s, font, brush, layoutRectangle, format);
        }

        /// <see cref="Game"/>
        internal override void Refresh()
        {   pictureBox.Invalidate();
        }
    }
}