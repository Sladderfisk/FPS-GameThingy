using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject normalUi;
    [SerializeField] private GameObject controllUi;

    private void Start()
    {
        normalUi.SetActive(true);
        controllUi.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Controlls()
    {
        normalUi.SetActive(false);
        controllUi.SetActive(true);
    }

    public void Exit()
    {
        normalUi.SetActive(true);
        controllUi.SetActive(false);
    }
}
