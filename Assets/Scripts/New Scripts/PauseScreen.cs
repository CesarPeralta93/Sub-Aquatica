using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{

    public GameObject pausecanvas;
    public GameObject pausePanel;
    public GameObject ajustesPanel;
    public GameObject controlesPanel;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void AjustesButton()
    {
        pausePanel.SetActive(false);
        ajustesPanel.SetActive(true);
    }

    public void ControlesButton()
    {
        pausePanel.SetActive(false);
        controlesPanel.SetActive(true);
    }

    public void SalirAjustes()
    {
        ajustesPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void SalirControles()
    {
        controlesPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void Salir()
    {
        pausecanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
