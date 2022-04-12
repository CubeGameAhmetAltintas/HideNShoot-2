using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool.Mesh
{
    [System.Serializable]
    public class MeshCopierModel
    {
        public UnityEngine.Mesh Target;
        public MeshModel MeshModel;

        public void CopyModel(UnityEngine.Mesh target)
        {
            MeshModel.CreateMesh(target.vertices, target.triangles, target.normals, target.uv);
        }

        public void CopyModel()
        {
            MeshModel.CreateMesh(Target.vertices, Target.triangles, Target.normals, Target.uv);
        }
    }
}