using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void Quit() {
        Application.Quit();
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
}
