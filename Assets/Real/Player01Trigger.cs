using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Trigger : MonoBehaviour
{
    public Collider Col;
    public bool Check = false;

    void Start()
    {
        Col.enabled = true;
    }

    void Update()
    {
        // เปิดใช้งาน Collider ถ้า Player01Action.Hits เป็น false
        Col.enabled = !Player01Action.Hits;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player02")
        {
            Player01TakeAction.Hits = true;
            if(Check)
            {
                GetComponentInParent<Player01TakeAction>().OnHits();
            }

            // คุณอาจต้องการรีเซ็ตค่า Hits หลังจากช่วงเวลาหนึ่ง
            StartCoroutine(ResetHits());
        }
    }

    private IEnumerator ResetHits()
    {
        yield return new WaitForSeconds(1f); // รอ 1 วินาที (หรือเวลาที่คุณต้องการ)
        Player01TakeAction.Hits = false;
    }
}
