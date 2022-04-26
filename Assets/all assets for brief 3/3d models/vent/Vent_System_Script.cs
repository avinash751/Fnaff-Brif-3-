using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_System_Script : MonoBehaviour
{
    public CameraController cameraObject;
    public bool interactable;

    [Header("Vent variables")]
    public bool Use_Vent_Enabled;
    public Light[] Vent_Lights;

    [Header("Vent Door variables")]
    public bool Vent_Door_Clicked;

    public Animator Vent_Door_Animator;
    public string[] VentDoorAnim;


    // Start is called before the first frame update
    void Start()
    {
        Vent_Door_Animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InputVentDoor()
    {
        if(Input.GetMouseButtonDown(0)&&!Vent_Door_Clicked && interactable)
        {
            PlayVentDoorAnimation(Vent_Door_Animator, 0, true);
            StartCoroutine(EnableOrDisableUseOfVent(0.3f, true));
            Vent_Door_Clicked = true;
        }
        else if(Input.GetMouseButtonDown(0) && Vent_Door_Clicked && interactable)
        {
            PlayVentDoorAnimation(Vent_Door_Animator, 1, true);
            StartCoroutine(EnableOrDisableUseOfVent(0.1f, false));
            Vent_Door_Clicked = false;
        }
    }

    public void InputTurningOnVentLights()
    {
        if(Input.GetMouseButton(0) && Use_Vent_Enabled)
        {
            cameraObject.Allow_FOV_Change = false;
            EnableOrDisbaleVentLights(true);
        }
        else
        {
            cameraObject.Allow_FOV_Change = true;
            EnableOrDisbaleVentLights(false);
        }
    }

    void PlayVentDoorAnimation(Animator animator, int anim_Index, bool Enabled)
    {
        animator.enabled = enabled;
        animator.Play(VentDoorAnim[anim_Index]);
        StartCoroutine(MakeVentDoorInteractable(true, 0.3f));
        StartCoroutine(MakeVentDoorInteractable(false, 0));
    }

    IEnumerator EnableOrDisableUseOfVent(float time, bool Enabled)
    {
        yield return new WaitForSeconds(time);
        Use_Vent_Enabled = Enabled;
    }

    public void EnableOrDisbaleVentLights(bool enabled)
    {
        foreach(Light light in Vent_Lights)
        {
            light.enabled = enabled;
        }
    }
    IEnumerator MakeVentDoorInteractable(bool Enabled, float time)
    {
        yield return new WaitForSeconds(time);
        {
            interactable = enabled;
        }
    }
}
