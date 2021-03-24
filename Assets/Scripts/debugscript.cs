using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class debugscript : MonoBehaviour
{
    public Text Speed;
    public Text Torque;
    public Text Inputs;
    public Text FeedbackForce;
    public CarController cc;

    void Update()
    {
        var v = Input.GetAxis("Vertical");
        Inputs.text = v.ToString();
        Torque.text = cc.avgAppliedTorque.ToString() + "rad s^-1";
        Speed.text = cc.RbSpeedValue.ToString() + "ms^-1";
        FeedbackForce.text = (cc.RbSpeedValue * -0.5f).ToString() + "j N";
    }
}
