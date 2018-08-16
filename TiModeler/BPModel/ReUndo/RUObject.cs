namespace Thyt.TiPLM.CLT.TiModeler.BPModel.ReUndo
{
    using System;

    [Serializable]
    public abstract class RUObject
    {
        protected RUObject()
        {
        }

        public virtual void Redo()
        {
        }

        public virtual void Undo()
        {
        }
    }
}

