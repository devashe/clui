using System;
using System.Collections.Generic;
using System.Text;

namespace DeVashe.CluiLib
{
    public partial class CluiMenuItem<T>
    {
        public CluiMenuItem(string label, T data, Action<T, Clui> handler)
        {
            Label = label ?? throw new ArgumentNullException(nameof(label));
            Data = data;
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public string Label { get; set; }
        public T Data { get; set; }
        public Action<T, Clui> Handler { get; set; }
    }
}
