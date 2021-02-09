using System;

namespace DeVashe.CluiLib
{
    public class SimpleCliMenuItem
    {
        public SimpleCliMenuItem(string label, Action<SimpleCliMenuItem, Clui> handler)
        {
            Label = label;
            Handler = handler;
        }

        public string Label { get; set; }
        public Action<SimpleCliMenuItem, Clui> Handler { get; set; }
    }
}