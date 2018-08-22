using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Admin.DataModel;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.Admin.NewResponsibility;
using Thyt.TiPLM.PLL.Product2;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Resource.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class PropertyPanel : UserControl {
        public DEBusinessItem Item;
        private DEMetaClassEx meta;
        private Guid uOid;

        public PropertyPanel() {
            this.InitializeComponent();
        }

        public PropertyPanel(DEBusinessItem item, DEMetaClassEx meta, Guid uOid, bool readOnly) {
            this.InitializeComponent();
            this.UpdateUI(item, meta, uOid, readOnly);
        }

        public bool UpdateData(bool updateIteration) {
            try {
                foreach (Control control in this.pnlProperty.Controls) {
                    if (typeof(TextBox).IsInstanceOfType(control) && !((TextBox)control).ReadOnly) {
                        DEMetaAttribute tag = (DEMetaAttribute)control.Tag;
                        object attrValue = PSConvert.String2Attribute(((TextBox)control).Text, tag);
                        if (!tag.NullAllowed && ((attrValue == null) || attrValue.Equals(""))) {
                            MessageBox.Show(tag.Label + "不能为空。", "输入提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            control.Focus();
                            return false;
                        }
                        this.Item.Iteration.SetAttrValue(tag.Name, attrValue);
                    }
                }
                if (updateIteration) {
                    this.Item.Iteration = PLItem.UpdateItemIteration(this.Item.Iteration, this.uOid, false);
                }
                return true;
            } catch (Exception exception) {
                MessageBox.Show("输入信息格式错误，请检查信息合法性!" + exception.Message);
                return false;
            }
        }

        public void UpdateUI(DEBusinessItem item, DEMetaClassEx meta, Guid uOid, bool readOnly) {
            this.Item = item;
            if (meta != null) {
                this.meta = meta;
            }
            this.uOid = uOid;
            this.Text = "编辑对象" + item.Master.Id;
            int y = 20;
            int x = 20;
            int num3 = 150;
            int num4 = 50;
            int height = 30;
            int width = 100;
            int num7 = 150;
            this.pnlProperty.Controls.Clear();
            foreach (DEMetaAttribute attribute in this.meta.Attributes) {
                if (attribute.IsViewable) {
                    Label label = new Label {
                        Location = new Point(x, y + 5),
                        Size = new Size(width, height),
                        Text = attribute.Label + "："
                    };
                    TextBox box = null;
                    ResCombo combo = null;
                    if (!readOnly) {
                        if (attribute.LinkedResClass != Guid.Empty) {
                            combo = new ResCombo(ModelContext.MetaModel.GetClass(attribute.LinkedResClass).Name, attribute);
                        } else {
                            box = new TextBox();
                        }
                    } else {
                        box = new TextBox();
                    }
                    if (!attribute.IsEditable || (PLGrantPerm.CanDoClassAttribute(ClientData.LogonUser.Oid, item.Master.ClassName, attribute.Name, item) == 1)) {
                        if (box != null) {
                            box.ReadOnly = true;
                        } else if (combo != null) {
                            combo.readOnly = true;
                        }
                    }
                    if (readOnly) {
                        if (box != null) {
                            box.ReadOnly = true;
                        } else if (combo != null) {
                            combo.readOnly = true;
                        }
                    }
                    if (box != null) {
                        box.Location = new Point(num3, y);
                        box.Size = new Size(num7, height);
                        box.Tag = attribute;
                    } else if (combo != null) {
                        combo.Location = new Point(num3, y);
                        combo.Size = new Size(num7, height);
                        combo.Tag = attribute;
                    }
                    object attrValue = this.Item.Iteration.GetAttrValue(attribute.Name);
                    if (box != null) {
                        if (attrValue is Guid) {
                            box.Text = PrincipalRepository.GetPrincipalName((Guid)attrValue);
                        } else {
                            box.Text = PSConvert.Attribute2String(attrValue, attribute);
                        }
                    } else if (combo != null) {
                        if ((attribute.LinkType == 0) && (attribute.DataType == 8)) {
                            if (attrValue == null) {
                                combo.ResValue = "";
                            } else {
                                combo.ResValue = combo.GetResourceID(new Guid((byte[])attrValue), ModelContext.MetaModel.GetClass(attribute.LinkedResClass).Name);
                            }
                        } else {
                            combo.ResValue = PSConvert.Attribute2String(attrValue, attribute);
                        }
                    }
                    this.pnlProperty.Controls.Add(label);
                    if (box != null) {
                        this.pnlProperty.Controls.Add(box);
                    } else if (combo != null) {
                        this.pnlProperty.Controls.Add(combo);
                    }
                    y += num4;
                }
            }
        }
    }
}

