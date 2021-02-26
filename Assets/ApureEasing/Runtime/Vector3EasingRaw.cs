using Unity.VisualScripting;
using UnityEngine;

namespace ApureEasing
{
    [UnitTitle("Vector3 Easing Raw")]
    [UnitCategory("ApureEasing")]
    public class Vector3EasingRawNode : Unit
    {
        [DoNotSerialize]
        public ValueOutput value { get; private set; }

        [DoNotSerialize]
        public ValueInput input { get; private set; }

        [DoNotSerialize]
        public ValueInput easing { get; private set; }

        [DoNotSerialize]
        public ValueInput endValue { get; private set; }

        [DoNotSerialize]
        public ValueInput startValue { get; private set; }

        protected override void Definition()
        {
            input = ValueInput(nameof(input), 0f);
            easing = ValueInput(nameof(easing), Easing.Linear);
            startValue = ValueInput(nameof(startValue), Vector3.zero);
            endValue = ValueInput(nameof(endValue), Vector3.one);
            value = ValueOutput(nameof(value), GetOutput);
        }

        private Vector3 GetOutput(Flow flow)
        {
            var v = flow.GetValue<float>(input);
            var t = flow.GetValue<Easing>(easing);
            var s = flow.GetValue<Vector3>(startValue);
            var e = flow.GetValue<Vector3>(endValue);

            return Vector3.Lerp(s, e, EasingConvert.Get(t, v));
        }
    }
}