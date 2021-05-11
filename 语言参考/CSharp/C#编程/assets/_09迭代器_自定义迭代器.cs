using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp7
{
    class CustomList : IEnumerable, IEnumerator
    {
        private int[] List;
        public CustomList(int[] arr)
        {
            if (arr != null)
            {
                List = arr;
            }
           
        }

        private int position = -1;
        public IEnumerator GetEnumerator()//实现IEnumerable
        {
            Reset();//复原光标
            return this;//继承了IEnumerator，返回自己//返回IEnumerator

        }
        public bool MoveNext()//方法表示光标移动到下一个元素，并判断光标是否溢出
        {
            ++position;//表示光标
            return position < List.Length;//当溢出时，表示当前遍历对象以达到最大光标位置。
        }
        public object Current => List[position];//当MoveNext返回true时，返回当前光标所在元素的值。

        public void Reset()//重新调用遍历时将光标复原，在GetEnumerator方法中调用。
        {
            position = -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CustomList list = new CustomList(new int[] { 1, 5, 9, 6, 4, 7, 85, 5, 5 });
            foreach (var item in list)
            {
                Console.WriteLine(item); 
            }
        }
    }
}
