using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollow : MonoBehaviour
{
    public CarController cc;

    [SerializeField] public Transform Left;
    [SerializeField] public Transform Right;
    [SerializeField] public Transform Middle;

    public float scandir = 1f;

    [SerializeField] float PropGain;

    public LayerMask ll;

    private Vector3 Lhit;
    private Vector3 Mhit;
    private Vector3 Rhit;

    //public LayerMask LineLayer;

    // Start is called before the first frame update
    void Start()
    {
        cc.AIhori = 0;
        cc.AIverti = 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit hitL;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Left.transform.position, Left.transform.TransformDirection(Vector3.down), out hitL, Mathf.Infinity, ll))
        {
            Lhit = hitL.point;
            if(hitL.collider.gameObject.name == "line")
            {
                cc.AIhori += PropGain;
                if(cc.AIverti >= 0.2f)
                {
                    cc.AIverti -= 0.025f;
                }
                
                Debug.DrawRay(Left.transform.position, Left.transform.TransformDirection(Vector3.down) * hitL.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(Left.transform.position, Left.transform.TransformDirection(Vector3.down) * 1000, Color.black);
            }

            
        }
        


        RaycastHit hitM;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Middle.transform.position, Middle.transform.TransformDirection(Vector3.down), out hitM, Mathf.Infinity, ll))
        {
            Mhit = hitM.point;
            if (hitM.collider.gameObject.name == "line")
            {
                cc.AIhori = 0f;
                if (cc.AIverti >= 0.2f)
                {
                    cc.AIverti += 0.025f;
                }
                Debug.DrawRay(Middle.transform.position, Middle.transform.TransformDirection(Vector3.down) * hitM.distance, Color.green);
            }
            else
            {
                Debug.DrawRay(Middle.transform.position, Middle.transform.TransformDirection(Vector3.down) * 1000, Color.black);
            }

            
        }
        

        RaycastHit hitR;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Right.transform.position, Right.transform.TransformDirection(Vector3.down), out hitR, Mathf.Infinity, ll))
        {
            Rhit = hitR.point;
            if(hitR.collider.gameObject.name == "line")
            {
                cc.AIhori -= PropGain;
                if (cc.AIverti >= 0.2f)
                {
                    cc.AIverti -= 0.025f;
                }
                Debug.DrawRay(Right.transform.position, Right.transform.TransformDirection(Vector3.down) * hitR.distance, Color.blue);
            }
            else
            {
                Debug.DrawRay(Right.transform.position, Right.transform.TransformDirection(Vector3.down) * 1000, Color.black);
            }

            
        }
        
    }


   

    private void OnDrawGizmos()
    {
        

        Gizmos.color = Color.red;
        Gizmos.DrawLine(Left.position, Lhit);
        Gizmos.DrawWireSphere(Lhit, scandir);
        Gizmos.DrawWireSphere(Left.position, scandir);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(Middle.position, Mhit);
        Gizmos.DrawWireSphere(Mhit, scandir);
        Gizmos.DrawWireSphere(Middle.position, scandir);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Right.position, Rhit);
        Gizmos.DrawWireSphere(Rhit, scandir);
        Gizmos.DrawWireSphere(Right.position, scandir);
    }
}
