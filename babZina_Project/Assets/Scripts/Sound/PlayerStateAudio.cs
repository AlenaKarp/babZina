//this empty line for UTF-8 BOM header
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAudio : MonoBehaviour
{
    [Serializable]
    public struct StateWithAudio
    {
        public AudioClip audioClip;
        public PlayerPhysics.State state;
        public bool loop;
    }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<StateWithAudio> statesWithAudio;

    private IPlayerPhysics playerPhysics;

    internal void Init(IPlayerPhysics playerPhysics)
    {
        if (playerPhysics != null)
        {
            playerPhysics.CurrentState.OnValueChanged -= OnPlayerStateChanged;
        }

        this.playerPhysics = playerPhysics;
        playerPhysics.CurrentState.OnValueChanged += OnPlayerStateChanged;
    }

    private void OnPlayerStateChanged(PlayerPhysics.State state)
    {
        foreach (StateWithAudio stateWithAudio in statesWithAudio)
        {
            if (stateWithAudio.state == state)
            {
                audioSource.clip = stateWithAudio.audioClip;
                audioSource.loop = stateWithAudio.loop;
                audioSource.Play();
                return;
            }
        }

        audioSource.Stop();
        audioSource.clip = null;
        audioSource.loop = false;
    }
}

