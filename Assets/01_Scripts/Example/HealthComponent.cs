using UnityEngine;
using UnityEngine.Events;

namespace Example
{
    public class HealthComponent : MonoBehaviour
    {
        public UnityEvent<float> HealthChangedPercentageEvent;
        public UnityEvent<GameObject> DeathEvent;

        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float health = 100;
        public float Health
        {
            get { return health; }
            set
            {
                health = value;
                health = Mathf.Max(health, 0);
                HealthChangedPercentageEvent?.Invoke(Mathf.Clamp01(health/maxHealth));
            }
        }

        public void TakeDamage(float amount)
        {
            if(Health > 0)
            {
                Health -= amount;
                if(Health <= 0)
                {
                    DeathEvent?.Invoke(gameObject);
                }
            }
        }
    }

}


