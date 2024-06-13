using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MissionSpace
{
    public class TimerCountDown_MS : MonoBehaviour
    {
        public static TimerCountDown_MS instance;
        public float timeLeft = 300.0f;
        public bool stop = true;

        private float minutes;
        private float seconds;

        public Text text;
        bool IsRattigCalculation = false;
        private void Awake()
        {
            instance = this;
        }
        public void startTimer(float from,Text time)
        {
            stop = false;
            timeLeft = from;
            text = time;
            IsRattigCalculation = false;
            //Update();
            //StartCoroutine(updateCoroutine());
        }
        public void startTimer(float from, bool _IsRattigCalculation)
        {
            stop = false;
            timeLeft = from;
            IsRattigCalculation = _IsRattigCalculation;
            //Update();
            //StartCoroutine(updateCoroutine());
        }
        void Update()
        {
            if (stop) return;
            timeLeft -= Time.deltaTime;

            minutes = Mathf.Floor(timeLeft / 60);
            seconds = timeLeft % 60;

            if (seconds > 59) seconds = 59;

            if (minutes < 0)
            {
                stop = true;
                minutes = 0;
                seconds = 0;
                TimeEnd();
            }
            text.text = "0"+string.Format("{00}:{1:00}", minutes, seconds);
            //        fraction = (timeLeft * 100) % 100;
        }

        public void StopTimer()
        {
            stop = true;

        }
        void TimeEnd()
        {

            if (IsRattigCalculation) return;
            if (0 == UIManager_MS.instance.current_level)
            {
                Level1Manager_MS.instance.Timerup();
            }
            else 
            if (1 == UIManager_MS.instance.current_level)
            {
                Level2Manager_MS.instance.Timerup();
            }
            else if (2 == UIManager_MS.instance.current_level)
            {
                Level3Manager_MS.instance.Timerup();
            }
            else if (3 == UIManager_MS.instance.current_level)
            {
                Level4Manager_MS.instance.Timerup();
            }
            else if (4 == UIManager_MS.instance.current_level)
            {
                Level5Manager_MS.instance.Timerup();
            }
        }


    }
}