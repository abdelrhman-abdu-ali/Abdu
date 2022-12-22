using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Advertisements;
using System;

public class GameManger : MonoBehaviour
{
    public GameObject Circel, Knife,SpecialKnife, line, piontTOthrow, dragger, obstacles, background, knifeanime, knifeanimeSpecial;
    public GameObject soundThrow, circelsound, obstaclessound,Sounds;
    public GameObject ballon, ballonspawner, specialballon,ballonBoom,powerBallon,powerBallonPositon,obs1,obs2;
    public bool powerBallonDeoplyer = false;
    public GameObject Blind;
    bool play = true;
    GameObject newKnife, newKnife1, newKnife2;
    Vector3 draggerReset, lineReset, lineScaler;
    Vector3 PointReseter;
    GameObject knifeChild;
    Vector3 point = new Vector3();
    bool instantiater = false;
    bool instantiater1 = false;
    bool instantiater2 = false;
    bool instantiater3 = false;
    bool instantiater4 = false;
    bool instantiateSknife = false;
    bool GotShot = false;
    GameObject knifeanimeclone;
    GameObject ballonclone, ballonclone2, ballonclone3;
    Vector3 knifescale = new Vector3();
    int[] ballonPOs = new int[50];
    [Range(0, 44)]
    public int balloons = 0;
    [Range(0, 12)]
    public int Specialballoons = 0;
    public float speed;
    public bool balloonChecker, SballoonChecker;
    int counterValue;
    int thislevel;
    int scoretowinlevel;
    Sprite sprite;
    int selected=3;
    public int checkKnife;
    [Space(20)]
    [Header("Charcter'sGrips")]
    public GameObject rightarmgrip;
    public GameObject lefttarmgrip;
    public GameObject lefttleggrip;
    public GameObject righttleggrip;
    /// <Levels>
    public GameObject levels;
    /// </Levels>
    ////////////UI////////////////////////////////////
    [Space(10)]
    [Header("UI.Element")]
    public TextMeshProUGUI time;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Scoreanime;
    public TextMeshProUGUI Knives;
    public TextMeshProUGUI peakstext;
    public Text level;
    public Text txt_scoretowin;
    public int peaksno;
    public int scoreBalloon;
    public  int scoreSpecialBalloon;
    int totlascore;
    int totalKnive=0 ;
    int onlyoneBallon;
    public GameObject pauseMenu,WinMenu,LoseMenu,scorenimeGoal;
   public int gameStates=0;
   public AdManager ads;
    /////////////////////////////////////////
    public GameObject TrajectoryPointPrefeb;
    public GameObject BallPrefb;
    //Vector3 vel;
    //float angle = 0;
    private GameObject ball;
    private bool isPressed=false;
    private bool  isBallThrown;
    private float power = 25;
    private int numOfTrajectoryPoints = 10;
    private List<GameObject> trajectoryPoints;
    private void Awake()
    {
        trajectoryPoints = new List<GameObject>();
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {


            GameObject dot = (GameObject)Instantiate(TrajectoryPointPrefeb);
            trajectoryPoints.Insert(i, dot);
        }
        isPressed = false;
        isBallThrown = false;
        gameStates = 0;
        //vel = new Vector3();

        setLevel();
        setReward();
        sprite = ballonBoom.GetComponent<SpriteRenderer>().sprite;
        StartCoroutine("StartCounter");
        Application.runInBackground = true;
        obstacles.gameObject.GetComponent<Animation>().Play("obstecleanime");
        totalKnive = totalKnive + checkKnife;
        Knives.text = totalKnive.ToString();
        peakstext.text = peaksno.ToString();
       
    }
    void Start()
    {
        knifescale = Knife.transform.localScale;
        ObjectRester();
        knifeanimeclone = Instantiate(knifeanime, dragger.transform.position, Quaternion.identity);
        ballonSpawnerr1();
        ballonSpawnerr2();
        /////////////////////////////
        //   TrajectoryPoints are instatiated
        

    }
 
