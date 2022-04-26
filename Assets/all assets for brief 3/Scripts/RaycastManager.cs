using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [Header("Raycasting info")]

    public RaycastHit GameObject_Clicked;
    public Ray Camera_Ray;
    public string GameObject_Clicked_Name;
    public float Layers_Effected;

    [Header("GameObject & Script references")]
    public FanScript Fan_Object;
    public BarLightScript Bar_Light_Object;
    public BarChair Bar_Chair_Object;
    public Telephone_Script Telephone_Object;
    public Vent_System_Script Vent_System_Object;
    public Camera Camera_Object;



    // Start is called before the first frame update
    void Start()
    {
        Layers_Effected = LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        Raycasting();
    }

    void Raycasting()
    {
        Camera_Ray = Camera_Object.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(Camera_Ray, out GameObject_Clicked, Layers_Effected))
        {
            GameObject_Clicked_Name = GameObject_Clicked.collider.name;

            FanClicked();
            BarLightClicked();
            BarChairClicked();
            TelephoneClicked();
            VentDoorClicked();
            VentClicked();

        }
    }

    void FanClicked()
    {
        if (GameObject_Clicked.collider.tag == "Fan")
        {
            Fan_Object.FanClickingInput();
        }
    }

    void BarLightClicked()
    {
        if (GameObject_Clicked.collider.tag == "BarLight")
        {
            Bar_Light_Object.BarLightInput();
        }
    }

    void BarChairClicked()
    {
        if (GameObject_Clicked.collider.tag == "BarChair" && Input.GetMouseButtonDown(0))
        {
            Bar_Chair_Object = GameObject_Clicked.collider.GetComponent<BarChair>();
            Bar_Chair_Object.ChairInput();
        }
    }

    void TelephoneClicked()
    {
        if(GameObject_Clicked.collider.tag == "Telephone")
        {
            Telephone_Object.TelephonePickUpInput();
        }
    }

    void VentDoorClicked()
    {
        if(GameObject_Clicked.collider.tag == "VentDoor")
        {
            Vent_System_Object.InputVentDoor();
        }
    }

    void VentClicked()
    {
        if (GameObject_Clicked.collider.tag == "Vent")
        {
            Vent_System_Object.InputTurningOnVentLights();
        }
        else
        {
            Vent_System_Object.EnableOrDisbaleVentLights(false);
        }
    }
}
