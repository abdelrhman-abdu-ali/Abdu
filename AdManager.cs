using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class AdManager : MonoBehaviour,IUnityAdsListener
{
    public Action onRewarded;
    int peeks, Knives;
    // Start is called before the first frame update
    void Start()
    {
        peeks = 1;
        Knives =1;
        Advertisement.Initialize("4158009");
        Advertisement.AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAd()
    {
         if(Advertisement.IsReady("Interstitial"))
        {
            Advertisement.Show("Interstitial");
        }
    }
    public void PlayAdReward(Action Success)
    {
        onRewarded = Success;
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId== "Rewarded_Android"&& showResult ==ShowResult.Finished)
        {
            Debug.Log("rewarded");
            onRewarded.Invoke();
            creatReward();
        }
    }
    public void creatReward ()
    {
        BinaryFormatter Reward_knife = new BinaryFormatter();
        BinaryFormatter Reward_Peeks = new BinaryFormatter();
        File.Delete(Application.persistentDataPath + "/Rewards.save");
        FileStream Reward2 = File.Create(Application.persistentDataPath + "/Rewards.save");
        Reward_knife.Serialize(Reward2, Knives);
        Reward_Peeks.Serialize(Reward2, peeks);
        Reward2.Close();
    }

}

