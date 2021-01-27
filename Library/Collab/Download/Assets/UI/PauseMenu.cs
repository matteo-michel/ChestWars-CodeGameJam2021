using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
    }

    //Bouton pour reprendre le jeu
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public GameObject parametresWindow;
    public void ParametreButton()
    {
        parametresWindow.SetActive(true);
    }

    public void FermerParametresWindow()
    {
        parametresWindow.SetActive(false);
    }

    public void MenuPrincipalButton()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
