using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter
{
    public class SpriteSheet
    {
        Texture2D sheet;                // sprite sheet's texture
        Sprite[] sprites;               // array of sprites from sprite sheet

        
        public SpriteSheet(Texture2D texture, int width, int height, int offset = 0, int gutter = 0)
        {
            sheet = texture;
            var col = (texture.width - offset) / (width + gutter);
            var rows = (texture.height - offset) / (height + gutter);
            sprites = new Sprite[rows * col];

            for (int y = 0; y < rows; y++)
            {
                for (int x =0; x < col; x++)
                {
                    sprites[y * col + x] = new Sprite(new Rectangle(
                        x * (width + gutter) + offset,
                        y * (height + gutter) + offset,
                        width,
                        height
                        ), texture
                    );
                }
            }
        }

        public Sprite this[int index]
        {
            get => sprites[index];
        }

        public int Count => sprites.Length;

    }
}
