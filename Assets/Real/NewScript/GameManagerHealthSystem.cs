using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerHealthSystem : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public GameObject playerObject;
        public ScriptableHealth playerHealth;
        public Slider healthSlider;
    }

    public List<PlayerData> players = new List<PlayerData>();

    void Start()
    {
        foreach (var player in players)
        {
            if (player.playerObject.activeSelf)
            {
                player.healthSlider.maxValue = player.playerHealth.maxHealth;
                player.healthSlider.value = player.playerHealth.currentHealth;
            }
        }
    }

    void Update()
    {
        UpdateHealthSliders();
    }

    private void UpdateHealthSliders()
    {
        foreach (var player in players)
        {
            if (player.playerObject.activeSelf && player.healthSlider.value != player.playerHealth.currentHealth)
            {
                player.healthSlider.value = player.playerHealth.currentHealth;
            }
        }
    }
}