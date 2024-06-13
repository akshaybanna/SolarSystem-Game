using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MissionSpace
{
public class substanceScript_MS : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
            if (UIManager_MS.instance.IsBoyCharacter)
            {
              
                transform.GetChild(0).transform.SetAsFirstSibling();
               
               
            }
            else
            {
                print("aaya");
                transform.GetChild(1).transform.SetAsFirstSibling();
                
            }
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
          
        }
}
}