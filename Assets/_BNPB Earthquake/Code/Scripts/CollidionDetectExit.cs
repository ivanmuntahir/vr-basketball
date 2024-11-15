using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidionDetectExit : MonoBehaviour
{
    [SerializeField] string nameTag;
    [SerializeField] GameObject[] objectToSpawn;
    [SerializeField] GameObject[] objectToDestroy;
    [SerializeField] AudioClip[] soundToStop;
    [SerializeField] bool looping;
    private AudioSource audioSource;
    [SerializeField] Material[] material;
    [SerializeField] GameObject assignedAudioObject;

    void Start()
    {
        // Menambahkan komponen AudioSource ke objek ini jika belum ada
        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        //Check NameTag
        if (other.gameObject.tag == nameTag)
        {
            PlayAssignedAudio();
            // Destroy Objects
            for (int i = 0; i < objectToDestroy.Length; i++)
            {
                objectToDestroy[i].SetActive(false);
            }

            // Spawn Object
            for (int i = 0; i < objectToSpawn.Length; i++)
            {
                objectToSpawn[i].SetActive(true);
            }


            // Stop Sound
            for (int i = 0; i < soundToStop.Length; i++)
            {
                if (soundToStop[i] != null)
                {
                    if (looping == true)
                    {
                        audioSource.clip = soundToStop[i];
                        audioSource.loop = true;
                        audioSource.Stop();
                    }
                    else
                    {
                        audioSource.clip = soundToStop[i];
                        audioSource.Stop();
                    }
                }
            }

            Renderer renderer = GetComponent<Renderer>();
            //Change Material
            for (int i = 0; i < material.Length; i++)
            {
                if (renderer != null && material[i] != null)
                {
                    renderer.material = material[i];
                }
            }
        }
    }
    private void PlayAssignedAudio()
    {
        if (assignedAudioObject != null)
        {
            AudioSource assignedAudioSource = assignedAudioObject.GetComponent<AudioSource>();
            if (assignedAudioSource != null)
            {
                assignedAudioSource.loop = looping;

                if (assignedAudioSource.isPlaying)
                {
                    assignedAudioSource.Stop();
                }

                assignedAudioSource.Play();
            }
            else
            {
                Debug.LogWarning("Assigned GameObject does not have an AudioSource component.");
            }
        }
    }

}
