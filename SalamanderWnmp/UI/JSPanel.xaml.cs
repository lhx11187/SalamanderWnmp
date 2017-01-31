﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SalamanderWnmp.UI
{
    /// <summary>
    /// JSPanel.xaml 的交互逻辑
    /// </summary>
    public partial class JSPanel : Window
    {
        public JSPanel()
        {
            InitializeComponent();
        }

        private void title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
            e.Handled = true;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            e.Handled = true;
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            string code = this.txtCode.Text;
            if(string.IsNullOrEmpty(code))
            {
                MessageBox.Show("请输入js代码");
                return;
            }
            this.txtOutput.Text = RunNode(code);
        }

        /// <summary>
        /// 运行js脚本
        /// </summary>
        /// <param name="code"></param>
        private string RunNode(string code)
        {
            Process scriptProc = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "node.exe";
            info.RedirectStandardError = true;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            scriptProc.StartInfo = info;
            try
            {
                scriptProc.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show("node未安装或者未设置node环境变量！");
                return "";
            }
            StreamWriter writer = scriptProc.StandardInput;
            writer.WriteLine(code);
            writer.Close();
            string outStr = scriptProc.StandardOutput.ReadToEnd();
            scriptProc.Close();
            return outStr;

        }

        private void txtCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Tab)
            {
                int index = txtCode.SelectionStart;
                //txtCode.
            }
            e.Handled = true;
        }
    }
}
