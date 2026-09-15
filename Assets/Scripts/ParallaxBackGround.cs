using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackGround : MonoBehaviour
{
    private GameObject cm;
    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;
    void Start()
    {
        cm = GameObject.Find("Main Camera");
        xPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    void Update()
    {
        float distanceMoved=cm.transform.position.x*(1-parallaxEffect);
        float distanceToMove = cm.transform.position.x * parallaxEffect;
        transform.position=new Vector3(xPosition+distanceToMove,transform.position.y);
        
        if(distanceMoved > xPosition+length)
            xPosition+=length;
        else if(distanceMoved < xPosition-length)
            xPosition-=length;
    }
}
