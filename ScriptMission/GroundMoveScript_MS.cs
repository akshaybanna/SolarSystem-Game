using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissionSpace
{


public class GroundMoveScript_MS : MonoBehaviour
{
        public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Level2Manager_MS.instance.ISGameOver) return;
        transform.Translate(Vector2.left * Time.deltaTime * 2.5f);
        if (transform.GetComponent<RectTransform>().localPosition.x <= -2000)
        {
            transform.GetComponent<RectTransform>().localPosition = new Vector2(2400, transform.GetComponent<RectTransform>().localPosition.y);
                coinTrueFalse(true);
        }
       
    }
        public void coinTrueFalse(bool istrue)
        {
            if (coin != null)
            {
                coin.SetActive(istrue);
            }
        }

}
}
