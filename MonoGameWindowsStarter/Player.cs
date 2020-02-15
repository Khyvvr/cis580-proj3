using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// defines possible states for player to be in
    /// </summary>
    enum PlayerState
    {
        Idle,
        MoveRight,
        MoveLeft,
        MoveUp,
        MoveDown,
        JumpRight,
        JumpLeft,
        Dead
    }

    /// <summary>
    /// 
    /// </summary>
    public class Player
    {
        const int FRAME_RATE = 400;                 // speed of player in milliseconds
        const int JUMP_TIMER = 500;                 // duration of player's jump in miliseconds

        Sprite[] frames;                            // player sprite frames

        int currentFrame = 0;                       // current frame
        PlayerState state = PlayerState.Idle;       // player's animation state initialized to idle
        int playerSpeed = 4;                        // player's speed

        bool isJumping = false;                     // bool to determine if player is jumping
        bool isFalling = false;                     // bool to determine if player is falling
        TimeSpan jumpTimer;                         // timer to determine jump duration
        TimeSpan animationTimer;                    // timer for animation duration

        SpriteEffects spriteEffects = SpriteEffects.None;       // current sprite effects

        Color color = Color.White;                  // color of the sprite
        Vector2 origin = new Vector2(x,y);          // origin/center of sprite

        public Vector2 Position = new Vector2(x, y);    // get and sets position of player on screen

        /// <summary>
        /// player constructor
        /// </summary>
        /// <param name="frames"> frames from spritesheet associated with player </param>
        public Player(IEnumerable<Sprite> frames)
        {
            this.frames = frames.ToArray();
            state = PlayerState.MoveRight;
        }

        public void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();         // gets the current state of the keyboard

            if (isJumping)
            {
                jumpTimer += gameTime.ElapsedGameTime;
                Position.Y -= (250 / (float)jumpTimer.TotalMilliseconds);

                if (jumpTimer.TotalMilliseconds >= JUMP_TIMER)
                {
                    isJumping = false;
                    isFalling = true;
                }
            }

            if (isFalling)
            {
                Position.Y += playerSpeed;

                if (Position.Y > 400)
                {
                    Position.Y = 400;
                    isFalling = false;
                }
            }

            if (!isJumping && !isFalling && keyboard.IsKeyDown(Keys.Space))
            {
                isJumping = true;
                jumpTimer = new TimeSpan(0);
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                if (isJumping || isFalling) state = PlayerState.JumpLeft;
                else state = PlayerState.MoveLeft;
                Position.X -= playerSpeed;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                if (isJumping || isFalling) state = PlayerState.JumpRight;
                else state = PlayerState.MoveRight;
                Position.X += playerSpeed;
            }
            else
            {
                state = PlayerState.Idle;
            }



            switch (state)
            {
                case PlayerState.Idle:
                    currentFrame = 0;
                    animationTimer = new TimeSpan(0);
                    break;
                case PlayerState.JumpLeft:
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    currentFrame = 7;
                    break;
                case PlayerState.JumpRight:
                    spriteEffects = SpriteEffects.None;
                    currentFrame = 7;
                    break;
                case PlayerState.MoveLeft:
                    animationTimer += gameTime.ElapsedGameTime;
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    // Walking frames are 9 & 10
                    currentFrame = (int)animationTimer.TotalMilliseconds / FRAME_RATE + 9;
                    if (animationTimer.TotalMilliseconds > FRAME_RATE * 2)
                    {
                        animationTimer = new TimeSpan(0);
                    }
                    break;
                case PlayerState.MoveRight:
                    animationTimer += gameTime.ElapsedGameTime;
                    spriteEffects = SpriteEffects.None;
                    // Walking frames are 9 & 10
                    currentFrame = (int)animationTimer.TotalMilliseconds / FRAME_RATE + 9;
                    if (animationTimer.TotalMilliseconds > FRAME_RATE * 2)
                    {
                        animationTimer = new TimeSpan(0);
                    }
                    break;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            frames[currentFrame].Draw(spriteBatch, Position, color, 0, origin, 2, spriteEffects, 1);
        }
    }
}
