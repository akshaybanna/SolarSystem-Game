using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;

namespace MissionSpace
{
    public class UIManager_MS : MonoBehaviour
    {

        public static UIManager_MS instance;

        [Header("Panels Reference")]
        [SerializeField]
        private Panel[] panels;

        [Header("Level Reference")]
        [SerializeField]
       // private GameObject[] Levels;



        [Header("PopUps Reference")]
        public PopUp[] popsUps;



        // loading referances
        [Header("loading Reference")]
        public Image loading_bar;
        float loading_time = 0;

        // GamePlay TopBar referances
        [Header("Top bar in GamePlay Reference")]
        public GameObject Timer_toppanelGameplay;
        public GameObject Retry_life;
        public GameObject[] Star;
        public Text Coin_text;

        //level 2 Referances
        [Header("level 2 Referances ")]
        public Button[] PrepositionWord_Button;
        public bool IsBoyCharacter;
        public int current_level;

        public VideoClip[] GameVideo_boy;
        public VideoClip[] GameVideo_girl;
        public VideoPlayer Videoplayer;
        public VideoClip [] Videoplayer_clip;

        public GameObject GameWin_button;
        public float[] ScrollValue;
        public Transform scroll_object;

        public Animator Character;
      
        public GameObject WinningCharacter;
       //public Sprite[] character_sprite;
        public Text WinningText;

        public Animator FadeEffect;

        public AnimationClip[] animationClips;
        public Animator characterAnim;
      

        public GameObject FadeAnim;
        public Button SettingButton;

        public Color[] LockedColor;

        #region Global Message Popup

        public Text KnowlegdeFact_text;
        public Text HowToPlayText;
        public VideoPlayer HowToplayVedio;

        // cmsData
        public Animator MsgBox_levelMenu;
        public GameObject MsgButtonTap;
        public Text[] Level_name;

        public GameObject messagePopup;
        public Text message_text;

         public GameObject swipeAnimation;
        #endregion
        AssetBundleCreateRequest bundle;
        AssetBundle assetbundle;
       public IEnumerator LoadVideo()
        {
            string filename = "5_vid";

            string tempPath = (Application.persistentDataPath + "/AssetData");
            tempPath = Path.Combine(tempPath, filename + ".unity");

            if (File.Exists(tempPath))
            {

                bundle = AssetBundle.LoadFromFileAsync(tempPath);
                yield return bundle;

                assetbundle = bundle.assetBundle;

                Debug.Log(assetbundle == null ? "failed" : "Success");


                print("assetbundle.isStreamedSceneAssetBundle");
                string[] scenePaths = assetbundle.GetAllAssetNames();


                print(scenePaths[0]);
                //  var myLoadedAssetBundle = AssetBundle.LoadFromFile(tempPath);

                string[] videonames = assetbundle.GetAllAssetNames();


                GameVideo_boy[0] = assetbundle.LoadAsset<VideoClip>(videonames[0]);
                GameVideo_boy[1] = assetbundle.LoadAsset<VideoClip>(videonames[1]);
                GameVideo_boy[2] = assetbundle.LoadAsset<VideoClip>(videonames[2]);
                GameVideo_boy[3] = assetbundle.LoadAsset<VideoClip>(videonames[3]);
                GameVideo_girl[0] = assetbundle.LoadAsset<VideoClip>(videonames[4]);
                GameVideo_girl[1] = assetbundle.LoadAsset<VideoClip>(videonames[5]);
                GameVideo_girl[2] = assetbundle.LoadAsset<VideoClip>(videonames[6]);
                GameVideo_girl[3] = assetbundle.LoadAsset<VideoClip>(videonames[7]);
                Videoplayer_clip[0] = assetbundle.LoadAsset<VideoClip>(videonames[8]);
                Videoplayer_clip[1] = assetbundle.LoadAsset<VideoClip>(videonames[9]);
                Videoplayer_clip[2] = assetbundle.LoadAsset<VideoClip>(videonames[10]);
                Videoplayer_clip[3] = assetbundle.LoadAsset<VideoClip>(videonames[11]);
                Videoplayer_clip[4] = assetbundle.LoadAsset<VideoClip>(videonames[12]);
            }
        }
        void SetDataLevelScreen()
        {

            for (int i = 0; i < Level_name.Length; i++)
            {
                Level_name[i].text = APIHandler_MS.instance._CMSDabase[i].LevelName;
            }

            if (APIHandler_MS.instance.home_data.Home_Screen_Text.Count >= 1)
            {
                ChangeText();
                MsgButtonTap.SetActive(true);
                //StartCoroutine("ChangeText");
            }
            else
            {
                MsgBox_levelMenu.gameObject.SetActive(false);
                MsgButtonTap.SetActive(false);
            }
        }
        int index;
        public void ChangeText()
        {

            print(index);
            if (index < APIHandler_MS.instance.home_data.Home_Screen_Text.Count)
            {
                MsgBox_levelMenu.gameObject.SetActive(false);
                MsgBox_levelMenu.transform.GetComponentInChildren<Text>().text = APIHandler_MS.instance.home_data.Home_Screen_Text[index];

                MsgBox_levelMenu.gameObject.SetActive(true);
                index++;
            }
            else
            {
                MsgBox_levelMenu.gameObject.SetActive(false);
                MsgButtonTap.SetActive(false);
            }


        }


