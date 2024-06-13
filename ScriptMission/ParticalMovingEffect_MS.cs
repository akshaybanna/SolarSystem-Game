using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalMovingEffect_MS : MonoBehaviour
{
    public Transform[] point;
    Vector2 Targetpoint;
    public float speed;
    bool IsMove;
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsMove)
        {
            //transform.position = Vector2.MoveTowards(transform.position,Targetpoint,Time.deltaTime*)
        }
    }
    
}
