using UnityEngine;
using TMPro;

namespace BWV
{
    public class StatsPanel : MonoBehaviour
    {
        public TMP_Text[] valueStats;
        public TMP_Text[] descriptionStats;

        private void Start()
        {
            descriptionStats[0].text = "Gold";
            descriptionStats[1].text = "Happy";
            descriptionStats[2].text = "Favor";
            descriptionStats[3].text = "Title";
        }
        public void RefreshGoals(Goals playerStats) 
        {
            valueStats[0].text = playerStats.gold.ToString();
            valueStats[1].text = playerStats.happiness.ToString();
            valueStats[2].text = playerStats.favor.ToString();
            valueStats[3].text = playerStats.title.ToString();
        }
        public void Open()
        {
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}