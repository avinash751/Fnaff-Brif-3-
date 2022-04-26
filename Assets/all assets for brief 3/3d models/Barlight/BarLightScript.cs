using System.Collections;
using UnityEngine;

public class BarLightScript : MonoBehaviour
{
    public Light[] LightSource;
    public bool Is_Flickering;
    public bool Is_Enabled;
    public int Times_To_Flicker;
    public float Timeer_Delay;
    public int Flickered_Amount;
    public bool onstart;
    public MeshRenderer bulb;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        ActivateBarLight();
        startBlinkingLight();
    }

    public void BarLightInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Is_Enabled)
            {
                EnableOrDisbaleLightSources(false);
                Is_Enabled = false;
                onstart = true;
                Is_Flickering = true;
            }
            else if (!Is_Enabled)
            {
                Is_Flickering = false;
                Flickered_Amount = 0;
                Is_Enabled = true;
            }
        }
    }

    void ActivateBarLight()
    {
        if (Is_Enabled == true)
        {
            if (Flickered_Amount < Times_To_Flicker)
            {
                StartCoroutine(StartFlickeringToEnableLight());
            }
            else
            {
                EnableOrDisbaleLightSources(true);
            }
        }
    }

    IEnumerator StartFlickeringToEnableLight()
    {
        EnableOrDisbaleLightSources(true);
        Is_Flickering = true;
        Timeer_Delay = Random.Range(0.5f, 0.8f);

        yield return new WaitForSeconds(Timeer_Delay);
        EnableOrDisbaleLightSources(false);
        Flickered_Amount++;
        Is_Flickering = false;
    }

    void EnableOrDisbaleLightSources(bool Enable)
    {
        foreach (Light light in LightSource)
        {
            light.enabled = Enable;
        }
        if (Enable)
        {
            bulb.material.EnableKeyword("_EMISSION");
        }
        else
        {
            bulb.material.DisableKeyword("_EMISSION");
        }
    }
    void startBlinkingLight()
    {
        if (Is_Flickering == true && Is_Enabled == false)
        {
            StartCoroutine(StartBlinkingLight());
        }
    }
    IEnumerator StartBlinkingLight()
    {
        Is_Flickering = false;
        EnableOrDisbaleLightSources(false);
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        EnableOrDisbaleLightSources(true);
        yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        Is_Flickering = true;
    }


}
