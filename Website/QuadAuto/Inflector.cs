// Copyright (c)  2012 QuadAutomotive Group.
// All rights reserved.
// 
// Redistribution and use in source and binary forms are permitted
// provided that the above copyright notice and this paragraph are
// duplicated in all such forms and that any documentation,
// advertising materials, and other materials related to such
// distribution and use acknowledge that the software was developed
// by the <organization>.  The name of the
// University may not be used to endorse or promote products derived
// from this software without specific prior written permission.
// THIS SOFTWARE IS PROVIDED ``AS IS'' AND WITHOUT ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace QuadAuto
{
    public static class Inflector
    {
        public const string UpperCase = @"^[A-Z]+$";
        private static readonly List<InflectorRule> Plurals = new List<InflectorRule>();
        private static readonly List<InflectorRule> Singulars = new List<InflectorRule>();
        private static readonly List<string> Uncountables = new List<string>();

        static Inflector()
        {
            AddPluralRule("$", "s");
            AddPluralRule("s$", "s");
            AddPluralRule("(ax|test)is$", "$1es");
            AddPluralRule("(octop|vir)us$", "$1i");
            AddPluralRule("(alias|status)$", "$1es");
            AddPluralRule("(bu)s$", "$1ses");
            AddPluralRule("(buffal|tomat)o$", "$1oes");
            AddPluralRule("([ti])um$", "$1a");
            AddPluralRule("sis$", "ses");
            AddPluralRule("(?:([^f])fe|([lr])f)$", "$1$2ves");
            AddPluralRule("(hive)$", "$1s");
            AddPluralRule("([^aeiouy]|qu)y$", "$1ies");
            AddPluralRule("(x|ch|ss|sh)$", "$1es");
            AddPluralRule("(matr|vert|ind)ix|ex$", "$1ices");
            AddPluralRule("([m|l])ouse$", "$1ice");
            AddPluralRule("^(ox)$", "$1en");
            AddPluralRule("(quiz)$", "$1zes");

            AddSingularRule("s$", String.Empty);
            AddSingularRule("ss$", "ss");
            AddSingularRule("(n)ews$", "$1ews");
            AddSingularRule("([ti])a$", "$1um");
            AddSingularRule("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
            AddSingularRule("(^analy)ses$", "$1sis");
            AddSingularRule("([^f])ves$", "$1fe");
            AddSingularRule("(hive)s$", "$1");
            AddSingularRule("(tive)s$", "$1");
            AddSingularRule("([lr])ves$", "$1f");
            AddSingularRule("([^aeiouy]|qu)ies$", "$1y");
            AddSingularRule("(s)eries$", "$1eries");
            AddSingularRule("(m)ovies$", "$1ovie");
            AddSingularRule("(x|ch|ss|sh)es$", "$1");
            AddSingularRule("([m|l])ice$", "$1ouse");
            AddSingularRule("(bus)es$", "$1");
            AddSingularRule("(o)es$", "$1");
            AddSingularRule("(shoe)s$", "$1");
            AddSingularRule("(cris|ax|test)es$", "$1is");
            AddSingularRule("(octop|vir)i$", "$1us");
            AddSingularRule("(alias|status)$", "$1");
            AddSingularRule("(alias|status)es$", "$1");
            AddSingularRule("^(ox)en", "$1");
            AddSingularRule("(vert|ind)ices$", "$1ex");
            AddSingularRule("(matr)ices$", "$1ix");
            AddSingularRule("(quiz)zes$", "$1");

            AddIrregularRule("person", "people");
            AddIrregularRule("man", "men");
            AddIrregularRule("child", "children");
            AddIrregularRule("sex", "sexes");
            AddIrregularRule("tax", "taxes");
            AddIrregularRule("move", "moves");

            AddUnknownCountRule("equipment");
            AddUnknownCountRule("information");
            AddUnknownCountRule("rice");
            AddUnknownCountRule("money");
            AddUnknownCountRule("species");
            AddUnknownCountRule("series");
            AddUnknownCountRule("fish");
            AddUnknownCountRule("sheep");
        }

        private static void AddIrregularRule(string singular, string plural)
        {
            AddPluralRule(String.Concat("(", singular[0], ")", singular.Substring(1), "$"),
                          String.Concat("$1", plural.Substring(1)));
            AddSingularRule(String.Concat("(", plural[0], ")", plural.Substring(1), "$"),
                            String.Concat("$1", singular.Substring(1)));
        }

        private static void AddUnknownCountRule(string word)
        {
            Uncountables.Add(word.ToLower());
        }

        private static void AddPluralRule(string rule, string replacement)
        {
            Plurals.Add(new InflectorRule(rule, replacement));
        }

        private static void AddSingularRule(string rule, string replacement)
        {
            Singulars.Add(new InflectorRule(rule, replacement));
        }

        public static string MakePlural(this string word)
        {
            return ApplyRules(Plurals, word);
        }

        public static string MakeSingular(this string word)
        {
            return ApplyRules(Singulars, word);
        }

        private static string ApplyRules(IList<InflectorRule> rules, string word)
        {
            string result = word;
            if (!Uncountables.Contains(word.ToLower()))
            {
                for (int i = rules.Count - 1; i >= 0; i--)
                {
                    string currentPass = rules[i].Apply(word);
                    if (currentPass != null)
                    {
                        result = currentPass;
                        break;
                    }
                }
            }
            return result;
        }

        public static string ToTitleCase(this string word)
        {
            return Regex.Replace(ToHumanCase(AddUnderscores(word)), @"\b([a-z])",
                                 match => match.Captures[0].Value.ToUpper());
        }

        public static string ToHumanCase(this string lowercaseAndUnderscoredWord)
        {
            return MakeInitialCaps(Regex.Replace(lowercaseAndUnderscoredWord, @"_", " "));
        }

        public static string ToProper(this string sourceString)
        {
            string propertyName = sourceString.ToPascalCase();
            return propertyName;
        }

        public static string ToPascalCase(this string lowercaseAndUnderscoredWord)
        {
            return ToPascalCase(lowercaseAndUnderscoredWord, true);
        }

        public static string ToPascalCase(this string text, bool removeUnderscores)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            text = text.Replace("_", " ");
            string joinString = removeUnderscores ? String.Empty : "_";
            string[] words = text.Split(' ');
            if (words.Length > 1 || words[0].IsUpperCase())
            {
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length > 0)
                    {
                        string word = words[i];
                        string restOfWord = word.Substring(1);

                        if (restOfWord.IsUpperCase())
                            restOfWord = restOfWord.ToLower(CultureInfo.CurrentUICulture);

                        char firstChar = char.ToUpper(word[0], CultureInfo.CurrentUICulture);
                        words[i] = String.Concat(firstChar, restOfWord);
                    }
                }
                return String.Join(joinString, words);
            }
            return String.Concat(words[0].Substring(0, 1).ToUpper(CultureInfo.CurrentUICulture), words[0].Substring(1));
        }

        public static string ToCamelCase(this string lowercaseAndUnderscoredWord)
        {
            return MakeInitialLowerCase(ToPascalCase(lowercaseAndUnderscoredWord));
        }

        public static string AddUnderscores(this string pascalCasedWord)
        {
            return
                Regex.Replace(
                    Regex.Replace(Regex.Replace(pascalCasedWord, @"([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])",
                                  "$1_$2"), @"[-\s]", "_").ToLower();
        }

        public static string MakeInitialCaps(this string word)
        {
            return String.Concat(word.Substring(0, 1).ToUpper(), word.Substring(1).ToLower());
        }

        public static string MakeInitialLowerCase(this string word)
        {
            return String.Concat(word.Substring(0, 1).ToLower(), word.Substring(1));
        }

        public static string AddOrdinalSuffix(this string number)
        {
            if (number.IsStringNumeric())
            {
                int n = int.Parse(number);
                int nMod100 = n%100;

                if (nMod100 >= 11 && nMod100 <= 13)
                    return String.Concat(number, "th");

                switch (n%10)
                {
                    case 1:
                        return String.Concat(number, "st");
                    case 2:
                        return String.Concat(number, "nd");
                    case 3:
                        return String.Concat(number, "rd");
                    default:
                        return String.Concat(number, "th");
                }
            }
            return number;
        }

        public static bool IsStringNumeric(this string str)
        {
            double result;
            return (double.TryParse(str, NumberStyles.Float, NumberFormatInfo.CurrentInfo, out result));
        }

        public static bool IsUpperCase(this string inputString)
        {
            return Regex.IsMatch(inputString, UpperCase);
        }

        public static string ConvertUnderscoresToDashes(this string underscoredWord)
        {
            return underscoredWord.Replace('_', '-');
        }

        private class InflectorRule
        {
            private readonly Regex _regex;
            private readonly string _replacement;

            public InflectorRule(string regexPattern, string replacementText)
            {
                _regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
                _replacement = replacementText;
            }

            public string Apply(string word)
            {
                if (!_regex.IsMatch(word))
                    return null;

                string replace = _regex.Replace(word, _replacement);
                if (word == word.ToUpper())
                    replace = replace.ToUpper();

                return replace;
            }
        }
    }
}