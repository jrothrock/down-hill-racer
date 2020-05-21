using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform camera;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = camera.position + offset;
    }
}
