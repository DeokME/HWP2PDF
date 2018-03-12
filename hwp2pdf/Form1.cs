using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hwp2pdf
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            string InputFilePath = args[0];
            string outputFilePath = args[1];

            bool res = Convert(InputFilePath, outputFilePath);

            if (res)
            {
                this.Close();
            }

        }

        public bool Convert(string inputFilePath, string outputFilePath)

        {

            const string HNCRoot = @"HKEY_Current_User\Software\HNC\HwpCtrl\Modules";

            string myProjectPath = Path.GetFullPath(".\\");
            
            try{
                if (Microsoft.Win32.Registry.GetValue(HNCRoot, "FilePathChecker", "Not Exist").Equals("Not Exist")) { 
                    Microsoft.Win32.Registry.SetValue(HNCRoot, "FilePathChecker", Environment.CurrentDirectory + "\\" + "FilePathCheckerModuleExample.dll");
                }
            }catch {
                Microsoft.Win32.Registry.SetValue(HNCRoot, "FilePathChecker", Environment.CurrentDirectory + "\\" + "FilePathCheckerModuleExample.dll");
            }

            bool result = axHwpCtrl1.RegisterModule("FilePathCheckDLL", "FilePathChecker");
            axHwpCtrl1.Open(inputFilePath);
            axHwpCtrl1.SaveAs(outputFilePath, "PDF", null);
            axHwpCtrl1.Clear();

            return true;
        }
        

    }
}
