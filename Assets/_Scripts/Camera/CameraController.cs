using System.Collections;
using UnityEngine;

namespace BWV
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float fieldOfView = 0.125f;

        public void RepositionCamera(Vector3 position, float fov)
        {
            Vector3 targetPosition = position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }

        void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target);
        }

        public IEnumerator RepositionCamera(Vector3 position, Vector3 offset)
        {
            Vector3 targetPosition = position + offset;
            Vector3 finalPosition = position;

            while (Vector3.Distance(this.transform.localPosition, finalPosition) > 0.0125f)
            {
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, finalPosition, Time.deltaTime * smoothSpeed);
                yield return null;
            }
            this.transform.position = finalPosition;
        }

        public IEnumerator AdjustPOV(float fov, float speed)
        {
            float startPosition = Camera.main.fieldOfView;
            float finalPosition = fov;

            while (Camera.main.fieldOfView != finalPosition)
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, finalPosition, Time.deltaTime * speed);
                yield return null;
            }            
        }
    }
}