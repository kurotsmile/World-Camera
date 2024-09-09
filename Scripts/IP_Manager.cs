using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;

public class  IP_Data
{
    public string ip;
    public int port;
}
public class IP_Manager : MonoBehaviour
{

    public IList<IP_Data> list_ip = null;

    public void On_Load()
    {
        list_ip = this.GenerateRandomIPList(200);
    }

    List<IP_Data> GenerateRandomIPList(int count)
    {
        List<IP_Data> ipList = new List<IP_Data>();
        System.Random random = new System.Random();

        for (int i = 0; i < count; i++)
        {
            IP_Data iP = new IP_Data();
            iP.ip= $"{random.Next(1, 256)}.{random.Next(0, 256)}.{random.Next(0, 256)}.{random.Next(0, 256)}";
            iP.port= random.Next(1000, 9999);
            ipList.Add(iP);
        }

        return ipList;
    }

    public IP_Data Get(int index)
    {
        return this.list_ip[index];
    }

    public bool IsIPAlive(string ipAddress, int port, int timeout = 5000,UnityAction act_done = null,UnityAction act_fail=null)
    {
        try
        {
            using (TcpClient client = new TcpClient())
            {
                var result = client.BeginConnect(ipAddress, port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(timeout);

                if (!success)
                {
                    act_fail?.Invoke();
                    return false;
                }

                client.EndConnect(result);
                act_done?.Invoke();
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Error: {ex.Message}");
            act_fail?.Invoke();
            return false;
        }
    }
}
