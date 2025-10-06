using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Panel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGameLaunched()
    {
        Panel.SetActive(true);
    }

    public void StartGameHip()
    {
        Panel.SetActive(false);
        PlayerManager.Type = PlayerManager.PlayerType.Hippo;
        SceneManager.LoadScene("MainGame");
    }

    public void StartGameCheese()
    {
        Panel.SetActive(false);
        PlayerManager.Type = PlayerManager.PlayerType.Cheese;
        SceneManager.LoadScene("MainGame");
    }

    public void StartGameCreatura()
    {
        Panel.SetActive(false);
        PlayerManager.Type = PlayerManager.PlayerType.Rat;
        SceneManager.LoadScene("MainGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
