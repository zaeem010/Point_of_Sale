using Microsoft.AspNetCore.Mvc;
using IMS.Models;
using System.Text.Json;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace IMS.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Add the Notification/Message to the returning view
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="result">Bolean, either success or error</param>
        /// <param name="delay">in miliseconds, how much longer the notification will show. Default 3.5 sec</param>
        public void AddNotificationToView(string message, bool result, int delay = 3500)
        {
            var ToasterResult = new ToasterResult()
            {
                Success = result,
                Message = message,
                Delay = delay
            };

            TempData["ToasterResult"] = JsonSerializer.Serialize(ToasterResult);
        }
        public string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            string new_word = Regex.Replace(words, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            return new_word;
        }


        public List<TranscationDetails> AddMasterintransactionid(VoucherSavemodel VoucherSavemodel)
        {
            var TranscationDetails = new List<TranscationDetails>();
            foreach (var t in VoucherSavemodel.TranscationDetailList)
            {
                
                TranscationDetails.Add(new TranscationDetails
                {
                    Id = t.Id,
                    Invid = VoucherSavemodel.dynamicid,
                    Vtype = t.Vtype,
                    TransDes = t.TransDes,
                    TransDate = t.TransDate,
                    ThirdLevelId = t.ThirdLevelId,
                    Dr = t.Dr,
                    Cr = t.Cr,
                    UserId = t.UserId,
                    HeadId=t.HeadId
                });
            }
            return TranscationDetails;
        }
        public List<VoucherDetail> AddMasterinVoucherdetail(VoucherSavemodel VoucherSavemodel)
        {
            var VoucherDetail = new List<VoucherDetail>();
            foreach (var t in VoucherSavemodel.VoucherMaster.VoucherDetail)
            {
                VoucherDetail.Add(new VoucherDetail
                {
                    Id = t.Id,
                    VoucherMasterId = VoucherSavemodel.dynamicid,
                    Amount = t.Amount,
                    Narration = t.Narration,
                    CleDate = t.CleDate,
                    CheckNo = t.CheckNo,
                    ThirdLevelId = t.ThirdLevelId,
                });
            }
            return VoucherDetail;
        }
    }
}
