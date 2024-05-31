using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace RestaurantMenu.Helpers
{
    public class Global
    {
        /// <summary>
        /// Decrypt Method
        /// </summary>
        /// <param name="input">Input Text To Decrypt</param>
        /// <returns>Decrypted Text</returns>
        public static string Decrypt(string input)
        {
            string encryptedText = input;

            encryptedText = encryptedText.Replace("*1*2*", "+");

            return (Encryption.Decrypt(encryptedText));
        }

        /// <summary>
        /// Encrypt Method
        /// </summary>
        /// <param name="input">Input Text To Encrypt</param>
        /// <returns>Encrypted Text</returns>
        public static string Encrypt(string input)
        {
            string sampleText = string.Empty;

            sampleText = Encryption.Encrypt(input);

            sampleText = sampleText.Replace("+", "*1*2*");

            return (sampleText);
        }

        public DateTime UTCtoEDT(DateTime dt)
        {
            try
            {
                //https://stackoverflow.com/questions/7908343/list-of-timezone-ids-for-use-with-findtimezonebyid-in-c/17300423
                TimeZoneInfo targetZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                DateTime newDT = TimeZoneInfo.ConvertTimeFromUtc(dt, targetZone);
                return newDT;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        static Random rd = new Random();
        public string GenerateStr(int stringLength)
        {
            try
            {
                const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
                char[] chars = new char[stringLength];

                for (int i = 0; i < stringLength; i++)
                {
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                }

                return new string(chars);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Random random = new Random();
        //int randomNumber = random.Next(0, 1000);

        public string RemoveSpecialCharacters(string input)
        {
            return Regex.Replace(input, @"(@|&|'|\(|\)|<|>|#|,|-|=)", "");
        }
        public string RemoveSpecialCharactersFromEmail(string input)
        {
            return Regex.Replace(input, @"(&|'|\(|\)|<|>|#|,|=)", "");
        }
    }
}