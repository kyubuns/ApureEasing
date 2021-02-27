using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace ApureEasing
{
    [UnitTitle("Vector2 Easing")]
    [UnitCategory("ApureEasing")]
    public class Vector2EasingNode : Unit
    {
        [DoNotSerialize]
        public ControlInput start { get; private set; }

        [DoNotSerialize]
        public ControlOutput complete { get; private set; }

        [DoNotSerialize]
        public ControlOutput tick { get; private set; }

        [DoNotSerialize]
        public ValueOutput value { get; private set; }

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
        private Vector2 cachedStartValue;
        private Vector2 cachedEndValue;
        private Easing cachedEasing;

        protected override void Definition()
        {
            start = ControlInputCoroutine(nameof(start), RunCoroutine);
            complete = ControlOutput(nameof(complete));
            tick = ControlOutput(nameof(tick));
            easing = ValueInput(nameof(easing), Easing.Linear);
            duration = ValueInput(nameof(duration), 1f);
            startValue = ValueInput(nameof(startValue), Vector2.zero);
            endValue = ValueInput(nameof(endValue), Vector2.one);
            value = ValueOutput(nameof(value), GetOutput);
        }

        private IEnumerator RunCoroutine(Flow flow)
        {
            cachedDuration = flow.GetValue<float>(duration);
            cachedStartValue = flow.GetValue<Vector2>(startValue);
            cachedEndValue = flow.GetValue<Vector2>(endValue);
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

        private Vector2 GetOutput(Flow flow)
        {
            if (!(startTime + cachedDuration > Time.time)) return cachedEndValue;
            var v = (Time.time - startTime) / cachedDuration;
            return Vector2.Lerp(cachedStartValue, cachedEndValue, EasingConvert.Get(cachedEasing, v));
        }
    }
}