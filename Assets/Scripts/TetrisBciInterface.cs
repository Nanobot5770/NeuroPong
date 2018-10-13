using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;

public class TetrisBciInterface : MonoBehaviour
{

    public int port = 1000;

    public bool ActivateCommunication = true;

    private static bool IsListeningToUdp;

    private static int mPort;

    private static string receivedCommand;

    private static UdpClient udpClient;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Starting Communication");
        mPort = port;
        IsListeningToUdp = ActivateCommunication;
        new Thread(ListenToBCI).Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (receivedCommand != null)
        {
            OnCommandReceived(receivedCommand);
            receivedCommand = null;
        }

        if (ActivateCommunication != IsListeningToUdp)
        {
            IsListeningToUdp = ActivateCommunication;
        }
    }

    public void OnCommandReceived(string cmd)
    {
        String[] values = cmd.Split('_');

        if(values.Length == 2)
        {
            String piece = values[0].ToLower();
            int rotation = int.Parse(values[1]);

            SetNextPiece(piece, rotation);
        }
    }

    public void SetNextPiece(String piece, int rotation)
    {
        Debug.Log(string.Format("Next Piece {0} @ Angle {1}", piece, rotation));
    }

    static void ListenToBCI()
    {
        udpClient = new UdpClient(mPort);
        

        while (IsListeningToUdp)
        {
            var remoteEndPoint = new IPEndPoint(IPAddress.Any, mPort);
            byte[] receivedResults = udpClient.Receive(ref remoteEndPoint);

            MemoryStream memoryStream = new MemoryStream(receivedResults);
            BinaryFormatter formatter = new BinaryFormatter();
            memoryStream.Position = 0;
            //Debug.Log("Results? " + receivedResults.Length);

            if (receivedCommand == null)
            {
                receivedCommand = ParseMessage(receivedResults);
                //Debug.Log("In Thread: " + receivedCommand);
            }
        }

        Debug.Log("Stopping Communication");

        udpClient.Close();
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Stopping Communication");
        IsListeningToUdp = false;
        udpClient.Close();
    }

    private static string ParseMessage(byte[] receivedResults)
    {
        return System.Text.Encoding.UTF8.GetString(receivedResults, 0, receivedResults.Length);
    }
}
