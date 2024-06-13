using MissionSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace MissionSpace
{
    public class VideoHandler_MS : MonoBehaviour
    {
        VideoPlayer _videoPlayer;
        int totalframe;
        int currentframe;
        //public GameObject loading;
        // Start is called before the first frame update
        void OnEnable()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            //_videoPlayer.clip = UIManager_Preposition.instance.GameVideo[UIManager_Preposition.instance.current_level-1];
            _videoPlayer.Play();
            _videoPlayer.loopPointReached += OnMovieFinished;

        }





        public void SkipVideo()
        {


            _videoPlayer.Stop();
            transform.parent.gameObject.SetActive(false);
            RenderTexture.active = _videoPlayer.targetTexture;
            GL.Clear(true, true, Color.black);
            RenderTexture.active = null;
        }


        void OnMovieFinished(VideoPlayer player)
        {

            SkipVideo();
            //Debug.Log("Event for movie end called");
        }

    }
}
