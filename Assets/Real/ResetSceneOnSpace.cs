using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneOnSpace : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // โหลดซีนปัจจุบันอีกครั้ง
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
