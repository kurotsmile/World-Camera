using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
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
        list_ip = this.GenerateRandomIPList(500);
    }

    List<IP_Data> GenerateRandomIPList(int count)
    {
        List<IP_Data> ipList = new List<IP_Data>();
        System.Random random = new System.Random();

        for (int i = 0; i < count; i++)
        {
            IP_Data iP = new IP_Data();
            //iP.ip= $"{random.Next(1, 256)}.{random.Next(0, 256)}.{random.Next(0, 256)}.{random.Next(0, 256)}";
            iP.ip= $"27.79.{random.Next(0, 256)}.{random.Next(0, 256)}";
            iP.port= random.Next(1000, 9999);
            ipList.Add(iP);
        }

        return ipList;
    }

    public IP_Data Get(int index)
    {
        return this.list_ip[index];
    }


    public async Task CheckIPAsync(string ipAddress, int port, UnityAction<bool> actionSuccess, UnityAction<string> actionFail, CancellationToken cancellationToken)
    {
        try
        {
            bool isAlive = await IsIPAliveAsync(ipAddress, port,5000, cancellationToken);
            actionSuccess?.Invoke(isAlive);
        }
        catch (OperationCanceledException)
        {
            actionFail?.Invoke("Operation was cancelled.");
        }
        catch (Exception ex)
        {
            actionFail?.Invoke(ex.Message);
        }
    }

    public async void CheckIPAsync_one(string ipAddress, int port, UnityAction<bool> actionSuccess, UnityAction<string> actionFail)
    {
        try
        {
            bool isAlive = await IsIPAliveAsync(ipAddress, port, 5000, CancellationToken.None);
            actionSuccess?.Invoke(isAlive);
        }
        catch (Exception ex)
        {
            actionFail?.Invoke(ex.Message);
        }
    }

    Task<bool> IsIPAliveAsync(string ipAddress, int port, int timeout,CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    var result = client.BeginConnect(ipAddress, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(timeout);

                    cancellationToken.ThrowIfCancellationRequested();

                    if (!success)
                    {
                        return false;
                    }

                    client.EndConnect(result);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }, cancellationToken);
    }

}
