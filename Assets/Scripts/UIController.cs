using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject TitlePanel;
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public GameObject PlayerHUD;
    public TMP_InputField InputField;
    public TMP_Text ScoreText;

    private int score;

    private void OnEnable()
    {
        EventManager.Instance.OnDropPop += UpdateScore;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDropPop -= UpdateScore;
    }
    public void DisablePanels()
    {
        TitlePanel.SetActive(false);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PlayerHUD.SetActive(false);
    }

    public void EnableTitlePanel()
    {
        DisablePanels();
        TitlePanel.SetActive(true);
    }

    public void EnablePausePanel()
    {
        DisablePanels();
        PausePanel.SetActive(true);
    }

    public void EnableWinPanel()
    {
        DisablePanels();
    }

    public void EnableGameOverPanel()
    {
        DisablePanels();
        GameOverPanel.SetActive(true);
    }

    public void EnablePlayerHUD()
    {
        DisablePanels();
        PlayerHUD.SetActive(true);
        EventSystem.current.SetSelectedGameObject(InputField.gameObject, null);
        InputField.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    public void UpdateScore()
    {
        score += 500;
        ScoreText.text = "Score: " + score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        ScoreText.text = "Score: " + score.ToString();
    }
}
