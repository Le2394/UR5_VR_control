using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using System.Threading;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;

public class TCPClient : MonoBehaviour
{
    public string serverIP = "172.20.10.3";
    public int serverPort = 3333;

    private TcpClient client;
    private NetworkStream stream;
    private Thread clientThread;

    // Start is called before the first frame update
    void Start()
    {
        clientThread = new Thread(new ThreadStart(ConnectToServer));
        clientThread.Start();
    }

    private void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();
            Debug.Log("Connected to server.");
        }
        catch (Exception e)
        {
            Debug.LogError("Error connecting to server: " + e.Message);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SendSignal("0");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SendSignal("1");
        }
    }
    */
    void SendSignal()
    {
        try
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes("1");
            stream.Write(data, 0, data.Length);
            Debug.Log("Signal sent: " + "1");
        }
        catch (Exception e)
        {
            Debug.LogError("Error sending signal: " + e.Message);
        }
    }

    private void OnDestroy()
    {
        if (clientThread != null && clientThread.IsAlive)
        {
            clientThread.Abort();
        }

        if (client != null)
        {
            client.Close();
            Debug.Log("Connection closed.");
        }
    }
}

