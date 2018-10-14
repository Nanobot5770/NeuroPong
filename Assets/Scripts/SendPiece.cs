using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SendPiece : MonoBehaviour {

    public string ip = "192.168.43.228";
    public int port = 1200;

    private IPEndPoint sendEndPoint;

    // Use this for initialization
    void Start () {
        IPAddress mAddress = null;
        IPAddress.TryParse(ip, out mAddress);

        sendEndPoint = new IPEndPoint(mAddress, port);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendStuff(string piece)
    {
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Debug.Log(piece);
        byte[] send_buffer = Encoding.ASCII.GetBytes(piece);
        sock.SendTo(send_buffer, sendEndPoint);

        sock.Close();
    }
}
