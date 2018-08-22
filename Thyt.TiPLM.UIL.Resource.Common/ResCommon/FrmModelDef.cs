    using DevExpress.XtraTab;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Common.UserControl;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.ResCommon
{
    public partial class FrmModelDef : FormPLM
    {
        private ArrayList al_attrs = new ArrayList();
        private bool b_chek;
        private bool b_new;
       
        private string className;
        private Guid g_ClsOid;
        
        private emResourceType myEmResType;
        private DEResFolder myFolder;
        private DEResModel myModel;
     
        private UCResGrid ucUser;

        public FrmModelDef(Guid g_clsid, string str_clsLabel, emResourceType emResType)
        {
            this.InitializeComponent();
            this.g_ClsOid = g_clsid;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(g_clsid);
            this.className = class2.Name;
            this.myEmResType = emResType;
            this.SetFolder();
        }

        private void AddChildNode(DEResModelPrimaryKey defathermpk, ArrayList al_modelpks, TreeNode theNode)
        {
            foreach (DEResModelPrimaryKey key in al_modelpks)
            {
                if (defathermpk.PLM_OID == key.PLM_FATHEROID)
                {
                    TreeNode node = new TreeNode {
                        Text = key.PLM_SHOWNAME,
                        Tag = key,
                        Checked = key.PLM_ISSHOWDATA
                    };
                    theNode.Nodes.Add(node);
                    this.AddChildNode(key, al_modelpks, node);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.lB_Attr.SelectedItems.Count != 0)
            {
                DEResModelPrimaryKey key = new DEResModelPrimaryKey();
                DEMetaAttribute metaAttr = new DEMetaAttribute();
                metaAttr = this.GetMetaAttr(this.lB_Attr.SelectedItem.ToString());
                if (((metaAttr.DataType2 == PLMDataType.Guid) || (metaAttr.DataType2 == PLMDataType.Grid)) || (((metaAttr.DataType2 == PLMDataType.Card) || (metaAttr.DataType2 == PLMDataType.Blob)) || (metaAttr.DataType2 == PLMDataType.Clob)))
                {
                    MessageBoxPLM.Show("此类型的属性不能设为分类主键！", "模型定义", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (this.tV_define.SelectedNode != null)
                {
                    if (this.IsExistSameNodeName(this.tV_define.SelectedNode, this.lB_Attr.SelectedItem.ToString()))
                    {
                        MessageBoxPLM.Show("同级目录下有相同的主键名！", "模型定义", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        TreeNode node = new TreeNode();
                        DEResModelPrimaryKey tag = new DEResModelPrimaryKey();
                        tag = (DEResModelPrimaryKey) this.tV_define.SelectedNode.Tag;
                        node.Text = this.lB_Attr.SelectedItem.ToString();
                        key.PLM_OID = Guid.NewGuid();
                        key.PLM_FATHEROID = tag.PLM_OID;
                        key.PLM_MODELOID = this.myModel.PLM_OID;
                        key.PLM_ATTROID = metaAttr.Oid;
                        key.PLM_SHOWNAME = this.lB_Attr.SelectedItem.ToString();
                        key.PLM_OPTION = 0;
                        node.Tag = key;
                        this.tV_define.SelectedNode.Nodes.Add(node);
                    }
                }
                else if (this.IsExistSameNodeName(null, this.lB_Attr.SelectedItem.ToString()))
                {
                    MessageBoxPLM.Show("同级目录下有相同的主键名！", "模型定义", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    TreeNode node2 = new TreeNode {
                        Text = this.lB_Attr.SelectedItem.ToString()
                    };
                    key.PLM_OID = Guid.NewGuid();
                    key.PLM_FATHEROID = Guid.Empty;
                    key.PLM_MODELOID = this.myModel.PLM_OID;
                    key.PLM_ATTROID = metaAttr.Oid;
                    key.PLM_SHOWNAME = this.lB_Attr.SelectedItem.ToString();
                    key.PLM_OPTION = 0;
                    node2.Tag = key;
                    this.tV_define.Nodes.Add(node2);
                }
            }
        }

        private void btnAddVirtual_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tB_Virtual.Text.Trim()))
            {
                MessageBoxPLM.Show("虚拟主键不能为空！", "增加虚拟主键提示", MessageBoxButtons.OK);
            }
            else
            {
                DEResModelPrimaryKey key = new DEResModelPrimaryKey();
                if (this.tV_define.SelectedNode != null)
                {
                    if (this.IsExistSameNodeName(this.tV_define.SelectedNode, this.tB_Virtual.Text.Trim()))
                    {
                        MessageBoxPLM.Show("同级目录下有相同的主键名！", "模型定义", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        TreeNode node = new TreeNode {
                            Text = this.tB_Virtual.Text.Trim()
                        };
                        DEResModelPrimaryKey tag = new DEResModelPrimaryKey();
                        tag = (DEResModelPrimaryKey) this.tV_define.SelectedNode.Tag;
                        key.PLM_OID = Guid.NewGuid();
                        key.PLM_FATHEROID = tag.PLM_OID;
                        key.PLM_MODELOID = this.myModel.PLM_OID;
                        key.PLM_ATTROID = Guid.Empty;
                        key.PLM_SHOWNAME = this.tB_Virtual.Text.Trim();
                        key.PLM_OPTION++;
                        node.Tag = key;
                        this.tV_define.SelectedNode.Nodes.Add(node);
                    }
                }
                else if (this.IsExistSameNodeName(null, this.tB_Virtual.Text.Trim()))
                {
                    MessageBoxPLM.Show("同级目录下有相同的主键名！", "模型定义", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    TreeNode node2 = new TreeNode {
                        Text = this.tB_Virtual.Text.Trim()
                    };
                    key.PLM_OID = Guid.NewGuid();
                    key.PLM_FATHEROID = Guid.Empty;
                    key.PLM_MODELOID = this.myModel.PLM_OID;
                    key.PLM_ATTROID = Guid.Empty;
                    key.PLM_SHOWNAME = this.tB_Virtual.Text.Trim();
                    key.PLM_OPTION++;
                    node2.Tag = key;
                    this.tV_define.Nodes.Add(node2);
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.tV_define.SelectedNode != null)
            {
                if (this.tV_define.SelectedNode.Parent != null)
                {
                    DEResModelPrimaryKey tag = (DEResModelPrimaryKey) this.tV_define.SelectedNode.Tag;
                    this.tV_define.SelectedNode.Parent.Nodes.Remove(this.tV_define.SelectedNode);
                    this.tV_define.SelectedNode = null;
                }
                else
                {
                    DEResModelPrimaryKey key2 = (DEResModelPrimaryKey) this.tV_define.SelectedNode.Tag;
                    this.tV_define.Nodes.Remove(this.tV_define.SelectedNode);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ArrayList pKTreeObjectLst = new ArrayList();
            PLResModel model = new PLResModel();
            pKTreeObjectLst = this.GetPKTreeObjectLst();
            this.myModel.PLM_MODELPKLIST = pKTreeObjectLst;
            if (this.b_new)
            {
                model.CreateModel(this.myModel);
            }
            else if (pKTreeObjectLst.Count == 0)
            {
                model.DeleteModel(this.myModel);
            }
            else
            {
                model.UpdateModel(this.myModel);
            }
            base.Dispose();
        }

        private void btnSetShow_Click(object sender, EventArgs e)
        {
            if (this.tV_define.SelectedNode == null)
            {
                MessageBoxPLM.Show("请选择主键！", "设置主键数据显示提示", MessageBoxButtons.OK);
            }
            else
            {
                this.b_chek = true;
                DEResModelPrimaryKey tag = (DEResModelPrimaryKey) this.tV_define.SelectedNode.Tag;
                if (this.tV_define.SelectedNode.Checked)
                {
                    this.tV_define.SelectedNode.Checked = false;
                    if (tag.PLM_ISSHOWDATA)
                    {
                        tag.PLM_OPTION -= 2;
                    }
                }
                else
                {
                    this.tV_define.SelectedNode.Checked = true;
                    if (!tag.PLM_ISSHOWDATA)
                    {
                        tag.PLM_OPTION += 2;
                    }
                }
            }
        }

        private void chB_isExpand_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chB_isExpand.Checked)
            {
                this.myModel.PLM_OPTION = 1;
            }
            else
            {
                this.myModel.PLM_OPTION = 0;
            }
        }

        private void CheckDefineAttr()
        {
            switch (ResFunc.CheckDefineAttrLst(this.g_ClsOid))
            {
                case 0:
                case 1:
                    break;

                case 2:
                    MessageBoxPLM.Show("数据模型删除属性,请检查资源类节点属性显示定义和模型定义", "提示");
                    return;

                case 3:
                    MessageBoxPLM.Show("数据模型新增属性,请检查资源类节点属性显示定义", "提示");
                    return;

                case 4:
                    MessageBoxPLM.Show("数据模型删除并新增了属性,请检查资源类节点属性显示定义和模型定义", "提示");
                    break;

                default:
                    return;
            }
        }
         
        private void FrmModelDef_Load(object sender, EventArgs e)
        {
            this.CheckDefineAttr();
            this.LoadAttrData();
            this.LoadAttrToControl();
            this.LoadModelData();
            this.InitModelTree();
        }

        private ArrayList GetAttrList(string str_clsname) {
            return ModelContext.MetaModel.GetAttributes(str_clsname);
        }
        private DEMetaAttribute GetMetaAttr(Guid g_oid)
        {
            foreach (DEMetaAttribute attribute in this.al_attrs)
            {
                if (attribute.Oid == g_oid)
                {
                    return attribute;
                }
            }
            return new DEMetaAttribute();
        }

        private DEMetaAttribute GetMetaAttr(string str_label)
        {
            foreach (DEMetaAttribute attribute in this.al_attrs)
            {
                if (attribute.Label == str_label)
                {
                    return attribute;
                }
            }
            return new DEMetaAttribute();
        }

        private void GetPKObjectATNode(ArrayList al_pks, TreeNode tnode)
        {
            if (tnode.GetNodeCount(false) > 0)
            {
                foreach (TreeNode node in tnode.Nodes)
                {
                    DEResModelPrimaryKey tag = (DEResModelPrimaryKey) node.Tag;
                    al_pks.Add(tag);
                    this.GetPKObjectATNode(al_pks, node);
                }
            }
        }

        private ArrayList GetPKTreeObjectLst()
        {
            ArrayList list = new ArrayList();
            foreach (TreeNode node in this.tV_define.Nodes)
            {
                DEResModelPrimaryKey tag = (DEResModelPrimaryKey) node.Tag;
                list.Add(tag);
                this.GetPKObjectATNode(list, node);
            }
            return list;
        }

        private ArrayList GetRoot(ArrayList al_modelpks)
        {
            ArrayList list = new ArrayList();
            foreach (DEResModelPrimaryKey key in al_modelpks)
            {
                if (key.PLM_FATHEROID == Guid.Empty)
                {
                    list.Add(key);
                }
            }
            return list;
        }


        private void InitModelTree()
        {
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            list = this.myModel.PLM_MODELPKLIST;
            foreach (DEResModelPrimaryKey key in this.GetRoot(list))
            {
                TreeNode node = new TreeNode {
                    Text = key.PLM_SHOWNAME,
                    Tag = key,
                    Checked = key.PLM_ISSHOWDATA
                };
                this.tV_define.Nodes.Add(node);
                this.AddChildNode(key, list, node);
            }
        }

        private bool IsExistSameNodeName(TreeNode theNode, string strName)
        {
            bool flag = false;
            if (theNode == null)
            {
                if (this.tV_define.Nodes.Count == 0)
                {
                    return false;
                }
                foreach (TreeNode node in this.tV_define.Nodes)
                {
                    if (node.Text.Trim().ToLower() == strName.Trim().ToLower())
                    {
                        return true;
                    }
                }
                return flag;
            }
            if (theNode.Nodes.Count == 0)
            {
                return false;
            }
            foreach (TreeNode node2 in theNode.Nodes)
            {
                if (node2.Text.Trim().ToLower() == strName.Trim().ToLower())
                {
                    return true;
                }
            }
            return flag;
        }

        private void LoadAttrData()
        {
            DEResFolder defolder = new DEResFolder {
                Oid = this.g_ClsOid,
                ClassOid = this.g_ClsOid,
                ClassName = this.className
            };
            ArrayList showAttrList = ResFunc.GetShowAttrList(defolder, emTreeType.NodeTree);
            this.al_attrs = ResFunc.CloneMetaAttrLst(showAttrList);
        }

        private void LoadAttrToControl()
        {
            foreach (DEMetaAttribute attribute in this.al_attrs)
            {
                if (attribute.Name != "OID")
                {
                    this.lB_Attr.Items.Add(attribute.Label);
                }
            }
        }

        private void LoadModelData()
        {
            this.myModel = new PLResModel().GetModel(this.g_ClsOid);
            this.chB_isExpand.Checked = this.myModel.PLM_ISEXPAND;
            if ((this.myModel == null) || (this.myModel.PLM_OID == Guid.Empty))
            {
                this.b_new = true;
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.g_ClsOid);
                this.myModel = new DEResModel();
                this.myModel.PLM_OID = Guid.NewGuid();
                this.myModel.PLM_CLASSOID = this.g_ClsOid;
                this.myModel.PLM_CLASSNAME = class2.Name;
                this.myModel.PLM_CREATOR = ClientData.LogonUser.Oid;
                this.myModel.PLM_CREATETIME = DateTime.Now;
                this.myModel.PLM_MODELPKLIST = new ArrayList();
                this.myModel.PLM_OPTION = 0;
            }
        }

        private void SetFolder()
        {
            this.myFolder = new DEResFolder();
            this.myFolder.Oid = this.g_ClsOid;
            this.myFolder.ClassOid = this.g_ClsOid;
            this.myFolder.ClassName = this.className;
            ArrayList clsTreeCls = new ArrayList();
            clsTreeCls = new PLReference().GetClsTreeCls(this.g_ClsOid);
            if (clsTreeCls.Count > 0)
            {
                DEDefCls cls = (DEDefCls) clsTreeCls[0];
                this.myFolder.Filter = cls.FILTER;
                this.myFolder.FilterString = cls.FILTERSTRING;
                this.myFolder.FilterValue = cls.FILTERVALUE;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1)
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.ucUser == null)
                {
                    this.ucUser = new UCResGrid(this.className);
                    this.ucUser.Dock = DockStyle.Fill;
                    this.ucUser.ReLoad(this.myFolder);
                    this.panel4.Controls.Add(this.ucUser);
                }
                ArrayList pKTreeObjectLst = new ArrayList();
                pKTreeObjectLst = this.GetPKTreeObjectLst();
                if (pKTreeObjectLst.Count > 0)
                {
                    DEResModel themodel = new DEResModel {
                        PLM_OID = this.myModel.PLM_OID,
                        PLM_CLASSOID = this.myModel.PLM_CLASSOID,
                        PLM_CLASSNAME = this.myModel.PLM_CLASSNAME,
                        PLM_CREATOR = this.myModel.PLM_CREATOR,
                        PLM_CREATETIME = this.myModel.PLM_CREATETIME,
                        PLM_OPTION = this.myModel.PLM_OPTION,
                        PLM_MODELPKLIST = pKTreeObjectLst
                    };
                    this.ucUser.ReloadModel(themodel);
                }
                this.ucUser.AllowDoubleClick = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void tV_define_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!this.b_chek)
            {
                e.Cancel = true;
            }
            else
            {
                this.b_chek = false;
            }
        }
    }
}

