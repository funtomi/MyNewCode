
    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Project2;
    using Thyt.TiPLM.PLL.Project2;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public class ProjectInput : UltraTextEditor
    {
        private string _locValue = "";
        private bool b_ReadOnly;
        private Container components;
        private DEProject curInput;
        private ProjectDropListHandler dlhandler;
        private UCProjectPicker ucUser;

        public event ProjectDropListHandler DropListChanged;

        public event SelectProjectHandler ProjectTextChanged;

        public ProjectInput()
        {
            this.InitializeComponent();
            this.InitializeConfig();
        }

        protected override void Dispose(bool disposing)
        {
            if (this.dlhandler != null)
            {
                this.ucUser.ProjectSelected -= this.dlhandler;
            }
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public Guid GetProjectValue()
        {
            if (this.Text.Trim().Length == 0)
            {
                return Guid.Empty;
            }
            return this.curInput.Moid;
        }

        private void InitializeComponent()
        {
            DropDownEditorButton button = new DropDownEditorButton("SelectProject") {
                RightAlignDropDown = DefaultableBoolean.False
            };
            base.ButtonsRight.Add(button);
            base.NullText = "";
            this.b_ReadOnly = true;
            base.Size = new Size(100, 0x15);
        }

        private void InitializeConfig()
        {
            this.ucUser = new UCProjectPicker(this.curInput);
            DropDownEditorButton button = base.ButtonsRight["SelectProject"] as DropDownEditorButton;
            button.Control = this.ucUser;
            this.dlhandler = new ProjectDropListHandler(this.ucUser_ProjectSelected);
            this.ucUser.ProjectSelected += this.dlhandler;
            base.BeforeEditorButtonDropDown += new BeforeEditorButtonDropDownEventHandler(this.ProjectCombo_BeforeDropDown);
            this.AllowDrop = true;
            base.TextChanged += new EventHandler(this.ProjectCombo_TextChanged);
            base.KeyUp += new KeyEventHandler(this.ProjectComboInput_KeyUp);
        }

        private void ProjectCombo_BeforeDropDown(object sender, BeforeEditorButtonDropDownEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (base.Width > 0x113)
            {
                if (this.ucUser != null)
                {
                    this.ucUser.Width = base.Width;
                }
            }
            else if (this.ucUser != null)
            {
                this.ucUser.Width = 0x110;
            }
            if (this.ucUser != null)
            {
                this.ucUser.ReLoad(this.curInput);
            }
            Cursor.Current = Cursors.Default;
        }

        private void ProjectCombo_TextChanged(object sender, EventArgs e)
        {
            if (this.ProjectTextChanged != null)
            {
                this.ProjectTextChanged(this.curInput);
            }
        }

        private void ProjectComboInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.b_ReadOnly)
            {
                this.Text = this._locValue;
            }
        }

        private void SetEditText(DEProject prjObj)
        {
            if (prjObj != null)
            {
                this._locValue = prjObj.Name + "(" + prjObj.ID + ")";
                this.Text = prjObj.Name + "(" + prjObj.ID + ")";
            }
        }

        public void SetInput(Guid g_oid)
        {
            DEProject projectByMasterOid = PLProject.Agent.GetProjectByMasterOid(ClientData.LogonUser.Oid, g_oid);
            if (projectByMasterOid == null)
            {
                PLProject project2 = new PLProject();
                foreach (DEProject project3 in project2.GetAllProjects(ClientData.LogonUser.Oid))
                {
                    if (project3.Moid == g_oid)
                    {
                        projectByMasterOid = project3;
                        break;
                    }
                }
            }
            this.curInput = projectByMasterOid;
            this.SetEditText(projectByMasterOid);
        }

        private void ucUser_ProjectSelected(DEProject prjObj)
        {
            bool flag = false;
            if (base.Tag == null)
            {
                if (this.curInput != null)
                {
                    if (prjObj.ProductOid != this.curInput.ProductOid)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
            }
            this.curInput = prjObj;
            this.SetEditText(prjObj);
            if (flag && (this.DropListChanged != null))
            {
                this.DropListChanged(prjObj);
                base.CloseEditorButtonDropDowns();
            }
        }
    }
}

