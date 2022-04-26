using UnityEngine;

public class BarChair : MonoBehaviour
{
    /// <summary>
    /// check if mouse button clicked
    /// Then  move and rotate the chair
    ///  if clicked again 
    ///  Then move back to orginal position
    /// </summary>
    /// 
    [Header("Common Variables")]

    public RaycastManager raycastManager;
    public bool Moved;
    public bool Is_Chair_Moving;
    public float LerpValue;
    public float Allow_To_Move_delay;

    [Header("Position Lerping Variables")]
    public Vector3[] Lerping_Positions;


    [Header("Position Lerping Variables")]
    public Quaternion[] Lerping_Rotations;


    private void Start()
    {
        Lerping_Positions[0] = transform.position;
        Lerping_Rotations[0] = transform.rotation;
        raycastManager = GameObject.FindObjectOfType<RaycastManager>();
    }
    void Update()
    {
        StartMovingBarChair();
    }

    public void ChairInput()
    {
        if (!Moved && Is_Chair_Moving == false)
        {
            Moved = true;
            Is_Chair_Moving = true;

            Invoke("AllowToMoveBarChairAfterDelay", Allow_To_Move_delay);
        }
        else if (Moved && Is_Chair_Moving == false)
        {
            Moved = false;
            Is_Chair_Moving = true;

            Invoke("AllowToMoveBarChairAfterDelay", Allow_To_Move_delay);
        }
    }

    void StartMovingBarChair()
    {
        if (!Moved)
        {
            ChangeBarChairPositionAndRotationTo(0); // Moved position;

        }
        else if (Moved)
        {
            ChangeBarChairPositionAndRotationTo(1); // Orginal position;
        }
    }

    void ChangeBarChairPositionAndRotationTo(int Lerp_Index)
    {
        if (Is_Chair_Moving)
        {
            transform.position = Vector3.Lerp(transform.position, Lerping_Positions[Lerp_Index], LerpValue * Mathf.SmoothStep(0, 1, LerpValue));
            transform.rotation = Quaternion.Lerp(transform.rotation, Lerping_Rotations[Lerp_Index], LerpValue * Mathf.SmoothStep(0, 1, LerpValue));
            Debug.Log("Its lerpiong");
        }
    }

    void AllowToMoveBarChairAfterDelay()
    {
        Is_Chair_Moving = false;
    }
}
