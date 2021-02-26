using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace ApureEasing
{
    [UnitTitle("FloatEasing")]
    [UnitCategory("ApureEasing")]
    public class FloatEasingNode : Unit
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

        protected override void Definition()
        {
            start = ControlInputCoroutine(nameof(start), RunCoroutine);
            tick = ControlOutput(nameof(tick));
            complete = ControlOutput(nameof(complete));
            easing = ValueInput(nameof(easing), Easing.Linear);
            duration = ValueInput(nameof(duration), 1f);
            startValue = ValueInput(nameof(startValue), 0f);
            endValue = ValueInput(nameof(endValue), 1f);
            value = ValueOutput(nameof(value), GetOutput);
        }

        private IEnumerator RunCoroutine(Flow flow)
        {
            var d = flow.GetValue<float>(duration);
            startTime = Time.time;

            while (startTime + d > Time.time)
            {
                flow.Invoke(tick);
                yield return null;
            }
            flow.Invoke(tick);
            yield return complete;
        }

        private float GetOutput(Flow flow)
        {
            var t = flow.GetValue<Easing>(easing);
            var s = flow.GetValue<float>(startValue);
            var e = flow.GetValue<float>(endValue);
            var d = flow.GetValue<float>(duration);

            if (!(startTime + d > Time.time)) return e;
            var v = (Time.time - startTime) / d;
            return Mathf.Lerp(s, e, EasingConvert.Get(t, v));
        }
    }
}