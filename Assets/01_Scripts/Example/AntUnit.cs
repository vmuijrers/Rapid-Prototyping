using UnityEngine;

namespace Example
{
    public class AntUnit : BaseUnit
    {
        public Animator animator { get; private set; }
        public Vector3 TargetPosition { get; set; }
        public GameObject TargetFood { get; set; }

        [SerializeField] private float hungerDamagePerSecond = 5f;
        protected override void Awake()
        {
            base.Awake();
            animator = GetComponentInChildren<Animator>();
        }

        protected override void Update()
        {
            health.TakeDamage(hungerDamagePerSecond * Time.deltaTime);
        }

        public void EatFood(GameObject food)
        {
            food.GetComponent<HealthComponent>().TakeDamage(10);
            health.Health += 10;
        }
    }

}


