using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MissionSpace
{
    
    public class InventoryMovePlanet_MS : MonoBehaviour
    {
        Vector2 Targetpos;
       public bool IsMove;
        bool isfirstmove;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (IsMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, Targetpos, Time.deltaTime * 5);
                if (Vector2.Distance(transform.position, Targetpos) < .01f)
                {
                    if(isfirstmove)
                    {
                       
                       
                    }
                    else
                    {
                        Level1Manager_MS.instance.NextPlanetInList();
                    }
                    IsMove = false;
                }
            }

        }

        public void movePlanet(Vector2 targetpos,bool isfris)
        {
           // print(targetpos);
            Targetpos = targetpos;
           
            if (!isfris)
            {
                GetComponent<Image>().enabled = true;
                IsMove = true;
                isfirstmove = isfris;
            }
            else
            {
                Invoke("waitsometime",1f);
            }
        }

        void waitsometime()
        {
            transform.position = Targetpos;
            Level1Manager_MS.instance.SelectPlanetForGameStart();
        }


       public  void stopPlanet()
        {
            CancelInvoke("waitsometime");
            IsMove = false;
        }


    }
}
