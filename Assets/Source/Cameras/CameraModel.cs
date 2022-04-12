using UnityEngine;


namespace Cameras
{
    public class CameraModel : MonoBehaviour
    {
        public CameraTargetModel FollowTarget;
        public CameraTargetModel LookTarget;
        public Transform Pivot;
        public Camera Camera;

        public float FollowSpeed;
        public LerpTypes FollowType;
        public Vector3 FollowOffset;
        Vector3 followPoint;

        float shakeDuration;
        float shakeValue;
        bool isShaking;
        Vector3 shakePos;

        public float LookSpeed;
        public LerpTypes LookType;
        public Vector3 LookOffset;
        Vector3 lookPoint;

        Vector3 leftMouseStartPosition;
        Vector3 rightMouseStartPosition;

        public float zoomValue = 1;
        [SerializeField] float rotateAroundAngle = 0;

        private void Update()
        {
            cameraUpdate();
            inputUpdates();
        }

        private void cameraUpdate()
        {
            followUpdate();
            lookUpdate();
            shakeUpdate();
        }

        private void followUpdate()
        {
            switch (FollowType)
            {
                case LerpTypes.None:
                    followPoint = FollowTarget.GetPosition(this) + shakePos + (transform.forward * zoomValue);
                    transform.position = followPoint;
                    break;
                case LerpTypes.Lerp:
                    followPoint = Vector3.Lerp(transform.position, FollowTarget.GetPosition(this) + shakePos + (transform.forward * zoomValue), FollowSpeed);
                    transform.position = followPoint;
                    break;
                case LerpTypes.MoveTowards:
                    followPoint = Vector3.MoveTowards(transform.position, FollowTarget.GetPosition(this) + shakePos + (transform.forward * zoomValue), FollowSpeed);
                    transform.position = followPoint;
                    break;
            }

        }

        public void SetShake(float duration, float shakeValue)
        {
            shakeDuration = duration;
            this.shakeValue = shakeValue;
            isShaking = true;
        }

        private void shakeUpdate()
        {
            if (isShaking)
            {
                shakePos = Random.insideUnitSphere * shakeValue * Time.deltaTime;
                if (shakeDuration > 0)
                {
                    shakeDuration -= Time.deltaTime;
                }

                if (shakeDuration <= 0)
                {
                    onShakeEnd();
                }
            }
        }

        private void onShakeEnd()
        {
            shakeDuration = 0;
            isShaking = false;
            shakePos = Vector3.zero;
        }

        private void lookUpdate()
        {
            switch (LookType)
            {
                case LerpTypes.None:
                    lookPoint = LookTarget.GetLookPosition(this) + (transform.forward * zoomValue);
                    transform.LookAt(lookPoint);
                    break;
                case LerpTypes.Lerp:
                    lookPoint = Vector3.Lerp(lookPoint, LookTarget.GetLookPosition(this), LookSpeed) + (transform.forward * zoomValue);
                    transform.LookAt(lookPoint);
                    break;
                case LerpTypes.MoveTowards:
                    lookPoint = Vector3.MoveTowards(lookPoint, LookTarget.GetLookPosition(this), LookSpeed) + (transform.forward * zoomValue);
                    transform.LookAt(lookPoint);
                    break;
            }
        }

        private void movement(Vector2 dir)
        {
            FollowOffset += new Vector3(dir.x, 0, dir.y) * 5 * Time.deltaTime;
        }

        private void rotateAround()
        {

        }

        private void zoom()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                zoomValue += 5 * Time.deltaTime;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                zoomValue -= 5 * Time.deltaTime;
            }
        }

        private void inputUpdates()
        {
            if (Input.GetMouseButtonDown(0))
            {
                leftMouseStartPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonDown(0))
            {
                rightMouseStartPosition = Input.mousePosition;
            }

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetMouseButton(0))
                {
                    movement((leftMouseStartPosition - Input.mousePosition).normalized);
                }

                if (Input.GetMouseButton(1))
                {
                    rotateAround();
                }

                zoom();
            }
        }

        public void ResetPosition()
        {
            followPoint = FollowTarget.GetPosition(this) + shakePos + (transform.forward * zoomValue);
            transform.position = followPoint;
        }

    }
}