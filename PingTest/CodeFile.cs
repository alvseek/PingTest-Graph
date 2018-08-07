using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PingTest {

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

            private void ReShowForm (Form openForm)
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
            switch (license)
            {
                case "honestymakesthebestoutofyou":
                case "kindnessisatwowaygiving":
                case "hardworkneverlies":
                case "truelovelastforever":
                case "evenasmallkindnesscount":
                    return "Contributor";
                case "wherethereislovenothingistoomuchtroubleandthereisalwaystime":
                case "ifyoueverfeelalonethemedicineistomakeotherpeoplefeelsaccompanied":
                case "everytearscountbuteveryangereraseit":
                case "thosewhoactpaidhigherthanthosewhotalkbutthosewhodobothgetmostofit":
                case "behappybehappybefullofjoy":
                    return "Sponsor";
                case "whathumanneedthemostisthewarmofheart":
                case "thetruehappinessalwayscomewithgivingsomething":
                case "asuccessisalwaysmadefromgoodmistakes":
                case "awarmhugissohelpingthatyoumaywanttotradethewholeworldforit":
                case "thefinalgoalofbeinghumanistomaketheworldjustabitbetter":
                    return "The Great Philanthropist";
            }
            return string.Empty;
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
