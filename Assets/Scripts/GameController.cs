using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;


public class GameController : MonoBehaviour
{
    public UIController UIController;
    public InputAction PauseAction;

    private enum eGameState
    {
        TitleScreen,
        Start,
        Idle,
        Resume,
        Pause,
        GameOver
    }

    private eGameState State;

    private void OnEnable()
    {
        PauseAction.Enable();
        PauseAction.started += TogglePause;
        EventManager.Instance.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        PauseAction.Disable();
        PauseAction.started -= TogglePause;
        EventManager.Instance.OnGameOver -= GameOver;
    }

    private void Start()
    {
        State = eGameState.TitleScreen;
    }
    private void Update()
    {
        switch (State)
        {
            case eGameState.TitleScreen:
                Time.timeScale = 0;
                UIController.DisablePanels();
                UIController.EnableTitlePanel();
                break;

            case eGameState.Start:
                UIController.DisablePanels();
                UIController.EnablePlayerHUD();
                UIController.ResetScore();
                Time.timeScale = 1;
                SetIdleState();
                break;

            case eGameState.Pause:
                Time.timeScale = 0;
                UIController.DisablePanels();
                UIController.EnablePausePanel();
                break;

            case eGameState.Resume:
                Time.timeScale = 1;
                UIController.DisablePanels();
                UIController.EnablePlayerHUD();
                SetIdleState();
                break;

            case eGameState.GameOver:
                Time.timeScale = 0;
                UIController.EnableGameOverPanel();
                break;

            case eGameState.Idle:
                break;
        }
    }



    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        State = eGameState.Start;
    }

    public void Resume()
    {
        State = eGameState.Resume;
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        if (State == eGameState.Idle)
        {
            State = eGameState.Pause;
        }
        else if (State == eGameState.Pause)
        {
            State = eGameState.Resume;
        }
    }

    public void QuitPlay()
    {
        State = eGameState.TitleScreen;
    }

    private void SetIdleState()
    {
        State = eGameState.Idle;
    }

    private void GameOver()
    {
        State = eGameState.GameOver;
    }
}
