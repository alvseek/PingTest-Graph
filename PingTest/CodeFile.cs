using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace PingTest
{

    public class UIFunction
    {
        public class Win32Access
        {
            //Creates a layered window.
            private const uint WS_EX_LAYERED = 0x00080000;

            //Specifies that a window created with this style should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
            //The window appears transparent because the bits of underlying sibling windows have already been painted.
            private const uint WS_EX_TRANSPARENT = 0x00000020;

            private const int GWL_EXSTYLE = -20;

            private static class NativeMethods
            {
                [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
                public static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

                [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
                public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

                [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
                public static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

                [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
                public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);
            }
            // This static method is required because Win32 does not support
            // GetWindowLongPtr directly
            private static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
            {
                if (IntPtr.Size == 8)
                    return NativeMethods.GetWindowLongPtr64(hWnd, nIndex);
                else
                    return NativeMethods.GetWindowLongPtr32(hWnd, nIndex);
            }

            // This helper static method is required because the 32-bit version of user32.dll does not contain this API
            // (on any versions of Windows), so linking the method will fail at run-time. The bridge dispatches the request
            // to the correct function (GetWindowLong in 32-bit mode and GetWindowLongPtr in 64-bit mode)
            private static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
            {
                if (IntPtr.Size == 8)
                    return NativeMethods.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
                else
                    return new IntPtr(NativeMethods.SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
            }

            private void ReShowForm(Form openForm)
            {
                openForm.Hide();
                openForm.ShowInTaskbar = openForm.TopMost;
                openForm.ShowInTaskbar = !openForm.TopMost;
                openForm.Show();
            }

            public void MakeUnClickable(Form openForm)
            {
                if (openForm.Opacity == 1) openForm.Opacity = (float)99.99 / 100;
                IntPtr initialStyle = GetWindowLongPtr(openForm.Handle, GWL_EXSTYLE);
                SetWindowLongPtr(new HandleRef(openForm, openForm.Handle), GWL_EXSTYLE, (IntPtr)((int)initialStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT));
                Properties.Settings.Default.Clickable = false;
                Properties.Settings.Default.Save();
                ReShowForm(openForm);

            }

            public void MakeClickable(Form openForm)
            {
                IntPtr initialStyle = GetWindowLongPtr(openForm.Handle, GWL_EXSTYLE);
                SetWindowLongPtr(new HandleRef(openForm, openForm.Handle), GWL_EXSTYLE, (IntPtr)((int)initialStyle & ~WS_EX_LAYERED & ~WS_EX_TRANSPARENT));
                SetWindowLongPtr(new HandleRef(openForm, openForm.Handle), GWL_EXSTYLE, (IntPtr)((int)initialStyle | WS_EX_LAYERED));
                Properties.Settings.Default.Clickable = true;
                Properties.Settings.Default.Save();
                ReShowForm(openForm);
            }
        }

        public enum UIPosition { Center, Top, Bottom, Right, Left, TopRight, TopLeft, BottomRight, BottomLeft };

        public void MoveToDefault(Form openForm)
        {
            openForm.Location = new Point((Screen.PrimaryScreen.Bounds.Width / 2) + (openForm.Width), (Screen.PrimaryScreen.Bounds.Height / 2) + (openForm.Height / 2));
        }

        public void MovePosition(Form openForm, UIPosition uiPosition)
        {
            switch (uiPosition)
            {
                case UIPosition.Center:
                    openForm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (openForm.Width / 2), (Screen.PrimaryScreen.WorkingArea.Height / 2) - (openForm.Height / 2));
                    break;
                case UIPosition.Top:
                    openForm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (openForm.Width / 2), Screen.PrimaryScreen.WorkingArea.Top);
                    break;
                case UIPosition.Bottom:
                    openForm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (openForm.Width / 2), Screen.PrimaryScreen.WorkingArea.Bottom - openForm.Height);
                    break;
                case UIPosition.Right:
                    openForm.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - openForm.Width, (Screen.PrimaryScreen.WorkingArea.Height / 2) - (openForm.Height / 2));
                    break;
                case UIPosition.Left:
                    openForm.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left, (Screen.PrimaryScreen.WorkingArea.Height / 2) - (openForm.Height / 2));
                    break;
                case UIPosition.TopRight:
                    openForm.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - openForm.Width, Screen.PrimaryScreen.WorkingArea.Top);
                    break;
                case UIPosition.TopLeft:
                    openForm.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left, Screen.PrimaryScreen.WorkingArea.Top);
                    break;
                case UIPosition.BottomRight:
                    openForm.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - openForm.Width, Screen.PrimaryScreen.WorkingArea.Bottom - openForm.Height);
                    break;
                case UIPosition.BottomLeft:
                    openForm.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left, Screen.PrimaryScreen.WorkingArea.Bottom - openForm.Height);
                    break;
            }

        }
    }

    public class AppFunction
    {
        FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
        public enum LicenseType { Value, Title };
        private string type = "SOFTWARE";
        string companyName;
        string softwareName;
        string donationLink;
        public string DonationLink
        {
            get { return this.donationLink; }
        }

        public AppFunction()
        {
            companyName = versionInfo.CompanyName;
            softwareName = versionInfo.ProductName;
            donationLink = "www.indonesiamadjoe.com/?target=donation&source=" + softwareName;
        }

        public string CheckLicenseString(string license)
        {
            EncryptLicense encryptLicense = new EncryptLicense();
            short[] decodeLicense = encryptLicense.DecodeLicense(license);
            if (encryptLicense.EncryptContributor(decodeLicense))
            {
                return "Contributor";
            }
            else if (encryptLicense.EncryptSponsor(decodeLicense))
            {
                return "Sponsor";
            }
            else if (encryptLicense.EncryptPhilanthropist(decodeLicense))
            {
                return "The Great Philanthropist";
            }
            else return string.Empty;
        }

        private class EncryptLicense
        {
            public bool EncryptContributor(short[] encrypted)
            {
                short[][] type1 = new short[5][];
                type1[0] = new short[] { 241, 235, 245, 226, 235, 240, 241, 235, 240, 239, 225, 222, 225, 228, 240, 239, 225, 231, 221, 233, 245, 240, 239, 225, 234, 235, 228 };
                type1[1] = new short[] { 219, 226, 221, 234, 221, 219, 237, 213, 235, 227, 235, 232, 213, 231, 221, 231, 231, 217, 226, 216, 226, 221, 223 };
                type1[2] = new short[] { 219, 205, 209, 212, 218, 205, 222, 205, 214, 211, 218, 215, 223, 204, 218, 201, 208 };
                type1[3] = new short[] { 222, 209, 226, 209, 222, 219, 210, 224, 223, 205, 216, 209, 226, 219, 216, 209, 225, 222, 224 };
                type1[4] = new short[] { 232, 226, 233, 227, 215, 231, 231, 217, 226, 216, 226, 221, 223, 224, 224, 213, 225, 231, 213, 226, 217, 234, 217 };
                foreach (short[] type1check in type1)
                {
                    if (Enumerable.SequenceEqual(encrypted, type1check))
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool EncryptSponsor(short[] encrypted)
            {
                short[][] type2 = new short[5][];
                type2[0] = new short[] { 289, 297, 293, 304, 303, 309, 285, 307, 296, 285, 303, 293, 289, 302, 289, 292, 304, 288, 298, 285, 289, 296, 286, 305, 299, 302, 304, 292, 287, 305, 297, 299, 299, 304, 303, 293, 291, 298, 293, 292, 304, 299, 298, 289, 306, 299, 296, 303, 293, 289, 302, 289, 292, 304, 289, 302, 289, 292, 307 };
                type2[1] = new short[] { 298, 299, 303, 308, 295, 310, 307, 309, 297, 297, 295, 313, 306, 299, 299, 300, 299, 306, 310, 309, 299, 310, 312, 299, 302, 314, 309, 299, 305, 295, 307, 309, 314, 313, 303, 299, 308, 303, 297, 303, 298, 299, 307, 299, 302, 314, 299, 308, 309, 306, 295, 306, 299, 299, 300, 312, 299, 316, 299, 315, 309, 319, 300, 303 };
                type2[2] = new short[] { 256, 245, 241, 255, 237, 254, 241, 254, 241, 243, 250, 237, 261, 254, 241, 258, 241, 256, 257, 238, 256, 250, 257, 251, 239, 255, 254, 237, 241, 256, 261, 254, 241, 258, 241 };
                type2[3] = new short[] { 316, 305, 302, 311, 316, 315, 311, 309, 316, 301, 303, 304, 316, 311, 298, 311, 300, 311, 304, 319, 301, 315, 311, 304, 316, 316, 317, 298, 307, 308, 297, 316, 311, 304, 319, 301, 315, 311, 304, 316, 310, 297, 304, 316, 314, 301, 304, 303, 305, 304, 300, 305, 297, 312, 316, 299, 297, 311, 304, 319, 301, 315, 311, 304, 316 };
                type2[4] = new short[] { 241, 231, 226, 222, 231, 228, 228, 237, 222, 221, 218, 241, 232, 232, 217, 224, 221, 218, 241, 232, 232, 217, 224, 221, 218 };

                foreach (short[] type2check in type2)
                {
                    if (Enumerable.SequenceEqual(encrypted, type2check))
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool EncryptPhilanthropist(short[] encrypted)
            {
                short[][] type3 = new short[5][];
                type3[0] = new short[] { 258, 256, 239, 243, 246, 244, 253, 251, 256, 239, 261, 243, 246, 258, 257, 247, 258, 257, 253, 251, 243, 246, 258, 242, 243, 243, 252, 252, 239, 251, 259, 246, 258, 239, 246, 261 };
                type3[1] = new short[] { 263, 270, 265, 264, 276, 261, 269, 271, 275, 263, 270, 265, 278, 265, 263, 264, 276, 265, 279, 261, 269, 271, 259, 275, 281, 257, 279, 268, 257, 275, 275, 261, 270, 265, 272, 272, 257, 264, 261, 277, 274, 276, 261, 264, 276 };
                type3[2] = new short[] { 257, 243, 249, 239, 258, 257, 247, 251, 242, 253, 253, 245, 251, 253, 256, 244, 243, 242, 239, 251, 257, 263, 239, 261, 250, 239, 257, 247, 257, 257, 243, 241, 241, 259, 257, 239 };
                type3[3] = new short[] { 302, 291, 300, 297, 288, 286, 294, 300, 297, 305, 287, 294, 297, 290, 305, 287, 290, 302, 287, 286, 283, 300, 302, 297, 302, 302, 296, 283, 305, 307, 283, 295, 303, 297, 307, 302, 283, 290, 302, 289, 296, 291, 298, 294, 287, 290, 297, 301, 301, 291, 289, 303, 290, 295, 300, 283, 305, 283 };
                type3[4] = new short[] { 292, 279, 294, 294, 279, 276, 294, 283, 276, 275, 294, 293, 295, 284, 278, 286, 292, 289, 297, 279, 282, 294, 279, 285, 275, 287, 289, 294, 293, 283, 288, 275, 287, 295, 282, 281, 288, 283, 279, 276, 280, 289, 286, 275, 289, 281, 286, 275, 288, 283, 280, 279, 282, 294 };

                foreach (short[] type3check in type3)
                {
                    if (Enumerable.SequenceEqual(encrypted, type3check))
                    {
                        return true;
                    }
                }
                return false;
            }

            public short[] DecodeLicense(string input)
            {
                short[] encrypted = new short[input.Length];
                for (int x = 0; x < input.Length; x++)
                {
                    encrypted[input.Length - 1 - x] = (short)((short)input[x] + 70 + (input.Length * 2));
                }
                return encrypted;
            }
        }

        public string CheckLicense(LicenseType licenseType, out bool exception)
{
    try
    {
        string keyPath = type + "\\" + companyName + "\\" + softwareName;
        RegistryKey rk = Registry.CurrentUser.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadSubTree);
        string rkValue = rk == null ? string.Empty : rk.GetValue("license") == null ? string.Empty : rk.GetValue("license").ToString();
        if (rk != null) rk.Close();
        exception = false;
        if (rkValue != string.Empty)
        {
            if (licenseType == LicenseType.Title)
            {
                return CheckLicenseString(rkValue);
            }
            else
            {
                return (rkValue);
            }
        }
    }
    catch
    {
        exception = true;
    }
    return string.Empty;
}

public void CreateLicenseKey(string license, out bool exception)
{
    try
    {
        RegistryKey rk = Registry.CurrentUser.OpenSubKey(type, RegistryKeyPermissionCheck.ReadWriteSubTree);
        if (rk.GetValue(companyName) == null)
        {
            rk.CreateSubKey(companyName);
        }
        rk = rk.OpenSubKey(companyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
        if (rk.GetValue(softwareName) == null)
        {
            rk.CreateSubKey(softwareName);
        }
        rk = rk.OpenSubKey(softwareName, RegistryKeyPermissionCheck.ReadWriteSubTree);
        rk.SetValue("license", license);
        rk.Close();
        exception = false;
    }
    catch
    {
        exception = true;
    }
}

public void DeleteLicenseKey(out bool exception)
{
    try
    {
        string keyPath = type + "\\" + companyName + "\\" + softwareName;
        RegistryKey rk = Registry.CurrentUser.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
        rk.DeleteValue("license");
        rk.Close();
        exception = false;
    }
    catch
    {
        exception = true;
    }
}
    }
}
