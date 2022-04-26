using System.Collections;
using TMPro;
using UnityEngine;

public class TimeUi : MonoBehaviour
{
    public TextMeshProUGUI Time_Text;
    public float Timer;
    public int Current_Time;
    public int UpdateTimeDelay;

    [Header("Color Lerping Variables")]
    public bool Change_Color;
    public float Lerp_Value;
    public float Change_Color_Duration;

    void Start()
    {

    }
    private void Update()
    {
        StartCoroutine(ChangeColorWhenTimeChnage());
    }
    public void UpdateTime()
    {
        Timer += Time.deltaTime;
        if (Timer >= UpdateTimeDelay && Current_Time != 6)
        {
            UpdateTimeText();
            Timer = 0;
        }
    }

    void UpdateTimeText()
    {
        Change_Color = true;
        Current_Time++;
        Time_Text.text = Current_Time + " AM";
    }

    IEnumerator ChangeColorWhenTimeChnage()
    {
        if (Change_Color)
        {
            ChangeColorTo(Color.red);
            yield return new WaitForSeconds(Change_Color_Duration);
            ChangeColorTo(Color.white);

            yield return new WaitForSeconds(Change_Color_Duration);
            Change_Color = false;
        }
    }
    void ChangeColorTo(Color Text_Color)
    {
        float timer = 0;
        timer += Time.deltaTime;
        if (timer < Change_Color_Duration)
        {
            Time_Text.color = Color.Lerp(Time_Text.color, Text_Color, Lerp_Value * Mathf.SmoothStep(0, 1, Lerp_Value));
        }
    }

}
