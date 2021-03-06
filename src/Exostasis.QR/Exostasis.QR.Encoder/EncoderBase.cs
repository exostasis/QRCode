﻿/*
 * Copyright 2016 Shawn Abtey. This source code is protected under the GNU General Public License 
 *  This file is part of Exostasis.QR.
 *  
 *  Exostasis.QR is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the 
 *  Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 *  
 *  Exostasis.QR is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of 
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
 *  
 *  You should have received a copy of the GNU General Public License along with Exostasis.QR.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exostasis.QR.Common;
using Exostasis.QR.Common.Enum;

namespace Exerostasis.QR.Encoder
{
    public abstract class EncoderBase
    {
        private readonly byte[] _padBytes = {0xEC, 0x11}; 

        public int Version { get; protected set; }

        protected BitArray CharacterCountIndicator { get; set; }

        protected BitArray ModeIndicator { get; set; }

        protected byte[] EncodedBytes { get; private set; }

        protected string UnencodedString { get; set; }

        public ErrorCorrectionLevel ErrorCorrectionLevel { get; protected set; }

        protected int DataPerBitString { get; set; }

        protected int BitsPerBitString { get; set; }

        protected int BitsPerCharacterCountIndicator { get; set; }

        protected int MaximumPossibleCharacterCount { get; set; }

        protected abstract List<BitArray> Encode();

        protected abstract void DetermineBitsPerCharacterCountIndicator();

        private int GetRequiredDataBits ()
        {
            return 8 * Constants.CodewordTable[Version, (int)ErrorCorrectionLevel];
        }

        private int GetDifferenceBetweenRequiredVsActual (List<BitArray> bitArray)
        {
            return GetRequiredDataBits() - CalculateListBitArrayDataBitCount(bitArray);
        }

        private int CalculateListBitArrayDataBitCount(List<BitArray> bitArray)
        {
            var sum = 0;

            foreach (var tempBitArray in bitArray)
            {
                sum += tempBitArray.Length;
            }

            return sum;
        }

        private void Terminate(List<BitArray> bitArray)
        {
            var differenceBetweenRequiredVsActual = GetDifferenceBetweenRequiredVsActual(bitArray);
            if (differenceBetweenRequiredVsActual >= 4)
            {
                bitArray.Add(new BitArray(4));
            }
            else if (differenceBetweenRequiredVsActual < 4 && differenceBetweenRequiredVsActual > 0)
            {
                bitArray.Add(new BitArray(differenceBetweenRequiredVsActual));
            }
        }

        private void MakeMultipleOf8(List<BitArray> bitArray)
        {
            var dataBitCount = CalculateListBitArrayDataBitCount(bitArray);
            var numberOf0SToAdd = 8 - dataBitCount % 8;

            if (numberOf0SToAdd > 0)
            {
                bitArray.Add(new BitArray(numberOf0SToAdd));
            }
        }

        private void Pad(List<BitArray> bitArray)
        {

            var numberOfBytesToAdd = GetDifferenceBetweenRequiredVsActual(bitArray) / 8;

            for(var i = 0; i < numberOfBytesToAdd; ++ i)
            {
                bitArray.Add(new BitArray(BitConverter.GetBytes(_padBytes[i % 2])));
                bitArray.Last().Length = 8;
            }
        }

        private byte[] ConvertListBitArrayToByteArray(List<BitArray> bitArray)
        {
            var bytes = new List<byte>();
            var currentSpot = 7;
            var tempByte = 0;

            foreach (var array in bitArray)
            {
                for (var i = array.Count - 1; i >= 0; i--)
                {         
                    if (array[i])
                    {
                        tempByte = tempByte | (1 << currentSpot);
                    }
                    --currentSpot;
                    if (currentSpot == -1)
                    {
                        bytes.Add(Convert.ToByte(tempByte));
                        tempByte = 0;
                        currentSpot = 7;
                    }
                }
            }

            return bytes.ToArray();
        }

        protected void DetermineMinimumVersionAndMaximumErrorCorrection()
        {
            var maximumLength = 0;

            for (var i = (int)ErrorCorrectionLevel.H; i >= 0; --i)
            {
                ErrorCorrectionLevel = (ErrorCorrectionLevel)i;

                for (var j = 0; j < 40; ++j)
                {
                    Version = j;

                    DetermineBitsPerCharacterCountIndicator();

                    maximumLength = GetMaximumCharacterCount();

                    if (maximumLength >= UnencodedString.Length)
                    {
                        break;
                    }
                }

                if (maximumLength >= UnencodedString.Length)
                {
                    break;
                }
            }
        }

        protected int GetMaximumCharacterCount()
        {
            return (GetRequiredDataBits() - 4 - BitsPerCharacterCountIndicator) * DataPerBitString / BitsPerBitString;
        }

        public EncoderBase(string unencodedString)
        {
            UnencodedString = unencodedString;
            CharacterCountIndicator = new BitArray(BitConverter.GetBytes(UnencodedString.Length));
        }

        public byte[] DataEncode()
        {
            List<BitArray> bitArray;
            bitArray = Encode();
            Terminate(bitArray);
            MakeMultipleOf8(bitArray);
            Pad(bitArray);
            EncodedBytes = ConvertListBitArrayToByteArray(bitArray);
            
            return EncodedBytes;
        }
    }
}
