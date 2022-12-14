using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class detect_video_end : MonoBehaviour
{

    private VideoPlayer videoPlayer;
    private RawImage rawImage;
    private bool played = false;
    [SerializeField]

    void Start() {
        videoPlayer = this.GetComponent<VideoPlayer>();
        videoPlayer.Pause();
    }

    // Update is called once per frame
    void Update() {
        if(played == false) {
            if(videoPlayer.isPrepared)
            {
                videoPlayer.Play();
                played = true;
            }
        } else {
            if(videoPlayer.isPlaying == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
