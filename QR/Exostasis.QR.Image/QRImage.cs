﻿using System;
using System.Collections;
using System.Collections.Generic;
using Exostasis.QR.Common.Image;
using System.Drawing;
using Exostasis.QR.Common;
using Exostasis.QR.Common.Enum;
using Exostasis.QR.DataMask;

namespace Exostasis.QR.Image
{
    public class QrImage
    {
        private int Version { get; }
        private int Scale { get; }
        private Module[,] _elements;

        private FinderPattern TopLeftFinderPattern { get; set; }
        private FinderPattern TopRightFinderPattern { get; set; }
        private FinderPattern BottomLeftFinderPattern { get; set; }

        private TimingPattern LeftTimingPattern { get; set; }
        private TimingPattern TopTimingPattern { get; set; }

        private List<Element> ExcludedElments { get; }

        private ErrorCorrectionLevel ErrorCorrectionLevel { get; }

        public QrImage(int version, int scale, List<BitArray> structuredArray, ErrorCorrectionLevel errorCorrectionLevel)
        {
            Version = version;
            Scale = scale;
            _elements = new Module[GetModuleSize(), GetModuleSize()];
            ErrorCorrectionLevel = errorCorrectionLevel;
            ExcludedElments = new List<Element>();           
            AddFinderPatterns();
            AddSeperators();
            AddAlignmentPatterns();
            AddTimingPatterns();
            AddDarkModule();
            WriteBitArray(structuredArray);

            var dataMasker = new DataMasker(Version, ExcludedElments, _elements, GetModuleSize());
            dataMasker.CalculateDataMask();
            WriteFormatStringAndVersionInformation(dataMasker.MaskVerion);
        }        

        private void AddAlignmentPatterns()
        {
            List<Cord> possibleCords = CalculateAlignmentPatternCords();

            possibleCords.ForEach(cord =>
            {
                for (int y = 0; y < AlignmentPattern.ModulesHeigh; ++y)
                {
                    for (int x = 0; x < AlignmentPattern.ModulesWide; ++x)
                    {
                        if (_elements[cord.X - AlignmentPattern.ModulesWide / 2 + x, cord.Y - AlignmentPattern.ModulesHeigh / 2 + y] 
                            != null)
                        {
                            return;
                        }
                    }
                }

                ExcludedElments.Add(new AlignmentPattern(cord, ref _elements));
            });          
        }

        private void AddDarkModule()
        {
            ExcludedElments.Add(new Module(new Cord(BottomLeftFinderPattern.TopRightCord.X + 1, BottomLeftFinderPattern.TopRightCord.Y - 1), 
                Color.Black, ref _elements));
        }

        private void AddFinderPatterns()
        {
            TopLeftFinderPattern = new FinderPattern(new Cord(0, 0), ref _elements);
            TopRightFinderPattern = new FinderPattern(new Cord(GetModuleSize() - FinderPattern.ModulesWide, 0), ref _elements);
            BottomLeftFinderPattern = new FinderPattern(new Cord(0, GetModuleSize() - FinderPattern.ModulesHeigh), ref _elements);

            ExcludedElments.Add(TopLeftFinderPattern);
            ExcludedElments.Add(TopRightFinderPattern);
            ExcludedElments.Add(BottomLeftFinderPattern);
        }

