using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LatheTimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public PlayableAsset[] playableAssets;
    public int currentTimeline = 0;

    private LatheController latheController;

    void Start()
    {
        latheController = FindObjectOfType<LatheController>();
        if (latheController == null)
        {
            Debug.LogError("No latheController found in LatheTimelineController");
        }
        //Null check PlayableAssets
        if (playableAssets == null || playableAssets.Length == 0)
        {
            Debug.LogError("No Playable Assets found in LatheTimelineController");
        }
    }

    // Start the Timeline
    public void PlayTimeline()
    {
        if (playableDirector != null)
        {
            playableDirector.playableAsset = playableAssets[currentTimeline];
            playableDirector.Play();
        }
    }

    // Pause the Timeline
    public void PauseTimeline()
    {
        if (playableDirector != null)
        {
            playableDirector.Pause();
        }
    }

    // Resume the Timeline
    public void ResumeTimeline()
    {
        if (playableDirector != null)
        {
            playableDirector.Resume();
        }
    }

    // Stop the Timeline
    public void StopTimeline()
    {
        if (playableDirector != null)
        {
            playableDirector.Stop();
        }
    }

    // Check if the Timeline is playing
    public bool IsPlaying()
    {
        if (playableDirector != null)
        {
            return playableDirector.state == PlayState.Playing;
        }
        return false;
    }
}
