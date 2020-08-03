using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField] private Transform[] audioSpectrumObjects;
    //public float heightMultiplier;
    public int numberOfSamples = 1024;
    [Range(1, 100)] public float heightMultiplier;
    public FFTWindow fftWindow;
    public float lerpTime = 0.5f;
    private 

    void Start()
    {
        heightMultiplier = PlayerPrefsManager.GetSensitivity();
    }

    void Update()
    {
        // Initialize float array
        float[] spectrum = new float[numberOfSamples];

        // Populate array with frequency spectrum data
        // 0 means listening to all channels
        GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, fftWindow);

        // loop over audioSpectrumObjects and modify according to frequency and spectrum data
        // this loop matches the array element to an object on a one-to-one basis
        for (int i = 0; i < audioSpectrumObjects.Length; i++)
        {
            // apply height multiplyer to intensity 
            float intensity = spectrum[i] * heightMultiplier;

            // calculate object's scale
            float lerpY = Mathf.Lerp(audioSpectrumObjects[i].localScale.y, intensity, lerpTime);
            Vector3 newScale = new Vector3(audioSpectrumObjects[i].localScale.x, lerpY, audioSpectrumObjects[i].localScale.z);

            // apply new scale to object
            audioSpectrumObjects[i].localScale = newScale;
        }
    }
}
