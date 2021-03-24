using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VijayShenanigans : MonoBehaviour
{
    [SerializeField] public CarController mycar;
    [SerializeField] public LineFollow lf;
    private float CarExtension = 0f;
    private bool PDctrl = false;
    [SerializeField]private WheelCollider[] kek;
    [SerializeField] private Transform[] kek1;
    [SerializeField] private Text autopilot;


    // Update is called once per frame
    private void Start()
    {
        autopilot.color = Color.red;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (PDctrl)
            {
                PDctrl = false;
                lf.enabled = false;
                autopilot.color = Color.red;
            }
            else if (!PDctrl)
            {
                PDctrl = true;
                lf.enabled = true;
                autopilot.color = Color.green;
            }
        }
        
        
        if (Input.GetKey(KeyCode.Space))
        {
            CarExtension = Mathf.Lerp(CarExtension, 3f , 5f * Time.deltaTime);
            foreach(WheelCollider elem in kek)
            {
                elem.radius = Mathf.Lerp(elem.radius, 0.47f, 5f *  Time.deltaTime);
            }
            foreach(Transform k in kek1)
            {
             
                k.localScale = Vector3.Lerp(k.localScale , new Vector3(2.2f, 2.2f, 2.2f), 5f*Time.deltaTime);
            }
        }
        else
        {
            CarExtension = Mathf.Lerp(CarExtension, 0.2f , 5f * Time.deltaTime);
            foreach (WheelCollider elem in kek)
            {
                elem.radius = Mathf.Lerp(elem.radius, 0.24f, 5f * Time.deltaTime);
            }
            foreach (Transform k in kek1)
            {
                k.localScale =  Vector3.Lerp(k.localScale, new Vector3(0.98f, 0.98f, 0.98f), 5f * Time.deltaTime);
            }
        }

        mycar.BL.suspensionDistance = CarExtension;
        mycar.FL.suspensionDistance = CarExtension;
        mycar.BR.suspensionDistance = CarExtension;
        mycar.FR.suspensionDistance = CarExtension;
    }
}
