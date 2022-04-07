using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Principal : MonoBehaviour
{
    public List<Button> Buttons;


    void Start()
    {
        Buttons[0].onClick.AddListener(TaskOnClick_Scene1);
        Buttons[1].onClick.AddListener(TaskOnClick_Scene2);
        Buttons[2].onClick.AddListener(TaskOnClick_Exit);
    }

    void TaskOnClick_Scene1()
    {
        SceneManager.LoadScene("CardsScene", LoadSceneMode.Single);
    }
    void TaskOnClick_Scene2()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    void TaskOnClick_Exit()
    {
        Application.Quit();
    }

}
