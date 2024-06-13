using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MissionSpace
{

    public class TravelBubbleScritp_MS : MonoBehaviour
    {
        // Start is called before the first frame update

        public bool IsPreposition;
        bool IsTravel;

       public  Vector2 TargetPos;
        Vector2 startpos;
        float speed;
        void Start()
        {
            BubblestartTavel();
            speed = 5;


        }


        // Update is called once per frame
        void Update()
        {
            if (IsTravel)
            {

                transform.position = Vector2.MoveTowards(transform.position, TargetPos, Time.deltaTime * speed);
                if (Vector2.Distance(transform.position, TargetPos) < .01f)
                {
                    IsTravel = false;
                    BubblestartTavel();
                    speed = 1;
                    // print("reacj");
                }
            }
        }

        // call to find random point on the screen for bubble
        //void randomPoint()
        //{
        //    //startpos=transform
        //    float xpos = Random.Range(Level1Manager_Preposition.instance.topLeft_Point.position.x, Level1Manager_Preposition.instance.topRight_Point.position.x);
        //    float Ypos = Random.Range(Level1Manager_Preposition.instance.topLeft_Point.position.y, Level1Manager_Preposition.instance.DownRight_Point.position.y);
        //    TargetPos = new Vector2(xpos, Ypos);

        //    // print(TargetPos);
        //}



        public void BubblestartTavel()
        {
            //randomPoint();
            IsTravel = true;


        }

       
        private void OnTriggerEnter2D(Collider2D collision)
        {

            //if (collision.tag == "Bubble")
            //{
            //    print("hit");
            //    IsTravel = false;
            //    BubblestartTavel();

            //}

        }


    }
}
