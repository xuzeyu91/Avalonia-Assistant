﻿using Microsoft.SemanticKernel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Assistant.Domain.NativePlugins
{
    public class ChromePlugin
    {
        private readonly Kernel _kernel;

        public ChromePlugin(Kernel kernel)
        {
            _kernel = kernel;
        }

        [KernelFunction, Description("打开Chrome浏览器")]
        public string OpenChrome([Description("url地址")] string url)
        {
            string chromePath = Registry.GetValue(
    @"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe",
    "",
    null) as string;

            if (chromePath == null)
            {
                // 尝试在另一个常见的注册表位置查找
                chromePath = Registry.GetValue(
                    @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe",
                    "",
                    null) as string;
            }

            if (!string.IsNullOrEmpty(chromePath))
            {
                Process.Start(chromePath, url);
                return $"打开完成:{url}";
            }
            else
            {
                // Chrome未安装或找不到chrome.exe
               
                return "Chrome浏览器未找到！";
            }
        }

    }
}
