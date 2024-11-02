using Microsoft.AspNetCore.Mvc;
using NetCorePrjBase.DL.PublicModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePrjBase.DL.ToolsBlu
{
    public static class SystemExtensions
    {
        public static bool IsNullOrEmpty(this string? Value)
        {
            try
            {
                if (Value is null || string.IsNullOrEmpty(Value) || string.IsNullOrWhiteSpace(Value)
                    || string.IsNullOrWhiteSpace(Value.Trim()))
                    return true;
                return false;
            }
            catch
            {
                return true;
            }
        }

        public static void AddError(this ErrorsVM res, string message)
        {
            if (res.LstErrors is null)
                res.LstErrors = new List<string>();
            res.LstErrors.Add(message);
        }
        public static void ExceptionError(this ErrorsVM res, Exception ex)
        {
            if (ex is not null)
            {
                res.Message = "خطا در اجرای برنامه";
                res.AddError(ex.Message);
                if (ex.InnerException is not null)
                    res.AddError(ex.InnerException.Message);
            }
        }

        public static DateTime ToPersianDateTime(this DateTime Value)
        {
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();

            return new DateTime(persianCalendar.GetYear(Value), persianCalendar.GetMonth(Value)
                , persianCalendar.GetDayOfMonth(Value), persianCalendar.GetHour(Value),
                persianCalendar.GetMinute(Value), persianCalendar.GetSecond(Value));
        }

        public static long Val64(this string? Value)
        {
            long num = -3333;
            long.TryParse(Value, out num);
            return num;
        }

        public static double Val72(this string? Value)
        {
            double num = -3333;
            double.TryParse(Value, out num);
            return num;
        }

        public static int Val32(this string? Value)
        {
            int num = -3333;
            int.TryParse(Value, out num);
            return num;
        }

        public static byte Val16(this string? Value)
        {
            byte num;
            byte.TryParse(Value, out num);
            return num;
        }
        public static int RandoNumber6()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            ParameterExpression param = expr1.Parameters[0];
            if (ReferenceEquals(param, expr2.Parameters[0]))
                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, expr2.Body), param);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, Expression.Invoke(expr2, param)), param);
        }

        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            ParameterExpression param = expr1.Parameters[0];
            if (ReferenceEquals(param, expr2.Parameters[0]))
                return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, expr2.Body), param);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, Expression.Invoke(expr2, param)), param);
        }

        public static string ToSplitArray(this List<int> array)
        {
            string ar = "";
            for (int i = 0; i < array.Count; i++)
            {
                if (i == array.Count - 1)
                    ar += $@"{array[i]}";
                else
                    ar += $@"{array[i]}, ";
            }
            return ar;
        }


        public static string EncryptString(this string plainText, string? key = null)
        {
            if (key == null || key.IsNullOrEmpty())
                key = ProjectConsts.KeyEncDec;
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(this string cipherText, string? key = null)
        {
            try
            {
                //cipherText= Convert.ToBase64String(Encoding.UTF8.GetBytes(cipherText));
                if (key == null || key.IsNullOrEmpty())
                    key = ProjectConsts.KeyEncDec;

                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch { return ""; }
        }

        public static string GetMD5(this string text)
        {
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            return sBuilder.ToString().ToUpper();
        }

        public static string GetBase64File(this string Path, string DefualtPath = "")
        {
            try
            {
                if (File.Exists(Path))
                {
                    var bytes = File.ReadAllBytes(Path);
                    return Convert.ToBase64String(bytes);
                }
                else
                {
                    if (File.Exists(DefualtPath))
                        return Convert.ToBase64String(File.ReadAllBytes(DefualtPath));
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        // An Extenstion Method that Takes a class and returns its properties and values as ExcelFile
        public static FileContentResult ExportToExcel<T>(this List<T> data, string sheetName = "Sheet", string fileName = "Data") where T : class
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(sheetName);
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

                var validProperties = new List<PropertyInfo>();
                foreach (var property in properties)
                {
                    if (data.Any(d => property.GetValue(d) != null))
                    {
                        validProperties.Add(property);
                    }
                }
                for (int i = 0; i < validProperties.Count; i++)
                {
                    var displayNameAttribute = validProperties[i].GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                    worksheet.Cells[1, i + 1].Value = displayNameAttribute?.DisplayName ?? validProperties[i].Name;
                }
                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < validProperties.Count; j++)
                    {
                        var value = validProperties[j].GetValue(data[i]);

                        if (value is DateTime dateTimeValue)
                        {
                            var persianDateTime = dateTimeValue.ToPersianDateTime().ToString();
                            worksheet.Cells[i + 2, j + 1].Value = persianDateTime;
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1].Value = value;
                        }
                    }
                }
                worksheet.Cells.AutoFitColumns();
                var fileBytes = package.GetAsByteArray();
                return new FileContentResult(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = $"{fileName}.xlsx"
                };
            }
        }

        public static string NormalizeNationalCode(this string code)
            => code.PadLeft(10, '0');

        public static string DeCryptPaymentParams(this string Value)
        {
            var decodedString = "";
            try
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    var decryptedData = rsa.Decrypt(Convert.FromBase64String(Value), false);
                    return Encoding.UTF8.GetString(decryptedData);
                }
            }
            catch (Exception ex)
            {
                decodedString = string.Empty;
            }
            return decodedString;
        }
        public static DateTime GetAge(this DateTime bithday)
            => Convert.ToDateTime(DateTime.Now - bithday);

        public static bool HasValueObj(this int? Value)
            => (Value is not null && Value.Value > 0);
        public static bool HasValueObj(this long? Value)
            => (Value is not null && Value.Value > 0);
        public static bool IsNullObj(this object? Value)
            => (Value is null || Value == null);

        public static DateTimeModelDTO ToDateTimeModelDTO(this DateTime bithday)
        {
            var age = new DateTime((DateTime.Now - bithday).Ticks);
            DateTimeModelDTO res = new(age.Year - 1, age.Month - 1, age.Day - 1);
            res.DateTime = age;
            return res;
        }
        public static DateTimeModelDTO GetModelDateTime(this DateTime dt)
        {
            DateTimeModelDTO res = new(dt.Year, dt.Month, dt.Day);
            res.DateTime = dt;
            return res;
        }

        public static bool CheckBetween(this DateTimeModelDTO dtm, DateTimeModelDTO dtm2, DateTimeModelDTO dtm1)
        {
            if (dtm.Year < dtm2.Year && dtm.Year > dtm1.Year) return true;
            else if (dtm.Year == dtm1.Year && dtm.Year == dtm2.Year && dtm.Month < dtm2.Month && dtm.Month > dtm2.Month) return true;
            else if (dtm.Year == dtm1.Year && dtm.Year == dtm2.Year && dtm.Month == dtm2.Month && dtm.Month == dtm2.Month && dtm.Day <= dtm2.Day && dtm1.Day <= dtm.Day) return true;
            return false;
        }

        public static bool CheckBetween(this decimal? Point, decimal PointMax, decimal PointMin)
        => Point >= PointMin && Point <= PointMax;

        public static DateTime ConvertTimeSpanToDateTime(this TimeSpan TimeSpan)
            => Convert.ToDateTime(TimeSpan);

        
    }
}
