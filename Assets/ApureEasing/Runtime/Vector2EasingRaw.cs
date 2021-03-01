using Unity.VisualScripting;
using UnityEngine;

namespace ApureEasing
{
    [UnitTitle("Vector2 Easing Raw")]
    [UnitCategory("ApureEasing")]
    public class Vector2EasingRawNode : Unit
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
            startValue = ValueInput(nameof(startValue), Vector2.zero);
            endValue = ValueInput(nameof(endValue), Vector2.one);
            value = ValueOutput(nameof(value), GetOutput);
            Requirement(input, value);
            Requirement(easing, value);
            Requirement(startValue, value);
            Requirement(endValue, value);
        }

        private Vector2 GetOutput(Flow flow)
        {
            var v = flow.GetValue<float>(input);
            var t = flow.GetValue<Easing>(easing);
            var s = flow.GetValue<Vector2>(startValue);
            var e = flow.GetValue<Vector2>(endValue);

            return Vector2.Lerp(s, e, EasingConvert.Get(t, v));
        }
    }
}