using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausaMenu : MonoBehaviour
{
    public static bool GameisPaused = false;
    //public GameObject pauseMenuUI;
    public Button Boton_Pausa;
    public Button Play;
    public Button Menu;
    public Animator ControladorPausa;

    // Start is called before the first frame update
    void Start()
    {
        Boton_Pausa.onClick.AddListener(MenuPausa);
        Play.onClick.AddListener(Resume);
        Menu.onClick.AddListener(TaskOnClick_MenuPrincipal);
        //ControladorPausa.SetTrigger("Pausaintro");
    }

    public void MenuPausa()
    {
        if (GameisPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void TaskOnClick_MenuPrincipal()
    {
        Time.timeScale = 1f;
        GameisPaused = false;
        SceneManager.LoadScene("Menu_Principal", LoadSceneMode.Single);
    }

    void Resume()
    {
        GameisPaused = false;
        ControladorPausa.SetTrigger("MeterPanel");
        //Time.timeScale = 1f;
    }

    void Pause()
    {
        GameisPaused = true;
        ControladorPausa.SetTrigger("SacarPanel");
        //Time.timeScale = 0f;
    }
   
}
