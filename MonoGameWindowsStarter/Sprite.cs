using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// class to represent sprite artwork
    /// </summary>
    public struct Sprite
    {
        private Rectangle source;       // source rectangle of sprite
        private Texture2D texture;      // sprite's texture

        /// <summary>
        /// Sprite constructor
        /// </summary>
        /// <param name="rec"> source rectangle of sprite from spritesheet </param>
        /// <param name="texture"> the sprites texture </param>
        public Sprite(Rectangle rec, Texture2D texture)
        {
            this.source = rec;
            this.texture = texture;
        }

        public float GetWidth()
        {
            return this.texture.Width;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destRec, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture, destRec, source, color, rotation, origin, effects, layerDepth);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destRec, Color color)
        {
            spriteBatch.Draw(texture, destRec, source, color);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(texture, position, source, color);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture, position, source, color, rotation, origin, scale, effects, layerDepth);
        }
    }
}
