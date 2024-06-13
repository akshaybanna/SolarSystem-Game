using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MissionSpace
{


    public class RocketMovement_MS : MonoBehaviour
    {
        bool Isbool;
        // Start is called before the first frame update
        void OnEnable()
        {
            Isbool = false;
            GetComponent<Animator>().Play("idle");
            transform.localPosition = new Vector2(transform.localPosition.x,0);
        }

        // Update is called once per frame
        void Update()
        {

            if (Level4Manager_MS.instance.IsGameover) return;
            if (Isbool)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                print(pos.y);
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(pos.y,-0.2f,6.0f),0);
            }
        }
            
      public  void PointerDown()
        {
            Isbool = true;
        }
       public void PointerUp()
        {
            Isbool = false;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {

            print (collision.name+ "      ++++++++   ");
            if(collision.transform.name=="Distortion")
            {
               // transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
                collision.transform.gameObject.SetActive(false);
                print(collision.transform.GetComponent<MoveObject_MS>().isright);
                if (collision.transform.GetComponent<MoveObject_MS>().isright)
                {
                    transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
                    Level4Manager_MS.instance.NextQuestion(true);
                }
                else
                {

                    GetComponent<Animator>().Play("ShakeAnimation");
                    Level4Manager_MS.instance.NextQuestion(false);
                    Invoke("waitsometime", 1f);

                }
                collision.transform.GetComponent<MoveObject_MS>().changepos();
               
                // collision.transform.GetComponent<MoveObject>().ReachTargetpoint();

            }
            

        }
        void waitsometime()
        {
            GetComponent<Animator>().Play("idle");
           
           // transform.GetChild(1).gameObject.SetActive(false);
        }


    }
}