using System;
using System.Linq;
using HidLibrary;
using System.Globalization;

namespace Sleddog.TEMPer
{
    public class TEMPer2
    {
        private static readonly int VendorId = 0x0C45;
        private static readonly int ProductId = 0x7401;

        private static readonly byte[] ReadTemperateureCommand = { 0x00, 0x01, 0x80, 0x33, 0x01, 0x00, 0x00, 0x00, 0x00 };

        public static void Main(String[] args)
        {
            var hidDevices = new HidEnumerator().Enumerate(VendorId, ProductId);
            IHidDevice device = null;
            try
            {
                device = hidDevices.Single(hd => hd.Capabilities.UsagePage == -256);
                device.Write(ReadTemperateureCommand);
                var data = device.Read();
                while (data.Status != HidDeviceData.ReadStatus.Success) ;
                device.CloseDevice();

                CultureInfo ci = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                ci.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = ci;
                
                Console.Write("{\"in\":" + Convert(data.Data[3], data.Data[4]) + ",\"out\":" + Convert(data.Data[5], data.Data[6]) + "}");

            }
            catch (Exception e)
            {
                Console.Write("Device is not connected");
                Console.ReadLine();
                Environment.Exit(-1);
            }
        }

        private static float Convert(byte t1, byte t2)
        {
            return t1 + (t2 >> 4) / 16f;

        }

    }
}