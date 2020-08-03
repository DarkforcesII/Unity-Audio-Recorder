using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrostweepGames.Plugins.Native;

public class MyCustomMic : MonoBehaviour
{
    private AudioClip _workingClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        RequestPermission();
        audioSource = GetComponent<AudioSource>();
        StartRecord();
    }

    private void RequestPermission()
    {
        CustomMicrophone.RequestMicrophonePermission();
    }

    private void StartRecord()
    {
        if (!CustomMicrophone.HasConnectedMicrophoneDevices())
            return;

        //_workingClip = CustomMicrophone.Start(CustomMicrophone.devices[0], true, 4, 44100);
        _workingClip = CustomMicrophone.Start(CustomMicrophone.devices[0], true, 4, 44100);
        audioSource.clip = _workingClip;
        audioSource.Play();
        //audioSource.Stop();
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