    private void FixedUpdate()
    {
        if (gameStates == 0)
        {
            Circel.transform.eulerAngles += new Vector3(0, 0, speed * Time.deltaTime);
            obstacles.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
            if (obstacles.transform.position.x > 5)
            {
                obstacles.transform.position = new Vector3(-4.6f, obstacles.transform.position.y, obstacles.transform.position.z);
            }
            peakstext.text = peaksno.ToString();
            Knives.text = checkKnife.ToString();

        }

        if (rightarmgrip.activeSelf == false && lefttarmgrip.activeSelf == false && lefttleggrip.activeSelf == false && righttleggrip.activeSelf == false)
        {
            selected = 3;
        }
        spawn();
        SpawnSpecial();
        //if (!ballonclone&&!ballonclone2)
        //{
        //    ballonSpawnerCheck();
        //}
        ////Touch touch = Input.GetTouch(0);
        ////Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -5f));
        ////dragger.transform.position = Vector3.Lerp(dragger.transform.position, worldPosition, 60 * Time.deltaTime);


    }
    private void Update()
    {
        Dragging();
       
    }
    private void Dragging ()
    {

        Vector3 vel;
        float angle;
       // Touch touch = Input.GetTouch(0);

        //if (Input.touchCount > 0)
        { 
            if (gameStates == 0)
        {
                //Vector3 worldPosition2 = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y,10f));
                Vector3 worldPosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit2 = Physics2D.Raycast(worldPosition2, worldPosition2);
                if (hit2)
                {
                    if (hit2.collider.gameObject.tag == "obs" )  
                    {

                        //////////////////////////////////////////
                        //if (isBallThrown)
                        //{ return; }
                        //Touch touch2 = Input.GetTouch(0);
                        if (Input.GetMouseButton(0))// if (touch.phase == TouchPhase.Moved)
                        {


                            //if (!ball)
                            //{ createBall(); }
                            //isPressed = true;
                           
                            {
                               // vel = GetForceFrom(new Vector3(0f, 0f, 0f), -.09f * Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 2f)));
                                vel = GetForceFrom(new Vector3(0f, 0f, 0f), -.09f * Camera.main.ScreenToWorldPoint(Input.mousePosition));
                                angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg ;
                                TrajectoryPointPrefeb.transform.eulerAngles = new Vector3(0, 0, angle);
                                setTrajectoryPoints(TrajectoryPointPrefeb.transform.position, vel);
                            }
                               

                            ////Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -5f));
                            ////dragger.transform.position = Vector3.Lerp(dragger.transform.position, worldPosition, 60 * Time.deltaTime);
                            if (play)
                            {
                                Sounds.gameObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();
                                Sounds.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Play();

                                play = false;
                            }
                            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            dragger.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
                        }
                        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                       
                        //if (Input.GetMouseButtonDown(0) && !play)
                        //{
                        //    vel = new Vector3(0, 0, 0);
                        //    angle = 0f;
                        //    //sceneRestart();
                        //}
                        //if ((Input.GetTouch(0).phase == TouchPhase.Ended && isPressed) || (Input.GetTouch(0).phase == TouchPhase.Canceled && isPressed))
                        if(Input.GetMouseButtonUp(0))
                        {
                            Sounds.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Stop();
                            Sounds.gameObject.transform.GetChild(5).gameObject.GetComponent<AudioSource>().Play();
                            play = true;
                            dragger.transform.position = draggerReset;
                            //   point = piontTOthrow.transform.position;
                            point = trajectoryPoints[9].gameObject.transform.position;
                            anime();
                            instantiater = true;
                            piontTOthrow.transform.position = PointReseter;
                            isPressed = false;
                            //vel = new Vector3();
                            for (int i = 0; i < numOfTrajectoryPoints; i++)
                            {
                                trajectoryPoints[i].gameObject.transform.position = TrajectoryPointPrefeb.gameObject.transform.position;
                            }

                            isPressed = false;
                            isBallThrown = false;
                        }
                    }

                }
                

                // when mouse button is pressed, cannon is rotated as per mouse movement and projectile trajectory path is displayed.

                //ProggressSystem();

            }
        }
    }
    public void setLevel()
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
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        balloons = (int)bf_ballons.Deserialize(file);
        Specialballoons = (int)bf_Specialballons.Deserialize(file);
        counterValue = (int)bf_countDown.Deserialize(file);
        powerBallonDeoplyer = (bool)bf_PowerBallon.Deserialize(file);
        if (powerBallonDeoplyer)
        {
            deployPwoerBallon();
        }
        Blind.gameObject.SetActive((bool)bf_blind.Deserialize(file));
        speed = (float)bf_speed.Deserialize(file);
        obs1.gameObject.SetActive((bool)bf_obstecl1.Deserialize(file));
        obs2.gameObject.SetActive((bool)bf_obstecl2.Deserialize(file));
        level.text= (string)bf_levels.Deserialize(file);
        scoretowinlevel = (int)bf_scoreTowin.Deserialize(file);
        txt_scoretowin.text= txt_scoretowin.text+scoretowinlevel.ToString(); 
        thislevel = (int)bf_levl.Deserialize(file);
        file.Close();
       

    }
    //public void ThrowObjectLine()
    //{
        

    //    //RaycastHit2D hitDragger = Physics2D.Raycast(worldPosition, worldPosition);
    //    //if (hitDragger)
    //    {
    //        //if (hitDragger.collider.gameObject.tag=="Finish")
    //        if (Input.GetMouseButton(0))
    //        {
    //            {
    //            if (dragger.transform.hasChanged == true)
    //            {
    //                       // dragger.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);

    //                        //float y = Input.GetAxis("Mouse Y") * 0.1f * Time.deltaTime;
    //                        //float Z = Input.GetAxis("Mouse X") * 10f * Time.deltaTime;
    //                        //Vector3 scale = line.transform.localScale;
    //                        //Vector3 euler = line.transform.localEulerAngles;
    //                        //float y= dragger.transform.position.y * 0.1f * Time.deltaTime;
    //                        //float Z = dragger.transform.position.x * 10f * Time.deltaTime;
    //                        //Vector3 scale = line.transform.localScale;
    //                        //Vector3 euler = line.transform.localEulerAngles;
    //                        //scale.y += y/2;
    //                        //euler.z += Z/2;
    //                        //line.transform.localScale = scale;
    //                        //line.transform.eulerAngles = euler;
    //                        float x = dragger.transform.position.x * Time.deltaTime * -2.7f;
    //                        float y = dragger.transform.position.y * Time.deltaTime * -0.7f;
    //                        Vector3 pos = piontTOthrow.gameObject.transform.position;
    //                        pos.x += x;
    //                        pos.y += y;
    //                        //piontTOthrow.gameObject.transform.position= pos;
    //                        piontTOthrow.gameObject.transform.position = Vector3.LerpUnclamped(piontTOthrow.transform.position, pos, 60 * Time.deltaTime);
    //                }
    //        }
    //        if (play)
    //        {
    //            Sounds.gameObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();
    //            Sounds.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Play();

    //            play = false;
    //        }
            


    //    }
    //    if (Input.GetMouseButtonUp(0)&& piontTOthrow.transform.position.y > -1.8 && piontTOthrow.transform.position.y < 4 && piontTOthrow.transform.position.x < 2.3 && piontTOthrow.transform.position.x > -2.3)
    //    {
    //        {
    //            point = piontTOthrow.transform.position;
    //            anime();
    //            instantiater = true;
    //            dragger.transform.position = draggerReset;
    //            line.transform.eulerAngles = lineReset;
    //            line.transform.localScale = lineScaler;
    //            piontTOthrow.transform.position = PointReseter;
    //            Sounds.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Stop();
    //            Sounds.gameObject.transform.GetChild(5).gameObject.GetComponent<AudioSource>().Play();
    //        }
    //        play = true;
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        dragger.transform.position = draggerReset;
    //        line.transform.eulerAngles = lineReset;
    //        line.transform.localScale = lineScaler;
    //        piontTOthrow.transform.position = PointReseter;
    //        Sounds.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Stop();

    //    }
    //    }
    //}

    //public void ThrowObjectLineTouch()
    //{
    //    Touch touch = Input.GetTouch(0);
    //    {
    //        if (Input.touchCount > 0)
    //        {

    //            Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
    //            if (touchedPos.x < 1 && touchedPos.x > -1
    //              && touchedPos.y < -3.445 && touchedPos.y > -5.5 )
    //            {
                   
    //                if (dragger.transform.hasChanged == true)
    //                {
    //                    dragger.transform.position = Vector3.Lerp(dragger.transform.position, touchedPos, 60 * Time.deltaTime);
    //                    Vector2 touchPos = new Vector2(touch.position.x, touch.position.y);

    //                    float y = Vector2.Distance(touchedPos, draggerReset) * -5.0f * Time.deltaTime;
    //                    float Z = (draggerReset.x - touchedPos.x) * -25;
    //                    Vector3 scale = line.transform.localScale;
    //                    scale.y = lineScaler.y + y;
    //                    Vector3 euler = line.transform.localEulerAngles;
    //                    euler.z = lineReset.z+Z;
    //                    line.transform.localScale = scale;
    //                    line.transform.eulerAngles = euler;
    //                }
    //            }
    //        }
    //        if (play)
    //        {
    //            Sounds.gameObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();
    //            Sounds.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Play();

    //            play = false;
    //        }
    //    }
    //    if (Input.touches[0].phase == TouchPhase.Ended&&piontTOthrow.transform.position.y>-1.8 && piontTOthrow.transform.position.y < 4 && piontTOthrow.transform.position.x<2.3 && piontTOthrow.transform.position.x > -2.3)
    //    {

    //        {
    //            point = piontTOthrow.transform.position;
    //            anime();
    //            instantiater = true;
    //            dragger.transform.position = draggerReset;
    //            line.transform.eulerAngles = lineReset;
    //            line.transform.localScale = lineScaler;
    //            Sounds.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Stop();
    //            Sounds.gameObject.transform.GetChild(5).gameObject.GetComponent<AudioSource>().Play();
            
    //        play = true;
    //    }


    //    }
    //    if (Input.touches[0].phase == TouchPhase.Ended)
    //    {

    //        dragger.transform.position = draggerReset;
    //        line.transform.eulerAngles = lineReset;
    //        line.transform.localScale = lineScaler;
    //        Sounds.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Stop();

    //    }

    //}

    public void ObjectRester()
    {
        draggerReset = new Vector3();
        lineReset = new Vector3();
        lineScaler = new Vector3();
        PointReseter = new Vector3();
        draggerReset = dragger.transform.position;
        lineReset = line.transform.eulerAngles;
        lineScaler = line.transform.localScale;
        PointReseter = piontTOthrow.transform.position;
    }

    public void colider()
    {
        RaycastHit2D hit = Physics2D.Raycast(knifeChild.transform.position, knifeChild.transform.TransformDirection(Vector2.zero), 2f);
        if (hit)
        {
            if (hit.collider.tag == "3adawy")
            {
                Debug.Log("hit");
                knifeChild.transform.SetParent(hit.collider.transform); 
                Sounds.gameObject.transform.GetChild(10).gameObject.GetComponent<AudioSource>().Play();
                gripEreraser();
            }
            if (hit.collider.tag == "Circel")
            {
                knifeChild.GetComponent<SpriteRenderer>().sortingOrder = 3;

                knifeChild.transform.SetParent(Circel.transform);
              //  knifeChild.transform.localScale = knifescale;
                circelsound.GetComponent<AudioSource>().Play();

            }
            if (hit.collider.tag == "obs")
            {
                knifeChild.transform.SetParent(hit.transform);
                obstaclessound.GetComponent<AudioSource>().Play();
                Debug.Log("hitobs");
                knifeChild.GetComponent<SpriteRenderer>().sortingOrder = 13;
            }
            if (hit.collider.tag == "BackGround")
            {
                knifeChild.transform.SetParent(background.transform);

            }
            if (hit.collider.tag == "ballon")
            {
                GameObject boom = Instantiate(ballonBoom, hit.collider.gameObject.transform.position,Quaternion.identity);
                boom.gameObject.transform.position = hit.collider.gameObject.transform.position;
                boom.gameObject.transform.SetParent(hit.collider.gameObject.transform);
               // hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                knifeChild.transform.SetParent(Circel.transform);
                totlascore = totlascore+scoreBalloon;
                Score.text = totlascore.ToString();
                Scoreanime.text = "+"+scoreBalloon.ToString();
                Scoreanime.gameObject.transform.DOLocalMove(scorenimeGoal.transform.localPosition, 1f);
                Invoke("resetScoreAnime", 1f);
                Destroy(hit.collider.gameObject, .5f);
                Sounds.gameObject.transform.GetChild(6).gameObject.GetComponent<AudioSource>().Play();
            }
            if (hit.collider.tag == "Ballon2")
            {
                GameObject boom = Instantiate(ballonBoom, hit.collider.gameObject.transform.position, Quaternion.identity);
                boom.gameObject.transform.position = hit.collider.gameObject.transform.position;
                boom.gameObject.transform.SetParent(hit.collider.gameObject.transform);
                hit.collider.gameObject.GetComponent<Animator>().speed=0;
                knifeChild.transform.SetParent(Circel.transform);
                totlascore = totlascore + scoreSpecialBalloon;
                Score.text = totlascore.ToString();
                Scoreanime.text = "+" + scoreSpecialBalloon.ToString();
                Scoreanime.gameObject.transform.DOLocalMove(scorenimeGoal.transform.localPosition, 1f);
                Invoke("resetScoreAnime", 1f);
                Destroy(hit.collider.gameObject, .5f);
                Sounds.gameObject.transform.GetChild(7).gameObject.GetComponent<AudioSource>().Play();

            }
            if (hit.collider.tag == "ballon3")
            {
                Debug.Log("asd");
                Destroy(hit.collider.gameObject, .5f);
                GameObject boom = Instantiate(ballonBoom, hit.collider.gameObject.transform.position, Quaternion.identity);
                boom.gameObject.transform.position = hit.collider.gameObject.transform.position;
                boom.gameObject.transform.SetParent(hit.collider.gameObject.transform);
                knifeChild.transform.SetParent(Circel.transform);
                //hit.collider.gameObject.transform.localScale = new Vector3(1f,1f,1f);
                Sounds.gameObject.transform.GetChild(7).gameObject.GetComponent<AudioSource>().Play();
                if (Blind.gameObject.activeSelf == true)
                {
                    peaksno++;

                }
                else { checkKnife++; }
            }

            if (hit.collider.tag == "Knife")
            {
                Destroy(hit.collider.gameObject);

            }

        }
    }

    public void anime()
    {
        knifeanimeclone.transform.DOMove(point, 0.5f).SetEase(Ease.InOutSine);
        knifeanimeclone.GetComponent<Animation>().Play("KnifeanimeStarter");
        soundThrow.GetComponent<AudioSource>().Play();


    }
    public void spawn()
    {
        if (knifeanimeclone)
        {
            if (knifeanimeclone.GetComponent<Animation>().isPlaying == false && instantiater == true)

            {
                ////Normal Knife/////
                if (instantiateSknife == false)
                {
                    knifeChild = Instantiate(Knife, point, Quaternion.identity);
                    colider();
                    Destroy(knifeanimeclone);
                    knifeanimeclone = Instantiate(knifeanime, dragger.transform.position, Quaternion.identity);
                    //checkKnife = 1;
                    instantiater = false;
                    GotShot = true;
                }
                
                ////////////////////
                /// special knife////
               //if (checkKnife>0)
                {
                    if(instantiateSknife==true)
                    {
                        knifeChild = Instantiate(SpecialKnife, point, Quaternion.identity);
                        colider();
                        SpeciKnifePower();
                        Destroy(knifeanimeclone);
                        knifeanimeclone = Instantiate(knifeanime, dragger.transform.position, Quaternion.identity);

                        instantiater = false;
                        instantiateSknife = false;
                        GotShot = true;

                    }


                }


                ///////////////
               // knifeanimeclone.transform.rotation = Quaternion.Euler(63.274f, 2.969f, 2.224f);
                instantiater = false;
            }
        }
    }


    public void ballonSpawnerr1()
    {
       
        List<int> x = new List<int>();
        for (int j=0;j<45;j++)
        {
            x.Add(-1);
        }
        for (int i = 0; i < balloons; i++)
        {
            onlyoneBallon = 0;
            onlyoneBallon = UnityEngine.Random.Range(0, (x.Count));
            while(x.Contains(onlyoneBallon))
            {
                onlyoneBallon = UnityEngine.Random.Range(0, x.Count);
            }
            x[i] = onlyoneBallon;
            ballonclone = Instantiate(ballon, ballonspawner.gameObject.transform.GetChild(onlyoneBallon).transform.position, Quaternion.identity);
            ballonclone.transform.SetParent(Circel.transform);
            ballonPOs[i] = onlyoneBallon; 
        }
        balloonChecker = false;

    }
    public void ballonSpawnerr2()
    {
        List<int> x = new List<int>();
        for (int j = 45; j < 56; j++)
        {
            x.Add(j);
        }
        for (int i = 0; i < Specialballoons; i++)
        {
            int onlyone = x[UnityEngine.Random.Range(0, 10)];
            ballonclone2 = Instantiate(specialballon, ballonspawner.gameObject.transform.GetChild(onlyone).transform.position, Quaternion.identity);
            ballonclone2.transform.SetParent(Circel.transform);
            x.Remove(onlyone);
        }
        SballoonChecker = false;

    }

    public void ballonSpawnerCheck()
    {
        if (!balloonChecker)
        {
            balloonChecker = true;
            ballonSpawnerr1();

        }
        if (!ballonclone2)
        {
            SballoonChecker = true;

            ballonSpawnerr2();
        }
    }

    public void deployPwoerBallon()
    {
        ballonclone3 = Instantiate(powerBallon, powerBallonPositon.transform.position, Quaternion.identity);
        ballonclone3.gameObject.transform.SetParent(Circel.transform);
       ballonclone3.gameObject.transform.DOPunchPosition(new Vector3(0f,-10f,0f), 10f,1,10f).SetLoops(-1);
    }
    public void ProggressSystem ()
    {
        //if (counterValue > 1&& counterValue < 10)
        //{
        //    if (speed > -30)
        //    {
        //        speed--;
        //    }
        //}
        if (counterValue > 1 )
        {
            if (speed <30)
            {
                speed=speed+1;
            }
        }

    }
    public void gripEreraser()
    {
        GameObject[] list;
        list = new GameObject[4];
        list[0]=(rightarmgrip);
        list[1]=(lefttarmgrip);
        list[2]=(righttleggrip);
        list[3]=(lefttleggrip);
        List<int> destroyerr = new List<int>();
        for (int j = 0; j < 4; j++)
        {
            destroyerr.Add(j);

        }
      

        for (int i = 0; i < destroyerr.Count; i++)
        {
            if (list[selected].activeSelf == true)
            {
                list[selected].SetActive(false);
              //  destroyerr.RemoveRange(0, selected);
            }
            
           
        }
        selected=selected-1;




    }
    public void SpecialKnifeSelector ()
    {
        if (checkKnife > 0&& GotShot)
        {
            checkKnife--;

            instantiateSknife = true;
           //
            Knives.text = checkKnife.ToString();
            Destroy(knifeanimeclone);
            knifeanimeclone = Instantiate(knifeanimeSpecial, dragger.transform.position, Quaternion.identity);
            Debug.Log("xx");
            GotShot = false;
        }
      
    }
    public void SpeciKnifePower()
    {

         newKnife = Instantiate(SpecialKnife, knifeChild.gameObject.transform.position, Quaternion.identity);
         newKnife1 = Instantiate(SpecialKnife, knifeChild.gameObject.transform.position, Quaternion.identity);
         newKnife2 = Instantiate(SpecialKnife, knifeChild.gameObject.transform.position, Quaternion.identity);
       
        //SpecialKnife, ballonspawner.gameObject.transform.GetChild(ballonPOs[2]).transform.position,
        newKnife.transform.DORotate(new Vector3 (0,0,-1400f), .5f);
        newKnife1.transform.DORotate(new Vector3(0, 0, -1400f), .5f);
        newKnife2.transform.DORotate(new Vector3(0, 0, -1400f), .5f);
        newKnife.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), .5f);
        newKnife1.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), .5f);
        newKnife2.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), .5f);
        if (instantiater4 == false)
        {
            newKnife.transform.DOMove(ballonspawner.gameObject.transform.GetChild(ballonPOs[0]).transform.position, .5f).SetEase(Ease.InOutSine);
            newKnife1.transform.DOMove(ballonspawner.gameObject.transform.GetChild(ballonPOs[1]).transform.position, .5f).SetEase(Ease.InOutSine);
            newKnife2.transform.DOMove(ballonspawner.gameObject.transform.GetChild(ballonPOs[2]).transform.position, .5f).SetEase(Ease.InOutSine);
        }
        if (instantiater4 == true)
        {
            newKnife.transform.DOMove(ballonspawner.gameObject.transform.GetChild(ballonPOs[3]).transform.position, .5f).SetEase(Ease.InOutSine);
            newKnife1.transform.DOMove(ballonspawner.gameObject.transform.GetChild(ballonPOs[4]).transform.position, .5f).SetEase(Ease.InOutSine);
            newKnife2.transform.DOMove(ballonspawner.gameObject.transform.GetChild(ballonPOs[5]).transform.position, .5f).SetEase(Ease.InOutSine);
            instantiater4 = false;
        }

        newKnife.transform.SetParent(Circel.transform);
        newKnife1.transform.SetParent(Circel.transform);
        newKnife2.transform.SetParent(Circel.transform);
        Invoke("SpeciKnifePowerTimer", .5f);
    }
    public void SpeciKnifePowerTimer ()
    {
        instantiater1 = true;
        instantiater2 = true;
        instantiater3 = true;
        instantiater4 = true;

    }
    public void SpawnSpecial ()
    {
        if (newKnife)
        {
            if (instantiater1 == true)
            {
                RaycastHit2D hit = Physics2D.Raycast(newKnife.transform.position, newKnife.transform.TransformDirection(Vector2.zero), 2f);
                if (hit)
                {
                    if (hit.collider.tag == "ballon")
                    {
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                        knifeChild.transform.SetParent(Circel.transform);
                        totlascore = totlascore + scoreBalloon;
                        Score.text = totlascore.ToString();
                        Scoreanime.text = "+" + scoreBalloon.ToString();
                        Scoreanime.gameObject.transform.DOMove(scorenimeGoal.transform.localPosition, 1f);
                        Invoke("resetScoreAnime", 1f);
                        Destroy(hit.collider.gameObject, .5f);
                        instantiater1 = false;

                    }

                }
            }
        }

        if (newKnife1)
        {
            if (instantiater2 == true)
            {
                RaycastHit2D hit2 = Physics2D.Raycast(newKnife1.transform.position, newKnife1.transform.TransformDirection(Vector2.zero), 2f);
                if (hit2)
                {
                    if (hit2.collider.tag == "ballon")
                    {
                        hit2.collider.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                        knifeChild.transform.SetParent(Circel.transform);
                        totlascore = totlascore + scoreBalloon;
                        Score.text = totlascore.ToString();
                        Scoreanime.text = "+" + scoreBalloon.ToString();
                        Scoreanime.gameObject.transform.DOMove(scorenimeGoal.transform.localPosition, 1f);
                        Invoke("resetScoreAnime", 1f);
                        Destroy(hit2.collider.gameObject, .5f);
                        instantiater2 = false;

                    }

                }
            }
        }
        if (newKnife2)
        {
            if (instantiater3 == true)
            {
                RaycastHit2D hit3 = Physics2D.Raycast(newKnife2.transform.position, newKnife2.transform.TransformDirection(Vector2.zero), 2f);
                if (hit3)
                {
                    if (hit3.collider.tag == "ballon")
                    {
                        hit3.collider.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                        knifeChild.transform.SetParent(Circel.transform);
                        totlascore = totlascore + scoreBalloon;
                        Score.text = totlascore.ToString();
                        Scoreanime.text = "+" + scoreBalloon.ToString();
                        Scoreanime.gameObject.transform.DOMove(scorenimeGoal.transform.localPosition, 1f);
                        Invoke("resetScoreAnime", 1f);
                        Destroy(hit3.collider.gameObject, .5f);
                        instantiater3 = false;

                    }

                }
            }
        }

    }
    
    public void  sceneRestart ()

    {
        Sounds.gameObject.transform.GetChild(8).gameObject.GetComponent<AudioSource>().Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
    public void pauseGame()

    {
        Sounds.gameObject.transform.GetChild(8).gameObject.GetComponent<AudioSource>().Play();

        gameStates = 1;
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }
    public void ContinueGame()

    {
        Sounds.gameObject.transform.GetChild(8).gameObject.GetComponent<AudioSource>().Play();

        gameStates = 0;
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }
    public void GotomainMenu ()
    {
        Sounds.gameObject.transform.GetChild(8).gameObject.GetComponent<AudioSource>().Play();

        Time.timeScale = 1;
        SceneManager.LoadScene("UI");
    }
    public void winner ()
    {
        if(totlascore> scoretowinlevel|| totlascore == scoretowinlevel)
        {
            gameStates = 1;
            StopCoroutine("StartCounter");
            FileStream fileCurrentLevel = File.Open(Application.persistentDataPath + "/fileCurrentLevel.save", FileMode.Open);
            BinaryFormatter bf_currentleveltogo = new BinaryFormatter();
            int x = thislevel + 1;
            bf_currentleveltogo.Serialize(fileCurrentLevel, x);
            fileCurrentLevel.Close();
            WinMenu.gameObject.SetActive(true);
            int evenlevel = thislevel % 2;
            if (evenlevel == 0)
            {
                ads.PlayAd();

            }
        }

    }
    public void nextlevel()
    {
        Sounds.gameObject.transform.GetChild(8).gameObject.GetComponent<AudioSource>().Play();
        levels.gameObject.GetComponent<LevelManager>().LevelFucntions();
        levels.gameObject.GetComponent<LevelManager>().functions[thislevel+1].Invoke();
        SceneManager.LoadScene("GameScene");
    }
    public void Loser()
    {
        LoseMenu.gameObject.SetActive(true);
        gameStates = 1;
        int evenlevel = thislevel%2;
        if(evenlevel==0)
        {
            ads.PlayAd();

        }
    }

    IEnumerator StartCounter()
    {
        yield return new WaitForSeconds(1f);
        time.text = counterValue.ToString();
        counterValue--;
        StartCoroutine("StartCounter");
        winner();
        if (counterValue<0)
        {
            StopCoroutine("StartCounter");
            rightarmgrip.gameObject.SetActive(false);
            lefttarmgrip.gameObject.SetActive(false);
            righttleggrip.gameObject.SetActive(false);
            lefttleggrip.gameObject.SetActive(false);
            LoseMenu.gameObject.SetActive(true);
            gameStates = 1;

        }
    }
    public void resetScoreAnime ()
    {
        Scoreanime.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-21f, 100f);
        Scoreanime.text = "";
    }
    /////////////////////////////////////////
    ///
    private void throwBall()
    {
        ball.SetActive(true);
        ball.gameObject.GetComponent<Rigidbody>().useGravity = true;
        ball.gameObject.GetComponent<Rigidbody>().AddForce(GetForceFrom(ball.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)), ForceMode.Impulse);
        isBallThrown = true;
    }
  
    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * power;
    }
    
    private void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {

        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;

        fTime += 0.1f;
        for (int i = 0; i < 10; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2f);
            trajectoryPoints[i].transform.position = pos;
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
        isPressed = true;


        //float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        //float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        //float fTime = 0;

        //fTime += 0.1f;
        //for (int i = 0; i < 10; i++)
        //{
        //    float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
        //    float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
        //    Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2f);
        //    trajectoryPoints[i].transform.position = pos;
        //    trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
        //    fTime += 0.1f;
        //}
        //isPressed = true;

    }
 
    public void playAudioSpecial()
    {
        Sounds.gameObject.transform.GetChild(9).gameObject.GetComponent<AudioSource>().Play();

    }
    public void playrewardedAdd()
    {
        ads.PlayAdReward(ads.gameObject.GetComponent<AdManager>().onRewarded);
    }
    public void setReward ()
    {
        if (File.Exists(Application.persistentDataPath + "/Rewards.save"))
        {
            BinaryFormatter Reward_knife = new BinaryFormatter();
            BinaryFormatter Reward_Peeks = new BinaryFormatter();
            FileStream file3 = File.Open(Application.persistentDataPath + "/Rewards.save", FileMode.Open);
            checkKnife = (int)Reward_knife.Deserialize(file3);
            peaksno = (int)Reward_Peeks.Deserialize(file3);
            file3.Close();
            File.Delete(Application.persistentDataPath + "/Rewards.save");
        }

    }
}
   

    //// make making ads system  
