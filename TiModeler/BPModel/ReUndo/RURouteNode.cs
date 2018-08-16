namespace Thyt.TiPLM.CLT.TiModeler.BPModel.ReUndo
{
    using System;
    using System.Collections;
    using System.Drawing;
    using Thyt.TiPLM.CLT.Admin.BPM.Modeler;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.CLT.TiModeler.BPModel;

    [Serializable]
    public class RURouteNode : RUObject
    {
        private ArrayList associateLines;
        private RouteNode currentRouteNode;
        private WFTEditor mainWindow;
        private Point moveBaseFinal;
        private Point moveBaseOriginal;
        private string operate;

        public RURouteNode(WFTEditor wftEditor, RouteNode selectedRouteNode, string operateType)
        {
            this.mainWindow = wftEditor;
            this.currentRouteNode = selectedRouteNode;
            this.operate = operateType;
            this.associateLines = wftEditor.viewPanel.shapeData.FindAssociatedLine(selectedRouteNode);
        }

        public RURouteNode(WFTEditor wftEditor, RouteNode selectedRouteNode, Point moveBaseOriginal, Point moveBaseFinal)
        {
            this.mainWindow = wftEditor;
            this.currentRouteNode = selectedRouteNode;
            this.operate = "OP_Move";
            this.moveBaseOriginal = moveBaseOriginal;
            this.moveBaseFinal = moveBaseFinal;
        }

        private void addRouteNode()
        {
            foreach (object obj2 in this.associateLines)
            {
                this.mainWindow.viewPanel.addLine((Line) obj2);
                if (this.mainWindow.arrayDeleteObject.Contains(obj2))
                {
                    this.mainWindow.arrayDeleteObject.Remove(obj2);
                }
            }
            this.mainWindow.viewPanel.addRouteNode(this.currentRouteNode);
            if (this.mainWindow.arrayDeleteObject.Contains(this.currentRouteNode))
            {
                this.mainWindow.arrayDeleteObject.Remove(this.currentRouteNode);
            }
            if (this.mainWindow.viewPanel.selectedShape != null)
            {
                this.mainWindow.viewPanel.selectedShape.isSelected = false;
            }
            ((FrmMain) this.mainWindow.MdiParent).AddTreeNode(this.mainWindow, this.currentRouteNode);
            this.currentRouteNode.isSelected = true;
            this.mainWindow.viewPanel.selectedShape = this.currentRouteNode;
            this.mainWindow.viewPanel.setMainFrmTreeSelected();
        }

        private void deleteRouteNode()
        {
            if (!this.currentRouteNode.isSelected)
            {
                this.currentRouteNode.isSelected = true;
                this.mainWindow.viewPanel.selectedShape = this.currentRouteNode;
                this.mainWindow.viewPanel.setMainFrmTreeSelected();
            }
            ((FrmMain) this.mainWindow.MdiParent).DelTreeNodeByShape(this.currentRouteNode);
            this.mainWindow.viewPanel.deleteAssociatedLine(this.currentRouteNode);
            this.mainWindow.viewPanel.shapeData.root.textNodAry.Remove(this.currentRouteNode.text);
            this.mainWindow.viewPanel.shapeData.root.routeNodAry.Remove(this.currentRouteNode);
            this.mainWindow.arrayDeleteObject.Add(this.currentRouteNode);
            this.currentRouteNode.isSelected = false;
            this.mainWindow.viewPanel.selectedShape = null;
            this.mainWindow.viewPanel.setMainFrmTreeSelected();
        }

        private void moveRouteNode(Point position1, Point position2)
        {
            this.currentRouteNode.moveShape(position1, position2);
            if (this.mainWindow.viewPanel.selectedShape != null)
            {
                this.mainWindow.viewPanel.selectedShape.isSelected = false;
            }
            this.currentRouteNode.isSelected = true;
            this.mainWindow.viewPanel.selectedShape = this.currentRouteNode;
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
                            this.moveRouteNode(this.moveBaseOriginal, this.moveBaseFinal);
                        }
                        return;
                    }
                }
                else
                {
                    this.addRouteNode();
                    return;
                }
                this.deleteRouteNode();
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
                            this.moveRouteNode(this.moveBaseFinal, this.moveBaseOriginal);
                        }
                        return;
                    }
                }
                else
                {
                    this.deleteRouteNode();
                    return;
                }
                this.addRouteNode();
            }
        }
    }
}

