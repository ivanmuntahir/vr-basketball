using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class VolumeHandler : MonoBehaviour
{
    public AudioSource audioSource;

    public void OnVolumeChanged(Slider slider)
    {
        if(slider != null)
        {
            audioSource.volume = slider.value;
        }
        
    }

}

