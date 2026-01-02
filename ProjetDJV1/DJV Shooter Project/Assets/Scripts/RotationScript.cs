using UnityEngine;

public class PlayerRotationTPS : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float rotationSpeed = 720f;

    void Update()
    {


        Vector3 camForward = cam.transform.forward;
        camForward.y = 0f; 


        if (camForward.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(camForward);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}