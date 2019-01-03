using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Tools;

namespace DesktopApp
{
    public partial class frmConfig : Form
    {
        private frmMain frmParent;
        public frmConfig(frmMain _frmMain)
        {
            InitializeComponent();

            frmParent = _frmMain;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Application.StartupPath + @"\config.lsf";
                string skinfile = cbSkin.SelectedItem.ToString();
                SysConfig config = new SysConfig();
                if (skinfile == "None")
                {
                    config.SkinFile = "";
                }
                else
                {
                    config.SkinFile = Application.StartupPath + @"\Skins\" + skinfile;
                }
                

                if (File.Exists(filePath))
                    File.Delete(filePath);

                KBFile kbFile = new KBFile();
                kbFile.path = filePath;
                int result = kbFile.writeObjectToFile(config);

                if (result > 0)
                {
                    frmParent.frmLogin.setSkin(config.SkinFile);
                    untCommon.InfoMsg("设置成功！");
                }
                else
                {
                    untCommon.InfoMsg("设置失败！");
                }
            }
            catch (Exception ex)
            {
                
                 untCommon.ErrorMsg("皮肤设置异常："+ex.Message);
            }
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            loadSkin();
        }

        private void loadSkin()
        {

            string[] files = FileHelper.GetFileNames(Application.StartupPath + @"\Skins\");

            if (files == null || files.Length < 1)
            {
                untCommon.InfoMsg("皮肤文件不存在！");
                this.Close();
            }

            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                cbSkin.Items.Add(fileInfo.Name);
            }

            cbSkin.Items.Add("None");
        }
    }
}
