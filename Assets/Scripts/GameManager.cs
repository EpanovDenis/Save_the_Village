using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ImageTimer HarvestTimer;
    public ImageTimer EatingTimer;
    public Image RaidTimerImg;
    public Image PeasantTimerImg;
    public Image WarriorTimerImg;
    public Image musicOnOff;
    public GameObject BattleImg;
    public AudioSource BackgroundMusic;
    public AudioSource ClickHire;
    

    public Sprite musicOn, musicOff;

    public Button peasantButton;
    public Button warriorButton;

    public Text resourcesText;   
    public Text NextTimerText;
    public Text BattlePanelText;
    public Text GameOverText;
    public Text TimeWinnerText;

    public GameObject MainMenu;
    public GameObject GameOverScreen;
    public GameObject PanelExitTheGame;
    public GameObject BattlePanel;
    public GameObject InstructionsPanel;
    public GameObject WinerPanel;

    public int peasantCount;
    public int warriorCount;
    public int weathCount;

    public int weathPerPeasant;
    public int weathToWarrior;

    public int peasantCost;
    public int warriorCost;

    public float peasantCreateTime;
    public float warriorCreateTime;
    public float raidMaxTime;
    public int raidIncrease;
    public int nextRaid;

    private float peasantTimer = -2;
    private float warriorTimer = -2;
    private float raidTimer;
    private int nextCountWeath;
    private int nextCountEating;
    private int waveNamb = 0;

    private bool startFlag = false;

    void Start()
    {
        Time.timeScale = 1;
       
        raidTimer = raidMaxTime;
        UpdateText();            
        
    }
        
    void Update()
    {
        if (weathCount >= 1000)
        {
            Time.timeScale = 0;
            TimeWinnerText.text = "Time until victory:" + Mathf.Round(Time.time) + "sec.";
            WinerPanel.SetActive(true);
        }

        if (startFlag == true)
        {
            raidTimer -= Time.deltaTime;
            RaidTimerImg.fillAmount = raidTimer / raidMaxTime;

            if (RaidTimerImg.fillAmount == 0)
            {
                waveNamb++;                
                Time.timeScale = 0;
                BattlePanelText.text = nextRaid + " VS " + warriorCount + "\n" + "WIN";
                BattlePanel.SetActive(true);
            }

            if (raidTimer <= 0)
            {             
                raidTimer = raidMaxTime;
                warriorCount -= nextRaid;
                nextRaid += raidIncrease;                
            }            

            if (HarvestTimer.tick)
            {
                weathCount += peasantCount * weathPerPeasant;
            }

            if (EatingTimer.tick)
            {
                weathCount -= warriorCount * weathToWarrior;
            }

            if (peasantTimer > 0)
            {
                peasantTimer -= Time.deltaTime;
                PeasantTimerImg.fillAmount = peasantTimer / peasantCreateTime;
            }
            else if (peasantTimer > -1)
            {
                PeasantTimerImg.fillAmount = 1;
                peasantButton.interactable = true;
                peasantCount += 1;
                peasantTimer = -2;
            }

            if (warriorTimer > 0)
            {
                warriorTimer -= Time.deltaTime;
                WarriorTimerImg.fillAmount = warriorTimer / warriorCreateTime;
            }            
            else if (warriorTimer > -1)
            {
                WarriorTimerImg.fillAmount = 1;
                warriorButton.interactable = true;
                warriorCount += 1;
                warriorTimer = -2;
            }

            nextCountWeath = peasantCount * weathPerPeasant;
            nextCountEating = warriorCount * weathToWarrior;

            UpdateText();

            if (warriorCount < 0)
            {
                waveNamb = waveNamb - 1;
                BattlePanel.SetActive(false);
                Time.timeScale = 0;
                GameOverText.text = " Your village is ravaged!You survived " + waveNamb + " raids! ";
                GameOverScreen.SetActive(true);
                startFlag = false;
            }
        }    
        
        if (nextCountEating > weathCount || warriorTimer > 0)
        {
            warriorButton.interactable = false;
        }
        else
        {
            warriorButton.interactable = true;
        }
    }

    public void CreatePeasant()
    {
        ClickHire.Play();
        weathCount -= peasantCost;
        peasantTimer = peasantCreateTime;
        peasantButton.interactable = false;
    }

    public void CtreateWarrior()
    {
        ClickHire.Play();
        warriorCount -= warriorCost;
        warriorTimer = warriorCreateTime;
        warriorButton.interactable = false;
    }

    public void StartGameButtonClick()
    {
        ClickHire.Play();
        startFlag = true;
        MainMenu.SetActive(false);
        WinerPanel.SetActive(false);
    }

    public void MainMusicPause()
    {
        ClickHire.Play();
        if (BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Pause();
            musicOnOff.sprite = musicOff;
        }
        else
        {
            BackgroundMusic.Play();
            musicOnOff.sprite = musicOn;
        }
    }

    public void ExitTheGameButtonClick()
    {
        ClickHire.Play();
        PanelExitTheGame.SetActive(true);
        Time.timeScale = 0;
    }

    public void ExitTheGameButtonYesClick()
    {
        ClickHire.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);                        
    }

    public void ExitTheGameButtonNoClick()
    {
        ClickHire.Play();
        PanelExitTheGame.SetActive(false);
        Time.timeScale = 1;
    }      
    
    public void NextWaveButton()
    {
        ClickHire.Play();
        BattlePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void InstructionsPanelButton()
    {
        ClickHire.Play();
        MainMenu.SetActive(false);
        InstructionsPanel.SetActive(true);
    }

    public void InstructionsPanelButtonExit()
    {
        ClickHire.Play();
        InstructionsPanel.SetActive(false);
        MainMenu.SetActive(true);
    }

    private void UpdateText()
    {
        resourcesText.text = weathCount + "\n\n" + peasantCount + "\n" + warriorCount;
        NextTimerText.text = "+"+ nextCountWeath + "\n" + "-" + nextCountEating + "\n" + nextRaid; 
    }
}
