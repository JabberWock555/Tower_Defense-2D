using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UiManager : MonoBehaviour
{
    private static UiManager instance;
    public static UiManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private GameObject[] MenuList;
    [SerializeField] private Text MessageBox;
    private int currMenu;

    private void Start()
    {
        currMenu = (int)Menus.MainMenu;
        MenuList[currMenu].SetActive(true);
        MessageBox.gameObject.SetActive(false);
    }

    public void openLevelMenu()
    {
        LoadMenu(Menus.LevelMenu);
    }

    public void LoadLevel(int levelNo)
    {
        if(GameManager.Instance.GetLevelStatus(levelNo - 1) != LevelStatus.Level_Locked)
        {
            SceneManager.LoadScene(1);
            GameManager.Instance.LoadLevel(levelNo - 1);
        }
        else
        {
            string msg = " Level Locked!";
            StartCoroutine(DisplayMessage(msg));
        }
    }

    public void openLevelCompleteMenu()
    {
        SceneManager.LoadScene(0);
        LoadMenu(Menus.CompletedMenu);
    }

    public void openLevelFailedMenu()
    {
        SceneManager.LoadScene(0);
        LoadMenu(Menus.FailMenu);
    }

    public void PlayButton()
    {
        int currLevel = GameManager.Instance.GetLevelNo();
        SceneManager.LoadScene(1);
        GameManager.Instance.LoadLevel(currLevel);

    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
        LoadMenu(Menus.MainMenu);
    }

    public void PauseButton()
    {
        SceneManager.LoadScene(0);
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
        MenuList[currMenu].SetActive(false);
        currMenu = (int)menus;
        MenuList[currMenu].SetActive(true);
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
