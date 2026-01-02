using UnityEngine;

public class PlayerRotationTPS : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float rotationSpeed = 720f;

    void Update()
    {


        Vector3 camForward = cam.transform.forward;
        camForward.y = 0f; // IMPORTANT


        if (camForward.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(camForward);

        Debug.DrawRay(transform.position, Vector3.forward * 2f, Color.red);
        Debug.Log("Target rotation: " + targetRotation.eulerAngles);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}