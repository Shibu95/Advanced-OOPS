using System;
using System.Collections;

namespace MetroCardManagement
{
    public partial class CustomList<Type>
    {
        private int _count;
        private int _size;
        public int Count { get { return _count; } }
        public int Size { get { return _size; } }

        private Type[] _array;

        public Type this[int index]
        {
            get { return _array[index]; }
            set { _array[index] = value; }
        }

        public CustomList()
        {
            _count = 0;
            _size = 4;
            _array = new Type[_size];
        }

        public CustomList(int size)
        {
            _count=0;
            _size=size;
            _array=new Type[size];
        }

        public void Add(Type data)
        {
            if(_count==_size)
            {
                GrowSize();
            }
            _array[_count]=data;
            _count++;
        }

        public void GrowSize()
        {
            _size*=2;
            Type[] temp=new Type[_size];
            for(int i=0;i<_count;i++)
            {
                temp[i]=_array[i];
            }
            _array= temp;
        }

        public void AddRange(CustomList<Type> dataList)
        {
            _size=_size+dataList.Count+4;
            Type[] temp= new Type[_size];
            for(int i=0;i<_count;i++)
            {
                temp[i]=_array[i];
            }
            int k=0;
            for(int j=_count;j<_count+dataList.Count;j++)
            {
                temp[j]=dataList[k];
                k++;
            }
            _array=temp;
            _count+=dataList.Count;
        }

    }
}