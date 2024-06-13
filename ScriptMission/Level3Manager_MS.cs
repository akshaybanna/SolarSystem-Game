using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MissionSpace
{ 
[System.Serializable]
public class SolarProperties
{
        public List<string> Properties_question;
        public List<string> value;
}
public class Level3Manager_MS : MonoBehaviour
{
        public static Level3Manager_MS instance;
    [Header("Game Value referances ")]
    public int currentQuestionNo;
    public int Coin;
  // public int StoredCoin;



   // public SolarProperties[] _Question;
    public SolarProperties[] Shuffled_Question;
    public SolarProperties[] Question;
    public string[] BaseQuestion;
    public ShipMovementScript_MS _shipScript;

    [Header("Ui referances ")]
    public Text BaseQuestion_text;
    public Text MainQuestion_text;
    public Animator MsgBox;
    public Text Coin_text;
    public Text timer_text;
        public Text health;
        CMS_Data cms_level = new CMS_Data();
    bool IsRightMove;

        public Text Instruction_text;
        public GameObject KnowledgeFact;

        [HideInInspector] public bool IsTimerEnable;
        float CountDowntime = 240;  /// if cms not given timeperlevel then we use for ratting star calculate constant value 

  

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        public void InstrucationPanal_Active()
        {
            CMSValue(2);
            // instructionspanel.SetActive(true);

        }

        public  void StartGame()
        {

            //PlayerPrefs.DeleteKey("Coin"); 
            _shipScript.gameObject.transform.GetChild(0).GetComponent<Animator>().Play("idle");
           // CMSValue(2);
            RandomQuestion();
            print("start time");

            StartCoroutine("waitsomeTime");
            // cms time
            CheckTimerPerLevel();

        }


    IEnumerator waitsomeTime()
    {
         yield return new WaitForSeconds(.5f);
        _shipScript.MoveAutomaticSpaceship();
        Random_question();
        yield return new WaitForSeconds(1f);
            _shipScript.gameObject.transform.GetChild(0).GetComponent<Animator>().gameObject.SetActive(true);
            _shipScript.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Iscome");
            CancelInvoke("Sometime");
            Invoke("Sometime", .5f);
        }
    // Update is called once per frame
    void Update()
    {

    }
    int Properties_rand = 0;
        public void Random_question()
        {

            int Base_rand = Random.Range(0, Question.Length);
            int Properties_rand = Random.Range(0, Shuffled_Question[Base_rand].Properties_question.Count);
            BaseQuestion_text.text = Shuffled_Question[Base_rand].Properties_question[Properties_rand];
            MainQuestion_text.text = Shuffled_Question[Base_rand].value[Properties_rand];
            print(Shuffled_Question.Length);
            for (int i = 0; i < Shuffled_Question.Length; i++)
            {
                for (int j = 0; j < Shuffled_Question[i].value.Count; j++)
                {
                    print(Shuffled_Question[i].value[j] + "   -----" + MainQuestion_text.text);
                    if (Shuffled_Question[i].value[j] == MainQuestion_text.text)
                    {
                        print("match");
                        Shuffled_Question[i].Properties_question.Remove(BaseQuestion_text.text);
                        Shuffled_Question[i].value.Remove(MainQuestion_text.text);
                    }
                }
            }
        }
           
        
           

  


       

        

        void RandomQuestion()
        {
            for (int i = 0; i < Question.Length; i++)
            {
                Shuffled_Question[i].Properties_question.Clear();
                Shuffled_Question[i].value.Clear();
                for (int j = 0; j < Question[i].Properties_question.Count; j++)
                {
                    Shuffled_Question[i].Properties_question.Add(Question[i].Properties_question[j]);
                    Shuffled_Question[i].value.Add(Question[i].value[j]);
                }
               
                
            } 
        }


        public void ReachTopositoin()
        {
            if (IsRightMove)
            {
                Coin += 10;
                // StoredCoin = Coin;
                SoundManager_MS.instanace.pennyCollectPlay();
                CoinUpdate();
            }
           
        }

       public bool IsOnlyOnce;
        public void ClickOnPlanet(int id)
        {
            if (IsOnlyOnce) return;
            IsOnlyOnce = true;
            _shipScript.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Isreturn");
            print("click");
            for (int i = 0; i < Question[id].value.Count; i++)
            {
                if (Question[id].value[i] == (MainQuestion_text.text) )
                {
                    IsRightMove = true;
                  
                    _shipScript.RightAction(id);
                    break;
                }
                else
                {
                    
                    IsRightMove = false;
                }
            }
           
            Right_WrongMsgShow();
        }

        void Right_WrongMsgShow()
        {
            print("wrong move");
            Text msgtext = MsgBox.GetComponentInChildren<Text>();
            if (IsRightMove)
            {
                msgtext.text = cms_level.RightMove_Msg;
                SoundManager_MS.instanace.rightmovePlay();
                //CheckGameOverORGameWin();


            }
            else
            {
                print(cms_level.MaxWrongMove);
                cms_level.MaxWrongMove--;
                updateHeath(cms_level.MaxWrongMove);
                // Life_ChangeUpdate();
                msgtext.text = cms_level.WrongMove_Msg;
                _shipScript.GetComponent<Animator>().Play("shakeAnimation");
                _shipScript.StopShip();
               CancelInvoke("WaitforMsg");
                SoundManager_MS.instanace.WrongMovePlay();
                Invoke("WaitforMsg", 1f);
                //CheckGameOverORGameWin();
            }

           


           // MsgBox.GetComponent<Animator>().Play("msganim");
           
        }
        void WaitforMsg()
        {
          //  MsgBox.GetComponent<Animator>().Play("return");
           // _shipScript.GetComponent<Animator>().enabled = false;
            _shipScript.GetComponent<Animator>().Play("idle");
            // next question
            _shipScript.StartShip();
            if (!IsRightMove)
            checkwrong();
         



        }  
        public void NextQuestion()
        {
           
            CheckGameOverORGameWin();
           
           
        }
        void CheckGameOverORGameWin()
        {
            if (currentQuestionNo == cms_level.MaxRightMove)
            {
                GameWin();
            }else
            {
                 //   currentQuestionNo++;
                StartCoroutine("waitsomeTime");
            }
            
        }
        void checkwrong()
        {
            print(cms_level.MaxWrongMove);
            if (cms_level.MaxWrongMove < 0)
            {
                _shipScript.Destory();
               Invoke("GameOver",1f);
              //  MsgBox.GetComponent<Animator>().Play("return");
                
                _shipScript.StopShip();
            }else
                NextQuestion();
           
           
        }
        void Sometime()
        {
            IsOnlyOnce = false;
        }

       
        //======//Gameover function ===============================================
        public void GameOver()
        {

            TimerCountDown_MS.instance.StopTimer();
            UIManager_MS.instance.OnClick_popupsButton(1);
            _shipScript.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Isreturn");

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
            SaveCoin();
            TimerCountDown_MS.instance.StopTimer();
            //StarRating();
            UIManager_MS.instance.OnClick_popupsButton(0);
            _shipScript.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Isreturn");
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


        public void SaveCoin()
        {
            PlayerPrefs.SetInt("Coin",Coin);
        }
       public void GetCoin()
        {
            if (PlayerPrefs.HasKey("Coin"))
            {
                Coin=PlayerPrefs.GetInt("Coin");
                CoinUpdate();
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
           // Start();
        }
       public void GameReset()
        {
            //Coin = 0;
            IsRightMove = false;
            CancelInvoke("Sometime");
            
            currentQuestionNo = 0;
            _shipScript.GetComponent<Animator>().Play("idle");
            _shipScript.gameObject.transform.GetChild(0).GetComponent<Animator>().gameObject.SetActive(false);
            CoinUpdate();
            timer_text.text = "00:00";
            _shipScript.Reset();
            _shipScript.StopShip();
            GetCoin();
            InstrucationPanal_Active();
            CancelInvoke("GameOver");
            UIManager_MS.instance.WiningPoupsound();
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
            int j;
            for (int i = 0; i < APIHandler_MS.instance._Level3.Length; i++)
            {
                Question[i].Properties_question.Clear();
                Question[i].value.Clear();
                switch (i)
                {
                    case 0:
                         j = 0;
                        for (int k = 0; k < APIHandler_MS.instance._Level3[j].Properties.Count; k++)
                        {
                            Question[i].Properties_question.Add(APIHandler_MS.instance._Level3[j].Question[k]);
                            Question[i].value.Add(APIHandler_MS.instance._Level3[j].Properties[k]);

                        }


                        break;
                    case 1:
                         j = 3;
                        for (int k = 0; k < APIHandler_MS.instance._Level3[j].Properties.Count; k++)
                        {
                            Question[i].Properties_question.Add(APIHandler_MS.instance._Level3[j].Question[k]);
                            Question[i].value.Add(APIHandler_MS.instance._Level3[j].Properties[k]);

                        }

                        break;
                    case 2:
                        j = 1;
                        for (int k = 0; k < APIHandler_MS.instance._Level3[j].Properties.Count; k++)
                        {
                            Question[i].Properties_question.Add(APIHandler_MS.instance._Level3[j].Question[k]);
                            Question[i].value.Add(APIHandler_MS.instance._Level3[j].Properties[k]);

                        }

                        break;
                    case 3:
                        j = 2;
                        for (int k = 0; k < APIHandler_MS.instance._Level3[j].Properties.Count; k++)
                        {
                            Question[i].Properties_question.Add(APIHandler_MS.instance._Level3[j].Question[k]);
                            Question[i].value.Add(APIHandler_MS.instance._Level3[j].Properties[k]);

                        }

                        break;
                    case 4:
                        j = 7;
                        for (int k = 0; k < APIHandler_MS.instance._Level3[j].Properties.Count; k++)
                        {
                            Question[i].Properties_question.Add(APIHandler_MS.instance._Level3[j].Question[k]);
                            Question[i].value.Add(APIHandler_MS.instance._Level3[j].Properties[k]);

                        }

                        break;
                    case 5:
                        j = 6;
                        for (int k = 0; k < APIHandler_MS.instance._Level3[j].Properties.Count; k++)
                        {
                            Question[i].Properties_question.Add(APIHandler_MS.instance._Level3[j].Question[k]); ;
                            Question[i].value.Add(APIHandler_MS.instance._Level3[j].Properties[k]);

                        }

                        break;
                    case 6:
                        j = 5;
                        for (int k = 0; k < APIHandler_MS.instance._Level3[j].Properties.Count; k++)
                        {
                            Question[i].Properties_question.Add(APIHandler_MS.instance._Level3[j].Question[k]);
                            Question[i].value.Add(APIHandler_MS.instance._Level3[j].Properties[k]);

                        }

                        break;
                    case 7:
                        j = 4;
                        for (int k = 0; k < APIHandler_MS.instance._Level3[j].Properties.Count; k++)
                        {
                            Question[i].Properties_question.Add(APIHandler_MS.instance._Level3[j].Question[k]);
                            Question[i].value.Add(APIHandler_MS.instance._Level3[j].Properties[k]);

                        }

                        break;
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