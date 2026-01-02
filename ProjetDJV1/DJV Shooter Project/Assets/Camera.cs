using UnityEngine;


//Je me suis aidé d'un peu d'IA pour le script de la caméra car j'arrivais pas à en avoir une fonctionnelle et dcp tout était injouable
public class Camera : MonoBehaviour
{
    [Header("Références")]
    public Transform player; // Le joueur à suivre

    [Header("Sensibilité de la souris")]
    public float mouseSensitivityX = 150f;
    public float mouseSensitivityY = 100f;

    [Header("Limites de rotation verticale")]
    public float minY = -30f;
    public float maxY = 60f;

    private float rotX = 0f; // rotation verticale
    private float rotY = 0f; // rotation horizontale

    void Start()
    {
        // On lock la souris au centre
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.eulerAngles;
        rotY = angles.y;
        rotX = angles.x;
    }

    void LateUpdate()
    {
        // Lecture de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        rotY += mouseX;
        rotX -= mouseY;

        // Limite la rotation verticale
        rotX = Mathf.Clamp(rotX, minY, maxY);

        // Applique la rotation au pivot
        transform.rotation = Quaternion.Euler(rotX, rotY, 0f);

        // Déplace le pivot avec le joueur
        transform.position = player.position;
    }
}