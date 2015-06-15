using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SRT_Transcript
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void processDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drop Ocurred");
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
                ValidateAndConverSRT(file);
        }
        private void ValidateAndConverSRT(string fileName)
        {
            string sExtension;
            string sDirectory;
            string sTranscript = "";
            // Open the file to read from. 
            using (StreamReader sr = File.OpenText(fileName))
            {
                sExtension = System.IO.Path.GetExtension(fileName);
                sDirectory = System.IO.Path.GetDirectoryName(fileName);
                Console.WriteLine(sDirectory);
                Console.WriteLine(sExtension);
                if (!String.Equals(sExtension, ".srt", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Dropped file is not a subtitle file");
                }
                else
                {
                    FSM cStateMachine = new FSM();
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {

                        //STATE_INITIAL, STATE_NUM, STATE_TIME, STATE_ALPHA, STATE_BLANK, STATE_ACCEPT, STATE_REJECT, STATE_MAX
                        /*fptrStateFunc[] StateFuncArr = { 
                                                           cStateMachine.bSTATE_NUM_FN, cStateMachine.bSTATE_TIME_FN, 
                                                           cStateMachine.bSTATE_ALPHA_FN, cStateMachine.bSTATE_BLANK_FN, 
                                                           cStateMachine.bSTATE_ACCEPT, cStateMachine.bSTATE_REJECT
                                                       };
                        */
                        if (cStateMachine.bSTATE_NUM_FN(s) || cStateMachine.bSTATE_BLANK_FN(s) || cStateMachine.bSTATE_TIME_FN(s))
                            continue;
                        else
                            sTranscript = sTranscript + s;
                    }
                }
            }
            string sTransFileName = @sDirectory + @"\" + System.IO.Path.GetFileNameWithoutExtension(fileName) + ".txt";
            // Append a number if file exists. 
            try
            {

                if (File.Exists(sTransFileName))
                {
                    sTransFileName = @sDirectory + @"\" +  System.IO.Path.GetFileNameWithoutExtension(fileName) + "_SRTTrans" + ".txt";
                    if (File.Exists(sTransFileName)) { File.Delete(sTransFileName); }
                }
                // Create the file. 
                using (FileStream fs = File.Create(sTransFileName))
                {
                    Byte[] bInfo = new UTF8Encoding(true).GetBytes(sTranscript);
                    fs.Write(bInfo, 0, bInfo.Length);
                }

                sExtension = System.IO.Path.GetExtension(sTransFileName);
                sDirectory = System.IO.Path.GetDirectoryName(sTransFileName);
                Console.WriteLine(sDirectory);
                Console.WriteLine(sExtension);

                // Open the stream and read it back. 
                using (StreamReader sr = File.OpenText(sTransFileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                       // Console.WriteLine(s);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

}