using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class UImanager : MonoBehaviour
{
    public GameObject ticket1, ticket2, MenuScene, StartScene, AboutUsScene, ConfigScene, levelScene, levelmanger;
    public GameObject background;
    public GameObject canvas;
    public Image image;
    public Slider Slider;
    public GameObject Sounds;
    public GameObject soundtrack;
    float soundValue;
    private void Awake()
    {
        soundtrack = GameObject.FindGameObjectWithTag("Audio");
        SoundRedear();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       SoundControler();
    }
    public void CutTicketsAndEnter()
    {
        ticket1.gameObject.transform.DOShakePosition(3f, 10, 10);
        ticket2.gameObject.transform.DOShakePosition(3f, 10, 10);
        ticket1.gameObject.transform.DOLocalMoveX(-205f, 1f).SetDelay(3f);
        ticket2.gameObject.transform.DOLocalMoveX(510f, 1f).SetDelay(3f);
        Invoke("LoadUI", 3.5f);
        Sounds.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadUI()
    {
        StartScene.gameObject.SetActive(false);
        MenuScene.gameObject.SetActive(true);
        canvas.gameObject.GetComponent<Image>().sprite = background.gameObject.GetComponent<Image>().sprite;
        soundtrack.gameObject.SetActive(true);

    }
    public void LoadAboutUs()
    {
        AboutUsScene.gameObject.SetActive(true);
        Sounds.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();

    }
    public void CloseAboutUs()
    {
        AboutUsScene.gameObject.SetActive(false);
        Sounds.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();

    }
    public void LoadConfig()
    {
        ConfigScene.gameObject.SetActive(true);
        Sounds.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();

    }
    public void CloseConfig()
    {
        ConfigScene.gameObject.SetActive(false);
        Sounds.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
        SoundWriter();
    }
    public void LoadlevelSc()
    {
        levelScene.gameObject.SetActive(true);
        Sounds.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();

    }
    public void CloseLevelSc()
    {
        levelScene.gameObject.SetActive(false);
        Sounds.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();

    }

    public void LoadFaceBook()
    {
        Application.OpenURL("https://www.facebook.com/OBinteractivesolutions");
        Sounds.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();

    }

    public void LoadWebSite()
    {
        Application.OpenURL("https://offbeateg.com");
        Sounds.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();

    }
    public void SoundControler()
    {
        soundtrack.gameObject.GetComponent<AudioSource>().volume = Slider.value;

    }
    public void SoundRedear()
    {
        BinaryFormatter bf_Sound= new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/Sound.save"))
        {
            FileStream audio = File.Open(Application.persistentDataPath + "/Sound.save", FileMode.Open);
            Slider.value = (float)bf_Sound.Deserialize(audio);
            audio.Close();
        }
        else
        {
            Slider.value = .5f;
            FileStream audio = File.Create(Application.persistentDataPath + "/Sound.save");
            audio.Close();
        }
    }
    public void SoundWriter()
    {
        soundValue= Slider.value;
        BinaryFormatter bf_Sound = new BinaryFormatter();
        FileStream audio = File.Open(Application.persistentDataPath + "/Sound.save", FileMode.Open);
        bf_Sound.Serialize(audio, soundValue);
        audio.Close();
    }
    
}

