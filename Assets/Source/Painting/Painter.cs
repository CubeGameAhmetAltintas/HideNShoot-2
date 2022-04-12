using UnityEngine;

namespace Tool.Painter
{
    [System.Serializable]
    public class Painter
    {
        public Texture2D PaintedTexture;

        public void Paint(Vector2 coord, Vector2Int paintSize, Texture2D referance)
        {
            coord.x = (coord.x * PaintedTexture.width) - (paintSize.x / 2);
            coord.y = (coord.y * PaintedTexture.height) - (paintSize.y / 2);

            coord.x = coord.x < 0 ? 0 : coord.x;
            coord.y = coord.y < 0 ? 0 : coord.y;

            if (coord.x + paintSize.x < PaintedTexture.width && coord.y + paintSize.y < PaintedTexture.height)
            {
                PaintedTexture.SetPixels((int)coord.x, (int)coord.y, paintSize.x, paintSize.y, referance.GetPixels((int)coord.x, (int)coord.y, paintSize.x, paintSize.y));
                PaintedTexture.Apply();
            }
        }

        public void Paint(Vector2 coord, Vector2Int paintSize, Color[] colors)
        {
            coord.x = (coord.x * PaintedTexture.width) - (paintSize.x / 2);
            coord.y = (coord.y * PaintedTexture.height) - (paintSize.y / 2);

            coord.x = coord.x < 0 ? 0 : coord.x;
            coord.y = coord.y < 0 ? 0 : coord.y;

            if (coord.x + paintSize.x < PaintedTexture.width && coord.y + paintSize.y < PaintedTexture.height)
            {
                PaintedTexture.SetPixels((int)coord.x, (int)coord.y, paintSize.x, paintSize.y, colors);
                PaintedTexture.Apply();
            }
        }

        public void Stamp(Vector2 coord, Texture2D referance)
        {
            coord.x = (coord.x * PaintedTexture.width) - (referance.width / 2);
            coord.y = (coord.y * PaintedTexture.height) - (referance.height / 2);

            coord.x = coord.x < 0 ? 0 : coord.x;
            coord.y = coord.y < 0 ? 0 : coord.y;

            if (coord.x + referance.width < PaintedTexture.width && coord.y + referance.height < PaintedTexture.height)
            {
                PaintedTexture.SetPixels((int)coord.x, (int)coord.y, referance.width, referance.height, referance.GetPixels(0, 0, referance.width, referance.height));
                PaintedTexture.Apply();
            }
        }

        public void PaintAll(Texture2D referance)
        {
            PaintedTexture.SetPixels(0, 0, PaintedTexture.width, PaintedTexture.height, referance.GetPixels(0, 0, referance.width, referance.height));
            PaintedTexture.Apply();
        }

        public void PaintAll(Color[] colors)
        {
            PaintedTexture.SetPixels(0, 0, PaintedTexture.width, PaintedTexture.height, colors);
            PaintedTexture.Apply();
        }
    }
}