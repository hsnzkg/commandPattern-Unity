using System.Globalization;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugCanvas : MonoBehaviour
{
    public Text fixedDeltaTime;
    public Text unscaledFixedDeltaTime;

    public Text deltaTime;
    public Text unscaledDeltaTime;

    public Text inputs;

    void Update()
    {
        fixedDeltaTime.text = Time.fixedDeltaTime.ToString() + "  Fixed Delta Time";
        unscaledFixedDeltaTime.text = Time.fixedUnscaledDeltaTime.ToString() + " Fixed Unscaled Delta Time";
        deltaTime.text = Time.deltaTime.ToString() +  " Delta Time";
        unscaledDeltaTime.text = Time.unscaledDeltaTime.ToString() +  " Unscaled Delta Time";
        inputs.text = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")).ToString();
    }
}
