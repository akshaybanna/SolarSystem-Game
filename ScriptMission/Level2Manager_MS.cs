using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MissionSpace
{
  
  
    public class Level2Manager_MS : MonoBehaviour
    {

        public static Level2Manager_MS instance;

       


      
        public int Coin;

        public Animator MsgBox;
        public Transform [] orbit_point;


        [Header("Ui referances ")]
       public Image[] planet_images;
        public Text timer_text;

        //private variable
        private bool IsShoot;
        Vector2 DeflautBulletPos;
        GameObject TargetBubble;
        public bool IsRightMove;
        public Transform currentPlanet;

        public string [] lightmin;
        public string [] Km;
        public Text[] lightmin_text;
        public Text[] km_text;
        CMS_Data cms_level = new CMS_Data();
       

        [HideInInspector]public bool IsTimerEnable;
        [HideInInspector] public bool ISGameOver;
        [HideInInspector] public bool IsTriggerCoin;
        [HideInInspector] public GameObject NearByPreposition_coin;
        [HideInInspector] public string NearByPreposition_object;
        float CountDowntime=240;  /// if cms not given timeperlevel then we use for ratting star calculate constant value 

        public Text Instruction_text;
        public GameObject KnowledgeFact;
        public Text health;
        private void Awake()
        {
            instance = this;
        }



        public void InstrucationPanal_Active()
        {
            CMSValue(1);
            // instructionspanel.SetActive(true);

        }




        // Start is called before the first frame update
        public void   StartGame()
        {
           
           Level1Manager_MS.instance.ShufflePlanetNameFunction(Level1Manager_MS.instance.PlanetName, Level1Manager_MS.instance.PlanetIcon);
            SetIcon();

            for (int i = 0; i < orbit_point.Length; i++)
            {
                orbit_point[i].GetChild(0).GetComponentInChildren<Animator>().enabled = false;
            }

            // PrepositionWordAssign();
            // MsgBox.gameObject.SetActive(false);


            // cms time
            CheckTimerPerLevel();
        }

        void SetIcon()
        {
            for (int i = 0; i < Level1Manager_MS.instance.planet_images.Length; i++)
            {
                // print("call");
                planet_images[i].gameObject.SetActive(true);
                planet_images[i].sprite = Level1Manager_MS.instance.random_PlanetIcon[i];
                planet_images[i].GetComponent<mousedrag_MS>().orbit = checkPlanetOrbit(Level1Manager_MS.instance.random_planetList[i]);
            }
        }
        int checkPlanetOrbit(string planetname)
        {
           

            switch (planetname)
            {
                case "MERCURY":
                    return 0;

                case "VENUS":
                    return 1;

                case "EARTH":
                    return 2;

                case "MARS":
                    return 3;

                case "JUPITER":
                    return 4;

                case "SATURN":
                    return 5;

                case "URANUS":
                    return 6;

                case "NEPTUNE":
                    return 7;
            }
            return 0;
        }
        // Update is called once per frame
      

        public void  PrepositionButt_Animation(bool Istrue)
        {
            foreach (var item in UIManager_MS.instance.PrepositionWord_Button)
            {
                item.interactable= (Istrue);
            }
        }

      





    





    //void Right_WrongMsgShow()
    //{


    //}

    void WaitforMsg()
        {
            MsgBox.gameObject.SetActive(false);
        }
        int rightDrop;
       public void CheckGameOverORGameWin(bool Istrue)
        {

            if (Istrue)
            {
                rightDrop++;
                SoundManager_MS.instanace.rightmovePlay();
                if (rightDrop == 8)
                {
                   Invoke("GameWin",4);
                }
            }
            else
            {
                
                cms_level.MaxWrongMove--;
                print(cms_level.MaxWrongMove);
                updateHeath(cms_level.MaxWrongMove);
                Handheld.Vibrate();
                SoundManager_MS.instanace.WrongMovePlay();
                if (cms_level.MaxWrongMove < 0)
                {

                   
                    GameOver();
                }
            }
            
        }

        public void TimeUp()
        {
            if ((Coin >= cms_level.MaxRightMove))
            {
                GameWin();
              
            }
        }

        //public void  CheckOrbit(string name)
        //{
        //    print(name);
        //    if(name==)
        //    currentPlanet = null;
        //}
       

        //=====================================================================================

     
        //======//Gameover function ===============================================
        public void GameOver()
        {
            
            TimerCountDown_MS.instance.StopTimer();
            UIManager_MS.instance.OnClick_popupsButton(1);
          

        }
        void CheckWinStarRatting()
        {
            if (cms_level.TimePerLevel == 0)
            {
                if (TimerCountDown_MS.instance.timeLeft >= 180)
                    UIManager_MS.instance.WinStarActive(3);
                else if (TimerCountDown_MS.instance.timeLeft >=120 && TimerCountDown_MS.instance.timeLeft < 180)
                {
                    UIManager_MS.instance.WinStarActive(2);
                    //  print("star2");
                }
                else
                    UIManager_MS.instance.WinStarActive(1);

            }
            else
            {
                if (TimerCountDown_MS.instance.timeLeft >= (cms_level.TimePerLevel - cms_level.RattingStar3_Time))
                    UIManager_MS.instance.WinStarActive(3);
                else if (TimerCountDown_MS.instance.timeLeft >= (cms_level.TimePerLevel - cms_level.RattingStar2_Time) && TimerCountDown_MS.instance.timeLeft < (cms_level.TimePerLevel - cms_level.RattingStar3_Time))
                {
                    UIManager_MS.instance.WinStarActive(2);
                    //  print("star2");
                }
                else
                    UIManager_MS.instance.WinStarActive(1);

            }
            
        }
        public void Timerup()
        {

            TimerCountDown_MS.instance.StopTimer();
            UIManager_MS.instance.OnClick_popupsButton(10);


        }
        ///=========================================================//////////////////
        //Gameover function 
        public void GameWin()
        {
            
            TimerCountDown_MS.instance.StopTimer();
            CheckWinStarRatting();
            UIManager_MS.instance.OnClick_popupsButton(0);
            UIManager_MS.instance.SetLevelLock();
            UIManager_MS.instance.LevelUnlockLock();


        }
        public void SetRaycasting(Image _script)
        {
            for (int i = 0; i < planet_images.Length; i++)
            {
                planet_images[i].raycastTarget = false;
            }
            _script.raycastTarget = true;
        }
        public void ReSetRaycasting()
        {
            for (int i = 0; i < planet_images.Length; i++)
            {
                planet_images[i].raycastTarget = true;
            }
        }
       
       public void resetGame()
        {
            rightDrop = 0;
            IsRightMove = false;
            print("reset");
           
            for (int i = 0; i < planet_images.Length; i++)
            {
                planet_images[i].transform.localPosition = planet_images[i].transform.parent.GetChild(3).transform.localPosition;
                planet_images[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < orbit_point.Length; i++)
            {
                orbit_point[i].GetChild(0).gameObject.SetActive(false);
            }
            timer_text.text = "00:00";
            ReSetRaycasting();
            CancelInvoke("GameWin");
            InstrucationPanal_Active();
            UIManager_MS.instance.WiningPoupsound();
        }

        void CoinUpdate()
        {
            UIManager_MS.instance.Coin_text.text = Coin.ToString();
        }
        public void updateHeath(int text)
        {

            health.text = (text + 1).ToString();
        }
        #region BackedData
        void CMSValue(int currentLevel)
        {
            cms_level.LevelName = APIHandler_MS.instance._CMSDabase[currentLevel].LevelName;

            cms_level.NumberOfQuestion_CMS = APIHandler_MS.instance._CMSDabase[currentLevel].NumberOfQuestion_CMS;
            cms_level.MaxRightMove = APIHandler_MS.instance._CMSDabase[currentLevel].MaxRightMove;
            cms_level.MaxWrongMove = APIHandler_MS.instance._CMSDabase[currentLevel].MaxWrongMove;
            updateHeath(cms_level.MaxWrongMove);

            cms_level.RightMove_Msg = APIHandler_MS.instance._CMSDabase[currentLevel].RightMove_Msg;
            cms_level.WrongMove_Msg = APIHandler_MS.instance._CMSDabase[currentLevel].WrongMove_Msg;

            cms_level.TimePerLevel = APIHandler_MS.instance._CMSDabase[currentLevel].TimePerLevel;

            cms_level.Instrucation = APIHandler_MS.instance._CMSDabase[currentLevel].Instrucation;
            cms_level.Knowledge_Fact = APIHandler_MS.instance._CMSDabase[currentLevel].Knowledge_Fact;
            cms_level.How_To_Play = APIHandler_MS.instance._CMSDabase[currentLevel].How_To_Play;

            cms_level.RattingStar1_Time = APIHandler_MS.instance._CMSDabase[currentLevel].RattingStar1_Time;
            cms_level.RattingStar2_Time = APIHandler_MS.instance._CMSDabase[currentLevel].RattingStar2_Time;
            cms_level.RattingStar3_Time = APIHandler_MS.instance._CMSDabase[currentLevel].RattingStar3_Time;


            // check Cms Knowledge enable or disable 
            CheckKnowledgeFact();
            //set instrucation win 
            SetInstrucationText();
            // set how to play Text and vedio
            UIManager_MS.instance.SetHowToPlayText();

            AditionalDataSet();



        }

        void AditionalDataSet()
        {
            // Aditional Data load
            for (int i = 0; i < APIHandler_MS.instance._level2.Length; i++)
            {

               
                lightmin_text[i].text = APIHandler_MS.instance._level2[i].Lightmin;
                if (lightmin_text[i].text=="")
                {
                    lightmin_text[i].transform.parent.GetComponent<Text>().text = "";
                }
                    km_text[i].text = APIHandler_MS.instance._level2[i].km;
                if (km_text[i].text == "")
                {
                    km_text[i].transform.parent.GetComponent<Text>().text = "";
                }
            }
        }
        //=========check timer per level condition ================
        void CheckTimerPerLevel()
        {
            if (cms_level.TimePerLevel == 0)
            {
                IsTimerEnable = false;
                timer_text.transform.parent.gameObject.SetActive(false);
                TimerCountDown_MS.instance.startTimer(CountDowntime, true);
            }
            else
            {
                IsTimerEnable = true;
                timer_text.transform.parent.gameObject.SetActive(true);
                TimerCountDown_MS.instance.startTimer(cms_level.TimePerLevel, timer_text);

            }
        }
        //=====================================================================================
        // check Disable or Enable Knowledge Fact
        void CheckKnowledgeFact()
        {
            if (cms_level.Knowledge_Fact == "")
                KnowledgeFact.SetActive(false);
            else
            {
                KnowledgeFact.SetActive(true);
                UIManager_MS.instance.SetknowledgeFactTextAccordingTolevel();
            }

        }
        // check Disable or Enable Knowledge Fact
        void SetInstrucationText()
        {
            Instruction_text.text = cms_level.Instrucation;
        }
        #endregion
    }
}
