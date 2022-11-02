using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntajeManager : MonoBehaviour
{
    public Text puntaje;
    public static int puntajes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        puntaje.text = puntajes.ToString();
    }
}
