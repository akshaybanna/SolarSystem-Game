using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MissionSpace
{
  
    public class ShipMovementScript_MS : MonoBehaviour
    {
        bool ismove;
        public Transform point_left, point_right;
        Transform Target_point;
        Vector2 targetpos;
        public Transform Parent_planet;
        public ParticleSystem partical;
        // Start is called before the first frame update
        void Start()
        {
            // MoveAutomaticSpaceship();
        }
        bool isright;
        // Update is called once per frame
        void Update()
        {
            if (isright)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetpos, Time.deltaTime * 6);
                if (Vector2.Distance(transform.position, targetpos) < .1f)
                {
                    
                    isright = false;
                    transform.GetChild(2).GetComponentInChildren<Text>().text = Level3Manager_MS.instance.MainQuestion_text.text;
                    transform.GetChild(2).gameObject.SetActive(true);
                    Level3Manager_MS.instance.ReachTopositoin();
                    Invoke("Effectend", 3f);
                    //MoveAutomaticSpaceship();
                }
            }


            if (ismove)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target_point.position, Time.deltaTime * 2);
                if (Vector2.Distance(transform.position, Target_point.position) < .1f)
                {
                    MoveAutomaticSpaceship();
                }
            }
        }

        void Effectend()
        {
            transform.GetChild(2).gameObject.SetActive(false);
            Level3Manager_MS.instance.currentQuestionNo++;
            Level3Manager_MS.instance.NextQuestion();
            
        }

        bool IsFlag;
        public void MoveAutomaticSpaceship()
        {
            IsFlag = !IsFlag;
            if (IsFlag)
            {
                Target_point = point_left;

            }
            else
            {
                Target_point = point_right;
            }
            ismove = true;
        }

        public void RightAction(int id)
        {
            targetpos = new Vector2(Parent_planet.GetChild(id).GetChild(0).transform.position.x, transform.position.y);
            ismove = false;
            isright = true;
        }
        public void StopShip()
        {
            ismove = false;
            isright = false;
        }
        public void StartShip()
        {
            ismove = true;
           // isright = false;
        }

        // public partical partical;
        public void Destory()
        {
            partical.Play();
            transform.GetChild(1).GetComponent<Image>().enabled = false;
        }
        public void Reset()
        {
            transform.GetChild(1).GetComponent<Image>().enabled = true;
        }

    }
}