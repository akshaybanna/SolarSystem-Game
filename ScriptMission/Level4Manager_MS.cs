using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MissionSpace
{ 
[System.Serializable]
public class PlanetProperties
{
   public string name;
   public List<string> properties = new List<string>();
      
}
    public class Level4Manager_MS : MonoBehaviour
    {
        public static Level4Manager_MS instance;
        [Header("Game Value referances ")]
        public int currentQuestionNo;
        public int Coin;
        public string [] Light_Minutes;
        public Transform Button_Light_Minutes;


        public PlanetProperties[] Shuffled_Question;
        public PlanetProperties[] Question;
        
       

        [Header("Ui referances ")]
        public Image mainPlanet;
        public Text heading_text;
        public Text BaseQuestion_text;
        public Text MainQuestion_text;
        public Animator MsgBox;
        public Text Coin_text;
        public Transform button_parent;
        public Transform planets;
        public Image Fuel_gameplay;

        public MoveObject_MS rocket;
        public float speed=3;

        CMS_Data cms_level = new CMS_Data();
        bool IsRightMove;
        public bool IsGameover;
        public Animator Fuel;

        public Button StartTour_button;
        

        public GameObject[] ScreenPart;
        public GameObject[] RandomObject;
        public ParticleSystem DestoryEffect;
        public GameObject instructionspanel;


        public Text timer_text;

        [HideInInspector] public bool IsTimerEnable;
        float CountDowntime = 240;  /// if cms not given timeperlevel then we use for ratting star calculate constant value 
        Text msgtext;

        public Text Instruction_text;
        public GameObject KnowledgeFact;

        public Text health;
        private void Awake()
        {
            instance = this;
           
        }
        private void OnEnable()
        {
            
        }

        // Start is called before the first frame update
        void Start()    
        {
         //   PlayerPrefs.DeleteKey("Unlock");
             msgtext = MsgBox.GetComponentInChildren<Text>();
        }
        public void InstrucationPanal_Active()
        {
            CMSValue(3);
           instructionspanel.SetActive(true);

        }
        public void StartGame()
        {
           // CMSValue(3);
            msgtext = MsgBox.GetComponentInChildren<Text>();
            Coin = Level3Manager_MS.instance.Coin;
            CoinUpdate();
            RandomProperties();

           
            //TimerCountDown.instance.startTimer(CountDowntime, timer_text);
        }


        void RandomProperties()
        {
           
            for (int i = 0; i < Question.Length; i++)
            {
                Shuffled_Question[i].properties.Clear();
                for (int j = 0; j < Question[i].properties.Count; j++)
                {
                    Shuffled_Question[i].properties.Add(Question[i].properties[j]);
                }
               
            }
        }




        public int selectedPlanet;
        public void ClickOnPlanet(int id)
        {
            heading_text.text = "select the correct distance of "+ Level1Manager_MS.instance.PlanetIcon[id].name  + " in light minutes";
            mainPlanet.sprite = Level1Manager_MS.instance.PlanetIcon[id];
            selectedPlanet = id;
            Fuel.gameObject.SetActive(false);
            ScreenPart[1].SetActive(true);
           

        }

        bool isLightMinClick;
        public void clickOnLightMinButton(int id)
        {

            isLightMinClick = true;
            StartTour_button.interactable = true;
            print(button_parent.GetChild(id).GetComponentInChildren<Text>().text + " " + Light_Minutes[selectedPlanet]);
            if (button_parent.GetChild(id).GetComponentInChildren<Text>().text==(Light_Minutes[selectedPlanet]))

                msgtext.text = "Right selection you are ready for tour";

            else
                msgtext.text = "Wrong selection. Correct light Distace is " + Light_Minutes[selectedPlanet] +". but you are ready for tour";

            //selectedPlanet = id;
            MsgBox.GetComponent<Animator>().Play("msganim");

            StartTour_button.GetComponent<Animator>().enabled = true;
            CancelInvoke("WaitforMsg");
            Invoke("WaitforMsg", 1.5f);
        }
        public string PlanetString;
        void SaveData(string planetIndex)
        {
           // if(PlayerPrefs.HasKey("Unlock"))
            {

                if (PlayerPrefs.GetString("Unlock").Contains(planetIndex) == false)
                {
                    PlayerPrefs.SetString("Unlock", PlayerPrefs.GetString("Unlock")+ planetIndex + ",");
                }
           
            }
           // PlayerPrefs.SetString();
        }

    public bool GetData(string PlanetIndex)
        {
            bool isContain =false;
            print(PlayerPrefs.GetString("Unlock"));
            if (PlayerPrefs.HasKey("Unlock"))
            {
                if (PlayerPrefs.GetString("Unlock").Contains(PlanetIndex))
                {
                    isContain = true;
              
                }
            }
            return isContain;
        }

        void Light_mintuesButton(bool isbool)
        {
            for (int i = 0; i < Button_Light_Minutes.childCount; i++)
            {
                Button_Light_Minutes.transform.GetChild(i).GetComponentInChildren<Text>().text = Light_Minutes[i];
                Button_Light_Minutes.transform.GetChild(i).GetComponent<Button>().interactable = isbool;
            }
        }
        public void StartTour()
        {

           
            if (!isLightMinClick)
            {
                msgtext.text = "Please select the distance";
                MsgBox.GetComponent<Animator>().Play("msganim");
                CancelInvoke("WaitforMsg");
                Invoke("WaitforMsg", 1.5f);
                isLightMinClick = false;
            }
            else

            if(!planets.GetChild(selectedPlanet).GetChild(0).gameObject.activeSelf)
            {
                StartTour_button.interactable = false;
                Fuel.gameObject.SetActive(true);
                Light_mintuesButton(false);
                Invoke("instructionpanel_part2", 2f);

            }
            else if (int.Parse(planets.GetChild(selectedPlanet).GetComponentInChildren<Text>().text) <= Level3Manager_MS.instance.Coin)
            {

                StartTour_button.interactable = false;
                print("call");
                Level3Manager_MS.instance.Coin -= int.Parse(planets.GetChild(selectedPlanet).GetComponentInChildren<Text>().text);
                Coin = Level3Manager_MS.instance.Coin;
                Level3Manager_MS.instance.SaveCoin();
                CoinUpdate();
                Fuel.gameObject.SetActive(true);
                
                Invoke("instructionpanel_part2", 2f);
                //print("sadfas");
            }
            else
            {
                msgtext.text = "You don't have enough Pennies. \n Play Level 3  Again";
                MsgBox.GetComponent<Animator>().Play("msganim");
                CancelInvoke("WaitforMsg");
                Invoke("WaitforMsg", 1.5f);
            }

            StartTour_button.GetComponent<Animator>().enabled = false;

        }

        void instructionpanel_part2()
        {
            ScreenOpen(2);
            UIManager_MS.instance.ActivatePopByIndex(7);
        }

        public void waitforFilingfule()
        {
            

          
            StartTour_button.interactable = true;
            for (int i = 0; i < RandomObject.Length; i++)
            {
                RandomObject[i].GetComponent<MoveObject_MS>().GameStart();
            }
            // cms time
            CheckTimerPerLevel();
        }
       
        void ScreenOpen(int id)
        {
            for (int i = 0; i < ScreenPart.Length; i++)
            {
                ScreenPart[i].SetActive(false);
                if (id == i)
                {
                    ScreenPart[i].SetActive(true);
                }
            }
        }

        public void CurrentQuestion()
        {
            currentQuestionNo++;
            print("call current question");
        }
        int maxRightMove;
        void Right_WrongMsgShow()
        {
            print("wrong move");
            Text msgtext = MsgBox.GetComponentInChildren<Text>();
            if (IsRightMove)
            {
                SoundManager_MS.instanace.rightmovePlay();
                msgtext.text = cms_level.RightMove_Msg;
                maxRightMove++;
               // Coin += 10;
                //CoinUpdate();
                if (speed<=5.5f)
                speed += .5f;
                CheckGameOverORGameWin();
            }
            else
            {
                print(cms_level.MaxWrongMove);
                cms_level.MaxWrongMove--;
                updateHeath(cms_level.MaxWrongMove);
                SoundManager_MS.instanace.WrongMovePlay();
                if (speed >= 2)
                {
                    speed -= .5f;
                }
               
                Fuel_gameplay.fillAmount =(float) (Fuel_gameplay.fillAmount / 2f);
                // Life_ChangeUpdate();
                msgtext.text = cms_level.WrongMove_Msg;
                checkwrong();
            }


           

            //MsgBox.GetComponent<Animator>().Play("msganim");
            //CancelInvoke("WaitforMsg");
            //Invoke("WaitforMsg", 2f);
        }
        void WaitforMsg()
        {
            MsgBox.GetComponent<Animator>().Play("return");
            // next question
          //  if (!IsRightMove)           
        }
        public void NextQuestion(bool right)
        {
            IsRightMove = right;
            Right_WrongMsgShow();
            print("call"+ IsRightMove);
           

        }


        public string Randomquestion(bool isright)
        {
            string properties = "";
            if (isright)
            {

                int rnd = Random.Range(0, Shuffled_Question[selectedPlanet].properties.Count);
                print("rnd value--------------" + Shuffled_Question[selectedPlanet].properties.Count);
                if (Shuffled_Question[selectedPlanet].properties.Count == 0)
                    return "";
                properties = Shuffled_Question[selectedPlanet].properties[rnd];
                Shuffled_Question[selectedPlanet].properties.RemoveAt(rnd);


            }
            else
            {
                int rnd = Random.Range(0, 8);
               // print("rnd value--------------" + rnd);


                int rnd_pro = Random.Range(0, Shuffled_Question[rnd].properties.Count);
                print("rnd_pro=============" + Shuffled_Question[rnd].properties.Count);
                if (Shuffled_Question[rnd].properties.Count == 0)
                    return "";
               
                properties = Shuffled_Question[rnd].properties[rnd_pro];
               
               
                Shuffled_Question[rnd].properties.RemoveAt(rnd_pro);
            }


           
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < Shuffled_Question[i].properties.Count; j++)
                    {
                        if (Shuffled_Question[i].properties[j] == properties)
                        {
                            Shuffled_Question[i].properties.Remove(properties);
                        }
                    }
                    
                }
           
            CheckGameOverORGameWin();
            return properties;
            
        }
        void CheckGameOverORGameWin()
        {
            if ((currentQuestionNo) >= cms_level.NumberOfQuestion_CMS )
            {
                if(maxRightMove >= 2)
                {
                    if (IsGameover)
                        return;
                    GameWin();
                    SaveData(selectedPlanet.ToString());
                }
                else
                {
                    GameOver();
                }
                
            }
            else
            {

                print("call wrong quesetin");
                //currentQuestionNo++;

            }

        }
        void checkwrong()
        {
           // print(cms_level1.Retry);
            if (cms_level.MaxWrongMove <= 0)
            {

                Invoke("GameOver",1f);
                if(rocket!=null)
                rocket.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                DestoryEffect.Play();
                MsgBox.GetComponent<Animator>().Play("return");
         
            }
           
        }

     
        //==========check Retry condition=============
        void CheckRetry()
        {
            if (cms_level.MaxWrongMove == 0)
            {


                //UIManager_Preposition.instance.Retry_life.SetActive(false);

            }
            else
            {
                //UIManager_Preposition.instance.Retry_life.SetActive(true);

            }
            Life_ChangeUpdate();
        }
        //======//Gameover function ===============================================
        public void GameOver()
        {

           
            IsGameover = true;

            TimerCountDown_MS.instance.StopTimer();
            UIManager_MS.instance.OnClick_popupsButton(1);


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
            IsGameover = true;
            TimerCountDown_MS.instance.StopTimer();
            
            UIManager_MS.instance.OnClick_popupsButton(0);
            CheckWinStarRatting();
            UIManager_MS.instance.SetLevelLock();
            UIManager_MS.instance.LevelUnlockLock();

        }
        void CheckWinStarRatting()
        {
            if (cms_level.TimePerLevel == 0)
            {
                if (TimerCountDown_MS.instance.timeLeft >= 180)
                    UIManager_MS.instance.WinStarActive(3);
                else if (TimerCountDown_MS.instance.timeLeft >= 120 && TimerCountDown_MS.instance.timeLeft < 180)
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


        void CoinUpdate()
        {
           
            Coin_text.text = Coin.ToString();
        }
        void Life_ChangeUpdate()
        {
            // Retry_life.transform.GetChild(0).GetComponentInChildren<Text>().text = cms_level1.Retry.ToString();
        }

        public void Retry()
        {
            GameReset();
            StartGame();
        }
        public void GameReset()
        {
            print("call");
            CancelInvoke("GameOver");
            Level3Manager_MS.instance.GetCoin();
            Coin = Level3Manager_MS.instance.Coin;
            CoinUpdate();
            IsRightMove = false;
            IsGameover = false;
            currentQuestionNo = 0;
            for (int i = 0; i < RandomObject.Length; i++)
            {
                RandomObject[i].GetComponent<MoveObject_MS>().StopGame();
            }

            speed = 3;
            Fuel_gameplay.fillAmount = 1;
            StartTour_button.GetComponent<Animator>().enabled = false;
            isLightMinClick = false;
            maxRightMove = 0;
            timer_text.text = "00:00";
            // StartTour_button.interactable = false   ;
            if (rocket!=null)
            rocket.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            for (int i = 0; i < planets.childCount; i++)
            {
                if(GetData(i.ToString())==true)
                {
                    planets.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
                    planets.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
                    planets.GetChild(i).transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                  //  planets.GetChild(i).transform.GetChild(2).gameObject.SetActive(false);
                }
               
            }
            Light_mintuesButton(true);
            InstrucationPanal_Active();
           // CancelInvoke();
            ScreenOpen(0);
            UIManager_MS.instance.WiningPoupsound();


            CoinUpdate();   


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
            for (int i = 0; i < APIHandler_MS.instance._Level4.Length; i++)
            {
                Question[i].properties.Clear();
                for (int j = 0; j < APIHandler_MS.instance._Level4[i].Properties.Count; j++)
                {
                   Question[i].properties.Add(APIHandler_MS.instance._Level4[i].Properties[j]);
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