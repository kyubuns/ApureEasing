using Unity.VisualScripting;
using UnityEngine;

namespace ApureEasing
{
    [UnitTitle("Quaternion Easing Raw")]
    [UnitCategory("ApureEasing")]
    public class QuaternionEasingRawNode : Unit
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
            startValue = ValueInput(nameof(startValue), Quaternion.identity);
            endValue = ValueInput(nameof(endValue), Quaternion.identity);
            value = ValueOutput(nameof(value), GetOutput);
            Requirement(input, value);
            Requirement(easing, value);
            Requirement(startValue, value);
            Requirement(endValue, value);
        }

        private Quaternion GetOutput(Flow flow)
        {
            var v = flow.GetValue<float>(input);
            var t = flow.GetValue<Easing>(easing);
            var s = flow.GetValue<Quaternion>(startValue);
            var e = flow.GetValue<Quaternion>(endValue);

            return Quaternion.Lerp(s, e, EasingConvert.Get(t, v));
        }
    }
}