using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool.Painter
{

    [System.Serializable]
    public class MeshPainter
    {
        public Painter Painter;
        public Vector2Int PaintSize;
        public Vector3 Offset;
        public LayerMask Layer;

        public void CloneAndSetMesVisual(Renderer renderer)
        {
            Material material = Object.Instantiate(renderer.material);
            Painter.PaintedTexture = Object.Instantiate(material.mainTexture) as Texture2D;
            material.mainTexture = Painter.PaintedTexture;
            renderer.material = material;
        }

        public Vector3 PaintWithRaycast(Camera cam, Vector3 screenPos, Texture2D referance)
        {
            Ray ray = cam.ScreenPointToRay(screenPos + Offset);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, Layer))
            {
                Painter.Paint(hit.textureCoord, PaintSize, referance);
            }

            return hit.point;
        }


        public Vector3 PaintWithRaycast(Camera cam, Vector3 screenPos, Color[] colors)
        {
            Ray ray = cam.ScreenPointToRay(screenPos + Offset);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, Layer))
            {
                Painter.Paint(hit.textureCoord, PaintSize, colors);
            }

            return hit.point;
        }

        public Vector3 PaintWithRaycast(Vector3 origin, Vector3 direction, Texture2D referance)
        {
            Ray ray = new Ray(origin,direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, Layer))
            {
                Painter.Paint(hit.textureCoord, PaintSize, referance);
            }

            return hit.point;
        }

        public Vector3 PaintWithRaycast(Vector3 origin, Vector3 direction, Color[] colors)
        {
            Ray ray = new Ray(origin, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, Layer))
            {
                Painter.Paint(hit.textureCoord, PaintSize, colors);
            }

            return hit.point;
        }


        public Vector3 StampWithRaycast(Camera cam, Vector3 screenPos, Texture2D referance)
        {
            Ray ray = cam.ScreenPointToRay(screenPos + Offset);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, Layer))
            {
                Painter.Stamp(hit.textureCoord, referance);
            }

            return hit.point;
        }

    }
}