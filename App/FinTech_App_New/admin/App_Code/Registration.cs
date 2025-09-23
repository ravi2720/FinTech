using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for Registration
/// </summary>
public class Registration :  IValidatableObject
{
    public Registration()
    {
        ID = 0;
    }
    
    public int ID { get; set; }
    public int RoleID { get; set; }
    public string CityName { get; set; }
    public int TitleID { get; set; }
    public string Name { get; set; }
    public string ShopName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string AlterNativeMobile { get; set; }
    public string Aadhar { get; set; }
   
    public string Pan { get; set; }
    public int GenderID { get; set; }
    public int LoginPin { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string PinCode { get; set; }
    public string DOB { get; set; }
    public int CityID { get; set; }
    public int StateID { get; set; }
    public int PackageID { get; set; }
    public int ParentID { get; set; }
    public bool IsActive { get; set; }
    //public System.DateTime CreateDate { get; set; }
    //public System.DateTime UpdatedDate { get; set; }

    public bool IsAnyNullOrEmpty(object myObject)
    {
        //foreach (PropertyInfo pi in myObject.GetType().GetProperties())
        //{
        //    if (pi.PropertyType == typeof(string))
        //    {
        //        string value = (string)pi.GetValue(myObject);
        //        if (string.IsNullOrEmpty(value))
        //        {
        //            return false;
        //        }
        //    }
        //}
        return true;

    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Pan))
        {
            yield return new ValidationResult("Can't Null");
        }
    }
}