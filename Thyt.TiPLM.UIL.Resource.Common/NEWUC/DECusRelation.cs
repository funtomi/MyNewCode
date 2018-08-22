namespace Thyt.TiPLM.UIL.Resource.Common.NEWUC
{
    using System;

    public class DECusRelation
    {
        private DateTime _PLM_CREATETIME;
        private string _PLM_CREATOR;
        private string _PLM_LEFTCLASS;
        private Guid _PLM_LEFTOBJ;
        private Guid _PLM_OID;
        private int _PLM_ORDER;
        private string _PLM_RIGHTCLASS;
        private Guid _PLM_RIGHTOBJ;
        private int _PLM_RIGHTREVISION;
        private string _PLM_VIEW;

        public DateTime PLM_CREATETIME
        {
            get{
               return this._PLM_CREATETIME;
            }set
            {
                this._PLM_CREATETIME = value;
            }
        }

        public string PLM_CREATOR
        {
            get{
               return this._PLM_CREATOR;
            }set
            {
                this._PLM_CREATOR = value;
            }
        }

        public string PLM_LEFTCLASS
        {
            get { 
               return this._PLM_LEFTCLASS;
            }set
            {
                this._PLM_LEFTCLASS = value;
            }
        }

        public Guid PLM_LEFTOBJ
        {
            get{
               return this._PLM_LEFTOBJ;
            }set
            {
                this._PLM_LEFTOBJ = value;
            }
        }

        public Guid PLM_OID
        {
            get { 
               return this._PLM_OID;
            }set
            {
                this._PLM_OID = value;
            }
        }

        public int PLM_ORDER
        {
            get {
               return this._PLM_ORDER;
            }set
            {
                this._PLM_ORDER = value;
            }
        }

        public string PLM_RIGHTCLASS
        {
            get { 
               return this._PLM_RIGHTCLASS;
            }set
            {
                this._PLM_RIGHTCLASS = value;
            }
        }

        public Guid PLM_RIGHTOBJ
        {
            get{
              return  this._PLM_RIGHTOBJ;
            }set
            {
                this._PLM_RIGHTOBJ = value;
            }
        }

        public int PLM_RIGHTREVISION
        {
            get { 
               return this._PLM_RIGHTREVISION;
            }set
            {
                this._PLM_RIGHTREVISION = value;
            }
        }

        public string PLM_VIEW
        {
            get {
                return this._PLM_VIEW;
            }
            set
            {
                this._PLM_VIEW = value;
            }
        }
    }
}

