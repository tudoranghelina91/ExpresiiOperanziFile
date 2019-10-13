using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpresiiOperanziFile
{
    class Program
    {
        public static int Evaluate(MathTreeNode root)
        {
            if (root.Value == '*')
                return Evaluate(root.Left) * Evaluate(root.Right);
            else if (root.Value == '+')
                return Evaluate(root.Left) + Evaluate(root.Right);
            else if (char.IsLetter(root.Value))
            {
                return root.Value;
            }

            else
            {
                return Convert.ToInt32(root.Value.ToString());
            }
        }

        public static void PreOrder(MathTreeNode currentNode)
        {
            Console.Write($"{currentNode.Value} ");
            if (currentNode.Left != null)
            {
                PreOrder(currentNode.Left);
            }


            if (currentNode.Right != null)
            {
                PreOrder(currentNode.Right);
            }

        }

        public static string ShuntingYard(string infix)
        {
            Queue<char> outputQueue = new Queue<char>();
            Stack<char> operatorStack = new Stack<char>();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < infix.Length; i++)
            {
                if (char.IsLetterOrDigit(infix[i]))
                {
                    outputQueue.Enqueue(infix[i]);
                }

                if (infix[i] == '*' || infix[i] == '+')
                {
                    while (operatorStack.Count > 0 && infix[i] <= operatorStack.Peek() && operatorStack.Peek() != '(')
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }

                    operatorStack.Push(infix[i]);
                }

                if (infix[i] == '(')
                {
                    operatorStack.Push(infix[i]);
                }

                if (infix[i] == ')')
                {
                    while (operatorStack.Peek() != '(')
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }

                    if (operatorStack.Peek() == '(')
                    {
                        operatorStack.Pop();
                    }
                }
            }

            while (operatorStack.Count != 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            while (outputQueue.Count != 0)
            {
                sb.Append(outputQueue.Dequeue());
            }

            return sb.ToString();
        }

        public static MathTreeNode BuildTree(string pattern)
        {
            Stack<MathTreeNode> mathTreeNodes = new Stack<MathTreeNode>();
            for (int i = 0; i < pattern.Length; i++)
            {
                if (char.IsLetterOrDigit(pattern[i]))
                {
                    mathTreeNodes.Push(new MathTreeNode(pattern[i]));
                }

                else
                {
                    MathTreeNode root = new MathTreeNode(pattern[i]);
                    root.Left = mathTreeNodes.Pop();
                    root.Right = mathTreeNodes.Pop();
                    mathTreeNodes.Push(root);
                }

            }
            return mathTreeNodes.Pop();
        }

        static string ReadFile(string path)
        {
            if (path != null)
            {
                StreamReader sr = new StreamReader(path);
                return sr.ReadLine();
            }

            return null;
        }



        static void Main(string[] args)
        {
            string pattern = ReadFile("Exemplul1.txt");
            string pattern2 = ReadFile("Exemplul2.txt");

            // expresia transformata din infix in postfix pentru cele doua siruri de intrare
            Console.WriteLine(ShuntingYard(pattern));
            Console.WriteLine(ShuntingYard(pattern2));

            string patternNew = ShuntingYard(pattern);
            string patternNew2 = ShuntingYard(pattern2);

            MathTreeNode root1 = BuildTree(patternNew);
            MathTreeNode root2 = BuildTree(patternNew2);

            Console.WriteLine("Rezultatul pentru root 1 - cifre");
            Console.WriteLine(Evaluate(root1));
            Console.WriteLine("Rezultatul pentru root 2 - litere");
            Console.WriteLine((char)Evaluate(root2));

            Console.ReadKey();
        }
    }
}