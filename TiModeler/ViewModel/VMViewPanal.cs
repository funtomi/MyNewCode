namespace Thyt.TiPLM.CLT.TiModeler.ViewModel
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.View;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.View;
    using Thyt.TiPLM.UIL.Admin.ViewNetwork;
    using Thyt.TiPLM.UIL.Common;

    public partial class VMViewPanal : Panel
    {
        public double arrowAngle;
        public double arrowLong;
        public bool breakLine;
        public bool CursorDown;
        public bool CursorFlag;
        public VMDoc1 dataDoc;
        private VMShape1 endShape;
        public Graphics g;
        public bool hasSaved;
        private TextBox inputBox;
        public bool isCaptured;
        public VMFrame mainWindow;
        public static int MaxClientX = 0x640;
        public static int MaxClientY = 0x500;
        public bool modified;
        public Point mousePosition;
        public Point moveBase;
        public Rectangle mulSelRec;
        public ArrayList multiSelected;
        public Pen myPen;
        public bool RecDown;
        public bool RecFlag;
        public bool ReserveDown;
        public bool ReserveFlag;
        public VMShape1 selectedShape;
        private VMShape1 startShape;
        private VMLine1 tempLine;
        private Point tempLineEnd;
        private Point tempLineStart;
        public int turningPosition;
        public bool wantInput;
        public bool wantLine;
        public int wantMovePoint;
        public bool wantMoveShape;
        public bool wantMulSelect;

        public VMViewPanal(VMFrame mainWindow)
        {
            this.mainWindow = mainWindow;
            this.InitializeComponent();
            this.InitializeImageList();
            this.init();
        }

        public VMViewPanal(VMFrame mainWindow, ArrayList selectedViewList)
        {
            this.mainWindow = mainWindow;
            this.InitializeComponent();
            this.InitializeImageList();
            try
            {
                this.init();
                this.AddSelectedOldView(selectedViewList);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void addLine(VMLine1 line)
        {
            this.dataDoc.AddLine(line);
        }

        public void addNode(VMNode1 node)
        {
            this.dataDoc.AddNode(node);
        }

        private void AddOldNode(VMNode1 node)
        {
            this.dataDoc.AddSelectedOldNode(node);
        }

        private void addPoint(int index, Point point)
        {
            if (this.tempLine != null)
            {
                this.tempLine.insetPoint(index, point);
            }
        }

        public void AddSelectedOldView(ArrayList selectedViewList)
        {
            if ((selectedViewList != null) && (selectedViewList.Count != 0))
            {
                for (int i = 0; i < selectedViewList.Count; i++)
                {
                    DEView view = (DEView) selectedViewList[i];
                    ArrayList list = new ArrayList();
                    list.AddRange(this.dataDoc.nodeList);
                    for (int j = 0; j < list.Count; j++)
                    {
                        VMNode1 node = (VMNode1) list[j];
                        Guid guid2 = new Guid(view.GetName());
                        if (guid2.Equals(node.OID))
                        {
                            MessageBox.Show("视图“" + view.Label + "”在视图模型中已经存在，不用再添加了！", "添加原有视图", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            selectedViewList.RemoveAt(i);
                            i--;
                            list.RemoveAt(j);
                            j--;
                            break;
                        }
                    }
                }
                if ((selectedViewList != null) && (selectedViewList.Count != 0))
                {
                    foreach (DEView view2 in selectedViewList)
                    {
                        Guid oID = new Guid(view2.GetName());
                        int x = new Random().Next(0, 500);
                        int y = new Random().Next(0, 400);
                        Point startPoint = new Point(x, y);
                        VMNode1 node2 = new VMNode1(oID, startPoint, view2.Label);
                        this.AddOldNode(node2);
                    }
                    PLViewModel model = new PLViewModel();
                    try
                    {
                        this.dataDoc.TheDataSet = model.FillOldViewInfoToDataSet(this.dataDoc.TheDataSet, selectedViewList);
                    }
                    catch (Exception)
                    {
                        throw new ViewException("添加原有视图失败！");
                    }
                }
            }
        }

        public void BuildMenu(string type, VMShape1 selectedShape)
        {
            this.cmuCommon.MenuItems.Clear();
            string str = type;
            if (str != null)
            {
                if (str != "Node")
                {
                    if (str != "Line")
                    {
                        if (str != "Text")
                        {
                            if (str != "Empty")
                            {
                                return;
                            }
                            ViewFrameMenuProcess process3 = new ViewFrameMenuProcess(this, (VMText1) selectedShape, ClientData.LogonUser);
                            int num = 0;
                            if (!((DEViewModel) this.mainWindow.Tag).Locker.Equals(Guid.Empty))
                            {
                                if (((DEViewModel) this.mainWindow.Tag).Locker.Equals(ClientData.LogonUser.Oid))
                                {
                                    num = 1;
                                }
                                else
                                {
                                    num = 2;
                                }
                            }
                            switch (num)
                            {
                                case 1:
                                    this.cmuCommon.MenuItems.AddRange(process3.BuildSelfEditMenuItems());
                                    return;

                                case 2:
                                    this.cmuCommon.MenuItems.AddRange(process3.BuildEditMenuItems());
                                    break;

                                case 0:
                                    if (((DEViewModel) this.mainWindow.Tag).IsActive == 'A')
                                    {
                                        this.cmuCommon.MenuItems.AddRange(process3.BuiltInitMenuItemOfActivedVM());
                                        return;
                                    }
                                    this.cmuCommon.MenuItems.AddRange(process3.BuiltInitMenuItemOfUnActivedVM());
                                    return;
                            }
                        }
                        return;
                    }
                }
                else
                {
                    ViewFrameMenuProcess process = new ViewFrameMenuProcess(this, (VMNode1) selectedShape, ClientData.LogonUser);
                    this.cmuCommon.MenuItems.AddRange(process.BuildViewMenuItems());
                    return;
                }
                ViewFrameMenuProcess process2 = new ViewFrameMenuProcess(this, (VMLine1) selectedShape, ClientData.LogonUser);
                this.cmuCommon.MenuItems.AddRange(process2.BuildViewRelationMenuItems());
            }
        }

        public void changeText()
        {
            if (this.wantInput)
            {
                if (this.inputBox.Text.Trim().Equals(""))
                {
                    MessageBox.Show("视图名称不允许为空！", "视图模型管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (this.inputBox.Text.Trim().Length > 100)
                {
                    MessageBox.Show("视图名称过长！\n请将其控制在100个字符以内！", "视图模型管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (!this.dataDoc.ChangeText((VMNode1) ((VMText1) this.selectedShape).objectShape, this.inputBox.Text.Trim()))
                {
                    this.inputBox.Text = ((VMText1) this.selectedShape).objectShape.text.caption;
                }
                this.inputBox.Visible = false;
                this.wantInput = false;
                this.Refresh();
            }
            this.mainWindow.Focus();
        }

        private Point[] checkPointInLine(Point point, ref int index, ref VMLine1 line){
          return  this.dataDoc.checkPointInLine(point, ref index, ref line);
        }

        private Rectangle createRec(Point p1, Point p2)
        {
            int x;
            int y;
            int num3;
            int num4;
            if (p1.X >= p2.X)
            {
                x = p2.X;
                num3 = p1.X - p2.X;
            }
            else
            {
                x = p1.X;
                num3 = p2.X - p1.X;
            }
            if (p1.Y >= p2.Y)
            {
                y = p2.Y;
                num4 = p1.Y - p2.Y;
            }
            else
            {
                y = p1.Y;
                num4 = p2.Y - p1.Y;
            }
            return new Rectangle(x, y, num3, num4);
        }

        private int deleteAssociatedLine(VMShape1 shape){
          return  this.dataDoc.DelAssociatedLine(shape);
        }

        private bool deleteLinePoint(VMLine1 line, Point point){ 
          return  line.deletePoint(point);
    }

        public bool deleteObject()
        {
            string str;
            if (((DEViewModel) this.mainWindow.Tag).Locker != ClientData.LogonUser.Oid)
            {
                return false;
            }
            if ((this.selectedShape != null) && ((str = this.selectedShape.GetType().Name.ToString()) != null))
            {
                if (str == "VMLine1")
                {
                    this.dataDoc.DelLine((VMLine1) this.selectedShape);
                    this.selectedShape = null;
                    this.Refresh();
                }
                else if (str == "VMNode1")
                {
                    try
                    {
                        this.dataDoc.RemoveNode((VMNode1) this.selectedShape);
                        this.selectedShape = null;
                        this.Refresh();
                    }
                    catch (ViewException exception)
                    {
                        MessageBox.Show(exception.Message, "删除视图节点", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
            }
            return true;
        }

        private void drawArrow(Graphics g, Pen myPen, Point p1, Point p2)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (p1.X == p2.X)
            {
                if (p1.Y != p2.Y)
                {
                    if (p1.Y < p2.Y)
                    {
                        num = p2.X - ((int) (this.arrowLong * Math.Sin(this.arrowAngle)));
                        num2 = p2.X + ((int) (this.arrowLong * Math.Sin(this.arrowAngle)));
                        num3 = num4 = p2.Y - ((int) (this.arrowLong * Math.Cos(this.arrowAngle)));
                        g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                        g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                    }
                    else
                    {
                        num = p2.X - ((int) (this.arrowLong * Math.Sin(this.arrowAngle)));
                        num2 = p2.X + ((int) (this.arrowLong * Math.Sin(this.arrowAngle)));
                        num3 = num4 = p2.Y + ((int) (this.arrowLong * Math.Cos(this.arrowAngle)));
                        g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                        g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                    }
                }
            }
            else
            {
                double num5;
                double num6;
                double num7;
                if (p1.X < p2.X)
                {
                    if (p1.Y == p2.Y)
                    {
                        num = num2 = p2.X - ((int) (this.arrowLong * Math.Sin(this.arrowAngle)));
                        num3 = p2.Y + ((int) (this.arrowLong * Math.Cos(this.arrowAngle)));
                        num4 = p2.Y + ((int) (this.arrowLong * Math.Cos(this.arrowAngle)));
                        g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                        g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                    }
                    else if (p1.Y < p2.Y)
                    {
                        num5 = ((double) (p2.Y - p1.Y)) / ((double) (p2.X - p1.X));
                        num6 = Math.Atan(num5);
                        num = p2.X - ((int) (this.arrowLong * Math.Cos(num6 - this.arrowAngle)));
                        num3 = p2.Y - ((int) (this.arrowLong * Math.Sin(num6 - this.arrowAngle)));
                        num7 = num6 + this.arrowAngle;
                        num2 = p2.X - ((int) (this.arrowLong * Math.Cos(num7)));
                        num4 = p2.Y - ((int) (this.arrowLong * Math.Sin(num7)));
                        g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                        g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                    }
                    else
                    {
                        num5 = ((double) (p1.Y - p2.Y)) / ((double) (p2.X - p1.X));
                        num6 = Math.Atan(num5);
                        num = p2.X - ((int) (this.arrowLong * Math.Cos(num6 - this.arrowAngle)));
                        num3 = p2.Y + ((int) (this.arrowLong * Math.Sin(num6 - this.arrowAngle)));
                        num7 = num6 + this.arrowAngle;
                        num2 = p2.X - ((int) (this.arrowLong * Math.Cos(num7)));
                        num4 = p2.Y + ((int) (this.arrowLong * Math.Sin(num7)));
                        g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                        g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                    }
                }
                else if (p1.Y == p2.Y)
                {
                    num = num2 = p2.X + ((int) (this.arrowLong * Math.Sin(this.arrowAngle)));
                    num3 = p2.Y + ((int) (this.arrowLong * Math.Cos(this.arrowAngle)));
                    num4 = p2.Y + ((int) (this.arrowLong * Math.Cos(this.arrowAngle)));
                    g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                    g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                }
                else if (p1.Y < p2.Y)
                {
                    num5 = ((double) (p2.Y - p1.Y)) / ((double) (p1.X - p2.X));
                    num6 = Math.Atan(num5);
                    num = p2.X + ((int) (this.arrowLong * Math.Cos(num6 - this.arrowAngle)));
                    num3 = p2.Y - ((int) (this.arrowLong * Math.Sin(num6 - this.arrowAngle)));
                    num7 = num6 + this.arrowAngle;
                    num2 = p2.X + ((int) (this.arrowLong * Math.Cos(num7)));
                    num4 = p2.Y - ((int) (this.arrowLong * Math.Sin(num7)));
                    g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                    g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                }
                else
                {
                    num5 = ((double) (p1.Y - p2.Y)) / ((double) (p1.X - p2.X));
                    num6 = Math.Atan(num5);
                    num = p2.X + ((int) (this.arrowLong * Math.Cos(num6 - this.arrowAngle)));
                    num3 = p2.Y + ((int) (this.arrowLong * Math.Sin(num6 - this.arrowAngle)));
                    num7 = num6 + this.arrowAngle;
                    num2 = p2.X + ((int) (this.arrowLong * Math.Cos(num7)));
                    num4 = p2.Y + ((int) (this.arrowLong * Math.Sin(num7)));
                    g.DrawLine(myPen, num, num3, p2.X, p2.Y);
                    g.DrawLine(myPen, num2, num4, p2.X, p2.Y);
                }
            }
        }

        private void drawShape(Graphics g)
        {
            for (int i = 0; i < this.dataDoc.nodeList.Count; i++)
            {
                ((VMShape1) this.dataDoc.nodeList[i]).draw(g);
            }
            for (int j = 0; j < this.dataDoc.lineList.Count; j++)
            {
                ((VMLine1) this.dataDoc.lineList[j]).draw(g);
            }
        }

        private ArrayList getMulSelect(Rectangle rec){
         return   this.dataDoc.getMulSelect(rec);
        }

        private void init()
        {
            this.modified = false;
            this.multiSelected = new ArrayList();
            this.selectedShape = null;
            this.tempLine = null;
            this.arrowLong = 10.0;
            this.arrowAngle = 0.52359877559829882;
            this.isCaptured = false;
            this.wantLine = false;
            this.breakLine = false;
            this.wantMoveShape = false;
            this.wantMovePoint = -1;
            this.wantInput = false;
            this.inputBox = new TextBox();
            this.turningPosition = -2;
            this.CursorFlag = true;
            this.RecFlag = false;
            this.ReserveFlag = false;
            this.CursorDown = false;
            this.RecDown = false;
            this.ReserveDown = false;
            this.myPen = new Pen(Color.Black);
            this.dataDoc = new VMDoc1((DEViewModel) this.mainWindow.Tag);
            this.BackColor = SystemColors.Window;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this.inputBox.KeyDown += new KeyEventHandler(this.inputBox_KeyDown);
        }


        private void InitializeImageList()
        {
            ClientData.MyImageList.AddIcon("ICO_VIW_EXIT");
            ClientData.MyImageList.AddIcon("ICO_VIW_SAVE");
        }

        private bool inMulSel(Point point)
        {
            for (int i = 0; i < this.multiSelected.Count; i++)
            {
                if (((VMShape1) this.multiSelected[i]).isInMe(point))
                {
                    return true;
                }
            }
            return false;
        }

        private bool inMulSel(VMShape1 nodeShape) {
         return   this.multiSelected.Contains(nodeShape);
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.changeText();
            }
        }

        private VMShape1 inShape(Point point) {
         return   this.dataDoc.checkInShape(point);
        }

        public bool isNewAddedNode(Guid nodeOid)
        {
            DataTable table = this.dataDoc.TheDataSet.Tables["PLM_ADM_VIEW"];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i].RowState == DataRowState.Added)
                {
                    Guid guid = new Guid((byte[]) table.Rows[i]["PLM_OID"]);
                    if (guid.Equals(nodeOid))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void moveMulShape(Point p1, Point p2)
        {
            for (int i = 0; i < this.multiSelected.Count; i++)
            {
                ((VMNode1) this.multiSelected[i]).moveShape(p1, p2);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.g = e.Graphics;
            if (this.wantLine)
            {
                if (this.isCaptured)
                {
                    this.g.DrawLine(this.myPen, this.moveBase, this.mousePosition);
                    this.drawArrow(this.g, this.myPen, this.moveBase, this.mousePosition);
                }
            }
            else if (this.turningPosition != -2)
            {
                if (this.isCaptured)
                {
                    this.g.DrawLine(this.myPen, this.tempLineStart, this.mousePosition);
                    this.g.DrawLine(this.myPen, this.tempLineEnd, this.mousePosition);
                }
            }
            else if (this.wantMulSelect)
            {
                Pen pen = new Pen(Color.Black) {
                    DashStyle = DashStyle.Dash
                };
                e.Graphics.DrawRectangle(pen, this.mulSelRec);
            }
            this.drawShape(this.g);
        }

        private void releaseMulSelected()
        {
            int num = this.multiSelected.Count - 1;
            for (int i = num; i >= 0; i--)
            {
                ((VMNode1) this.multiSelected[i]).isSelected = false;
                this.multiSelected.RemoveAt(i);
            }
        }

        public void saveFile()
        {
            this.dataDoc.SaveDSToDB();
            this.hasSaved = true;
        }

        public void setModified(bool index)
        {
            if (index)
            {
                this.modified = true;
            }
            else
            {
                this.modified = false;
            }
        }

        private void setMulSelectSymbol()
        {
            for (int i = 0; i < this.multiSelected.Count; i++)
            {
                ((VMNode1) this.multiSelected[i]).isSelected = true;
            }
        }

        public void showInputBox(VMText1 nodeText)
        {
            this.inputBox.Width = nodeText.width;
            this.inputBox.Height = nodeText.height;
            base.Controls.Add(this.inputBox);
            this.inputBox.Text = nodeText.caption;
            this.inputBox.Location = nodeText.startPoint;
            this.inputBox.Show();
            this.inputBox.Focus();
        }

        private void VMView_Click(object sender, EventArgs e)
        {
            if (((DEViewModel) this.mainWindow.Tag).Locker == ClientData.LogonUser.Oid)
            {
                this.changeText();
                this.modified = true;
            }
        }

        private void VMView_DoubleClick(object sender, EventArgs e)
        {
            if (this.selectedShape != null)
            {
                if (this.selectedShape.GetType().Name.Equals("VMLine1"))
                {
                    if ((((DEViewModel) this.mainWindow.Tag).Locker == ClientData.LogonUser.Oid) && !this.deleteLinePoint((VMLine1) this.selectedShape, this.mousePosition))
                    {
                        if (this.breakLine)
                        {
                            this.breakLine = false;
                            this.turningPosition = -2;
                        }
                        else
                        {
                            Point[] pointArray = this.checkPointInLine(this.mousePosition, ref this.turningPosition, ref this.tempLine);
                            if (pointArray != null)
                            {
                                this.tempLineStart = pointArray[0];
                                this.tempLineEnd = pointArray[1];
                                this.breakLine = true;
                            }
                        }
                    }
                }
                else if (this.selectedShape.GetType().Name.Equals("VMText1"))
                {
                    if (((DEViewModel) this.mainWindow.Tag).Locker == ClientData.LogonUser.Oid)
                    {
                        this.wantInput = true;
                        this.showInputBox((VMText1) this.selectedShape);
                    }
                }
                else if (this.selectedShape.GetType().Name.Equals("VMNode1"))
                {
                    bool isInEdit = false;
                    if (((DEViewModel) this.mainWindow.Tag).Locker == ClientData.LogonUser.Oid)
                    {
                        isInEdit = true;
                    }
                    bool isNew = false;
                    if (this.isNewAddedNode(((VMNode1) this.selectedShape).OID))
                    {
                        isNew = true;
                    }
                    new FrmNodeProperty((VMNode1) this.selectedShape, this.dataDoc, isInEdit, isNew).ShowDialog(this);
                }
            }
        }

        private void VMView_MouseDown(object sender, MouseEventArgs e)
        {
            base.Focus();
            this.mousePosition = new Point(e.X, e.Y);
            VMShape1 selectedShape = this.whichSelected(this.mousePosition, ref this.wantLine, ref this.wantMoveShape);
            if (e.Button == MouseButtons.Left)
            {
                this.isCaptured = true;
                this.moveBase = this.mousePosition;
                this.mulSelRec = this.createRec(this.moveBase, this.mousePosition);
                if (!this.wantInput)
                {
                    if (selectedShape != null)
                    {
                        if (this.multiSelected.Count > 0)
                        {
                            if (!this.inMulSel(this.mousePosition))
                            {
                                this.releaseMulSelected();
                                if ((this.selectedShape != null) && !this.inMulSel(this.selectedShape))
                                {
                                    this.selectedShape.isSelected = false;
                                }
                                this.selectedShape = selectedShape;
                                this.selectedShape.isSelected = true;
                            }
                            this.wantLine = false;
                        }
                        else
                        {
                            if (this.selectedShape != null)
                            {
                                this.selectedShape.isSelected = false;
                            }
                            this.selectedShape = selectedShape;
                            this.selectedShape.isSelected = true;
                        }
                        if (this.selectedShape != null)
                        {
                            this.wantMovePoint = this.selectedShape.isPointInSymbol(this.mousePosition);
                        }
                        if (this.wantLine)
                        {
                            this.startShape = this.selectedShape;
                        }
                    }
                    else if (this.multiSelected.Count > 0)
                    {
                        this.releaseMulSelected();
                        this.wantMulSelect = true;
                    }
                    else
                    {
                        this.wantMulSelect = true;
                        if (this.selectedShape != null)
                        {
                            this.selectedShape.isSelected = false;
                            this.selectedShape = null;
                        }
                        else
                        {
                            this.wantMulSelect = true;
                        }
                    }
                    this.Refresh();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                string type = "";
                if (selectedShape is VMNode1)
                {
                    this.selectedShape = selectedShape;
                    type = "Node";
                }
                else if (selectedShape is VMLine1)
                {
                    this.selectedShape = selectedShape;
                    type = "Line";
                }
                else if (selectedShape is VMText1)
                {
                    type = "Text";
                }
                else
                {
                    type = "Empty";
                }
                this.BuildMenu(type, selectedShape);
                if (this.cmuCommon.MenuItems.Count > 0)
                {
                    this.cmuCommon.Show(this, this.mousePosition);
                }
            }
        }

        private void VMView_MouseMove(object sender, MouseEventArgs e)
        {
            if (((DEViewModel) this.mainWindow.Tag).Locker == ClientData.LogonUser.Oid)
            {
                this.mousePosition = new Point(e.X, e.Y);
                if (!this.wantInput && this.isCaptured)
                {
                    if (this.multiSelected.Count > 0)
                    {
                        this.moveMulShape(this.moveBase, this.mousePosition);
                        this.moveBase = this.mousePosition;
                        this.Refresh();
                    }
                    else
                    {
                        if (this.selectedShape != null)
                        {
                            if (this.wantMoveShape && (this.selectedShape != null))
                            {
                                this.selectedShape.moveShape(this.moveBase, this.mousePosition);
                                this.moveBase = this.mousePosition;
                            }
                            this.Refresh();
                        }
                        if (this.wantLine)
                        {
                            if (this.isCaptured)
                            {
                                this.Refresh();
                            }
                        }
                        else if (this.turningPosition != -2)
                        {
                            if (this.isCaptured)
                            {
                                this.Refresh();
                            }
                        }
                        else if (this.wantMovePoint != -1)
                        {
                            if (this.isCaptured && (this.selectedShape != null))
                            {
                                this.selectedShape.movePoint(this.wantMovePoint, this.moveBase, this.mousePosition);
                                this.moveBase = this.mousePosition;
                                this.Refresh();
                            }
                        }
                        else
                        {
                            this.mulSelRec = this.createRec(this.moveBase, this.mousePosition);
                            this.Refresh();
                        }
                    }
                }
            }
        }

        private void VMView_MouseUp(object sender, MouseEventArgs e)
        {
            this.mousePosition = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                this.isCaptured = false;
                if (this.wantLine)
                {
                    this.endShape = this.inShape(this.mousePosition);
                    if (((this.endShape != null) && !(this.endShape is VMText1)) && (this.startShape != this.endShape))
                    {
                        ErrorDetective1 detective = new ErrorDetective1(this.dataDoc, this.startShape, this.endShape);
                        if (detective.ErrorFree())
                        {
                            VMLine1 line = new VMLine1(Guid.NewGuid(), this.startShape, this.endShape);
                            this.addLine(line);
                        }
                        else
                        {
                            MessageBox.Show("逻辑错误！！！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        this.startShape = null;
                        this.endShape = null;
                    }
                    this.wantLine = false;
                    this.moveBase = this.mousePosition;
                }
                else if (this.RecFlag)
                {
                    VMNode1 node = new VMNode1(Guid.NewGuid(), this.mousePosition);
                    if (this.selectedShape != null)
                    {
                        this.selectedShape.isSelected = false;
                    }
                    this.selectedShape = node;
                    this.addNode(node);
                    this.RecFlag = false;
                    this.mainWindow.controlTBButton(TagForViewWork.TOOLBAR_REC);
                    this.moveBase = this.mousePosition;
                }
                else if (this.breakLine && (this.turningPosition != -2))
                {
                    this.addPoint(this.turningPosition, this.mousePosition);
                    this.breakLine = false;
                    this.turningPosition = -2;
                    this.moveBase = this.mousePosition;
                }
                else if (this.wantMulSelect)
                {
                    Rectangle rec = this.createRec(this.moveBase, this.mousePosition);
                    this.multiSelected = this.getMulSelect(rec);
                    this.setMulSelectSymbol();
                    if (this.multiSelected.Count == 1)
                    {
                        this.selectedShape = (VMNode1) this.multiSelected[0];
                        this.multiSelected.RemoveAt(0);
                    }
                    this.wantMulSelect = false;
                    this.moveBase = this.mousePosition;
                    this.mulSelRec = this.createRec(this.moveBase, this.mousePosition);
                }
            }
            this.wantMovePoint = -1;
            this.Refresh();
        }

        private VMShape1 whichSelected(Point point, ref bool wantLine, ref bool wantMoveShape) {
         return   this.dataDoc.checkSelection(point, ref wantLine, ref wantMoveShape);
        }
    }
}

