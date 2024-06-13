using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MissionSpace
{
    public class SoundManager_MS : MonoBehaviour
    {
        public static SoundManager_MS instanace;
        public AudioClip wrongMove_audioclip;
        public AudioClip rightmove_audioclip;
        public AudioClip buttonclick_audioclip;
        public AudioClip pennyCollect_audioclip;
        public AudioClip GamewinSound;
        public AudioClip winingPopupSound;
        AudioSource _audio;
        public AudioSource[] _audiosorce;
        public AudioSource _bgsource;
        public Image music_button;
        public Image sound_button;
        public Sprite[] onoff;


        public void Awake()
        {
            instanace = this;
            GetSoundSatus();
        }
        // Start is called before the first frame update
        void Start()
        {
            _audio = GetComponent<AudioSource>();
        }



        void GetSoundSatus()
        {
            if (PlayerPrefs.HasKey("GameSound"))
            {
                print("call");
                if (0 == PlayerPrefs.GetInt("GameSound"))
                {

                    foreach (var item in _audiosorce)
                    {
                        item.enabled = false;
                    }


                    sound_button.sprite = onoff[0];
                }
                else
                {
                    foreach (var item in _audiosorce)
                    {
                        item.enabled = true;
                    }

                    sound_button.sprite = onoff[1];
                }
            }

            if (PlayerPrefs.HasKey("GameMusic"))
            {
                print(PlayerPrefs.GetInt("GameMusic"));
                if (0 == PlayerPrefs.GetInt("GameMusic"))
                {
                    _bgsource.enabled = false;
                    music_button.sprite = onoff[0];

                }
                else
                {

                    _bgsource.enabled = true;
                    music_button.sprite = onoff[1];

                }

            }

        }

        public void SetSound(bool issound)
        {
            if (issound)
            {
                if (sound_button.sprite.name == "On")
                {
                    PlayerPrefs.SetInt("GameSound", 0);

                }
                else
                {

                    PlayerPrefs.SetInt("GameSound", 1);
                }

            }
            else
            {
                if (music_button.sprite.name == "On")
                {

                    PlayerPrefs.SetInt("GameMusic", 0);
                }
                else
                {

                    PlayerPrefs.SetInt("GameMusic", 1);
                }
            }

            GetSoundSatus();
        }

        public void buttonclickSound()
        {

            _audio.PlayOneShot(buttonclick_audioclip);
        }
        public void rightmovePlay()
        {

            _audio.PlayOneShot(rightmove_audioclip);
        }
        public void WrongMovePlay()
        {

            _audio.PlayOneShot(wrongMove_audioclip);
        }
        public void pennyCollectPlay()
        {

            _audio.PlayOneShot(pennyCollect_audioclip);
        }
    }
}