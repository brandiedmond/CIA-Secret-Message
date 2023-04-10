using System.IO;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Media;
using System.Linq;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

class PPMM{
    #region Public Properties
    public string? PpmType { get; private set; }
    public string? PpmComment { get; private set; }
    public string? PpmDimensions { get; private set; }
    public string? PpmWidth { get; private set; }
    public string? PpmHieght { get; private set; }
    public int PpmMaxColor { get; private set; }
    public string? PpmColorsString { get; private set; }
    public char[]? PpmColorsChars { get; private set; }
    #endregion

    #region Public Methods
    public BitmapMaker LoadPPMImage(string path) {
        // Open the ppm image as a FileStream
        FileStream ppmImage = new FileStream(path, FileMode.Open);

        // Create a string variable
        string ppmString = "";

        // Loop through the loaded ppm data
        while (ppmImage.Position < ppmImage.Length) {
            // Store the bytes as chars in the string
            ppmString += (char)ppmImage.ReadByte();
        }// End while

        // Split the string into a string array
        string[] ppmData = ppmString.Split("\n");

        // Read header
        PpmType = ppmData[0];             // P3 (ASCII) or P6 (binary)
        PpmComment = ppmData[1];             // Comment line
        PpmDimensions = ppmData[2];             // Width and height of image
        PpmMaxColor = int.Parse(ppmData[3]);  // Maximum value of each color (255)

        // Set dimensions of image
        string[] arrayDimensions = PpmDimensions.Split();
        int width = int.Parse(arrayDimensions[0]);
        int height = int.Parse(arrayDimensions[1]);

        // Create new color array to store each pixel color
        Color[] colorArray = new Color[width * height];

        // Displaying the image if type is P3
        if (PpmType == "P3") {
            // Add colors to string array, skipping the first 4 lines and excluding empty lines
            string[] colorStrings = ppmData.Skip(4).Where(val => val != "").ToArray();

            // Store the text of the color strings
            PpmColorsString = string.Join("\n", colorStrings);

            // Loop through the color string array and add each r,g,b set to an index in the color array
            for (int cIndex = 0; cIndex < (width * height); cIndex++) {
                // For every three bytes, the color index increases by one
                int byteIndex = cIndex * 3;

                // Set the a,r,g,b values of each index of the color array
                colorArray[cIndex].A = 255;
                colorArray[cIndex].R = byte.Parse(colorStrings[byteIndex]);
                colorArray[cIndex].G = byte.Parse(colorStrings[++byteIndex]);
                colorArray[cIndex].B = byte.Parse(colorStrings[++byteIndex]);
            }// End for
        }// End if

        // Displaying the image if type is P6
        else if (PpmType == "P6") {
            // Create a char array
            //PpmChars = new char[width * height];

            // Convert the ascii characters to chars
            PpmColorsChars = ppmData[4].ToCharArray();

            // Loop through the color char array and add each r,g,b set to an index in the color array
            for (int cIndex = 0; cIndex < (width * height); cIndex++) {
                // For every three bytes, the color index increases by one
                int byteIndex = cIndex * 3;

                // Set the a,r,g,b values of each index of the color array
                colorArray[cIndex].A = 255;
                colorArray[cIndex].R = (byte)PpmColorsChars[byteIndex];
                colorArray[cIndex].G = (byte)PpmColorsChars[++byteIndex];
                colorArray[cIndex].B = (byte)PpmColorsChars[++byteIndex];
            }// End for
        }// End else if

        // Create a bitmap to hold image data
        BitmapMaker bmpImage = new BitmapMaker(width, height);

        // Create an index to loop through each color of the color array
        int colorIndex = 0;

        // Loop through pixel data to set the pixels
        for (int y = 0; y < width; y++) {
            for (int x = 0; x < height; x++) {
                bmpImage.SetPixel(x, y, colorArray[colorIndex]);
                colorIndex++;
            }// End for
        }// End for

        // Close the FileStream
        ppmImage.Close();

        // Return bitmap image of ppm file
        return bmpImage;
    }// End LoadPPM()
    #endregion
}// End class