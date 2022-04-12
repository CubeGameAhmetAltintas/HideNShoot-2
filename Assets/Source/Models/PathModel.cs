using UnityEngine;

namespace Tool.Path
{
    [System.Serializable]
    public class PathModel
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 EulerAngles;

        public PathModel()
        {
            Position = Vector3.zero;
            SetRotation(Quaternion.identity);
        }

        public void SetRotation(Quaternion rot)
        {
            Rotation = rot;
            EulerAngles = Rotation.eulerAngles;
        }
    }
}