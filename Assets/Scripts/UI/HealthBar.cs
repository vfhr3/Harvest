using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HealthComponent health;
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private float speed;
        private float _targetValue;
        
        public void Initialize(HealthComponent healthComponent)
        {
            health ??= healthComponent;
            health.OnHealthChanged += HandleHealthChanged;

            _targetValue = (float)health.Current / health.Max;
            scrollbar.value = _targetValue;
        }

        private void HandleHealthChanged(int current)
        {
            _targetValue = (float)health.Current / health.Max;
        }

        private void OnDestroy()
        {
            if (health != null)
            {
                health.OnHealthChanged -= HandleHealthChanged;
            }
        }

        private void Update()
        {
            if (!Mathf.Approximately(scrollbar.value, _targetValue))
            {
                scrollbar.value = Mathf.MoveTowards(scrollbar.value, _targetValue, speed * Time.deltaTime);
            }
        }
    }
}