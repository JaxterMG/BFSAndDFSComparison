using System;
using System.IO;

namespace AisdLab3
{
    
    class BinaryTreeNode
    {
        public BinaryTreeNode(int value)
        {
            Value = value;
        }

        public BinaryTreeNode Left { get; set; }
        public BinaryTreeNode Right { get; set; }
        public int Value { get; private set; }   
    }
    public class BinaryTree
    {
        private BinaryTreeNode _head;
        public int _count = 0;

        public void Add(int value)
        {
            if (_head == null)
            {
                _head = new BinaryTreeNode(value);
            }
            else
            {
                AddTo(_head, value);
            }
        }

        // Рекурсивная вставка.
        private void AddTo(BinaryTreeNode node, int value)
        {
            if (node.Value > value)
            {
                if (node.Left == null)
                {

                    node.Left = new BinaryTreeNode(value);

                }
                else
                {
                    AddTo(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode(value);
                }
                else
                {
                    AddTo(node.Right, value);
                }
            }
        }
        public void StartSeatchInWidth(int Value)
        {
            BinaryTreeNode _node = _head;
            if(_node.Value == Value)
            {
                return;
            }
            SearchInWidth(_node, Value);
        }
        private void SearchInWidth(BinaryTreeNode _node, int Value)
        {
            
            if(_node.Left!=null && _node.Right!=null)
            {
                if (_node.Left.Value != Value && _node.Right.Value != Value)
                {
                    SearchInWidth(_node.Left, Value);
                    SearchInWidth(_node.Right, Value);
                }
                else return;

            }
            else
            {
                return;
            }
        }
        public  void StartSeatchInDepth(int Value)
        {
           BinaryTreeNode _node = _head;
           SearchInDepth(_node, Value);
        }

        private void SearchInDepth(BinaryTreeNode _node, int Value )
        {
            
            
            if (_node.Left != null)
            {
                if (Value == _node.Left.Value)
                {
                    return ;
                }
                else
                {
                    SearchInDepth(_node.Left, Value);
                }
            }
            if(_node.Right!=null)
            {
                if (Value == _node.Right.Value)
                {
                    return ;
                }
                else
                {
                    SearchInDepth(_node.Right, Value);
                }
            }
            return ;
        }
      
        

      

        public int Count
        {
            get;
        }
    }
    


    class Program
    {
        static Random rand = new Random();
        static int ChooseValueToSearch(int[] _values)
        {
            int temp = rand.Next(0,_values.Length-1);
            int _value = _values[temp];

            return _value;
        }

        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            int sortAmount = 1000;
            int maxArr = 1000;
            int step = 10;
            double[] time1 = new double[maxArr];
            double[] time2 = new double[maxArr];
            int k = 0;

            for (int i = 10; i <= maxArr; i += step)
            {


                for (int j = 0; j <= sortAmount; j++)
                {
                    int _valueToSearch;
                    int[] _randomNums = new int[i];
                    BinaryTree _tree = new BinaryTree();
                    for (int o = 0; o < _randomNums.Length; o++)
                    {
                        _randomNums[o] = rand.Next(0, i);
                        _tree.Add(_randomNums[0]);
                    }
                    _valueToSearch = ChooseValueToSearch(_randomNums);
                    
                    myStopwatch.Start(); //запуск
                    _tree.StartSeatchInDepth(_valueToSearch);
                    myStopwatch.Stop(); //остановить
                    time1[k] += myStopwatch.Elapsed.TotalSeconds;
                    myStopwatch.Reset();

                    myStopwatch.Start(); //запуск
                    _tree.StartSeatchInWidth(_valueToSearch);
                    myStopwatch.Stop(); //остановить

                    time2[k] += myStopwatch.Elapsed.TotalSeconds;
                    myStopwatch.Reset();
                }
                Console.WriteLine("Iteration " + (k + 1));
                Console.WriteLine("RunTime quicksort " + time1[k] / sortAmount);
                Console.WriteLine("RunTime combsort " + time2[k] / sortAmount);
                ++k;


            }

            using (StreamWriter sw = new StreamWriter(File.OpenWrite("Data.txt")))
            {

                for (k = 0; k < (maxArr / step); k++)
                {
                    sw.WriteLine("{0}\t{1}\t{2}", 10 * k + 10, time1[k] / sortAmount, time2[k] / sortAmount);
                }
            }

            Console.WriteLine("File has been filled ");
            Console.ReadLine();
           
           
        }
    }
}
