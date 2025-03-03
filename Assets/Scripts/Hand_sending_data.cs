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
using UnityEditor.PackageManager;
using UnityEngine.UIElements;
using UnityEditor;

public class Hand_sending_data : PostProcessProvider
{
    Thread thread;
    [SerializeField]
    private string connectionIP = "172.20.10.3";
    [SerializeField]
    private int connectionPort = 3333;
    IPAddress address;
    TcpClient tcpClient = new TcpClient();
    TcpListener listener;

    public Text HandPos;
    public Text TouchMessage;
    bool running;
    static ASCIIEncoding encoding = new ASCIIEncoding();

    private float handPositionX = 0f;
    private float handPositionY = 0f;
    private float handPositionZ = 0f;

    Finger thumb;
    private void Start()
    {
        ThreadStart start = new ThreadStart(GetInfo);
        thread = new Thread(start);
        thread.Start();
    }

    private void Update()
    {

    }
    void GetInfo()
    {
        //address = IPAddress.Parse(connectionIP);
        //listener = new TcpListener(IPAddress.Any, connectionPort);
        //listener.Start();
        //tcpClient = listener.AcceptTcpClient();
        tcpClient.Connect(connectionIP, connectionPort);
        print("SERVER CONNECTED");
        running = true;
        while (running)
        {
            SendData();
        }
    }
    private void SendData()
    {
        NetworkStream stream = tcpClient.GetStream();
        
        //print((handPositionX, handPositionY, handPositionZ));
        /*
        byte[] myWriteBuffer = Encoding.UTF8.GetBytes("Hand Position: ");
        stream.Write(myWriteBuffer, 0, myWriteBuffer.Length);

        byte[] handPosition = Encoding.UTF8.GetBytes((handPositionX, handPositionY, handPositionZ).ToString());
        stream.Write(handPosition, 0, handPosition.Length);
        */
        if (Collide.IsTouched == true)
       {
           byte[] TouchCondition = encoding.GetBytes("1");
           stream.Write(TouchCondition, 0, TouchCondition.Length);
        }
       else if(Collide.IsTouched == false)
       {
           byte[] TouchCondition = encoding.GetBytes("0");
           stream.Write(TouchCondition, 0, TouchCondition.Length);
       }
    }

    public override void ProcessFrame(ref Frame inputFrame)
    {
        foreach (var hand in inputFrame.Hands)
        {
            Vector3 HandPosition = hand.PalmPosition;

            handPositionX = HandPosition.x;
            handPositionY = HandPosition.y;
            handPositionZ = HandPosition.z;
            Finger _index = hand.GetIndex();
            print(_index.ToString());

        }
    }
    public void OnGUI() 
    {
        HandPos.text = "Your hand position is: " + (handPositionX, handPositionY, handPositionZ).ToString();    
        
        if (Collide.IsTouched == true)
        {
            TouchMessage.text = "You have touched the object";
        }
        else if (Collide.IsTouched == false)
        {
            TouchMessage.text = "You haven't touched the object yet";
        }

        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
        GUIStyle fontSizeButton = new GUIStyle(GUI.skin.GetStyle("button"));
        fontSize.fontSize = 22;
        fontSizeButton.fontSize = 22;
        GUI.Label(new Rect(10, 400, 200, 50), "Your hand position is: " + (handPositionX, handPositionY, handPositionZ).ToString(), fontSize);

        if (GUI.Button(new Rect(Screen.width-250, 10, 200, 100), "START", fontSizeButton))
        {

            Debug.Log("Start");


        }

        if (GUI.Button(new Rect(10, 1000, 200, 50), "QUIT", fontSizeButton))
        {

            Debug.Log("Quit");
            EditorApplication.ExitPlaymode();


        }
    }
}

