using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField] private Camera cameraOfScene;
    

    // Update is called once per frame
    void Update()
    {
        Ray ray = cameraOfScene.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(ray, out var x))
        {
            var mouseLookingPositionOnPlane = ray.GetPoint(x);
            var position = transform.position;

            var directionToTarget = targetPosition - position;

            var dot = Vector3.Dot(transform.forward, directionToTarget.normalized);
            var speedPenalty = (dot + 1f) / 2f;

            var newPosition = Vector3.MoveTowards(position, targetPosition, speedPenalty * speed * Time.deltaTime);
            _characterController.Move(newPosition - position);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(directionToTarget),
                angularSpeed * Time.deltaTime);
        }
        
    }
    
}
