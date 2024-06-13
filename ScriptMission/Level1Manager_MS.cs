using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MissionSpace
{


    public class Level1Manager_MS : MonoBehaviour
    {



        public static Level1Manager_MS instance;

        [Header("Game Value referances ")]
        public int SelectedPlanet;
        public int Coin;




        [Header("Game supported referances ")]
        public GameObject bubblePrefab;
        public List<string> PlanetName = new List<string>();
        public List<Sprite> PlanetIcon = new List<Sprite>();

        public List<string> random_planetList = new List<string>();
        public List<Sprite> random_PlanetIcon = new List<Sprite>();
        public GameObject bullet;

        public Image MainPlanet;
        public Transform TextPart_Object;

        public GameObject Char_Prefab;
        public Transform parent_char;
        public GameObject SelectedText;




        [Header("Ui referances ")]
        public Transform[] Random_point;
        public Image[] planet_images;
        public Sprite Default_sprite;
        public Animator MsgBox;



        //private variable
        private bool IsShoot;
        Vector2 DeflautBulletPos;
        GameObject TargetBubble;
        bool IsRightMove;
        CMS_Data cms_level = new CMS_Data();
        [HideInInspector] public bool IsTimerEnable;
        private GameObject[] char_clone = new GameObject[8];
        public Text timer_text;
        

        float CountDowntime = 240;  /// if cms not given timeperlevel then we use for ratting star calculate constant value 

       
        public Text Instruction_text;
        public GameObject KnowledgeFact;
        public Text health;

        private void Awake()
        {
            instance = this;
        }


        public void InstrucationPanal_Active()
        {
            CMSValue(0);
           // instructionspanel.SetActive(true);

        }

      

        // Start is called before the first frame update
        public void StartGame()
        {
            IsGameOVer = false;
           
            // DeflautBulletPos = bullet.transform.position;
            ShufflePlanetNameFunction(PlanetName, PlanetIcon);
            SetIcon();
            Initalization();
            // cms time
            CheckTimerPerLevel();

        }

        void Initalization()
        {


            for (int i = 0; i < TextPart_Object.childCount; i++)
            {
                TextPart_Object.GetChild(i).GetChild(0).gameObject.SetActive(false);
                
            }
            for (int i = 0; i < parent_char.childCount; i++)
            {
                parent_char.GetChild(i).gameObject.SetActive(false);
                parent_char.GetChild(i).GetComponentInChildren<DragScript_MS>().ResetPosition();
            }
            planet_images[SelectedPlanet].GetComponent<InventoryMovePlanet_MS>().movePlanet(MainPlanet.transform.position, true);


            //SelectPlanetForGame();
        }

        void SetIcon()
        {
            for (int i = 0; i < planet_images.Length; i++)
            {
                planet_images[i].sprite = random_PlanetIcon[i];
            }
        }

        public void SetRaycasting(string _script)
        {
            for (int i = 0; i < parent_char.childCount; i++)
            {
                if (_script == parent_char.GetChild(i).GetChild(0).name)

                    parent_char.GetChild(i).GetChild(0).GetComponent<Image>().raycastTarget = true;

                else
                    parent_char.GetChild(i).GetChild(0).GetComponent<Image>().raycastTarget = false;
            }
        }
        public void ReSetRaycasting()
        {
            for (int i = 0; i < parent_char.childCount; i++)
            {

                parent_char.GetChild(i).GetChild(0).GetComponent<Image>().raycastTarget = true;
            }
        }

        public void SelectPlanetForGameStart()
        {
            for (int i = 0; i < TextPart_Object.childCount; i++)
            {
                TextPart_Object.GetChild(i).GetChild(0).gameObject.SetActive(false);
               // parent_char.GetChild(i).GetComponentInChildren<DragScript>().ResetPosition();
            }

            MainPlanet.sprite = random_PlanetIcon[SelectedPlanet];
            char[] planetname = random_planetList[SelectedPlanet].ToCharArray();

            for (int i = 0; i < TextPart_Object.childCount; i++)
            {
                if (planetname.Length > i)
                {
                    TextPart_Object.GetChild(i).gameObject.SetActive(true);
                    TextPart_Object.GetChild(i).GetChild(0).GetComponent<Text>().text = planetname[i].ToString();
                }

                else
                {
                    TextPart_Object.GetChild(i).gameObject.SetActive(false);
                    parent_char.GetChild(i).gameObject.SetActive(false);
                }
            }


            //  ShuffleTargetPointforText();
            StartCoroutine(Text_Genrate(planetname));

        }


        IEnumerator Text_Genrate(char[] list)
        {

            List<char> _templist = new List<char>();
            List<char> _shuffle_name = new List<char>();
            char[] extraword = PlanetName[Random.Range(0, PlanetName.Count)].ToCharArray();
            for (int i = 0; i < list.Length; i++)
            {
                _templist.Add(list[i]);
            }

            for (int i = 0; i < list.Length; i++)
            {
                int rnd = Random.Range(0, _templist.Count);
                _shuffle_name.Add(_templist[rnd]);
                _templist.Remove(_templist[rnd]);
            }

            for (int i = 0; i < parent_char.childCount; i++)
            {
                parent_char.GetChild(i).gameObject.SetActive(false);

            }
            for (int i = 0; i < _shuffle_name.Count; i++)
            {

                parent_char.GetChild(i).transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text = _shuffle_name[i].ToString();
                parent_char.GetChild(i).gameObject.SetActive(true);

            }

            for (int i = 0; i < 2; i++)
            {
                parent_char.GetChild(_shuffle_name.Count + i).transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text = extraword[i].ToString();
                parent_char.GetChild(_shuffle_name.Count + i).gameObject.SetActive(true);
                //print("call call");
            }

            StartFirstAnimation();
            yield return new WaitForSeconds(2f);
            if (!parent_char.GetChild(1).GetChild(0).GetComponent<DragScript_MS>().Istouch)
                parent_char.GetChild(1).GetChild(0).GetComponent<Animator>().enabled = true;
            if (!parent_char.GetChild(3).GetChild(0).GetComponent<DragScript_MS>().Istouch)
                parent_char.GetChild(3).GetChild(0).GetComponent<Animator>().enabled = true;
            if (!parent_char.GetChild(5).GetChild(0).GetComponent<DragScript_MS>().Istouch)
                parent_char.GetChild(5).GetChild(0).GetComponent<Animator>().enabled = true;
            if (!parent_char.GetChild(8).GetChild(0).GetComponent<DragScript_MS>().Istouch)
                parent_char.GetChild(8).GetChild(0).GetComponent<Animator>().enabled = true;

        }

        void StartFirstAnimation()
        {
            if (!parent_char.GetChild(0).GetChild(0).GetComponent<DragScript_MS>().Istouch)
                parent_char.GetChild(0).GetChild(0).GetComponent<Animator>().enabled = true;
            if (!parent_char.GetChild(2).GetChild(0).GetComponent<DragScript_MS>().Istouch)
                parent_char.GetChild(2).GetChild(0).GetComponent<Animator>().enabled = true;
            if (!parent_char.GetChild(4).GetChild(0).GetComponent<DragScript_MS>().Istouch)
                parent_char.GetChild(4).GetChild(0).GetComponent<Animator>().enabled = true;
            if (!parent_char.GetChild(6).GetChild(0).GetComponent<DragScript_MS>().Istouch)
                parent_char.GetChild(6).GetChild(0).GetComponent<Animator>().enabled = true;
            if (!parent_char.GetChild(2).GetChild(0).GetComponent<DragScript_MS>().Istouch)
                parent_char.GetChild(7).GetChild(0).GetComponent<Animator>().enabled = true;
        }


        public void ShufflePlanetNameFunction(List<string> list, List<Sprite> _sprite)
        {
            List<string> _templist = new List<string>();
            List<Sprite> _tempsprite = new List<Sprite>();
            random_planetList.Clear();
            random_PlanetIcon.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                _templist.Add(list[i]);
                _tempsprite.Add(_sprite[i]);
            }

            for (int i = 0; i < list.Count; i++)
            {
                int rnd = Random.Range(0, _templist.Count);
                random_planetList.Add(_templist[rnd]);
                random_PlanetIcon.Add(_tempsprite[rnd]);
                _templist.Remove(_templist[rnd]);
                _tempsprite.Remove(_tempsprite[rnd]);
            }


        }



        public string NearByLocation(Transform current)
        {
            // print(current.position+"    "+ TextPart_Object.GetChild(0).transform.position);

            // print(Vector2.Distance(current.position, TextPart_Object.GetChild(0).transform.position));
            for (int i = 0; i < TextPart_Object.childCount; i++)
            {
                // print(Vector2.Distance(current.localPosition, TextPart_Object.GetChild(i).transform.localPosition));
                if (Vector2.Distance(current.position, TextPart_Object.GetChild(i).transform.position) < 0.5f)
                {
                    print("aaya");
                    SelectedText = TextPart_Object.GetChild(i).GetChild(0).gameObject;
                    return (TextPart_Object.GetChild(i).GetChild(0).GetComponent<Text>().text);

                }
            }
            return null;
        }

        bool IsGameOVer;

        public void NextPlanetInList()
        {
            if (IsGameOVer) return;

            RightMoveCount = 0;
            SelectedPlanet++;
            if (SelectedPlanet >= PlanetName.Count)
            {
                Right_WrongMsgShow();
                return;
            }
            else
            {
                Initalization();
            }
        }

        ////funcation overloaded for sprite shuffiling
        //void ShuffleTargetPointforText()
        //{
        //    List<GameObject> randompoint = new List<GameObject>();
        //    List<GameObject> _templist = new List<GameObject>();
        //    print(parent_char.GetChild(0).childCount);
        //    for (int i = 0; i < parent_char.GetChild(0).childCount; i++)
        //    {
        //        _templist.Add(parent_char.GetChild(0).GetChild(i).gameObject);

        //    }
        //    for (int i = 0; i < parent_char.GetChild(0).childCount; i++)
        //    {
        //        int rnd = Random.Range(0, _templist.Count);
        //        randompoint.Add(_templist[rnd]);
        //        _templist.Remove(_templist[rnd]);
        //    }
        //   // print( randompoint[i]);
        //    for (int i = 0; i < char_clone.Length; i++)
        //    {
        //        char_clone[i].GetComponent<TravelBubbleScritp>().TargetPos = randompoint[i].transform.position;
        //    }

        //}


        int RightMoveCount;
        public void AfterDragLetter(bool IsTrue)
        {


            if (IsTrue)
            {
                SelectedText.SetActive(true);
                RightMoveCount++;
                SoundManager_MS.instanace.rightmovePlay();
                if (RightMoveCount >= random_planetList[SelectedPlanet].Length)
                {
                    print("complete");

                    AfterCorrectNameEffect();




                }
                // print("Right Move");

                IsRightMove = true;
               
            }
            else
            {

                IsRightMove = false;
                SoundManager_MS.instanace.WrongMovePlay();
                Right_WrongMsgShow();
            }
            IsShoot = true;
        }


        void Right_WrongMsgShow()
        {
            print("wrong move");
            Text msgtext = MsgBox.GetComponentInChildren<Text>();
            if (IsRightMove)
            {
                msgtext.text = cms_level.RightMove_Msg;
                Coin++;
                CoinUpdate();
            }
            else
            {
                print(cms_level.MaxWrongMove);
                cms_level.MaxWrongMove--;
                updateHeath(cms_level.MaxWrongMove);
                // Life_ChangeUpdate();
                msgtext.text = cms_level.WrongMove_Msg;
            }

            CheckGameOverORGameWin();

            MsgBox.gameObject.SetActive(false);
            MsgBox.gameObject.SetActive(true);
            CancelInvoke("WaitforMsg");
            Invoke("WaitforMsg", 1f);
        }


        void AfterCorrectNameEffect()
        {
            planet_images[SelectedPlanet].GetComponent<InventoryMovePlanet_MS>().movePlanet(planet_images[SelectedPlanet].transform.parent.GetChild(3).transform.position, false);
            MainPlanet.sprite = Default_sprite;


        }



        void WaitforMsg()
        {
            MsgBox.gameObject.SetActive(false);
        }

        void CheckGameOverORGameWin()
        {
            if (SelectedPlanet == PlanetName.Count)
            {
                GameWin();
            }
            if (cms_level.MaxWrongMove < 0)
            {
                GameOver();
            }
        }


       
        //=====================================================================================

     
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
        public void GameWin()
        {

            TimerCountDown_MS.instance.StopTimer();
          // StarRating();
            UIManager_MS.instance.OnClick_popupsButton(0);
            UIManager_MS.instance.SetLevelLock();
            UIManager_MS.instance.LevelUnlockLock();
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
            UIManager_MS.instance.Coin_text.text = Coin.ToString();
        }
       


        //=========================================================//////////////////
        //Game Reset 
        public void Retry()
        {

            switch (UIManager_MS.instance.current_level)
            {
                case 0:
                    resetGame();
                    StartGame();
                    break;
                case 1:
                    Level2Manager_MS.instance.resetGame();
                    Level2Manager_MS.instance.StartGame();
                    break;
                case 2:
                    Level3Manager_MS.instance.GameReset();
                    Level3Manager_MS.instance.StartGame();
                    break;
                case 3:
                    Level4Manager_MS.instance.GameReset();
                    Level4Manager_MS.instance.StartGame();
                    break;
                case 4:
                    Level5Manager_MS.instance.GameReset();
                    Level5Manager_MS.instance.StartGame();
                    break;
            }

            UIManager_MS.instance.ActivatePopByIndex(-1);


        }

        public void ShowGoToMenu()
        {
            StartCoroutine(SetGoToMenu());
        }
        public IEnumerator SetGoToMenu()
        {
            UIManager_MS.instance.FadeInOut();
            yield return new WaitForSeconds(0.5f);
            GoToMenu();
        }



        public void GoToMenu()
        {
            switch (UIManager_MS.instance.current_level)
            {
                case 0:
                    resetGame();
                    StartGame();
                    break;
                case 1:
                    Level2Manager_MS.instance.resetGame();

                    break;
                case 2:
                    Level3Manager_MS.instance.GameReset();

                    break;
                case 3:
                    Level4Manager_MS.instance.GameReset();

                    break;
                case 4:
                    Level5Manager_MS.instance.GameReset();

                    break;
            }
            UIManager_MS.instance.ActivatePopByIndex(-1);
            UIManager_MS.instance.ActivatePanelByIndex(2);
            UIManager_MS.instance.Character.transform.GetComponent<Animator>().Play("idle");
            TimerCountDown_MS.instance.StopTimer();

        }

        public void resetGame()
        {

            SelectedPlanet = 0;
            RightMoveCount = 0;
            random_PlanetIcon.Clear();
            random_planetList.Clear();
            MainPlanet.sprite = Default_sprite;
            timer_text.text = "00:00";
            StopAllCoroutines();
            for (int i = 0; i < planet_images.Length; i++)
            {
                planet_images[i].transform.position = planet_images[i].transform.parent.GetChild(3).transform.position;
                planet_images[i].transform.GetComponent<Image>().enabled = false;
                planet_images[i].GetComponent<InventoryMovePlanet_MS>().stopPlanet();
            }
            for (int i = 0; i < parent_char.childCount; i++)
            {
                parent_char.GetChild(i).gameObject.SetActive(false);
                parent_char.GetChild(i).GetComponentInChildren<DragScript_MS>().ResetPosition();
            }
            InstrucationPanal_Active();
            ReSetRaycasting();
            IsGameOVer = true;
            UIManager_MS.instance.WiningPoupsound();
        }
        public void updateHeath(int text)
        {

            health.text = (text + 1).ToString();
        }
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


    }
}
