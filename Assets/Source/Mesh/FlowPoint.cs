using UnityEngine;

namespace Tool.Mesh
{
    public class FlowPoint : ObjectModel
    {
        public FlowMeshController Controller;
        public Transform[] Points;
        public float Speed, RotationSpeed;

        public override void Initialize()
        {
            base.Initialize();
        }

        public void FlowUpdate()
        {

        }

        public void OnComplete()
        {
            Controller.OnCompletePath();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Points != null)
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    if (i + 1 < Points.Length)
                    {
                        Gizmos.DrawLine(Points[i].position, Points[i + 1].position);
                    }
                }
            }
        }
#endif
    }
}