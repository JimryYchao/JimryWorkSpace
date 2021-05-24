using System;
using System.Collections;
using System.Drawing;


namespace JimryYchao
{
    class Font
    {
        string FontName; //4 bytes
        int size;        //4 bytes
        Color color;     //4 bytes
    }

    public class Charactor
    //单个字符（内部指针大小8+22+4（Font指针）+2的内存补齐）= 36bytes
    {
        char c;//2 bytes
        Font f;//20 bytes
    }
    class System
    {
        static void Main(string[] args)
        {
            ArrayList list = new ArrayList(100000);
            for (int i = 0; i < list.Count; i++)
            {
                //36*100000 = 3600k---> 3m 预估会占有犹存大小
                Charactor c = new Charactor();
                list.Add(c);
            }
        }
    }
}
