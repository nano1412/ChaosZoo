using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01TriggerInChallenge : MonoBehaviour
{
    public Collider Col;
    public bool Check = false;

    void Start()
    {
        // คุณอาจต้องการรีเซ็ตค่าเริ่มต้นของ Collider ที่นี่
        Col.enabled = true;
    }

    void Update()
    {
        // เปิดใช้งาน Collider ถ้า Player01Action.Hits เป็น false
        Col.enabled = !Player01TakeActionInChallenge.Hits;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player02")
        {
            Player01TakeActionInChallenge.Hits = true;
            if(Check)
            {
                GetComponentInParent<Player01TakeActionInChallenge>().OnHits();
            }

            // คุณอาจต้องการรีเซ็ตค่า Hits หลังจากช่วงเวลาหนึ่ง
            StartCoroutine(ResetHits());
        }
    }

    private IEnumerator ResetHits()
    {
        yield return new WaitForSeconds(1f); // รอ 1 วินาที (หรือเวลาที่คุณต้องการ)
        Player01TakeActionInChallenge.Hits = false;
    }
}