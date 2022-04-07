using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using Vuforia;

public class Game_Script : MonoBehaviour
{
    public List<GameObject> cards;
    public List<GameObject> prefabs;
    public TMP_Text Reloj;
    public TMP_Text Ask_Contador;
    public GameObject PanelError;
    public GameObject PanelCorrecto;
    public AudioClip audioCorrecto;
    //public AudioClip audioIngles;
    public AudioClip audioIncorrecto;
    public AudioClip audioReloj;
    public List<RawImage> CardsErrores;
    public GameObject PanelFinal;
    public Button Corregir;
    public Button PlayAgain;


    private AudioSource Reproductor;
    //private AudioSource ReproductorCorrecto;
    //private AudioSource ReproductorIncorrecto;
    private string emptyStr;
    private List<Custom_Observer> fun = new List<Custom_Observer>();
    private bool[] errores = new bool[8]; 
    private int[] num = new int[8];
    private int askstodo = 8;
    private int num_ask;
    private int contadorerrores;
    private GameObject prefab_selected;
    private float Seconds = 60f;
    private float timepause;
    private float timeRemaining;
    private bool compare;
    private bool answered;
    private bool pause;
    private bool fixingerror;

    void Start()
    {
        timeRemaining = Seconds;
        compare = false;
        answered = false;
        pause = false;
        fixingerror = false;
        Reloj.text = " " + timeRemaining;
        num_ask = 0;
        Ask_Contador.text = " " + num_ask;
        int cardnum = cards.Count;
        for (int s = 0; s < cardnum; s++)
        {
            fun.Add(cards[s].GetComponent<Custom_Observer>());
        }
        Reproductor = gameObject.AddComponent<AudioSource>();
        Reproductor.clip = audioReloj;
        Corregir.onClick.AddListener(Fix_Button);
        PlayAgain.onClick.AddListener(Play_Again);
        //ReproductorCorrecto.clip = audioCorrecto;
        //ReproductorIncorrecto.clip = audioIncorrecto;
        //Debug.Log(fun);

    }

    void Update()
    {
        //es necesaria una pausa para retirar la tarjeta entre preguntas 
        if (!pause)
        {
            if (timeRemaining == Seconds)
            {
                if (askstodo > num_ask)
                {
                    //Seleccionamos una nueva pregunta cada vez que el contador este en 60 secs
                    if (!fixingerror)
                    {
                        //caso en el que no solucionamos errores
                        setQuestion();
                        Debug.Log("Lo hizo Set");
                    }
                    else
                    {
                        //caso donde si se solucionan
                        Fix_Error();
                        Debug.Log("Lo hizo Fix");
                    }
                    //prefab_selected.SetActive(true);
                    Ask_Contador.text = " " + num_ask;
                    Debug.Log("Se ha escogido pregunta" + num_ask);
                }
                else
                {
                    GameOver();
                }
            }

            Reloj.text = " " + timeRemaining.ToString("f0");
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                Reloj.text = "0";
                timeRemaining = CheckAnswer(compare);
            }
            else
            {
                if (answered)
                {
                    timeRemaining = CheckAnswer(compare);
                }
            }

            for (int i = 0; i < fun.Count; i++)
            {
                if (fun[i].sendflagstatus())
                {
                    answered = true;
                    compare = cards[i].CompareTag(prefab_selected.tag);
                    break;
                }
                //compare = false;
            }
        }
        else
        {
            if (timepause <= 0)
            {
                //la pausa termino
                PanelCorrecto.SetActive(false);
                PanelError.SetActive(false);
                Reproductor.Stop();
                //ReproductorIncorrecto.Stop();
                pause = false;
            }
            else
            {
                timepause -= Time.deltaTime;
            }
        }

    }

    private void GameOver()
    {
        PanelFinal.SetActive(true);
        Color green_new = new Color32(0, 255, 0, 226);
        Color red_new = new Color32(255, 0, 0, 215);
        Reproductor.Stop();
        fixingerror = false;
        contadorerrores = 0;
        //askstodo = 0;
        for (int i = 0; i < 8; i++)
        {
            if (errores[i])
            {
                //iluminamos de color verde las tarjetas correctas
                CardsErrores[i].color = green_new;
            }
            else
            {
                //iluminamos de color rojo las tarjetas incorrectas
                //askstodo++;
                CardsErrores[i].color = red_new;
                contadorerrores++;
            }
        }
        //si hay errores activamso el boton de caso contrario lo desaparecemos
        if (contadorerrores > 0)
        {
            Corregir.gameObject.SetActive(true);
        }
        else
        {
            Corregir.gameObject.SetActive(false); ;
        }
    }

    private void Fix_Error()
    {
        compare = false;
        answered = false;
        Reproductor.clip = audioReloj;
        Reproductor.Play();
        int num_pregunta;
        for (int i = num_ask; i < 8; i++)
        {
            if (errores[i])
            {
                // tarjetas correctas
                //todas las tarjetas son correctas
            }
            else
            {
                //tarjetas incorrectas
                //num_pregunta = i;
                //num[num_ask] = num_pregunta;
                num_pregunta = num[i];
                num_ask = i+1;
                prefab_selected = prefabs[num_pregunta];
                prefab_selected.SetActive(true);
                contadorerrores--;
                break;
            }
        }
    }

    private void Play_Again()
    {
        PanelFinal.SetActive(false);
        num_ask = 0;
        fixingerror = false;
    }

    private void Fix_Button()
    {
        fixingerror = true;
        PanelFinal.SetActive(false);
        num_ask = 0;
    }

    private float CheckAnswer(bool compare)
    {
        Debug.Log(compare);
        Reproductor.Stop();
        if (compare)
        {
            //Caso en el que fue contestada correactamente la pregunta 
            Debug.Log("La respuesta es correcta");
            errores[num_ask - 1] = true;
            PanelCorrecto.SetActive(true);
            Reproductor.clip = audioCorrecto;
            Reproductor.Play();
        }
        else
        {
            //Caso en el que fue contestado erroneamente
            Debug.Log("La respuesta es incorrecta");
            errores[num_ask - 1] = false;
            PanelError.SetActive(true);
            Reproductor.clip = audioIncorrecto;
            Reproductor.Play();
        }
        prefab_selected.SetActive(false);
        pause = true;
        timepause = 3f;
        if(contadorerrores == 0 && fixingerror)
        {
            num_ask = 8;
            fixingerror = false;
        }
        return Seconds;
    }

    private void setQuestion()
    {
        int random = Random.Range(0, prefabs.Count);
        compare = false;
        answered = false;
        Reproductor.clip = audioReloj;
        Reproductor.Play();
        if (num_ask == 0)
        {
            num[num_ask] = random;
            num_ask++;
            prefab_selected = prefabs[random];
            prefab_selected.SetActive(true);
            Debug.Log("EL prefab escogido es");
            Debug.Log(random);
        }
        else
        {
            while (CheckQuestion(random, num_ask))
            {
                random = Random.Range(0, prefabs.Count);
            }
            num[num_ask] = random;
            num_ask++;
            prefab_selected = prefabs[random];
            prefab_selected.SetActive(true);
            Debug.Log(prefab_selected.tag);
        }
        
    }

    //revisamos que la pregunta no se repita
    private bool CheckQuestion(int random, int num_ask)
    {
        //bool flag;
        for(int i=0; i < num_ask; i++)
        {
            if(num[i] == random)
            {
                return true;
            }
        }
        return false;
    }
}
