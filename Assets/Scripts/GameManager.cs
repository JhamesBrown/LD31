﻿using UnityEngine;
using System.Collections;
using GMnameSpace;
using System.Collections.Generic;


public class GameManager : MonoBehaviour 
{
    public GameObject Player { get; set; }
    public GameObject ThePrizePrefab { get; set; }
    public GameObject PlayerPrefab { get; set; }
    public CheckPoint CurrentCheckPoint = new CheckPoint();
    public GUIStyle MainGUIStyle;
    public List<Vector3> PrizePositions = new List<Vector3> ();
    public Transform Levels;
    public bool GameComplete { get; set; }
    
    public static GameManager GM;

    #region Awake,Start and Update
    void Awake ()
    {
        fillPrizePositionList();
        CurrentCheckPoint.nextCheckPoint();
        Player = GameObject.FindGameObjectWithTag("Player");
        GameComplete = false;
        Levels = GameObject.Find("Levels").transform;
               

        for (int i = 0; i < Levels.childCount; i++)
            Levels.GetChild(i).gameObject.SetActive(false);

        Levels.GetChild(0).gameObject.SetActive(true);           

    }

    void Start()
    {
        PlayerPrefab = (GameObject)Resources.Load("Player");
        ThePrizePrefab = (GameObject)Resources.Load("ThePrize");

        
    }

    void Update()
    {
        if (Player == null)
        {
            Instantiate(PlayerPrefab, CurrentCheckPoint.PlayerPosition, Quaternion.identity);
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        
            
    }
    #endregion

    public void PrizeGot()
    {

        CurrentCheckPoint.nextCheckPoint();
        if ((CurrentCheckPoint.CheckPointNum) < Levels.childCount)
        {
            CurrentCheckPoint.PrizePosition = PrizePositions[CurrentCheckPoint.CheckPointNum];
            Instantiate(ThePrizePrefab, CurrentCheckPoint.PrizePosition, Quaternion.identity);
            Levels.GetChild(CurrentCheckPoint.CheckPointNum - 1).gameObject.SetActive(false);
            Levels.GetChild(CurrentCheckPoint.CheckPointNum).gameObject.SetActive(true);    
        }
        else
            GameComplete = true;
              
    }

    void OnGUI ()
    {
        GUI.BeginGroup(new Rect(10, 10, 200, 80));
        GUI.Label(new Rect(0, 00, 200, 20), "CP " + CurrentCheckPoint.CheckPointNum, MainGUIStyle);
        GUI.Label(new Rect(0, 20, 200, 20), "PPos " + CurrentCheckPoint.PlayerPosition, MainGUIStyle);
        GUI.Label(new Rect(0, 40, 200, 20), "GameComplete " + GameComplete, MainGUIStyle);
        GUI.EndGroup();
    }

    void fillPrizePositionList()
    {
        PrizePositions.Add(new Vector3(4.0f, 0.0f, -2.2f));
        PrizePositions.Add(new Vector3(-4.5f, 0.0f, 5.7f));
        PrizePositions.Add(new Vector3(4.0f, 0.0f, -2.2f));
        PrizePositions.Add(new Vector3(-4.5f, 0.0f, 5.7f));
        PrizePositions.Add(new Vector3(4.0f, 0.0f, -2.2f));
        PrizePositions.Add(new Vector3(-4.5f, 0.0f, 5.7f));
        PrizePositions.Add(new Vector3(4.0f, 0.0f, -2.2f));
        PrizePositions.Add(new Vector3(-4.5f, 0.0f, 5.7f));
    }
}
