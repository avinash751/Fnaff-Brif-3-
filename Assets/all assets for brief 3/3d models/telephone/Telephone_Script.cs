using UnityEngine;
using System.Collections;

public class Telephone_Script : MonoBehaviour
{
    /// <summary>
    /// phone ring
    /// if pick up, start dialogue
    /// if no pick up, wait, and play dialogue
    /// after dialogue finish set a timer to play next dialogue
    /// then repeat
    /// </summary>
    public TimeUi Game_Time;

    [Header("Phone PickUp Variables")]
    public AudioSource Phone_Ringer_Sound;
    public bool Is_Phone_Ringing;
    public bool Pick_Up;
    public bool Is_Telephone_Interactable;
    public float Phone_Ring_timer = 0;
    //public float Dialogue_Wait_Timer;

    [Header("Phone Dialogue variables")]
    public AudioSource[] PhoneGuy_Dialogues;
    public bool Is_Dialogue_Playing;
    public bool Is_Dialogue_Started;
    public bool[] Dialogue_Switched = new bool[2];
    public int Bool_Dialogue_Index;
    public int DialogueIndex;
    public float Time_taken_For_Dialogue;

    [Header("Animator Controllers & Animations")]
    public Animator Telephone_Animator;
    public string Telephone_Ringing_Anim;
    public string[] Telephone_PickUP_Animations;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayOrStopAnimation(Telephone_Animator, Telephone_Ringing_Anim,true,0));
        Is_Phone_Ringing = true;
        PlayAndIncrementTheDialogueWhenFinished();
    }

    // Update is called once per frame
    void Update()
    {
        StartTheWholeTelephoneProcess();
    }

    public void TelephonePickUpInput()
    {
        if(Input.GetMouseButtonDown(0) & Pick_Up == false & Is_Telephone_Interactable && !Is_Dialogue_Started)
        {
            StartCoroutine(MakeTelephoneInteractable(false, 0));
            StartCoroutine(EnableOrDisablePickUp(0, true, 0));
            StartCoroutine(MakeTelephoneInteractable(true, 0.5f));

        }
        else if(Input.GetMouseButtonDown(0) & Pick_Up == true & Is_Telephone_Interactable && !Is_Dialogue_Started)
        {
            StartCoroutine(MakeTelephoneInteractable(false, 0));
            StartCoroutine(EnableOrDisablePickUp(0, false, 1));
            StartCoroutine(MakeTelephoneInteractable(true, 0.1f));
        }
    }

    // checks the time and depending on time, it ill start ringing and check if phone will be picked up.
    void StartTheWholeTelephoneProcess()
    {
        if (Game_Time.Current_Time == 1 && !Dialogue_Switched[0])
        {
            Debug.Log("Telephone Process is starting");
            StartPhoneRinging();
            PlayDialogueIfPhoneNotPickedUp();
            Invoke("PlayDialogueIfPhonePickedUp", 0.4f);
        }
        else if( Game_Time.Current_Time == 2 && !Dialogue_Switched[1])
        {
            Debug.Log("Telephone Process is starting");
            StartPhoneRinging();
            PlayDialogueIfPhoneNotPickedUp();
            Invoke("PlayDialogueIfPhonePickedUp", 0.4f);
        }
        else
        {
            Debug.Log("Telephone Process Can not begin at this time");
        }
    }

    void StartPhoneRinging()
    {
        if (Is_Phone_Ringing)
        {
            PutTelephoneBackToOrgin();
            StartCoroutine(PlayOrStopAnimation(Telephone_Animator, Telephone_Ringing_Anim,false,0.4f));
            Phone_Ring_timer = 0;
            Phone_Ringer_Sound.Play();
            Debug.Log("Phone is Ringing");
            Is_Dialogue_Started = false;
            Is_Phone_Ringing = false;
           
        }
    }


    // checks if ringer time has gone beyond its length, if it did, it plays dialogue imedialtly.
    void PlayDialogueIfPhoneNotPickedUp()
    {
        float Phone_Ringing_Time = Phone_Ringer_Sound.clip.length;
        Phone_Ring_timer += Time.deltaTime;
        Debug.Log("Waiting if phone will be picked up");

        if (!Is_Dialogue_Playing && Phone_Ring_timer > Phone_Ringing_Time && !Is_Dialogue_Started)
        {
            StartTheWholeDialogueProcess();
            StartCoroutine(PlayOrStopAnimation(Telephone_Animator, Telephone_Ringing_Anim, true, 0));
        }
    }

    void PlayDialogueIfPhonePickedUp()
    {
        if(Pick_Up == true & !Is_Dialogue_Started)
        {
            StartTheWholeDialogueProcess();
            StartCoroutine(MakeTelephoneInteractable(false,0.3f));
            Phone_Ringer_Sound.Stop();
            StartCoroutine(PlayOrStopAnimation(Telephone_Animator, Telephone_Ringing_Anim, true, 0));
            StartCoroutine(PlayOrStopAnimation(Telephone_Animator, Telephone_PickUP_Animations[0], false, 0));
           
        }
    }


    //it starts the dialogue process, allowing the dialogue to play and switch to the next one, when finished
    void StartTheWholeDialogueProcess()
    {
        Is_Dialogue_Started = true;
        PlayAndIncrementTheDialogueWhenFinished();

        StartCoroutine(MakeTelephoneInteractable(false, 0));
        Dialogue_Switched[Bool_Dialogue_Index] = true;
        Bool_Dialogue_Index++;
        Debug.Log("phone is not picked up, so dialogue will start playing");
    }

    void PlayAndIncrementTheDialogueWhenFinished()
    {
        Debug.Log("Phone ringing stoped");
        Is_Dialogue_Playing = true;
        StartCoroutine(MakeTelephoneInteractable(false, 0.5f));
        if (Is_Dialogue_Playing)
        {
            PhoneGuy_Dialogues[DialogueIndex].Play();
            Debug.Log("Dialogue" + DialogueIndex + "is Playing");
            Time_taken_For_Dialogue = PhoneGuy_Dialogues[DialogueIndex].clip.length;
           
            Invoke("StopPlayingDialogueAfterSeconds", Time_taken_For_Dialogue);
            Invoke("EnablePhoneRingingAfterSeconds", Time_taken_For_Dialogue);
            StartCoroutine(MakeTelephoneInteractable(true, Time_taken_For_Dialogue));
        }
    }
    void StopPlayingDialogueAfterSeconds()
    {
        Is_Dialogue_Playing = false;
        DialogueIndex++;

        Debug.Log("Dialogue will stop in some time");
    }
    void EnablePhoneRingingAfterSeconds()
    {
       
        Is_Phone_Ringing = true;
        Debug.Log("Phone will ring in some time");
        PutTelephoneBackToOrgin();
    }

    IEnumerator EnableOrDisablePickUp(float time, bool PickUpBool, int anim)
    {
        yield return new WaitForSeconds(time);
        Pick_Up = PickUpBool;
        StartCoroutine(PlayOrStopAnimation(Telephone_Animator, Telephone_PickUP_Animations[anim], false, 0));
    }

    IEnumerator MakeTelephoneInteractable(bool enable,float Time_Taken)
    {
        yield return new WaitForSeconds(Time_Taken);
        Is_Telephone_Interactable = enable;
    }

    void PutTelephoneBackToOrgin()
    {
        if (Pick_Up == true)
        {
            StartCoroutine(PlayOrStopAnimation(Telephone_Animator, Telephone_PickUP_Animations[1], false, 0));
            StartCoroutine(MakeTelephoneInteractable(false, 0));
            StartCoroutine(MakeTelephoneInteractable(true, 0.5f));
            Pick_Up = false;
        }
    }


    IEnumerator PlayOrStopAnimation(Animator Anim_Controller, string anim_To_Play, bool Stop_Anim,float time)
    {
        yield return new  WaitForSeconds(time);
        if(!Stop_Anim)
        {
            Anim_Controller.enabled = true;
            Anim_Controller.Play(anim_To_Play);
        }
        else if(Stop_Anim)
        {
            Anim_Controller.enabled = false;
        }
    }

    
}
