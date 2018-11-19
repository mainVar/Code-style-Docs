//!*****************************************************************************
//! __Revisions:__
//!  Date       | Author              | Comments
//!  ---------- | ------------------- | ----------------
//!  19/11/2018 | mainvar             | CRC 16
//
//******************************************************************************
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CRC16
{
    
    static class Program
    {
        static void Main()
        {
            string input = "";
            Console.Title = "CRC-16";
           
            start:
            Console.WriteLine("input value(hex):");
            input = Console.ReadLine();
            var bytes = HexToBytes(input);

            string hex = Crc16.ComputeChecksum(bytes).ToString("x2");
            Console.WriteLine(hex); //c061
            Console.WriteLine("New value? (y):");
            input = Console.ReadLine();
            if(input =="y")
            {
                goto start;
            }

            
        }
        /// <summary>
        /// Calculate checksum
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        static byte[] HexToBytes(string input)
        {
            byte[] result = new byte[input.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(input.Substring(2 * i, 2), 16);
            }
          
            return result;
          
        }
        /// <summary>
        /// Calculation of checksum bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static class Crc16
        {
            const ushort polynomial = 0xA001;
            static readonly ushort[] table = new ushort[256];

            public static ushort ComputeChecksum(byte[] bytes)
            {
                ushort crc = 0;
                for (int i = 0; i < bytes.Length; ++i)
                {
                    byte index = (byte)(crc ^ bytes[i]);
                    crc = (ushort)((crc >> 8) ^ table[index]);
                }
             
                return crc;
            }
            /// <summary>
            /// CRC 16 
            /// </summary>
            /// <param name="initialValue"></param>
            static Crc16()
            {
                ushort value;
                ushort temp;
                for (ushort i = 0; i < table.Length; ++i)
                {
                    value = 0;
                    temp = i;
                    for (byte j = 0; j < 8; ++j)
                    {
                        if (((value ^ temp) & 0x0001) != 0)
                        {
                            value = (ushort)((value >> 1) ^ polynomial);
                        }
                        else
                        {
                            value >>= 1;
                        }
                        temp >>= 1;
                    }
                    table[i] = value;
                }
            }
        }
    }

}
