using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace ApureEasing
{
    [UnitTitle("Vector3Easing")]
    [UnitCategory("ApureEasing")]
    public class Vector3EasingNode : Unit
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
            startValue = ValueInput(nameof(startValue), Vector3.zero);
            endValue = ValueInput(nameof(endValue), Vector3.one);
            value = ValueOutput(nameof(value), GetOutput);
        }

        IEnumerator RunCoroutine(Flow flow)
        {
            var d = flow.GetValue<float>(duration);
            startTime = Time.time;

            while (startTime + d > Time.time)
            {
                yield return tick;
                yield return null;
            }
            yield return tick;
            flow.Invoke(complete);
        }

        private Vector3 GetOutput(Flow flow)
        {
            var t = flow.GetValue<Easing>(easing);
            var s = flow.GetValue<Vector3>(startValue);
            var e = flow.GetValue<Vector3>(endValue);
            var d = flow.GetValue<float>(duration);

            if (!(startTime + d > Time.time)) return e;
            var v = (Time.time - startTime) / d;
            return Vector3.Lerp(s, e, EasingConvert.Get(t, v));
        }
    }
}