using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    [SerializeField] ParticleSystem p1 = default;
    [SerializeField] ParticleSystem p2 = default;



    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire_start () {
        //foreach(ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
        //{
            p1.Play();
            p2.Play();
        //}
    }

    void Fire_stop () {
        //foreach(ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
        //{
            p1.Stop();
            p2.Stop();
        //}
    }
}
