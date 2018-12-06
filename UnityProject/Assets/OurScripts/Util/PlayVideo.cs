using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
/// <summary>
/// Permet de lancer une video qui remplace un RawImage des qu'elle est chargee.
/// </summary>
public class PlayVideo : MonoBehaviour {
    public RawImage image;
    public VideoClip videoToPlay;
    public int speed;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = true;
        videoPlayer.isLooping = true;
        videoPlayer.playbackSpeed = speed;
        videoPlayer.source = VideoSource.VideoClip; 
        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();
        //Wait until video is prepared
        WaitForSeconds waitTime = new WaitForSeconds(1f);
        while (!videoPlayer.isPrepared)
        {          
            yield return waitTime;
            
            break;
        }
        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;
        //Play Video
        videoPlayer.Play();

       
    }
}
