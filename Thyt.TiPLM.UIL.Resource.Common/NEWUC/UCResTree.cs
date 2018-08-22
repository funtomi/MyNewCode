    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.NEWUC {
    public partial class UCResTree : UserControlPLM
    {
        private ArrayList al_master;
        private ArrayList al_name;
        private ArrayList al_sbl;
        private bool b_start;
        private DECusRelation csb2sb;
        private DECusRelationLst csb2sbs;
        private Guid g_root_mid;
        private int i_pos;
        private string strRelName;
        private string strRootCode;
        private string strSBLTAB;
        

        public event SelectResHandler ResSelected;

        public UCResTree()
        {
            this.csb2sb = new DECusRelation();
            this.strRootCode = "1";
            this.strRelName = "SB2SB";
            this.strSBLTAB = "SBL";
            this.al_master = new ArrayList();
            this.al_sbl = new ArrayList();
            this.al_name = new ArrayList();
            this.g_root_mid = Guid.Empty;
            this.b_start = true;
            this.InitializeComponent();
        }

        public UCResTree(ArrayList al_host, int i_pos) : this()
        {
            this.g_root_mid = (Guid) al_host[0];
            this.strRelName = al_host[1].ToString();
            this.strSBLTAB = al_host[2].ToString();
            this.i_pos = i_pos;
        }
 

        private ArrayList GetChildMasterIDLst(Guid i_oid)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this.csb2sbs.Count; i++)
            {
                DECusRelation relation = this.csb2sbs.Item(i);
                if (relation.PLM_LEFTOBJ == i_oid)
                {
                    list.Add(relation.PLM_RIGHTOBJ);
                }
            }
            return list;
        }

        private Guid GetItem2MasterID(Guid i_oid)
        {
            for (int i = 0; i < this.al_sbl.Count; i++)
            {
                if (((Guid) this.al_sbl[i]) == i_oid)
                {
                    return (Guid) this.al_master[i];
                }
            }
            return Guid.Empty;
        }

        private Guid GetMaster2ItemID(Guid m_oid)
        {
            for (int i = 0; i < this.al_master.Count; i++)
            {
                if (((Guid) this.al_master[i]) == m_oid)
                {
                    return (Guid) this.al_sbl[i];
                }
            }
            return Guid.Empty;
        }

        private string GetNodeCode(Guid m_oid)
        {
            string str = "";
            for (int i = 0; i < this.al_master.Count; i++)
            {
                Guid guid = (Guid) this.al_master[i];
                if (guid == m_oid)
                {
                    str = this.al_name[i].ToString();
                }
            }
            return str;
        }

        private Guid GetRightTreeParentID(Guid g_m_id)
        {
            Guid empty = Guid.Empty;
            for (int i = 0; i < this.csb2sbs.Count; i++)
            {
                this.csb2sb = this.csb2sbs.Item(i);
                if (this.csb2sb.PLM_RIGHTOBJ == g_m_id)
                {
                    return this.csb2sb.PLM_LEFTOBJ;
                }
            }
            return empty;
        }

        private ArrayList GetRootObjectLST(Guid g_root_mid)
        {
            ArrayList list = new ArrayList();
            Guid empty = Guid.Empty;
            if (g_root_mid != Guid.Empty)
            {
                empty = this.GetMaster2ItemID(g_root_mid);
                if (this.IsHasLeftObject(empty))
                {
                    list.Add(g_root_mid);
                }
                return list;
            }
            for (int i = 0; i < this.al_master.Count; i++)
            {
                Guid guid2 = (Guid) this.al_master[i];
                Guid guid3 = (Guid) this.al_sbl[i];
                if (this.IsHasLeftObject(guid3) && this.IsNoExitRightObject(guid2))
                {
                    list.Add(guid2);
                }
            }
            return list;
        }


        private void InitObject()
        {
            this.csb2sbs = new DECusRelationLst();
            this.al_master.Clear();
            this.al_sbl.Clear();
            ArrayList itemMasters = PLItem.Agent.GetItemMasters(this.strSBLTAB, ClientData.LogonUser.Oid);
            ArrayList masterOids = new ArrayList(itemMasters.Count);
            ArrayList revNums = new ArrayList(itemMasters.Count);
            foreach (DEItemMaster2 master in itemMasters)
            {
                masterOids.Add(master.Oid);
                revNums.Add(0);
            }
            Guid curView = ClientData.UserGlobalOption.CurView;
            foreach (DEBusinessItem item in PLItem.Agent.GetBizItemsByMasters(masterOids, revNums, curView, ClientData.LogonUser.Oid, BizItemMode.BizItem))
            {
                if (item.Iteration.LinkRelationSet.GetRelationBizItemList(this.strRelName) == null)
                {
                    DERelationList list5 = PLItem.Agent.GetLinkRelations(item.IterOid, this.strSBLTAB, this.strRelName, ClientData.LogonUser.Oid);
                    item.Iteration.LinkRelationSet.RelationBizItemLists[this.strRelName] = list5;
                    this.al_master.Add(item.MasterOid);
                    this.al_sbl.Add(item.Iteration.Oid);
                    this.al_name.Add(item.Name);
                    if (list5.Count > 0)
                    {
                        for (int i = 0; i < list5.Count; i++)
                        {
                            DERelation2 relation = new DERelation2();
                            DECusRelation relation2 = new DECusRelation();
                            relation = (DERelation2) list5[i];
                            relation2.PLM_LEFTOBJ = item.IterOid;
                            relation2.PLM_RIGHTOBJ = relation.RightObj;
                            this.csb2sbs.Add(relation2);
                        }
                    }
                }
            }
        }

        private void InitRightTree()
        {
            this.TV_class.Nodes.Clear();
            ArrayList rootObjectLST = new ArrayList();
            rootObjectLST = this.GetRootObjectLST(this.g_root_mid);
            for (int i = 0; i < rootObjectLST.Count; i++)
            {
                Guid guid = (Guid) rootObjectLST[i];
                TreeNode node = new TreeNode(this.GetNodeCode(guid)) {
                    Tag = guid
                };
                this.TV_class.Nodes.Add(node);
                this.LoadNodeTree(guid, node);
            }
            this.TV_class.ExpandAll();
        }

        private bool IsHasLeftObject(Guid g_i_id)
        {
            for (int i = 0; i < this.csb2sbs.Count; i++)
            {
                this.csb2sb = this.csb2sbs.Item(i);
                if (this.csb2sb.PLM_LEFTOBJ == g_i_id)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsNoExitRightObject(Guid g_m_id)
        {
            for (int i = 0; i < this.csb2sbs.Count; i++)
            {
                this.csb2sb = this.csb2sbs.Item(i);
                if (this.csb2sb.PLM_RIGHTOBJ == g_m_id)
                {
                    return false;
                }
            }
            return true;
        }

        private void LoadNodeTree(Guid m_oid, TreeNode thenode)
        {
            for (int i = 0; i < this.al_master.Count; i++)
            {
                if (((Guid) this.al_master[i]) == m_oid)
                {
                    for (int j = 0; j < this.csb2sbs.Count; j++)
                    {
                        this.csb2sb = this.csb2sbs.Item(j);
                        if (((Guid) this.al_sbl[i]) == this.csb2sb.PLM_LEFTOBJ)
                        {
                            string nodeCode = this.GetNodeCode(this.csb2sb.PLM_RIGHTOBJ);
                            if (nodeCode.Trim().Length > 0)
                            {
                                TreeNode node = new TreeNode(nodeCode) {
                                    Tag = this.csb2sb.PLM_RIGHTOBJ
                                };
                                thenode.Nodes.Add(node);
                                this.LoadNodeTree(this.csb2sb.PLM_RIGHTOBJ, node);
                            }
                        }
                    }
                }
            }
        }

        private void TV_class_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!this.b_start && (this.ResSelected != null))
            {
                this.ResSelected("{" + this.TV_class.SelectedNode.Tag.ToString() + "}{" + this.TV_class.SelectedNode.Text + "}", 2, this.i_pos);
            }
        }

        private void UCResTree_Enter(object sender, EventArgs e)
        {
            this.b_start = false;
        }

        private void UCResTree_Load(object sender, EventArgs e)
        {
            this.InitObject();
            this.InitRightTree();
        }
    }
}

