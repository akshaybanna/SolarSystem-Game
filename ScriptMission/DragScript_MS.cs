using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MissionSpace
{
    public class DragScript_MS : MonoBehaviour
    {
       public bool Istouch;
        Vector2 startpos;
        GameObject currentobject;
        public Color Press_color;
        public Color text_color;
        public Color Default_color;
        public Color Default_text_color;
       public bool onlyfirsttime = false;
        // Start is called before the first frame update
        void Start()
        {
            onlyfirsttime = true;
            startpos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (Istouch)
            {
                Vector2 v;
               
              
                    v = Camera.main.ScreenToWorldPoint(Input.mousePosition);



                transform.position = v;
               // Level1Manager_Preposition.instance.NearByLocation(transform);
            }
        }


      

        public void OnPointerEnter()
        {
            if (Istouch) return;
           GetComponent<Animator>().enabled = false;
            Istouch = true;
           Level1Manager_MS.instance.SetRaycasting (transform.name);
          Coloreffect(text_color, Press_color, new Vector2(1.2f, 1.2f));

        }
        void Coloreffect(Color textcolor, Color image_color, Vector2 size)
        {
            print(transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
            transform.GetChild(0).GetChild(0).GetComponent<Text>().color = textcolor;
            transform.GetChild(0).GetChild(1).GetComponent<Image>().color = image_color;
            transform.GetChild(0).GetChild(2).GetComponent<Image>().color = image_color;
            transform.GetChild(0).transform.localScale = size;
        }
        public void OnPointerExit()
        {

            Istouch = false;
            print("exit");
           string text = Level1Manager_MS.instance.NearByLocation(this.transform);
            print(text);
            if (text!=null)
            {
                if (text == transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
                {
                    gameObject.transform.parent.gameObject.SetActive(false);
                    transform.position = startpos;
                   


                    Level1Manager_MS.instance.AfterDragLetter(true);
                }
                else
                {
                    print("wrong");
                    transform.position = startpos;
                    GetComponent<Animator>().enabled = true;
                   
                    Level1Manager_MS.instance.AfterDragLetter(false);
                }
            }
            else
            {
                transform.position = startpos;
                GetComponent<Animator>().enabled = true;
                
            }
            // if()
            Coloreffect(Default_text_color, Default_color, new Vector2(1, 1));


            Level1Manager_MS.instance.ReSetRaycasting();
        }
       
        public void ResetPosition()
        {
            if(onlyfirsttime ==true)
            {
                transform.position = startpos;
                Istouch = false;
                Level1Manager_MS.instance.ReSetRaycasting();
                Coloreffect(Default_text_color, Default_color, new Vector2(1, 1));
            }
        }
            
            // GetComponent<Animator>().enabled = true;
      
       
    }   
}
