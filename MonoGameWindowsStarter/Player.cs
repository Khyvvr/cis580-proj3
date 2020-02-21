using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// defines possible states for player to be in
    /// </summary>
    enum PlayerState
    {
        Idle,
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        Ghost
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

        TimeSpan animationTimer;                    // timer for animation duration

        SpriteEffects spriteEffects = SpriteEffects.None;       // current sprite effects

        Color color = Color.White;                  // color of the sprite
        Vector2 origin = new Vector2(45, 85);          // origin/center of sprite

        public Vector2 Position = new Vector2(100, 100);    // get and sets position of player on screen

        public BoundingRectangle Bounds => new BoundingRectangle(Position - 1.8f * origin, 44, 44);

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

            if (keyboard.IsKeyDown(Keys.Left))
            {
                state = PlayerState.MoveLeft;
                Position.X -= playerSpeed;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                state = PlayerState.MoveRight;
                Position.X += playerSpeed;
            }
            else if (keyboard.IsKeyDown(Keys.Up))
            {
                state = PlayerState.MoveUp;
                Position.Y -= playerSpeed;
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                state = PlayerState.MoveDown;
                Position.Y += playerSpeed;
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
                case PlayerState.MoveLeft:
                    animationTimer += gameTime.ElapsedGameTime;
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    // Walking frames are 1 & 2
                    currentFrame = (int)animationTimer.TotalMilliseconds / FRAME_RATE + 1;
                    if (animationTimer.TotalMilliseconds > FRAME_RATE * 2)
                    {
                        animationTimer = new TimeSpan(0);
                    }
                    break;
                case PlayerState.MoveRight:
                    animationTimer += gameTime.ElapsedGameTime;
                    spriteEffects = SpriteEffects.None;
                    // Walking frames are 1 & 2
                    currentFrame = (int)animationTimer.TotalMilliseconds / FRAME_RATE + 1;
                    if (animationTimer.TotalMilliseconds > FRAME_RATE * 2)
                    {
                        animationTimer = new TimeSpan(0);
                    }
                    break;
                case PlayerState.MoveUp:
                    animationTimer += gameTime.ElapsedGameTime;
                    spriteEffects = SpriteEffects.None;
                    // Walking frames are 1 & 2
                    currentFrame = (int)animationTimer.TotalMilliseconds / FRAME_RATE + 1;
                    if (animationTimer.TotalMilliseconds > FRAME_RATE * 2)
                    {
                        animationTimer = new TimeSpan(0);
                    }
                    break;
                case PlayerState.MoveDown:
                    animationTimer += gameTime.ElapsedGameTime;
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    // Walking frames are 1 & 2
                    currentFrame = (int)animationTimer.TotalMilliseconds / FRAME_RATE + 1;
                    if (animationTimer.TotalMilliseconds > FRAME_RATE * 2)
                    {
                        animationTimer = new TimeSpan(0);
                    }
                    break;
            }
        }

        public void CheckForBarrierCollision(IEnumerable<IBoundable> barriers)
        {
            Debug.WriteLine($"Checking fo barrier collisions against {barriers.Count()} barriers");
            foreach(Barrier barrier in barriers)
            {
                if (Bounds.Collides(barrier.Bounds, barrier.Bounds.X))
                {
                    Position.X -= 1;
                }

                if (Bounds.Collides(barrier.Bounds, barrier.Bounds.Y))
                {
                    Position.Y -= 1;
                }

                if (Bounds.Collides(barrier.Bounds, barrier.Bounds.X + barrier.Bounds.Width))
                {
                    Position.X += 1;
                }

                if (Bounds.Collides(barrier.Bounds, barrier.Bounds.Y + barrier.Bounds.Height))
                {
                    Position.Y += 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            frames[currentFrame].Draw(spriteBatch, Position, color, 0, origin, 2, spriteEffects, 1);
        }
    }
}
