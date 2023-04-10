﻿using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


 class PPMReader
 {
    public static BitmapMaker ReadBitmapFromPPM(string file) {
        var reader = new BinaryReader(new FileStream(file, FileMode.Open));
        if (reader.ReadChar() != 'P' || reader.ReadChar() != '3') {
            return null;
        }
        reader.ReadChar(); //Eat newline
        string widths = "", heights = "";
        char temp;
        while ((temp = reader.ReadChar()) != ' ') {
            widths += temp;
        }
        while ((temp = reader.ReadChar()) >= '0' && temp <= '9') {
            heights += temp;
        }
        if (reader.ReadChar() != '2' || reader.ReadChar() != '5' || reader.ReadChar() != '5') {
            return null;
        }
        reader.ReadChar(); //Eat the last newline
        int width = int.Parse(widths),
            height = int.Parse(heights);
        BitmapMaker bitmap = new BitmapMaker(width, height);
        //Read in the pixels
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                //bitmap.SetPixel(x, y, new Bitmap.Color() {
                //    Red = reader.ReadByte(),
                //    Green = reader.ReadByte(),
                //    Blue = reader.ReadByte()
                //);
            }
        }
        return bitmap;
    }

 }

