using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetEncoding
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"c:\temp\test.dat";
            using (StreamWriter streamWriter = File.CreateText(path))
            {
                string myValue = "MyValue";
                streamWriter.Write(myValue);
            }

            using (FileStream fileStream = File.OpenRead(path))
            {
                byte[] data = new byte[fileStream.Length];
                for (int index = 0; index < fileStream.Length; index++)
                {
                    data[index] = (byte)fileStream.ReadByte();
                }
                Console.WriteLine(Encoding.UTF8.GetString(data)); // Displays: MyValue

                using (StreamReader streamWriter = File.OpenText(path))
                {
                    Console.WriteLine(streamWriter.ReadLine()); // Displays: MyValue
                }
            }

            //Print_AllCodePage();
            //Print_MostCommonsEncodings();

            //Print_BitConverter();
            Print_EncodeAlphabet();

            Console.ReadKey();
        }

        private static void Print_AllCodePage()
        {
            // Print the header.
            Console.Write("CodePage identifier and name     ");
            Console.Write("BrDisp   BrSave   ");
            Console.Write("MNDisp   MNSave   ");
            Console.WriteLine("1-Byte   ReadOnly ");

            // For every encoding, get the property values.
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding e = ei.GetEncoding();

                Console.Write("{0,-6} {1,-25} ", ei.CodePage, ei.Name);
                Console.Write("{0,-8} {1,-8} ", e.IsBrowserDisplay, e.IsBrowserSave);
                Console.Write("{0,-8} {1,-8} ", e.IsMailNewsDisplay, e.IsMailNewsSave);
                Console.WriteLine("{0,-8} {1,-8} ", e.IsSingleByte, e.IsReadOnly);
            }
        }

        private static void Print_MostCommonsEncodings()
        {
            // The characters to encode:
            //    Latin Small Letter Z (U+007A)
            //    Latin Small Letter A (U+0061)
            //    Combining Breve (U+0306)
            //    Latin Small Letter AE With Acute (U+01FD)
            //    Greek Small Letter Beta (U+03B2)
            //    a high-surrogate value (U+D8FF)
            //    a low-surrogate value (U+DCFF)
            char[] myChars = new char[] { 'z', 'a', '\u0306', '\u01FD', '\u03B2', '\uD8FF', '\uDCFF' };

            // Get different encodings.
            Encoding encUTF7 = Encoding.GetEncoding(65000);
            Encoding u7 = Encoding.UTF7;
            Encoding encUTF8 = Encoding.GetEncoding(65001);
            Encoding u8 = Encoding.UTF8;
            Encoding u16LE = Encoding.Unicode;
            Encoding u16BE = Encoding.BigEndianUnicode;
            Encoding encUTF32 = Encoding.GetEncoding(12000);
            Encoding u32 = Encoding.UTF32;
            Encoding encASCII = Encoding.ASCII;
            Encoding encDefault = Encoding.Default;

            // Encode the entire array, and print out the counts and the resulting bytes.
            PrintCountsAndBytes(myChars, u7);
            PrintCountsAndBytes(myChars, u8);
            PrintCountsAndBytes(myChars, u16LE);
            PrintCountsAndBytes(myChars, u16BE);
            PrintCountsAndBytes(myChars, u32);
            PrintCountsAndBytes(myChars, encASCII);
            PrintCountsAndBytes(myChars, encDefault);
        }

        public static void PrintCountsAndBytes(char[] chars, Encoding enc)
        {

            // Display the name of the encoding used.
            Console.Write("{0,-30} :", enc.ToString());

            // Display the exact byte count.
            int iBC = enc.GetByteCount(chars);
            Console.Write(" {0,-3}", iBC);

            // Display the maximum byte count.
            int iMBC = enc.GetMaxByteCount(chars.Length);
            Console.Write(" {0,-3} :", iMBC);

            // Encode the array of chars.
            byte[] bytes = enc.GetBytes(chars);

            // Display all the encoded bytes.
            PrintHexBytes(bytes);
        }

        public static void PrintHexBytes(byte[] bytes)
        {

            if ((bytes == null) || (bytes.Length == 0))
            {
                Console.WriteLine("<none>");
            }
            else
            {
                for (int i = 0; i < bytes.Length; i++)
                    Console.Write("{0:X2} ", bytes[i]);
                Console.WriteLine();
            }
        }

        private static void Print_BitConverter()
        {
            // Cria e escreve uma string contendo o símbolo para PI.
            string stringFonte = "Area = \u03A0r^2";
            Console.WriteLine("Texto Fonte : " + stringFonte);

            // Escreve os bytes codificados UTF-16 para string fonte
            byte[] utf16String = Encoding.Unicode.GetBytes(stringFonte);
            Console.WriteLine("Bytes UTF-16: {0}",
            BitConverter.ToString(utf16String));

            // Converte a string fonte codificada UTF-16 para UTF-8 e ASCII.
            byte[] utf8String = Encoding.UTF8.GetBytes(stringFonte);
            byte[] asciiString = Encoding.ASCII.GetBytes(stringFonte);

            // Escreve o array de bytes da codificacao UTF-8 e ASCII.
            Console.WriteLine("Bytes UTF-8 : {0}",
            BitConverter.ToString(utf8String));
            Console.WriteLine("Bytes ASCII : {0}",
            BitConverter.ToString(asciiString));

            // Converte os bytes codificados UTF-8 e ASCII de volta para a codificação string UTF-16 e escreve
            Console.WriteLine("Texto UTF-8  : {0}",
            Encoding.UTF8.GetString(utf8String));
            Console.WriteLine("Texto  ASCII : {0}",
            Encoding.ASCII.GetString(asciiString));
        }

        private static void Print_EncodeAlphabet()
        {
            Encoding enc = Encoding.GetEncoding(1253);
            Encoding altEnc = Encoding.GetEncoding("windows-1253");
            Console.WriteLine("{0} = Code Page {1}: {2}", enc.EncodingName,
                              altEnc.CodePage, enc.Equals(altEnc));
            string greekAlphabet = "Α α Β β Γ γ Δ δ Ε ε Ζ ζ Η η " +
                                   "Θ θ Ι ι Κ κ Λ λ Μ μ Ν ν Ξ ξ " +
                                   "Ο ο Π π Ρ ρ Σ σ ς Τ τ Υ υ " +
                                   "Φ φ Χ χ Ψ ψ Ω ω";
            Console.OutputEncoding = Encoding.UTF8;
            byte[] bytes = enc.GetBytes(greekAlphabet);
            Console.WriteLine("{0,-12} {1,20} {2,20:X2}", "Character",
                              "Unicode Code Point", "Code Page 1253");
            for (int ctr = 0; ctr < bytes.Length; ctr++)
            {
                if (greekAlphabet[ctr].Equals(' '))
                    continue;

                Console.WriteLine("{0,-12} {1,20} {2,20:X2}", greekAlphabet[ctr],
                                  GetCodePoint(greekAlphabet[ctr]), bytes[ctr]);
            }
        }

        private static string GetCodePoint(char ch)
        {
            string retVal = "u+";
            byte[] bytes = Encoding.Unicode.GetBytes(ch.ToString());
            for (int ctr = bytes.Length - 1; ctr >= 0; ctr--)
                retVal += bytes[ctr].ToString("X2");

            return retVal;
        }

    }
}
