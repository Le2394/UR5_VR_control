using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Palmmedia.ReportGenerator.Core.Common;

public class TCP : MonoBehaviour
{
    Thread thread;
    [SerializeField]
    private string connectionIP = "192.168.55.108";
    [SerializeField]
    private int connectionPort = 10000;
    IPAddress address;
    TcpClient tcpClient = new TcpClient();
    TcpListener listener;

    bool running;
    static ASCIIEncoding encoding = new ASCIIEncoding();

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
        Console.WriteLine("SERVER CONNECTED");
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
        
        byte[] Space = encoding.GetBytes(" ");
        stream.Write(Space, 0, Space.Length);
        Debug.Log(" ");
        byte[] Joint_1 = BitConverter.GetBytes(IK_toolkit.Joint_1);
        stream.Write(Joint_1, 0, Joint_1.Length);
        print(BitConverter.ToSingle(Joint_1));
        byte[] Joint_2 = BitConverter.GetBytes(IK_toolkit.Joint_2);
        stream.Write(Joint_2, 0, Joint_2.Length);
        print(BitConverter.ToSingle(Joint_2));
        byte[] Joint_3 = BitConverter.GetBytes(IK_toolkit.Joint_3);
        stream.Write(Joint_3, 0, Joint_3.Length);
        print(BitConverter.ToSingle(Joint_3));
        byte[] Joint_4 = BitConverter.GetBytes(IK_toolkit.Joint_4);
        stream.Write(Joint_4, 0, Joint_4.Length);
        print(BitConverter.ToSingle(Joint_4));
        byte[] Joint_5 = BitConverter.GetBytes(IK_toolkit.Joint_5);
        stream.Write(Joint_5, 0, Joint_5.Length);
        print(BitConverter.ToSingle(Joint_5));
        byte[] Joint_6 = BitConverter.GetBytes(IK_toolkit.Joint_6);
        stream.Write(Joint_6, 0, Joint_6.Length);
        print(BitConverter.ToSingle(Joint_6));
        */
    }
}


