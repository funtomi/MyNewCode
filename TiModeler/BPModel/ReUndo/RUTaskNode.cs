namespace Thyt.TiPLM.CLT.TiModeler.BPModel.ReUndo
{
    using System;
    using System.Collections;
    using System.Drawing;
    using Thyt.TiPLM.CLT.Admin.BPM.Modeler;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.CLT.TiModeler.BPModel;

    [Serializable]
    public class RUTaskNode : RUObject
    {
        private ArrayList associateLines;
        private TaskNode currentTaskNode;
        private WFTEditor mainWindow;
        private Point moveBaseFinal;
        private Point moveBaseOriginal;
        private string operate;

        public RUTaskNode(WFTEditor wftEditor, TaskNode selectedTaskNode, string operateType)
        {
            this.mainWindow = wftEditor;
            this.currentTaskNode = selectedTaskNode;
            this.operate = operateType;
            this.associateLines = wftEditor.viewPanel.shapeData.FindAssociatedLine(selectedTaskNode);
        }

        public RUTaskNode(WFTEditor wftEditor, TaskNode selectedTaskNode, Point moveBaseOriginal, Point moveBaseFinal)
        {
            this.mainWindow = wftEditor;
            this.currentTaskNode = selectedTaskNode;
            this.operate = "OP_Move";
            this.moveBaseOriginal = moveBaseOriginal;
            this.moveBaseFinal = moveBaseFinal;
        }

        private void addTasknode()
        {
            foreach (object obj2 in this.associateLines)
            {
                this.mainWindow.viewPanel.addLine((Line) obj2);
                if (this.mainWindow.arrayDeleteObject.Contains(obj2))
                {
                    this.mainWindow.arrayDeleteObject.Remove(obj2);
                }
            }
            this.mainWindow.viewPanel.addTaskNode(this.currentTaskNode);
            if (this.mainWindow.arrayDeleteObject.Contains(this.currentTaskNode))
            {
                this.mainWindow.arrayDeleteObject.Remove(this.currentTaskNode);
            }
            if (this.mainWindow.viewPanel.selectedShape != null)
            {
                this.mainWindow.viewPanel.selectedShape.isSelected = false;
            }
            ((FrmMain) this.mainWindow.MdiParent).AddTreeNode(this.mainWindow, this.currentTaskNode);
            this.currentTaskNode.isSelected = true;
            this.mainWindow.viewPanel.selectedShape = this.currentTaskNode;
            this.mainWindow.viewPanel.setMainFrmTreeSelected();
        }

        private void deleteTaskNode()
        {
            if (!this.currentTaskNode.isSelected)
            {
                this.currentTaskNode.isSelected = true;
                this.mainWindow.viewPanel.selectedShape = this.currentTaskNode;
                this.mainWindow.viewPanel.setMainFrmTreeSelected();
            }
            ((FrmMain) this.mainWindow.MdiParent).DelTreeNodeByShape(this.currentTaskNode);
            this.mainWindow.viewPanel.deleteAssociatedLine(this.currentTaskNode);
            this.mainWindow.viewPanel.shapeData.root.textNodAry.Remove(this.currentTaskNode.text);
            this.mainWindow.viewPanel.shapeData.root.taskNodAry.Remove(this.currentTaskNode);
            this.mainWindow.arrayDeleteObject.Add(this.currentTaskNode);
            this.currentTaskNode.isSelected = false;
            this.mainWindow.viewPanel.selectedShape = null;
            this.mainWindow.viewPanel.setMainFrmTreeSelected();
        }

        private void moveTaskNode(Point position1, Point position2)
        {
            this.currentTaskNode.moveShape(position1, position2);
            if (this.mainWindow.viewPanel.selectedShape != null)
            {
                this.mainWindow.viewPanel.selectedShape.isSelected = false;
            }
            this.currentTaskNode.isSelected = true;
            this.mainWindow.viewPanel.selectedShape = this.currentTaskNode;
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
                            this.moveTaskNode(this.moveBaseOriginal, this.moveBaseFinal);
                        }
                        return;
                    }
                }
                else
                {
                    this.addTasknode();
                    return;
                }
                this.deleteTaskNode();
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
                            this.moveTaskNode(this.moveBaseFinal, this.moveBaseOriginal);
                        }
                        return;
                    }
                }
                else
                {
                    this.deleteTaskNode();
                    return;
                }
                this.addTasknode();
            }
        }
    }
}

