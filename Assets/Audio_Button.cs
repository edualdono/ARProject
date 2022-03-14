using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Vuforia;

public class Audio_Button : MonoBehaviour
{
    public VirtualButtonBehaviour vButtonEspanol;
    public VirtualButtonBehaviour vButtonIngles;
    public AudioClip audioEspanol;
    public AudioClip audioIngles;

    private AudioSource Reproductor;


    // Start is called before the first frame update
    void Start()
    {
        vButtonEspanol.RegisterOnButtonPressed(OnButtonPressed);
        //vButtonEspanol.RegisterOnButtonReleased(OnButtonReleased);
        
        vButtonIngles.RegisterOnButtonPressed(OnButtonPressed2);
        //vButtonIngles.RegisterOnButtonReleased(OnButtonReleased2);

        Reproductor = gameObject.AddComponent<AudioSource>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("Se ha presionado espanol");
        Reproductor.clip = audioEspanol;
        Reproductor.Play();
    }

    public void OnButtonPressed2(VirtualButtonBehaviour vb)
    {
        Debug.Log("Se ha presionado ingles");
        Reproductor.clip = audioIngles;
        Reproductor.Play();
    }
    
}
