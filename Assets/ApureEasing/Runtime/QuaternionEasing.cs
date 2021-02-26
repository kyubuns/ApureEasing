using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace ApureEasing
{
    [UnitTitle("Quaternion Easing")]
    [UnitCategory("ApureEasing")]
    public class QuaternionEasingNode : Unit
    {
        [DoNotSerialize]
        public ControlInput start { get; private set; }

        [DoNotSerialize]
        public ControlOutput tick { get; private set; }

        [DoNotSerialize]
        public ValueOutput value { get; private set; }

        [DoNotSerialize]
        public ControlOutput complete { get; private set; }

        [DoNotSerialize]
        public ValueInput easing { get; private set; }

        [DoNotSerialize]
        public ValueInput duration { get; private set; }

        [DoNotSerialize]
        public ValueInput endValue { get; private set; }

        [DoNotSerialize]
        public ValueInput startValue { get; private set; }

        private float startTime;
        private float cachedDuration;
        private Quaternion cachedStartValue;
        private Quaternion cachedEndValue;
        private Easing cachedEasing;

        protected override void Definition()
        {
            start = ControlInputCoroutine(nameof(start), RunCoroutine);
            tick = ControlOutput(nameof(tick));
            complete = ControlOutput(nameof(complete));
            easing = ValueInput(nameof(easing), Easing.Linear);
            duration = ValueInput(nameof(duration), 1f);
            startValue = ValueInput(nameof(startValue), Quaternion.identity);
            endValue = ValueInput(nameof(endValue), Quaternion.identity);
            value = ValueOutput(nameof(value), GetOutput);
        }

        private IEnumerator RunCoroutine(Flow flow)
        {
            cachedDuration = flow.GetValue<float>(duration);
            cachedStartValue = flow.GetValue<Quaternion>(startValue);
            cachedEndValue = flow.GetValue<Quaternion>(endValue);
            cachedEasing = flow.GetValue<Easing>(easing);
            startTime = Time.time;

            while (startTime + cachedDuration > Time.time)
            {
                flow.Invoke(tick);
                yield return null;
            }
            flow.Invoke(tick);
            yield return complete;
        }

        private Quaternion GetOutput(Flow flow)
        {
            if (!(startTime + cachedDuration > Time.time)) return cachedEndValue;
            var v = (Time.time - startTime) / cachedDuration;
            return Quaternion.Lerp(cachedStartValue, cachedEndValue, EasingConvert.Get(cachedEasing, v));
        }
    }
}