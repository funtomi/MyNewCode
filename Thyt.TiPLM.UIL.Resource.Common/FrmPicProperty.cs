    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common
{
    public partial class FrmPicProperty : FormPLM
    {
        
        private DEPicture myPicture;
        private ArrayList picList;
        
        private int type;

        public FrmPicProperty(DEPicture myPicture, int operType)
        {
            this.myPicture = new DEPicture();
            this.picList = new ArrayList();
            this.type = -1;
            this.InitializeComponent();
            this.myPicture = myPicture;
            this.type = operType;
        }

        public FrmPicProperty(DEPicture myPicture, ArrayList picList, int operType)
        {
            this.myPicture = new DEPicture();
            this.picList = new ArrayList();
            this.type = -1;
            this.InitializeComponent();
            this.myPicture = myPicture;
            this.picList = picList;
            this.type = operType;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.type == 0)
            {
                if ((this.txtAlias.Text == null) || (this.txtAlias.Text.Trim() == ""))
                {
                    MessageBoxPLM.Show("图片名称不可为空！", "修改图片", MessageBoxButtons.OK);
                    this.txtAlias.Focus();
                }
                else
                {
                    if (this.txtAlias.Text.Trim() != "")
                    {
                        foreach (DEPicture picture in this.picList)
                        {
                            if ((this.txtAlias.Text.Trim() == picture.Alias) && (this.myPicture.Oid != picture.Oid))
                            {
                                MessageBoxPLM.Show("该图片已经存在", "修改图片", MessageBoxButtons.OK);
                                this.txtAlias.Focus();
                                return;
                            }
                        }
                    }
                    bool flag = false;
                    if (this.txtAlias.Text != this.myPicture.Alias)
                    {
                        this.myPicture.Alias = this.txtAlias.Text.Trim();
                        flag = true;
                    }
                    if (this.txtDescrip.Text != this.myPicture.Description)
                    {
                        this.myPicture.Description = this.txtDescrip.Text.Trim();
                        flag = true;
                    }
                    if (!flag)
                    {
                        base.DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        this.myPicture.Modifier = ClientData.LogonUser.LogId;
                        this.myPicture.ModifyTime = DateTime.Now;
                        PLPicture picture2 = new PLPicture();
                        try
                        {
                            picture2.ModifyPicture(this.myPicture);
                            base.DialogResult = DialogResult.OK;
                        }
                        catch (PLMException exception)
                        {
                            MessageBoxPLM.Show("修改图片出错：" + exception.Message, "图片资源");
                            base.DialogResult = DialogResult.Cancel;
                        }
                        catch (Exception exception2)
                        {
                            MessageBoxPLM.Show("修改图片出错：" + exception2.Message, "图片资源");
                            base.DialogResult = DialogResult.Cancel;
                        }
                    }
                }
            }
            else
            {
                base.Close();
            }
        }
         

        private void FrmPicProperty_Load(object sender, EventArgs e)
        {
            if (this.myPicture != null)
            {
                this.txtAlias.Text = this.myPicture.Alias;
                this.txtDescrip.Text = this.myPicture.Description;
            }
            if (this.type == 1)
            {
                this.txtAlias.ReadOnly = true;
                this.txtDescrip.ReadOnly = true;
            }
        }


        public DEPicture newPicture
        {
            get {
                return this.myPicture;
            }
            set
            {
                this.myPicture = value;
            }
        }
    }
}

