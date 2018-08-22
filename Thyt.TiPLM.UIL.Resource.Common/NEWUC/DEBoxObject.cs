namespace Thyt.TiPLM.UIL.Resource.Common.NEWUC
{
    using System;

    public class DEBoxObject
    {
        private string _ClassName;
        private string _ConAttrShowPos;
        private string _Context;
        private string _ControlType;
        private string _FilterAttribue;
        private string _FilterPos;
        private int _FilterType;
        private int _FilterValuePos;
        private int _Height;
        private bool _IsConAttrType;
        private bool _IsReflex;
        private object _MetaObject;
        private string _RelationTable;
        private string _ShowPos;
        private string _ShowType;
        private int _Width;

        public string ClassName
        {
            get{
               return this._ClassName;
            }set
            {
                this._ClassName = value;
            }
        }

        public string ConAttrShowPos
        {
            get{
               return this._ConAttrShowPos;
            }set
            {
                this._ConAttrShowPos = value;
            }
        }

        public string ControlType
        {
            get{ 
               return this._ControlType;
            }set
            {
                this._ControlType = value;
            }
        }

        public string CONVALUE
        {
            get {
               return this._Context;
            }set
            {
                this._Context = value;
            }
        }

        public string FilterAttribue
        {
            get { 
               return this._FilterAttribue;
            }set
            {
                this._FilterAttribue = value;
            }
        }

        public string FilterPos
        {
            get{
               return this._FilterPos;
            }set
            {
                this._FilterPos = value;
            }
        }

        public int FilterType
        {
            get { 
               return this._FilterType;
            }set
            {
                this._FilterType = value;
            }
        }

        public int Height
        {
            get{
               return this._Height;
            }set
            {
                this._Height = value;
            }
        }

        public bool IsConAttrType
        {
            get { 
               return this._IsConAttrType;
            }set
            {
                this._IsConAttrType = value;
            }
        }

        public bool IsReflex
        {
            get{
              return  this._IsReflex;
            }set
            {
                this._IsReflex = value;
            }
        }

        public object MetaObject
        {
            get{ 
               return this._MetaObject;
            }set
            {
                this._MetaObject = value;
            }
        }

        public string RelationTableName
        {
            get{
               return this._RelationTable;
            }set
            {
                this._RelationTable = value;
            }
        }

        public string ShowPos
        {
            get { 
               return this._ShowPos;
            }set
            {
                this._ShowPos = value;
            }
        }

        public string ShowType
        {
            get{
               return this._ShowType;
            }set
            {
                this._ShowType = value;
            }
        }

        public int Width
        {
            get {
                return this._Width;
            }
            set
            {
                this._Width = value;
            }
        }
    }
}

