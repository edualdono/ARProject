using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Vuforia;

public class Lenguage_Button : MonoBehaviour
{
    public VirtualButtonBehaviour vButtonEspanol;
    public VirtualButtonBehaviour vButtonIngles;
    public AudioClip audioEspanol;
    public AudioClip audioIngles;

    private AudioSource Reproductor;
    private AudioSource Reproductor2;


    // Start is called before the first frame update
    void Start()
    {
        vButtonEspanol.RegisterOnButtonPressed(OnButtonPressed);
        vButtonEspanol.RegisterOnButtonReleased(OnButtonReleased);

        vButtonIngles.RegisterOnButtonPressed(OnButtonPressed2);
        vButtonIngles.RegisterOnButtonReleased(OnButtonReleased2);

        Reproductor = gameObject.AddComponent<AudioSource>();
        Reproductor.clip = audioEspanol;
        Reproductor.playOnAwake = false;

        Reproductor2 = gameObject.AddComponent<AudioSource>();
        Reproductor2.clip = audioIngles;
        Reproductor2.playOnAwake = false;
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //Debug.Log("Se ha presionado espanol");
        //Reproductor.clip = audioEspanol;
        Reproductor.Play();
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        //Debug.Log("Se ha retirado espanol");
        Reproductor.Stop();
    }

    public void OnButtonPressed2(VirtualButtonBehaviour vb)
    {
        //Debug.Log("Se ha presionado ingles");
        //Reproductor.clip = audioIngles;
        Reproductor2.Play();
    }

    public void OnButtonReleased2(VirtualButtonBehaviour vb)
    {
        //Debug.Log("Se ha retirado espanol");
        Reproductor2.Stop();
    }
}
