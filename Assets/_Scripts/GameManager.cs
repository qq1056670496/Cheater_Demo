using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public Button continueBtn;
    [SerializeField]private GameObject _dontDestoryGo;
    private void Awake()
    {
        _instance = this;
        if(!PlayerPrefs.HasKey("Level")&&continueBtn!=null)
            continueBtn.interactable = false;
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void BackToMenu()
    {
        //存档 
        Save();
        SceneManager.LoadScene(0);
    }
    public void ExitGameAndSave()
    {
        //存档 
        Save();
        Application.Quit();
    }
    public void ExitGameNoSave()
    {
        Application.Quit();
    }
    public void Save()
    {
        UIPanelManager.Instance.KnapsackPanel.Save();
        UIPanelManager.Instance.CharacterPanel.Save();
        GameObject.FindWithTag("Player").GetComponent<PlayerState>().Save();
        ShortCutPanel._instance.Save();
    }
    private void Load()
    {
        UIPanelManager.Instance.KnapsackPanel.Load();
        UIPanelManager.Instance.CharacterPanel.Load();
        GameObject.FindWithTag("Player").GetComponent<PlayerState>().Load();
        ShortCutPanel._instance.Load();
    }
    private void OnSceneWasLoaded(Scene scene,LoadSceneMode mode)
    {
        Load();
    }

    public void NewGameBtn()
    {
        
        SceneManager.LoadScene(1);
        SoundManager._instance.StopAllSounds();
        SoundManager._instance.PlayAudio("Bgm1",0.2f).loop=true;
    }
    public void LoadGameBtn()
    {
        SceneManager.LoadScene(1);
        SoundManager._instance.StopAllSounds();
        SoundManager._instance.PlayAudio("Bgm1", 0.2f).loop = true;
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }
}
