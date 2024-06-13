using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MissionSpace;
public class ScientistMovingEffect_MS : MonoBehaviour
{
    bool ismove,ismoveinstrument;
    Vector2 Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ismove)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target, Time.deltaTime *10);
            transform.localScale -= new Vector3(.1f, .1f,.1f) * Time.deltaTime*3;
            if (Vector2.Distance(transform.position, Target) < .01f)
            {
                ismove = false;
               // print("aaya");
                Level5Manager_MS.instance.ReachPhotoToTarget();
            }
        }
        if (ismoveinstrument)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target, Time.deltaTime*8);
            transform.localScale -= new Vector3(.1f, .1f, .1f) * Time.deltaTime * 3;
            if (Vector2.Distance(transform.position, Target) < .01f)
            {
                ismoveinstrument = false;
                Level5Manager_MS.instance.CompleteTravalImage();
            }
        }
    }

    public void CallStart(Vector2 target,Sprite image , string text)
    {
        Target = target;
        GetComponent<Image>().sprite = image;
        GetComponentInChildren<Text>().text = text;
        transform.localScale = new Vector3(1f, 1f, 1f);
        print("aaya");
        ismove = true;
    }
    public void CallStartForInstrument(Vector2 target, Sprite image)
    {
        Target = target;
        GetComponent<Image>().sprite = image;
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponentInChildren<Text>().text = "";
       // print("aaya");
        ismoveinstrument = true;
    }
}
