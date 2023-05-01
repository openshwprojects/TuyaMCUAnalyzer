using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TuyaMCUAnalyzer
{

    public class ByteRingBuffer
    {
        public byte[] data = new byte[1024];
        public byte[] tmp = new byte[2];
        public int ofsIn, ofsOut;

        public void addData(byte b)
        {
            data[ofsIn] = b;
            ofsIn++;
            ofsIn %= data.Length;
        }
        public void addData(byte[] inData, int len)
        {
            for (int i = 0; i < len; i++)
            {
                addData(inData[i]);
            }
        }
        public void consumeBytes(int i)
        {
            ofsOut += i;
            ofsOut %= data.Length;
        }

        public static void doTestFor(ByteRingBuffer b, int cnt)
        {
            for (byte i = 0; i < cnt; i++)
            {
                b.addData((byte)(i % 255));
            }
            for (byte i = 0; i < cnt; i++)
            {
                byte iB = (byte)(i % 255);
                byte chk = b.getByte(0);
                if (chk != iB)
                {
                    MessageBox.Show("ByteRingBuffer test error");
                }
                b.consumeBytes(1);
            }
            for (byte i = 0; i < cnt; i++)
            {
                b.addData((byte)(i % 255));
            }
            for (byte i = 0; i < cnt; i++)
            {
                byte iB = (byte)(i % 255);
                byte chk = b.getByte(i);
                if (chk != iB)
                {
                    MessageBox.Show("ByteRingBuffer test error");
                }
            }
            b.consumeBytes(cnt);
        }
        internal static void doSelfTest()
        {
            ByteRingBuffer b = new ByteRingBuffer();
            doTestFor(b, 100);
            // doTestFor(b, 255);
            //doTestFor(b, 1000);
        }

        public int getSize()
        {
            int remain_buf_size = 0;

            if (ofsIn >= ofsOut)
            {
                remain_buf_size = ofsIn - ofsOut;
            }
            else
            {
                remain_buf_size = ofsIn + data.Length - ofsOut;
            }

            return remain_buf_size;
        }
        public byte getByte(int ofs)
        {
            int idx = (ofsOut + ofs) % data.Length;
            return data[idx];
        }

        public short getShort(int ofs)
        {
            tmp[0] = getByte(ofs + 1);
            tmp[1] = getByte(ofs);
            short value = BitConverter.ToInt16(tmp, 0);
            return value;
        }

        public byte[] getDataFromTo(int start, int len)
        {
            byte[] ret = new byte[len];
            for (int i = 0; i < len; i++)
            {
                ret[i] = getByte(start + i);
            }
            return ret;
        }
        public static string getHexString(byte[] data)
        {
            return getHexString(data, 0, data.Length);
        }
        public static string getHexString(byte[] data, int start, int len)
        {
            string s = "";
            for (int i = 0; i < len; i++)
            {
                if (i != 0)
                {
                    s += " ";
                }
                s += data[start + i].ToString("X2");
            }
            return s;
        }

        internal void clearBuffer()
        {
            ofsIn = ofsOut = 0;
        }
    }
}
