using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace ApureEasing
{
    [UnitTitle("WaitAll")]
    [UnitCategory("ApureEasing")]
    public class WaitAllNode : Unit
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
        private List<bool> _counter = new List<bool>();

        protected override void Definition()
        {
            next = ControlOutput(nameof(next));

            reset = ControlInput(nameof(reset), Reset);

            enter = new List<ControlInput>();
            _called = false;
            _counter = new List<bool>();
            for (var i = 0; i < Count; i++)
            {
                var i1 = i;
                var input = ControlInput($"{i}", _ => Run(i1 ,_));
                enter.Add(input);
                _counter.Add(false);
                Succession(input, next);
            }
        }

        private ControlOutput Reset(Flow flow)
        {
            _called = false;
            for (var i = 0; i < _counter.Count; ++i) _counter[i] = false;
            return null;
        }

        private ControlOutput Run(int i, Flow flow)
        {
            if (_called) return null;

            _counter[i] = true;

            if (_counter.All(x => x))
            {
                _called = true;
                var reference = flow.stack.ToReference();
                Flow.New(reference).StartCoroutine(next);
            }

            return null;
        }
    }
}