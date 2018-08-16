namespace Thyt.TiPLM.CLT.TiModeler.BPModel.ReUndo
{
    using System;
    using System.Drawing;
    using Thyt.TiPLM.CLT.Admin.BPM.Modeler;
    using Thyt.TiPLM.CLT.TiModeler.BPModel;

    [Serializable]
    public class RUSENode : RUObject
    {
        private Shape currentShape;
        private WFTEditor mainWindow;
        private Point moveBaseFinal;
        private Point moveBaseOriginal;
        private string operate;

        public RUSENode(WFTEditor wftEditor, Shape selectedShape, Point moveBaseOriginal, Point moveBaseFinal)
        {
            this.mainWindow = wftEditor;
            this.currentShape = selectedShape;
            this.operate = "OP_Move";
            this.moveBaseOriginal = moveBaseOriginal;
            this.moveBaseFinal = moveBaseFinal;
        }

        private void moveShape(Point position1, Point position2)
        {
            this.currentShape.moveShape(position1, position2);
            if (this.mainWindow.viewPanel.selectedShape != null)
            {
                this.mainWindow.viewPanel.selectedShape.isSelected = false;
            }
            this.currentShape.isSelected = true;
            this.mainWindow.viewPanel.selectedShape = this.currentShape;
            this.mainWindow.viewPanel.setMainFrmTreeSelected();
        }

        public override void Redo()
        {
            string str;
            if (((str = this.operate) != null) && (str == "OP_Move"))
            {
                this.moveShape(this.moveBaseOriginal, this.moveBaseFinal);
            }
        }

        public override void Undo()
        {
            string str;
            if (((str = this.operate) != null) && (str == "OP_Move"))
            {
                this.moveShape(this.moveBaseFinal, this.moveBaseOriginal);
            }
        }
    }
}

