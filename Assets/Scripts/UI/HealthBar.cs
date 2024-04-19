using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turnpturn.Datas;
using Turnpturn.Game.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Turnpturn.UI
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name;
        [SerializeField]
        private float _durationTime = 1f;
        private Slider _healthSlider;
        private Unit _targetUnit;

        private void OnDisable()
        {
            _targetUnit.OnHealthModified -= ModifyHealth;
            //_targetUnit.OnAction -= ModifyHealth;
        }
    

        // Update is called once per frame
        void Update()
        {

        }

        public void SetupHealthBar(Unit unit)
        {
            _healthSlider = GetComponent<Slider>();
            _targetUnit = unit;
            _healthSlider.maxValue = unit.UnitCarac.MaxHealth;
            _healthSlider.value = unit.CurrentHP;
            _name.text = $"{unit.UnitName} ({unit.UnitElement.ElementName})";
            unit.OnHealthModified += ModifyHealth;
            //unit.OnAction += ModifyHealth;
        }

        public void LooseHealth(int amount)
        {
            _healthSlider.value -= amount;
        }
        public void ModifyHealth()
        {
            StartCoroutine(SlowlyModifyHealthBarValue(_targetUnit.CurrentHP, _durationTime));
        }
        private IEnumerator SlowlyModifyHealthBarValue(int targetValue, float duration)
        {
            float startValue = _healthSlider.value;
            float time = 0;
            while (time < duration)
            {
                _healthSlider.value = Mathf.Lerp(startValue, targetValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            _healthSlider.value = targetValue;
            _targetUnit.TargetActionComplete();
        }
    }
}