//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    internal event Action<PlayerPhysics.State> OnNewState = (state) => { };

    [SerializeField] private PlayerStateAudio playerStateAudio;
    [SerializeField] private Transform playerModel;

    private readonly Vector3 forward = Vector3.right;
    IPlayerPhysics playerPhysics;

    internal void Init(IPlayerPhysics playerPhysics)
    {
        if (playerPhysics != null)
        {
            playerPhysics.CurrentState.OnValueChanged -= OnPlayerStateChanged;
        }

        this.playerPhysics = playerPhysics;

        playerStateAudio.Init(playerPhysics);

        playerPhysics.CurrentState.OnValueChanged += OnPlayerStateChanged;

        playerPhysics.IsMovingForward.OnValueChanged += IsMovingForwardChanged;
        IsMovingForwardChanged(playerPhysics.IsMovingForward.Value);
    }

    private void IsMovingForwardChanged(bool isForward)
    {
        Vector3 forward = isForward ? this.forward : -this.forward;
        playerModel.rotation = Quaternion.LookRotation(forward, transform.up);
    }

    private void OnPlayerStateChanged(PlayerPhysics.State state)
    {
        OnNewState(state);
    }

    private void FixedUpdate()
    {
        this.transform.position = playerPhysics.PlayerPosition;
    }
}
