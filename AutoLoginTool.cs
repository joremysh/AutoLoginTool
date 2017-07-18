using AutoLoginTool.Controller;
using AutoLoginTool.Model;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace AutoLoginTool.View
{
    public partial class AutoLoginTool : Form
    {
        public AutoLoginTool()
        {
            InitializeComponent();
            model = new ModelClass();
            ctrl = new ControlClass();
            _connectionInfo = new BindingList<ConnectionInfo>();
            SetConnectInfos(_connectionInfo);
            string batPath = model.BatPath;
#if DEBUG
            batPath = @"D:\net use.bat";
#endif
            if (File.Exists(batPath))
            {
                SetConnectInfos(ctrl.ReadBat(batPath));
            }
        }
        private ModelClass model;
        private ControlClass ctrl;
        private BindingList<ConnectionInfo> _connectionInfo;
        public void SetConnectInfos(BindingList<ConnectionInfo> connectInfo)
        {
            _connectionInfo = connectInfo;
            dataGridView1.DataSource = _connectionInfo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl.WriteBat(_connectionInfo, model.BatPath);
                MessageBox.Show("儲存完成");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(model.BatPath))
                {
                    MessageBox.Show("找不到設定檔，此電腦可能尚未設定過，或者是設定檔已經移除");
                }
                else
                {
                    File.Delete(model.BatPath);
                    MessageBox.Show("設定檔移除成功");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
