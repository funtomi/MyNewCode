    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Common;
    using Thyt.TiPLM.PLL.Environment;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common {
    public partial class FrmNewID : FormPLM
    {
        
        private string className;
        
        private RevisionEffectivityWay effway;
        private Guid groupOid;
        private string id;
        private BusinessItemType itemType;
       
        private bool newId;
        
        private string revLabel;
        private int securityLevel;
       

        public FrmNewID(string oldId)
        {
            this.groupOid = Guid.Empty;
            this.InitializeComponent();
            if (oldId != null)
            {
                this.txtId.Text = oldId;
            }
            this.txtId.Enabled = false;
            this.btnecms.Enabled = false;
        }

        public FrmNewID(string oldID, BusinessItemType itemType, string className)
        {
            this.groupOid = Guid.Empty;
            this.InitializeComponent();
            this.txtId.Text = oldID;
            this.className = className;
            this.itemType = itemType;
            this.btnecms.Enabled = ConstCommon.FUNCTION_ECMS;
            this.txtId.SelectAll();
        }

        public FrmNewID(string oldId, BusinessItemType itemType, string className, bool whenCreating)
        {
            this.groupOid = Guid.Empty;
            this.InitializeComponent();
            this.txtId.Text = oldId;
            this.className = className;
            this.itemType = itemType;
            this.InitDisplayWhenCreateItem();
            this.btnecms.Enabled = ConstCommon.FUNCTION_ECMS;
            this.txtId.SelectAll();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.id = null;
        }

        private void btnecms_Click(object sender, EventArgs e)
        {
            FrmGetCode code = new FrmGetCode {
                ItemType = this.itemType,
                ClassName = this.className
            };
            if (code.ShowDialog() == DialogResult.OK)
            {
                this.txtId.Text = code.Id;
                this.txtId.Tag = code.CodeId;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.id = this.txtId.Text.Trim();
            if (this.id.Length == 0)
            {
                MessageBoxPLM.Show("请输入对象的新代号。");
                this.txtId.SelectAll();
            }
            else if (this.id.Length > 0x40)
            {
                MessageBoxPLM.Show("代号的长度不能超过64。");
                this.txtId.SelectAll();
            }
            else
            {
                if (this.itemType != BusinessItemType.Folder)
                {
                    if (this.cobSecurityLevel.SelectedIndex > -1)
                    {
                        this.securityLevel = Convert.ToInt32(this.cobSecurityLevel.SelectedItem);
                    }
                    else
                    {
                        DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.className);
                        if (class2 != null)
                        {
                            this.securityLevel = class2.SecurityLevel;
                        }
                        else
                        {
                            this.securityLevel = 1;
                        }
                    }
                    if ((this.cobGroup.SelectedItem != null) && (this.cobGroup.SelectedItem is DEOrganization))
                    {
                        this.groupOid = ((DEOrganization) this.cobGroup.SelectedItem).Oid;
                    }
                    else
                    {
                        this.groupOid = Guid.Empty;
                    }
                }
                this.revLabel = this.txtRevLabel.Text.Trim();
                this.effway = (this.cobFolderEffWay.SelectedIndex == 0) ? RevisionEffectivityWay.LastRev : RevisionEffectivityWay.PreciseIter;
                base.DialogResult = DialogResult.OK;
            }
        }
         
        private void FrmNewID_Load(object sender, EventArgs e)
        {
            if (this.itemType == BusinessItemType.Folder)
            {
                this.btnecms.Visible = false;
                this.txtId.CharacterCasing = CharacterCasing.Normal;
                if (this.effway != RevisionEffectivityWay.NA)
                {
                    this.cobFolderEffWay.Visible = true;
                    this.labEffway.Visible = true;
                    if (this.effway == RevisionEffectivityWay.PreciseIter)
                    {
                        this.cobFolderEffWay.SelectedIndex = 1;
                    }
                    else
                    {
                        this.cobFolderEffWay.SelectedIndex = 0;
                    }
                }
            }
            else if (this.itemType == BusinessItemType.Unknown)
            {
                this.btnecms.Visible = false;
                this.txtId.CharacterCasing = CharacterCasing.Normal;
                this.cobFolderEffWay.Visible = false;
            }
            else
            {
                if (PLSystemParam.ParameterIDCaseSensitive)
                {
                    this.txtId.CharacterCasing = CharacterCasing.Normal;
                }
                else
                {
                    this.txtId.CharacterCasing = CharacterCasing.Upper;
                }
                if (PLSystemParam.ParameterIDAutoGenerate)
                {
                    this.txtId.ReadOnly = true;
                    this.btnecms.Visible = false;
                    this.txtId.Text = PLItem.AutoGenerateID();
                }
                else if (!ConstCommon.FUNCTION_ECMS)
                {
                    this.btnecms.Visible = false;
                }
                else if (PLSystemParam.ParameterForcedUseCodeAsID)
                {
                    this.txtId.ReadOnly = true;
                }
            }
        }

        public static string GetNewId(string oldId, BusinessItemType itemType, string className, out string codeId)
        {
            FrmNewID wid = new FrmNewID(oldId, itemType, className) {
                newId = false
            };
            if (ServiceSwitches.IsTiIntegratorTopMost)
            {
                wid.TopMost = true;
            }
            wid.ShowDialog();
            codeId = wid.txtId.Tag as string;
            return wid.id;
        }

        public static string GetNewId(string oldId, BusinessItemType itemType, string className, RevisionEffectivityWay oldEff, out string codeId, out RevisionEffectivityWay newEff)
        {
            FrmNewID wid = new FrmNewID(oldId, itemType, className) {
                newId = false
            };
            if (ServiceSwitches.IsTiIntegratorTopMost)
            {
                wid.TopMost = true;
            }
            wid.effway = oldEff;
            wid.ShowDialog();
            codeId = wid.txtId.Tag as string;
            newEff = wid.effway;
            return wid.id;
        }

        private void InitDisplayWhenCreateItem()
        {
            base.Height = 0x70;
            this.pnlSecurity.Visible = true;
            base.Height += 0x60;
            PLUser user = new PLUser();
            int userSecurityByOid = user.GetUserSecurityByOid(ClientData.LogonUser.Oid);
            this.cobGroup.Properties.Items.Clear();
            object[] items = new object[userSecurityByOid];
            for (int i = 1; i <= userSecurityByOid; i++)
            {
                items[i - 1] = i.ToString();
            }
            this.cobSecurityLevel.Properties.Items.AddRange(items);
            int securityLevel = ModelContext.MetaModel.GetClass(this.className).SecurityLevel;
            if ((securityLevel >= 1) && (securityLevel <= userSecurityByOid))
            {
                this.cobSecurityLevel.SelectedIndex = securityLevel - 1;
            }
            else if ((userSecurityByOid >= 1) && (userSecurityByOid <= securityLevel))
            {
                this.cobSecurityLevel.SelectedIndex = userSecurityByOid - 1;
            }
            else
            {
                this.cobSecurityLevel.SelectedIndex = -1;
            }
            ArrayList belongingProjGroupByUserOid = user.GetBelongingProjGroupByUserOid(ClientData.LogonUser.Oid);
            this.cobGroup.Properties.Items.Add("");
            if ((belongingProjGroupByUserOid != null) && (belongingProjGroupByUserOid.Count > 0))
            {
                this.cobGroup.Properties.Items.AddRange(belongingProjGroupByUserOid.ToArray());
            }
            else
            {
                this.cobGroup.Enabled = false;
            }
            ArrayList parentOrgs = user.GetParentOrgs(ClientData.LogonUser.Oid);
            foreach (DEOrganization organization in parentOrgs)
            {
                this.cobOrgs.Properties.Items.Add(organization.Name);
            }
            this.cobOrgs.Tag = parentOrgs;
            this.cobOrgs.Text = "未指定";
            this.txtRevLabel.Text = "1";
        }

    }
}

