using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissionSpace
{

    public class CharacterControllerScript_MS : MonoBehaviour
    {
        Rigidbody2D character;
       
        // Start is called before the first frame update
        void Start()
        {
            character =GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        void Run()
        {
       
            character.GetComponent<Animator>().SetTrigger("IsRun");
        }
        void Jump()
        {
            character.AddForce(Vector2.up * 400, ForceMode2D.Force);
            character.GetComponent<Animator>().SetTrigger("IsJump");
        }


   
        private void OnTriggerEnter2D(Collider2D collision)
        {
       
            if (collision.transform.tag == "On")
            {
                Level2Manager_MS.instance.PrepositionButt_Animation(true);
                Level2Manager_MS.instance.NearByPreposition_object = collision.transform.tag;
                Level2Manager_MS.instance.IsTriggerCoin = true;
                Level2Manager_MS.instance.NearByPreposition_coin = collision.transform.GetComponentInParent<GroundMoveScript_MS>().coin;
                Jump();
            }
            if (collision.transform.tag == "Coin")
            {
               
              
            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.tag == "Coin")
            {
                print("coin exit");
               
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.tag == "HeightExit")
            {
                Level2Manager_MS.instance.PrepositionButt_Animation(false);
                character.GetComponent<Animator>().SetTrigger("IsRun");
                Level2Manager_MS.instance.NearByPreposition_object = null;
                Level2Manager_MS.instance.IsTriggerCoin = false;
            }
        }

        void HideObject()
        {

        }
   
    }
}