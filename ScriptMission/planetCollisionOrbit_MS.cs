using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissionSpace
{
    public class planetCollisionOrbit_MS : MonoBehaviour
    {
        public static planetCollisionOrbit_MS instance;
        bool isRight, Ismove;
       public string name;
        // Start is called before the first frame update
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
          
        }

        private void OnTriggerEnter(Collider other)
        {
            print(other.transform.name);
            if (other.transform.name.Contains( "Letter"))
            {
                //print("other" + other.name);
                Level2Manager_MS.instance.IsRightMove=true;
                name = other.name;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.name.Contains("Letter"))
            {
                
                Level2Manager_MS.instance.IsRightMove = false;
            }
        }
        
    }
}