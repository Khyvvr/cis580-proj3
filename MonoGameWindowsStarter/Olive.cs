using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MonoGameWindowsStarter
{
    enum OliveAnimState
    {
        Idle,
        Moving
    }
    public class Olive
    {
        Random random = new Random();
        const int FRAME_RATE = 300;

        Sprite[] frames;

        int currentFrame = 0;

        OliveAnimState animationState = OliveAnimState.Moving;

        int speed = 5;

        TimeSpan animationTimer;

        SpriteEffects spriteEffects = SpriteEffects.None;

        Color color = Color.White;

        Vector2 origin = new Vector2(44, 44);

        public Vector2 Position = new Vector2(400, 400);

        public BoundingRectangle Bounds => new BoundingRectangle(Position - 1.8f * origin, 75, 75);

        public Olive(IEnumerable<Sprite> frames)
        {
            this.frames = frames.ToArray();
            animationState = OliveAnimState.Moving;
        }

        public void Update(GameTime gameTime)
        {
            if (animationState == OliveAnimState.Moving)
            {
                Position += (float)gameTime.ElapsedGameTime.TotalMilliseconds * new Vector2(
                                                                                        (float)random.NextDouble(),
                                                                                        (float)random.NextDouble());
            }

            switch (animationState)
            {
                case OliveAnimState.Idle:
                    currentFrame = 0;
                    animationTimer = new TimeSpan(0);
                    break;
                case OliveAnimState.Moving:
                    animationTimer += gameTime.ElapsedGameTime;

                    if (animationTimer.TotalMilliseconds > FRAME_RATE * 2)
                    {
                        animationTimer = new TimeSpan(0);
                    }
                    currentFrame = (int)Math.Floor(animationTimer.TotalMilliseconds / FRAME_RATE) + 1;
                    break;
            }
        }

        public bool CheckForPlayerCollision(Player player)
        {
            Debug.WriteLine($"Checking collisions against {player} player");
            if (Bounds.CollidesWith(player.Bounds))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
#if VISUAL_DEBUG
            VisualDebugging.DrawRectangle(spriteBatch, Bounds, Color.White);
#endif
            frames[currentFrame].Draw(spriteBatch, Position, color, 0, origin, 2, spriteEffects, 1);
        }
    }
}
