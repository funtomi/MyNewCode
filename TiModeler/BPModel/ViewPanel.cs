
    using System;
    using System.Collections;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Reflection;
    using System.Resources;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.Admin.BPM;
    using Thyt.TiPLM.CLT.Admin.BPM.Modeler;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.CLT.TiModeler.BPModel.ReUndo;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.Common.Admin.BPM;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.BPM;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.CLT.TiModeler.BPModel {
    public partial class ViewPanel : Panel {
        public double arrowAngle;
        public double arrowLong;
        public bool breakLine;
        public bool cursorFlag;
        private Rectangle disRectangle;
        public bool endNodeFlag;
        private Shape endShape;
        public Graphics g;
        private int initClientX;
        private int initClientY;
        private TextBox inputBox;
        public bool isCaptured;
        public WFTEditor mainWindow;
        public bool modified;
        public Point mousePosition;
        public Point moveBase;
        public Point moveBaseOriginal;
        public Rectangle mutiRec;
        public Pen myPen;
        public Point pointPositionOriginal;
        private ResourceManager rmTiModeler;
        public bool routeNodeFlag;
        private int scrollX;
        private int scrollY;
        private int scrPosX;
        private int scrPosY;
        public Shape selectedShape;
        public ArrayList selectedShapeList;
        public Model shapeData;
        public bool startNodeFlag;
        private Shape startShape;
        public bool taskNodeFlag;
        private Line tempLine;
        private Point tempLineEnd;
        private Point tempLineStart;
        public int turningPosition;
        public bool wantInput;
        public bool wantLine;
        public int wantMovePoint;
        public bool wantMoveShape;

        public ViewPanel() {
            this.InitializeComponent();
            this.init();
        }

        public ViewPanel(WFTEditor mainWindow) {
            this.mainWindow = mainWindow;
            this.InitializeComponent();
            this.init();
        }

        private void addEndNode(EndNode endNode) {
            this.shapeData.root.endNode = endNode;
            this.addTextNode(endNode.text);
        }

        public void addLine(Shape line) {
            this.shapeData.root.linAry.Add(line);
        }

        private void addPoint(int index, Point point) {
            if (this.tempLine != null) {
                this.tempLine.insetPoint(index, point);
            }
        }

        public void addRouteNode(Shape routeNode) {
            this.shapeData.root.routeNodAry.Add(routeNode);
            this.addTextNode(routeNode.text);
        }

        private void addStartNode(StartNode startNode) {
            this.shapeData.root.startNode = startNode;
            this.addTextNode(startNode.text);
        }

        public void addTaskNode(Shape taskNode) {
            this.shapeData.root.taskNodAry.Add(taskNode);
            if (taskNode.realNode is DELActivityDefinition) {
                DELActivityDefinition realNode = taskNode.realNode as DELActivityDefinition;
                realNode.Priority = this.shapeData.root.taskNodAry.Count;
            }
            this.addTextNode(taskNode.text);
        }

        private void addTextNode(NodeText textNode) {
            this.shapeData.root.textNodAry.Add(textNode);
        }

        private void buildStartAndEnd() {
            DELActivityDefinition definition = new DELActivityDefinition(Guid.NewGuid(), this.shapeData.template.ID);
            StartNode startNode = new StartNode(new Point(50, 50));
            definition.Name = startNode.text.caption;
            startNode.realNode = definition;
            this.addStartNode(startNode);
            DELActivityDefinition definition2 = new DELActivityDefinition(Guid.NewGuid(), this.shapeData.template.ID);
            EndNode endNode = new EndNode(new Point(500, 200));
            definition2.Name = endNode.text.caption;
            endNode.realNode = definition2;
            this.addEndNode(endNode);
        }

        public void ChangeParentTreeNodeText() {
            FrmMain mdiParent = (FrmMain)this.mainWindow.MdiParent;
            if (mdiParent.tvwNavigator.SelectedNode.Tag is Shape) {
                mdiParent.tvwNavigator.SelectedNode.Text = ((Shape)mdiParent.tvwNavigator.SelectedNode.Tag).text.caption;
            }
        }

        public void changeText() {
            if (this.wantInput) {
                if (!this.inputBox.ClientRectangle.Contains(this.mousePosition)) {
                    if (this.inputBox.Text.Equals("")) {
                        MessageBox.Show("名字不能为空");
                    } else {
                        ((NodeText)this.selectedShape).caption = this.inputBox.Text;
                        ((NodeText)this.selectedShape).objectShape.realNode.Name = ((NodeText)this.selectedShape).caption;
                        this.mainWindow.Focus();
                        this.inputBox.Visible = false;
                        this.wantInput = false;
                        this.Refresh();
                        this.ChangeParentTreeNodeText();
                    }
                }
            } else {
                this.mainWindow.Focus();
            }
        }

        public static bool CheckNodes(TreeNode parentNode, string objectType) {
            foreach (TreeNode node in parentNode.Nodes) {
                if (((DEMetaClass)node.Tag).Name == objectType) {
                    node.Checked = true;
                    return true;
                }
                if (CheckNodes(node, objectType)) {
                    return true;
                }
            }
            return false;
        }

        private Point[] checkPointInLine(Point point, ref int index, ref Line line) {
            return this.shapeData.root.checkPointInLine(point, ref index, ref line);
        }

        private void cmiNodeAuth_Click(object sender, EventArgs e) {
            DELActivityDefinition realNode = this.selectedShape.realNode as DELActivityDefinition;
            DELBPMEntityList list = this.mainWindow.proTemplate.GrantList[realNode.ID] as DELBPMEntityList;
            if (list == null) {
                list = new DELBPMEntityList();
                this.mainWindow.proTemplate.GrantList.Add(realNode.ID, list);
            }
            new FrmAuthDef(realNode.ID, "Activity", list, new ArrayList()).ShowDialog();
        }

        public int deleteAssociatedLine(Shape shape) {
            foreach (object obj2 in this.shapeData.FindAssociatedLine(shape)) {
                if (!this.mainWindow.arrayDeleteObject.Contains(obj2)) {
                    this.mainWindow.arrayDeleteObject.Add(obj2);
                }
            }
            return this.shapeData.deleteAssociatedLine(shape);
        }

        private bool deleteLinePoint(Line line, Point point) {
            return line.deletePoint(point);
        }
        public void deleteObject() {
            if (this.selectedShape == null) {
                if (this.selectedShapeList.Count > 0) {
                    bool flag = true;
                    for (int i = 0; i < this.selectedShapeList.Count; i++) {
                        if ((this.selectedShapeList[i].GetType().Name.ToString() == "StartNode") || (this.selectedShapeList[i].GetType().Name.ToString() == "EndNode")) {
                            flag = false;
                        }
                    }
                    if (flag) {
                        RUMutiShape shape2 = new RUMutiShape(this.mainWindow, this.selectedShapeList, "OP_Delete");
                        this.mainWindow.stackUndo.Push(shape2);
                        this.mainWindow.setRUToolbarStatus();
                        for (int j = 0; j < this.selectedShapeList.Count; j++) {
                            Shape shape3;
                            string str2 = ((Shape)this.selectedShapeList[j]).GetType().Name.ToString();
                            if (str2 != null) {
                                if (str2 != "Line") {
                                    if (str2 == "TaskNode") {
                                        goto Label_05AC;
                                    }
                                    if (str2 == "RouteNode") {
                                        goto Label_07C1;
                                    }
                                    if (str2 == "NodeText") {
                                    }
                                } else if (this.shapeData.root.linAry.Contains(this.selectedShapeList[j])) {
                                    this.shapeData.root.linAry.Remove(this.selectedShapeList[j]);
                                    this.mainWindow.arrayDeleteObject.Add(this.selectedShapeList[j]);
                                }
                            }
                            goto Label_0850;
                        Label_05AC:
                            shape3 = (Shape)this.selectedShapeList[j];
                            DELActivityDefinition realNode = (DELActivityDefinition)((TaskNode)shape3).realNode;
                            if (this.mainWindow.isProcessRuleFirstOpen) {
                                BPMAdmin admin2 = new BPMAdmin();
                                try {
                                    ArrayList theAllProcessRuleList = new ArrayList();
                                    admin2.GetAllRulesByProcessDefinitionID(BPMClient.UserID, realNode.ProcessDefinitionID, out theAllProcessRuleList);
                                    foreach (DELProcessRule rule3 in theAllProcessRuleList) {
                                        if ((rule3.leftActivityID == realNode.ID) || (rule3.rightActivityID == realNode.ID)) {
                                            MessageBox.Show("您要删除的活动 (" + realNode.Name + ") 定义了人员分派规则,请先删除这些规则！", "TiDesk", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                            return;
                                        }
                                    }
                                } catch (Exception exception2) {
                                    MessageBox.Show(exception2.Message);
                                }
                            } else {
                                foreach (DELProcessRule rule4 in this.mainWindow.TheAllProcessRuleList) {
                                    if ((rule4.leftActivityID == realNode.ID) || (rule4.rightActivityID == realNode.ID)) {
                                        MessageBox.Show("您要删除的活动 (" + realNode.Name + ") 定义了人员分派规则,请先删除这些规则！", "TiDesk", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                        return;
                                    }
                                }
                            }
                            this.deleteAssociatedLine(shape3);
                            this.shapeData.root.textNodAry.Remove(shape3.text);
                            this.shapeData.root.taskNodAry.Remove(shape3);
                            this.mainWindow.arrayDeleteObject.Add(shape3);
                            this.selectedShape = shape3;
                            this.setMainFrmTreeSelected();
                            ((FrmMain)this.mainWindow.MdiParent).DelTreeNodeByShape(shape3);
                            this.selectedShape = null;
                            goto Label_0850;
                        Label_07C1:
                            shape3 = (Shape)this.selectedShapeList[j];
                            this.deleteAssociatedLine(shape3);
                            this.shapeData.root.textNodAry.Remove(shape3.text);
                            this.shapeData.root.routeNodAry.Remove(shape3);
                            this.mainWindow.arrayDeleteObject.Add(shape3);
                            this.selectedShape = shape3;
                            this.setMainFrmTreeSelected();
                            ((FrmMain)this.mainWindow.MdiParent).DelTreeNodeByShape(shape3);
                            this.selectedShape = null;
                        Label_0850:
                            ((Shape)this.selectedShapeList[j]).isSelected = false;
                        }
                        this.selectedShapeList.Clear();
                        this.Refresh();
                    } else {
                        MessageBox.Show(this.rmTiModeler.GetString("strCanNotDeleteNode"));
                    }
                }
            } else {
                string str = this.selectedShape.GetType().Name.ToString();
                if (str != null) {
                    if (str != "Line") {
                        if (str != "StartNode") {
                            Shape selectedShape;
                            if (str == "EndNode") {
                                MessageBox.Show(this.rmTiModeler.GetString("strCanNotDeleteNode"));
                                return;
                            }
                            if (str == "TaskNode") {
                                selectedShape = this.selectedShape;
                                this.selectedShape = null;
                                DELActivityDefinition definition = (DELActivityDefinition)((TaskNode)selectedShape).realNode;
                                if (this.mainWindow.isProcessRuleFirstOpen) {
                                    BPMAdmin admin = new BPMAdmin();
                                    try {
                                        ArrayList list = new ArrayList();
                                        admin.GetAllRulesByProcessDefinitionID(BPMClient.UserID, definition.ProcessDefinitionID, out list);
                                        foreach (DELProcessRule rule in list) {
                                            if ((rule.leftActivityID == definition.ID) || (rule.rightActivityID == definition.ID)) {
                                                MessageBox.Show("您要删除的活动 (" + definition.Name + ") 定义了人员分派规则,请先删除这些规则！", "TiDesk", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                                selectedShape.isSelected = false;
                                                return;
                                            }
                                        }
                                    } catch (Exception exception) {
                                        MessageBox.Show(exception.Message);
                                    }
                                } else {
                                    foreach (DELProcessRule rule2 in this.mainWindow.TheAllProcessRuleList) {
                                        if ((rule2.leftActivityID == definition.ID) || (rule2.rightActivityID == definition.ID)) {
                                            MessageBox.Show("您要删除的活动 (" + definition.Name + ") 定义了人员分派规则,请先删除这些规则！", "TiDesk", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                            selectedShape.isSelected = false;
                                            return;
                                        }
                                    }
                                }
                                RUTaskNode node = new RUTaskNode(this.mainWindow, (TaskNode)selectedShape, "OP_Delete");
                                this.mainWindow.stackUndo.Push(node);
                                this.mainWindow.setRUToolbarStatus();
                                this.deleteAssociatedLine(selectedShape);
                                this.shapeData.root.textNodAry.Remove(selectedShape.text);
                                this.shapeData.root.taskNodAry.Remove(selectedShape);
                                this.mainWindow.arrayDeleteObject.Add(selectedShape);
                                ((FrmMain)this.mainWindow.MdiParent).DelTreeNodeByShape(selectedShape);
                                this.Refresh();
                                return;
                            }
                            if (str == "RouteNode") {
                                selectedShape = this.selectedShape;
                                this.selectedShape = null;
                                RURouteNode node2 = new RURouteNode(this.mainWindow, (RouteNode)selectedShape, "OP_Delete");
                                this.mainWindow.stackUndo.Push(node2);
                                this.mainWindow.setRUToolbarStatus();
                                this.deleteAssociatedLine(selectedShape);
                                this.shapeData.root.textNodAry.Remove(selectedShape.text);
                                this.shapeData.root.routeNodAry.Remove(selectedShape);
                                this.mainWindow.arrayDeleteObject.Add(selectedShape);
                                ((FrmMain)this.mainWindow.MdiParent).DelTreeNodeByShape(selectedShape);
                                this.Refresh();
                                return;
                            }
                            if (str != "NodeText") {
                                return;
                            }
                            return;
                        }
                    } else {
                        RULine line = new RULine(this.mainWindow, (Line)this.selectedShape, "OP_Delete");
                        this.mainWindow.stackUndo.Push(line);
                        this.mainWindow.setRUToolbarStatus();
                        this.shapeData.root.linAry.Remove(this.selectedShape);
                        this.mainWindow.arrayDeleteObject.Add(this.selectedShape);
                        this.selectedShape = null;
                        this.Refresh();
                        return;
                    }
                    MessageBox.Show(this.rmTiModeler.GetString("strCanNotDeleteNode"));
                }
            }
        }

        private void drawArrow(Graphics g, Pen myPen, Point p1, Point p2) {
            int num;
            int num2;
            int num3;
            int num4;
            if (p1.X == p2.X) {
                if (p1.Y != p2.Y) {
                    if (p1.Y < p2.Y) {
                        num = p2.X - ((int)(this.arrowLong * Math.Sin(this.arrowAngle)));
                        num2 = p2.X + ((int)(this.arrowLong * Math.Sin(this.arrowAngle)));
                        num3 = num4 = p2.Y - ((int)(this.arrowLong * Math.Cos(this.arrowAngle)));
                        g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                        g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                    } else {
                        num = p2.X - ((int)(this.arrowLong * Math.Sin(this.arrowAngle)));
                        num2 = p2.X + ((int)(this.arrowLong * Math.Sin(this.arrowAngle)));
                        num3 = num4 = p2.Y + ((int)(this.arrowLong * Math.Cos(this.arrowAngle)));
                        g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                        g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                    }
                }
            } else {
                double num5;
                double num6;
                double num7;
                if (p1.X < p2.X) {
                    if (p1.Y == p2.Y) {
                        num = num2 = p2.X - ((int)(this.arrowLong * Math.Sin(this.arrowAngle)));
                        num3 = p2.Y + ((int)(this.arrowLong * Math.Cos(this.arrowAngle)));
                        num4 = p2.Y + ((int)(this.arrowLong * Math.Cos(this.arrowAngle)));
                        g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                        g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                    } else if (p1.Y < p2.Y) {
                        num5 = ((double)(p2.Y - p1.Y)) / ((double)(p2.X - p1.X));
                        num6 = Math.Atan(num5);
                        num = p2.X - ((int)(this.arrowLong * Math.Cos(num6 - this.arrowAngle)));
                        num3 = p2.Y - ((int)(this.arrowLong * Math.Sin(num6 - this.arrowAngle)));
                        num7 = num6 + this.arrowAngle;
                        num2 = p2.X - ((int)(this.arrowLong * Math.Cos(num7)));
                        num4 = p2.Y - ((int)(this.arrowLong * Math.Sin(num7)));
                        g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                        g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                    } else {
                        num5 = ((double)(p1.Y - p2.Y)) / ((double)(p2.X - p1.X));
                        num6 = Math.Atan(num5);
                        num = p2.X - ((int)(this.arrowLong * Math.Cos(num6 - this.arrowAngle)));
                        num3 = p2.Y + ((int)(this.arrowLong * Math.Sin(num6 - this.arrowAngle)));
                        num7 = num6 + this.arrowAngle;
                        num2 = p2.X - ((int)(this.arrowLong * Math.Cos(num7)));
                        num4 = p2.Y + ((int)(this.arrowLong * Math.Sin(num7)));
                        g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                        g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                    }
                } else if (p1.Y == p2.Y) {
                    num = num2 = p2.X + ((int)(this.arrowLong * Math.Sin(this.arrowAngle)));
                    num3 = p2.Y + ((int)(this.arrowLong * Math.Cos(this.arrowAngle)));
                    num4 = p2.Y + ((int)(this.arrowLong * Math.Cos(this.arrowAngle)));
                    g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                    g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                } else if (p1.Y < p2.Y) {
                    num5 = ((double)(p2.Y - p1.Y)) / ((double)(p1.X - p2.X));
                    num6 = Math.Atan(num5);
                    num = p2.X + ((int)(this.arrowLong * Math.Cos(num6 - this.arrowAngle)));
                    num3 = p2.Y - ((int)(this.arrowLong * Math.Sin(num6 - this.arrowAngle)));
                    num7 = num6 + this.arrowAngle;
                    num2 = p2.X + ((int)(this.arrowLong * Math.Cos(num7)));
                    num4 = p2.Y - ((int)(this.arrowLong * Math.Sin(num7)));
                    g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                    g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                } else {
                    num5 = ((double)(p1.Y - p2.Y)) / ((double)(p1.X - p2.X));
                    num6 = Math.Atan(num5);
                    num = p2.X + ((int)(this.arrowLong * Math.Cos(num6 - this.arrowAngle)));
                    num3 = p2.Y + ((int)(this.arrowLong * Math.Sin(num6 - this.arrowAngle)));
                    num7 = num6 + this.arrowAngle;
                    num2 = p2.X + ((int)(this.arrowLong * Math.Cos(num7)));
                    num4 = p2.Y + ((int)(this.arrowLong * Math.Sin(num7)));
                    g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                    g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                }
            }
        }

        public static void FindCheckedNode(TreeNode parentNode, ArrayList nodeList) {
            foreach (TreeNode node in parentNode.Nodes) {
                if (node.Checked) {
                    nodeList.Add(node);
                }
                FindCheckedNode(node, nodeList);
            }
        }

        public DELOperation FindOperation(Guid OperationID) {
            for (int i = 0; i < Model.BPMOperationList.Count; i++) {
                if (((DELOperation)Model.BPMOperationList[i]).ID.Equals(OperationID)) {
                    return (DELOperation)Model.BPMOperationList[i];
                }
            }
            return null;
        }

        public TreeNode FindTreeNode(TreeNode rootNode, Shape shape) {
            for (int i = 0; i < rootNode.Nodes.Count; i++) {
                if (shape == rootNode.Nodes[i].Tag) {
                    return rootNode.Nodes[i];
                }
            }
            return null;
        }

        private int getRouteNodeNumber() {
            return (this.shapeData.root.routeNodAry.Count + 1);
        }
        private int getTaskNodeNumber() {
            return (this.shapeData.root.taskNodAry.Count + 1);
        }
        protected void init() {
            this.rmTiModeler = new ResourceManager("Thyt.TiPLM.CLT.TiModeler.TiModelerStrings", Assembly.GetExecutingAssembly());
            this.modified = false;
            this.selectedShape = null;
            this.arrowLong = 10.0;
            this.arrowAngle = 0.26179938779914941;
            this.isCaptured = false;
            this.wantLine = false;
            this.breakLine = false;
            this.wantMoveShape = false;
            this.wantMovePoint = -1;
            this.wantInput = false;
            this.inputBox = new TextBox();
            this.turningPosition = -2;
            this.mousePosition = new Point(0, 0);
            this.tempLine = null;
            this.cursorFlag = true;
            this.startNodeFlag = false;
            this.endNodeFlag = false;
            this.taskNodeFlag = false;
            this.routeNodeFlag = false;
            this.myPen = new Pen(Color.Black);
            this.mutiRec = new Rectangle();
            this.selectedShapeList = new ArrayList();
            this.BackColor = SystemColors.Window;
            base.MouseUp += new MouseEventHandler(this.ViewPanel_MouseUp);
            base.MouseDown += new MouseEventHandler(this.ViewPanel_MouseDown);
            base.MouseMove += new MouseEventHandler(this.ViewPanel_MouseMove);
            base.Click += new EventHandler(this.ViewPanel_Click);
            base.DoubleClick += new EventHandler(this.ViewPanel_DoubleClick);
            this.inputBox.KeyDown += new KeyEventHandler(this.inputBox_KeyDown);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this.shapeData = new Model();
            this.buildStartAndEnd();
        }

        public void InitFrmEndDlg(FrmEndNode endDlg, EndNode endNode) {
            DELActivityDefinition realNode = (DELActivityDefinition)endNode.realNode;
            endDlg.txtDescription.Text = realNode.Description;
        }

        public void InitFrmProcessDlg(FrmProcessEdit processDlg, DELProcessDefinition template) {
            processDlg.proTemplate = template;
            processDlg.picture = this.shapeData.root;
            ArrayList namefilter = new ArrayList { 
                "OBJECTSTATE",
                "RESOURCE",
                "PPCLASSIFY",
                "COLLECTION",
                "FORM"
            };
            UIDataModel.FillCustomizedClassTree(processDlg.tvwObjTypes, 0, 0, namefilter);
            foreach (DELRProcessDef2Obj obj2 in processDlg.proTemplate.ProcessDefToObjList) {
                foreach (TreeNode node in processDlg.tvwObjTypes.Nodes) {
                    if (((DEMetaClass)node.Tag).Name == obj2.ObjectType) {
                        node.Checked = true;
                        break;
                    }
                    if (CheckNodes(node, obj2.ObjectType)) {
                        break;
                    }
                }
            }
            bool flag = false;
            for (int i = 0; i < processDlg.operationList.Count; i++) {
                if (processDlg.proTemplate.ProcessDefToObjList.Count > 0) {
                    OprType type = (OprType)processDlg.operationList[i];
                    if (type.Key == ((DELRProcessDef2Obj)processDlg.proTemplate.ProcessDefToObjList[0]).OperationType) {
                        ArrayList dataSource = processDlg.cobOperations.Properties.DataSource as ArrayList;
                        if ((dataSource != null) && (dataSource.Count > i)) {
                            processDlg.cobOperations.SelectedIndex = i;
                        }
                        flag = true;
                        break;
                    }
                }
                flag = false;
            }
            if (!flag) {
                processDlg.chbOperation.Checked = false;
                processDlg.cobOperations.DropDownStyle = ComboBoxStyle.DropDown;
                processDlg.cobOperations.Text = "";
                processDlg.cobOperations.Enabled = false;
            } else {
                processDlg.chbOperation.Checked = true;
                processDlg.cobOperations.DropDownStyle = ComboBoxStyle.DropDownList;
                processDlg.cobOperations.Enabled = true;
            }
            processDlg.rtbNote.Text = template.Description;
            processDlg.chkModifyMonitor.Checked = template.AllowModifyMonitor;
            processDlg.chbAllowNoActorsAtInstantiation.Checked = template.AllowNoActorsAtInstantiation;
            processDlg.ChkUseOrgFilter.Checked = template.UseOrgFilter;
            processDlg.chkAutoAdd.Checked = template.AddInitiatorAsMonitor;
            processDlg.txtProcessCreator.Text = template.CreatorName;
            processDlg.txtProcessCreateDate.Text = template.CreationDate.ToString();
            processDlg.txtProcessName.Text = template.Name;
            if (template.Priority == 2) {
                processDlg.cobPriority.SelectedIndex = 2;
            } else if (template.Priority == 1) {
                processDlg.cobPriority.SelectedIndex = 1;
            } else {
                processDlg.cobPriority.SelectedIndex = 0;
            }
            switch (template.OverTimeHandleRule) {
                case "不做处理":
                case "无处理":
                    processDlg.cobOverTimeHandler.SelectedIndex = 0;
                    break;

                case "发出警报":
                case "发出警告":
                case "Warning":
                    processDlg.cobOverTimeHandler.SelectedIndex = 1;
                    break;

                case "暂停过程":
                case "SuspendProcess":
                    processDlg.cobOverTimeHandler.SelectedIndex = 2;
                    break;

                case "终止过程":
                case "AbortProcess":
                    processDlg.cobOverTimeHandler.SelectedIndex = 3;
                    break;

                default:
                    processDlg.cobOverTimeHandler.SelectedIndex = 0;
                    break;
            }
            if ((processDlg.cobOverTimeHandler.SelectedIndex == 0) && processDlg.cobOverTimeHandler.Visible) {
                processDlg.nudDuration.Enabled = false;
                processDlg.cobDurationUnit.Enabled = false;
            } else {
                processDlg.nudDuration.Enabled = true;
                processDlg.cobDurationUnit.Enabled = true;
            }
            processDlg.lstActorUserList.Clear();
            processDlg.lvwActorUser.Items.Clear();
            for (int j = 0; j < template.Actor.Users.Count; j++) {
                processDlg.lstActorUserList.Add(template.Actor.Users[j]);
                ListViewItem item = new ListViewItem(((DELRParticipantDefUser)processDlg.lstActorUserList[j]).NAME, 0);
                if (((DELRParticipantDefUser)processDlg.lstActorUserList[j]).UserType == 0) {
                    item.SubItems.Add("人员");
                } else if (((DELRParticipantDefUser)processDlg.lstActorUserList[j]).UserType == 3) {
                    item.SubItems.Add("角色");
                } else {
                    item.SubItems.Add("组织");
                }
                processDlg.lvwActorUser.Items.Add(item);
            }
            processDlg.lstMonitorUserList.Clear();
            processDlg.lvwMonitorUser.Items.Clear();
            for (int k = 0; k < template.Monitor.Users.Count; k++) {
                processDlg.lstMonitorUserList.Add(template.Monitor.Users[k]);
                ListViewItem item2 = new ListViewItem(((DELRParticipantDefUser)processDlg.lstMonitorUserList[k]).NAME, 0);
                if (((DELRParticipantDefUser)processDlg.lstMonitorUserList[k]).UserType == 0) {
                    item2.SubItems.Add("人员");
                } else if (((DELRParticipantDefUser)processDlg.lstMonitorUserList[k]).UserType == 3) {
                    item2.SubItems.Add("角色");
                } else {
                    item2.SubItems.Add("组织");
                }
                processDlg.lvwMonitorUser.Items.Add(item2);
            }
            processDlg.lstOrgList.Clear();
            processDlg.lvwOrg.Items.Clear();
            for (int m = 0; m < template.Organization.Users.Count; m++) {
                processDlg.lstOrgList.Add(template.Organization.Users[m]);
                ListViewItem item3 = new ListViewItem(((DELRParticipantDefUser)processDlg.lstOrgList[m]).NAME, 0) {
                    SubItems = { "组织" }
                };
                processDlg.lvwOrg.Items.Add(item3);
            }
            processDlg.nudDuration.Value = template.Duration;
            if (template.DurationUnit.Equals(this.rmTiModeler.GetString("strHour"))) {
                processDlg.cobDurationUnit.SelectedIndex = 0;
            } else if (template.DurationUnit.Equals(this.rmTiModeler.GetString("strDay"))) {
                processDlg.cobDurationUnit.SelectedIndex = 1;
            }
            processDlg.cobBusinessType.SelectedIndex = template.Classification;
            processDlg.proVariableList.Clear();
            for (int n = 0; n < template.Variables.Count; n++) {
                processDlg.proVariableList.Add(template.Variables[n]);
                string str = "";
                if (((DELProcessDataDefinition)template.Variables[n]).SubID == 0) {
                    str = "(Array)";
                }
                if (((DELProcessDataDefinition)template.Variables[n]).SubID <= 0) {
                    ListViewItem item4 = new ListViewItem(((DELProcessDataDefinition)template.Variables[n]).Name) {
                        Tag = template.Variables[n]
                    };
                    switch (((DELProcessDataDefinition)template.Variables[n]).DataType) {
                        case 0:
                            item4.SubItems.Add(FrmBOBrowser.Translation(ProcessDataType.STRING.ToString() + str));
                            break;

                        case 1:
                            item4.SubItems.Add(FrmBOBrowser.Translation(ProcessDataType.INTEGER.ToString() + str));
                            break;

                        case 2:
                            item4.SubItems.Add(FrmBOBrowser.Translation(ProcessDataType.REAL.ToString() + str));
                            break;

                        case 3:
                            item4.SubItems.Add(FrmBOBrowser.Translation(ProcessDataType.BOOL.ToString().ToUpper()));
                            break;

                        case 4:
                            item4.SubItems.Add(FrmBOBrowser.Translation(ProcessDataType.DATETIME.ToString() + str));
                            break;

                        case 5:
                            item4.SubItems.Add(FrmBOBrowser.Translation(ProcessDataType.USER.ToString() + str));
                            break;
                    }
                    item4.SubItems.Add(FrmBOBrowser.Translation(((DELProcessDataDefinition)template.Variables[n]).Value.ToUpper()));
                    processDlg.lvwProVariable.Items.Add(item4);
                }
            }
            processDlg.groupList.Clear();
            for (int num6 = 0; num6 < template.TargetBusinessObjects.Count; num6++) {
                processDlg.groupList.Add(template.TargetBusinessObjects[num6]);
                DELProcessDataDefinition definition = (DELProcessDataDefinition)template.TargetBusinessObjects[num6];
                ListViewItem item5 = new ListViewItem(definition.Name) {
                    Tag = definition
                };
                string text = "";
                if (definition.DataType == 9) {
                    text = "业务对象组";
                } else if (definition.DataType == 8) {
                    text = "参考对象组";
                } else {
                    text = "过程变量组";
                }
                item5.SubItems.Add(text);
                item5.SubItems.Add(definition.Description);
                processDlg.lvwGroup.Items.Add(item5);
            }
            processDlg.lvwActOrders.BeginUpdate();
            processDlg.lvwActOrders.Items.Clear();
            ArrayList list3 = new ArrayList();
            foreach (Shape shape in this.shapeData.root.taskNodAry) {
                if ((shape != this.shapeData.root.startNode) && (shape != this.shapeData.root.endNode)) {
                    DELActivityDefinition realNode = shape.realNode as DELActivityDefinition;
                    list3.Add(realNode);
                }
            }
            list3.Sort();
            for (int num7 = 0; num7 < list3.Count; num7++) {
                DELActivityDefinition definition3 = list3[num7] as DELActivityDefinition;
                definition3.Priority = num7 + 1;
                //2018/8/16 by kexp ListViewItem没有int类型的构造函数，所以转为string
                ListViewItem item6 = new ListViewItem(definition3.Priority.ToString()) {
                    SubItems = { definition3.Name },
                    Tag = definition3
                };
                processDlg.lvwActOrders.Items.Add(item6);
            }
            processDlg.lvwActOrders.EndUpdate();
            processDlg.txtOrder.Minimum = 0M;
            processDlg.txtOrder.Maximum = list3.Count + 1;
        }

        public void InitFrmRouteDlg(FrmRouteDlg routeDlg, RouteNode routeNode) {
            routeDlg.routeNode = routeNode;
            routeDlg.picture = this.shapeData.root;
            routeDlg.proTemplate = this.shapeData.template;
            DELRouterDefinition realNode = (DELRouterDefinition)routeNode.realNode;
            routeDlg.txtBoxNodeName.Text = realNode.Name;
            switch (realNode.JoinType) {
                case 0:
                    routeDlg.comBoxPred.SelectedIndex = 1;
                    routeDlg.txtBoxPredecessor.Text = "任意一个节点完成";
                    break;

                case 1:
                    routeDlg.comBoxPred.SelectedIndex = 0;
                    routeDlg.txtBoxPredecessor.Text = "所有节点完成";
                    break;

                case 2:
                    routeDlg.comBoxPred.SelectedIndex = 2;
                    routeDlg.txtBoxPredecessor.Text = realNode.JoinExpression;
                    break;
            }
            switch (realNode.SplitType) {
                case 3:
                    routeDlg.comBoxSucc.SelectedIndex = 1;
                    routeDlg.txtBoxSuccessor.Text = "所有后继节点都被触发";
                    break;

                case 4:
                    routeDlg.comBoxSucc.SelectedIndex = 0;
                    routeDlg.txtBoxSuccessor.Text = realNode.SplitExpression;
                    break;
            }
            for (int i = 0; i < this.shapeData.root.linAry.Count; i++) {
                DELRStepDef2StepDef realLine = ((Line)this.shapeData.root.linAry[i]).realLine;
                if (((Line)this.shapeData.root.linAry[i]).startShape == this.selectedShape) {
                    if (realLine.IsTrue == 1) {
                        routeDlg.lstSplitorTrueList.Add(this.shapeData.root.linAry[i]);
                        routeDlg.lstSplitorTrue.Items.Add(((Line)this.shapeData.root.linAry[i]).endShape.realNode.Name);
                    } else {
                        routeDlg.lstSplitorFalseList.Add((Line)this.shapeData.root.linAry[i]);
                        routeDlg.lstSplitorFalse.Items.Add(((Line)this.shapeData.root.linAry[i]).endShape.realNode.Name);
                    }
                }
            }
            if (!routeDlg.nodeIsEnd(this.shapeData.root.endNode, routeNode)) {
                routeDlg.lstAllNodeSList.Add(this.shapeData.root.endNode);
                routeDlg.lstAllNodeS.Items.Add(this.shapeData.root.endNode.realNode.Name);
            }
            for (int j = 0; j < this.shapeData.root.routeNodAry.Count; j++) {
                if (((routeNode != this.shapeData.root.routeNodAry[j]) && !routeDlg.nodeIsEnd((Shape)this.shapeData.root.routeNodAry[j], routeNode)) && !routeDlg.nodeIsStart((Shape)this.shapeData.root.routeNodAry[j], routeNode)) {
                    routeDlg.lstAllNodeSList.Add(this.shapeData.root.routeNodAry[j]);
                    routeDlg.lstAllNodeS.Items.Add(((RouteNode)this.shapeData.root.routeNodAry[j]).realNode.Name);
                }
            }
            for (int k = 0; k < this.shapeData.root.taskNodAry.Count; k++) {
                if (((routeNode != this.shapeData.root.taskNodAry[k]) && !routeDlg.nodeIsEnd((Shape)this.shapeData.root.taskNodAry[k], routeNode)) && !routeDlg.nodeIsStart((Shape)this.shapeData.root.taskNodAry[k], routeNode)) {
                    routeDlg.lstAllNodeSList.Add(this.shapeData.root.taskNodAry[k]);
                    routeDlg.lstAllNodeS.Items.Add(((TaskNode)this.shapeData.root.taskNodAry[k]).realNode.Name);
                }
            }
        }

        public void InitFrmStartDlg(FrmStartNode startDlg, StartNode startNode) {
            startDlg.picture = this.shapeData.root;
            startDlg.startNode = startNode;
            DELActivityDefinition realNode = (DELActivityDefinition)startNode.realNode;
            if (this.shapeData.template.InitiationType == 0) {
                startDlg.rbtManual.Checked = true;
            } else if (this.shapeData.template.InitiationType == 1) {
                startDlg.rbtInvoke.Checked = true;
            } else {
                startDlg.rbtEvent.Checked = true;
                startDlg.txtEvent.Text = this.shapeData.template.InitiationEvent;
                startDlg.txtEventKey.Text = this.shapeData.template.EventKey;
            }
            startDlg.txtDescription.Text = realNode.Description;
            for (int i = 0; i < this.shapeData.root.linAry.Count; i++) {
                DELRStepDef2StepDef realLine = ((Line)this.shapeData.root.linAry[i]).realLine;
                if (((Line)this.shapeData.root.linAry[i]).startShape == startNode) {
                    startDlg.lstSplitorNodeList.Add(this.shapeData.root.linAry[i]);
                    startDlg.lstSplitorNode.Items.Add(((Line)this.shapeData.root.linAry[i]).endShape.realNode.Name);
                }
            }
            if (!startDlg.nodeIsEnd(this.shapeData.root.endNode, startNode)) {
                startDlg.lstAllNodeSList.Add(this.shapeData.root.endNode);
                startDlg.lstAllNodeS.Items.Add(this.shapeData.root.endNode.realNode.Name);
            }
            for (int j = 0; j < this.shapeData.root.routeNodAry.Count; j++) {
                if (((startNode != this.shapeData.root.routeNodAry[j]) && !startDlg.nodeIsEnd((Shape)this.shapeData.root.routeNodAry[j], startNode)) && !startDlg.nodeIsStart((Shape)this.shapeData.root.routeNodAry[j], startNode)) {
                    startDlg.lstAllNodeSList.Add(this.shapeData.root.routeNodAry[j]);
                    startDlg.lstAllNodeS.Items.Add(((RouteNode)this.shapeData.root.routeNodAry[j]).realNode.Name);
                }
            }
            for (int k = 0; k < this.shapeData.root.taskNodAry.Count; k++) {
                if (((startNode != this.shapeData.root.taskNodAry[k]) && !startDlg.nodeIsEnd((Shape)this.shapeData.root.taskNodAry[k], startNode)) && !startDlg.nodeIsStart((Shape)this.shapeData.root.taskNodAry[k], startNode)) {
                    startDlg.lstAllNodeSList.Add(this.shapeData.root.taskNodAry[k]);
                    startDlg.lstAllNodeS.Items.Add(((Shape)this.shapeData.root.taskNodAry[k]).realNode.Name);
                }
            }
        }

        public void InitFrmTaskPropertyDlg(FrmTaskPropertyDlg taskDlg, TaskNode taskNode) {
            taskDlg.proTemplate = this.shapeData.template;
            taskDlg.picture = this.shapeData.root;
            taskDlg.taskNode = taskNode;
            taskDlg.realTaskNode = (DELActivityDefinition)taskNode.realNode;
            taskDlg.lstActorUserList.Clear();
            taskDlg.lstActorUser.Items.Clear();
            for (int i = 0; i < taskDlg.realTaskNode.Actor.Users.Count; i++) {
                DELRParticipantDefUser user = (DELRParticipantDefUser)taskDlg.realTaskNode.Actor.Users[i];
                taskDlg.lstActorUserList.Add(user);
                taskDlg.lstActorUser.Items.Add(FrmTaskPropertyDlg.ConstructItemFromUser(user));
            }
            taskDlg.lstMonitorUserList.Clear();
            taskDlg.lstMonitorUser.Items.Clear();
            for (int j = 0; j < taskDlg.realTaskNode.Monitor.Users.Count; j++) {
                DELRParticipantDefUser user2 = (DELRParticipantDefUser)taskDlg.realTaskNode.Monitor.Users[j];
                taskDlg.lstMonitorUserList.Add(user2);
                taskDlg.lstMonitorUser.Items.Add(FrmTaskPropertyDlg.ConstructItemFromUser(user2));
            }
            taskDlg.chkNotify.Checked = taskDlg.realTaskNode.IsUseNotify;
            taskDlg.chkFile.Checked = taskDlg.realTaskNode.IsMailSendFile;
            if (taskDlg.chkNotify.Checked) {
                taskDlg.txtTitle.Text = taskDlg.realTaskNode.NotifyTitle;
                taskDlg.rtContent.Text = taskDlg.realTaskNode.NotifyContent;
                taskDlg.lstNotifyUserList.Clear();
                taskDlg.lstNotifyUser.Items.Clear();
                for (int num3 = 0; num3 < taskDlg.realTaskNode.Notifier.Users.Count; num3++) {
                    DELRParticipantDefUser user3 = (DELRParticipantDefUser)taskDlg.realTaskNode.Notifier.Users[num3];
                    taskDlg.lstNotifyUserList.Add(user3);
                    taskDlg.lstNotifyUser.Items.Add(FrmTaskPropertyDlg.ConstructItemFromUser(user3));
                }
            }
            taskDlg.txtTaskName.Text = ((DELActivityDefinition)taskNode.realNode).Name;
            taskDlg.txtDescription.Text = ((DELActivityDefinition)taskNode.realNode).Description;
            taskDlg.nudDuration.Value = ((DELActivityDefinition)taskNode.realNode).Duration;
            DELActivityDefinition realNode = (DELActivityDefinition)taskNode.realNode;
            if (realNode.SubFlowID != Guid.Empty) {
                BPMAdmin admin = new BPMAdmin();
                string name = "暂时无子流程";
                admin.GetProcessDefinitionNameByID(BPMClient.UserID, realNode.SubFlowID, out name);
                taskDlg.txtFlowName.Text = name;
                taskDlg.cbSubflow.Checked = true;
                taskDlg.btnSetSubFlow.Enabled = true;
            }
            if (((DELActivityDefinition)taskNode.realNode).CanChangeActor) {
                taskDlg.chbChangeActor.Checked = true;
            } else {
                taskDlg.chbChangeActor.Checked = false;
            }
            if (((DELActivityDefinition)taskNode.realNode).DurationUnit == this.rmTiModeler.GetString("strHour")) {
                taskDlg.cobTimeUnit.SelectedIndex = 0;
            } else if (((DELActivityDefinition)taskNode.realNode).DurationUnit == this.rmTiModeler.GetString("strDay")) {
                taskDlg.cobTimeUnit.SelectedIndex = 1;
            }
            if (((DELActivityDefinition)taskNode.realNode).GetOption(0).Equals("Y")) {
                taskDlg.chbNoObjThenThrough.Checked = true;
            } else {
                taskDlg.chbNoObjThenThrough.Checked = false;
            }
            if (((DELActivityDefinition)taskNode.realNode).GetOption(6).Equals("Y")) {
                taskDlg.chbDirectThrough.Checked = true;
            } else {
                taskDlg.chbDirectThrough.Checked = false;
            }
            if (((DELActivityDefinition)taskNode.realNode).GetOption(2).Equals("Y")) {
                taskDlg.chbWorkItemDevolve.Checked = true;
            } else {
                taskDlg.chbWorkItemDevolve.Checked = false;
            }
            if (((DELActivityDefinition)taskNode.realNode).GetOption(1).Equals("Y")) {
                taskDlg.chbRestrictActor.Checked = true;
            } else {
                taskDlg.chbRestrictActor.Checked = false;
            }
            if (((DELActivityDefinition)taskNode.realNode).GetOption(4).Equals("Y")) {
                taskDlg.chkActorPre.Checked = true;
            } else {
                taskDlg.chkActorPre.Checked = false;
            }
            taskDlg.chbAutoSendMessage.Checked = ((DELActivityDefinition)taskNode.realNode).AutoSendMessage;
            switch (((DELActivityDefinition)taskNode.realNode).OverTimeHandleRule) {
                case "不做处理":
                case "无处理":
                    taskDlg.cobOverTime.SelectedIndex = 0;
                    break;

                case "自动完成":
                    taskDlg.cobOverTime.SelectedIndex = 1;
                    break;

                default:
                    taskDlg.cobOverTime.SelectedIndex = 0;
                    break;
            }
            if (taskDlg.cobOverTime.SelectedIndex == 1) {
                taskDlg.numComplete.Visible = true;
                taskDlg.numComplete.Value = ((DELActivityDefinition)taskNode.realNode).Quorum;
                taskDlg.label7.Visible = true;
                taskDlg.label8.Visible = true;
            } else {
                taskDlg.numComplete.Visible = false;
                taskDlg.label7.Visible = false;
                taskDlg.label8.Visible = false;
            }
            if (((DELActivityDefinition)taskNode.realNode).Priority == 2) {
                taskDlg.cobPriority.SelectedIndex = 2;
            } else if (((DELActivityDefinition)taskNode.realNode).Priority == 1) {
                taskDlg.cobPriority.SelectedIndex = 1;
            } else {
                taskDlg.cobPriority.SelectedIndex = 0;
            }
            if (((DELActivityDefinition)taskNode.realNode).DocumentationID == Guid.Empty) {
                taskDlg.chbInitAsActor.Checked = false;
            } else {
                taskDlg.chbInitAsActor.Checked = true;
            }
            if (((DELActivityDefinition)taskNode.realNode).GetOption(7).Equals("Y")) {
                taskDlg.chbFilterWithOrg.Checked = true;
            } else {
                taskDlg.chbFilterWithOrg.Checked = false;
            }
            taskDlg.lstActivatedAuto.Items.Clear();
            for (int k = 0; k < ((DELActivityDefinition)taskNode.realNode).OperationsWhenActivated.Count; k++) {
                DELRActivityDefOperation opr = (DELRActivityDefOperation)((DELActivityDefinition)taskNode.realNode).OperationsWhenActivated[k];
                DELOperation operation2 = Model.FindOperation(opr.OperationID);
                taskDlg.lstActivatedAuto.Items.Add(FrmTaskPropertyDlg.ConstructItemFromOpr(opr, operation2.NAME));
            }
            taskDlg.lstFinishedAuto.Items.Clear();
            for (int m = 0; m < ((DELActivityDefinition)taskNode.realNode).OperationsWhenCompleted.Count; m++) {
                DELRActivityDefOperation operation3 = (DELRActivityDefOperation)((DELActivityDefinition)taskNode.realNode).OperationsWhenCompleted[m];
                if (operation3 != null) {
                    DELOperation operation4 = Model.FindOperation(operation3.OperationID);
                    taskDlg.lstFinishedAuto.Items.Add(FrmTaskPropertyDlg.ConstructItemFromOpr(operation3, operation4.NAME));
                }
            }
            taskDlg.lstManual.Items.Clear();
            for (int n = 0; n < ((DELActivityDefinition)taskNode.realNode).OperationsWhenExecuted.Count; n++) {
                DELRActivityDefOperation operation5 = (DELRActivityDefOperation)((DELActivityDefinition)taskNode.realNode).OperationsWhenExecuted[n];
                DELOperation operation6 = Model.FindOperation(operation5.OperationID);
                taskDlg.lstManual.Items.Add(FrmTaskPropertyDlg.ConstructItemFromOpr(operation5, operation6.NAME));
            }
            for (int num7 = 0; num7 < this.shapeData.root.linAry.Count; num7++) {
                DELRStepDef2StepDef realLine = ((Line)this.shapeData.root.linAry[num7]).realLine;
                if (((Line)this.shapeData.root.linAry[num7]).startShape == taskNode) {
                    taskDlg.lstSplitorNodeList.Add(this.shapeData.root.linAry[num7]);
                    taskDlg.lstSplitorNode.Items.Add(((Line)this.shapeData.root.linAry[num7]).endShape.realNode.Name);
                }
            }
            if (!taskDlg.nodeIsEnd(this.shapeData.root.endNode, taskNode)) {
                taskDlg.lstAllNodeSList.Add(this.shapeData.root.endNode);
                taskDlg.lstAllNodeS.Items.Add(this.shapeData.root.endNode.realNode.Name);
            }
            for (int num8 = 0; num8 < this.shapeData.root.routeNodAry.Count; num8++) {
                if (((taskNode != this.shapeData.root.routeNodAry[num8]) && !taskDlg.nodeIsEnd((Shape)this.shapeData.root.routeNodAry[num8], taskNode)) && !taskDlg.nodeIsStart((Shape)this.shapeData.root.routeNodAry[num8], taskNode)) {
                    taskDlg.lstAllNodeSList.Add(this.shapeData.root.routeNodAry[num8]);
                    taskDlg.lstAllNodeS.Items.Add(((RouteNode)this.shapeData.root.routeNodAry[num8]).realNode.Name);
                }
            }
            for (int num9 = 0; num9 < this.shapeData.root.taskNodAry.Count; num9++) {
                if (((taskNode != this.shapeData.root.taskNodAry[num9]) && !taskDlg.nodeIsEnd((Shape)this.shapeData.root.taskNodAry[num9], taskNode)) && !taskDlg.nodeIsStart((Shape)this.shapeData.root.taskNodAry[num9], taskNode)) {
                    taskDlg.lstAllNodeSList.Add(this.shapeData.root.taskNodAry[num9]);
                    taskDlg.lstAllNodeS.Items.Add(((TaskNode)this.shapeData.root.taskNodAry[num9]).realNode.Name);
                }
            }
        }


        private void inputBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                this.changeText();
            }
        }

        private Shape inShape(Point point) {
            return this.shapeData.root.checkInShape(point);
        }
        public void ModifyEndValue(FrmEndNode endDlg, EndNode endNode) {
            DELActivityDefinition realNode = (DELActivityDefinition)endNode.realNode;
            realNode.Description = endDlg.txtDescription.Text;
        }

        public void ModifyProcessValue(FrmProcessEdit processDlg, DELProcessDefinition template) {
            template.ProcessDefToObjList.Clear();
            ArrayList nodeList = new ArrayList();
            foreach (TreeNode node in processDlg.tvwObjTypes.Nodes) {
                if (node.Checked) {
                    nodeList.Add(node);
                }
                FindCheckedNode(node, nodeList);
            }
            foreach (TreeNode node2 in nodeList) {
                DELRProcessDef2Obj obj2 = new DELRProcessDef2Obj {
                    Oid = Guid.NewGuid(),
                    ProcessDefOid = template.ID,
                    ObjectType = ((DEMetaClass)node2.Tag).Name
                };
                if (processDlg.chbOperation.Checked) {
                    obj2.OperationType = processDlg.cobOperations.SelectedValue.ToString();
                } else {
                    obj2.OperationType = "NULL";
                }
                obj2.CreatorOid = ClientData.LogonUser.Oid;
                obj2.Description = "";
                template.ProcessDefToObjList.Add(obj2);
            }
            if (((template.ProcessDefToObjList.Count == 0) && processDlg.chbOperation.Checked) && (processDlg.cobOperations.SelectedValue != null)) {
                DELRProcessDef2Obj obj3 = new DELRProcessDef2Obj {
                    Oid = Guid.NewGuid(),
                    ProcessDefOid = template.ID,
                    OperationType = processDlg.cobOperations.SelectedValue.ToString(),
                    CreatorOid = ClientData.LogonUser.Oid,
                    Description = ""
                };
                template.ProcessDefToObjList.Add(obj3);
            }
            template.Expression = "";
            template.OwnerID = ClientData.LogonUser.Oid;
            template.Description = processDlg.rtbNote.Text;
            template.AllowModifyMonitor = processDlg.chkModifyMonitor.Checked;
            template.AllowNoActorsAtInstantiation = processDlg.chbAllowNoActorsAtInstantiation.Checked;
            template.AddInitiatorAsMonitor = processDlg.chkAutoAdd.Checked;
            template.UseOrgFilter = processDlg.ChkUseOrgFilter.Checked;
            template.CreatorName = processDlg.txtProcessCreator.Text;
            template.Name = processDlg.txtProcessName.Text.Trim();
            if (processDlg.cobPriority.SelectedIndex == 2) {
                template.Priority = 2;
            } else if (processDlg.cobPriority.SelectedIndex == 1) {
                template.Priority = 1;
            } else {
                template.Priority = 0;
            }
            template.OverTimeHandleRule = processDlg.cobOverTimeHandler.Text;
            template.Duration = (int)processDlg.nudDuration.Value;
            if (processDlg.cobDurationUnit.SelectedIndex == 0) {
                template.DurationUnit = this.rmTiModeler.GetString("strHour");
            } else if (processDlg.cobDurationUnit.SelectedIndex == 1) {
                template.DurationUnit = this.rmTiModeler.GetString("strDay");
            }
            template.Classification = processDlg.cobBusinessType.SelectedIndex;
            for (int i = 0; i < processDlg.DeletedActorList.Count; i++) {
                if (((DELRParticipantDefUser)processDlg.DeletedActorList[i]).RowInDataSet != null) {
                    ((DELRParticipantDefUser)processDlg.DeletedActorList[i]).RowInDataSet.Delete();
                }
            }
            template.Actor.Users.Clear();
            for (int j = 0; j < processDlg.lstActorUserList.Count; j++) {
                template.Actor.Users.Add(processDlg.lstActorUserList[j]);
            }
            for (int k = 0; k < processDlg.DeletedMonitorList.Count; k++) {
                if (((DELRParticipantDefUser)processDlg.DeletedMonitorList[k]).RowInDataSet != null) {
                    ((DELRParticipantDefUser)processDlg.DeletedMonitorList[k]).RowInDataSet.Delete();
                }
            }
            template.Monitor.Users.Clear();
            for (int m = 0; m < processDlg.lstMonitorUserList.Count; m++) {
                template.Monitor.Users.Add(processDlg.lstMonitorUserList[m]);
            }
            for (int n = 0; n < processDlg.DeletedOrgList.Count; n++) {
                if (((DELRParticipantDefUser)processDlg.DeletedOrgList[n]).RowInDataSet != null) {
                    ((DELRParticipantDefUser)processDlg.DeletedOrgList[n]).RowInDataSet.Delete();
                }
            }
            template.Organization.Users.Clear();
            for (int num6 = 0; num6 < processDlg.lstOrgList.Count; num6++) {
                template.Organization.Users.Add(processDlg.lstOrgList[num6]);
            }
            for (int num7 = 0; num7 < processDlg.DeletedParameterList.Count; num7++) {
                if (((DELProcessDataDefinition)processDlg.DeletedParameterList[num7]).RowInDataSet != null) {
                    ((DELProcessDataDefinition)processDlg.DeletedParameterList[num7]).RowInDataSet.Delete();
                }
            }
            template.Variables.Clear();
            for (int num8 = 0; num8 < processDlg.proVariableList.Count; num8++) {
                template.Variables.Add(processDlg.proVariableList[num8]);
            }
            template.TargetBusinessObjects.Clear();
            for (int num9 = 0; num9 < processDlg.groupList.Count; num9++) {
                template.TargetBusinessObjects.Add(processDlg.groupList[num9]);
            }
            for (int num10 = 0; num10 < processDlg.lvwActOrders.Items.Count; num10++) {
                DELActivityDefinition tag = processDlg.lvwActOrders.Items[num10].Tag as DELActivityDefinition;
                tag.Priority = num10 + 1;
            }
        }

        public void ModifyRouterValue(FrmRouteDlg routeDlg, RouteNode routeNode) {
            DELRouterDefinition realNode = (DELRouterDefinition)routeNode.realNode;
            realNode.Name = routeDlg.txtBoxNodeName.Text.Trim();
            routeNode.text.caption = realNode.Name;
            switch (routeDlg.comBoxPred.SelectedIndex) {
                case 0:
                    realNode.JoinType = 1;
                    break;

                case 1:
                    realNode.JoinType = 0;
                    break;

                case 2:
                    realNode.JoinType = 2;
                    break;
            }
            realNode.JoinExpression = routeDlg.txtBoxPredecessor.Text;
            switch (routeDlg.comBoxSucc.SelectedIndex) {
                case 0:
                    realNode.SplitType = 4;
                    break;

                case 1:
                    realNode.SplitType = 3;
                    break;
            }
            realNode.SplitExpression = routeDlg.txtBoxSuccessor.Text;
            routeNode.decideImage();
            for (int i = 0; i < routeDlg.DeledLineList.Count; i++) {
                if (((Line)routeDlg.DeledLineList[i]).realLine.RowInDataSet != null) {
                    ((Line)routeDlg.DeledLineList[i]).realLine.RowInDataSet.Delete();
                }
                this.shapeData.root.linAry.Remove(routeDlg.DeledLineList[i]);
            }
            for (int j = 0; j < routeDlg.AddedLineList.Count; j++) {
                this.shapeData.root.linAry.Add(routeDlg.AddedLineList[j]);
            }
        }

        public void ModifyStartValue(FrmStartNode startDlg, StartNode startNode) {
            DELActivityDefinition realNode = (DELActivityDefinition)startNode.realNode;
            if (startDlg.rbtManual.Checked) {
                this.shapeData.template.InitiationType = 0;
            } else if (startDlg.rbtInvoke.Checked) {
                this.shapeData.template.InitiationType = 1;
            } else {
                this.shapeData.template.InitiationType = 2;
                this.shapeData.template.InitiationEvent = startDlg.txtEvent.Text;
                this.shapeData.template.EventKey = startDlg.txtEventKey.Text;
            }
            realNode.Description = startDlg.txtDescription.Text;
            for (int i = 0; i < startDlg.DeledLineList.Count; i++) {
                if (((Line)startDlg.DeledLineList[i]).realLine.RowInDataSet != null) {
                    ((Line)startDlg.DeledLineList[i]).realLine.RowInDataSet.Delete();
                }
                this.shapeData.root.linAry.Remove(startDlg.DeledLineList[i]);
            }
            for (int j = 0; j < startDlg.AddedLineList.Count; j++) {
                this.shapeData.root.linAry.Add(startDlg.AddedLineList[j]);
            }
        }

        public void ModifyTaskValue(FrmTaskPropertyDlg taskDlg, TaskNode taskNode) {
            for (int i = 0; i < taskDlg.DeletedActorList.Count; i++) {
                if (((DELRParticipantDefUser)taskDlg.DeletedActorList[i]).RowInDataSet != null) {
                    ((DELRParticipantDefUser)taskDlg.DeletedActorList[i]).RowInDataSet.Delete();
                }
            }
            for (int j = 0; j < taskDlg.DeletedMonitorList.Count; j++) {
                if (((DELRParticipantDefUser)taskDlg.DeletedMonitorList[j]).RowInDataSet != null) {
                    ((DELRParticipantDefUser)taskDlg.DeletedMonitorList[j]).RowInDataSet.Delete();
                }
            }
            for (int k = 0; k < taskDlg.DeletedNotifyList.Count; k++) {
                if (((DELRParticipantDefUser)taskDlg.DeletedNotifyList[k]).RowInDataSet != null) {
                    ((DELRParticipantDefUser)taskDlg.DeletedNotifyList[k]).RowInDataSet.Delete();
                }
            }
            DELActivityDefinition realNode = (DELActivityDefinition)taskNode.realNode;
            bool flag = taskDlg.cbSubflow.Checked;
            realNode.Actor.Users.Clear();
            for (int m = 0; m < taskDlg.lstActorUserList.Count; m++) {
                realNode.Actor.Users.Add(taskDlg.lstActorUserList[m]);
            }
            realNode.Monitor.Users.Clear();
            for (int n = 0; n < taskDlg.lstMonitorUserList.Count; n++) {
                realNode.Monitor.Users.Add(taskDlg.lstMonitorUserList[n]);
            }
            realNode.Notifier.Users.Clear();
            for (int num6 = 0; num6 < taskDlg.lstNotifyUserList.Count; num6++) {
                realNode.Notifier.Users.Add(taskDlg.lstNotifyUserList[num6]);
            }
            if (taskDlg.chkNotify.Checked) {
                realNode.SetOption(9, "Y");
                realNode.SetOption(10, taskDlg.chkFile.Checked ? "Y" : "N");
                realNode.NotifyTitle = taskDlg.txtTitle.Text.Trim();
                realNode.NotifyContent = taskDlg.rtContent.Text.Trim();
                realNode.NotifyExp = taskDlg.myAct.NotifyExp;
            } else {
                realNode.SetOption(9, "N");
            }
            realNode.Name = taskDlg.txtTaskName.Text.Trim();
            taskNode.text.caption = realNode.Name;
            realNode.Description = taskDlg.txtDescription.Text;
            realNode.Duration = (int)taskDlg.nudDuration.Value;
            if (taskDlg.cobTimeUnit.SelectedIndex == 0) {
                realNode.DurationUnit = this.rmTiModeler.GetString("strHour");
            } else if (taskDlg.cobTimeUnit.SelectedIndex == 1) {
                realNode.DurationUnit = this.rmTiModeler.GetString("strDay");
            }
            if (taskDlg.chbNoObjThenThrough.Checked) {
                realNode.SetOption(0, "Y");
            } else {
                realNode.SetOption(0, "N");
            }
            if (taskDlg.chbDirectThrough.Checked) {
                realNode.SetOption(6, "Y");
            } else {
                realNode.SetOption(6, "N");
            }
            if (taskDlg.chbWorkItemDevolve.Checked) {
                realNode.SetOption(2, "Y");
            } else {
                realNode.SetOption(2, "N");
            }
            if (taskDlg.chbRestrictActor.Checked) {
                realNode.SetOption(1, "Y");
            } else {
                realNode.SetOption(1, "N");
            }
            if (taskDlg.chkActorPre.Checked) {
                realNode.SetOption(4, "Y");
            } else {
                realNode.SetOption(4, "N");
            }
            realNode.AutoSendMessage = taskDlg.chbAutoSendMessage.Checked;
            realNode.CanChangeActor = taskDlg.chbChangeActor.Checked;
            realNode.OverTimeHandleRule = taskDlg.cobOverTime.Text;
            realNode.Quorum = (int)taskDlg.numComplete.Value;
            realNode.NodeType = StepType.ACTIVITY;
            realNode.ProcessDefinitionID = this.shapeData.template.ID;
            if (taskDlg.chbInitAsActor.Checked) {
                realNode.DocumentationID = Guid.NewGuid();
            } else {
                realNode.DocumentationID = Guid.Empty;
            }
            if (taskDlg.chbFilterWithOrg.Checked) {
                ((DELActivityDefinition)taskNode.realNode).SetOption(7, "Y");
            } else {
                ((DELActivityDefinition)taskNode.realNode).SetOption(7, "N");
            }
            realNode.UpdateDate = DateTime.Now;
            if (!taskDlg.cbSubflow.Checked) {
                realNode.SubFlowID = Guid.Empty;
                foreach (DELDataMappingDef def in realNode.DataMappingList) {
                    if (def.RowInDataSet != null) {
                        def.RowInDataSet.Delete();
                    }
                }
                realNode.DataMappingList.Clear();
            }
            for (int num7 = 0; num7 < taskDlg.DelNodOprList.Count; num7++) {
                DELRActivityDefOperation oneActOpr = (DELRActivityDefOperation)taskDlg.DelNodOprList[num7];
                this.OnDeleteRActOperDef(oneActOpr);
            }
            if (flag) {
                foreach (DELRActivityDefOperation operation2 in realNode.OperationsWhenActivated) {
                    this.OnDeleteRActOperDef(operation2);
                    if (operation2.RowInDataSet != null) {
                        operation2.RowInDataSet.Delete();
                    }
                }
                foreach (DELRActivityDefOperation operation3 in realNode.OperationsWhenCompleted) {
                    this.OnDeleteRActOperDef(operation3);
                    if (operation3.RowInDataSet != null) {
                        operation3.RowInDataSet.Delete();
                    }
                }
                foreach (DELRActivityDefOperation operation4 in realNode.OperationsWhenExecuted) {
                    this.OnDeleteRActOperDef(operation4);
                    if (operation4.RowInDataSet != null) {
                        operation4.RowInDataSet.Delete();
                    }
                }
            }
            realNode.OperationsWhenActivated.Clear();
            if (!flag) {
                for (int num8 = 0; num8 < taskDlg.lstActivatedAuto.Items.Count; num8++) {
                    realNode.OperationsWhenActivated.Add(taskDlg.lstActivatedAuto.Items[num8].Tag);
                }
            }
            realNode.OperationsWhenCompleted.Clear();
            if (!flag) {
                for (int num9 = 0; num9 < taskDlg.lstFinishedAuto.Items.Count; num9++) {
                    realNode.OperationsWhenCompleted.Add(taskDlg.lstFinishedAuto.Items[num9].Tag);
                }
            }
            realNode.OperationsWhenExecuted.Clear();
            if (!flag) {
                for (int num10 = 0; num10 < taskDlg.lstManual.Items.Count; num10++) {
                    realNode.OperationsWhenExecuted.Add(taskDlg.lstManual.Items[num10].Tag);
                }
            }
            for (int num11 = 0; num11 < taskDlg.DeledLineList.Count; num11++) {
                if (((Line)taskDlg.DeledLineList[num11]).realLine.RowInDataSet != null) {
                    ((Line)taskDlg.DeledLineList[num11]).realLine.RowInDataSet.Delete();
                }
                this.shapeData.root.linAry.Remove(taskDlg.DeledLineList[num11]);
            }
            for (int num12 = 0; num12 < taskDlg.AddedLineList.Count; num12++) {
                this.shapeData.root.linAry.Add(taskDlg.AddedLineList[num12]);
            }
        }

        public void MoveShape(Point movePoint) {
            if (this.selectedShape != null) {
                this.selectedShape.moveShape(this.moveBase, movePoint);
            } else if (this.selectedShapeList.Count > 0) {
                int maxClientX = ConstForModeler.MaxClientX;
                int maxClientY = ConstForModeler.MaxClientY;
                int num3 = 0;
                int num4 = 0;
                for (int i = 0; i < this.selectedShapeList.Count; i++) {
                    if ((this.selectedShapeList[i].GetType().Name.ToString() != "NodeText") && (this.selectedShapeList[i].GetType().Name.ToString() != "Line")) {
                        if (((Shape)this.selectedShapeList[i]).rec.X < maxClientX) {
                            maxClientX = ((Shape)this.selectedShapeList[i]).rec.X;
                        }
                        if (((Shape)this.selectedShapeList[i]).rec.Y < maxClientY) {
                            maxClientY = ((Shape)this.selectedShapeList[i]).rec.Y;
                        }
                        if ((((Shape)this.selectedShapeList[i]).rec.Y + ((Shape)this.selectedShapeList[i]).rec.Height) > num3) {
                            num3 = ((Shape)this.selectedShapeList[i]).rec.Y + ((Shape)this.selectedShapeList[i]).rec.Height;
                        }
                        if ((((Shape)this.selectedShapeList[i]).rec.X + ((Shape)this.selectedShapeList[i]).rec.Width) > num4) {
                            num4 = ((Shape)this.selectedShapeList[i]).rec.X + ((Shape)this.selectedShapeList[i]).rec.Width;
                        }
                    }
                }
                this.mutiRec.X = maxClientX;
                this.mutiRec.Y = maxClientY;
                this.mutiRec.Height = num3 - maxClientY;
                this.mutiRec.Width = num4 - maxClientX;
                Point point = new Point {
                    X = (this.mutiRec.X + movePoint.X) - this.moveBase.X,
                    Y = (this.mutiRec.Y + movePoint.Y) - this.moveBase.Y
                };
                if (point.X < 0) {
                    point.X = 0;
                }
                if (point.Y < 0) {
                    point.Y = 0;
                }
                if (point.X > (ConstForModeler.MaxClientX - this.mutiRec.Width)) {
                    point.X = ConstForModeler.MaxClientX - this.mutiRec.Width;
                }
                if (point.Y > (ConstForModeler.MaxClientY - this.mutiRec.Height)) {
                    point.Y = ConstForModeler.MaxClientY - this.mutiRec.Height;
                }
                movePoint.X = this.moveBase.X + (point.X - this.mutiRec.X);
                movePoint.Y = this.moveBase.Y + (point.Y - this.mutiRec.Y);
                for (int j = 0; j < this.selectedShapeList.Count; j++) {
                    if (((Shape)this.selectedShapeList[j]) is Line) {
                        for (int k = 0; k < ((Line)this.selectedShapeList[j]).pointsList.Count; k++) {
                            ((Line)this.selectedShapeList[j]).movePoint(k, this.moveBase, movePoint);
                        }
                    } else {
                        ((Shape)this.selectedShapeList[j]).moveShape(this.moveBase, movePoint);
                    }
                }
            }
            this.moveBase = movePoint;
            this.Refresh();
        }

        private void OnDeleteRActOperDef(DELRActivityDefOperation OneActOpr) {
            OneActOpr.OnDelete();
            DELOperation operation = Model.FindOperation(OneActOpr.OperationID);
            DELOperationDefinitionArgs args = this.mainWindow.proTemplate.OperationDefinitionArguments[OneActOpr.ID] as DELOperationDefinitionArgs;
            if (args != null) {
                args.SetDelete();
                if (args.State == DataRowState.Detached) {
                    this.mainWindow.proTemplate.OperationDefinitionArguments.Remove(OneActOpr.ID);
                } else {
                    args.ActivityDefinitionID = OneActOpr.ActivityDefinitionID;
                    args.OperationDefinitionID = OneActOpr.OperationID;
                    if (operation != null) {
                        args.AddinOid = operation.AddinOid;
                    } else {
                        args.AddinOid = Guid.Empty;
                    }
                }
                if (args.Tag == null) {
                    args.Tag = new ArrayList();
                }
                (args.Tag as ArrayList).Add(OneActOpr.ID);
            } else if (operation.AddinOid != Guid.Empty) {
                args = new DELOperationDefinitionArgs {
                    ActivityDefinitionID = OneActOpr.ActivityDefinitionID,
                    OperationDefinitionID = OneActOpr.OperationID
                };
                args.SetAttach();
                args.SetDelete();
                if (operation != null) {
                    args.AddinOid = operation.AddinOid;
                } else {
                    args.AddinOid = Guid.Empty;
                }
                if (args.Tag == null) {
                    args.Tag = new ArrayList();
                }
                (args.Tag as ArrayList).Add(OneActOpr.ID);
                this.mainWindow.proTemplate.OperationDefinitionArguments.Add(OneActOpr.ID, args);
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            this.g = e.Graphics;
            if (this.wantLine) {
                if (this.isCaptured) {
                    this.g.DrawLine(this.myPen, this.tempLineStart, this.tempLineEnd);
                    this.drawArrow(this.g, this.myPen, this.tempLineStart, this.tempLineEnd);
                }
            } else if (this.turningPosition != -2) {
                if (this.isCaptured) {
                    this.g.DrawLine(this.myPen, this.tempLineStart, this.mousePosition);
                    this.g.DrawLine(this.myPen, this.tempLineEnd, this.mousePosition);
                }
            } else if (this.isCaptured && !this.wantMoveShape) {
                this.myPen.DashStyle = DashStyle.Dot;
                this.g.DrawRectangle(this.myPen, this.mutiRec);
                this.myPen.DashStyle = DashStyle.Solid;
            }
            this.shapeData.root.draw(this.g);
            if (this.selectedShape != null) {
                this.selectedShape.draw(this.g);
            }
            foreach (Shape shape in this.selectedShapeList) {
                shape.draw(this.g);
            }
        }

        private void reSetScrollRange(Point point) {
            int num2 = point.X + 80;
            int num4 = point.Y + 100;
            if ((num4 <= ConstForModeler.MaxClientY) && (num2 <= ConstForModeler.MaxClientX)) {
                if (num4 > this.scrollY) {
                    this.scrPosY = num4 - this.initClientY;
                    this.scrollY = num4;
                }
                if (num2 > this.scrollX) {
                    this.scrPosX = num2 - this.initClientX;
                    this.scrollX = num2;
                }
            }
        }

        private void reSetScrollRange(Shape shape) {
            int num = shape.startPoint.X + shape.width;
            int num2 = num + 80;
            int num3 = shape.startPoint.Y + shape.height;
            int num4 = num3 + 100;
            if ((num4 <= ConstForModeler.MaxClientY) && (num2 <= ConstForModeler.MaxClientX)) {
                if (num4 > this.scrollY) {
                    this.scrPosY = num4 - this.initClientY;
                    this.scrollY = num4;
                }
                if (num2 > this.scrollX) {
                    this.scrPosX = num2 - this.initClientX;
                    this.scrollX = num2;
                }
            }
        }

        public void setMainFrmTreeSelected() {
            if (this.selectedShape == null) {
                TreeNode keyByValue = (TreeNode)((FrmMain)this.mainWindow.MdiParent).HashMDiWindows.GetKeyByValue(this.mainWindow);
                ((FrmMain)this.mainWindow.MdiParent).tvwNavigator.SelectedNode = keyByValue;
            } else if ((this.selectedShape.GetType().Name.ToString().Equals("StartNode") || this.selectedShape.GetType().Name.ToString().Equals("EndNode")) || ((this.selectedShape.GetType().Name.ToString().Equals("TaskNode") || this.selectedShape.GetType().Name.ToString().Equals("RouteNode")) || this.selectedShape.GetType().Name.ToString().Equals("NodeText"))) {
                TreeNode rootNode = (TreeNode)((FrmMain)this.mainWindow.MdiParent).HashMDiWindows.GetKeyByValue(this.mainWindow);
                if (rootNode != null) {
                    TreeNode node3 = this.FindTreeNode(rootNode, this.selectedShape);
                    if (node3 != null) {
                        ((FrmMain)this.mainWindow.MdiParent).tvwNavigator.SelectedNode = node3;
                    }
                }
            }
        }

        public void setModified(bool index) {
            if (index) {
                this.modified = true;
            } else {
                this.modified = false;
            }
        }

        public void setSize() {
            this.disRectangle = base.ClientRectangle;
            this.scrollX = this.disRectangle.Right;
            this.scrollY = this.disRectangle.Bottom;
            this.initClientX = this.scrollX;
            this.initClientY = this.scrollY;
            this.scrPosX = 0;
            this.scrPosY = 0;
        }

        public void showInputBox(NodeText nodeText) {
            base.Controls.Add(this.inputBox);
            this.inputBox.Text = nodeText.caption;
            this.inputBox.Location = nodeText.startPoint;
            this.inputBox.Show();
            this.inputBox.Focus();
        }

        public void ViewPanel_Click(object sender, EventArgs e) {
            this.modified = true;
        }

        public void ViewPanel_DoubleClick(object sender, EventArgs e) {
            this.wantMovePoint = -1;
            if (this.selectedShape != null) {
                if (this.selectedShape.GetType().Name.Equals("Line")) {
                    int i = ((Line)this.selectedShape).findPointPosition(this.mousePosition);
                    if (i > -1) {
                        Point p = (Point)((Line)this.selectedShape).pointsList[i];
                        this.deleteLinePoint((Line)this.selectedShape, this.mousePosition);
                        RULine line = new RULine(this.mainWindow, (Line)this.selectedShape, i, p, "OP_DELETE_POINT");
                        this.mainWindow.stackUndo.Push(line);
                        this.mainWindow.setRUToolbarStatus();
                    } else if (!this.breakLine) {
                        Point[] pointArray = this.checkPointInLine(this.mousePosition, ref this.turningPosition, ref this.tempLine);
                        if (pointArray != null) {
                            this.tempLineStart = pointArray[0];
                            this.tempLineEnd = pointArray[1];
                            this.breakLine = true;
                        }
                    } else {
                        this.breakLine = false;
                        this.turningPosition = -2;
                    }
                } else if (!this.selectedShape.GetType().Name.Equals("NodeText")) {
                    if (this.selectedShape.GetType().Name.Equals("TaskNode")) {
                        this.mainWindow.showNodeProperty();
                    } else if (this.selectedShape.GetType().Name.Equals("RouteNode")) {
                        this.mainWindow.showNodeProperty();
                    } else if (this.selectedShape.GetType().Name.Equals("StartNode")) {
                        this.mainWindow.showNodeProperty();
                    } else if (this.selectedShape.GetType().Name.Equals("EndNode")) {
                        this.mainWindow.showNodeProperty();
                    }
                }
                this.modified = true;
            }
        }

        public void ViewPanel_MouseDown(object sender, MouseEventArgs e) {
            base.Focus();
            Point point = new Point(e.X, e.Y);
            this.mousePosition = point;
            this.tempLineEnd = this.tempLineStart = this.mousePosition;
            Shape item = this.whichSelected(point, ref this.wantLine, ref this.wantMoveShape);
            if (e.Button == MouseButtons.Left) {
                if (this.wantInput) {
                    return;
                }
                if (Control.ModifierKeys == Keys.Control) {
                    return;
                }
                this.isCaptured = true;
                if (item != null) {
                    if ((this.selectedShapeList.Count <= 1) || !item.isSelected) {
                        for (int i = 0; i < this.selectedShapeList.Count; i++) {
                            ((Shape)this.selectedShapeList[i]).isSelected = false;
                        }
                        this.selectedShapeList.Clear();
                        if (this.selectedShape != null) {
                            this.selectedShape.isSelected = false;
                        }
                        this.selectedShape = item;
                        this.selectedShape.isSelected = true;
                        this.wantMovePoint = this.selectedShape.isPointInSymbol(point);
                        if ((this.selectedShape is Line) && (this.wantMovePoint != -1)) {
                            this.pointPositionOriginal = (Point)((Line)this.selectedShape).pointsList[this.wantMovePoint];
                        }
                        if (this.wantLine) {
                            this.tempLineStart = point;
                            this.startShape = this.selectedShape;
                        }
                        if (this.wantMoveShape) {
                            this.moveBaseOriginal = point;
                            this.moveBase = point;
                        }
                    } else {
                        this.wantLine = false;
                        this.wantMoveShape = true;
                        this.moveBaseOriginal = point;
                        this.moveBase = point;
                    }
                } else {
                    if (this.selectedShape != null) {
                        this.selectedShape.isSelected = false;
                    }
                    this.mutiRec.X = 0;
                    this.mutiRec.Y = 0;
                    this.mutiRec.Width = 0;
                    this.mutiRec.Height = 0;
                    for (int j = 0; j < this.selectedShapeList.Count; j++) {
                        ((Shape)this.selectedShapeList[j]).isSelected = false;
                    }
                    this.selectedShapeList.Clear();
                    this.selectedShape = null;
                }
            } else if (e.Button == MouseButtons.Right) {
                if (item != null) {
                    if ((this.selectedShapeList.Count == 0) || !this.selectedShapeList.Contains(item)) {
                        for (int k = 0; k < this.selectedShapeList.Count; k++) {
                            ((Shape)this.selectedShapeList[k]).isSelected = false;
                        }
                        this.selectedShapeList.Clear();
                        if (this.selectedShape != null) {
                            this.selectedShape.isSelected = false;
                        }
                        this.selectedShape = item;
                        this.selectedShape.isSelected = true;
                    }
                } else if (this.selectedShape != null) {
                    this.selectedShape.isSelected = false;
                    this.selectedShape = null;
                } else if (this.selectedShapeList.Count > 0) {
                    for (int m = 0; m < this.selectedShapeList.Count; m++) {
                        ((Shape)this.selectedShapeList[m]).isSelected = false;
                    }
                    this.selectedShapeList.Clear();
                }
            }
            this.modified = true;
            this.Refresh();
            this.setMainFrmTreeSelected();
        }

        public void ViewPanel_MouseMove(object sender, MouseEventArgs e) {
            if (!this.wantInput && (Control.ModifierKeys != Keys.Control)) {
                Point point = new Point(e.X, e.Y);
                if (this.wantLine) {
                    if (this.isCaptured) {
                        this.tempLineEnd = point;
                        this.Refresh();
                    }
                } else if (this.turningPosition != -2) {
                    if (this.isCaptured) {
                        this.mousePosition = point;
                        this.Refresh();
                    }
                } else if (this.wantMovePoint != -1) {
                    if (this.isCaptured && (this.selectedShape != null)) {
                        this.selectedShape.movePoint(this.wantMovePoint, this.mousePosition, point);
                        this.Refresh();
                        this.mousePosition = point;
                    }
                } else if (this.wantMoveShape) {
                    if (this.isCaptured) {
                        this.MoveShape(point);
                    }
                } else if (this.isCaptured) {
                    int x;
                    int y;
                    this.tempLineEnd = point;
                    if (this.tempLineStart.X <= this.tempLineEnd.X) {
                        x = this.tempLineStart.X;
                    } else {
                        x = this.tempLineEnd.X;
                    }
                    if (this.tempLineStart.Y <= this.tempLineEnd.Y) {
                        y = this.tempLineStart.Y;
                    } else {
                        y = this.tempLineEnd.Y;
                    }
                    this.mutiRec.X = x;
                    this.mutiRec.Y = y;
                    this.mutiRec.Width = Math.Abs((int)(this.tempLineEnd.X - this.tempLineStart.X));
                    this.mutiRec.Height = Math.Abs((int)(this.tempLineEnd.Y - this.tempLineStart.Y));
                    for (int i = 0; i < this.selectedShapeList.Count; i++) {
                        ((Shape)this.selectedShapeList[i]).isSelected = false;
                    }
                    this.selectedShapeList.Clear();
                    this.whichSelected(this.mutiRec);
                    this.Refresh();
                }
            }
        }

        public void ViewPanel_MouseUp(object sender, MouseEventArgs e) {
            MenuItemEx ex3;
            MenuItemEx ex8;
            this.mousePosition = new Point(e.X, e.Y);
            if (e.Button != MouseButtons.Left) {
                if (e.Button != MouseButtons.Right) {
                    goto Label_0F46;
                }
                if (this.selectedShape == null) {
                    if (this.selectedShapeList.Count > 0) {
                        ContextMenu menu2 = new ContextMenu();
                        MenuItemEx item = new MenuItemEx("copyMuti", "复制", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_COPY")
                        };
                        item.Click += new EventHandler(this.mainWindow.cmiNodeCopy_Click);
                        menu2.MenuItems.Add(item);
                        MenuItemEx ex12 = new MenuItemEx("deleteMuti", "删除所选", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                        };
                        ex12.Click += new EventHandler(this.mainWindow.cmiShapeDelete_Click);
                        menu2.MenuItems.Add(ex12);
                        menu2.Show(this, this.mousePosition);
                    } else {
                        this.mainWindow.ControlPerm();
                        this.mainWindow.ControlPaste();
                        this.mainWindow.cmuProcessProperty.Show(this, this.mousePosition);
                    }
                    goto Label_0F46;
                }
                if (this.selectedShape.GetType().Name.ToString().Equals("Line")) {
                    if (((Line)this.selectedShape).startShape.realNode.NodeType == StepType.ROUTER) {
                        if (this.mainWindow.cmuLineProperty.MenuItems.Count == 2) {
                            MenuItemEx ex = new MenuItemEx("-", "-", null, null);
                            this.mainWindow.cmuLineProperty.MenuItems.Add(ex);
                            ex = new MenuItemEx("deleteLine", "删除连线", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                            };
                            ex.Click += new EventHandler(this.mainWindow.cmiShapeDelete_Click);
                            this.mainWindow.cmuLineProperty.MenuItems.Add(ex);
                        }
                        this.mainWindow.cmuLineProperty.Show(this, this.mousePosition);
                    } else {
                        ContextMenu menu = new ContextMenu();
                        MenuItemEx ex2 = new MenuItemEx("deleteLine", "删除连线", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                        };
                        ex2.Click += new EventHandler(this.mainWindow.cmiShapeDelete_Click);
                        menu.MenuItems.Add(ex2);
                        menu.Show(this, this.mousePosition);
                    }
                    goto Label_0F46;
                }
                if (typeof(NodeText).IsInstanceOfType(this.selectedShape)) {
                    goto Label_0F46;
                }
                if ((!(this.selectedShape.realNode is DELActivityDefinition) || (this.selectedShape.realNode.Name.ToUpper() == "START")) || (this.selectedShape.realNode.Name.ToUpper() == "END")) {
                    if (((this.selectedShape.realNode is DELRouterDefinition) || (this.selectedShape.realNode.Name.ToUpper() != "START")) || (this.selectedShape.realNode.Name.ToUpper() != "END")) {
                        MenuItemEx ex6 = null;
                        foreach (MenuItemEx ex7 in this.mainWindow.cmuNodeProperty.MenuItems) {
                            if (ex7.Name == "Auth") {
                                ex6 = ex7;
                                break;
                            }
                        }
                        if (ex6 != null) {
                            this.mainWindow.cmuNodeProperty.MenuItems.Remove(ex6);
                        }
                    }
                    goto Label_0D42;
                }
                ex3 = null;
                foreach (MenuItemEx ex4 in this.mainWindow.cmuNodeProperty.MenuItems) {
                    if (ex4.Name == "Auth") {
                        ex3 = ex4;
                        break;
                    }
                }
            } else {
                this.isCaptured = false;
                if (Control.ModifierKeys == Keys.Control) {
                    Shape shape = this.whichSelected(this.mousePosition, ref this.wantLine, ref this.wantMoveShape);
                    if (shape != null) {
                        if (this.selectedShapeList.Count > 0) {
                            if (this.selectedShapeList.Contains(shape)) {
                                this.selectedShapeList.Remove(shape);
                                shape.isSelected = false;
                            } else {
                                this.selectedShapeList.Add(shape);
                                shape.isSelected = true;
                            }
                        } else if (this.selectedShape != null) {
                            if (this.selectedShape != shape) {
                                this.selectedShapeList.Add(this.selectedShape);
                                this.selectedShape = null;
                                this.selectedShapeList.Add(shape);
                                shape.isSelected = true;
                            } else {
                                this.selectedShape = null;
                                shape.isSelected = false;
                            }
                        } else {
                            this.selectedShape = shape;
                            this.selectedShape.isSelected = true;
                        }
                    }
                } else {
                    if (this.wantInput) {
                        return;
                    }
                    if (this.wantMovePoint != -1) {
                        if ((this.selectedShape != null) && (this.selectedShape is Line)) {
                            Point point = (Point)((Line)this.selectedShape).pointsList[this.wantMovePoint];
                            if (!this.pointPositionOriginal.Equals(point)) {
                                RULine line = new RULine(this.mainWindow, (Line)this.selectedShape, this.wantMovePoint, this.pointPositionOriginal, point);
                                this.mainWindow.stackUndo.Push(line);
                                this.mainWindow.setRUToolbarStatus();
                            }
                        }
                        this.wantMovePoint = -1;
                    }
                    if (this.breakLine && (this.turningPosition != -2)) {
                        this.addPoint(this.turningPosition, this.mousePosition);
                        this.breakLine = false;
                        this.turningPosition = -2;
                        RULine line2 = new RULine(this.mainWindow, (Line)this.selectedShape, this.turningPosition, this.mousePosition, "OP_ADD_POINT");
                        this.mainWindow.stackUndo.Push(line2);
                        this.mainWindow.setRUToolbarStatus();
                    }
                    if (this.wantMoveShape) {
                        if ((this.selectedShape != null) && !this.moveBase.Equals(this.moveBaseOriginal)) {
                            if (this.selectedShape is TaskNode) {
                                RUTaskNode node = new RUTaskNode(this.mainWindow, (TaskNode)this.selectedShape, this.moveBaseOriginal, this.moveBase);
                                this.mainWindow.stackUndo.Push(node);
                                this.mainWindow.setRUToolbarStatus();
                            } else if (this.selectedShape is RouteNode) {
                                RURouteNode node2 = new RURouteNode(this.mainWindow, (RouteNode)this.selectedShape, this.moveBaseOriginal, this.moveBase);
                                this.mainWindow.stackUndo.Push(node2);
                                this.mainWindow.setRUToolbarStatus();
                            } else if ((this.selectedShape is StartNode) || (this.selectedShape is EndNode)) {
                                RUSENode node3 = new RUSENode(this.mainWindow, this.selectedShape, this.moveBaseOriginal, this.moveBase);
                                this.mainWindow.stackUndo.Push(node3);
                                this.mainWindow.setRUToolbarStatus();
                            }
                        } else if ((this.selectedShapeList.Count > 0) && !this.moveBase.Equals(this.moveBaseOriginal)) {
                            RUMutiShape shape2 = new RUMutiShape(this.mainWindow, this.selectedShapeList, this.moveBaseOriginal, this.moveBase);
                            this.mainWindow.stackUndo.Push(shape2);
                            this.mainWindow.setRUToolbarStatus();
                        } else if (this.moveBase.Equals(this.moveBaseOriginal)) {
                            for (int i = 0; i < this.selectedShapeList.Count; i++) {
                                ((Shape)this.selectedShapeList[i]).isSelected = false;
                            }
                            this.selectedShapeList.Clear();
                            this.selectedShape = this.whichSelected(this.mousePosition, ref this.wantLine, ref this.wantMoveShape);
                            if (this.selectedShape != null) {
                                this.selectedShape.isSelected = true;
                            }
                        }
                        this.wantLine = false;
                        this.wantMoveShape = false;
                    }
                    if (this.wantLine) {
                        Point point2 = new Point(e.X, e.Y);
                        this.endShape = this.inShape(point2);
                        if (((this.endShape != null) && !typeof(NodeText).IsInstanceOfType(this.endShape)) && (!typeof(NodeText).IsInstanceOfType(this.startShape) && (this.startShape != this.endShape))) {
                            ErrorDetective detective = new ErrorDetective(this.shapeData, this.startShape, this.endShape);
                            if (detective.Dynamic_ErrorFree()) {
                                DELRStepDef2StepDef def = new DELRStepDef2StepDef();
                                Line theLine = new Line(this.startShape, this.endShape) {
                                    realLine = def
                                };
                                RULine line4 = new RULine(this.mainWindow, theLine, "OP_Add");
                                this.mainWindow.stackUndo.Push(line4);
                                this.mainWindow.setRUToolbarStatus();
                                this.addLine(theLine);
                            } else {
                                MessageBox.Show("图形逻辑错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            this.startShape = null;
                            this.endShape = null;
                        }
                        this.wantLine = false;
                        this.tempLineStart = this.tempLineEnd;
                    }
                    if (this.startNodeFlag) {
                        if (this.shapeData.root.startNode != null) {
                            MessageBox.Show("只能有一个开始节点", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        } else {
                            DELActivityDefinition definition = new DELActivityDefinition(Guid.NewGuid(), this.shapeData.template.ID);
                            StartNode startNode = new StartNode(new Point(e.X, e.Y)) {
                                realNode = definition
                            };
                            this.addStartNode(startNode);
                        }
                    }
                    if (this.endNodeFlag) {
                        if (this.shapeData.root.endNode != null) {
                            MessageBox.Show("只能有一个结束节点", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        } else {
                            DELActivityDefinition definition2 = new DELActivityDefinition(Guid.NewGuid(), this.shapeData.template.ID);
                            EndNode endNode = new EndNode(new Point(e.X, e.Y)) {
                                realNode = definition2
                            };
                            this.addEndNode(endNode);
                        }
                    }
                    if (this.taskNodeFlag) {
                        DELActivityDefinition definition3 = new DELActivityDefinition(Guid.NewGuid(), this.shapeData.template.ID);
                        string name = "";
                        TaskNode taskNode = new TaskNode(new Point(e.X, e.Y), name);
                        definition3.Name = taskNode.text.caption;
                        taskNode.realNode = definition3;
                        FrmTaskPropertyDlg taskDlg = new FrmTaskPropertyDlg(true) {
                            proTemplate = this.shapeData.template,
                            realTaskNode = definition3,
                            myAct = definition3
                        };
                        for (int j = 0; j < Model.BPMOperationList.Count; j++) {
                            if ((((DELOperation)Model.BPMOperationList[j]).NAME == "BO浏览器") || (((DELOperation)Model.BPMOperationList[j]).NAME == "BO")) {
                                DELRActivityDefOperation operation = new DELRActivityDefOperation {
                                    ActivityDefinitionID = definition3.ID,
                                    ProcessDefinitionID = this.shapeData.template.ID,
                                    WhenExecute = 6,
                                    ExecuteOrder = 0
                                };
                                DELOperation operation2 = (DELOperation)Model.BPMOperationList[j];
                                operation.OperationID = operation2.ID;
                                operation.OperationName = operation2.Name;
                                definition3.OperationsWhenExecuted.Add(operation);
                                break;
                            }
                        }
                        this.InitFrmTaskPropertyDlg(taskDlg, taskNode);
                        taskDlg.chbChangeActor.Checked = true;
                        int num3 = 1;
                        do {
                            taskDlg.txtTaskName.Text = "任务" + num3;
                            num3++;
                        }
                        while (!taskDlg.validateData());
                        this.ModifyTaskValue(taskDlg, taskNode);
                        RUTaskNode node7 = new RUTaskNode(this.mainWindow, taskNode, "OP_Add");
                        this.mainWindow.stackUndo.Push(node7);
                        this.mainWindow.setRUToolbarStatus();
                        this.addTaskNode(taskNode);
                        ((FrmMain)this.mainWindow.MdiParent).AddTreeNode(this.mainWindow, taskNode);
                    }
                    if (this.routeNodeFlag) {
                        this.getRouteNodeNumber();
                        FrmRouteDlg routeDlg = new FrmRouteDlg(true);
                        DELRouterDefinition realNode = new DELRouterDefinition {
                            ID = Guid.NewGuid()
                        };
                        RouteNode routeNode = new RouteNode(new Point(e.X, e.Y), realNode);
                        this.selectedShape = routeNode;
                        this.InitFrmRouteDlg(routeDlg, routeNode);
                        int num4 = 1;
                        do {
                            routeDlg.txtBoxNodeName.Text = "路由" + num4;
                            num4++;
                        }
                        while (!routeDlg.validateData());
                        this.ModifyRouterValue(routeDlg, routeNode);
                        routeNode.decideImage();
                        RURouteNode node9 = new RURouteNode(this.mainWindow, routeNode, "OP_Add");
                        this.mainWindow.stackUndo.Push(node9);
                        this.mainWindow.setRUToolbarStatus();
                        this.addRouteNode(routeNode);
                        ((FrmMain)this.mainWindow.MdiParent).AddTreeNode(this.mainWindow, routeNode);
                    }
                }
                goto Label_0F46;
            }
            if (ex3 == null) {
                MenuItemEx ex5 = new MenuItemEx("Auth", new EventHandler(this.cmiNodeAuth_Click)) {
                    Text = "设置权限"
                };
                this.mainWindow.cmuNodeProperty.MenuItems.Add(ex5);
            }
        Label_0D42:
            ex8 = null;
            foreach (MenuItemEx ex9 in this.mainWindow.cmuNodeProperty.MenuItems) {
                if (ex9.Name == "deleteNode") {
                    ex8 = ex9;
                    break;
                }
            }
            if (ex8 == null) {
                MenuItemEx ex10 = new MenuItemEx("deleteNode", "删除节点", null, null) {
                    ImageList = ClientData.MyImageList.imageList,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                };
                ex10.Click += new EventHandler(this.mainWindow.cmiShapeDelete_Click);
                this.mainWindow.cmuNodeProperty.MenuItems.Add(ex10);
            }
            this.mainWindow.cmuNodeProperty.Show(this, this.mousePosition);
        Label_0F46:
            this.Refresh();
        }

        private void ViewPanel_Resize(object sender, EventArgs e) {
        }

        private void ViewPanel_SizeChanged(object sender, EventArgs e) {
        }

        private void whichSelected(Rectangle rec) {
            this.shapeData.root.getSelectionArray(rec, this.selectedShapeList);
        }

        private Shape whichSelected(Point point, ref bool wantLine, ref bool wantMoveShape) {
            return this.shapeData.root.checkSelection(point, ref wantLine, ref wantMoveShape);
        }
    }
}

