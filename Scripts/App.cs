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

    void Start()
    {
        carrot.Load_Carrot();
        this.ip.On_Load();

        foreach(string ip in this.ip.list_ip)
        {
            GameObject p_item= Instantiate(prefab_item);
            p_item.transform.SetParent(this.tr_area_main);
            p_item.transform.localScale = new Vector3(1f, 1f, 1f);

            Carrot_Box_Item item_ip = p_item.GetComponent<Carrot.Carrot_Box_Item>();
            item_ip.check_type();
            item_ip.set_title(ip);
            item_ip.set_tip(ip);
        }
    }

    void Update()
    {
        
    }
}
