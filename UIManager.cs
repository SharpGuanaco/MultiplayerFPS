using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button ServerButton;
    [SerializeField] private Button ClientButton;
    [SerializeField] private Button HostButton;
    [SerializeField] private Button StopButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private String currentState = "Home";

    private void Awake()
    {
        Cursor.visible = true;
    }
    

    private void Start()
    {
        HostButton.onClick.AddListener(() =>
            {
                if (NetworkManager.Singleton.StartHost())
                {
                    Debug.Log("host started");
                    currentState = "Host";
                }
                else
                {
                    Debug.Log("host failed");
                }
            }
        );
        
        ClientButton.onClick.AddListener(() =>
            {
                if (NetworkManager.Singleton.StartClient())
                {
                    Debug.Log("passed");
                    currentState = "Client";
                }
                else
                {
                    Debug.Log("failed");
                }
            }
        );
        
        ServerButton.onClick.AddListener(() =>
            {
                if (NetworkManager.Singleton.StartServer())
                {
                    Debug.Log("passed");
                    currentState = "Server";
                }
                else
                {
                    Debug.Log("failed");
                }
            }
        );
        
        
        StopButton.onClick.AddListener(() =>
            {
                if (currentState != "Home")
                {
                    
                    NetworkManager.Singleton.Shutdown(true);

                }

            }
        );
        
        QuitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            }
        );
        
    }
}
