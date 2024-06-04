using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isGamePaused = false;
    public GameObject pauseObjects;
    public GameObject panel;

    private bool isSettingsActive;
    public GameObject settingsObjects;

    private float volume;


    private void Start()
    {

        Time.timeScale = 1f;
        SetSliderVolume();
    }

    [SerializeField] private bool isMainMenu = false;
    private void Update()
    {
        if (isMainMenu == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
                RestartLevel();

            if (Input.GetKeyDown(KeyCode.Escape))
                Back();
        }

    }


    public void PauseGame()
    {
        SetSliderVolume();

        if (isGamePaused == false)
        {
            Time.timeScale = 0f;
            pauseObjects.SetActive(true);
            panel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseObjects.SetActive(false);
            panel.SetActive(false);
        }
        isGamePaused = !isGamePaused;
    }


    public void ShowSettings()
    {
        pauseObjects.SetActive(false);
        settingsObjects.SetActive(true);
        isSettingsActive = true;
    }


    //ESC works to
    public void Back()
    {
        if (isSettingsActive == true)
        {
            pauseObjects.SetActive(true);
            settingsObjects.SetActive(false);
            isSettingsActive = false;
        }
        else
            PauseGame();


    }


    //R works to
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    //can be use for main menu and next level
    public void LoadLevel(string LevelName)
    {
        try
        {
            SceneManager.LoadScene(LevelName);

        }
        catch
        {

            Debug.Log(LevelName + " not found");
        }

    }

    public void VolumeChange(GameObject slider)
    {
        volume = slider.GetComponent<Slider>().value;

        if (slider.name == "Music")
            FindObjectOfType<AudioManager>().VolumeUpdate(volume, true);
        else
            FindObjectOfType<AudioManager>().VolumeUpdate(volume, false);

    }
    //music dogru calismasi icin theme adli dosya olmasi zorunlu
    public GameObject musicSlider;
    public GameObject sfxSlider;
    public void SetSliderVolume()
    {
        Debug.Log(musicSlider.GetComponent<Slider>().value);
        Debug.Log(FindObjectOfType<AudioManager>().GetAudioVolume(true));
        musicSlider.GetComponent<Slider>().value = FindObjectOfType<AudioManager>().GetAudioVolume(true);
        sfxSlider.GetComponent<Slider>().value = FindObjectOfType<AudioManager>().GetAudioVolume(false);
    }


    public GameObject gameOverObjects;
    public GameObject nextLevelObjects;
    public void GameOver()
    {
        //FindObjectOfType<AudioManager>().Play("fail");

        Time.timeScale = 0f;
        panel.SetActive(true);
        gameOverObjects.SetActive(true);
        isMainMenu = true;

    }
    public void NextLevel()
    {
        //FindObjectOfType<AudioManager>().Play("win");

        Time.timeScale = 0f;
        panel.SetActive(true);
        nextLevelObjects.SetActive(true);
        isMainMenu = true;
    }




    public void CheckForEnemies()
    {
        StartCoroutine(CheckingEnemy());
    }

    IEnumerator CheckingEnemy()
    {
        yield return new WaitForSeconds(0.1f);

        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            yield return new WaitForSeconds(1f);
            NextLevel();
        }

        if (GameObject.FindGameObjectsWithTag("Ally").Length <= 0)
        {
            yield return new WaitForSeconds(1f);
            GameOver();
        }

        yield return null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
