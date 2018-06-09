// hello
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;

public class Controller : MonoBehaviour
{

    private Rigidbody rb;
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    private float x, y, z;
    //private float oldx, oldy, oldz;

    private bool use_tracking = true;//false;

    // Use this for initialization
    void Start()
    {
        x = 0.0f;
        y = 0.0f;
        z = 0.0f;
        /* 
        oldx = 0.0f;
        oldy = 0.0f;
        oldz = 0.0f; */
        rb = GetComponent<Rigidbody>();
        // ListenForData();
        ConnectToTcpServer();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_pos;
        if (use_tracking)
        {
            /*
            Vector3 old_pos = rb.position;
            Vector3 movement = new Vector3(x - oldx, y - oldy, z - oldz);
            Vector3 new_pos = old_pos + movement;

            */
            new_pos = new Vector3(x*1.8f + 0.08f, y*1.8f-0.59f, z*1.8f+0.42f);
            
            /*
            Debug.Log("Updated.");
            Debug.Log(x);
            Debug.Log(y);
            */
        }
        else
        {
            new_pos = rb.position;
        }
        new_pos.x = Mathf.Clamp(new_pos.x, -0.5f, 0.5f);
        new_pos.y = Mathf.Clamp(new_pos.y, -1.1f, -0.6f);
        new_pos.z = Mathf.Clamp(new_pos.z, 0.35f, 0.5f);
        rb.MovePosition(new_pos);

        if (Input.GetKey("up"))
        {
            Debug.Log("Tracking on.");
            use_tracking = true;

        }
        
        else if (Input.GetKey("down"))
        {
            Debug.Log("Tracking off.");
            use_tracking = false;
        }
    }

    public void TrackingOn()
    {
        use_tracking = true;
        Debug.Log("Tracking on.");
    }

    public void TrackingOff()
    {
        use_tracking = false;
        Debug.Log("Tracking off.");
    }


    /// <summary> 	

    /// Setup socket connection. 	

    /// </summary> 	

    private void ConnectToTcpServer()
    {

        try
        {

            clientReceiveThread = new Thread(new ThreadStart(ListenForData));

            clientReceiveThread.IsBackground = true;

            clientReceiveThread.Start();

        }

        catch (Exception e)
        {

            Debug.Log("On client connect exception " + e);

        }

    }

    /// <summary> 	

    /// Runs in background clientReceiveThread; Listens for incomming data. 	

    /// </summary>     

    private void ListenForData()
    {

        try
        {

            socketConnection = new TcpClient("10.34.168.245", 8080);

            Byte[] bytes = new Byte[1024];

            while (true)
            {

                // Get a stream object for reading 				

                using (NetworkStream stream = socketConnection.GetStream())
                {

                    byte[] myWriteBuffer = Encoding.ASCII.GetBytes("GET / HTTP/1.1\nHost: 10.34.160.167:8081\n\n");
                    stream.Write(myWriteBuffer, 0, myWriteBuffer.Length);

                    int length;

                    // Read incomming stream into byte arrary. 					

                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {

                        var incommingData = new byte[length];

                        Array.Copy(bytes, 0, incommingData, 0, length);

                        // Convert byte array to string message. 						

                        string serverMessage = Encoding.ASCII.GetString(incommingData);

                        Debug.Log("server message received as: " + serverMessage);

                        string[] split = serverMessage.Split('/');
                        if (split.Length < 3)
                            continue;
                     
                        string xyz = split[split.Length-2];

                        if (String.IsNullOrEmpty(xyz))
                            continue;
                        
                        string[] xyz_array = xyz.Split(',');
                        if (xyz_array.Length != 3)
                            continue;
                        //Debug.Log(xyz);

                        /*
                        oldx = x;
                        oldy = y;
                        oldz = z; */
                        x = float.Parse(xyz_array[0]);
                        y = float.Parse(xyz_array[1]);
                        z = float.Parse(xyz_array[2]);

                    }

                }

            }

        }

        catch (SocketException socketException)
        {

            Debug.Log("Socket exception: " + socketException);

        }

    }
}
