using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject[] MenuList;
    [SerializeField] private Text MessageBox;
    [SerializeField] private GameObject currMenu;
    [SerializeField] private Slider PlayerHealthBar;
    [SerializeField] private Slider TowerHealthBar;
    [SerializeField] private Text ScoreDisplay;
    private int currMenuNo;

    private void Start()
    {
        LoadMenu(Menus.MainMenu);
        MessageBox.gameObject.SetActive(false);
        string highScoreText = "HighScore : " + PlayerPrefs.GetInt("HighScore");
        StartCoroutine(DisplayMessage(highScoreText));
    }

    private void Update()
    {
        if (GameManager.Instance.Playing)
        {
            int playerHealth = PlayerController.Instance.getPlayerHealth();
            int towerHealth = PlayerController.Instance.getTowerHealth();

            PlayerHealthBar.value = playerHealth;
            TowerHealthBar.value = towerHealth;
            ScoreDisplay.text = "Score: " + GameManager.Instance.Score;
        }
    }

    public void openLevelMenu()
    {
        LoadMenu(Menus.LevelMenu);
    }

    public void LoadLevel(int levelNo)
    {

        if(GameManager.Instance.GetLevelStatus(levelNo - 1) != LevelStatus.Level_Locked)
        {
            GameManager.Instance.LoadLevel(levelNo - 1);
            currMenu.SetActive(false);
        }
        else
        {
            string msg = " Level Locked!";
            StartCoroutine(DisplayMessage(msg));
        }
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
    }

    public void openLevelCompleteMenu()
    {
        currMenu.SetActive(true);
        LoadMenu(Menus.CompletedMenu);
    }

    public void openLevelFailedMenu()
    {
        currMenu.SetActive(true);
        LoadMenu(Menus.FailMenu);
    }

    public void PlayButton()
    {
        int currLevel = GameManager.Instance.GetLevelNo();
        currMenu.SetActive(false);
        GameManager.Instance.LoadLevel(currLevel);
        SoundManager.Instance.Play(SoundEvents.ButtonClick);

    }

    public void QuitButton()
    {
        Application.Quit();
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
    }

    public void RetryButton()
    {
        currMenu.SetActive(false);
        GameManager.Instance.ReloadLevel();
    }

    public void ExitButton()
    {
        currMenu.SetActive(true);
        LoadMenu(Menus.MainMenu);
    }

    public void PauseButton()
    {
        currMenu.SetActive(true);
        LoadMenu(Menus.PauseMenu);
    }

    IEnumerator DisplayMessage(string message)
    {
        MessageBox.gameObject.SetActive(true);
        MessageBox.text = message;
        yield return new WaitForSeconds(5f);
        MessageBox.gameObject.SetActive(false);
    }

    private void LoadMenu(Menus menus)
    {
        currMenu.SetActive(false);
        currMenuNo = (int)menus;
        currMenu = MenuList[currMenuNo];
        currMenu.SetActive(true);
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
    }
}

public enum Menus
{
    MainMenu,
    LevelMenu,
    PauseMenu,
    CompletedMenu,
    FailMenu,
};
