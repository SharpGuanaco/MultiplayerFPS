using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private Behaviour[] componentsToDisable;
    [SerializeField] private NetworkObject networkingObject;
    private Camera sceneCamera;

    private void Start()
    {
        if (!networkingObject.IsOwner)
        {
            foreach (Behaviour component in componentsToDisable)
            {
                component.enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
