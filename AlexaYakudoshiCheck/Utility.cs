using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlexaYakudoshiCheck
{
    public partial class Function
    {
        string ComposeYakudoshiCheckResultText(int[] yakudoshiList, int birthYear,Gender gender)
        {
            string speechText = "";

            var thisYear = DateTime.Today.Year;
            var kazoeAge = thisYear - birthYear + 1;
            var genderString = gender == Gender.Male ? "男性" : "女性";

            if (yakudoshiList.Any(yaku => yaku == kazoeAge))
            {
                //厄年なら、それが西暦何年なのかを計算する。
                //var yakuSeireki = birthYear + kazoeAge - 1;
                speechText = $"{birthYear}年生まれの{genderString}は、今年が厄年です。数えで{kazoeAge}歳です。";
            }
            else
            {
                //一番近い厄年を教える。西暦と数え年
                var nextYaku =
                    yakudoshiList.Where(yaku => yaku > kazoeAge).FirstOrDefault();

                if (nextYaku != 0)
                {
                    var nextYakuSeireki = birthYear + nextYaku - 1;
                    speechText =
                        $"{birthYear}年生まれの{genderString}の、次の厄年は{nextYakuSeireki}年で、{nextYakuSeireki - thisYear}年後です。数えで{nextYaku}歳です。";
                }
                else
                {
                    var lastYakuSeireki = birthYear+yakudoshiList.Max()-1;
                    speechText = $"{birthYear}年生まれの{genderString}は、{lastYakuSeireki}年に最後の厄年を迎えました。";
                }
            }

            return speechText;
        }


        private enum Gender
        {
            Male,
            Female
        }
    }
}
