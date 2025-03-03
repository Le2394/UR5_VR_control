using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using System.Threading;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using UnityEngine.UI;
using System.Net.Http;

public class MotorClient : PostProcessProvider
{
    private const int BUFFER_SIZE = 1024;
    [SerializeField]
    private int PORT_NUMBER = 3333;
    Thread thread;
    [SerializeField]
    private string connectionIP = "192.168.55.107";
    TcpClient tcpClient = new TcpClient();
    TcpListener listener;
    static ASCIIEncoding encoding = new ASCIIEncoding();

    public Text HandPos;
    public Text TouchMessage;
    static public bool running;

    private float handPositionX = 0f;
    private float handPositionY = 0f;
    private float handPositionZ = 0f;

    void Start()
    {
        tcpClient.Connect(connectionIP, PORT_NUMBER);
        print("SERVER CONNECTED");
        running = true;
    }

    public void Update()
    {
        if (running)
        {
            SendData();
        }
    }
    public void SendData()
     {
         Stream stream = tcpClient.GetStream();
         //Console.Write(" Speed  (or  'exit' to close ): ");
         {
            if (Collide.IsTouched == true)
            {
                byte[] data = encoding.GetBytes("8000");
                stream.Write(data, 0, data.Length);
                print("8000");
            }

            else if (Collide.IsTouched == false)
            {
                byte[] data = encoding.GetBytes("0");
                stream.Write(data, 0, data.Length);
                print("0");
            }
             // Gửi dữ liệu tới server
         }
     } 
    public override void ProcessFrame(ref Frame inputframe)
    {
        foreach(var Hand in inputframe.Hands)
        {
            Vector3 HandPostion = Hand.PalmPosition;

            handPositionX = HandPostion.x;
            handPositionY = HandPostion.y;
            handPositionZ = HandPostion.z;
        }
    }
    public void OnGUI()
    {
        //Stream stream = tcpClient.GetStream();
        HandPos.text = "Your hand position is: " + (handPositionX, handPositionY, handPositionZ).ToString();

        if (Collide.IsTouched == true)
        {
            TouchMessage.text = "You have touched the object";
            //byte[] data = encoding.GetBytes("5000");
            //stream.Write(data, 0, data.Length);
        }
        else if (Collide.IsTouched == false)
        {
            TouchMessage.text = "You haven't touched the object yet";
            //byte[] data = encoding.GetBytes("0");
            //stream.Write(data, 0, data.Length);
        }
    }
}


