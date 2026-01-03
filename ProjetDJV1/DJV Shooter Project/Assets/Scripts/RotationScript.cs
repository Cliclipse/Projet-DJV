using UnityEngine;


//Je me suis aidé d'un peu d'IA pour le script de la caméra car j'arrivais pas à en avoir une fonctionnelle et dcp tout était injouable
public class Camera : MonoBehaviour
{
    [Header("Sensibilité de la souris")]
    public float mouseSensitivityX = 300f;

    [Header("Limites de rotation verticale")]
    public float minY = -30f;
    public float maxY = 60f;

    private float rotY = 0f; // rotation horizontale

    void Start()
    {
        // On lock la souris au centre
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.eulerAngles;
        rotY = angles.y;
    }

    void LateUpdate()
    {
        // Lecture de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;

        rotY += mouseX;


        // Applique la rotation au pivot
        transform.rotation = Quaternion.Euler(0f , rotY, 0f);
        
    }
}