
using Unity.VisualScripting;
using UnityEngine;

namespace BWV
{

    public class DayNightManager : MonoBehaviour
    {
        public delegate void DayNightShiftEvent(bool isNight);
        public static event DayNightShiftEvent OnDayNightShift;

        public bool continuousCycle = true;
        public Light moon;
        public Light sun;
        public float secondsInFullDay = 60f;
        [Range(0, 1)] public float currentTimeOfDay = 0;
        public float timeMultiplier = 1f;
        public bool isRunning = true;
        public float startingTime = 0f;

        [SerializeField] private Gradient sunColorGradient;
        private float sunInitialIntensity;
        private float sunInitialY;
        private bool isNight;

        private void Start()
        {
            sunInitialIntensity = sun.intensity;
            //currentTimeOfDay = startingTime / secondsInFullDay;
            sunInitialY = sun.transform.localRotation.eulerAngles.y;
        }

        private void Update()
        {
            if (!continuousCycle) return; 

            if (isRunning && GameState.IsInGame)
            {
                UpdateSun();

                currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

                if (currentTimeOfDay >= 1)
                {
                    currentTimeOfDay = 0;
                }
            }
        }

        private void UpdateSun()
        {
            sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, sunInitialY, 0);

            float intensityMultiplier = 1;

            if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
            {
               
                intensityMultiplier = 0;
            }
            else if (currentTimeOfDay <= 0.25f)
            {
                if (isNight)
                {
                    isNight = false;
                    OnDayNightShift(false);
                }
                intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
            }
            else if (currentTimeOfDay >= 0.73f)
            {
                if (!isNight)
                {
                    isNight = true;
                    OnDayNightShift(true);
                }
                intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
            }
           
            sun.intensity = sunInitialIntensity * intensityMultiplier;
            moon.intensity = 1 - sun.intensity;
        }

        public void StartCycle()
        {
            isRunning = true;
        }

        public void StopCycle()
        {
            isRunning = false;
        }

        public float GetTimeInHours()
        {
            return Mathf.FloorToInt(currentTimeOfDay * 24);
        }

   
    }
}