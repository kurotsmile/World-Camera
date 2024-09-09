using Carrot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    public Carrot.Carrot carrot;
    public GameObject prefab_item;
    public IP_Manager ip;
    public Transform tr_area_main;

    [Header("Icon")]
    public Sprite icon_ip;
    public Sprite icon_wifi;

    void Start()
    {
        carrot.Load_Carrot();
        this.ip.On_Load();

        foreach(IP_Data ip in this.ip.list_ip)
        {
            GameObject p_item= Instantiate(prefab_item);
            p_item.transform.SetParent(this.tr_area_main);
            p_item.transform.localScale = new Vector3(1f, 1f, 1f);

            Carrot_Box_Item item_ip = p_item.GetComponent<Carrot.Carrot_Box_Item>();
            item_ip.set_icon(icon_ip);
            item_ip.check_type();
            item_ip.set_title(ip.ip);
            item_ip.set_tip(ip.port.ToString());
        }
    }

    void Update()
    {
        
    }

    public void Btn_scan_ip()
    {
        foreach(Transform child in this.tr_area_main)
        {
            int index_p = child.GetComponentIndex();
            IP_Data ip = this.ip.Get(index_p);
            child.GetComponent<Carrot_Box_Item>().set_tip("Waiting");

            this.ip.IsIPAlive(ip.ip, ip.port, 5000, () =>
            {
                child.GetComponent<Carrot_Box_Item>().set_tip("Success");
            }, () =>
            {
                child.GetComponent<Carrot_Box_Item>().set_tip("Fail");
            });
        }
    }
}
