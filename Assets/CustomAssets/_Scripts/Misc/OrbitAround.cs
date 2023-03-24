using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    public Transform centerObject;
    public Vector3 axis = Vector3.up;
    public float distance = 9.0f;
    public float speed = 12.0f;

    void Start()
    {
        if (!centerObject)
        {
            centerObject = transform.parent;
        }
        transform.position = centerObject.position + Vector3.forward * distance;
    }

    void Update()
    {
        transform.RotateAround(centerObject.position, axis, speed * Time.deltaTime);
    }
}