namespace Thyt.TiPLM.UIL.Resource.Common.NEWUC
{
    using System;
    using System.Collections;

    public class DECusRelationLst
    {
        private ArrayList arrItems = new ArrayList();
        private int mCount;

        public bool Add(DECusRelation Item)
        {
            this.arrItems.Add(Item);
            return true;
        }

        public bool Clear()
        {
            this.arrItems.Clear();
            return true;
        }

        public DECusRelation Item(int Index) {
            return ((DECusRelation)this.arrItems[Index]);
        }
        public bool Remove(int Index)
        {
            this.arrItems.Remove(Index);
            return true;
        }

        public int Count
        {
            get
            {
                this.mCount = this.arrItems.Count;
                return this.mCount;
            }
        }
    }
}

