using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public GameObject levels;
    public GameObject view;
    public GameObject levelScene;
    public GameObject sounds;
    public GameObject stars;
    public GameObject StartButton;
    public List<UnityAction> functions = new List<UnityAction>();
    public string level;
    public int levl;
    public int currentLevel;
    public int countDown;
    public int scoreTowin;
    public int ballons;
    public int specialBallons;
    public bool PowerBallon;
    public bool obstecl1;
    public bool obstecl2;
    public float speed;
    public bool blind;
    UnityAction action;
    bool check;
    int selectedLevel;
    bool start;
    void Start()
    {
         //File.Delete(Application.persistentDataPath + "/fileCurrentLevel.save");
         start = false;
         check = false;
        LevelFucntions();
        CurentLevelToGo();
        movetoLevel();
        for (int i=0;i<101;)
        {   
            levels.gameObject.transform.GetChild(i).GetComponent<Button>().onClick.AddListener((functions[i]));
            levels.gameObject.transform.GetChild(i).GetComponent<Button>().onClick.AddListener((PlaySound));
            levels.gameObject.transform.GetChild(i).GetComponent<Button>().onClick.AddListener((ChangeButton));
            i++;
        }
        //  currentLevel = levl;
        
    }
    private void Update()
    {
        if (levelScene.gameObject.activeSelf == true && check == false)
        {
            levels.gameObject.transform.DOMove(view.gameObject.GetComponent<ScrollRect>().content.GetChild(101 - currentLevel).transform.position, 2f);
            check = true;
        }
        
    }
    private void FixedUpdate()
    {

        if(start)
        { 
            if(selectedLevel==currentLevel||selectedLevel<currentLevel)
            {
                stars.gameObject.transform.position = new Vector3(0f, 14f, 0f) + levels.gameObject.transform.GetChild(selectedLevel).transform.position;
            }

            for (int i = 1; i < 101;)
        {
            if (i == selectedLevel)
            {
                //Debug.Log(currentLevel.ToString());
                //Debug.Log(i.ToString());
                Color color = new Color(0.2113207f, 0.5933162f, 1, 1);
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().color = color;
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().fontSize = 9;
                levels.gameObject.transform.GetChild(i).GetComponent<Button>().enabled = true;
                stars.gameObject.transform.SetParent(levels.gameObject.transform.GetChild(i).transform);
            }
            if (i < selectedLevel)
            {
                    levels.gameObject.transform.GetChild(i).GetComponent<Text>().fontSize = 8;
                    levels.gameObject.transform.GetChild(i).GetComponent<Text>().color = Color.white;
                levels.gameObject.transform.GetChild(i).GetComponent<Button>().enabled = true;
                    levels.gameObject.transform.GetChild(i).GetComponent<Text>().fontStyle = FontStyle.Normal;

                }
                if (i > currentLevel)
            {
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().color = Color.gray;
                levels.gameObject.transform.GetChild(i).GetComponent<Button>().enabled = false;
            }
            if (i > selectedLevel && i < currentLevel)
            {
                Color color = new Color(0.2113207f, 0.5933162f, 1, 1);
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().color = Color.white;
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().fontSize = 8;
                levels.gameObject.transform.GetChild(i).GetComponent<Button>().enabled = true;
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().fontStyle = FontStyle.Normal;

                }

                i++;
        }
        }
    }
    public void LoadGame()
    {
        //functions[selectedLevel].Invoke();
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        start = false;
    }
    public void ChangeButton ()
    {
        StartButton.gameObject.GetComponent<Button>().interactable = true;
    }
    public void PlaySound()
    {
        sounds.gameObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();

        Debug.Log("shampo");
    }
    public void movetoLevel ()
    {
        for (int i = 1; i < 101; )
        {
            if (i == currentLevel)
            {
                Debug.Log(currentLevel.ToString());
                Debug.Log(i.ToString());
                Color color=new Color(0.2113207f, 0.5933162f, 1,1);
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().color = color;
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().fontSize = 12;
                levels.gameObject.transform.GetChild(i).GetComponent<Button>().enabled = true;
                stars.gameObject.transform.SetParent(levels.gameObject.transform.GetChild(i).transform);
            }
            if (i<currentLevel)
            {
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().color = Color.white;
                levels.gameObject.transform.GetChild(i).GetComponent<Button>().enabled = true;

            }
            if (i >currentLevel)
            {
                levels.gameObject.transform.GetChild(i).GetComponent<Text>().color=Color.gray;
                levels.gameObject.transform.GetChild(i).GetComponent<Button>().enabled = false;
            }

            i++;
        }
        selectedLevel = currentLevel;
        start = true;

    }
    public void CurentLevelToGo()
    {
        
         BinaryFormatter bf_currentLevel = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/fileCurrentLevel.save"))
        {
            FileStream fileCurrentLevel = File.Open(Application.persistentDataPath + "/fileCurrentLevel.save", FileMode.Open);
            currentLevel = (int)bf_currentLevel.Deserialize(fileCurrentLevel);
            Debug.Log("currentLevel");
            fileCurrentLevel.Close();
        }
        else
        {
            currentLevel =selectedLevel= 1;
            FileStream fileCurrentLevel2 = File.Create(Application.persistentDataPath + "/fileCurrentLevel.save");
            fileCurrentLevel2.Close();
        }


    }

    public void CreateOrLoadFile()
    {
        BinaryFormatter bf_ballons = new BinaryFormatter();
        BinaryFormatter bf_Specialballons = new BinaryFormatter();
        BinaryFormatter bf_levels = new BinaryFormatter();
        BinaryFormatter bf_levl = new BinaryFormatter();
        BinaryFormatter bf_currentLevel = new BinaryFormatter();
        BinaryFormatter bf_countDown = new BinaryFormatter();
        BinaryFormatter bf_scoreTowin = new BinaryFormatter();
        BinaryFormatter bf_PowerBallon = new BinaryFormatter();
        BinaryFormatter bf_obstecl1 = new BinaryFormatter();
        BinaryFormatter bf_obstecl2 = new BinaryFormatter();
        BinaryFormatter bf_speed = new BinaryFormatter();
        BinaryFormatter bf_blind = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf_ballons.Serialize(file, ballons);
        bf_Specialballons.Serialize(file, specialBallons);
        bf_countDown.Serialize(file, countDown);
        bf_PowerBallon.Serialize(file, PowerBallon);
        bf_blind.Serialize(file, blind);
        bf_speed.Serialize(file, speed);
        bf_obstecl1.Serialize(file, obstecl1);
        bf_obstecl2.Serialize(file, obstecl2);
        bf_levels.Serialize(file, level);
        bf_scoreTowin.Serialize(file, scoreTowin);
        bf_levl.Serialize(file, levl);
        bf_currentLevel.Serialize(file, currentLevel);
        file.Close();
    }    
    public void LevelFucntions()
    {
        functions.Add(level1);
        functions.Add(level1);
       functions.Add(level2);
       functions.Add(level3);
       functions.Add(level4);
       functions.Add(level5);
       functions.Add(level6);
       functions.Add(level7);
       functions.Add(level8);
       functions.Add(level9);
       functions.Add(level10);
       functions.Add(level11);
       functions.Add(level12);
       functions.Add(level13);
       functions.Add(level14);
       functions.Add(level15);
       functions.Add(level16);
       functions.Add(level17);
       functions.Add(level18);
       functions.Add(level19);
       functions.Add(level20);
       functions.Add(level21);
       functions.Add(level22);
       functions.Add(level23);
       functions.Add(level24);
       functions.Add(level25);
       functions.Add(level26);
       functions.Add(level27);
       functions.Add(level28);
       functions.Add(level29);
       functions.Add(level30);
       functions.Add(level31);
       functions.Add(level32);
       functions.Add(level33);
       functions.Add(level34);
       functions.Add(level35);
       functions.Add(level36);
       functions.Add(level37);
       functions.Add(level38);
       functions.Add(level39);
       functions.Add(level40);
       functions.Add(level41);
       functions.Add(level42);
       functions.Add(level43);
       functions.Add(level44);
       functions.Add(level45);
       functions.Add(level46);
       functions.Add(level47);
       functions.Add(level48);
       functions.Add(level49);
       functions.Add(level50); 
       functions.Add(level51);
       functions.Add(level52);
       functions.Add(level53);
       functions.Add(level54);
       functions.Add(level55);
       functions.Add(level56);
       functions.Add(level57);
       functions.Add(level58);
       functions.Add(level59);
       functions.Add(level60);
       functions.Add(level61);
       functions.Add(level62);
       functions.Add(level63);
       functions.Add(level64);
       functions.Add(level65);
       functions.Add(level66);
       functions.Add(level67);
       functions.Add(level68);
       functions.Add(level69);
       functions.Add(level70);
       functions.Add(level71);
       functions.Add(level72);
       functions.Add(level73);
       functions.Add(level74);
       functions.Add(level75);
       functions.Add(level76);
       functions.Add(level77);
       functions.Add(level78);
       functions.Add(level79);
       functions.Add(level80);
       functions.Add(level81);
       functions.Add(level82);
       functions.Add(level83);
       functions.Add(level84);
       functions.Add(level85);
       functions.Add(level86);
       functions.Add(level87);
       functions.Add(level88);
       functions.Add(level89);
       functions.Add(level90);
       functions.Add(level91);
       functions.Add(level92);
       functions.Add(level93);
       functions.Add(level94);
       functions.Add(level95);
       functions.Add(level96);
       functions.Add(level97);
       functions.Add(level98);
       functions.Add(level99);
       functions.Add(level100);
    


    }
    public void level1 ()
    {
        selectedLevel=levl = 1;
        level = "lvl1";
        countDown = 59;
        scoreTowin = 20;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = false;
        obstecl2 = false;
        speed = 0;
        blind = false;
        CreateOrLoadFile();
    }
    public void level2()
    {
        selectedLevel = levl = 2;
        level = "lvl2";
        countDown = 59;
        scoreTowin = 25;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = true;
        obstecl2 = false;
        speed = 0;
        blind = false;
        CreateOrLoadFile();

    }
    public void level3()
    {
        selectedLevel = levl = 3;
        level = "lvl3";
        countDown = 59;
        scoreTowin = 30;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = false;
        obstecl2 = false;
        speed = 0;
        blind = true;
        CreateOrLoadFile();
    }
    public void level4()
    {
        selectedLevel = levl = 4;
        level = "lvl4";
        countDown = 59;
        scoreTowin = 35;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = false;
        obstecl2 = false;
        speed = 0;
        blind = false;
        CreateOrLoadFile();

    }
    public void level5()
    {
        selectedLevel = levl = 5;
        level = "lvl5";
        countDown = 59;
        scoreTowin = 40;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = true;
        obstecl2 = false;
        speed = 0;
        blind = false;
        CreateOrLoadFile();

    }
    public void level6()
    {
        selectedLevel = levl = 6;
        level = "lvl6";
        countDown = 59;
        scoreTowin = 45;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 0;
        blind = false;
        CreateOrLoadFile();

    }
    public void level7()
    {
        selectedLevel = levl = 7;
        level = "lvl7";
        countDown = 59;
        scoreTowin = 50;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 0;
        blind = false;
        CreateOrLoadFile();
       
    }
    public void level8()
    {
        selectedLevel = levl = 8;
        level = "lvl8";
        countDown = 59;
        scoreTowin = 55;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 0;
        blind = false;
        CreateOrLoadFile();

    }
    public void level9()
    {
        selectedLevel = levl = 9;
        level = "lvl9";
        countDown = 59;
        scoreTowin = 65;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 0;
        blind = true;
        CreateOrLoadFile();
    }
    public void level10()
    {
        selectedLevel = levl = 10;
        level = "lvl10";
        countDown = 59;
        scoreTowin = 65;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = true;
        speed = 0;
        blind = false;
        CreateOrLoadFile();
 
    }

    public void level11()
    {
        selectedLevel = levl = 11;
        level = "lvl11";
        countDown = 40;
        scoreTowin = 20;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = false;
        obstecl2 = false;
        speed = 5;
        blind = true;
        CreateOrLoadFile();
     }
    public void level12()
    {
        selectedLevel = levl = 12;
        level = "lvl12";
        countDown = 40;
        scoreTowin = 25;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = false;
        obstecl2 = false;
        speed = 5;
        blind = false;
        CreateOrLoadFile();
     }
    public void level13()
    {
        selectedLevel = levl = 13;
        level = "lvl3";
        countDown = 40;
        scoreTowin = 30;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = true;
        obstecl2 = false;
        speed = 5;
        blind = false;
        CreateOrLoadFile();
    }
    public void level14()
    {
        selectedLevel = levl = 14;
        level = "lvl14";
        countDown = 40;
        scoreTowin = 35;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = false;
        obstecl2 = false;
        speed = 5;
        blind = false;
        CreateOrLoadFile();
 
    }
    public void level15()
    {
        selectedLevel = levl = 15;
        level = "lvl15";
        countDown = 40;
        scoreTowin = 40;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = true;
        obstecl2 = false;
        speed = 5;
        blind = false;
        CreateOrLoadFile();
        }
    public void level16()
    {
        selectedLevel = levl = 16;
        level = "lvl16";
        countDown = 40;
        scoreTowin = 45;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 5;
        blind = false;
        CreateOrLoadFile();
        
    }
    public void level17()
    {
        selectedLevel = levl = 17;
        level = "lvl17";
        countDown = 40;
        scoreTowin = 50;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 5;
        blind = false;
        CreateOrLoadFile();
     }
    public void level18()
    {
        selectedLevel = levl = 18;
        level = "lvl18";
        countDown = 40;
        scoreTowin = 55;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 5;
        blind = false;
        CreateOrLoadFile();
     }
    public void level19()
    {
        selectedLevel = levl = 19;
        level = "lvl19";
        countDown = 40;
        scoreTowin = 60;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 0;
        blind = true;
        CreateOrLoadFile();
     }

    public void level20()
    {
        selectedLevel = levl = 20;
        level = "lvl20";
        countDown = 40;
        scoreTowin = 65;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = true;
        speed = 5;
        blind = false;
        CreateOrLoadFile();
     }
    public void level21()
    {
        selectedLevel = levl =21;
        level = "lvl21";
        countDown = 59;
        scoreTowin = 20;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 10;
        blind = false;
        CreateOrLoadFile();
 
    }
    public void level22()
    {
        selectedLevel = levl = 22;
        level = "lvl22";
        countDown = 59;
        scoreTowin = 25;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 10;
        blind = false;
        CreateOrLoadFile();
 
    }
    public void level23()
    {
        selectedLevel = levl = 23;
        level = "lvl23";
        countDown = 59;
        scoreTowin = 30;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 10;
        blind = true;
        CreateOrLoadFile();
     }
    public void level24()
    {
        selectedLevel = levl = 24;
        level = "lvl24";
        countDown = 59;
        scoreTowin = 35;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 10;
        blind = false;
        CreateOrLoadFile();
     }
    public void level25()
    {
        selectedLevel = levl = 25;
        level = "lvl25";
        countDown = 59;
        scoreTowin = 40;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 10;
        blind = false;
        CreateOrLoadFile();
     }
    public void level26()
    {
        selectedLevel = levl = 26;
        level = "lvl26";
        countDown = 59;
        scoreTowin = 45;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed =10;
        blind = false;
        CreateOrLoadFile();
     }
    public void level27()
    {
        selectedLevel = levl = 27;
        level = "lvl27";
        countDown = 59;
        scoreTowin = 50;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 10;
        blind = false;
        CreateOrLoadFile();
     }
    public void level28()
    {
        selectedLevel = levl = 28;
        level = "lvl28";
        countDown = 59;
        scoreTowin = 55;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 10;
        blind = false;
        CreateOrLoadFile();
     }
    public void level29()
    {
        selectedLevel = levl = 29;
        level = "lvl29";
        countDown = 59;
        scoreTowin = 60;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 10;
        blind = true;
        CreateOrLoadFile();
     }
    public void level30()
    {
        selectedLevel = levl = 30;
        level = "lvl30";
        countDown = 59;
        scoreTowin = 65;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = true;
        speed = 10;
        blind = false;
        CreateOrLoadFile();
     }

    public void level31()
    {
        selectedLevel = levl = 31;
        level = "lvl31";
        countDown = 40;
        scoreTowin = 20;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 15;
        blind = true;
        CreateOrLoadFile();
     }
    public void level32()
    {
        selectedLevel = levl = 32;
        level = "lvl32";
        countDown = 40;
        scoreTowin = 20;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 15;
        blind = false;
        CreateOrLoadFile();
     }
    public void level33()
    {
        selectedLevel = levl = 33;
        level = "lvl33";
        countDown = 59;
        scoreTowin = 30;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 15;
        blind = false;
        CreateOrLoadFile();
     }
    public void level34()
    {
        selectedLevel = levl = 34;
        level = "lvl34";
        countDown = 40;
        scoreTowin = 35;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 15;
        blind = true;
        CreateOrLoadFile();
     }
    public void level35()
    {
        selectedLevel = levl = 35;
        level = "lvl35";
        countDown = 40;
        scoreTowin = 40;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 0;
        blind = false;
        CreateOrLoadFile();
     }
    public void level36()
    {
        selectedLevel = levl = 36;
        level = "lvl36";
        countDown = 40;
        scoreTowin = 45;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = false;
        obstecl2 = true;
        speed =15;
        blind = false;
        CreateOrLoadFile();
     }
    public void level37()
    {
        selectedLevel = levl = 37;
        level = "lvl37";
        countDown = 40;
        scoreTowin = 50;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 15;
        blind = false;
        CreateOrLoadFile();
     }
    public void level38()
    {
        selectedLevel = levl = 38;
        level = "lvl38";
        countDown = 40;
        scoreTowin = 55;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 15;
        blind = false;
        CreateOrLoadFile();
     }
    public void level39()
    {
        selectedLevel = levl = 39;
        level = "lvl39";
        countDown = 40;
        scoreTowin = 60;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 15;
        blind = true;
        CreateOrLoadFile();
     }
    public void level40()
    {
        selectedLevel = levl = 40;
        level = "lvl40";
        countDown = 40;
        scoreTowin = 65;
        ballons = 20;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = true;
        speed = 15;
        blind = false;
        CreateOrLoadFile();
     }

    public void level41()
    {
        selectedLevel = levl = 41;
        level = "lvl41";
        countDown = 59;
        scoreTowin = 20;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 20;
        blind = false;
        CreateOrLoadFile();
     }
    public void level42()
    {
        selectedLevel = levl = 42;
        level = "lvl42";
        countDown = 59;
        scoreTowin = 30;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed =20;
        blind = false;
        CreateOrLoadFile();
     }
    public void level43()
    {
        selectedLevel = levl = 43;
        level = "lvl43";
        countDown = 59;
        scoreTowin = 30;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 20;
        blind = false;
        CreateOrLoadFile();
     }
    public void level44()
    {
        selectedLevel = levl = 44;
        level = "lvl44";
        countDown = 59;
        scoreTowin = 35;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = false;
        obstecl2 = false;
        speed = 20;
        blind = false;
        CreateOrLoadFile();
     }
    public void level45()
    {
        selectedLevel = levl = 45;
        level = "lvl45";
        countDown = 59;
        scoreTowin = 40;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 20;
        blind = false;
        CreateOrLoadFile();
     }
    public void level46()
    {
        selectedLevel = levl = 46;
        level = "lvl46";
        countDown = 59;
        scoreTowin = 45;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 20;
        blind = false;
        CreateOrLoadFile();
     }
    public void level47()
    {
        selectedLevel = levl = 47;
        level = "lvl47";
        countDown = 59;
        scoreTowin = 50;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 20;
        blind = false;
        CreateOrLoadFile();
     }
    public void level48()
    {
        selectedLevel = levl = 48;
        level = "lvl48";
        countDown = 59;
        scoreTowin = 55;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = false;
        obstecl2 = true;
        speed = 20;
        blind = false;
        CreateOrLoadFile();
     }
    public void level49()
    {
        selectedLevel = levl = 49;
        level = "lvl49";
        countDown = 59;
        scoreTowin = 60;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 20;
        blind = true;
        CreateOrLoadFile();
     }
    public void level50()
    {
        selectedLevel = levl = 50;
        level = "lvl50";
        countDown = 59;
        scoreTowin = 20;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = true;
        obstecl2 = true;
        speed = 20;
        blind = false;
        CreateOrLoadFile();
     }

    public void level51()
    {
        selectedLevel = levl = 51;
        level = "lvl51";
        countDown =40;
        scoreTowin = 20;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 25;
        blind = true;
        CreateOrLoadFile();
     }
    public void level52()
    {
        selectedLevel = levl = 52;
        level = "lvl52";
        countDown = 40;
        scoreTowin = 25;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 25;
        blind = false;
        CreateOrLoadFile();
     }
    public void level53()
    {
        selectedLevel = levl = 53;
        level = "lvl53";
        countDown = 40;
        scoreTowin = 30;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 25;
        blind = false;
        CreateOrLoadFile();
     }
    public void level54()
    {
        selectedLevel = levl = 54;
        level = "lvl54";
        countDown = 40;
        scoreTowin = 35;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 25;
        blind = true;
        CreateOrLoadFile();
     }
    public void level55()
    {
        selectedLevel = levl = 55;
        level = "lvl55";
        countDown = 40;
        scoreTowin = 40;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = true;
        obstecl2 = false;
        speed = 25;
        blind = false;
        CreateOrLoadFile();
     }
    public void level56()
    {
        selectedLevel = levl = 56;
        level = "lvl56";
        countDown = 40;
        scoreTowin = 45;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 25;
        blind = false;
        CreateOrLoadFile();
     }
    public void level57()
    {
        selectedLevel = levl = 57;
        level = "lvl57";
        countDown = 40;
        scoreTowin = 50;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 25;
        blind = false;
        CreateOrLoadFile();
     }
    public void level58()
    {
        selectedLevel = levl = 58;
        level = "lvl58";
        countDown = 40;
        scoreTowin = 55;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 25;
        blind = false;
        CreateOrLoadFile();
     }
    public void level59()
    {
        selectedLevel = levl = 59;
        level = "lvl59";
        countDown = 40;
        scoreTowin = 60;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 25;
        blind = true;
        CreateOrLoadFile();
     }
    public void level60()
    {
        selectedLevel = levl = 60;
        level = "lvl60";
        countDown = 40;
        scoreTowin = 65;
        ballons = 15;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = true;
        speed = 25;
        blind = false;
        CreateOrLoadFile();
     }

    public void level61()
    {
        selectedLevel = levl = 61;
        level = "lvl61";
        countDown = 59;
        scoreTowin = 20;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 30;
        blind = false;
        CreateOrLoadFile();
     }
    public void level62()
    {
        selectedLevel = levl = 62;
        level = "lvl62";
        countDown = 59;
        scoreTowin = 25;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 30;
        blind = false;
        CreateOrLoadFile();
     }
    public void level63()
    {
        selectedLevel = levl = 63;
        level = "lvl63";
        countDown = 59;
        scoreTowin = 30;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 30;
        blind = true;
        CreateOrLoadFile();
     }
    public void level64()
    {
        selectedLevel = levl = 64;
        level = "lvl64";
        countDown = 59;
        scoreTowin = 35;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 30;
        blind = false;
        CreateOrLoadFile();
     }
    public void level65()
    {
        selectedLevel = levl = 65;
        level = "lvl65";
        countDown = 59;
        scoreTowin = 40;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 30;
        blind = false;
        CreateOrLoadFile();
     }
    public void level66()
    {
        selectedLevel = levl = 66;
        level = "lvl66";
        countDown = 59;
        scoreTowin = 45;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 30;
        blind = false;
        CreateOrLoadFile();
     }
    public void level67()
    {
        selectedLevel = levl = 67;
        level = "lvl67";
        countDown = 59;
        scoreTowin = 60;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 30;
        blind = false;
        CreateOrLoadFile();
     }
    public void level68()
    {
        selectedLevel = levl = 68;
        level = "lvl68";
        countDown = 59;
        scoreTowin = 55;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 30;
        blind = false;
        CreateOrLoadFile();
     }
    public void level69()
    {
        selectedLevel = levl = 69;
        level = "lvl69";
        countDown = 59;
        scoreTowin = 60;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 30;
        blind = true;
        CreateOrLoadFile();
     }
    public void level70()
    {
        selectedLevel = levl = 70;
        level = "lvl70";
        countDown = 59;
        scoreTowin = 65;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 =true;
        obstecl2 = true;
        speed = 30;
        blind = false;
        CreateOrLoadFile();
     }

    public void level71()
    {
        selectedLevel = levl = 71;
        level = "lvl71";
        countDown =40;
        scoreTowin = 20;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 35;
        blind = true;
        CreateOrLoadFile();
     }
    public void level72()
    {
        selectedLevel = levl = 72;
        level = "lvl72";
        countDown = 40;
        scoreTowin = 25;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 35;
        blind = false;
        CreateOrLoadFile();
     }
    public void level73()
    {
        selectedLevel = levl = 73;
        level = "lvl73";
        countDown = 30;
        scoreTowin = 30;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 35;
        blind = false;
        CreateOrLoadFile();
     }
    public void level74()
    {
        selectedLevel = levl = 74;
        level = "lvl74";
        countDown = 40;
        scoreTowin = 35;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 35;
        blind = true;
        CreateOrLoadFile();
     }
    public void level75()
    {
        selectedLevel = levl = 75;
        level = "lvl75";
        countDown = 40;
        scoreTowin = 40;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 35;
        blind = false;
        CreateOrLoadFile();
     }
    public void level76()
    {
        selectedLevel = levl = 76;
        level = "lvl76";
        countDown = 40;
        scoreTowin = 45;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 35;
        blind = false;
        CreateOrLoadFile();
     }
    public void level77()
    {
        selectedLevel = levl = 77;
        level = "lvl77";
        countDown = 40;
        scoreTowin = 50;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 35;
        blind = false;
        CreateOrLoadFile();
     }
    public void level78()
    {
        selectedLevel = levl = 78;
        level = "lvl78";
        countDown = 40;
        scoreTowin = 55;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 35;
        blind = false;
        CreateOrLoadFile();
     }
    public void level79()
    {
        selectedLevel = levl = 79;
        level = "lvl79";
        countDown = 40;
        scoreTowin = 60;
        ballons = 10;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 35;
        blind = true;
        CreateOrLoadFile();
     }
    public void level80()
    {
        selectedLevel = levl = 80;
        level = "lvl80";
        countDown = 40;
        scoreTowin = 65;
        ballons = 40;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = true;
        speed = 35;
        blind = false;
        CreateOrLoadFile();
     }

    public void level81()
    {
        selectedLevel = levl = 81;
        level = "lvl81";
        countDown = 59;
        scoreTowin = 20;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 40;
        blind = false;
        CreateOrLoadFile();
     }
    public void level82()
    {
        selectedLevel = levl = 82;
        level = "lvl82";
        countDown = 59;
        scoreTowin = 25;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 40;
        blind = false;
        CreateOrLoadFile();
     }
    public void level83()
    {
        selectedLevel = levl = 83;
        level = "lvl83";
        countDown = 59;
        scoreTowin = 30;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 40;
        blind = true;
        CreateOrLoadFile();
     }
    public void level84()
    {
        selectedLevel = levl =84;
        level = "lvl84";
        countDown = 59;
        scoreTowin = 35;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 40;
        blind = false;
        CreateOrLoadFile();
     }
    public void level85()
    {
        selectedLevel = levl = 85;
        level = "lvl85";
        countDown = 40;
        scoreTowin = 40;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 40;
        blind = false;
        CreateOrLoadFile();
     }
    public void level86()
    {
        selectedLevel = levl = 86;
        level = "lvl86";
        countDown = 59;
        scoreTowin = 45;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = false;
        obstecl1 = false;
        obstecl2 = true;
        speed = 40;
        blind = false;
        CreateOrLoadFile();
     }
    public void level87()
    {
        selectedLevel = levl = 87;
        level = "lvl87";
        countDown = 59;
        scoreTowin = 50;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 40;
        blind = false;
        CreateOrLoadFile();
     }
    public void level88()
    {
        selectedLevel = levl = 88;
        level = "lvl88";
        countDown = 59;
        scoreTowin = 55;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 40;
        blind = false;
        CreateOrLoadFile();
     }
    public void level89()
    {
        selectedLevel = levl = 89;
        level = "lvl89";
        countDown = 59;
        scoreTowin = 60;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 40;
        blind = true;
        CreateOrLoadFile();
     }
    public void level90()
    {
        selectedLevel = levl = 90;
        level = "lvl90";
        countDown = 59;
        scoreTowin = 65;
        ballons =5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = true;
        speed = 40;
        blind = false;
        CreateOrLoadFile();
     }

    public void level91()
    {
        selectedLevel = levl = 91;
        level = "lvl91";
        countDown = 40;
        scoreTowin = 20;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 45;
        blind = true;
        CreateOrLoadFile();
     }
    public void level92()
    {
        selectedLevel = levl = 92;
        level = "lvl92";
        countDown = 40;
        scoreTowin = 25;
        ballons =5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 45;
        blind = false;
        CreateOrLoadFile();
     }
    public void level93()
    {
        selectedLevel = levl = 93;
        level = "lvl93";
        countDown =40;
        scoreTowin = 30;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 45;
        blind = false;
        CreateOrLoadFile();
     }
    public void level94()
    {
        selectedLevel = levl = 94;
        level = "lvl94";
        countDown = 40;
        scoreTowin = 35;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 45;
        blind = true;
        CreateOrLoadFile();
     }
    public void level95()
    {
        selectedLevel = levl = 95;
        level = "lvl95";
        countDown = 40;
        scoreTowin =40;
        ballons = 25;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 45;
        blind = false;
        CreateOrLoadFile();
     }
    public void level96()
    {
        selectedLevel = levl = 96;
        level = "lvl96";
        countDown = 40;
        scoreTowin = 45;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 45;
        blind = false;
        CreateOrLoadFile();
     }
    public void level97()
    {
        selectedLevel = levl = 97;
        level = "lvl97";
        countDown = 40;
        scoreTowin =50;
        ballons = 4;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = false;
        speed = 45;
        blind = false;
        CreateOrLoadFile();
     }
    public void level98()
    {
        selectedLevel = levl = 98;
        level = "lvl98";
        countDown = 40;
        scoreTowin = 55;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = true;
        speed = 45;
        blind = false;
        CreateOrLoadFile();
     }
    public void level99()
    {
        selectedLevel = levl = 99;
        level = "lvl99";
        countDown = 40;
        scoreTowin = 60;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = false;
        obstecl2 = false;
        speed = 45;
        blind = true;
        CreateOrLoadFile();
     }
    public void level100()
    {
        selectedLevel = levl = 100;
        level = "lvl100";
        countDown = 40;
        scoreTowin = 65;
        ballons = 5;
        specialBallons = 4;
        PowerBallon = true;
        obstecl1 = true;
        obstecl2 = true;
        speed = 45;
        blind = false;
        CreateOrLoadFile();
     }
}


