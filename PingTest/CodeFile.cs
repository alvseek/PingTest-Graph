using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Reflection;

public class AppFunction
{ 
    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
    public enum LicenseType {Value, Title};
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
        donationLink = "www.indonesiamadjoe.com/?target=donation&source="+softwareName;
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

    public string CheckLicense(LicenseType licenseType, ref bool exception)
    {
        try
        {
            string keyPath = type + "\\" + companyName + "\\" + softwareName;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadSubTree);
            string rkValue = rk == null ? string.Empty : rk.GetValue("license") == null ? string.Empty : rk.GetValue("license").ToString();
            if (rk != null) rk.Close();
            if (rkValue != string.Empty)
            {
                exception = false;
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

    public void CreateLicenseKey(string license, ref bool exception)
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

    public void DeleteLicenseKey(ref bool exception)
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
