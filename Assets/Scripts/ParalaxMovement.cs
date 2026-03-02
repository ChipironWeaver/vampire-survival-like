using UnityEngine;

public class ParalaxMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 velocityMultiplier = Vector3.one;

    void FixedUpdate()
    {
        GetComponent<Transform>().position = new  Vector3(target.position.x * velocityMultiplier.x, target.position.y * velocityMultiplier.y, target.position.z * velocityMultiplier.z);
    }
}