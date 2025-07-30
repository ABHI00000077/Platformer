using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                         // The player to follow
    public Vector3 offset = new Vector3(0f, 1.5f, -10f);  // Camera position offset

    void LateUpdate()
    {
        if (target == null) return;

        // Instantly follow the target with offset
        transform.position = target.position + offset;
    }

    // Optional method for runtime target assignment
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
