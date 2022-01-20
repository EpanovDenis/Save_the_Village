using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{   
    public AudioSource Sound;    
    public GameObject PausePanel;
    public AudioSource ClickHire;

    private void Start()
    {
            
    }

    public void PauseGameMainMenu()
    {        
        ClickHire.Play();
        Time.timeScale = 0;
        Sound.Pause();
        PausePanel.SetActive(true);        
    }

    public void PauseGamePausePanel()
    {        
        Time.timeScale = 1;
        ClickHire.Play();
        Sound.Play();
        PausePanel.SetActive(false);
    }
}
