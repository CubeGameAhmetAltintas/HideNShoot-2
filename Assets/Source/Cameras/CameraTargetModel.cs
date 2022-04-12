using UnityEngine;


namespace Cameras
{
    public class CameraTargetModel : MonoBehaviour
    {
        public Vector3 GetPosition(CameraModel camera)
        {
            return transform.position + camera.FollowOffset;
        }

        public Vector3 GetLookPosition(CameraModel camera)
        {
            return transform.position + camera.LookOffset;
        }
    }

}