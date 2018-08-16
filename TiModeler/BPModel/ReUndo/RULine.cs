namespace Thyt.TiPLM.CLT.TiModeler.BPModel.ReUndo
{
    using System;
    using System.Drawing;
    using Thyt.TiPLM.CLT.Admin.BPM.Modeler;
    using Thyt.TiPLM.CLT.TiModeler.BPModel;

    [Serializable]
    public class RULine : RUObject
    {
        private Line currentLine;
        private int index;
        private WFTEditor mainWindow;
        private string operate;
        private Point pointFinal;
        private Point pointOriginal;
        private Point thePoint;
        private int wantMovePoint;

        public RULine(WFTEditor wftEditor, Line theLine, string operateType)
        {
            this.mainWindow = wftEditor;
            this.currentLine = theLine;
            this.operate = operateType;
        }

        public RULine(WFTEditor wftEditor, Line theLine, int wantMovePoint, Point original, Point final)
        {
            this.mainWindow = wftEditor;
            this.currentLine = theLine;
            this.operate = "OP_Move";
            this.wantMovePoint = wantMovePoint;
            this.pointOriginal = original;
            this.pointFinal = final;
        }

        public RULine(WFTEditor wftEditor, Line theLine, int i, Point p, string operateType)
        {
            this.mainWindow = wftEditor;
            this.currentLine = theLine;
            this.operate = operateType;
            this.index = i;
            this.thePoint = p;
            if (operateType == "OP_ADD_POINT")
            {
                this.index = this.currentLine.findPointPosition(p);
            }
        }

        private void addLine()
        {
            this.mainWindow.viewPanel.addLine(this.currentLine);
            if (this.mainWindow.arrayDeleteObject.Contains(this.currentLine))
            {
                this.mainWindow.arrayDeleteObject.Remove(this.currentLine);
            }
            if (this.mainWindow.viewPanel.selectedShape != null)
            {
                this.mainWindow.viewPanel.selectedShape.isSelected = false;
            }
            this.currentLine.isSelected = true;
            this.mainWindow.viewPanel.selectedShape = this.currentLine;
        }

        private void addPoint(int i, Point p)
        {
            this.currentLine.insetPoint(i, p);
        }

        private void deleteLine()
        {
            if (this.currentLine.isSelected)
            {
                this.currentLine.isSelected = false;
                this.mainWindow.viewPanel.selectedShape = null;
            }
            this.mainWindow.viewPanel.shapeData.root.linAry.Remove(this.currentLine);
            this.mainWindow.arrayDeleteObject.Add(this.currentLine);
        }

        private void deletePoint(int i, Point p)
        {
            this.currentLine.deletePoint(p);
        }

        private void movePointOnline(int wantMovePoint, Point p)
        {
            this.currentLine.pointsList[wantMovePoint] = p;
            if (this.mainWindow.viewPanel.selectedShape != null)
            {
                this.mainWindow.viewPanel.selectedShape.isSelected = false;
            }
            this.currentLine.isSelected = true;
            this.mainWindow.viewPanel.selectedShape = this.currentLine;
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
                            this.movePointOnline(this.wantMovePoint, this.pointFinal);
                            return;
                        }
                        if (operate == "OP_ADD_POINT")
                        {
                            this.addPoint(this.index, this.thePoint);
                            return;
                        }
                        if (operate == "OP_DELETE_POINT")
                        {
                            this.deletePoint(this.index, this.thePoint);
                        }
                        return;
                    }
                }
                else
                {
                    this.addLine();
                    return;
                }
                this.deleteLine();
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
                            this.movePointOnline(this.wantMovePoint, this.pointOriginal);
                            return;
                        }
                        if (operate == "OP_ADD_POINT")
                        {
                            this.deletePoint(this.index, this.thePoint);
                            return;
                        }
                        if (operate == "OP_DELETE_POINT")
                        {
                            this.addPoint(this.index, this.thePoint);
                        }
                        return;
                    }
                }
                else
                {
                    this.deleteLine();
                    return;
                }
                this.addLine();
            }
        }
    }
}

