    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Project2;
    using Thyt.TiPLM.PLL.Project2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public class ProjectInputPLM : PopupContainerEditPLM
    {
        private string _locValue = "";
        private bool b_ReadOnly;
        private Container components;
        private DEProject curInput;
        private ProjectDropListHandler dlhandler;
        private PopupContainerControl popupContainer;
        private UCProjectPicker ucUser;

        public event ProjectDropListHandler DropListChanged;

        public event SelectProjectHandler ProjectTextChanged;

        public ProjectInputPLM()
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
            base.Properties.TextEditStyle = TextEditStyles.Standard;
            this.popupContainer = new PopupContainerControl();
            base.Properties.PopupControl = this.popupContainer;
            this.NullText = "";
            this.b_ReadOnly = true;
            base.Size = new Size(100, 0x15);
        }

        private void InitializeConfig()
        {
            this.ucUser = new UCProjectPicker(this.curInput);
            this.popupContainer.Controls.Add(this.ucUser);
            base.Properties.PopupControl.Size = new Size(base.Width, this.ucUser.Height);
            this.ucUser.Dock = DockStyle.Fill;
            this.dlhandler = new ProjectDropListHandler(this.ucUser_ProjectSelected);
            this.ucUser.ProjectSelected += this.dlhandler;
            this.QueryPopUp += new CancelEventHandler(this.ProjectCombo_QueryPopUp);
            this.AllowDrop = true;
            base.TextChanged += new EventHandler(this.ProjectCombo_TextChanged);
            base.KeyUp += new KeyEventHandler(this.ProjectComboInput_KeyUp);
        }

        private void ProjectCombo_QueryPopUp(object sender, CancelEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            base.Properties.PopupControl.Width = base.Width;
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
                this.ClosePopup();
            }
        }

        public string NullText
        {
            get {
                return
                base.Properties.NullText;
            }
            set
            {
                base.Properties.NullText = value;
            }
        }
    }
}

