using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public TimeUi Time_Ui_Object;

    [Header("Stsrt Ui")]
    public bool Start_Screen_Ui;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Start_Screen_Ui)
        {
            Time_Ui_Object.UpdateTime();
        }

    }

    public void StartGame()
    {

        SceneManager.LoadScene(1);
    }
}
