using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp
{
    internal class PassportData
    {
        private int _birthYear = -1;
        private int _issueYear = -1;
        private int _expirationYear = -1;
        private string _height;
        private string _hairColor;
        private string _eyeColor;
        private string _passportId;
        private string _countryId;
        private string _originalData = "";

        public void AddStringData(string data)
        {
            _originalData += data;
        }

        private void ValidateBirthYear(string data)
        {
            try
            {
                var year = int.Parse(data);
                if (year >= 1920 && year <= 2002)
                {
                    _birthYear = year;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ValidateIssueYear(string data)
        {
            try
            {
                var year = int.Parse(data);
                if (year >= 2010 && year <= 2020)
                {
                    _issueYear = year;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ValidateExpirationYear(string data)
        {
            try
            {
                var year = int.Parse(data);
                if (year >= 2020 && year <= 2030)
                {
                    _expirationYear = year;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ValidateHeight(string data)
        {
            try
            {
                // cm
                if (data.Length == 5)
                {
                    var value = int.Parse(data.Substring(0, 3));
                    var unit = data.Substring(3);

                    if (unit == "cm" && value >= 150 && value <= 193)
                    {
                        _height = data;
                    }
                }
                // in
                else if (data.Length == 4)
                {
                    var value = int.Parse(data.Substring(0, 2));
                    var unit = data.Substring(2);

                    if (unit == "in" && value >= 59 && value <= 76)
                    {
                        _height = data;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ValidateHairColor(string data)
        {
            if (data[0] != '#') return;

            var charArray = data.Substring(1).ToCharArray();
            if (charArray.Any(c => !((c >= 48 && c <= 57) || (c >= 97 && c <= 102))))
            {
                return;
            }
            _hairColor = data;
        }

        private void ValidateEyeColor(string data)
        {
            if (data == "amb" || data == "blu" || data == "brn" || data == "gry" || data == "grn" || data == "hzl" ||
                data == "oth")
            {
                _eyeColor = data;
            }
        }

        private void ValidatePassportId(string data)
        {
            try
            {
                if (data.Length != 9) return;

                var _ = int.Parse(data);
                _passportId = data;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ValidateCountryId(string data)
        {
            _countryId = data;
        }

        public void ParseRawData()
        {
            var pieces = _originalData.Split(" ");

            foreach (var piece in pieces)
            {
                if (piece == "") continue;

                var fieldName = piece.Substring(0, 3);
                var fieldData = piece.Substring(4);
                switch (fieldName)
                {
                    case "byr":
                        ValidateBirthYear(fieldData);
                        break;
                    case "iyr":
                        ValidateIssueYear(fieldData);
                        break;
                    case "eyr":
                        ValidateExpirationYear(fieldData);
                        break;
                    case "hgt":
                        ValidateHeight(fieldData);
                        break;
                    case "hcl":
                        ValidateHairColor(fieldData);
                        break;
                    case "ecl":
                        ValidateEyeColor(fieldData);
                        break;
                    case "pid":
                        ValidatePassportId(fieldData);
                        break;
                    case "cid":
                        ValidateCountryId(fieldData);
                        break;
                }
            }
        }

        public bool Validate()
        {
            return _birthYear != -1 && _issueYear != -1 && _expirationYear != -1 && _height != null &&
                   _hairColor != null && _eyeColor != null && _passportId != null;
        }
    }

    public static class Dia04
    {
        public static int Puzzle(IEnumerable<string> lines)
        {
            var validCount = 0;
            var tempPassportData = new PassportData();

            foreach (var line in lines)
            {
                if (line != "")
                {
                    tempPassportData.AddStringData(line + " ");
                }
                else
                {
                    tempPassportData.ParseRawData();
                    if (tempPassportData.Validate())
                    {
                        validCount++;
                    }

                    tempPassportData = new PassportData();
                }
            }

            return validCount;
        }
    }
}