        private void AddSeperators()
        {        
            ExcludedElments.Add(new Seperator(new Cord(TopLeftFinderPattern.TopRightCord.X, TopLeftFinderPattern.TopRightCord.Y), 1,
                FinderPattern.ModulesHeigh + 1, ref _elements));
            ExcludedElments.Add(new Seperator(new Cord(TopLeftFinderPattern.BottomLeftCord.X, TopLeftFinderPattern.BottomLeftCord.Y),
                FinderPattern.ModulesWide, 1, ref _elements));

            ExcludedElments.Add(new Seperator(new Cord(TopRightFinderPattern.TopLeftCord.X - 1, TopRightFinderPattern.TopLeftCord.Y), 1,
                FinderPattern.ModulesHeigh + 1, ref _elements));
            ExcludedElments.Add(new Seperator(new Cord(TopRightFinderPattern.BottomLeftCord.X, TopRightFinderPattern.BottomLeftCord.Y),
                FinderPattern.ModulesWide, 1, ref _elements));

            ExcludedElments.Add(new Seperator(new Cord(BottomLeftFinderPattern.TopLeftCord.X, BottomLeftFinderPattern.TopLeftCord.Y - 1) ,
                FinderPattern.ModulesWide, 1, ref _elements));
            ExcludedElments.Add(new Seperator(new Cord(BottomLeftFinderPattern.TopRightCord.X, BottomLeftFinderPattern.TopRightCord.Y - 1), 1,
                FinderPattern.ModulesHeigh + 1, ref _elements));
        }

        private void AddTimingPatterns()
        {            
            TopTimingPattern = 
                new TimingPattern(new Cord(TopLeftFinderPattern.BottomRightCord.X + 1, TopLeftFinderPattern.BottomRightCord.Y - 1), 
                TopRightFinderPattern.BottomLeftCord.X - TopLeftFinderPattern.BottomRightCord.X, 1, ref _elements);

            LeftTimingPattern = 
                new TimingPattern(new Cord(TopLeftFinderPattern.BottomRightCord.X - 1, TopLeftFinderPattern.BottomRightCord.Y + 1),
                1, BottomLeftFinderPattern.TopRightCord.Y - 1 - TopLeftFinderPattern.BottomRightCord.Y - 1, ref _elements);

            ExcludedElments.Add(TopTimingPattern);
            ExcludedElments.Add(LeftTimingPattern);
        }        

        private List<Cord> CalculateAlignmentPatternCords()
        {
            List<Cord> cords = new List<Cord>();
            int[] possibleCords = Constants.AlignmentCords[Version]?.Values;

            if (possibleCords != null)
            {
                for (int i = 0; i < possibleCords.Length; ++i)
                {
                    for (int j = i; j < possibleCords.Length; ++j)
                    {
                        cords.Add(new Cord(possibleCords[i], possibleCords[j]));
                        if (possibleCords[i] != possibleCords[j])
                        {
                            cords.Add(new Cord(possibleCords[j], possibleCords[i]));
                        }
                    }
                }
            }

            return cords;
        }

        private int GetModuleSize()
        {
            return Version * 4 + 21;
        }

        private void WriteFormatStringAndVersionInformation(int maskVersion)
        {
            int errorCorrectionValue = 0;

            if (ErrorCorrectionLevel == ErrorCorrectionLevel.H)
            {
                errorCorrectionValue = 2;
            }
            else if (ErrorCorrectionLevel == ErrorCorrectionLevel.L)
            {
                errorCorrectionValue = 1;
            }
            else if (ErrorCorrectionLevel == ErrorCorrectionLevel.Q)
            {
                errorCorrectionValue = 3;
            }

            byte versionString = Convert.ToByte((errorCorrectionValue << 3) + maskVersion);
        }

