using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpresiiOperanziFile
{
    class MathTreeNode
    {
        public char Value { get; private set; }
        public MathTreeNode Left { get; set; }
        public MathTreeNode Right { get; set; }

        public MathTreeNode(char value)
        {
            Value = value;
        }
    }
}
