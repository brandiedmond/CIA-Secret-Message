using System;
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
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Media3D;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace CIA_Secret_Messes {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public BitmapMaker bitmapglobal;
        public MainWindow() {
            InitializeComponent();
        } // end main

        private Button GetButton;
        public OpenFileDialog openFileDialog;
        private TextBox textBox1;
        private string loadedImage;
        private object ColorCount;
        private object mypalette;
        private object x;
        private object y;

        private byte[] _pixelData;

        byte[] encrypted;

        public string PpmType { get; private set; }
        public int Width {
            get { return _width; }
        }//end property
        public int Height {
            get { return _height; }
        }//end property

        //THE NUMBER OF BYTES PER ROW.
        private int _stride;

        //THE BITMAP'S SIZE.
        private int _width;
        private int _height;

        public string TextBox { get; private set; }
      
        public BitmapMaker image { get; private set; }

        private void muiFile_Click(object sender, RoutedEventArgs e) {
             // Create an ofd
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Parameters for open file dialog
            openFileDialog.DefaultExt = ".ppm";
            openFileDialog.Filter = "PPM Files (.ppm)| * .ppm";

            // Shows the file dialog 
            bool? result = openFileDialog.ShowDialog();

            // Processing the dialog results to determine if a file was ever opened
            if (result == true) {

                loadedImage= openFileDialog.FileName;

                // Calling loadimage method
                LoadPPM(openFileDialog.FileName );
            } // end if
        } // end muuFile_Click

        public void LoadPPM(string path) {
            StreamReader InFile = new StreamReader(path);
            BitmapMaker databmp;

            //Get PPM header 
            string typePPM      = InFile.ReadLine();
            string CommentPPM   = InFile.ReadLine();
            string DimenionsPPM = InFile.ReadLine();
            string maxPPMvalue  = InFile.ReadLine();

            // Processesing the dimensions
            string[] arraydimns = DimenionsPPM.Split();
            int width = int.Parse(arraydimns[0]);
            int height = int.Parse(arraydimns[1]);

            // Lets build our bitmap
            databmp = new BitmapMaker(width, height);

            // Reading the pixel's data
            string redStr   = "";
            string greenStr = "";
            string bluStr   = "";
            

            // looping thru each pixels
                for (int posy = 0; posy < width; posy++) {
                    for (int posx = 0; posx < height; posx++) {

                     int countpix;
                      byte r;
                      byte g;
                      byte b; 

                        // Reading each pixel from the file
                        redStr = InFile.ReadLine();
                        greenStr = InFile.ReadLine();
                        bluStr = InFile.ReadLine();

                        // Converting the pix data into its bytes
                        r = byte.Parse(redStr);
                        g = byte.Parse(greenStr);
                        b = byte.Parse(bluStr);

                        //Pixels in bitmap
                        databmp.SetPixel(posx, posy, r, g, b);

                    } // end for
                }// end for


            WriteableBitmap writeableBmp1 = databmp.MakeBitmap();
            bitmapglobal = databmp;
            imgMain.Source = writeableBmp1; 

        } // end method

          private void SaveBtn_Click(object sender, RoutedEventArgs e) {
            // Creating an instance of SaveDialog class
           SaveFileDialog SaveFileDialog = new SaveFileDialog(); 
           // Respresents the directory to be displayed when the open file dialog appears for the first time
            SaveFileDialog.InitialDirectory = @"C:\"; 
            // Used to get/set title of open file dialog
            SaveFileDialog.Title = "Save text Files";
            // Indicates whether the dialog box displays a warning if the user specifies a file name that does not exist
            SaveFileDialog.CheckFileExists= true; 
            // Indicates whether the dialog box displays a warning if the user specifies a path that does not exist
            SaveFileDialog.CheckPathExists = false; 
            // Represents the default file name extension
            SaveFileDialog.DefaultExt = "txt"; 
            //Represents the filter on an ofd that is used to filter the type of files to be loaded during the browse option 
            SaveFileDialog.Filter = "Text files (*.txt) |*.txt| All Files (*.*) *.*"; 
            // Represents the index of the filter currently selected in file dialog box
            SaveFileDialog.FilterIndex = 2; 
            // If RestoreDirectory == true, open file dialog box restores the current directory before closing
            SaveFileDialog.RestoreDirectory= true; 
            // If the Save dialog is the same as the dialog result
            if (SaveFileDialog.ShowDialog() == DialogResult) {
                textBox1.Text = SaveFileDialog.FileName;
            } // end if
          }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();

            openfiledialog.Title = "Open Image";
            openfiledialog.Filter = "Image File|*.bmp; *.gif; *.jpg; *.jpeg; *.png;";

            if (openfiledialog.ShowDialog() == true)
            {
                image = new BitmapMaker(openfiledialog.FileName);
            }
        }

       
        private void EncryptButton_Click(object sender, TextChangedEventArgs e,byte[] key, byte[] iv, string plainText, string path) { 
            byte[] encrypted;
            FileStream ppmImage = new FileStream(path, FileMode.Open);
            // Create an Aes object with the specified key and IV.
            using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create()) {
                aes.Key = key;
                aes.IV = iv;

                // Create a new MemoryStream object to contain the encrypted bytes.
                using (MemoryStream memoryStream = new MemoryStream()) {
                    // Create a CryptoStream object to perform the encryption.
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write)) {
                        // Encrypt the plaintext.
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream)) {
                            streamWriter.Write(plainText);
                        } // end using

                        encrypted = memoryStream.ToArray();
                    } // end using
                } // end using
            } // end using 
        } // end EncryptButton

        private void Messegebox_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
