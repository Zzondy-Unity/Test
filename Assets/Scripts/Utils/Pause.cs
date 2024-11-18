using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject PauseBtn;
    [SerializeField] private GameObject ResumeBtn;
    [SerializeField] private GameObject PausePanel;

    //BGM
    //Input

    public event Action OnPauseEvent;
    public event Action OnResumeEvent;

    private void Awake()
    {
        UIManager.Instance.pause = this;
    }

    private void Start()
    {
        PausePanel.SetActive(false);
        PauseBtn.SetActive(true);
        ResumeBtn.SetActive(false);
    }

    public void OnPause()
    {
        BtnToggle();
        SoundManager.instance.PauseBGM();
        OnPauseEvent?.Invoke();
        Time.timeScale = 0f;
    }

    public void OnResume()
    {
        BtnToggle();
        SoundManager.instance.ResumeBGM();
        OnResumeEvent?.Invoke();
        Time.timeScale = 1f;
    }

    private void BtnToggle()
    {
        PauseBtn.SetActive(!PauseBtn.activeSelf);
        ResumeBtn.SetActive(!ResumeBtn.activeSelf);
        PausePanel.SetActive(ResumeBtn.activeSelf);
    }
}
