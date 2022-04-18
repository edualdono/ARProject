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

    private float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 1.1f;
        Boton_Pausa.onClick.AddListener(MenuPausa);
        Play.onClick.AddListener(Resume);
        Menu.onClick.AddListener(TaskOnClick_MenuPrincipal);
        //ControladorPausa.SetTrigger("Pausaintro");
    }

    void Update()
    {
        if (GameisPaused)
        {
            timeRemaining -= Time.deltaTime;
            if(timeRemaining <= 0)
            {
                Time.timeScale = 0f;
            }
        }
        
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
        AudioListener.pause = false;
    }

    void Resume()
    {
        GameisPaused = false;
        ControladorPausa.SetTrigger("MeterPanel");
        timeRemaining = 1.1f;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    void Pause()
    {
        GameisPaused = true;
        ControladorPausa.SetTrigger("SacarPanel");
        AudioListener.pause = true;
        //Time.timeScale = 0f;
    }
   
}
