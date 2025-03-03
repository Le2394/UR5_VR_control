using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;

public class TCPServer : MonoBehaviour
{
    private TcpListener listener;
    private bool running = false;
    public string ipAddress = "192.168.55.109";
    public int port = 10000;
    public bool control;

    private ConcurrentQueue<float[]> jointUpdateQueue = new ConcurrentQueue<float[]>();

    private Thread serverThread;
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        StartServer();
    }

    void StartServer()
    {
        try
        {
            IPAddress localAddr = IPAddress.Parse(ipAddress);
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            running = true;

            serverThread = new Thread(RunServer);
            serverThread.IsBackground = true;
            serverThread.Start();

            Debug.Log($"Server started on {ipAddress}:{port}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Server failed to start: {ex.Message}");
        }
    }

    private void RunServer()
    {
        while (running)
        {
            Debug.Log("Waiting for connection...");
            client = listener.AcceptTcpClient();
            stream = client.GetStream();
            Debug.Log("Client connected!");

            Thread clientThread = new Thread(HandleClient);
            clientThread.IsBackground = true;
            clientThread.Start();
        }
    }

    private void HandleClient()
    {
        try
        {
            byte[] buffer = new byte[24];

            while (client.Connected && running)
            {
                float[] jointValues = control
                    ? IK_control.joints
                    : new float[] { IK_toolkit.Joint_1, IK_toolkit.Joint_2, IK_toolkit.Joint_3, IK_toolkit.Joint_4, IK_toolkit.Joint_5, IK_toolkit.Joint_6 };

                Buffer.BlockCopy(jointValues, 0, buffer, 0, buffer.Length);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
                Debug.Log($"Sent joint values: {string.Join(", ", jointValues)}");

                Thread.Sleep(5);  // Minimal delay to avoid excessive CPU usage
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Client connection lost: {ex.Message}");
        }
        finally
        {
            client?.Close();
            Debug.Log("Client disconnected.");
        }
    }

    public void StopServer()
    {
        running = false;
        listener?.Stop();
        client?.Close();
        serverThread?.Join();
        Debug.Log("Server stopped.");
    }

    void OnApplicationQuit()
    {
        StopServer();
    }
}
