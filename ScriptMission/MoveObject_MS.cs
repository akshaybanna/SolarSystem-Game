using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MissionSpace
{
    public class MoveObject_MS : MonoBehaviour
    {
        bool ismove;
        public Transform point_left, point_right;
        Transform Target_point;
        public Vector2 targetpos;
        // public bool Isright;
       
        public bool isright;

        private void OnEnable()
        {
            transform.position = new Vector2(point_right.position.x+Random.Range(0,10), transform.position.y);
        }

        public void GameStart()
        {
            print("check start distortion");
            Target_point = point_left;
            ismove = false;
            IsFlag = false;
            Invoke(nameof(ReachTargetpoint), .5f);
           // Invoke("ReachTargetpoint",.5F);
            //print("call");
          

        }


        public void StopGame()
        {
            ismove=false;
        }
        // public Transform Parent_planet;
        // Start is called before the first frame update
      
        // Update is called once per frame
        void Update()
        {
            
            if (Level4Manager_MS.instance.IsGameover) return;
            if (ismove)
            {
                print("update");
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target_point.position.x, transform.position.y), Time.deltaTime * Level4Manager_MS.instance.speed);
                if (Vector2.Distance(transform.position, new Vector2(Target_point.position.x, transform.position.y)) < .1f)
                {
                    ismove = false;
                    if (GetComponent<Image>().enabled == true)
                    {
                        Level4Manager_MS.instance.CurrentQuestion();
                    }
                    transform.position = new Vector2(point_right.position.x, transform.position.y);
                    Target_point = point_left;
                    Invoke("ReachTargetpoint", .5F);
                }
            }
        }


        void MiteorBlast()
        {
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
            isright = false;
        }
        bool IsFlag;
        public void ReachTargetpoint()
        {
            int rnd_miteor = Random.Range(0, 2);
        Outer:
            if (rnd_miteor==1)
            {
                 
                MiteorBlast();
            }
            else
            {
                GetComponent<Image>().enabled = true;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(false);

                int rnd = Random.Range(0, 2);
                if (Level4Manager_MS.instance.Shuffled_Question[Level4Manager_MS.instance.selectedPlanet].properties.Count <= 0)
                {
                    rnd = 1;
                }
                if (rnd == 0)
                {
                   
                  
                        print("value" + Level4Manager_MS.instance.Shuffled_Question[Level4Manager_MS.instance.selectedPlanet].properties);
                        transform.GetChild(0).GetComponent<Text>().text = Level4Manager_MS.instance.Randomquestion(true);
                    if (transform.GetChild(0).GetComponent<Text>().text == "")
                    {
                        rnd_miteor = 1;
                        goto Outer;
                        // return;
                    }
                    print("right text"+transform.GetChild(0).GetComponent<Text>().text);
                    isright = true;
                   



                }
                else
                {

                    if (Level4Manager_MS.instance.Shuffled_Question[rnd].properties.Count <= 0)
                    {
                        print("value" + Level4Manager_MS.instance.Shuffled_Question[rnd].properties);
                        Invoke("ReachTargetpoint",.5f);
                        return;
                    }
                       

                   
                  
                        transform.GetChild(0).GetComponent<Text>().text = Level4Manager_MS.instance.Randomquestion(false);
                        if (transform.GetChild(0).GetComponent<Text>().text == "")
                        {
                            MiteorBlast();
                        rnd_miteor = 1;
                        goto Outer;
                    }
                        print("WrongText"+transform.GetChild(0).GetComponent<Text>().text);
                    isright = false;
                }
                print("call reach");
                for (int i = 0; i < Level4Manager_MS.instance.Question[Level4Manager_MS.instance.selectedPlanet].properties.Count; i++)
                {
                    if (Level4Manager_MS.instance.Question[Level4Manager_MS.instance.selectedPlanet].properties[i] == transform.GetChild(0).GetComponent<Text>().text)
                    {
                        isright = true;
                        break;
                    }
                    else
                    {
                        isright = false;
                    }
                }
                // Level4Manager_MissionSpace.instance.CurrentQuestion();
            }


           

           

           
            ismove = true;

         

        }

        public void changepos()
        {
            Level4Manager_MS.instance.CurrentQuestion();
            transform.position = new Vector2(point_right.position.x, transform.position.y);
            gameObject.SetActive(true);
            Invoke("ReachTargetpoint", .5F);
        }
       
       
    }
}
