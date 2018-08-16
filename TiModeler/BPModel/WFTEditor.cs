namespace Thyt.TiPLM.CLT.TiModeler.BPModel
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using Thyt.TiPLM.CLT.Admin.BPM;
    using Thyt.TiPLM.CLT.Admin.BPM.Common;
    using Thyt.TiPLM.CLT.Admin.BPM.ExportAndImport;
    using Thyt.TiPLM.CLT.Admin.BPM.Modeler;
    using Thyt.TiPLM.CLT.Admin.BPM.Verification;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.CLT.TiModeler.BPModel.ReUndo;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.Common.Admin.BPM;
    using Thyt.TiPLM.Common.Interface.Admin.BPM;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Admin.BPM;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Common;
    using Thyt.TiPLM.UIL.Addin;
    using Thyt.TiPLM.UIL.Common;

    public partial class WFTEditor : Form
    {
        public ArrayList arrayDeleteObject;
        
        private DataObject copyResult;
        private string fileEndChar;
        public bool isDel;
        public bool isNew;
        public bool isProcessRuleFirstOpen;
        
        public const string OP_ADD = "OP_Add";
        public const string OP_ADD_POINT = "OP_ADD_POINT";
        public const string OP_DELETE = "OP_Delete";
        public const string OP_DELETE_POINT = "OP_DELETE_POINT";
        public const string OP_MOVE = "OP_Move";
        public DELProcessDefinition proTemplate;
        private ResourceManager resProPropertyEdit;
        public static ResourceManager rmTiModeler;
        public Stack stackRedo;
        public Stack stackUndo;
        public ArrayList TheAllProcessRuleList;
        

        public WFTEditor(FrmMain tiMain)
        {
            rmTiModeler = new ResourceManager("Thyt.TiPLM.CLT.TiModeler.TiModelerStrings", Assembly.GetExecutingAssembly());
            this.tiMain = tiMain;
            this.InitializeComponent();
            this.init();
        }

        public WFTEditor(DELProcessDefinition template, FrmMain tiMain)
        {
            rmTiModeler = new ResourceManager("Thyt.TiPLM.CLT.TiModeler.TiModelerStrings", Assembly.GetExecutingAssembly());
            this.tiMain = tiMain;
            this.InitializeComponent();
            this.proTemplate = template;
            this.init();
        }

        private void AddNewOperation()
        {
            new FrmOperationEdit().ShowDialog(this);
        }

        private void AfterActivityIDChanged(DELProcessDefinition OldProcessDefinition, DELProcessDefinition template, Guid oldActivityId, Guid newActivityId)
        {
            DataRow row;
            ProcessDefinitionDS dataSet = template.GetDataSet();
            int count = dataSet.Tables["PLM_BPM_R_STEPDEF_STEPDEF"].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                row = dataSet.Tables["PLM_BPM_R_STEPDEF_STEPDEF"].Rows[i];
                if (new Guid((byte[]) row["PLM_FROMSTEPDEFINITIONID"]) == oldActivityId)
                {
                    row["PLM_FROMSTEPDEFINITIONID"] = newActivityId.ToByteArray();
                }
                if (new Guid((byte[]) row["PLM_TOSTEPDEFINITIONID"]) == oldActivityId)
                {
                    row["PLM_TOSTEPDEFINITIONID"] = newActivityId.ToByteArray();
                }
            }
            count = dataSet.Tables["PLM_BPM_PARTICIPANT_DEFINITION"].Rows.Count;
            for (int j = 0; j < count; j++)
            {
                row = dataSet.Tables["PLM_BPM_PARTICIPANT_DEFINITION"].Rows[j];
                if (new Guid((byte[]) row["PLM_ENTITYDEFINITIONID"]) == oldActivityId)
                {
                    row["PLM_ENTITYDEFINITIONID"] = newActivityId.ToByteArray();
                }
            }
            count = dataSet.Tables["PLM_BPM_R_ACTIVITYDEF_OPER"].Rows.Count;
            for (int k = 0; k < count; k++)
            {
                row = dataSet.Tables["PLM_BPM_R_ACTIVITYDEF_OPER"].Rows[k];
                if (new Guid((byte[]) row["PLM_ACTIVITYDEFINITIONID"]) == oldActivityId)
                {
                    row["PLM_ACTIVITYDEFINITIONID"] = newActivityId.ToByteArray();
                }
            }
            count = dataSet.Tables["PLM_BPM_R_OPER_PARAMETER_DEF"].Rows.Count;
            for (int m = 0; m < count; m++)
            {
                row = dataSet.Tables["PLM_BPM_R_OPER_PARAMETER_DEF"].Rows[m];
                if (new Guid((byte[]) row["PLM_ACTIVITYDIFINITIONID"]) == oldActivityId)
                {
                    row["PLM_ACTIVITYDIFINITIONID"] = newActivityId.ToByteArray();
                }
            }
            count = dataSet.Tables["PLM_BPM_BOBADN_DEF"].Rows.Count;
            for (int n = 0; n < count; n++)
            {
                row = dataSet.Tables["PLM_BPM_BOBADN_DEF"].Rows[n];
                if (new Guid((byte[]) row["PLM_ACTDEFOID"]) == oldActivityId)
                {
                    row["PLM_ACTDEFOID"] = newActivityId.ToByteArray();
                }
            }
            count = dataSet.Tables["PLM_BPM_BOGRULE_DEF"].Rows.Count;
            for (int num7 = 0; num7 < count; num7++)
            {
                row = dataSet.Tables["PLM_BPM_BOGRULE_DEF"].Rows[num7];
                if (new Guid((byte[]) row["PLM_ACTIVITY_ID"]) == oldActivityId)
                {
                    row["PLM_ACTIVITY_ID"] = newActivityId.ToByteArray();
                }
            }
            count = dataSet.Tables["PLM_BPM_DATA_MAPPING_DEF"].Rows.Count;
            for (int num8 = 0; num8 < count; num8++)
            {
                row = dataSet.Tables["PLM_BPM_DATA_MAPPING_DEF"].Rows[num8];
                if (new Guid((byte[]) row["PLM_SUP_ID"]) == oldActivityId)
                {
                    row["PLM_SUP_ID"] = newActivityId.ToByteArray();
                }
            }
            foreach (DELBPMGrantDef def in template.GrantDefList)
            {
                if (oldActivityId == def.ActivityDefID)
                {
                    def.ActivityDefID = newActivityId;
                }
            }
            this.CopyGrant(OldProcessDefinition, template, oldActivityId, newActivityId);
        }

        private void AfterParticipantIDChanged(DELProcessDefinition OldProcessDefinition, DELProcessDefinition template, Guid oldParticipantId, Guid newParticipantId)
        {
            ProcessDefinitionDS dataSet = template.GetDataSet();
            int count = dataSet.Tables["PLM_BPM_R_PARTICI_USER_DEF"].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow row = dataSet.Tables["PLM_BPM_R_PARTICI_USER_DEF"].Rows[i];
                if (new Guid((byte[]) row["PLM_PARTICIPANTID"]) == oldParticipantId)
                {
                    row["PLM_PARTICIPANTID"] = newParticipantId.ToByteArray();
                }
            }
        }

        private void AfterProcessDataIDChanged(DELProcessDefinition OldProcessDefinition, DELProcessDefinition template, Guid oldProcessDataId, Guid newProcessDataId)
        {
            DataRow row;
            ProcessDefinitionDS dataSet = template.GetDataSet();
            int count = dataSet.Tables["PLM_BPM_R_OPER_PARAMETER_DEF"].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                row = dataSet.Tables["PLM_BPM_R_OPER_PARAMETER_DEF"].Rows[i];
                if (new Guid((byte[]) row["PLM_PROCESSDEFINITIONDATAID"]) == oldProcessDataId)
                {
                    row["PLM_PROCESSDEFINITIONDATAID"] = newProcessDataId.ToByteArray();
                }
            }
            count = dataSet.Tables["PLM_BPM_DATA_MAPPING_DEF"].Rows.Count;
            for (int j = 0; j < count; j++)
            {
                row = dataSet.Tables["PLM_BPM_DATA_MAPPING_DEF"].Rows[j];
                if (new Guid((byte[]) row["PLM_SUP_OBJECT_ID"]) == oldProcessDataId)
                {
                    row["PLM_SUP_OBJECT_ID"] = newProcessDataId.ToByteArray();
                }
            }
            count = dataSet.Tables["PLM_BPM_BOGRULE_DEF"].Rows.Count;
            for (int k = 0; k < count; k++)
            {
                row = dataSet.Tables["PLM_BPM_BOGRULE_DEF"].Rows[k];
                string str = (string) row["PLM_BOG_IDLIST"];
                if (str.IndexOf(oldProcessDataId.ToString()) >= 0)
                {
                    row["PLM_BOG_IDLIST"] = str.Replace(oldProcessDataId.ToString(), newProcessDataId.ToString());
                }
            }
            foreach (DELProcessDataDefinition definition in template.TargetBusinessObjects)
            {
                if (definition.ID == oldProcessDataId)
                {
                    definition.ID = newProcessDataId;
                }
                foreach (DELRGroupDataDef def in definition.ObjectOrVarList)
                {
                    def.Oid = Guid.NewGuid();
                    if (oldProcessDataId.ToString() == def.BOID)
                    {
                        def.BOID = newProcessDataId.ToString();
                    }
                    if (oldProcessDataId == def.GroupDefinitionOid)
                    {
                        def.GroupDefinitionOid = newProcessDataId;
                    }
                }
                foreach (DELRGroupObjTypeDef def2 in definition.ObjectTypes)
                {
                    def2.Oid = Guid.NewGuid();
                    if (oldProcessDataId == def2.GroupDefOid)
                    {
                        def2.GroupDefOid = newProcessDataId;
                    }
                }
            }
            foreach (DELProcessDataDefinition definition2 in template.ReferenceBusinessObjects)
            {
                if (definition2.ID == oldProcessDataId)
                {
                    definition2.ID = newProcessDataId;
                }
            }
            foreach (DELProcessDataDefinition definition3 in template.Variables)
            {
                if (definition3.ID == oldProcessDataId)
                {
                    definition3.ID = newProcessDataId;
                }
            }
            foreach (DELBPMGrantDef def3 in template.GrantDefList)
            {
                if (oldProcessDataId == def3.ProcessDataDefID)
                {
                    def3.ProcessDataDefID = newProcessDataId;
                }
            }
        }

        private void AfterProcessIDChanged(DELProcessDefinition OldProcessDefinition, DELProcessDefinition template)
        {
            ProcessDefinitionDS dataSet = template.GetDataSet();
            DataRow row = dataSet.Tables["PLM_BPM_PROCESS_DEFINITION"].Rows[0];
            row["PLM_OID"] = template.ID.ToByteArray();
            int count = dataSet.Tables["PLM_BPM_PROCESSDATA_DEFINITION"].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                row = dataSet.Tables["PLM_BPM_PROCESSDATA_DEFINITION"].Rows[i];
                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
            }
            foreach (DELProcessDataDefinition definition in template.TargetBusinessObjects)
            {
                definition.ProcessDefinitionID = template.ID;
            }
            foreach (DELProcessDataDefinition definition2 in template.ReferenceBusinessObjects)
            {
                definition2.ProcessDefinitionID = template.ID;
            }
            foreach (DELProcessDataDefinition definition3 in template.Variables)
            {
                definition3.ProcessDefinitionID = template.ID;
            }
            count = dataSet.Tables["PLM_BPM_ACTIVITY_DEFINITION"].Rows.Count;
            for (int j = 0; j < count; j++)
            {
                row = dataSet.Tables["PLM_BPM_ACTIVITY_DEFINITION"].Rows[j];
                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
            }
            count = dataSet.Tables["PLM_BPM_ROUTER_DEFINITION"].Rows.Count;
            for (int k = 0; k < count; k++)
            {
                row = dataSet.Tables["PLM_BPM_ROUTER_DEFINITION"].Rows[k];
                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
            }
            count = dataSet.Tables["PLM_BPM_R_STEPDEF_STEPDEF"].Rows.Count;
            for (int m = 0; m < count; m++)
            {
                row = dataSet.Tables["PLM_BPM_R_STEPDEF_STEPDEF"].Rows[m];
                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
            }
            count = dataSet.Tables["PLM_BPM_PARTICIPANT_DEFINITION"].Rows.Count;
            for (int n = 0; n < count; n++)
            {
                row = dataSet.Tables["PLM_BPM_PARTICIPANT_DEFINITION"].Rows[n];
                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
                if (new Guid((byte[]) row["PLM_ENTITYDEFINITIONID"]) == OldProcessDefinition.ID)
                {
                    row["PLM_ENTITYDEFINITIONID"] = template.ID.ToByteArray();
                }
            }
            count = dataSet.Tables["PLM_BPM_R_PARTICI_USER_DEF"].Rows.Count;
            for (int num7 = 0; num7 < count; num7++)
            {
                row = dataSet.Tables["PLM_BPM_R_PARTICI_USER_DEF"].Rows[num7];
                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
            }
            count = dataSet.Tables["PLM_BPM_R_ACTIVITYDEF_OPER"].Rows.Count;
            for (int num8 = 0; num8 < count; num8++)
            {
                row = dataSet.Tables["PLM_BPM_R_ACTIVITYDEF_OPER"].Rows[num8];
                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
            }
            count = dataSet.Tables["PLM_BPM_R_OPER_PARAMETER_DEF"].Rows.Count;
            for (int num9 = 0; num9 < count; num9++)
            {
                row = dataSet.Tables["PLM_BPM_R_OPER_PARAMETER_DEF"].Rows[num9];
                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
            }
            count = dataSet.Tables["PLM_BPM_BOBADN_DEF"].Rows.Count;
            for (int num10 = 0; num10 < count; num10++)
            {
                row = dataSet.Tables["PLM_BPM_BOBADN_DEF"].Rows[num10];
                row["PLM_PRODEFOID"] = template.ID.ToByteArray();
            }
            count = dataSet.Tables["PLM_BPM_BOGRULE_DEF"].Rows.Count;
            for (int num11 = 0; num11 < count; num11++)
            {
                row = dataSet.Tables["PLM_BPM_BOGRULE_DEF"].Rows[num11];
                row["PLM_PROCESS_ID"] = template.ID.ToByteArray();
            }
            count = dataSet.Tables["PLM_BPM_DATA_MAPPING_DEF"].Rows.Count;
            for (int num12 = 0; num12 < count; num12++)
            {
                row = dataSet.Tables["PLM_BPM_DATA_MAPPING_DEF"].Rows[num12];
                row["PLM_OID"] = Guid.NewGuid().ToByteArray();
                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
            }
            foreach (DELProcessDataDefinition definition4 in template.TargetBusinessObjects)
            {
                foreach (DELRGroupDataDef def in definition4.ObjectOrVarList)
                {
                    Guid guid2 = new Guid();
                    def.Oid = guid2;
                    def.ProcessDefinitionOid = template.ID;
                }
            }
            foreach (DELBPMGrantDef def2 in template.GrantDefList)
            {
                def2.GrantDefID = Guid.NewGuid();
                def2.ProcessDefID = template.ID;
            }
            foreach (DELRProcessDef2Obj obj2 in template.ProcessDefToObjList)
            {
                obj2.Oid = Guid.NewGuid();
                obj2.ProcessDefOid = template.ID;
            }
        }

        private void AfterRActOperIDChanged(DELProcessDefinition OldProcessDefinition, DELProcessDefinition template, Guid oldOperDefId, Guid newOperDefId)
        {
            DataRow row;
            ProcessDefinitionDS dataSet = template.GetDataSet();
            int count = dataSet.Tables["PLM_BPM_BOBADN_DEF"].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                row = dataSet.Tables["PLM_BPM_BOBADN_DEF"].Rows[i];
                if (new Guid((byte[]) row["PLM_BOBDEFOID"]) == oldOperDefId)
                {
                    row["PLM_BOBDEFOID"] = newOperDefId.ToByteArray();
                    row["PLM_OID"] = Guid.NewGuid().ToByteArray();
                }
            }
            count = dataSet.Tables["PLM_BPM_BOGRULE_DEF"].Rows.Count;
            for (int j = 0; j < count; j++)
            {
                row = dataSet.Tables["PLM_BPM_BOGRULE_DEF"].Rows[j];
                if (new Guid((byte[]) row["PLM_BOB_ID"]) == oldOperDefId)
                {
                    row["PLM_BOB_ID"] = newOperDefId.ToByteArray();
                    row["PLM_OID"] = Guid.NewGuid().ToByteArray();
                }
            }
        }

        private void AfterRouterIDChanged(DELProcessDefinition OldProcessDefinition, DELProcessDefinition template, Guid oldRouterId, Guid newRouterId)
        {
            ProcessDefinitionDS dataSet = template.GetDataSet();
            int count = dataSet.Tables["PLM_BPM_R_STEPDEF_STEPDEF"].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow row = dataSet.Tables["PLM_BPM_R_STEPDEF_STEPDEF"].Rows[i];
                if (new Guid((byte[]) row["PLM_FROMSTEPDEFINITIONID"]) == oldRouterId)
                {
                    row["PLM_FROMSTEPDEFINITIONID"] = newRouterId.ToByteArray();
                }
                if (new Guid((byte[]) row["PLM_TOSTEPDEFINITIONID"]) == oldRouterId)
                {
                    row["PLM_TOSTEPDEFINITIONID"] = newRouterId.ToByteArray();
                }
            }
        }

        public void buildViewPanel()
        {
            this.viewPanel = new ViewPanel(this);
            this.viewPanel.Parent = this.panelMam;
            this.viewPanel.Size = new Size(0xbb8, 0xbb8);
            this.viewPanel.Location = new Point(0, 0);
            this.toolBarBunCursor.Pushed = true;
        }

        private void cmiClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cmiImageOutput_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(ConstForModeler.MaxClientX, ConstForModeler.MaxClientY);
            Graphics g = Graphics.FromImage(image);
            SolidBrush brush = new SolidBrush(Color.White);
            g.FillRectangle(brush, 0, 0, ConstForModeler.MaxClientX, ConstForModeler.MaxClientY);
            this.viewPanel.shapeData.root.draw(g);
            this.saveFileDialog1.FileName = this.viewPanel.shapeData.template.Name.Replace(':', '：');
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                Bitmap bitmap2 = ImageUtil.TrimBitmap(image, Color.White);
                if (this.saveFileDialog1.FileName.ToUpper().EndsWith(".JPG"))
                {
                    bitmap2.Save(this.saveFileDialog1.FileName, ImageFormat.Jpeg);
                }
                else
                {
                    bitmap2.Save(this.saveFileDialog1.FileName);
                }
                bitmap2 = null;
                MessageBox.Show("图片【" + this.saveFileDialog1.FileName + "】输出结束！", "提示");
                Cursor.Current = Cursors.Default;
            }
            image = null;
        }

        private void cmiLineFalse_Click(object sender, EventArgs e)
        {
            ((Line) this.viewPanel.selectedShape).realLine.IsTrue = 0;
            this.viewPanel.Refresh();
        }

        private void cmiLineTrue_Click(object sender, EventArgs e)
        {
            ((Line) this.viewPanel.selectedShape).realLine.IsTrue = 1;
            this.viewPanel.Refresh();
        }

        public void cmiNodeCopy_Click(object sender, EventArgs e)
        {
            if (this.viewPanel.selectedShape != null)
            {
                this.tiMain.TheCopiedObjectList.Clear();
                if (this.viewPanel.selectedShape.realNode is DELRouterDefinition)
                {
                    DELRouterDefinition realNode = (DELRouterDefinition) this.viewPanel.selectedShape.realNode;
                    DELRouterDefinition definition2 = new DELRouterDefinition {
                        ID = realNode.ID,
                        PositionX = realNode.PositionX,
                        PositionY = realNode.PositionY,
                        ProcessDefinitionID = realNode.ProcessDefinitionID,
                        Height = realNode.Height,
                        Width = realNode.Width,
                        DurationUnit = realNode.DurationUnit,
                        Duration = realNode.Duration,
                        DocumentationID = realNode.DocumentationID,
                        IconID = realNode.IconID,
                        JoinExpression = realNode.JoinExpression,
                        JoinType = realNode.JoinType,
                        NodeType = realNode.NodeType,
                        OverTimeHandleRule = realNode.OverTimeHandleRule,
                        Priority = realNode.Priority,
                        SplitExpression = realNode.SplitExpression,
                        SplitType = realNode.SplitType,
                        State = realNode.State
                    };
                    this.tiMain.TheCopiedObjectList.Add(definition2);
                }
                else if (this.viewPanel.selectedShape.realNode is DELActivityDefinition)
                {
                    DELActivityDefinition oldAct = (DELActivityDefinition) this.viewPanel.selectedShape.realNode;
                    if ((oldAct.Name == "Start") || (oldAct.Name == "End"))
                    {
                        MessageBox.Show("此类型节点不允许复制！");
                    }
                    else
                    {
                        DELActivityDefinition definition4 = this.OnCopyActivity(oldAct);
                        this.tiMain.TheCopiedObjectList.Add(definition4);
                    }
                }
            }
            else if (this.viewPanel.selectedShapeList.Count > 0)
            {
                this.tiMain.TheCopiedObjectList.Clear();
                foreach (object obj2 in this.viewPanel.selectedShapeList)
                {
                    if (obj2 is Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode)
                    {
                        Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode node = obj2 as Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode;
                        if (node.realNode is DELActivityDefinition)
                        {
                            DELActivityDefinition definition5 = (DELActivityDefinition) node.realNode;
                            if ((definition5.Name != "Start") && (definition5.Name != "End"))
                            {
                                DELActivityDefinition definition6 = this.OnCopyActivity(definition5);
                                this.tiMain.TheCopiedObjectList.Add(definition6);
                            }
                        }
                    }
                }
            }
        }

        private void cmiNodePaste_Click(object sender, EventArgs e)
        {
            if (this.tiMain.TheCopiedObjectList.Count != 0)
            {
                object theCopiedObjectList = this.tiMain.TheCopiedObjectList;
                if (this.tiMain.TheCopiedObjectList.Count == 1)
                {
                    theCopiedObjectList = this.tiMain.TheCopiedObjectList[0];
                }
                if (theCopiedObjectList is DELActivityDefinition)
                {
                    DELActivityDefinition oldAct = (DELActivityDefinition) theCopiedObjectList;
                    DELActivityDefinition definition2 = this.CopyActivity(oldAct);
                    string name = "";
                    Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode taskNode = new Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode(this.viewPanel.mousePosition, name);
                    definition2.Name = taskNode.text.caption;
                    taskNode.realNode = definition2;
                    FrmTaskPropertyDlg taskDlg = new FrmTaskPropertyDlg(true) {
                        proTemplate = this.proTemplate,
                        realTaskNode = definition2,
                        myAct = definition2
                    };
                    this.viewPanel.InitFrmTaskPropertyDlg(taskDlg, taskNode);
                    if (taskDlg.ShowDialog(this) == DialogResult.OK)
                    {
                        this.viewPanel.ModifyTaskValue(taskDlg, taskNode);
                        this.viewPanel.addTaskNode(taskNode);
                        this.viewPanel.selectedShape = taskNode;
                        taskNode.isSelected = true;
                        ((FrmMain) this.viewPanel.mainWindow.MdiParent).AddTreeNode(this.viewPanel.mainWindow, taskNode);
                        this.viewPanel.setMainFrmTreeSelected();
                        RUTaskNode node2 = new RUTaskNode(this, taskNode, "OP_Add");
                        this.stackUndo.Push(node2);
                        this.setRUToolbarStatus();
                    }
                }
                else if (theCopiedObjectList is DELRouterDefinition)
                {
                    DELRouterDefinition oldRouter = (DELRouterDefinition) theCopiedObjectList;
                    DELRouterDefinition realNode = this.CopyRouter(oldRouter);
                    FrmRouteDlg routeDlg = new FrmRouteDlg(true);
                    RouteNode routeNode = new RouteNode(this.viewPanel.mousePosition, realNode);
                    this.viewPanel.InitFrmRouteDlg(routeDlg, routeNode);
                    if (routeDlg.ShowDialog(this) == DialogResult.OK)
                    {
                        this.viewPanel.ModifyRouterValue(routeDlg, routeNode);
                        routeNode.decideImage();
                        this.viewPanel.addRouteNode(routeNode);
                        this.viewPanel.selectedShape = routeNode;
                        routeNode.isSelected = true;
                        ((FrmMain) this.viewPanel.mainWindow.MdiParent).AddTreeNode(this.viewPanel.mainWindow, routeNode);
                        this.viewPanel.setMainFrmTreeSelected();
                        RURouteNode node4 = new RURouteNode(this, routeNode, "OP_Add");
                        this.stackUndo.Push(node4);
                        this.setRUToolbarStatus();
                    }
                }
                else if (theCopiedObjectList is ArrayList)
                {
                    ArrayList list = theCopiedObjectList as ArrayList;
                    string str2 = "";
                    foreach (DELActivityDefinition definition5 in list)
                    {
                        DELActivityDefinition newAct = this.CopyActivity(definition5);
                        if (this.IsActExists(newAct))
                        {
                            str2 = str2 + newAct.Name + "\r\n";
                        }
                        else
                        {
                            Point startPoint = new Point(this.viewPanel.mousePosition.X + newAct.PositionX, this.viewPanel.mousePosition.Y + newAct.PositionY);
                            Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode node5 = new Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode(startPoint, newAct.Name) {
                                realNode = newAct
                            };
                            this.viewPanel.addTaskNode(node5);
                            ((FrmMain) this.viewPanel.mainWindow.MdiParent).AddTreeNode(this.viewPanel.mainWindow, node5);
                            this.viewPanel.setMainFrmTreeSelected();
                        }
                    }
                    if (str2.Length > 0)
                    {
                        MessageBox.Show("下列同名活动节点复制失败：\r\n" + str2);
                    }
                }
                this.viewPanel.Refresh();
            }
        }

        private void cmiNodeProperty_Click(object sender, EventArgs e)
        {
            this.showNodeProperty();
        }

        private void cmiProProperty_Click(object sender, EventArgs e)
        {
            this.ShowProPropertyDlg();
        }

        private void cmiSaveToDB_Click(object sender, EventArgs e)
        {
            this.saveToDataBase();
        }

        private void cmiSaveToLocal_Click(object sender, EventArgs e)
        {
            this.saveToLocal();
        }

        public void cmiShapeDelete_Click(object sender, EventArgs e)
        {
            this.viewPanel.deleteObject();
        }

        private void cmiVerify_Click(object sender, EventArgs e)
        {
            ErrorDetective detective = new ErrorDetective(this.viewPanel.shapeData);
            if (detective.Static_CheckError())
            {
                try
                {
                    this.viewPanel.shapeData.pictureToDataSet();
                }
                catch (Exception exception)
                {
                    BPMModelExceptionHandle.InvokeBPMExceptionHandler(exception);
                    return;
                }
                string errorResult = "";
                this.verifyModel(out errorResult);
                MessageBox.Show(errorResult, "验证结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public void ControlPaste()
        {
            this.cmiNodePaste.Enabled = this.tiMain.TheCopiedObjectList.Count > 0;
        }

        public void ControlPerm()
        {
            this.cmiSaveToDB.Visible = true;
            PLGrantPerm perm = new PLGrantPerm();
            if ((!this.tiMain.GetAllowCreateProcessManagement() || !this.isNew) && (perm.CanDoObjectOperation(ClientData.LogonUser.Oid, this.viewPanel.shapeData.template.ID, "BPM_PROCESS_DEFINITION", "ClaRel_MODIFY", 0, Guid.Empty) == 0))
            {
                this.cmiSaveToDB.Visible = false;
            }
        }

        public void controlTBButton(string flag)
        {
            if (flag.Equals("TBCursor"))
            {
                if (this.viewPanel.cursorFlag)
                {
                    this.toolBarBunCursor.Pushed = true;
                }
                else
                {
                    this.viewPanel.cursorFlag = true;
                    this.viewPanel.startNodeFlag = false;
                    this.viewPanel.endNodeFlag = false;
                    this.viewPanel.taskNodeFlag = false;
                    this.viewPanel.routeNodeFlag = false;
                    this.toolBarBunCursor.Pushed = true;
                    this.toolBarBunTaskNode.Pushed = false;
                    this.toolBarBunRouteNode.Pushed = false;
                    this.viewPanel.Cursor = Cursors.Default;
                }
            }
            else if (flag.Equals("TBStart"))
            {
                if (!this.viewPanel.startNodeFlag)
                {
                    this.viewPanel.cursorFlag = false;
                    this.viewPanel.startNodeFlag = true;
                    this.viewPanel.endNodeFlag = false;
                    this.viewPanel.taskNodeFlag = false;
                    this.viewPanel.routeNodeFlag = false;
                    this.toolBarBunCursor.Pushed = false;
                    this.toolBarBunTaskNode.Pushed = false;
                    this.toolBarBunRouteNode.Pushed = false;
                    this.viewPanel.Cursor = Cursors.Cross;
                }
            }
            else if (flag.Equals("TBEnd"))
            {
                if (!this.viewPanel.endNodeFlag)
                {
                    this.viewPanel.cursorFlag = false;
                    this.viewPanel.startNodeFlag = false;
                    this.viewPanel.endNodeFlag = true;
                    this.viewPanel.taskNodeFlag = false;
                    this.viewPanel.routeNodeFlag = false;
                    this.toolBarBunCursor.Pushed = false;
                    this.toolBarBunTaskNode.Pushed = false;
                    this.toolBarBunRouteNode.Pushed = false;
                    this.viewPanel.Cursor = Cursors.Cross;
                }
            }
            else if (flag.Equals("TBTask"))
            {
                if (this.viewPanel.taskNodeFlag)
                {
                    this.toolBarBunTaskNode.Pushed = true;
                }
                else
                {
                    this.viewPanel.cursorFlag = false;
                    this.viewPanel.startNodeFlag = false;
                    this.viewPanel.endNodeFlag = false;
                    this.viewPanel.taskNodeFlag = true;
                    this.viewPanel.routeNodeFlag = false;
                    this.toolBarBunCursor.Pushed = false;
                    this.toolBarBunTaskNode.Pushed = true;
                    this.toolBarBunRouteNode.Pushed = false;
                    this.viewPanel.Cursor = Cursors.Cross;
                }
            }
            else if (flag.Equals("TBRoute"))
            {
                if (this.viewPanel.routeNodeFlag)
                {
                    this.toolBarBunRouteNode.Pushed = true;
                }
                else
                {
                    this.viewPanel.cursorFlag = false;
                    this.viewPanel.startNodeFlag = false;
                    this.viewPanel.endNodeFlag = false;
                    this.viewPanel.taskNodeFlag = false;
                    this.viewPanel.routeNodeFlag = true;
                    this.toolBarBunCursor.Pushed = false;
                    this.toolBarBunTaskNode.Pushed = false;
                    this.toolBarBunRouteNode.Pushed = true;
                    this.viewPanel.Cursor = Cursors.Cross;
                }
            }
            else if (flag.Equals("TBUndo"))
            {
                RUObject obj2 = (RUObject) this.stackUndo.Pop();
                obj2.Undo();
                this.stackRedo.Push(obj2);
                this.setRUToolbarStatus();
                this.viewPanel.Refresh();
            }
            else if (flag.Equals("TBRedo"))
            {
                RUObject obj3 = (RUObject) this.stackRedo.Pop();
                obj3.Redo();
                this.stackUndo.Push(obj3);
                this.setRUToolbarStatus();
                this.viewPanel.Refresh();
            }
        }

        private DELActivityDefinition CopyActivity(DELActivityDefinition oldAct)
        {
            if (this.proTemplate == null)
            {
                this.proTemplate = this.viewPanel.shapeData.template;
            }
            DELActivityDefinition newAct = new DELActivityDefinition(Guid.NewGuid(), this.viewPanel.shapeData.template.ID) {
                Name = oldAct.Name,
                Options = oldAct.Options,
                DocumentationID = oldAct.DocumentationID,
                Duration = oldAct.Duration,
                DurationUnit = oldAct.DurationUnit,
                Height = oldAct.Height,
                IconID = oldAct.IconID,
                IsAutomatic = oldAct.IsAutomatic,
                MultiHandlersRule = oldAct.MultiHandlersRule,
                NodeType = oldAct.NodeType,
                OverTimeHandleRule = oldAct.OverTimeHandleRule,
                Priority = oldAct.Priority,
                PositionX = oldAct.PositionX,
                PositionY = oldAct.PositionY,
                Quorum = oldAct.Quorum,
                rmTiModeler = oldAct.rmTiModeler,
                State = oldAct.State,
                Width = oldAct.Width,
                AutoSendMessage = oldAct.AutoSendMessage,
                Description = oldAct.Description,
                SubFlowID = oldAct.SubFlowID
            };
            if (this.viewPanel.shapeData.template.ID == oldAct.ProcessDefinitionID)
            {
                if (this.viewPanel.shapeData.template.GrantDefList != null)
                {
                    ArrayList c = new ArrayList();
                    foreach (DELBPMGrantDef def in this.viewPanel.shapeData.template.GrantDefList)
                    {
                        if (def.ActivityDefID == oldAct.ID)
                        {
                            DELBPMGrantDef def2 = new DELBPMGrantDef {
                                GrantDefID = Guid.NewGuid(),
                                ActivityDefID = newAct.ID,
                                Autho = def.Autho,
                                Class = def.Class,
                                ClassID = def.ClassID,
                                CLSID = def.CLSID,
                                OperationID = def.OperationID,
                                ProcessDataDefID = def.ProcessDataDefID,
                                ProcessDefID = def.ProcessDefID,
                                Reserve1 = def.Reserve1,
                                Reserve2 = def.Reserve2,
                                theGrantType = def.theGrantType
                            };
                            c.Add(def2);
                        }
                    }
                    this.viewPanel.shapeData.template.GrantDefList.AddRange(c);
                }
                this.CopyGrant(oldAct.ID, newAct.ID);
            }
            this.CopyPartici(newAct.Actor, oldAct.Actor);
            this.CopyPartici(newAct.ActorPre, oldAct.ActorPre);
            this.CopyPartici(newAct.Monitor, oldAct.Monitor);
            newAct.OperationsWhenActivated = new DELBPMEntityList();
            newAct.OperationsWhenCompleted = new DELBPMEntityList();
            newAct.OperationsWhenExecuted = new DELBPMEntityList();
            ArrayList list2 = new ArrayList();
            ArrayList list3 = new ArrayList();
            foreach (DELRActivityDefOperation operation in oldAct.OperationsWhenActivated)
            {
                DELRActivityDefOperation operation2 = this.CopyActOper(newAct, operation);
                newAct.OperationsWhenActivated.Add(operation2);
                list2.Add(operation2);
                list3.Add(operation);
            }
            foreach (DELRActivityDefOperation operation3 in oldAct.OperationsWhenCompleted)
            {
                DELRActivityDefOperation operation4 = this.CopyActOper(newAct, operation3);
                newAct.OperationsWhenCompleted.Add(operation4);
                list2.Add(operation4);
                list3.Add(operation3);
            }
            foreach (DELRActivityDefOperation operation5 in oldAct.OperationsWhenExecuted)
            {
                DELRActivityDefOperation operation6 = this.CopyActOper(newAct, operation5);
                newAct.OperationsWhenExecuted.Add(operation6);
                list2.Add(operation6);
                list3.Add(operation5);
            }
            if (newAct.ProcessDefinitionID == oldAct.ProcessDefinitionID)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    DELRActivityDefOperation actOpr = list2[i] as DELRActivityDefOperation;
                    DELRActivityDefOperation operation8 = list3[i] as DELRActivityDefOperation;
                    DELOperation op = Model.FindOperation(actOpr.OperationID);
                    if ((op != null) && (op.AddinOid != Guid.Empty))
                    {
                        DELOperationDefinitionArgs oda = new DELOperationDefinitionArgs {
                            IsCopying = true,
                            AddinOid = op.AddinOid,
                            ActOperationDefID = operation8.ID,
                            ActivityDefinitionID = actOpr.ActivityDefinitionID,
                            OperationDefinitionID = op.ID,
                            ExecuteOrder = actOpr.ExecuteOrder
                        };
                        if (this.proTemplate.OperationDefinitionArguments.Contains(operation8.ID))
                        {
                            ArrayList list4 = new ArrayList();
                            DELOperationDefinitionArgs args2 = this.proTemplate.OperationDefinitionArguments[operation8.ID] as DELOperationDefinitionArgs;
                            if (args2.Tag != null)
                            {
                                list4.AddRange(args2.Tag as ArrayList);
                                oda.Tag = list4;
                            }
                        }
                        DELDefinitionSpecificArgs dsa = new DELDefinitionSpecificArgs(this.proTemplate, newAct, actOpr, op);
                        IOperationClientDefinition addinEntryObject = AddinFramework.Instance.GetAddinEntryObject(op.AddinOid) as IOperationClientDefinition;
                        if (addinEntryObject != null)
                        {
                            addinEntryObject.ConfigOperation(dsa, oda);
                            oda.IsCopying = false;
                            oda.ForceStateChange(DataRowState.Added);
                        }
                        this.proTemplate.OperationDefinitionArguments.Add(actOpr.ID, oda);
                    }
                }
            }
            return newAct;
        }

        private DELRActivityDefOperation CopyActOper(DELActivityDefinition newAct, DELRActivityDefOperation oldActOper)
        {
            DELRActivityDefOperation operation = new DELRActivityDefOperation {
                ActivityDefinitionID = newAct.ID,
                ProcessDefinitionID = this.viewPanel.shapeData.template.ID,
                WhenExecute = oldActOper.WhenExecute,
                ExecuteOrder = oldActOper.ExecuteOrder,
                OperationID = oldActOper.OperationID,
                OperationName = oldActOper.OperationName,
                Reserve1 = oldActOper.Reserve1
            };
            if (operation.ProcessDefinitionID == oldActOper.ProcessDefinitionID)
            {
                foreach (DELROperationParameterDef def in oldActOper.Parameters)
                {
                    DELROperationParameterDef def2 = new DELROperationParameterDef {
                        ActivityDefinitionID = newAct.ID,
                        Name = def.Name,
                        OperationID = def.OperationID,
                        OperationName = def.OperationName,
                        ParameterOrder = def.ParameterOrder,
                        ProcessDefinitionDataID = def.ProcessDefinitionDataID,
                        ProcessDefinitionID = def.ProcessDefinitionID,
                        Reserve1 = def.Reserve1,
                        WhenExecute = def.WhenExecute,
                        ExecuteOrder = def.ExecuteOrder
                    };
                    operation.Parameters.Add(def2);
                    this.CopyGrant(def.ID, def2.ID);
                }
                foreach (DELBOBRuleDef def3 in oldActOper.RuleList)
                {
                    DELBOBRuleDef def4 = new DELBOBRuleDef {
                        ActivityID = operation.ActivityDefinitionID,
                        ProcessID = operation.ProcessDefinitionID,
                        BOBrowserID = operation.ID,
                        BOGroupIDList = new ArrayList(def3.BOGroupIDList),
                        Name = def3.Name,
                        Options = def3.Options,
                        RowInDataSet = null
                    };
                    operation.RuleList.Add(def4);
                }
                this.CopyGrant(oldActOper.ID, operation.ID);
            }
            return operation;
        }

        private void CopyGrant(Guid oldEntityID, Guid newEntityID)
        {
            if (this.viewPanel.shapeData.template.GrantList != null)
            {
                DELBPMEntityList list = this.viewPanel.shapeData.template.GrantList[oldEntityID] as DELBPMEntityList;
                if (list != null)
                {
                    DELBPMEntityList list2 = new DELBPMEntityList();
                    this.viewPanel.shapeData.template.GrantList.Add(newEntityID, list2);
                    foreach (DEClassGrant grant in list)
                    {
                        DEClassGrant grant2 = new DEClassGrant {
                            Oid = Guid.NewGuid(),
                            ClassName = grant.ClassName,
                            IsAlwaysValid = grant.IsAlwaysValid,
                            IsInherit = grant.IsInherit,
                            PermLabel = grant.PermLabel,
                            PermName = grant.PermName,
                            PermStatus = grant.PermStatus,
                            Phase = grant.Phase,
                            Principal = newEntityID,
                            StartTime = grant.StartTime,
                            EndTime = grant.EndTime,
                            State = DataRowState.Added
                        };
                        list2.Add(grant2);
                    }
                }
            }
        }

        private void CopyGrant(DELProcessDefinition oldTemplete, DELProcessDefinition newTemplete, Guid oldEntityID, Guid newEntityID)
        {
            if (oldTemplete.GrantList != null)
            {
                DELBPMEntityList list = oldTemplete.GrantList[oldEntityID] as DELBPMEntityList;
                if (list != null)
                {
                    DELBPMEntityList list2 = new DELBPMEntityList();
                    newTemplete.GrantList.Add(newEntityID, list2);
                    foreach (DEClassGrant grant in list)
                    {
                        DEClassGrant grant2 = new DEClassGrant {
                            Oid = Guid.NewGuid(),
                            ClassName = grant.ClassName,
                            IsAlwaysValid = grant.IsAlwaysValid,
                            IsInherit = grant.IsInherit,
                            PermLabel = grant.PermLabel,
                            PermName = grant.PermName,
                            PermStatus = grant.PermStatus,
                            Phase = grant.Phase,
                            Principal = newEntityID,
                            StartTime = grant.StartTime,
                            EndTime = grant.EndTime,
                            State = DataRowState.Added
                        };
                        list2.Add(grant2);
                    }
                }
            }
        }

        private void CopyPartici(DELParticipantDefinition newPar, DELParticipantDefinition oldPar)
        {
            newPar.Name = oldPar.Name;
            newPar.Reserve1 = oldPar.Reserve1;
            newPar.Description = oldPar.Description;
            newPar.Users = new DELBPMEntityList();
            foreach (DELRParticipantDefUser user in oldPar.Users)
            {
                DELRParticipantDefUser user2 = new DELRParticipantDefUser {
                    ID = Guid.NewGuid(),
                    NAME = user.NAME,
                    ParticipantID = newPar.ID,
                    ProcessDefinitionID = newPar.ProcessDefinitionID,
                    Reserve1 = user.Reserve1,
                    UserID = user.UserID,
                    UserName = user.UserName,
                    UserType = user.UserType
                };
                newPar.Users.Add(user2);
            }
        }

        private DELRouterDefinition CopyRouter(DELRouterDefinition oldRouter) {
         return new DELRouterDefinition { 
                ID = Guid.NewGuid(),
                ProcessDefinitionID = this.viewPanel.shapeData.template.ID,
                Height = oldRouter.Height,
                Width = oldRouter.Width,
                DurationUnit = oldRouter.DurationUnit,
                Duration = oldRouter.Duration,
                DocumentationID = oldRouter.DocumentationID,
                IconID = oldRouter.IconID,
                JoinExpression = oldRouter.JoinExpression,
                JoinType = oldRouter.JoinType,
                NodeType = oldRouter.NodeType,
                OverTimeHandleRule = oldRouter.OverTimeHandleRule,
                Priority = oldRouter.Priority,
                SplitExpression = oldRouter.SplitExpression,
                SplitType = oldRouter.SplitType,
                State = oldRouter.State
            };
        }
        private void CreatNewProFromOld(DELProcessDefinition OldProcessDefinition, DELProcessDefinition template)
        {
            ProcessDefinitionDS dataSet = OldProcessDefinition.GetDataSet();
            template.SetDataSet(dataSet);
            dataSet = template.GetDataSet();
            template.GrantDefList = OldProcessDefinition.GrantDefList;
            template.TargetBusinessObjects = OldProcessDefinition.TargetBusinessObjects;
            template.Variables = OldProcessDefinition.Variables;
            template.ProcessDefToObjList = OldProcessDefinition.ProcessDefToObjList;
            Picture root = new Picture();
            this.AfterProcessIDChanged(OldProcessDefinition, template);
            try
            {
                Point point;
                int positionX;
                int positionY;
                DataRow row = dataSet.Tables["PLM_BPM_PROCESS_DEFINITION"].Rows[0];
                if (row["PLM_NAME"] != DBNull.Value)
                {
                    template.Name = (string) row["PLM_NAME"];
                }
                if (row["PLM_CLSID"] != DBNull.Value)
                {
                    template.CLSID = decimal.ToInt32((decimal) row["PLM_CLSID"]);
                }
                if (row["PLM_DESCRIPTION"] != DBNull.Value)
                {
                    template.Description = (string) row["PLM_DESCRIPTION"];
                }
                if (row["PLM_STATE"] != DBNull.Value)
                {
                    template.State = (string) row["PLM_STATE"];
                }
                if (row["PLM_INITIATIONTYPE"] != DBNull.Value)
                {
                    template.InitiationType = decimal.ToInt32((decimal) row["PLM_INITIATIONTYPE"]);
                }
                if (row["PLM_INITIATIONEVENT"] != DBNull.Value)
                {
                    template.InitiationEvent = (string) row["PLM_INITIATIONEVENT"];
                }
                if (row["PLM_EVENTKEY"] != DBNull.Value)
                {
                    template.EventKey = (string) row["PLM_EVENTKEY"];
                }
                if (row["PLM_ISVISIBLE"] != DBNull.Value)
                {
                    template.IsVisible = decimal.ToInt32((decimal) row["PLM_ISVISIBLE"]);
                }
                if (row["PLM_DURATION"] != DBNull.Value)
                {
                    template.Duration = decimal.ToInt32((decimal) row["PLM_DURATION"]);
                }
                if (row["PLM_DURATIONUNIT"] != DBNull.Value)
                {
                    template.DurationUnit = (string) row["PLM_DURATIONUNIT"];
                }
                if (row["PLM_OVERTIMEHANDLERULE"] != DBNull.Value)
                {
                    template.OverTimeHandleRule = (string) row["PLM_OVERTIMEHANDLERULE"];
                }
                if (row["PLM_PRIORITY"] != DBNull.Value)
                {
                    template.Priority = decimal.ToInt32((decimal) row["PLM_PRIORITY"]);
                }
                if (row["PLM_DOCUMENTAIONID"] != DBNull.Value)
                {
                    template.DocumentationID = new Guid((byte[]) row["PLM_DOCUMENTAIONID"]);
                }
                if (row["PLM_ICONID"] != DBNull.Value)
                {
                    template.IconID = new Guid((byte[]) row["PLM_ICONID"]);
                }
                if (row["PLM_CLASSIFICATION"] != DBNull.Value)
                {
                    template.Classification = decimal.ToInt32((decimal) row["PLM_CLASSIFICATION"]);
                }
                if (row["PLM_INSTANCESCOUNT"] != DBNull.Value)
                {
                    template.InstanceCount = decimal.ToInt32((decimal) row["PLM_INSTANCESCOUNT"]);
                }
                if (row["PLM_OWNERID"] != DBNull.Value)
                {
                    template.OwnerID = new Guid((byte[]) row["PLM_OWNERID"]);
                }
                if (row["PLM_ISPUBLISHED"] != DBNull.Value)
                {
                    template.Option = decimal.ToInt32((decimal) row["PLM_ISPUBLISHED"]);
                }
                if (row["PLM_EXPRESSION"] != DBNull.Value)
                {
                    template.Expression = (string) row["PLM_EXPRESSION"];
                }
                template.IsNew = 0;
                int count = dataSet.Tables["PLM_BPM_PROCESSDATA_DEFINITION"].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    row = dataSet.Tables["PLM_BPM_PROCESSDATA_DEFINITION"].Rows[i];
                    Guid newProcessDataId = Guid.NewGuid();
                    Guid oldProcessDataId = new Guid((byte[]) row["PLM_OID"]);
                    row["PLM_OID"] = newProcessDataId.ToByteArray();
                    this.AfterProcessDataIDChanged(OldProcessDefinition, template, oldProcessDataId, newProcessDataId);
                    if (((decimal.ToInt32((decimal) row["PLM_DATATYPE"]) == 9) || (decimal.ToInt32((decimal) row["PLM_DATATYPE"]) == 10)) || (decimal.ToInt32((decimal) row["PLM_DATATYPE"]) == 8))
                    {
                        foreach (DELProcessDataDefinition definition in template.TargetBusinessObjects)
                        {
                            if (oldProcessDataId == definition.ID)
                            {
                                definition.RowInDataSet = row;
                                definition.ID = newProcessDataId;
                                break;
                            }
                        }
                    }
                    else
                    {
                        DELProcessDataDefinition definition2 = new DELProcessDataDefinition();
                        if (row["PLM_CLSID"] != DBNull.Value)
                        {
                            definition2.CLSID = decimal.ToInt32((decimal) row["PLM_CLSID"]);
                        }
                        if (row["PLM_COUNT"] != DBNull.Value)
                        {
                            definition2.Count = decimal.ToInt32((decimal) row["PLM_COUNT"]);
                        }
                        if (row["PLM_DATATYPE"] != DBNull.Value)
                        {
                            definition2.DataType = decimal.ToInt32((decimal) row["PLM_DATATYPE"]);
                        }
                        if (row["PLM_VALUE"] != DBNull.Value)
                        {
                            definition2.Value = (string) row["PLM_VALUE"];
                        }
                        if (row["PLM_BOCLASS"] != DBNull.Value)
                        {
                            definition2.BOClass = (string) row["PLM_BOCLASS"];
                        }
                        if (row["PLM_BONAME"] != DBNull.Value)
                        {
                            definition2.BOName = (string) row["PLM_BONAME"];
                        }
                        if (row["PLM_STATE"] != DBNull.Value)
                        {
                            definition2.State = (string) row["PLM_STATE"];
                        }
                        if (row["PLM_DESCRIPTION"] != DBNull.Value)
                        {
                            definition2.Description = (string) row["PLM_DESCRIPTION"];
                        }
                        if (row["PLM_ISINPUT"] != DBNull.Value)
                        {
                            definition2.IsInput = decimal.ToInt32((decimal) row["PLM_ISINPUT"]);
                        }
                        if (row["PLM_ISOUTPUT"] != DBNull.Value)
                        {
                            definition2.IsOutput = decimal.ToInt32((decimal) row["PLM_ISOUTPUT"]);
                        }
                        if (row["PLM_MAXSUBID"] != DBNull.Value)
                        {
                            definition2.MaxSubID = decimal.ToInt32((decimal) row["PLM_MAXSUBID"]);
                        }
                        if (row["PLM_NAME"] != DBNull.Value)
                        {
                            definition2.Name = (string) row["PLM_NAME"];
                        }
                        if (row["PLM_OID"] != DBNull.Value)
                        {
                            definition2.ID = new Guid((byte[]) row["PLM_OID"]);
                        }
                        if (row["PLM_PROCESSDEFINITIONID"] != DBNull.Value)
                        {
                            definition2.ProcessDefinitionID = template.ID;
                        }
                        if (row["PLM_SUBID"] != DBNull.Value)
                        {
                            definition2.SubID = decimal.ToInt32((decimal) row["PLM_SUBID"]);
                        }
                        definition2.RowInDataSet = row;
                    }
                }
                int num5 = dataSet.Tables["PLM_BPM_PARTICIPANT_DEFINITION"].Rows.Count;
                for (int j = 0; j < num5; j++)
                {
                    row = dataSet.Tables["PLM_BPM_PARTICIPANT_DEFINITION"].Rows[j];
                    Guid newParticipantId = Guid.NewGuid();
                    Guid oldParticipantId = new Guid((byte[]) row["PLM_OID"]);
                    row["PLM_OID"] = newParticipantId.ToByteArray();
                    this.AfterParticipantIDChanged(OldProcessDefinition, template, oldParticipantId, newParticipantId);
                }
                ArrayList list = this.getProParticipants(template.ID, template.GetDataSet());
                for (int k = 0; k < list.Count; k++)
                {
                    if (((DELParticipantDefinition) list[k]).Name.Equals("ProcessActor"))
                    {
                        template.Actor = (DELParticipantDefinition) list[k];
                    }
                    else if (((DELParticipantDefinition) list[k]).Name.Equals("ProcessMonitor"))
                    {
                        template.Monitor = (DELParticipantDefinition) list[k];
                    }
                }
                int num8 = dataSet.Tables["PLM_BPM_ACTIVITY_DEFINITION"].Rows.Count;
                for (int m = 0; m < num8; m++)
                {
                    row = dataSet.Tables["PLM_BPM_ACTIVITY_DEFINITION"].Rows[m];
                    Guid newActivityId = Guid.NewGuid();
                    Guid oldActivityId = new Guid((byte[]) row["PLM_OID"]);
                    row["PLM_OID"] = newActivityId.ToByteArray();
                    this.AfterActivityIDChanged(OldProcessDefinition, template, oldActivityId, newActivityId);
                    switch (decimal.ToInt32((decimal) row["PLM_ISAUTOMATIC"]))
                    {
                        case 2:
                        {
                            Guid activityNodeID = new Guid((byte[]) row["PLM_OID"]);
                            DELActivityDefinition definition4 = new DELActivityDefinition(activityNodeID, template.ID);
                            if (row["PLM_OID"] != DBNull.Value)
                            {
                                definition4.ID = new Guid((byte[]) row["PLM_OID"]);
                            }
                            if (row["PLM_NAME"] != DBNull.Value)
                            {
                                definition4.Name = (string) row["PLM_NAME"];
                            }
                            if (row["PLM_CLSID"] != DBNull.Value)
                            {
                                definition4.CLSID = decimal.ToInt32((decimal) row["PLM_CLSID"]);
                            }
                            if (row["PLM_DESCRIPTION"] != DBNull.Value)
                            {
                                definition4.Description = (string) row["PLM_DESCRIPTION"];
                            }
                            if (row["PLM_PROCESSDEFINITIONID"] != DBNull.Value)
                            {
                                definition4.ProcessDefinitionID = template.ID;
                                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
                            }
                            if (row["PLM_CREATORID"] != DBNull.Value)
                            {
                                definition4.CreatorID = new Guid((byte[]) row["PLM_CREATORID"]);
                            }
                            if (row["PLM_STATE"] != DBNull.Value)
                            {
                                definition4.State = (string) row["PLM_STATE"];
                            }
                            if (row["PLM_DURATION"] != DBNull.Value)
                            {
                                definition4.Duration = decimal.ToInt32((decimal) row["PLM_DURATION"]);
                            }
                            if (row["PLM_DURATIONUNIT"] != DBNull.Value)
                            {
                                definition4.DurationUnit = (string) row["PLM_DURATIONUNIT"];
                            }
                            if (row["PLM_OVERTIMEHANDLERULE"] != DBNull.Value)
                            {
                                definition4.OverTimeHandleRule = (string) row["PLM_OVERTIMEHANDLERULE"];
                            }
                            if (row["PLM_PRIORITY"] != DBNull.Value)
                            {
                                definition4.Priority = decimal.ToInt32((decimal) row["PLM_PRIORITY"]);
                            }
                            if (row["PLM_ICONID"] != DBNull.Value)
                            {
                                definition4.IconID = new Guid((byte[]) row["PLM_ICONID"]);
                            }
                            if (row["PLM_DOCUMENTAIONID"] != DBNull.Value)
                            {
                                definition4.DocumentationID = new Guid((byte[]) row["PLM_DOCUMENTAIONID"]);
                            }
                            if (row["PLM_ISAUTOMATIC"] != DBNull.Value)
                            {
                                definition4.IsAutomatic = decimal.ToInt32((decimal) row["PLM_ISAUTOMATIC"]);
                            }
                            if (row["PLM_MULTIHANDLERSRULE"] != DBNull.Value)
                            {
                                definition4.MultiHandlersRule = (string) row["PLM_MULTIHANDLERSRULE"];
                            }
                            if (row["PLM_QUORUM"] != DBNull.Value)
                            {
                                definition4.Quorum = decimal.ToInt32((decimal) row["PLM_QUORUM"]);
                            }
                            if (row["PLM_POSITIONX"] != DBNull.Value)
                            {
                                definition4.PositionX = decimal.ToInt32((decimal) row["PLM_POSITIONX"]);
                            }
                            if (row["PLM_POSITIONY"] != DBNull.Value)
                            {
                                definition4.PositionY = decimal.ToInt32((decimal) row["PLM_POSITIONY"]);
                            }
                            if (row["PLM_WIDTH"] != DBNull.Value)
                            {
                                definition4.Width = decimal.ToInt32((decimal) row["PLM_WIDTH"]);
                            }
                            if (row["PLM_HEIGHT"] != DBNull.Value)
                            {
                                definition4.Height = decimal.ToInt32((decimal) row["PLM_HEIGHT"]);
                            }
                            definition4.RowInDataSet = row;
                            positionX = definition4.PositionX;
                            positionY = definition4.PositionY;
                            point = new Point(positionX, positionY);
                            StartNode node = new StartNode(point) {
                                width = definition4.Width,
                                height = definition4.Height,
                                text = { caption = definition4.Name },
                                realNode = definition4
                            };
                            root.startNode = node;
                            root.textNodAry.Add(node.text);
                            break;
                        }
                        case 3:
                        {
                            Guid guid8 = new Guid((byte[]) row["PLM_OID"]);
                            DELActivityDefinition definition5 = new DELActivityDefinition(guid8, template.ID);
                            if (row["PLM_NAME"] != DBNull.Value)
                            {
                                definition5.Name = (string) row["PLM_NAME"];
                            }
                            if (row["PLM_CLSID"] != DBNull.Value)
                            {
                                definition5.CLSID = decimal.ToInt32((decimal) row["PLM_CLSID"]);
                            }
                            if (row["PLM_DESCRIPTION"] != DBNull.Value)
                            {
                                definition5.Description = (string) row["PLM_DESCRIPTION"];
                            }
                            if (row["PLM_PROCESSDEFINITIONID"] != DBNull.Value)
                            {
                                definition5.ProcessDefinitionID = template.ID;
                                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
                            }
                            if (row["PLM_CREATORID"] != DBNull.Value)
                            {
                                definition5.CreatorID = new Guid((byte[]) row["PLM_CREATORID"]);
                            }
                            if (row["PLM_STATE"] != DBNull.Value)
                            {
                                definition5.State = (string) row["PLM_STATE"];
                            }
                            if (row["PLM_DURATION"] != DBNull.Value)
                            {
                                definition5.Duration = decimal.ToInt32((decimal) row["PLM_DURATION"]);
                            }
                            if (row["PLM_DURATIONUNIT"] != DBNull.Value)
                            {
                                definition5.DurationUnit = (string) row["PLM_DURATIONUNIT"];
                            }
                            if (row["PLM_OVERTIMEHANDLERULE"] != DBNull.Value)
                            {
                                definition5.OverTimeHandleRule = (string) row["PLM_OVERTIMEHANDLERULE"];
                            }
                            if (row["PLM_PRIORITY"] != DBNull.Value)
                            {
                                definition5.Priority = decimal.ToInt32((decimal) row["PLM_PRIORITY"]);
                            }
                            if (row["PLM_ICONID"] != DBNull.Value)
                            {
                                definition5.IconID = new Guid((byte[]) row["PLM_ICONID"]);
                            }
                            if (row["PLM_DOCUMENTAIONID"] != DBNull.Value)
                            {
                                definition5.DocumentationID = new Guid((byte[]) row["PLM_DOCUMENTAIONID"]);
                            }
                            if (row["PLM_ISAUTOMATIC"] != DBNull.Value)
                            {
                                definition5.IsAutomatic = decimal.ToInt32((decimal) row["PLM_ISAUTOMATIC"]);
                            }
                            if (row["PLM_MULTIHANDLERSRULE"] != DBNull.Value)
                            {
                                definition5.MultiHandlersRule = (string) row["PLM_MULTIHANDLERSRULE"];
                            }
                            if (row["PLM_QUORUM"] != DBNull.Value)
                            {
                                definition5.Quorum = decimal.ToInt32((decimal) row["PLM_QUORUM"]);
                            }
                            if (row["PLM_POSITIONX"] != DBNull.Value)
                            {
                                definition5.PositionX = decimal.ToInt32((decimal) row["PLM_POSITIONX"]);
                            }
                            if (row["PLM_POSITIONY"] != DBNull.Value)
                            {
                                definition5.PositionY = decimal.ToInt32((decimal) row["PLM_POSITIONY"]);
                            }
                            if (row["PLM_WIDTH"] != DBNull.Value)
                            {
                                definition5.Width = decimal.ToInt32((decimal) row["PLM_WIDTH"]);
                            }
                            if (row["PLM_HEIGHT"] != DBNull.Value)
                            {
                                definition5.Height = decimal.ToInt32((decimal) row["PLM_HEIGHT"]);
                            }
                            definition5.RowInDataSet = row;
                            positionX = definition5.PositionX;
                            positionY = definition5.PositionY;
                            point = new Point(positionX, positionY);
                            EndNode node2 = new EndNode(point) {
                                width = definition5.Width,
                                height = definition5.Height,
                                text = { caption = definition5.Name },
                                realNode = definition5
                            };
                            root.endNode = node2;
                            root.textNodAry.Add(node2.text);
                            break;
                        }
                        default:
                        {
                            Guid guid9 = new Guid((byte[]) row["PLM_OID"]);
                            DELActivityDefinition realTaskNode = new DELActivityDefinition(guid9, template.ID);
                            if (row["PLM_NAME"] != DBNull.Value)
                            {
                                realTaskNode.Name = (string) row["PLM_NAME"];
                            }
                            if (row["PLM_CLSID"] != DBNull.Value)
                            {
                                realTaskNode.CLSID = decimal.ToInt32((decimal) row["PLM_CLSID"]);
                            }
                            if (row["PLM_DESCRIPTION"] != DBNull.Value)
                            {
                                realTaskNode.Description = (string) row["PLM_DESCRIPTION"];
                            }
                            if (row["PLM_PROCESSDEFINITIONID"] != DBNull.Value)
                            {
                                realTaskNode.ProcessDefinitionID = template.ID;
                                row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
                            }
                            if (row["PLM_CREATORID"] != DBNull.Value)
                            {
                                realTaskNode.CreatorID = new Guid((byte[]) row["PLM_CREATORID"]);
                            }
                            if (row["PLM_STATE"] != DBNull.Value)
                            {
                                realTaskNode.State = (string) row["PLM_STATE"];
                            }
                            if (row["PLM_DURATION"] != DBNull.Value)
                            {
                                realTaskNode.Duration = decimal.ToInt32((decimal) row["PLM_DURATION"]);
                            }
                            if (row["PLM_DURATIONUNIT"] != DBNull.Value)
                            {
                                realTaskNode.DurationUnit = (string) row["PLM_DURATIONUNIT"];
                            }
                            if (row["PLM_OVERTIMEHANDLERULE"] != DBNull.Value)
                            {
                                realTaskNode.OverTimeHandleRule = (string) row["PLM_OVERTIMEHANDLERULE"];
                            }
                            if (row["PLM_PRIORITY"] != DBNull.Value)
                            {
                                realTaskNode.Priority = decimal.ToInt32((decimal) row["PLM_PRIORITY"]);
                            }
                            if (row["PLM_ICONID"] != DBNull.Value)
                            {
                                realTaskNode.IconID = new Guid((byte[]) row["PLM_ICONID"]);
                            }
                            if (row["PLM_DOCUMENTAIONID"] != DBNull.Value)
                            {
                                realTaskNode.DocumentationID = new Guid((byte[]) row["PLM_DOCUMENTAIONID"]);
                            }
                            if (row["PLM_ISAUTOMATIC"] != DBNull.Value)
                            {
                                realTaskNode.IsAutomatic = decimal.ToInt32((decimal) row["PLM_ISAUTOMATIC"]);
                            }
                            if (row["PLM_MULTIHANDLERSRULE"] != DBNull.Value)
                            {
                                realTaskNode.MultiHandlersRule = (string) row["PLM_MULTIHANDLERSRULE"];
                            }
                            if (row["PLM_QUORUM"] != DBNull.Value)
                            {
                                realTaskNode.Quorum = decimal.ToInt32((decimal) row["PLM_QUORUM"]);
                            }
                            if (row["PLM_POSITIONX"] != DBNull.Value)
                            {
                                realTaskNode.PositionX = decimal.ToInt32((decimal) row["PLM_POSITIONX"]);
                            }
                            if (row["PLM_POSITIONY"] != DBNull.Value)
                            {
                                realTaskNode.PositionY = decimal.ToInt32((decimal) row["PLM_POSITIONY"]);
                            }
                            if (row["PLM_WIDTH"] != DBNull.Value)
                            {
                                realTaskNode.Width = decimal.ToInt32((decimal) row["PLM_WIDTH"]);
                            }
                            if (row["PLM_HEIGHT"] != DBNull.Value)
                            {
                                realTaskNode.Height = decimal.ToInt32((decimal) row["PLM_HEIGHT"]);
                            }
                            if (row["PLM_RESERVE1"] != DBNull.Value)
                            {
                                realTaskNode.Options = (string) row["PLM_RESERVE1"];
                            }
                            realTaskNode.RowInDataSet = row;
                            positionX = realTaskNode.PositionX;
                            positionY = realTaskNode.PositionY;
                            point = new Point(positionX, positionY);
                            Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode node3 = new Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode(point) {
                                width = realTaskNode.Width,
                                height = realTaskNode.Height,
                                text = { caption = realTaskNode.Name },
                                realNode = realTaskNode
                            };
                            root.taskNodAry.Add(node3);
                            root.textNodAry.Add(node3.text);
                            ArrayList list2 = this.getProParticipants(realTaskNode.ID, template.GetDataSet());
                            for (int num11 = 0; num11 < list2.Count; num11++)
                            {
                                if (((DELParticipantDefinition) list2[num11]).Name.Equals("ActivityActor"))
                                {
                                    realTaskNode.Actor = (DELParticipantDefinition) list2[num11];
                                }
                                else if (((DELParticipantDefinition) list2[num11]).Name.Equals("ActivityMonitorRef"))
                                {
                                    realTaskNode.Monitor = (DELParticipantDefinition) list2[num11];
                                }
                                else if (((DELParticipantDefinition) list2[num11]).Name.Equals("ActivityActorPre"))
                                {
                                    realTaskNode.ActorPre = (DELParticipantDefinition) list2[num11];
                                }
                            }
                            Model.distributeR_Act_Oper(Model.getRActOpr(dataSet.Tables["PLM_BPM_R_ACTIVITYDEF_OPER"], "PLM_ACTIVITYDEFINITIONID", realTaskNode.ID), realTaskNode, template);
                            Model.sortList(realTaskNode.OperationsWhenActivated);
                            Model.sortList(realTaskNode.OperationsWhenCompleted);
                            Model.sortList(realTaskNode.OperationsWhenExecuted);
                            break;
                        }
                    }
                }
                int num12 = dataSet.Tables["PLM_BPM_ROUTER_DEFINITION"].Rows.Count;
                for (int n = 0; n < num12; n++)
                {
                    row = dataSet.Tables["PLM_BPM_ROUTER_DEFINITION"].Rows[n];
                    Guid newRouterId = Guid.NewGuid();
                    Guid oldRouterId = new Guid((byte[]) row["PLM_OID"]);
                    row["PLM_OID"] = newRouterId.ToByteArray();
                    this.AfterRouterIDChanged(OldProcessDefinition, template, oldRouterId, newRouterId);
                    DELRouterDefinition realNode = new DELRouterDefinition {
                        ID = new Guid((byte[]) row["PLM_OID"]),
                        NodeType = StepType.ROUTER
                    };
                    if (row["PLM_NAME"] != DBNull.Value)
                    {
                        realNode.Name = (string) row["PLM_NAME"];
                    }
                    if (row["PLM_CLSID"] != DBNull.Value)
                    {
                        realNode.CLSID = decimal.ToInt32((decimal) row["PLM_CLSID"]);
                    }
                    if (row["PLM_DESCRIPTION"] != DBNull.Value)
                    {
                        realNode.Description = (string) row["PLM_DESCRIPTION"];
                    }
                    if (row["PLM_PROCESSDEFINITIONID"] != DBNull.Value)
                    {
                        realNode.ProcessDefinitionID = template.ID;
                        row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
                    }
                    if (row["PLM_CREATORID"] != DBNull.Value)
                    {
                        realNode.CreatorID = new Guid((byte[]) row["PLM_CREATORID"]);
                    }
                    if (row["PLM_STATE"] != DBNull.Value)
                    {
                        realNode.State = (string) row["PLM_STATE"];
                    }
                    if (row["PLM_DURATION"] != DBNull.Value)
                    {
                        realNode.Duration = decimal.ToInt32((decimal) row["PLM_DURATION"]);
                    }
                    if (row["PLM_DURATIONUNIT"] != DBNull.Value)
                    {
                        realNode.DurationUnit = (string) row["PLM_DURATIONUNIT"];
                    }
                    if (row["PLM_OVERTIMEHANDLERULE"] != DBNull.Value)
                    {
                        realNode.OverTimeHandleRule = (string) row["PLM_OVERTIMEHANDLERULE"];
                    }
                    if (row["PLM_PRIORITY"] != DBNull.Value)
                    {
                        realNode.Priority = decimal.ToInt32((decimal) row["PLM_PRIORITY"]);
                    }
                    if (row["PLM_ICONID"] != DBNull.Value)
                    {
                        realNode.IconID = new Guid((byte[]) row["PLM_ICONID"]);
                    }
                    if (row["PLM_DOCUMENTAIONID"] != DBNull.Value)
                    {
                        realNode.DocumentationID = new Guid((byte[]) row["PLM_DOCUMENTAIONID"]);
                    }
                    if (row["PLM_JOINTYPE"] != DBNull.Value)
                    {
                        realNode.JoinType = decimal.ToInt32((decimal) row["PLM_JOINTYPE"]);
                    }
                    if (row["PLM_JOINEXPRESSION"] != DBNull.Value)
                    {
                        realNode.JoinExpression = (string) row["PLM_JOINEXPRESSION"];
                    }
                    if (row["PLM_SPLITTYPE"] != DBNull.Value)
                    {
                        realNode.SplitType = decimal.ToInt32((decimal) row["PLM_SPLITTYPE"]);
                    }
                    if (row["PLM_SPLITEXPRESSION"] != DBNull.Value)
                    {
                        realNode.SplitExpression = (string) row["PLM_SPLITEXPRESSION"];
                    }
                    if (row["PLM_POSITIONX"] != DBNull.Value)
                    {
                        realNode.PositionX = decimal.ToInt32((decimal) row["PLM_POSITIONX"]);
                    }
                    if (row["PLM_POSITIONY"] != DBNull.Value)
                    {
                        realNode.PositionY = decimal.ToInt32((decimal) row["PLM_POSITIONY"]);
                    }
                    if (row["PLM_WIDTH"] != DBNull.Value)
                    {
                        realNode.Width = decimal.ToInt32((decimal) row["PLM_WIDTH"]);
                    }
                    if (row["PLM_HEIGHT"] != DBNull.Value)
                    {
                        realNode.Height = decimal.ToInt32((decimal) row["PLM_HEIGHT"]);
                    }
                    realNode.RowInDataSet = row;
                    positionX = realNode.PositionX;
                    positionY = realNode.PositionY;
                    point = new Point(positionX, positionY);
                    RouteNode node4 = new RouteNode(point, realNode) {
                        width = realNode.Width,
                        height = realNode.Height,
                        text = { caption = realNode.Name },
                        realNode = realNode
                    };
                    root.routeNodAry.Add(node4);
                    root.textNodAry.Add(node4.text);
                }
                int num14 = dataSet.Tables["PLM_BPM_R_STEPDEF_STEPDEF"].Rows.Count;
                for (int num15 = 0; num15 < num14; num15++)
                {
                    row = dataSet.Tables["PLM_BPM_R_STEPDEF_STEPDEF"].Rows[num15];
                    DELRStepDef2StepDef def = new DELRStepDef2StepDef();
                    if (row["PLM_FROMSTEPDEFINITIONID"] != DBNull.Value)
                    {
                        def.FromStepDefinitionID = new Guid((byte[]) row["PLM_FROMSTEPDEFINITIONID"]);
                    }
                    if (row["PLM_TOSTEPDEFINITIONID"] != DBNull.Value)
                    {
                        def.ToStepDefinitionID = new Guid((byte[]) row["PLM_TOSTEPDEFINITIONID"]);
                    }
                    if (row["PLM_PROCESSDEFINITIONID"] != DBNull.Value)
                    {
                        def.ProcessDefinitionID = template.ID;
                        row["PLM_PROCESSDEFINITIONID"] = template.ID.ToByteArray();
                    }
                    if (row["PLM_FROMSTEPTYPE"] != DBNull.Value)
                    {
                        def.FromStepType = (string) row["PLM_FROMSTEPTYPE"];
                    }
                    if (row["PLM_TOSTEPTYPE"] != DBNull.Value)
                    {
                        def.ToStepType = (string) row["PLM_TOSTEPTYPE"];
                    }
                    if (row["PLM_ISTRUE"] != DBNull.Value)
                    {
                        def.IsTrue = decimal.ToInt32((decimal) row["PLM_ISTRUE"]);
                    }
                    if (row["PLM_POSITIONLIST"] != DBNull.Value)
                    {
                        def.PositionList = Model.encodePointCollection((string) row["PLM_POSITIONLIST"]);
                    }
                    Shape startShape = Model.findShape(def.FromStepDefinitionID, root);
                    Shape endShape = Model.findShape(def.ToStepDefinitionID, root);
                    def.RowInDataSet = row;
                    Line line = new Line(startShape, endShape) {
                        realLine = def
                    };
                    line.pointsList = line.realLine.PositionList;
                    root.linAry.Add(line);
                }
                ArrayList list4 = new ArrayList();
                foreach (DataTable table in dataSet.Tables)
                {
                    DataTable table2 = new DataTable {
                        TableName = table.TableName
                    };
                    foreach (DataColumn column in table.Columns)
                    {
                        DataColumn column2 = new DataColumn(column.ColumnName, column.DataType, column.Expression, column.ColumnMapping) {
                            Unique = false
                        };
                        table2.Columns.Add(column2);
                    }
                    foreach (DataRow row2 in table.Rows)
                    {
                        table2.Rows.Add(row2.ItemArray);
                    }
                    list4.Add(table2);
                }
                dataSet.Tables.Clear();
                foreach (DataTable table3 in list4)
                {
                    dataSet.Tables.Add(table3);
                }
                DataTable table4 = dataSet.Tables["PLM_BPM_R_ACTIVITYDEF_OPER"];
                foreach (DataRow row3 in table4.Rows)
                {
                    Guid newEntityID = Guid.NewGuid();
                    Guid oldEntityID = new Guid((byte[]) row3["PLM_OID"]);
                    this.CopyGrant(OldProcessDefinition, template, oldEntityID, newEntityID);
                    new Guid((byte[]) row3["PLM_ACTIVITYDEFINITIONID"]);
                    Guid operationID = new Guid((byte[]) row3["PLM_OPERATIONID"]);
                    decimal.ToInt32((decimal) row3["PLM_WHENEXECUTE"]);
                    row3["PLM_OID"] = newEntityID.ToByteArray();
                    this.AfterRActOperIDChanged(OldProcessDefinition, template, oldEntityID, newEntityID);
                    DELOperation operation = Model.FindOperation(operationID);
                    if (operation != null)
                    {
                        DELOperationDefinitionArgs args = new DELOperationDefinitionArgs();
                        template.OperationDefinitionArguments.Add(newEntityID, args);
                        args.IsCopying = true;
                        args.CustomData.Add(newEntityID, oldEntityID);
                        args.AddinOid = operation.AddinOid;
                    }
                }
                DataTable table5 = dataSet.Tables["PLM_BPM_R_OPER_PARAMETER_DEF"];
                foreach (DataRow row4 in table5.Rows)
                {
                    Guid guid15 = Guid.NewGuid();
                    Guid guid16 = new Guid((byte[]) row4["PLM_OID"]);
                    row4["PLM_OID"] = guid15.ToByteArray();
                    this.CopyGrant(OldProcessDefinition, template, guid16, guid15);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void deleteObject()
        {
            this.viewPanel.deleteObject();
        }

        private void DoGetTemplate(object objs)
        {
            ArrayList list = (ArrayList) objs;
            IProgressCallback callback = list[0] as IProgressCallback;
            Guid userID = (Guid) list[1];
            Guid theProcessDefinitionID = (Guid) list[2];
            try
            {
                DELProcessDefinition definition;
                callback.Begin(0, 100);
                callback.SetText("正在打开流程模板");
                new BPMAdmin().GetProcessDefinition(userID, theProcessDefinitionID, out definition);
                if (definition != null)
                {
                    this.viewPanel.shapeData.template = definition;
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
            finally
            {
                if (callback != null)
                {
                    callback.End();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private ArrayList getParticipantUserList(DataTable Par_User, DELParticipantDefinition proParticipant)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < Par_User.Rows.Count; i++)
            {
                DataRow row = Par_User.Rows[i];
                Guid g = new Guid((byte[]) row["PLM_PARTICIPANTID"]);
                if (proParticipant.ID.Equals(g))
                {
                    DELRParticipantDefUser user = new DELRParticipantDefUser {
                        RowInDataSet = row,
                        ParticipantID = new Guid((byte[]) row["PLM_PARTICIPANTID"]),
                        ProcessDefinitionID = new Guid((byte[]) row["PLM_PROCESSDEFINITIONID"]),
                        UserID = new Guid((byte[]) row["PLM_USERID"]),
                        UserName = (string) row["PLM_USERNAME"],
                        UserType = decimal.ToInt32((decimal) row["PLM_USERTYPE"])
                    };
                    list.Add(user);
                }
            }
            return list;
        }

        public Guid GetProceeGuid() {
         return this.viewPanel.shapeData.template.ID;
        }
        private ArrayList getProParticipants(Guid ProcessID, DataSet tempDataSet)
        {
            ArrayList list = new ArrayList();
            int count = tempDataSet.Tables["PLM_BPM_PARTICIPANT_DEFINITION"].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow row = tempDataSet.Tables["PLM_BPM_PARTICIPANT_DEFINITION"].Rows[i];
                if (ProcessID.Equals(new Guid((byte[]) row["PLM_ENTITYDEFINITIONID"])))
                {
                    DELParticipantDefinition proParticipant = new DELParticipantDefinition {
                        CLSID = decimal.ToInt32((decimal) row["PLM_CLSID"])
                    };
                    if (row["PLM_DESCRIPTION"] != DBNull.Value)
                    {
                        proParticipant.Description = (string) row["PLM_DESCRIPTION"];
                    }
                    proParticipant.EntityDefinitionID = new Guid((byte[]) row["PLM_ENTITYDEFINITIONID"]);
                    proParticipant.EntityInstanceType = (BPMEntityType) decimal.ToInt32((decimal) row["PLM_ENTITYDEFINITIONTYPE"]);
                    proParticipant.ID = new Guid((byte[]) row["PLM_OID"]);
                    proParticipant.RowInDataSet = row;
                    proParticipant.ProcessDefinitionID = new Guid((byte[]) row["PLM_PROCESSDEFINITIONID"]);
                    proParticipant.Name = (string) row["PLM_NAME"];
                    proParticipant.Users.Clear();
                    ArrayList list2 = this.getParticipantUserList(tempDataSet.Tables["PLM_BPM_R_PARTICI_USER_DEF"], proParticipant);
                    for (int j = 0; j < list2.Count; j++)
                    {
                        proParticipant.Users.Add((DELRParticipantDefUser) list2[j]);
                    }
                    list.Add(proParticipant);
                }
            }
            return list;
        }

        private void GetTemplate(Guid UserID, Guid ProcessID)
        {
            IProgressCallback progressWindow = ClientData.GetProgressWindow();
            ArrayList state = new ArrayList {
                progressWindow,
                UserID,
                ProcessID
            };
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoGetTemplate), state);
            progressWindow.ShowWindow();
        }

        private ArrayList GetUsersByOrg(Guid org)
        {
            ArrayList members = new ArrayList();
            PLOrganization organization = new PLOrganization();
            try
            {
                members = organization.GetMembers(org);
            }
            catch (ResponsibilityException exception)
            {
                MessageBox.Show(exception.Message, "分配组织", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return members;
            }
            catch
            {
                MessageBox.Show("获取拥有指定组织的人员列表失败！", "分配组织", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return members;
            }
            return members;
        }

        private ArrayList GetUsersByRole(Guid role)
        {
            ArrayList usersByRole = new ArrayList();
            PLRole role2 = new PLRole();
            try
            {
                usersByRole = role2.GetUsersByRole(role);
            }
            catch (ResponsibilityException exception)
            {
                MessageBox.Show(exception.Message, "分配组织", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return usersByRole;
            }
            catch
            {
                MessageBox.Show("获取拥有指定角色的人员列表失败！", "分配组织", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return usersByRole;
            }
            return usersByRole;
        }

        private void hsbParent_Scroll(object sender, ScrollEventArgs e)
        {
            int num = this.hsbParent.Value - e.NewValue;
            int x = this.viewPanel.Location.X;
            int y = this.viewPanel.Location.Y;
            this.viewPanel.Location = new Point(x + num, y);
        }

        private void init()
        {
            this.resProPropertyEdit = new ResourceManager(typeof(FrmProcessEdit));
            this.fileEndChar = "." + rmTiModeler.GetObject("strXML");
            this.toolBarBunCursor.Tag = "TBCursor";
            this.toolBarBunTaskNode.Tag = "TBTask";
            this.toolBarBunRouteNode.Tag = "TBRoute";
            this.toolBarBunUndo.Tag = "TBUndo";
            this.toolBarBunRedo.Tag = "TBRedo";
            this.toolBarBunCursor.ToolTipText = "选择";
            this.toolBarBunTaskNode.ToolTipText = "任务节点";
            this.toolBarBunRouteNode.ToolTipText = "路由节点";
            this.toolBarBunUndo.ToolTipText = "撤销";
            this.toolBarBunRedo.ToolTipText = "重做";
            this.InitToolBarImage();
            this.toolBarBunCursor.Style = ToolBarButtonStyle.ToggleButton;
            this.toolBarBunTaskNode.Style = ToolBarButtonStyle.ToggleButton;
            this.toolBarBunRouteNode.Style = ToolBarButtonStyle.ToggleButton;
            this.toolBarSeprate.Style = ToolBarButtonStyle.Separator;
            this.toolBarBunUndo.Style = ToolBarButtonStyle.PushButton;
            this.toolBarBunRedo.Style = ToolBarButtonStyle.PushButton;
            this.stackUndo = new Stack();
            this.stackRedo = new Stack();
            this.arrayDeleteObject = new ArrayList();
            this.setRUToolbarStatus();
            this.buildViewPanel();
            this.viewPanel.Show();
            this.viewPanel.setSize();
            this.viewPanel.Dock = DockStyle.None;
            this.viewPanel.Size = new Size(0xbb7, 0xbb7);
            this.vsbParent.Maximum = this.viewPanel.Height;
            this.vsbParent.LargeChange = this.panelMam.Height + 1;
            this.hsbParent.Maximum = this.viewPanel.Width;
            this.hsbParent.LargeChange = this.panelMam.Width + 1;
            this.copyResult = null;
            this.cmiNodePaste.Enabled = false;
            this.TheAllProcessRuleList = new ArrayList();
            this.isProcessRuleFirstOpen = true;
        }


        private void InitToolBarImage()
        {
            ClientData.MyImageList.AddIcon("ICO_BPM_LINE");
            ClientData.MyImageList.AddIcon("ICO_BPM_START");
            ClientData.MyImageList.AddIcon("ICO_BPM_END");
            ClientData.MyImageList.AddIcon("ICO_BPM_TASK");
            ClientData.MyImageList.AddIcon("ICO_BPM_ROUTER");
            ClientData.MyImageList.AddIcon("ICO_BPM_DEF");
            ClientData.MyImageList.AddIcon("ICO_BPM_ZOOMIN");
            ClientData.MyImageList.AddIcon("ICO_BPM_ZOOMOUT");
            ClientData.MyImageList.AddIcon("ICO_BPM_ZOOMPRE");
            ClientData.MyImageList.AddIcon("ICO_BPM_ZOOMFIT");
            ClientData.MyImageList.AddIcon("ICO_RES_PICTURE");
            ClientData.MyImageList.AddIcon("ICO_BPM_POINTER");
            ClientData.MyImageList.AddIcon("ICO_BPM_UNDO");
            ClientData.MyImageList.AddIcon("ICO_BPM_REDO");
            this.tbrForWin.ImageList = ClientData.MyImageList.imageList;
            this.toolBarBunCursor.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_POINTER");
            this.toolBarBunTaskNode.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_TASK");
            this.toolBarBunRouteNode.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_ROUTER");
            this.toolBarBunUndo.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_UNDO");
            this.toolBarBunRedo.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_REDO");
        }

        private bool IsActExists(DELActivityDefinition newAct)
        {
            foreach (Shape shape in this.viewPanel.shapeData.root.taskNodAry)
            {
                DELActivityDefinition realNode = shape.realNode as DELActivityDefinition;
                if (realNode.Name.Equals(newAct.Name))
                {
                    return true;
                }
            }
            return false;
        }

        private void menuItemOpenLocal_Click(object sender, EventArgs e)
        {
            this.openLocal();
        }

        private void ModifyOneProcess()
        {
            object keyByValue = ((FrmMain) base.MdiParent).HashMDiWindows.GetKeyByValue(this);
            if (keyByValue != null)
            {
                TreeNode rootNode = (TreeNode) keyByValue;
                this.tiMain.setNodesForProcess(rootNode, this.viewPanel);
                DELProcessDefProperty tag = rootNode.Tag as DELProcessDefProperty;
                if (rootNode.Text != this.viewPanel.shapeData.template.Name)
                {
                    rootNode.Text = this.viewPanel.shapeData.template.Name;
                    tag.Name = this.viewPanel.shapeData.template.Name;
                    TreeNode parent = rootNode.Parent;
                    parent.Nodes.Remove(rootNode);
                    int index = this.tiMain.FindPosition(parent, tag);
                    parent.Nodes.Insert(index, rootNode);
                    this.tiMain.tvwNavigator.SelectedNode = rootNode;
                }
                this.Text = rmTiModeler.GetString("strWFTEditorName") + "   " + this.viewPanel.shapeData.template.Name;
                tag.UpdateDate = this.viewPanel.shapeData.template.UpdateDate;
                tag.Duration = this.viewPanel.shapeData.template.Duration;
                tag.DurationUnit = this.viewPanel.shapeData.template.DurationUnit;
                tag.Description = this.viewPanel.shapeData.template.Description;
                this.tiMain.RefreshNode(rootNode);
            }
        }

        private DELActivityDefinition OnCopyActivity(DELActivityDefinition oldAct)
        {
            DELActivityDefinition newAct = new DELActivityDefinition(oldAct.ID, oldAct.ProcessDefinitionID) {
                ID = Guid.NewGuid(),
                Name = oldAct.Name,
                rmTiModeler = null,
                RowInDataSet = null,
                Options = oldAct.Options,
                DocumentationID = oldAct.DocumentationID,
                Duration = oldAct.Duration,
                DurationUnit = oldAct.DurationUnit,
                Height = oldAct.Height,
                IconID = oldAct.IconID,
                IsAutomatic = oldAct.IsAutomatic,
                MultiHandlersRule = oldAct.MultiHandlersRule,
                NodeType = oldAct.NodeType,
                OverTimeHandleRule = oldAct.OverTimeHandleRule,
                Priority = oldAct.Priority,
                PositionX = oldAct.PositionX,
                PositionY = oldAct.PositionY,
                Quorum = oldAct.Quorum,
                State = oldAct.State,
                Width = oldAct.Width,
                Description = oldAct.Description,
                SubFlowID = oldAct.SubFlowID
            };
            this.CopyPartici(newAct.Actor, oldAct.Actor);
            this.CopyPartici(newAct.ActorPre, oldAct.ActorPre);
            this.CopyPartici(newAct.Monitor, oldAct.Monitor);
            newAct.OperationsWhenActivated = new DELBPMEntityList();
            newAct.OperationsWhenCompleted = new DELBPMEntityList();
            newAct.OperationsWhenExecuted = new DELBPMEntityList();
            this.CopyGrant(oldAct.ID, newAct.ID);
            foreach (DELRActivityDefOperation operation in oldAct.OperationsWhenActivated)
            {
                DELRActivityDefOperation operation2 = this.CopyActOper(newAct, operation);
                operation2.ID = operation.ID;
                newAct.OperationsWhenActivated.Add(operation2);
            }
            foreach (DELRActivityDefOperation operation3 in oldAct.OperationsWhenCompleted)
            {
                DELRActivityDefOperation operation4 = this.CopyActOper(newAct, operation3);
                operation4.ID = operation3.ID;
                newAct.OperationsWhenCompleted.Add(operation4);
            }
            foreach (DELRActivityDefOperation operation5 in oldAct.OperationsWhenExecuted)
            {
                DELRActivityDefOperation operation6 = this.CopyActOper(newAct, operation5);
                operation6.ID = operation5.ID;
                newAct.OperationsWhenExecuted.Add(operation6);
            }
            return newAct;
        }

        public void openDataBase(Guid UserID, Guid ProcessID)
        {
            this.GetTemplate(UserID, ProcessID);
            if (this.viewPanel.shapeData.template != null)
            {
                DELProcessDefinition template = this.viewPanel.shapeData.template;
                this.viewPanel.Dispose();
                this.buildViewPanel();
                this.viewPanel.shapeData.template = template;
                this.viewPanel.shapeData.DataSetTopicture();
                this.viewPanel.Refresh();
            }
            else
            {
                MessageBox.Show(rmTiModeler.GetString("strGetProError"));
            }
        }

        public bool openLocal()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            OpenFileDialog dialog = new OpenFileDialog {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = (string) rmTiModeler.GetObject("strXML"),
                Filter = (string) rmTiModeler.GetObject("strFileFilter1")
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                Directory.SetCurrentDirectory(currentDirectory);
                string str2 = dialog.FileName.Trim();
                if (str2.Length >= 5)
                {
                    if (str2.Substring(str2.Length - 4).Equals(this.fileEndChar))
                    {
                        FileStream input = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                        XmlTextReader reader = new XmlTextReader(input);
                        DataSet dataSet = this.viewPanel.shapeData.template.GetDataSet();
                        dataSet.EnforceConstraints = false;
                        try
                        {
                            dataSet.ReadXml(reader);
                            this.viewPanel.shapeData.template.SetDataSet((ProcessDefinitionDS) dataSet);
                            this.viewPanel.shapeData.DataSetTopicture();
                            reader.Close();
                        }
                        catch (Exception exception)
                        {
                            reader.Close();
                            MessageBox.Show(exception.Message);
                            return false;
                        }
                        DELProcessDefProperty property = new DELProcessDefProperty(this.viewPanel.shapeData.template);
                        TreeNode node = new TreeNode(property.Name + " (Local)") {
                            Tag = property
                        };
                        if (TagForTiModeler.TreeNode_BPM != null)
                        {
                            TagForTiModeler.TreeNode_BPM.Nodes.Add(node);
                            ((FrmMain) base.MdiParent).tvwNavigator.SelectedNode = node;
                            ((FrmMain) base.MdiParent).HashMDiWindows.Add(((FrmMain) base.MdiParent).tvwNavigator.SelectedNode, this);
                            ((FrmMain) base.MdiParent).setNodesForProcess(((FrmMain) base.MdiParent).tvwNavigator.SelectedNode, this.viewPanel);
                        }
                        return true;
                    }
                    MessageBox.Show(this, (string) rmTiModeler.GetObject("strFormatError"), (string) rmTiModeler.GetObject("strWarn"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                MessageBox.Show(this, (string) rmTiModeler.GetObject("strFormatError"), (string) rmTiModeler.GetObject("strWarn"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            Directory.SetCurrentDirectory(currentDirectory);
            return false;
        }

        private void panelMam_Paint(object sender, PaintEventArgs e)
        {
        }

        public DELProcessDefinition PrepareExport()
        {
            ErrorDetective detective = new ErrorDetective(this.viewPanel.shapeData);
            if (detective.Static_CheckError())
            {
                try
                {
                    this.viewPanel.shapeData.pictureToDataSet();
                    return this.viewPanel.shapeData.template;
                }
                catch (Exception exception)
                {
                    BPMModelExceptionHandle.InvokeBPMExceptionHandler(exception);
                }
            }
            return null;
        }

        public bool SaveAsAndCreateNewWFTEditor(Guid oldProTempOid, out DELProcessDefinition proTemplate)
        {
            DELProcessDefinition definition;
            proTemplate = new DELProcessDefinition();
            BPMAdmin admin = new BPMAdmin();
            try
            {
                admin.GetProcessDefinition(ClientData.LogonUser.Oid, oldProTempOid, out definition);
            }
            catch
            {
                return false;
            }
            if (definition != null)
            {
                proTemplate.CreatorID = BPMClient.UserID;
                proTemplate.CreatorName = BPMClient.UserName;
                proTemplate.CreationDate = DateTime.Now;
                proTemplate.UpdateDate = DateTime.Now;
                if (definition.Description == "")
                {
                    proTemplate.Description = "";
                }
                else
                {
                    proTemplate.Description = definition.Description;
                }
                this.CreatNewProFromOld(definition, proTemplate);
                proTemplate.Name = "AS_" + proTemplate.Name;
                FrmModifyTemplateName name = new FrmModifyTemplateName(proTemplate.ID, proTemplate.Name);
                name.ShowDialog();
                if (name.DialogResult == DialogResult.OK)
                {
                    proTemplate.Name = name.txtNewName.Text.Trim();
                    return true;
                }
            }
            return false;
        }

        public bool saveToDataBase()
        {
            PLGrantPerm perm = new PLGrantPerm();
            if ((!this.tiMain.GetAllowCreateProcessManagement() || !this.isNew) && (perm.CanDoObjectOperation(ClientData.LogonUser.Oid, this.viewPanel.shapeData.template.ID, "BPM_PROCESS_DEFINITION", "ClaRel_MODIFY", 0, Guid.Empty) == 0))
            {
                MessageBox.Show("对不起，您没有修改权限", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            BPMAdmin admin = new BPMAdmin();
            bool flag = false;
            try
            {
                flag = admin.IsProcessDefNameInDB(this.viewPanel.shapeData.template.Name, this.viewPanel.shapeData.template.ID);
                this.viewPanel.shapeData.template.UpdateDate = DateTime.Now;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
            if (flag)
            {
                MessageBox.Show("模板名称重复！");
                return false;
            }
            ErrorDetective detective = new ErrorDetective(this.viewPanel.shapeData);
            if (!detective.Static_CheckError())
            {
                return false;
            }
            try
            {
                this.viewPanel.shapeData.pictureToDataSet();
            }
            catch (Exception exception2)
            {
                BPMModelExceptionHandle.InvokeBPMExceptionHandler(exception2);
                return false;
            }
            foreach (object obj2 in this.arrayDeleteObject)
            {
                if (obj2 is Line)
                {
                    if (((Line) obj2).realLine.RowInDataSet != null)
                    {
                        ((Line) obj2).realLine.RowInDataSet.Delete();
                    }
                }
                else if (obj2 is Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode)
                {
                    if (((Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode) obj2).realNode.RowInDataSet != null)
                    {
                        ((Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode) obj2).realNode.RowInDataSet.Delete();
                    }
                }
                else if ((obj2 is RouteNode) && (((RouteNode) obj2).realNode.RowInDataSet != null))
                {
                    ((RouteNode) obj2).realNode.RowInDataSet.Delete();
                }
            }
            this.arrayDeleteObject.Clear();
            this.stackRedo.Clear();
            this.stackUndo.Clear();
            this.setRUToolbarStatus();
            foreach (DELProcessRule rule in this.TheAllProcessRuleList)
            {
                if (rule.ruleOperator == 1)
                {
                    Guid leftActivityID = rule.leftActivityID;
                    Guid rightActivityID = rule.rightActivityID;
                    DELParticipantDefinition actor = new DELParticipantDefinition();
                    DELParticipantDefinition definition2 = new DELParticipantDefinition();
                    ArrayList list = new ArrayList();
                    ArrayList list2 = new ArrayList();
                    foreach (Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode node in this.viewPanel.shapeData.root.taskNodAry)
                    {
                        DELActivityDefinition realNode = (DELActivityDefinition) node.realNode;
                        if (realNode.ID == leftActivityID)
                        {
                            actor = realNode.Actor;
                        }
                        if (realNode.ID == rightActivityID)
                        {
                            definition2 = realNode.Actor;
                        }
                    }
                    foreach (DELRParticipantDefUser user in actor.Users)
                    {
                        switch (user.UserType)
                        {
                            case 0:
                                list.Add(user.UserID);
                                break;

                            case 3:
                                foreach (DEUser user2 in this.GetUsersByRole(user.UserID))
                                {
                                    list.Add(user2.Oid);
                                }
                                break;

                            case 4:
                                foreach (DEUser user3 in this.GetUsersByOrg(user.UserID))
                                {
                                    list.Add(user3.Oid);
                                }
                                break;
                        }
                    }
                    foreach (DELRParticipantDefUser user4 in definition2.Users)
                    {
                        switch (user4.UserType)
                        {
                            case 0:
                                list2.Add(user4.UserID);
                                break;

                            case 3:
                                foreach (DEUser user5 in this.GetUsersByRole(user4.UserID))
                                {
                                    list2.Add(user5.Oid);
                                }
                                break;

                            case 4:
                                foreach (DEUser user6 in this.GetUsersByOrg(user4.UserID))
                                {
                                    list2.Add(user6.Oid);
                                }
                                break;
                        }
                    }
                    foreach (Guid guid3 in list)
                    {
                        if (!list2.Contains(guid3))
                        {
                            MessageBox.Show("你定制的人员分派规则：(" + rule.ToString() + ") 为一致，但左活动::" + rule.leftActivityName + " 与右活动:" + rule.rightActivityName + " 的初始化人员不一致，请修改后再保存模板！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return false;
                        }
                    }
                    foreach (Guid guid4 in list2)
                    {
                        if (!list.Contains(guid4))
                        {
                            MessageBox.Show("你定制的人员分派规则：(" + rule.ToString() + ") 为一致，但左活动::" + rule.leftActivityName + " 与右活动:" + rule.rightActivityName + " 的初始化人员不一致，请修改后再保存模板！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return false;
                        }
                    }
                }
            }
            if (!this.isProcessRuleFirstOpen)
            {
                BPMAdmin admin2 = new BPMAdmin();
                try
                {
                    ArrayList theAllProcessRuleList = new ArrayList();
                    admin2.GetAllRulesByProcessDefinitionID(this.proTemplate.CreatorID, this.proTemplate.ID, out theAllProcessRuleList);
                    for (int i = 0; i < theAllProcessRuleList.Count; i++)
                    {
                        DELProcessRule item = (DELProcessRule) theAllProcessRuleList[i];
                        if (this.TheAllProcessRuleList.Contains(item))
                        {
                            this.TheAllProcessRuleList.Remove(item);
                        }
                        else
                        {
                            admin2.DeleteProcessRuleByOid(this.proTemplate.CreatorID, item.ID);
                        }
                    }
                }
                catch (Exception exception3)
                {
                    MessageBox.Show(exception3.Message);
                }
            }
            try
            {
                this.viewPanel.shapeData.saveToBase();
                this.isNew = false;
                this.ModifyOneProcess();
            }
            catch (Exception exception4)
            {
                MessageBox.Show("模板保存失败，原因:" + exception4.Message, "保存模板", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            this.viewPanel.shapeData.template.GetDataSet().AcceptChanges();
            this.viewPanel.modified = false;
            if (!this.isProcessRuleFirstOpen)
            {
                BPMAdmin admin3 = new BPMAdmin();
                try
                {
                    for (int j = 0; j < this.TheAllProcessRuleList.Count; j++)
                    {
                        DELProcessRule theProcessRule = (DELProcessRule) this.TheAllProcessRuleList[j];
                        admin3.CreateProessRule(this.proTemplate.CreatorID, theProcessRule);
                    }
                }
                catch (Exception exception5)
                {
                    MessageBox.Show(exception5.Message);
                }
            }
            this.isProcessRuleFirstOpen = true;
            return true;
        }

        public void saveToLocal()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            SaveFileDialog dialog = new SaveFileDialog {
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = (string) rmTiModeler.GetObject("strXML"),
                Filter = (string) rmTiModeler.GetObject("strFileFilter2")
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                FileStream w = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                XmlTextWriter writer = new XmlTextWriter(w, Encoding.Unicode);
                try
                {
                    this.viewPanel.shapeData.pictureToDataSet();
                    DataSet dataSet = this.viewPanel.shapeData.template.GetDataSet();
                    try
                    {
                        dataSet.WriteXml(writer);
                    }
                    catch (IOException exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    finally
                    {
                        writer.Close();
                        dialog.Dispose();
                        Directory.SetCurrentDirectory(currentDirectory);
                    }
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.Message);
                }
                finally
                {
                    dialog.Dispose();
                    Directory.SetCurrentDirectory(currentDirectory);
                }
                this.viewPanel.setModified(false);
            }
        }

        public void setRUToolbarStatus()
        {
            if (this.stackUndo.Count > 0)
            {
                this.toolBarBunUndo.Enabled = true;
            }
            else
            {
                this.toolBarBunUndo.Enabled = false;
            }
            if (this.stackRedo.Count > 0)
            {
                this.toolBarBunRedo.Enabled = true;
            }
            else
            {
                this.toolBarBunRedo.Enabled = false;
            }
        }

        public void showNodeProperty()
        {
            if (this.viewPanel.selectedShape != null)
            {
                string str = this.viewPanel.selectedShape.GetType().Name.ToString();
                if (str.Equals(rmTiModeler.GetString("strTaskNodeEng")))
                {
                    FrmTaskPropertyDlg taskDlg = new FrmTaskPropertyDlg(false) {
                        proTemplate = this.viewPanel.shapeData.template,
                        realTaskNode = (DELActivityDefinition) this.viewPanel.selectedShape.realNode,
                        myAct = (DELActivityDefinition) this.viewPanel.selectedShape.realNode
                    };
                    ArrayList list = new ArrayList();
                    for (int i = 0; i < this.TheAllProcessRuleList.Count; i++)
                    {
                        DELProcessRule rule = (DELProcessRule) this.TheAllProcessRuleList[i];
                        list.Add(rule);
                    }
                    taskDlg.TheAllProcessRuleList = this.TheAllProcessRuleList;
                    taskDlg.isProcessRuleFirstOpen = this.isProcessRuleFirstOpen;
                    this.viewPanel.InitFrmTaskPropertyDlg(taskDlg, (Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode) this.viewPanel.selectedShape);
                    if (taskDlg.ShowDialog(this) == DialogResult.OK)
                    {
                        this.viewPanel.ModifyTaskValue(taskDlg, (Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode) this.viewPanel.selectedShape);
                    }
                    else if (list.Count != 0)
                    {
                        this.TheAllProcessRuleList.Clear();
                        for (int j = 0; j < list.Count; j++)
                        {
                            DELProcessRule rule2 = (DELProcessRule) list[j];
                            this.TheAllProcessRuleList.Add(rule2);
                        }
                    }
                    this.isProcessRuleFirstOpen = taskDlg.getIsProcessRuleFirstOpen();
                    this.viewPanel.Refresh();
                }
                else if (str.Equals(rmTiModeler.GetString("strRouteNodeEng")))
                {
                    FrmRouteDlg routeDlg = new FrmRouteDlg(false);
                    this.viewPanel.InitFrmRouteDlg(routeDlg, (RouteNode) this.viewPanel.selectedShape);
                    if (routeDlg.ShowDialog(this) == DialogResult.OK)
                    {
                        this.viewPanel.ModifyRouterValue(routeDlg, (RouteNode) this.viewPanel.selectedShape);
                    }
                    this.viewPanel.Refresh();
                }
                else if (str.Equals(rmTiModeler.GetString("strStartNodeEng")))
                {
                    FrmStartNode startDlg = new FrmStartNode();
                    this.viewPanel.InitFrmStartDlg(startDlg, (StartNode) this.viewPanel.selectedShape);
                    if (startDlg.ShowDialog(this) == DialogResult.OK)
                    {
                        this.viewPanel.ModifyStartValue(startDlg, (StartNode) this.viewPanel.selectedShape);
                    }
                    this.viewPanel.Refresh();
                }
                else if (str.Equals(rmTiModeler.GetString("strEndNodeEng")))
                {
                    FrmEndNode endDlg = new FrmEndNode();
                    this.viewPanel.InitFrmEndDlg(endDlg, (EndNode) this.viewPanel.selectedShape);
                    if (endDlg.ShowDialog(this) == DialogResult.OK)
                    {
                        this.viewPanel.ModifyEndValue(endDlg, (EndNode) this.viewPanel.selectedShape);
                    }
                    this.viewPanel.Refresh();
                }
                this.viewPanel.ChangeParentTreeNodeText();
            }
        }

        public void ShowProPropertyDlg()
        {
            PLGrantPerm perm = new PLGrantPerm();
            bool isReadOnly = (!this.tiMain.GetAllowCreateProcessManagement() || !this.isNew) && (perm.CanDoObjectOperation(ClientData.LogonUser.Oid, this.viewPanel.shapeData.template.ID, "BPM_PROCESS_DEFINITION", "ClaRel_MODIFY", 0, Guid.Empty) == 0);
            FrmProcessEdit processDlg = new FrmProcessEdit(false) {
                proTemplate = this.viewPanel.shapeData.template
            };
            processDlg.SetReadonly(isReadOnly);
            this.viewPanel.InitFrmProcessDlg(processDlg, this.viewPanel.shapeData.template);
            if (processDlg.ShowDialog(this) == DialogResult.OK)
            {
                this.viewPanel.ModifyProcessValue(processDlg, this.viewPanel.shapeData.template);
            }
        }

        private void tbrForWin_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            string flag = e.Button.Tag.ToString();
            this.controlTBButton(flag);
        }

        public bool verifyModel(out string errorResult)
        {
            DataRow row;
            BPMVVerificationHandler handler = new BPMVVerificationHandler();
            ProcessDefinitionDS dataSet = this.viewPanel.shapeData.template.GetDataSet();
            int count = dataSet.Tables["PLM_BPM_ACTIVITY_DEFINITION"].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                row = dataSet.Tables["PLM_BPM_ACTIVITY_DEFINITION"].Rows[i];
                if (row.RowState != DataRowState.Deleted)
                {
                    Guid guid;
                    string str;
                    switch (decimal.ToInt32((decimal) row["PLM_ISAUTOMATIC"]))
                    {
                        case 2:
                        {
                            guid = new Guid((byte[]) row["PLM_OID"]);
                            str = (string) row["PLM_NAME"];
                            handler.AddStartNode(guid, str);
                            continue;
                        }
                        case 3:
                        {
                            guid = new Guid((byte[]) row["PLM_OID"]);
                            str = (string) row["PLM_NAME"];
                            handler.AddEndNode(guid, str);
                            continue;
                        }
                    }
                    guid = new Guid((byte[]) row["PLM_OID"]);
                    str = (string) row["PLM_NAME"];
                    handler.AddTaskNode(guid, str);
                }
            }
            int num4 = dataSet.Tables["PLM_BPM_ROUTER_DEFINITION"].Rows.Count;
            for (int j = 0; j < num4; j++)
            {
                row = dataSet.Tables["PLM_BPM_ROUTER_DEFINITION"].Rows[j];
                if (row.RowState != DataRowState.Deleted)
                {
                    Guid guid2 = new Guid((byte[]) row["PLM_OID"]);
                    string name = (string) row["PLM_NAME"];
                    int jointype = -1;
                    string joinexpression = null;
                    int splittype = -1;
                    string splitexpression = null;
                    if (row["PLM_JOINTYPE"] != DBNull.Value)
                    {
                        jointype = decimal.ToInt32((decimal) row["PLM_JOINTYPE"]);
                    }
                    if (row["PLM_JOINEXPRESSION"] != DBNull.Value)
                    {
                        joinexpression = row["PLM_JOINEXPRESSION"].ToString();
                    }
                    if (row["PLM_SPLITTYPE"] != DBNull.Value)
                    {
                        splittype = decimal.ToInt32((decimal) row["PLM_SPLITTYPE"]);
                    }
                    if (row["PLM_SPLITEXPRESSION"] != DBNull.Value)
                    {
                        splitexpression = row["PLM_SPLITEXPRESSION"].ToString();
                    }
                    handler.AddRouteNode(guid2, name, jointype, joinexpression, splittype, splitexpression);
                }
            }
            int num8 = dataSet.Tables["PLM_BPM_R_STEPDEF_STEPDEF"].Rows.Count;
            for (int k = 0; k < num8; k++)
            {
                row = dataSet.Tables["PLM_BPM_R_STEPDEF_STEPDEF"].Rows[k];
                if (row.RowState != DataRowState.Deleted)
                {
                    Guid empty = Guid.Empty;
                    Guid toguid = Guid.Empty;
                    string fromtype = "";
                    string totype = "";
                    int istrue = 0;
                    if (row["PLM_FROMSTEPDEFINITIONID"] != DBNull.Value)
                    {
                        empty = new Guid((byte[]) row["PLM_FROMSTEPDEFINITIONID"]);
                    }
                    if (row["PLM_TOSTEPDEFINITIONID"] != DBNull.Value)
                    {
                        toguid = new Guid((byte[]) row["PLM_TOSTEPDEFINITIONID"]);
                    }
                    if (row["PLM_FROMSTEPTYPE"] != DBNull.Value)
                    {
                        fromtype = (string) row["PLM_FROMSTEPTYPE"];
                    }
                    if (row["PLM_TOSTEPTYPE"] != DBNull.Value)
                    {
                        totype = (string) row["PLM_TOSTEPTYPE"];
                    }
                    if (row["PLM_ISTRUE"] != DBNull.Value)
                    {
                        istrue = decimal.ToInt32((decimal) row["PLM_ISTRUE"]);
                    }
                    handler.AddLine(empty, toguid, fromtype, totype, istrue);
                }
            }
            string str7 = "";
            handler.CreatePiExpression();
            int num11 = handler.Reduce();
            switch (num11)
            {
                case 0:
                    str7 = "没有发现错误。";
                    break;

                case 1:
                {
                    str7 = "可能隐含的错误：缺乏同步\n错误可能在这些节点上：";
                    ArrayList error = handler.GetError();
                    int num12 = error.Count;
                    for (int m = 0; m < num12; m++)
                    {
                        BPMVPath path = (BPMVPath) error[m];
                        if (path.Type == 4)
                        {
                            str7 = str7 + path.Name + " ";
                        }
                    }
                    break;
                }
                case 2:
                {
                    str7 = "可能隐含的错误：存在没有出路的环\n错误可能在这些节点上：";
                    ArrayList list2 = handler.GetError();
                    int num14 = list2.Count;
                    for (int n = 0; n < num14; n++)
                    {
                        BPMVPath path2 = (BPMVPath) list2[n];
                        if (path2.Type == 4)
                        {
                            str7 = str7 + path2.Name + " ";
                        }
                    }
                    break;
                }
                case 3:
                {
                    str7 = "可能隐含的错误：死锁\n错误可能在这些节点之间：";
                    ArrayList list3 = handler.GetError();
                    int num16 = list3.Count;
                    for (int num17 = 0; num17 < num16; num17++)
                    {
                        BPMVPath path3 = (BPMVPath) list3[num17];
                        if (path3.Type == 4)
                        {
                            str7 = str7 + path3.Name + " ";
                        }
                    }
                    break;
                }
            }
            errorResult = str7;
            return (num11 == 0);
        }

        private void vsbParent_Scroll(object sender, ScrollEventArgs e)
        {
            int num = this.vsbParent.Value - e.NewValue;
            int x = this.viewPanel.Location.X;
            int y = this.viewPanel.Location.Y;
            this.viewPanel.Location = new Point(x, y + num);
        }

        private void WFTEditor_Closing(object sender, CancelEventArgs e)
        {
            bool flag = false;
            if (this.isDel)
            {
                e.Cancel = false;
                return;
            }
            PLGrantPerm perm = new PLGrantPerm();
            if ((this.isNew && this.tiMain.GetAllowCreateProcessManagement()) || (this.viewPanel.modified && (perm.CanDoObjectOperation(ClientData.LogonUser.Oid, this.viewPanel.shapeData.template.ID, "BPM_PROCESS_DEFINITION", "ClaRel_MODIFY", 0, Guid.Empty) == 1)))
            {
                switch (MessageBox.Show(this, (string) rmTiModeler.GetObject("strSaveRemind"), (string) rmTiModeler.GetObject("strSave"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        flag = this.saveToDataBase();
                        goto Label_016B;

                    case DialogResult.No:
                        if (this.isNew)
                        {
                            new BPMAdmin().UnlockProcessDefinition(ClientData.LogonUser.Oid, this.viewPanel.shapeData.template.ID);
                            object keyByValue = ((FrmMain) base.MdiParent).HashMDiWindows.GetKeyByValue(this);
                            if (keyByValue != null)
                            {
                                this.tiMain.HashMDiWindows.Remove(keyByValue);
                                TreeNode node = (TreeNode) keyByValue;
                                node.Nodes.Clear();
                                TagForTiModeler.TreeNode_BPM.Nodes.Remove(node);
                                this.tiMain.RemoveOneProcess(node);
                            }
                            flag = true;
                        }
                        else
                        {
                            flag = true;
                        }
                        goto Label_016B;
                }
                e.Cancel = true;
                return;
            }
            flag = true;
        Label_016B:
            if (flag)
            {
                new BPMAdmin().UnlockProcessDefinition(ClientData.LogonUser.Oid, this.viewPanel.shapeData.template.ID);
                object key = ((FrmMain) base.MdiParent).HashMDiWindows.GetKeyByValue(this);
                if (key != null)
                {
                    ((FrmMain) base.MdiParent).HashMDiWindows.Remove(key);
                    TreeNode node2 = (TreeNode) key;
                    node2.Nodes.Clear();
                    return;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void WFTEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.deleteObject();
            }
            if (e.Control)
            {
                Keys keyCode = e.KeyCode;
            }
            if (e.Control)
            {
                Keys keys2 = e.KeyCode;
            }
            if ((e.Control && (e.KeyCode == Keys.Z)) && (this.stackUndo.Count > 0))
            {
                RUObject obj2 = (RUObject) this.stackUndo.Pop();
                obj2.Undo();
                this.stackRedo.Push(obj2);
                this.setRUToolbarStatus();
                this.viewPanel.Refresh();
            }
            if ((e.Control && (e.KeyCode == Keys.Y)) && (this.stackRedo.Count > 0))
            {
                RUObject obj3 = (RUObject) this.stackRedo.Pop();
                obj3.Redo();
                this.stackUndo.Push(obj3);
                this.setRUToolbarStatus();
                this.viewPanel.Refresh();
            }
            if (e.KeyCode == Keys.Down)
            {
                this.viewPanel.MoveShape(new Point(this.viewPanel.moveBase.X, this.viewPanel.moveBase.Y + 1));
            }
            if (e.KeyCode == Keys.Up)
            {
                this.viewPanel.MoveShape(new Point(this.viewPanel.moveBase.X, this.viewPanel.moveBase.Y - 1));
            }
            if (e.KeyCode == Keys.Right)
            {
                this.viewPanel.MoveShape(new Point(this.viewPanel.moveBase.X + 1, this.viewPanel.moveBase.Y));
            }
            if (e.KeyCode == Keys.Left)
            {
                this.viewPanel.MoveShape(new Point(this.viewPanel.moveBase.X - 1, this.viewPanel.moveBase.Y));
            }
        }

        private void WFTEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void WFTEditor_MouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}

