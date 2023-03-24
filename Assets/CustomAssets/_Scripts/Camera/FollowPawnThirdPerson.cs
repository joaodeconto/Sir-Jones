using UnityEngine;


namespace BWV
{
    public class FollowPawnThirdPerson : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -10f);
        [SerializeField] private float smoothTime = 0.3f;

        private Vector3 velocity = Vector3.zero;

        private void LateUpdate()
        {
            if (target != null)
            {
                Vector3 desiredPosition = target.position + offset;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
                transform.position = smoothedPosition;
                transform.LookAt(target);
            }
        }
    }
}