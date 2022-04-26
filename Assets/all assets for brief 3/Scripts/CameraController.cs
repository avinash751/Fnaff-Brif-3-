using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;

    [Header("Camera Rotation Info")]
    public float CameraSensetivity;
    public float cameraXClampvalue;
    public float cameraYaw;
    public float rotX;

    [Header("feild of view")]
    public float FOV;
    public bool  Allow_FOV_Change;
    // Start is called before the first frame update
    void Start()
    {
        Allow_FOV_Change = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseRotation();
        UpdateCameraFov();
    }
    void UpdateMouseRotation()
    {
        rotX += Input.GetAxis("Mouse X") * CameraSensetivity;
        rotX = Mathf.Clamp(rotX, -cameraXClampvalue, cameraXClampvalue);

        transform.localRotation = Quaternion.Euler(0f, rotX, 0f);
    }

    void UpdateCameraFov()
    {
        if(Allow_FOV_Change)
        {
            camera.fieldOfView = FOV;
            if (Input.GetMouseButton(0))
            {
                FOV -= 40 * Time.deltaTime * Mathf.SmoothStep(0, 1, 0.8f);
                FOV = Mathf.Clamp(FOV, 40, 60);
            }
            else if (Input.GetMouseButtonDown(0) == false)
            {
                FOV += 60 * Time.deltaTime * Mathf.SmoothStep(0, 1, 0.8f);
                FOV = Mathf.Clamp(FOV, 40, 60);
            }
        }
        
    }
}
