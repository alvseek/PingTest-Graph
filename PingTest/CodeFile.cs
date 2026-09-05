using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
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

    /// <summary>
    /// Validates a donation licence key.
    ///
    /// What is stored below is not a key but the SHA-256 digest of a key wrapped
    /// in a salt token at both ends, so this source carries nothing readable.
    ///
    /// This is a courtesy check and not a security boundary. PingTest Graph is
    /// free and open source, so anyone can build it with the check removed. Its
    /// only job is to remember that someone donated, and skip the donation
    /// prompt for them.
    /// </summary>
    public static class LicenseVerifier
    {
        /// <summary>Wrapped around the key at both ends before hashing.</summary>
        private const string SaltToken = "\"Baha'i\"";

        public const string TierContributor = "Contributor";
        public const string TierSponsor = "Sponsor";
        public const string TierPhilanthropist = "The Great Philanthropist";

        private static readonly string[] ContributorDigests =
        {
            "d6057ea72bf6e1e473ef289ef8414527371a0321ce6ad48534a1803ffe533808",
            "9698f7316cff17cbff7b31db212e7e176560ec9aed16a08bb6d85c74445061e7",
            "e0d28e8f79668ca18551d4d4a24ed0628b2cd43f2a3a3b43df5ae8a4fafae8ed",
            "1099b82c24a7464a3be1a3aa7dce6f5ea570ad88c2d2a2ef9cdcbab47f38fc21",
            "0ecb6a19750c30914fb2a3d8f42ad6f1e34476c0bcafa3343c3929fdae5db087",
        };

        private static readonly string[] SponsorDigests =
        {
            "9776ab92d357744def7ed6291b5ba07b9123cb7ecf207d9b8256a41bafa42569",
            "48b10d442d95d9a355c44ddad883dbd77af73c8fac8bca8ce4545be2458af458",
            "732c253a364bf61e449696f66daa36dda35481b5e4e38c7708603ed04c2b4bd2",
            "9658f4ceb5b0a4b8588e0973590287e7b2a944b3371c283239e6047479b44007",
            "a9c312a9f136792865f836519ec1a729d213ec66e58b758575a8934982cc3029",
        };

        private static readonly string[] PhilanthropistDigests =
        {
            "f65bcea9b711535ac1b067f8de1b61aed87ecb311bae67cf0c552f09c3dce6b3",
            "04a7bd874f7d91daf032223163aa77a4a7263cabb1933eca551dfc2958cd7f79",
            "4b28ee574af4f354c451c23d4e80def2d1e4e736e283a57a4e0d8bbf8fb13d26",
            "5b785b1e245a8ec27f13656d472fee6f9b56b3a43b7004a1bc579cb74486986d",
            "23aae21c193fc4a498a271d3931903b76430f06e12bcf7a65021d8db6d1f811b",
        };

        /// <summary>
        /// Returns the tier name for a valid key, or an empty string if the key
        /// is not recognised.
        /// </summary>
        public static string Verify(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return string.Empty;

            string candidate = Digest(key.Trim());

            if (Contains(ContributorDigests, candidate)) return TierContributor;
            if (Contains(SponsorDigests, candidate)) return TierSponsor;
            if (Contains(PhilanthropistDigests, candidate)) return TierPhilanthropist;

            return string.Empty;
        }

        /// <summary>Lowercase hex SHA-256 of SaltToken + key + SaltToken.</summary>
        private static string Digest(string key)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] salted = Encoding.UTF8.GetBytes(SaltToken + key + SaltToken);
                byte[] hash = sha256.ComputeHash(salted);

                StringBuilder hex = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash) hex.Append(b.ToString("x2"));
                return hex.ToString();
            }
        }

        private static bool Contains(string[] digests, string candidate)
        {
            foreach (string known in digests)
            {
                if (string.Equals(known, candidate, StringComparison.Ordinal)) return true;
            }
            return false;
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
            return LicenseVerifier.Verify(license);
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
