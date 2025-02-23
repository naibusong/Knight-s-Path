using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioDefination : MonoBehaviour
{
    public AudioClip audioClip;
    public PlayAudioEventSO playAudioEvent;
    public bool playOnEnable;

    private void OnEnable()
    {
        if (playOnEnable)
            PlayAudioClip();
    }
    public void PlayAudioClip()
    {
        playAudioEvent.RaiseEvent(audioClip);
    }
}
