using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour
{
    public static Client Instance { get; private set; }

    public int DataBufferSize { get; } = 4096;

    public IPAddress IP { get; } = IPAddress.Loopback;

    public int Port { get; } = 26950;

    public int ID { get; } = 0;

    private TCP _tcp;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Debug.Log("Instance already exists");
            Destroy(this);
        }
    }

    private void Start() => _tcp = new TCP();

    public void ConnectedToServer() => _tcp.Connect();
}