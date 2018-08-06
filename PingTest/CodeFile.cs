using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

public class UIFunction
{
    public enum UIPosition { Center, Top, Bottom, Right, Left, TopRight, TopLeft, BottomRight, BottomLeft};

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
