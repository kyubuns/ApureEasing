using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ApureEasing
{
    [UnitTitle("Parallel")]
    [UnitCategory("ApureEasing")]
    [TypeIcon(typeof(Sequence))]
    public class ParallelNode : Unit
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
        public ControlInput enter { get; private set; }

        [DoNotSerialize]
        public List<ControlOutput> next { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Run);

            next = new List<ControlOutput>();
            for (var i = 0; i < Count; i++)
            {
                var output = ControlOutput($"{i}");
                next.Add(output);
                Succession(enter, output);
            }
        }

        private ControlOutput Run(Flow flow)
        {
            var reference = flow.stack.ToReference();
            foreach (var output in next)
            {
                Flow.New(reference).StartCoroutine(output);
            }
            return null;
        }
    }
}