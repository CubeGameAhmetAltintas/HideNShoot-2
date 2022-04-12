using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool.Mesh
{
    [System.Serializable]
    public class MeshPointModel
    {
        public int VerticesIndex;
        public bool IsSelected;
        public Vector3 LocalPosition;
        public Vector3 Offset;

        public MeshPointModel(Vector3 pos)
        {
            LocalPosition = pos;
        }

        public MeshPointModel(Vector3 pos, float randomize)
        {
            LocalPosition = pos;
            Offset = Vector3.one * randomize;
        }

        public Vector3 GetPosition(Vector3 center, Vector3 scale)
        {
            return center + new Vector3(LocalPosition.x * scale.x, LocalPosition.y * scale.y, LocalPosition.z * scale.z) + Offset;
        }
    }
}