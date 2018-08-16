namespace Thyt.TiPLM.CLT.TiModeler.BPModel.ReUndo
{
    using System;
    using System.Collections;
    using System.Drawing;
    using Thyt.TiPLM.CLT.Admin.BPM.Modeler;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.CLT.TiModeler.BPModel;

    [Serializable]
    internal class RUMutiShape : RUObject
    {
        private ArrayList currentShapeList;
        private WFTEditor mainWindow;
        private Point moveBaseFinal;
        private Point moveBaseOriginal;
        private string operate;

        public RUMutiShape(WFTEditor wftEditor, ArrayList selectedShapeList, string operateType)
        {
            this.mainWindow = wftEditor;
            this.currentShapeList = new ArrayList();
            for (int i = 0; i < selectedShapeList.Count; i++)
            {
                if (!this.currentShapeList.Contains(selectedShapeList[i]))
                {
                    this.currentShapeList.Add(selectedShapeList[i]);
                }
                if ((selectedShapeList[i].GetType().Name.ToString() == "RouteNode") || (selectedShapeList[i].GetType().Name.ToString() == "TaskNode"))
                {
                    foreach (object obj2 in this.mainWindow.viewPanel.shapeData.FindAssociatedLine((Shape) selectedShapeList[i]))
                    {
                        if (!selectedShapeList.Contains(obj2))
                        {
                            this.currentShapeList.Add(obj2);
                        }
                    }
                }
            }
            this.operate = operateType;
        }

        public RUMutiShape(WFTEditor wftEditor, ArrayList selectedShapeList, Point moveBaseOriginal, Point moveBaseFinal)
        {
            this.mainWindow = wftEditor;
            this.currentShapeList = new ArrayList();
            for (int i = 0; i < selectedShapeList.Count; i++)
            {
                this.currentShapeList.Add(selectedShapeList[i]);
            }
            this.operate = "OP_Move";
            this.moveBaseOriginal = moveBaseOriginal;
            this.moveBaseFinal = moveBaseFinal;
        }

        private void addMutiShape()
        {
            if (this.mainWindow.viewPanel.selectedShape != null)
            {
                this.mainWindow.viewPanel.selectedShape.isSelected = false;
                this.mainWindow.viewPanel.selectedShape = null;
            }
            else
            {
                for (int j = 0; j < this.mainWindow.viewPanel.selectedShapeList.Count; j++)
                {
                    ((Shape) this.mainWindow.viewPanel.selectedShapeList[j]).isSelected = false;
                }
                this.mainWindow.viewPanel.selectedShapeList.Clear();
            }
            for (int i = 0; i < this.currentShapeList.Count; i++)
            {
                if ((this.currentShapeList[i].GetType().Name.ToString() != "Line") && (this.currentShapeList[i].GetType().Name.ToString() != "NodeText"))
                {
                    foreach (object obj2 in this.mainWindow.viewPanel.shapeData.FindAssociatedLine((Shape) this.currentShapeList[i]))
                    {
                        if (this.mainWindow.arrayDeleteObject.Contains(obj2))
                        {
                            this.mainWindow.viewPanel.addLine((Line) obj2);
                            this.mainWindow.arrayDeleteObject.Remove(obj2);
                        }
                    }
                    if (this.currentShapeList[i].GetType().Name.ToString() == "TaskNode")
                    {
                        this.mainWindow.viewPanel.addTaskNode((Shape) this.currentShapeList[i]);
                    }
                    else if (this.currentShapeList[i].GetType().Name.ToString() == "RouteNode")
                    {
                        this.mainWindow.viewPanel.addRouteNode((Shape) this.currentShapeList[i]);
                    }
                    ((FrmMain) this.mainWindow.MdiParent).AddTreeNode(this.mainWindow, (Shape) this.currentShapeList[i]);
                }
                else if ((this.currentShapeList[i].GetType().Name.ToString() == "Line") && !this.mainWindow.viewPanel.shapeData.root.linAry.Contains(this.currentShapeList[i]))
                {
                    this.mainWindow.viewPanel.addLine((Shape) this.currentShapeList[i]);
                }
                if (this.mainWindow.arrayDeleteObject.Contains(this.currentShapeList[i]))
                {
                    this.mainWindow.arrayDeleteObject.Remove(this.currentShapeList[i]);
                }
                if (this.currentShapeList[i].GetType().Name.ToString() == "Line")
                {
                    if (this.currentShapeList.Contains(((Line) this.currentShapeList[i]).startShape) && this.currentShapeList.Contains(((Line) this.currentShapeList[i]).endShape))
                    {
                        this.mainWindow.viewPanel.selectedShapeList.Add(this.currentShapeList[i]);
                        ((Shape) this.currentShapeList[i]).isSelected = true;
                    }
                }
                else
                {
                    this.mainWindow.viewPanel.selectedShapeList.Add(this.currentShapeList[i]);
                    ((Shape) this.currentShapeList[i]).isSelected = true;
                }
            }
        }

        private void deleteMutiShape()
        {
            for (int i = 0; i < this.mainWindow.viewPanel.selectedShapeList.Count; i++)
            {
                ((Shape) this.mainWindow.viewPanel.selectedShapeList[i]).isSelected = false;
            }
            this.mainWindow.viewPanel.selectedShapeList.Clear();
            if (this.mainWindow.viewPanel.selectedShape != null)
            {
                this.mainWindow.viewPanel.selectedShape.isSelected = false;
                this.mainWindow.viewPanel.selectedShape = null;
            }
            for (int j = 0; j < this.currentShapeList.Count; j++)
            {
                if (this.currentShapeList[j].GetType().Name.ToString() == "Line")
                {
                    if (this.mainWindow.viewPanel.shapeData.root.linAry.Contains(this.currentShapeList[j]))
                    {
                        this.mainWindow.arrayDeleteObject.Add(this.currentShapeList[j]);
                        this.mainWindow.viewPanel.shapeData.root.linAry.Remove(this.currentShapeList[j]);
                    }
                }
                else if (this.currentShapeList[j].GetType().Name.ToString() != "NodeText")
                {
                    if (!((Shape) this.currentShapeList[j]).isSelected)
                    {
                        ((Shape) this.currentShapeList[j]).isSelected = true;
                    }
                    this.mainWindow.viewPanel.selectedShape = (Shape) this.currentShapeList[j];
                    this.mainWindow.viewPanel.setMainFrmTreeSelected();
                    ((FrmMain) this.mainWindow.MdiParent).DelTreeNodeByShape((Shape) this.currentShapeList[j]);
                    this.mainWindow.viewPanel.deleteAssociatedLine((Shape) this.currentShapeList[j]);
                    this.mainWindow.viewPanel.shapeData.root.textNodAry.Remove(((Shape) this.currentShapeList[j]).text);
                    if (this.currentShapeList[j].GetType().Name.ToString() == "RouteNode")
                    {
                        this.mainWindow.viewPanel.shapeData.root.routeNodAry.Remove(this.currentShapeList[j]);
                    }
                    else if (this.currentShapeList[j].GetType().Name.ToString() == "TaskNode")
                    {
                        this.mainWindow.viewPanel.shapeData.root.taskNodAry.Remove(this.currentShapeList[j]);
                    }
                    this.mainWindow.arrayDeleteObject.Add((Shape) this.currentShapeList[j]);
                    ((Shape) this.currentShapeList[j]).isSelected = false;
                    this.mainWindow.viewPanel.selectedShape = null;
                }
            }
            this.mainWindow.viewPanel.setMainFrmTreeSelected();
        }

        private void moveShape(Point position1, Point position2)
        {
            if (this.mainWindow.viewPanel.selectedShape != null)
            {
                this.mainWindow.viewPanel.selectedShape.isSelected = false;
            }
            if (this.mainWindow.viewPanel.selectedShapeList.Count > 0)
            {
                for (int k = 0; k < this.mainWindow.viewPanel.selectedShapeList.Count; k++)
                {
                    ((Shape) this.mainWindow.viewPanel.selectedShapeList[k]).isSelected = false;
                }
                this.mainWindow.viewPanel.selectedShapeList.Clear();
            }
            for (int i = 0; i < this.currentShapeList.Count; i++)
            {
                ((Shape) this.currentShapeList[i]).isSelected = true;
                this.mainWindow.viewPanel.selectedShapeList.Add(this.currentShapeList[i]);
            }
            for (int j = 0; j < this.currentShapeList.Count; j++)
            {
                ((Shape) this.currentShapeList[j]).moveShape(position1, position2);
            }
            this.mainWindow.viewPanel.setMainFrmTreeSelected();
        }

        public override void Redo()
        {
            string operate = this.operate;
            if (operate != null)
            {
                if (operate != "OP_Add")
                {
                    if (operate != "OP_Delete")
                    {
                        if (operate == "OP_Move")
                        {
                            this.moveShape(this.moveBaseOriginal, this.moveBaseFinal);
                        }
                        return;
                    }
                }
                else
                {
                    this.addMutiShape();
                    return;
                }
                this.deleteMutiShape();
            }
        }

        public override void Undo()
        {
            string operate = this.operate;
            if (operate != null)
            {
                if (operate != "OP_Add")
                {
                    if (operate != "OP_Delete")
                    {
                        if (operate == "OP_Move")
                        {
                            this.moveShape(this.moveBaseFinal, this.moveBaseOriginal);
                        }
                        return;
                    }
                }
                else
                {
                    this.deleteMutiShape();
                    return;
                }
                this.addMutiShape();
            }
        }
    }
}

