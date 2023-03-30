using System;
using System.Collections;           


namespace MetroCardManagement
{
    public partial class CustomList<Type>:IEnumerable,IEnumerator
    {
        int i;
        public IEnumerator GetEnumerator()
        {
            i=-1;
            return(IEnumerator) this;
        }

        public bool MoveNext()
        {
            if(i<_count-1)
            {
                i++;
                return true;
            }
            Reset();
            return false;
            
        }

        public object Current{get{return _array[i];}}

        public void Reset()
        {
            i=-1;
        }
        
    }
}