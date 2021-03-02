using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace ApureEasing
{
    [UnitTitle("WhenAny")]
    [UnitCategory("ApureEasing")]
    public class WhenAnyNode : Unit
    {
        [Serialize]
        private int count;

        [DoNotSerialize]
        [Inspectable]
        [UnitHeaderInspectable("Count")]
        public int Count
        {
            get => Mathf.Max(2, count);
            set => count = Mathf.Max(value, 2);
        }

        [DoNotSerialize]
        public List<ControlInput> enter { get; private set; }

        [DoNotSerialize]
        public ControlInput reset { get; private set; }

        [DoNotSerialize]
        public ControlOutput next { get; private set; }

        private bool _called;

        protected override void Definition()
        {
            next = ControlOutput(nameof(next));

            reset = ControlInput(nameof(reset), Reset);

            enter = new List<ControlInput>();
            _called = false;
            for (var i = 0; i < Count; i++)
            {
                var i1 = i;
                var input = ControlInput($"{i}", _ => Run(i1 ,_));
                enter.Add(input);
                Succession(input, next);
            }
        }

        private ControlOutput Reset(Flow flow)
        {
            _called = false;
            return null;
        }

        private ControlOutput Run(int i, Flow flow)
        {
            if (_called) return null;

            _called = true;
            var reference = flow.stack.ToReference();
            Flow.New(reference).StartCoroutine(next);

            return null;
        }
    }
}