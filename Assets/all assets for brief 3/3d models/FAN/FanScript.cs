using System.Collections;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool clicked;
    [Header("Fan Motor Rotating Variables")]
    public Transform Fan_Motor_Position;
    public Quaternion[] Lerp_Rotation_Destinations;
    public int Motor_Rotation_Index;
    public float Rotation_Lerp_Value;
    public float Motor_Rotation_delay;
    public bool rotated;

    [Header("Fan Blade Rotating Variables")]
    public Transform Fan_Blade;
    public float Blade_Rotating_Speed;
    public float Speed_Multiplier;
    public float Blade_Lerp_Value;
    public float Duration;
    public bool OnStart;



    void Start()
    {
        Motor_Rotation_Index = 0;
    }

    // Update is called once per frame
    void Update()
    {

        ActivateOrDeactivateFanOnClick();

    }

    public void FanClickingInput()
    {
        if (Input.GetMouseButtonDown(0) && clicked == false)
        {
            clicked = true;
            Blade_Lerp_Value = 0;
            OnStart = true;
        }
        else if (Input.GetMouseButtonDown(0) && clicked == true)
        {
            clicked = false;
            Blade_Lerp_Value = 0;
            OnStart = false;
        }
    }

    void ActivateOrDeactivateFanOnClick()
    {
        if (clicked == true)
        {

            if (rotated == false)
            {
                StartCoroutine(FanMotorRotationdelay());

            }
            FanPartsToRotate(clicked);
        }
        else if (clicked == false)
        {

            FanPartsToRotate(clicked);
        }

    }

    void FanPartsToRotate(bool activate) // code to actually start rotataing all parts of the fan 
    {

        if (activate == true)
        {
            // fan motor rotation
            if (rotated == true)
            {
                SlerpeFanMotorTo(Fan_Motor_Position, Lerp_Rotation_Destinations[Motor_Rotation_Index]);
            }
            RotatefanBladeEnabled(true);
            // fan blade rotation

        }
        else if (activate == false)
        {
            SlerpeFanMotorTo(Fan_Motor_Position, Lerp_Rotation_Destinations[1]);
            RotatefanBladeEnabled(false);
        }



    }
    void RotatefanBladeEnabled(bool Start_Rotating)
    {
        float LerpedSpeedvalue;
        float Rot_speed;

        if (Start_Rotating == true)
        {

            Blade_Lerp_Value += Time.deltaTime / Duration;
            LerpedSpeedvalue = Mathf.Lerp(0, Blade_Rotating_Speed, Blade_Lerp_Value);
            Rot_speed = LerpedSpeedvalue * Speed_Multiplier * Time.deltaTime;
            Fan_Blade.Rotate(0, 0, Rot_speed);
        }
        else if (Start_Rotating == false && OnStart == false)
        {
            Blade_Lerp_Value += Time.deltaTime / Duration;
            LerpedSpeedvalue = Mathf.Lerp(Blade_Rotating_Speed, 0, Blade_Lerp_Value);
            Rot_speed = LerpedSpeedvalue * Speed_Multiplier * Time.deltaTime;
            Fan_Blade.Rotate(0, 0, Rot_speed);
        }
    }
    IEnumerator FanMotorRotationdelay() // time iontervels to switch between motor rotations
    {
        rotated = true;
        yield return new WaitForSeconds(Motor_Rotation_delay);
        IncreaseIndex();
        rotated = false;
    }
    void IncreaseIndex()
    {
        if (rotated == true && Motor_Rotation_Index <= 2)
        {
            Motor_Rotation_Index++;
        }
        else if (Motor_Rotation_Index >= 2 && rotated == true)
        {
            Motor_Rotation_Index = 0;
        }
    }
    void SlerpeFanMotorTo(Transform Current_Rotation, Quaternion Destination_Rotation)
    {
        Fan_Motor_Position.rotation = Quaternion.Slerp(Current_Rotation.rotation, Destination_Rotation, Rotation_Lerp_Value * Mathf.SmoothStep(0, 1, Rotation_Lerp_Value));
    }
}


