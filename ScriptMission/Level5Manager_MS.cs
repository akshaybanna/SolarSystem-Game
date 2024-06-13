using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MissionSpace
{ 
[System.Serializable]
public class PlanetFact
{
   public string name;
   public List<string>  properties = new List<string>();
      
}
    public class Level5Manager_MS : MonoBehaviour
    {
        public static Level5Manager_MS instance;
        [Header("Game Value referances ")]
        public int currentQuestionNo;
        public int Coin;




        public PlanetFact[] Question;
        public PlanetFact[] Shuffled_question;

        public Sprite[] scientist;

        public GameObject[] random_object;
        public Text mainQuestion;

        [Header("Ui referances ")]
        public Image mainPlanet;
        public Text mainPlanet_text;
        public Image wallphoto;
        
        public Animator MsgBox;
        public Text Coin_text;
        public Transform [] Scientist_photo;
        public Transform planets;
        public GameObject MovingImageEffect;

        public GameObject[] Retry_led;
        public Button[] YesNo_button;

        CMS_Data cms_level = new CMS_Data();
        bool IsRightMove;
        public Animator BG;


        public GameObject[] ScreenPart;

        public Text timer_text;

        public Text Instruction_text;
        public GameObject KnowledgeFact;

        [HideInInspector] public bool IsTimerEnable;
        float CountDowntime = 240;  /// if cms not given timeperlevel then we use for ratting star calculate constant value 
        Text msgtext;
        public Text health;
        private void Awake()
        {
            instance = this;
        }
        public void InstrucationPanal_Active()
        {
            CMSValue(4);
            // instructionspanel.SetActive(true);

        }
        // Start is called before the first frame update
        public void StartGame()    
        {
            GameReset();
           
            shuffleQuestion();
            Random_question();
            msgtext = MsgBox.GetComponentInChildren<Text>();
            selectedPlanet = Level4Manager_MS.instance.selectedPlanet;
            // cms time
            CheckTimerPerLevel();
            //CoinUpdate();
        }

        //public void StartGame()
        //{
        //    Start();
        //   selectedPlanet = Level4Manager_MissionSpace.instance.selectedPlanet;
        //}



        // Update is called once per frame
        void Update()
        {

        }
        int Properties_rand = 0;
        public void Random_question()
        {

            mainPlanet.sprite = Level1Manager_MS.instance.PlanetIcon[Level4Manager_MS.instance.selectedPlanet];
            mainPlanet_text.text = mainPlanet.sprite.name;
            switch (Level4Manager_MS.instance.selectedPlanet)
            {
                case 0:

                    Scientist_photo[0].GetComponentInChildren<Text>().text = "Galileo Galilei and Thomas Harriot";
                    Scientist_photo[1].GetComponentInChildren<Text>().text = "William Herschel";

                    Scientist_photo[0].GetComponent<Image>().sprite = scientist[0];
                    Scientist_photo[1].GetComponent<Image>().sprite = scientist[2];

                    Scientist_photo[0].GetComponent<rightAnswer_MS>().IsRight = true;

                    break;

                case 1:

                    Scientist_photo[1].GetComponentInChildren<Text>().text = "Galileo Galilei";
                    Scientist_photo[0].GetComponentInChildren<Text>().text = "Jean Joseph Le Verrier";

                    Scientist_photo[1].GetComponent<Image>().sprite = scientist[0];
                    Scientist_photo[0].GetComponent<Image>().sprite = scientist[3];

                    Scientist_photo[1].GetComponent<rightAnswer_MS>().IsRight = true;
                    break;
                case 2:
                    Questionset();

                    ScreenOpen(1);

                    break;
                case 3:

                    Scientist_photo[0].GetComponentInChildren<Text>().text = "Galileo Galilei";
                    Scientist_photo[1].GetComponentInChildren<Text>().text = "William Herschel";

                    Scientist_photo[0].GetComponent<Image>().sprite = scientist[0];
                    Scientist_photo[1].GetComponent<Image>().sprite = scientist[2];

                    Scientist_photo[0].GetComponent<rightAnswer_MS>().IsRight = true;

                    break;
                case 4:

                    Scientist_photo[0].GetComponentInChildren<Text>().text = "Galileo Galilei";
                    Scientist_photo[1].GetComponentInChildren<Text>().text = "William Herschel";

                    Scientist_photo[0].GetComponent<Image>().sprite = scientist[0];
                    Scientist_photo[1].GetComponent<Image>().sprite = scientist[2];

                    Scientist_photo[0].GetComponent<rightAnswer_MS>().IsRight = true;
                    break;
                case 5:

                    Questionset();

                    ScreenOpen(1);
                    break;
                case 6:

                    Scientist_photo[0].GetComponentInChildren<Text>().text = "Galileo Galilei";
                    Scientist_photo[1].GetComponentInChildren<Text>().text = "William Herschel";

                    Scientist_photo[0].GetComponent<Image>().sprite = scientist[0];
                    Scientist_photo[1].GetComponent<Image>().sprite = scientist[2];

                    Scientist_photo[1].GetComponent<rightAnswer_MS>().IsRight = true;
                    break;
                case 7:

                    Scientist_photo[0].GetComponentInChildren<Text>().text = "Galileo Galilei";
                    Scientist_photo[1].GetComponentInChildren<Text>().text = "Jean Joseph Le Verrier";

                    Scientist_photo[0].GetComponent<Image>().sprite = scientist[0];
                    Scientist_photo[1].GetComponent<Image>().sprite = scientist[3];

                    Scientist_photo[1].GetComponent<rightAnswer_MS>().IsRight = true;
                    break;
            }
        }


        void shuffleQuestion()
        {
           
            for (int i = 0; i < Question.Length; i++)
            {
                Shuffled_question[i].properties.Clear();
                for (int j = 0; j < Question[i].properties.Count; j++)
                {
                    Shuffled_question[i].properties.Add(Question[i].properties[j]);
                }
            }
              
        }

        int selectedPlanet;
        bool clickonce;
        public void ClickOnPhoto(int id)
        {
            if (clickonce) return;
            clickonce = true;
            MovingImageEffect.transform.position = Scientist_photo[0].transform.position;


            if (Scientist_photo[id].GetComponent<rightAnswer_MS>().IsRight)
            {
                Scientist_photo[id].gameObject.SetActive(false);
                wallphoto.transform.GetChild(0).GetComponent<Image>().sprite = Scientist_photo[id].GetComponent<Image>().sprite;
                wallphoto.GetComponentInChildren<Text>().text = "Galileo Galilei";

                MovingImageEffect.SetActive(true);
                MovingImageEffect.GetComponent<ScientistMovingEffect_MS>().CallStart(wallphoto.transform.position, wallphoto.transform.GetChild(0).GetComponent<Image>().sprite, wallphoto.GetComponentInChildren<Text>().text);
            }
            else
            {

                BG.Play("wrongMove");
                Invoke("CallBGoff", 2f);
                // msgtext.text = "Wrong selection";
                //MsgBox.GetComponent<Animator>().Play("msganim");
                // CancelInvoke("WaitforMsg");
                //Invoke("WaitforMsg", 2f);
                clickonce = false;
            }


        }

        void CallBGoff()
        {
            BG.Play("idle");
        }

        public void ReachPhotoToTarget()
        {

            print("reach");
            MovingImageEffect.SetActive(false);
            wallphoto.gameObject.SetActive(true);
            Questionset();

            ScreenOpen(1);


           

        }

        void Questionset()
        {

           
           
         int wrong = Random.Range(0,Shuffled_question.Length);

         int rad = Random.Range(0, Shuffled_question[wrong].properties.Count);
            if (Shuffled_question[wrong].properties.Count == 0)
            {
                Questionset();
                return;
            }
            mainQuestion.text = Shuffled_question[wrong].properties[rad];
           // print(Shuffled_question[wrong].properties[rad]);
            for (int i = 0; i < Shuffled_question.Length; i++)
            {
               // print("i value"+i);
               // print("Shuffled_question[i].properties.Count" + Shuffled_question[i].properties.Count);
                for (int j = 0; j < Shuffled_question[i].properties.Count; j++)
                {
                  //  print("j value"+j);
                   // print(Shuffled_question[i].properties[j]);
                  //  print("Shuffled_question[wrong].properties[rad]" + mainQuestion.text);
                    if (Shuffled_question[i].properties[j] == mainQuestion.text)
                    {
                        print("remove position" + i + "------" + j);
                       Shuffled_question[i].properties.Remove(mainQuestion.text);
                    }
                }
            }
               
    
         
            //Shuffled_question[wrong].properties.RemoveAt(rad);
            UserInterAction(true);

        }
        void UserInterAction(bool Isbool)
        {
            for (int i = 0; i < YesNo_button.Length; i++)
            {
                YesNo_button[i].interactable = Isbool;
            }
        }
       
        public void clickOnyesNo(int id)
        {

            UserInterAction(false);
            IsRightMove = false;
            if (id == 0)
            {
                for (int i = 0; i < Question[selectedPlanet].properties.Count; i++)
                {
                    if (Question[selectedPlanet].properties[i] ==(mainQuestion.text))
                    {
                        IsRightMove = true;
                       
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Question[selectedPlanet].properties.Count; i++)
                {
                    if (Question[selectedPlanet].properties[i] != (mainQuestion.text))
                    {
                        IsRightMove = true;
                        
                    }
                    else
                    {
                        IsRightMove = false;
                        break;
                    }
                    
                }
            }

            if (IsRightMove)
            {
                print("right");
                SoundManager_MS.instanace.rightmovePlay();
            }    
            else
            {
                print("wrong");
                SoundManager_MS.instanace.WrongMovePlay();
            }
            Right_WrongMsgShow();
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


        void Right_WrongMsgShow()
        {
            print("wrong move");
            Text msgtext = MsgBox.GetComponentInChildren<Text>();
            if (IsRightMove)
            {

                msgtext.text = cms_level.RightMove_Msg;
                Coin ++;
                CoinUpdate();
                MsgBox.transform.GetChild(1).gameObject.SetActive(true);
                MsgBox.transform.GetChild(1).GetComponent<Image>().sprite = random_object[currentQuestionNo].GetComponent<Image>().sprite;
                MsgBox.GetComponent<Animator>().Play("msganim");
               CancelInvoke("WaitforMsg");
               Invoke("WaitforMsg", 2f);
            }
            else
            {
              //  print(cms_level1.Retry);
                cms_level.MaxWrongMove--;
                updateHeath(cms_level.MaxWrongMove);
                // Life_ChangeUpdate();
                msgtext.text = cms_level.WrongMove_Msg;
                MsgBox.transform.GetChild(1).gameObject.SetActive(false);
                //
                BG.Play("wrongMove");




                if (cms_level.MaxWrongMove >= 0)
                {
                        Retry_led[cms_level.MaxWrongMove].SetActive(true);
                        Retry_led[cms_level.MaxWrongMove].GetComponent<Animator>().Play("LED_blink");

                       
                }
                Invoke("WrongMoveEffect", 2f);



            }


        }

        void WrongMoveEffect()
        {
            BG.Play("idle");
            NextQuestionAfterMsg();
            if (cms_level.MaxWrongMove >= 0)
                
                Retry_led[cms_level.MaxWrongMove].GetComponent<Animator>().Play("idle");
          
        }

        void WaitforMsg()
        {
            MsgBox.GetComponent<Animator>().Play("return");
            // next question
            if (IsRightMove)
            {
               
                MovingImageEffect.SetActive(true);
                MsgBox.transform.GetChild(1).gameObject.SetActive(false);
                MovingImageEffect.transform.position = MsgBox.transform.GetChild(1).transform.position;
                MovingImageEffect.GetComponent<ScientistMovingEffect_MS>().CallStartForInstrument(random_object[currentQuestionNo].transform.position, random_object[currentQuestionNo].GetComponent<Image>().sprite);
                currentQuestionNo++;
                NextQuestionAfterMsg();
            }
           
               
           

           
        }

        void NextQuestionAfterMsg()
        {
            CheckGameOverORGameWin();

           
          
        }


        public void CompleteTravalImage()
        {
            MovingImageEffect.SetActive(false);
            random_object[currentQuestionNo-1].SetActive(true);
        }
        public void NextQuestion()
        {
            CheckGameOverORGameWin();


        }
        void CheckGameOverORGameWin()
        {

            if (currentQuestionNo == cms_level.MaxRightMove)
            {
                Invoke("GameWin", .5f);
            }
            else
            {
                Questionset();
            }
            if (cms_level.MaxWrongMove <= 0)
                GameOver();


        }
        

       
        //======//Gameover function ===============================================
        public void GameOver()
        {
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

        void GameWin()
        {
            
            UIManager_MS.instance.GameWin_button.gameObject.SetActive(false);
           

            UIManager_MS.instance.popsUps[0].popUpPanel.GetComponent<AudioSource>().clip=SoundManager_MS.instanace.GamewinSound;
            UIManager_MS.instance.WinningCharacter.SetActive(true);
            UIManager_MS.instance.WinningText.text = "MISSION ACCOMPLISHED FOR "+ Level1Manager_MS.instance.PlanetName[Level4Manager_MS.instance.selectedPlanet];
            
            planets.gameObject.SetActive(true);
            for (int i = 0; i < planets.childCount; i++)
            {
                if (Level4Manager_MS.instance.GetData(i.ToString()) == true)
                {
                   
                    planets.GetChild(i).transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    //  planets.GetChild(i).transform.GetChild(2).gameObject.SetActive(false);
                }

            }
            TimerCountDown_MS.instance.StopTimer();
           
            UIManager_MS.instance.OnClick_popupsButton(0);
            CheckWinStarRatting();

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
           // Coin_text.text = Coin.ToString();
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
            Coin = 0;
            IsRightMove = false;
            currentQuestionNo = 0;

            for (int i = 0; i < random_object.Length; i++)
            {
                random_object[i].SetActive(false);
            }
            wallphoto.transform.gameObject.SetActive(false);
            timer_text.text = "00:00";
            for (int i = 0; i < Scientist_photo.Length; i++)
            {
                Scientist_photo[i].gameObject.SetActive(true);
                Scientist_photo[i].GetComponent<rightAnswer_MS>().IsRight = false;
            }
            BG.Play("idle");
            MsgBox.GetComponent<Animator>().Play("idle");
            for (int i = 0; i < Retry_led.Length; i++)
            {
                Retry_led[i].GetComponent<Animator>().Play("idle");
                Retry_led[i].SetActive(false);
            }
            clickonce = false;
            UserInterAction(true);
            UIManager_MS.instance.GameWin_button.gameObject.SetActive(true);
            ScreenOpen(0);
            CancelInvoke("GameWin");
            CoinUpdate();
            InstrucationPanal_Active();
            UIManager_MS.instance.WiningPoupsound();
        }
        public void updateHeath(int text)
        {

            health.text = (text + 0).ToString();
        }
        #region BackedData
        void CMSValue(int currentLevel)
        {
            cms_level.LevelName = APIHandler_MS.instance._CMSDabase[currentLevel].LevelName;

            cms_level.NumberOfQuestion_CMS = APIHandler_MS.instance._CMSDabase[currentLevel].NumberOfQuestion_CMS;
            cms_level.MaxRightMove = APIHandler_MS.instance._CMSDabase[currentLevel].MaxRightMove>6?6:APIHandler_MS.instance._CMSDabase[currentLevel].MaxRightMove;
            cms_level.MaxWrongMove = APIHandler_MS.instance._CMSDabase[currentLevel].MaxWrongMove > 3 ? 3: APIHandler_MS.instance._CMSDabase[currentLevel].MaxWrongMove;
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
            for (int i = 0; i < APIHandler_MS.instance._Level5.Length; i++)
            {
                Question[i].properties.Clear();
                for (int j = 0; j < APIHandler_MS.instance._Level5[i].Properties.Count; j++)
                {
                    Question[i].properties.Add(APIHandler_MS.instance._Level5[i].Properties[j]);
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