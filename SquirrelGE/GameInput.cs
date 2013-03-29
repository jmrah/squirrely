// GameInput.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Provides key up and key down information
//
// TODO: Try and implement this class with the windows 
//       GetAsyncKeyState or something similar to remove
//       dependency on the Game's form object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SquirrelGE
{   public class GameInput
    {   private Form form;
        private Keys lastKeyPressed = Keys.None;
        /// <summary>
        /// We're going to keep the key pressed in a queue, so the user can
        /// query which key was pressed and in which order, if needed.
        /// </summary>
        private List<Keys> keysPressed = new List<Keys>();
        private Keys keyUp = Keys.None;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);
        
        /// <summary>
        /// Gets the first key that was pressed
        /// </summary>
        public Keys KeyDown
        {   get
            {
                if (keysPressed.Count > 0) 
                { 
                    // if key at keysPressed(0) is not down
                    // if high order bit is set, key = down, else key = up
                    if ((GetKeyState((int)keysPressed[0]) & 0x8000) != 0x8000)
                    {
                        //Keys key = keysPressed[0];
                        keysPressed.Remove(keysPressed[0]);
                        return Keys.None;
                    }
                    return keysPressed[0]; 
                }
                else { return Keys.None; }
            }
        }
        
        /// <summary>
        /// Gets the last key that was let up
        /// </summary>
        public Keys KeyUp
        {   get 
            {
                return keyUp; 
            }
        }
        
        // This really should be here.  I shouldn't be passing a Form
        // from my Game.  I needed a quick solution at the time because
        // the assignment was getting marked.
        internal GameInput(Form form)
        {
            this.form = form;
            form.KeyDown += new KeyEventHandler(OnKeyDown);
            form.KeyUp += new KeyEventHandler(OnKeyUp);
        }
        private void OnKeyDown(Object sender, KeyEventArgs e)
        {
            if (lastKeyPressed != e.KeyCode)
            {   lastKeyPressed = e.KeyCode;
                keysPressed.Add(e.KeyCode);
            }
        }

        private void OnKeyUp(Object sender, KeyEventArgs e)
        {   keyUp = e.KeyCode;
            keysPressed.Remove(e.KeyCode);
            if (lastKeyPressed == e.KeyCode) { lastKeyPressed = Keys.None; }
        }
    }
}
