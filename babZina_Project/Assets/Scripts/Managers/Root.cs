//this empty line for UTF-8 BOM header
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private ControlsManager controlsManager;
    [SerializeField] private PlayerPhysics playerPhysics;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AngryScaleManager angryScaleManager;
    [SerializeField] private Scenario scenario;
    [SerializeField] private SaveManager saveManager;

    private void Awake()
    {
        controlsManager.Init(cameraManager, uiManager);
        playerPhysics.Init(controlsManager, angryScaleManager);
        cameraManager.Init(playerPhysics);
        playerView.Init(playerPhysics);
        scenario.Init(angryScaleManager, saveManager);
        uiManager.Init(angryScaleManager, scenario, saveManager);
        audioManager.Init();
    }
}
