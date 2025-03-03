using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI; // Only if you're using Unity UI elements like Buttons or Sliders

public class UnityTcpServer : MonoBehaviour
{
    // Fields for manually setting joint values in the Inspector
    public float joint1;
    public float joint2;
    public float joint3;
    public float joint4;
    public float joint5;
    public float joint6;

    // TCP-related fields
    private TcpListener server;
    private TcpClient client;
    private NetworkStream stream;
    private Thread serverThread;

    void Start()
    {
        serverThread = new Thread(StartServer);
        serverThread.Start();
    }


    public void SetJointValues(float j1, float j2, float j3, float j4, float j5, float j6)
    {
        float[] jointValues = new float[] { IK_toolkit.Joint_1, IK_toolkit.Joint_2, IK_toolkit.Joint_3, IK_toolkit.Joint_4, IK_toolkit.Joint_5, IK_toolkit.Joint_6};

        // Log each joint value for verification
        for (int i = 0; i < jointValues.Length; i++)
        {
            Debug.Log($"Joint {i + 1}: {jointValues[i]}");
        }

        // Send the joint values to ROS client
        SendJointValuesToClient(jointValues);
    }

    // Send joint values to ROS client
    void SendJointValuesToClient(float[] jointValues)
    {
        if (stream != null && stream.CanWrite)
        {
            byte[] data = new byte[24];  // 6 floats x 4 bytes each = 24 bytes
            Buffer.BlockCopy(jointValues, 0, data, 0, data.Length);

            try
            {
                stream.Write(data, 0, data.Length);  // Send data to the ROS client
                stream.Flush();
                Debug.Log("Joint values sent to ROS client.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error sending joint values: {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning("Stream is not ready for writing.");
        }
    }

    void StartServer()
    {
        server = new TcpListener(IPAddress.Any, 10000); // Replace with actual port
        server.Start();
        Debug.Log("Unity TCP Server started, waiting for connection...");

        client = server.AcceptTcpClient();
        Debug.Log("Client connected");

        stream = client.GetStream();
    }
}