       public void SetLevelLock()
        {
            int level = current_level + 1;
            if (PlayerPrefs.GetString("LevelLock").Contains(level.ToString())==false)
            {
                PlayerPrefs.SetString("LevelLock", PlayerPrefs.GetString("LevelLock")+(level + ","));
            }

        }

      public  void LevelUnlockLock()
        {
          string text =  PlayerPrefs.GetString("LevelLock");
            print(text);
            for (int i = 1; i < Level_name.Length; i++)
            {
                if (text.Contains(i.ToString()))
                {
                    print(i);
                    Level_name[i].transform.parent.GetComponent<Button>().interactable = true;
                    Level_name[i].transform.parent.GetComponent<Image>().color = LockedColor[1]; 
                    Level_name[i].transform.parent.GetChild(0).GetComponent<Text>().color = LockedColor[1];
                    Level_name[i].transform.parent.GetChild(1).GetComponent<Text>().color = LockedColor[1];
                    Level_name[i].transform.parent.GetChild(2).GetComponent<Image>().color = LockedColor[1];
                    Level_name[i].transform.parent.GetChild(3).gameObject.SetActive(false);
                }
                else
                {
                    Level_name[i].transform.parent.GetComponent<Button>().interactable = false;
                    Level_name[i].transform.parent.GetComponent<Image>().color = LockedColor[0];
                    Level_name[i].transform.parent.GetChild(0).GetComponent<Text>().color = LockedColor[0];
                    Level_name[i].transform.parent.GetChild(1).GetComponent<Text>().color = LockedColor[0];
                    Level_name[i].transform.parent.GetChild(2).GetComponent<Image>().color = LockedColor[0];
                    Level_name[i].transform.parent.GetChild(3).gameObject.SetActive(true);
                }
            }
        }