        public void WriteImage(string filename)
        {
            using (var qrBitmap = new Bitmap(GetModuleSize() * Scale, GetModuleSize() * Scale))
            {
                for (int y = 0; y < GetModuleSize(); ++y)
                {
                    for (int x = 0; x < GetModuleSize(); ++x)
                    {
                        if (_elements[x, y] != null)
                        {
                            for (int i = 0; i < Scale; ++i)
                            {
                                for (int j = 0; j < Scale; ++j)
                                {
                                    qrBitmap.SetPixel(x * Scale + i, y * Scale + j, _elements[x, y].PixelColor);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < Scale; ++i)
                            {
                                for (int j = 0; j < Scale; ++j)
                                {
                                    qrBitmap.SetPixel(x * Scale + i, y * Scale + j, Color.Blue);
                                }
                            }
                        }
                    }
                }
                qrBitmap.Save(filename);
            }
        }

        private void WriteBitArray(List<BitArray> structuredArray)
        {
            int x = GetModuleSize() - 1;
            int y = GetModuleSize() - 1;
            int listElement = 0;
            int listIndex = 0;
            bool goingUp = true;

            while (listIndex < structuredArray.Count)
            {
                if (x >= TopRightFinderPattern.BottomLeftCord.X - 1 && x < TopRightFinderPattern.BottomRightCord.X && 
                    y == TopRightFinderPattern.BottomLeftCord.Y + 1)
                {
                    new Module(new Cord(x, y), Color.White, ref _elements);
                }
                else if (x >= TopLeftFinderPattern.BottomLeftCord.X && x <= TopLeftFinderPattern.BottomRightCord.X + 1 && 
                    y == TopLeftFinderPattern.BottomLeftCord.Y + 1)
                {
                    new Module(new Cord(x, y), Color.White, ref _elements);
                }
                else if (x == TopLeftFinderPattern.TopRightCord.X + 1 && y >= TopLeftFinderPattern.TopRightCord.Y && 
                    y <= TopLeftFinderPattern.BottomRightCord.Y + 1)
                {
                    new Module(new Cord(x, y), Color.White, ref _elements);
                }
                else if (x == BottomLeftFinderPattern.TopRightCord.X + 1 && y >= BottomLeftFinderPattern.TopRightCord.Y &&
                    y < BottomLeftFinderPattern.BottomRightCord.Y)
                {
                    new Module(new Cord(x, y), Color.White, ref _elements);
                }
                else if (Version >= 6 && x >= TopRightFinderPattern.TopLeftCord.X - 4 && x < TopRightFinderPattern.TopLeftCord.X - 1 &&
                    y >= TopRightFinderPattern.TopLeftCord.Y && y < TopRightFinderPattern.BottomLeftCord.Y)
                {
                    new Module(new Cord(x, y), Color.White, ref _elements);
                }
                else if (Version >= 6 && x >= BottomLeftFinderPattern.TopLeftCord.X && x < BottomLeftFinderPattern.TopRightCord.X - 1 &&
                    y >= BottomLeftFinderPattern.TopLeftCord.Y - 4 && y < BottomLeftFinderPattern.TopLeftCord.Y - 1)
                {
                    new Module(new Cord(x, y), Color.White, ref _elements);
                }
                else if (_elements[x, y] == null)
                {
                    new Module(new Cord(x, y), structuredArray[listIndex][listElement] ? Color.Black : Color.White, ref _elements);

                    if (listElement == structuredArray[listIndex].Count - 1)
                    {
                        ++listIndex;
                        listElement = 0;
                    }
                    else
                    {
                        ++listElement;
                    }
                }

                if (x - 1 == LeftTimingPattern.TopLeftCord.X && y == 0)
                {
                    x -= 2;
                    goingUp = !goingUp;
                }
                else if (y == 0 && x <= 5 && x % 2 == 0 && goingUp)
                {
                    --x;
                    goingUp = !goingUp;
                }
                else if (y == GetModuleSize() - 1 && x <= 5 && x % 2 == 0 && !goingUp)
                {
                    --x;
                    goingUp = !goingUp;
                }                
                else if (x <= 5 && x % 2 == 1)
                {
                    --x;                
                }
                else if (x <= 5 && x % 2 == 0 && goingUp)
                {
                    ++x;
                    --y;
                }
                else if (x <= 5 && x % 2 == 0)
                {
                    ++x;
                    ++y;
                }
                else if (y == GetModuleSize() - 1 && x % 2 == 1 && !goingUp)
                {
                    --x;
                    goingUp = !goingUp;
                }
                else if (y == 0 && x % 2 == 1 && goingUp)
                {
                    --x;
                    goingUp = !goingUp;
                }
                else if (x % 2 == 0)
                {
                    --x;
                }
                else if (goingUp)
                {
                    ++x;
                    --y;
                }
                else
                {
                    ++x;
                    ++y;
                }                                
            }
        }
    }
}