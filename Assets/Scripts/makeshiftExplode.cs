using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeshiftExplode : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem psexplosion;
   
    [System.Obsolete]
    private void Start()
    {
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            var em = psexplosion.emission;
            em.enabled = true;
            em.SetBursts(new ParticleSystem.Burst[]
            {
                new ParticleSystem.Burst(1f,150)
            });
            
        }
    }
}
