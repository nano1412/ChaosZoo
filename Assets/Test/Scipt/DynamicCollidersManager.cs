using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ColliderFrameData
{
    public int frame;
    public Vector3 size;
    public Vector3 center;
}

public class DynamicCollidersManager : MonoBehaviour
{
    public List<Collider> colliders; // รายการ Collider ทั้งหมดในตัวละคร
    public List<ColliderFrameData> frameData; // ข้อมูลขนาดและตำแหน่งของ Collider ตามเฟรม

    private int currentFrame = 0;

    void Start()
    {
        // ตรวจสอบว่ามี Collider อยู่หรือไม่
        if (colliders == null || colliders.Count == 0)
        {
            colliders = new List<Collider>(GetComponentsInChildren<Collider>());
        }
    }

    void Update()
    {
        // อัปเดต Collider ทุกเฟรมตามอนิเมชั่น
        foreach (var data in frameData)
        {
            if (currentFrame == data.frame)
            {
                UpdateColliders(data.size, data.center);
            }
        }

        // เพิ่มการควบคุมเฟรมที่นี่ ถ้าต้องการให้เปลี่ยนเฟรมตามเวลา
        currentFrame++;
    }
    private void UpdateColliders(Vector3 size, Vector3 center)
    {
        foreach (var col in colliders)
        {
            if (col is BoxCollider boxCol)
            {
                boxCol.size = size;
                boxCol.center = center;
            }
            // เพิ่มการจัดการสำหรับ Collider อื่น ๆ เช่น SphereCollider หรือ CapsuleCollider ได้ที่นี่
        }
    }
}
