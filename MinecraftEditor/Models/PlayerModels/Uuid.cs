using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEditor.Models.PlayerModels
{
    public class Uuid
    {
        private readonly int[] data;

        public Uuid(int[] data)
        {
            this.data = data;
        }

        public Uuid(string value)
        {
            value = value.Replace("-", "");
            data = new int[4];

            int k = 0;
            byte[] temp = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    temp[j] = Convert.ToByte(value.Substring(k, 2), 16);
                    k += 2;
                }

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(temp);
                }

                data[i] = BitConverter.ToInt32(temp, 0);
            }
        }

        public override int GetHashCode()
        {
            //from: https://stackoverflow.com/a/3404820

            int hc = 4;
            foreach (int val in data)
            {
                hc = unchecked(hc * 314159 + val);
            }

            return hc;
        }

        public override bool Equals(object obj)
        {
            if (obj is Uuid other)
            {
                return data.SequenceEqual(other.data);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            var bytes = data.SelectMany(x =>
            {
                var temp = BitConverter.GetBytes(x);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(temp);
                }
                return temp;
            });

            StringBuilder hex = new StringBuilder(36);
            int i = 0;
            foreach (byte b in bytes)
            {
                switch (i)
                {
                    case 4:
                    case 6:
                    case 8:
                    case 10:
                        hex.Append("-");
                        break;
                }

                hex.AppendFormat("{0:x2}", b);
                i++;
            }

            return hex.ToString();
        }
    }
}
