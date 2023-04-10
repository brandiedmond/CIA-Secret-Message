
using System.Drawing;
using System.IO;

  class BitmaptoPPM
  {
    public static object writerB { get; private set; }

        public static void WriteBitmaptoPPM(string file, BitmapMaker bitmap) {
            
            //Use a streamwriter to write the text part of the encoding
            var writer = new StreamWriter(file);
            writer.Write("P3");
            writer.Write($"{bitmap.Width}{bitmap.Height}");
            writer.Write("225");
            writer.Close();

            // Switching to binary writer to write the data
            var writrB = new BinaryWriter(new FileStream(file, FileMode.Append));
            for (int x =0;  x < bitmap.Height; x++) {
               for (int y =0; y <bitmap.Width; y++) {
                        Color color = bitmap.SetPixel(y, x);
                        writerB.Equals(color.R);
                        writerB.Equals(color.G);
                        writerB.Equals(color.B);
               } // end for
                writer.Close();
            } // end for 
        }// end method 
  } // end class


