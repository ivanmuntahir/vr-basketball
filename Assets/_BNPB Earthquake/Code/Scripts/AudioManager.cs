using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NPC
{
    public string npcName;
    public AudioSource npcAudioSource;
    public AudioClip npcAudioClip;
}

[Serializable]
public class SFX
{
    public string sfxName;
    public AudioSource sfxAudioSource;
    public AudioClip sfxAudioClip;
}

public class AudioManager : MonoBehaviour
{
    [Header("NPC Audio")]
    public List<NPC> npcs;

    [Header("Background Music")]
    public AudioSource bgmAudioSource;
    public AudioClip bgmClip;

    [Header("Sound Effects")]
    public List<SFX> sfxList;

    // NPC Audio Management
    public void PlayAllNPCsAudio()
    {
        foreach (var npc in npcs)
        {
            PlayNPCAudio(npc);
        }
    }

    public void StopAllNPCsAudio()
    {
        foreach (var npc in npcs)
        {
            StopNPCAudio(npc);
        }
    }

    public void PlayNPCAudioByIndex(int index)
    {
        if (index >= 0 && index < npcs.Count)
        {
            PlayNPCAudio(npcs[index]);
        }
    }

    public void StopNPCAudioByIndex(int index)
    {
        if (index >= 0 && index < npcs.Count)
        {
            StopNPCAudio(npcs[index]);
        }
    }

    private void PlayNPCAudio(NPC npc)
    {
        if (npc.npcAudioSource != null && npc.npcAudioClip != null)
        {
            npc.npcAudioSource.clip = npc.npcAudioClip;
            npc.npcAudioSource.Play();
        }
    }

    private void StopNPCAudio(NPC npc)
    {
        if (npc.npcAudioSource != null)
        {
            npc.npcAudioSource.Stop();
        }
    }

    // Background Music Management
    public void PlayBGM()
    {
        if (bgmAudioSource != null && bgmClip != null)
        {
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.Play();
        }
    }

    public void StopBGM()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }
    }

    // SFX Management
    public void PlayAllSFX()
    {
        foreach (var sfx in sfxList)
        {
            PlaySFX(sfx);
        }
    }

    public void StopAllSFX()
    {
        foreach (var sfx in sfxList)
        {
            StopSFX(sfx);
        }
    }

    public void PlaySFXByIndex(int index)
    {
        if (index >= 0 && index < sfxList.Count)
        {
            PlaySFX(sfxList[index]);
        }
    }

    public void StopSFXByIndex(int index)
    {
        if (index >= 0 && index < sfxList.Count)
        {
            StopSFX(sfxList[index]);
        }
    }

    private void PlaySFX(SFX sfx)
    {
        if (sfx.sfxAudioSource != null && sfx.sfxAudioClip != null)
        {
            sfx.sfxAudioSource.clip = sfx.sfxAudioClip;
            sfx.sfxAudioSource.Play();
        }
    }

    private void StopSFX(SFX sfx)
    {
        if (sfx.sfxAudioSource != null)
        {
            sfx.sfxAudioSource.Stop();
        }
    }
}
