using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour
{
    public GameObject menuObject;
    public Button saveButton;
    public int fpsLimit;
    public Slider slider;
    public Slider sfxSlider;
    public Slider musicSlider;

    public Toggle vSync;
    public Toggle fullscreen;

    int targetFps;

    bool isPauseActive = false;

    public static event Action<bool> isPauseMenuActive;

    void start(){

    }
    void Update()
    {
        
        if(Application.targetFrameRate != targetFps)
            Application.targetFrameRate = targetFps;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPauseActive) {
                menuObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                isPauseActive = true;
                isPauseMenuActive?.Invoke(true);
            }
            else if (isPauseActive) {
                // print("call me :)");
                menuObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                isPauseActive = false;
                isPauseMenuActive?.Invoke(false);
            }            
        }
    }

    public void ButtonResume() {
        menuObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        isPauseActive = false;
        isPauseMenuActive?.Invoke(false);
    }

    public void ButtonMainMenu(int mainMenuIndex) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ButtonExitGame() {
        Application.Quit();
    }

    public void HardReset() {
        Application.Quit();
    }

    public void FPSLimiter(int fps_) {
        targetFps = fps_;
    }

    public void VSyncToggler(bool b_) {
        QualitySettings.vSyncCount = b_?1:0;
    }

    public void EnableSaveButton() {
        saveButton.interactable = true;
    }

    public void ButtonSaveSettings() {
        FPSLimiter((int)slider.value);
        VSyncToggler(vSync.enabled);
        SetLevelMusic(musicSlider.value);
        SetLevelSFX(sfxSlider.value);
        // PlayerPrefs.SetInt("Music", (int)musicSLider.value);
        //ScalableBufferManager.ResizeBuffers(renderScale.value, renderScale.value);

        if (fullscreen) {
            Screen.fullScreen = fullscreen;
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        } else {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        saveButton.interactable = false;      
    }
    public AudioMixer MusicMixer;

    public void SetLevelMusic (float musicSLider){
        MusicMixer.SetFloat("MusicVol", Mathf.Log10(musicSLider)*20);
    }
    public AudioMixer SFXMixer;
    public void SetLevelSFX (float musicSLider){
        SFXMixer.SetFloat("SFXVol",Mathf.Log10(musicSLider)*20 );
    }
}
