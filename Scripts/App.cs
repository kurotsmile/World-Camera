using Carrot;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class App : MonoBehaviour
{
    [Header("Main Obj")]
    public Carrot.Carrot carrot;
    public IP_Manager ip;

    [Header("UI Obj")]
    public GameObject prefab_item;
    public Transform tr_area_main;

    private bool is_play=false;
    private int index_check = 0;
    private CancellationTokenSource cancellationTokenSource;

    [Header("Icon")]
    public Sprite icon_ip;
    public Sprite icon_wifi;

    void Start()
    {
        carrot.Load_Carrot();
        this.Create_list_ip_and_load();
    }

    private void Create_list_ip_and_load()
    {
        this.ip.On_Load();
        this.carrot.clear_contain(this.tr_area_main);
        foreach (IP_Data ip in this.ip.list_ip)
        {
            GameObject p_item = Instantiate(prefab_item);
            p_item.transform.SetParent(this.tr_area_main);
            p_item.transform.localScale = new Vector3(1f, 1f, 1f);

            Carrot_Box_Item item_ip = p_item.GetComponent<Carrot.Carrot_Box_Item>();
            item_ip.set_icon(icon_ip);
            item_ip.check_type();
            item_ip.set_title(ip.ip);
            item_ip.set_tip(ip.port.ToString());

            item_ip.set_act(() =>
            {
                item_ip.set_tip("Waiting...");
                this.ip.CheckIPAsync_one(ip.ip, ip.port, (is_succss) =>
                {
                    if (is_succss)
                        item_ip.set_tip("Online");
                    else
                        item_ip.set_tip("Offline");
                }, (s_error) =>
                {
                    item_ip.set_tip("Error:" + s_error);
                });
            });
        }
    }

    public void Btn_scan_ip()
    {
        if (this.is_play)
        {
            cancellationTokenSource?.Cancel();
            this.is_play = false;
        }
        else
        {
            this.check_all_ip();
            this.is_play = true;
        }
    }

    async void check_all_ip()
    {
        cancellationTokenSource = new CancellationTokenSource();
        List<Task> tasks = new List<Task>();

        foreach (Transform child in this.tr_area_main)
        {
            int index=child.GetComponentIndex();
            IP_Data ip_check = this.ip.Get(index);

            Carrot_Box_Item item_ip = child.GetComponent<Carrot.Carrot_Box_Item>();
            item_ip.set_tip("Waiting...");
            tasks.Add(this.ip.CheckIPAsync(ip_check.ip, 554, (is_status)=>
            {
                if (is_status)
                    item_ip.set_tip("Online");
                else
                {
                    item_ip.set_tip("Offline");
                    Destroy(child.gameObject);
                }
            }, (s_error) =>
            {
                item_ip.set_tip("Error:" + s_error);
            }, cancellationTokenSource.Token));
        }

        try
        {
            await Task.WhenAll(tasks);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("IP checking was cancelled.");
        }
    }

    void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
    }

    public void btn_new_session()
    {
        this.Create_list_ip_and_load();
    }
}