        //void LevelLock(bool istrue,GameObject g)
        //{
        //    if (istrue)
        //    {
        //        for (int i = 0; i < g.transform.childCount; i++)
        //        {
        //            g.transform.GetChild(i). = true;
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < g.transform.childCount; i++)
        //        {
        //            g.transform.GetChild(i).GetComponent<Button>().interactable = true;
        //        }
        //    }
        //}
        public void Firsttimemsg()
        {
            swipeAnimation.SetActive(false);
            PlayerPrefs.SetString("first", "done");
        }
        private void Awake()
        {
            instance = this;
            Input.multiTouchEnabled = false;
        }
        private void Start()
        {
            PlayerPrefs.DeleteKey("first");
            if (!PlayerPrefs.HasKey("first"))
                swipeAnimation.SetActive(true);
            else
                swipeAnimation.SetActive(false);


            // PlayerPrefs.DeleteKey("LevelLock");
            LevelUnlockLock();
           // ActivatePanelByIndex(0); // first call loading panel 
            StartCoroutine(WaitForLoadingTime());
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                exit_POPUP();
            }
        }

        public void exit_POPUP()
        {
            popsUps[9].popUpPanel.SetActive(true);
            Pause();
        }

        public void Yes_exit()
        {
           // Application.Quit();
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        // first start loading panal and get data from api 
        IEnumerator WaitForLoadingTime()
        {

            while (loading_time<1)
            {
               // print(loading_time);
                yield return new WaitForSeconds(.001f);
                loading_time += .01f ;
                loading_bar.fillAmount = loading_time;
            }
            yield return new WaitForSeconds(1f);
            if (APIHandler_MS.instance.IsStatusTrue)
            {
                OnClickButton(2);
                SetDataLevelScreen();
            }
            else
            {
                StartCoroutine(WaitForLoadingTime());
            }

        }

       public  void ActivatePanelByIndex(int index)
        {
            foreach (Panel panel in panels)
            {
                if (index == panel._panelId)
                {
                    panel.panel.SetActive(true);
                }
                else
                {
                    panel.panel.SetActive(false);
                }
            }
        }

      public   void ActivatePopByIndex(int index)
        {
            foreach(PopUp popUp in popsUps)
            {
               // Debug.Log(index);
                if(index == popUp._popUpId)
                {
                    popUp.popUpPanel.SetActive(true);
                }
                else
                {
                    popUp.popUpPanel.SetActive(false);
                }
            }
        }


        public void OnClickButton(int id)
        {
            ActivatePanelByIndex(id);
        }

        public void OnClick_popupsButton(int id)
        {
            ActivatePopByIndex(id);
        }


        public void SetknowledgeFactTextAccordingTolevel()
        {
            KnowlegdeFact_text.text = APIHandler_MS.instance._CMSDabase[current_level].Knowledge_Fact;
        }
        public void SetHowToPlayText()
        {

            RenderTexture.active = HowToplayVedio.targetTexture;
            GL.Clear(true, true, Color.black);
            RenderTexture.active = null;
           
           
            HowToplayVedio.clip = Videoplayer_clip[current_level];
            HowToPlayText.text = APIHandler_MS.instance._CMSDabase[current_level].How_To_Play;
            HowToplayVedio.Prepare();
            HowToplayVedio.waitForFirstFrame = true;
        }

        public void ResetStart()
        {
            for (int i = 0; i < 3; i++)
            {
                Star[i].SetActive(false);
            }
        }
        public void WinStarActive(int num)
        {
            StarReset();
            StartCoroutine(startEnum(num));
        }

        IEnumerator startEnum(int num)
        {
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < num; i++)
            {

                Star[i].SetActive(true);

                yield return new WaitForSeconds(0.5f);
                // popsUps[0].popUpPanel.GetComponentInChildren<Animator>().Play("winpanelShaker", -1, 0f);
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void StarReset()
        {
            for (int i = 0; i < Star.Length; i++)
            {
                Star[i].SetActive(false);
            }

        }



        IEnumerator waitforanimation(float value)
        {
            Character.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Isturn");
            yield return new WaitForSeconds(.2f);
            characterAnim.Play(animationClips[current_level].name);
            yield return new WaitForSeconds(value);
            //Character.enabled = false;
            Character.transform.GetChild(0).GetComponent<Animator>().Play("retun");
            yield return new WaitForSeconds(.3f);
            
            //isonlyonce[current_level] = true;
            yield return new WaitForSeconds(.4f);
            for (int i = 0; i < isonlyonce.Length; i++)
            {
                if (current_level == i)
                {
                    isonlyonce[current_level] = true;
                   
                }
                else
                {
                    isonlyonce[i] = false;
                }


            }
            StartCoroutine( SetLevelByIndex());


        }
        bool isClick = false;
        // level menu click
        public bool[] isonlyonce = new bool[5];
        public  void SetCurrentLevel(int i )
        {
            SettingButton.interactable = false;
            MsgBox_levelMenu.gameObject.SetActive(false);
            MsgButtonTap.SetActive(false);
            if (isClick) return;
            isClick = true;
            current_level = i;
            print("current level" + current_level);
            switch (i)
            {
                case 0:
                    if (!isonlyonce[current_level])
                    {

                        StartCoroutine(waitforanimation(0.5f));
                    }
                    else
                    {
                        StartCoroutine(SetLevelByIndex());
                    }
                   



                    break;
                case 1:

                    if (!isonlyonce[current_level])
                    {
                        
                       
                        StartCoroutine(waitforanimation(0.8f));
                    }
                    else
                        StartCoroutine(SetLevelByIndex());


                    break;
                case 2:
                    if (!isonlyonce[current_level])
                    {
                       
                        StartCoroutine(waitforanimation(0.5f));
                    }
                    else
                        StartCoroutine(SetLevelByIndex());



                    break;
                case 3:
                    if (!isonlyonce[current_level])
                    {
                     
                        StartCoroutine(waitforanimation(0.5f));
                    }
                    else
                        StartCoroutine(SetLevelByIndex());



                    break;
                case 4:

                    if (!isonlyonce[current_level])
                    {
                      
                        StartCoroutine(waitforanimation(0.5f));
                    }
                    else
                        StartCoroutine(SetLevelByIndex());


                    break;

            }

            UIManager_MS.instance.WinningCharacter.SetActive(false);
            Level5Manager_MS.instance.planets.gameObject.SetActive(false);
            UIManager_MS.instance.WinningText.text = "CONGRATULATION";
            popsUps[0].popUpPanel.GetComponent<AudioSource>().clip=(SoundManager_MS.instanace.winingPopupSound);



        }

        IEnumerator SetLevelByIndex()
        {
            FadeInOut();
            yield return new WaitForSeconds(0.5f);
            SettingButton.interactable = true;
            levelActive(current_level);
        }
        void levelActive(int i )
        {
            switch (i)
            {
                case 0:


                  ActivatePopByIndex(4);
                    Level1Manager_MS.instance.resetGame();
                    ActivatePanelByIndex(3);

                    break;
                case 1:

                    ActivatePopByIndex(5);
                    Level2Manager_MS.instance.resetGame();
                    ActivatePanelByIndex(4);

                    break;
                case 2:

                    ActivatePopByIndex(6);
                    Level3Manager_MS.instance.GameReset();
                    ActivatePanelByIndex(5);

                    break;
                case 3:

                    ActivatePopByIndex(-1);
                    Level4Manager_MS.instance.GameReset();
                    ActivatePanelByIndex(6);
                    break;
                case 4:

                    ActivatePopByIndex(8);
                    Level5Manager_MS.instance.GameReset();
                    ActivatePanelByIndex(7);

                    break;

            }
            isClick = false;
        }
        public void FadeInOut()
        {
            FadeAnim.GetComponent<Animator>().Play("Fade", -1, 0f);
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }
        public void play_button()
        {
            Time.timeScale = 1;
        }

        public void skipButton()
        {
            OnClickButton(3);
           
        }




        public void OnNextLevel()
        {
            current_level++;
            levelActive(current_level);
            SetScrollViewOnLevel();
            //Level1Manager.instance.GoToMenu();
            onClick_VideoPlay();
           



        }

        //public void onVideoPlay()
        //{
        //    Videoplayer.clip = GameVideo[current_level - 1];
        //    popsUps[3].popUpPanel.SetActive(true);

        //}
        public void onClick_VideoPlay()
        {
            print(current_level);
            if(IsBoyCharacter)
            Videoplayer.clip = GameVideo_boy[current_level -1];
            else
             Videoplayer.clip = GameVideo_girl[current_level -1];
           
            popsUps[3].popUpPanel.SetActive(true);

        }

        void SetScrollViewOnLevel()
        {
          
            {
                scroll_object.transform.localPosition = new Vector2(ScrollValue[current_level], scroll_object.transform.position.y);
            }
        }

        public void WiningPoupsound()
        {
            if (current_level == 4)
            {
                popsUps[0].popUpPanel.GetComponent<AudioSource>().clip = SoundManager_MS.instanace.GamewinSound;
            }
            else
            {
                popsUps[0].popUpPanel.GetComponent<AudioSource>().clip = SoundManager_MS.instanace.winingPopupSound;
            }
        }

    }


    [Serializable]
    public class Panel
    {
        public string _panelName;
        public int _panelId;
        public GameObject panel;
    }

    [Serializable]
    public class PopUp
    {
        public string _popUpName;
        public int _popUpId;
        public GameObject popUpPanel;
    }





   

 
}



    