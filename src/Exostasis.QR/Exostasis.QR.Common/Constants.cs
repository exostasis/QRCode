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

using Exostasis.QR.Common.Image;

namespace Exostasis.QR.Common
{
    public static class Constants
    {
        public static readonly int[,] RequiredErrorCorrectionCodesPerBlock = { { 7, 10, 13, 17 }, { 10, 16, 22, 28 }, { 15, 26, 18, 22 }, 
            { 20, 18, 26, 16 }, { 26, 24, 18, 22 }, { 18, 16, 24, 28 }, { 20, 18, 18, 26 }, { 24, 22, 22, 26 }, { 30, 22, 20, 24 }, 
            { 18, 26, 24, 28 }, { 20, 30, 28, 24 }, { 24, 22, 26, 28 }, { 26, 22, 24, 22 }, { 30, 24, 20, 24 }, { 22, 24, 30, 24 }, 
            { 24, 28, 24, 30 }, { 28, 28, 28, 28 }, { 30, 26, 28, 28 }, { 28, 26, 26, 26 }, { 28, 26, 30, 28 }, { 28, 26, 28, 30 }, 
            { 28, 28, 30, 24 }, { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, { 26, 28, 30, 30 }, { 28, 28, 28, 30 }, { 30, 28, 30, 30 }, 
            { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, 
            { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, { 30, 28, 30, 30 }, 
            { 30, 28, 30, 30 } };

        public static readonly int[,] RequiredBlocksInGroup1 = { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 2, 2 }, { 1, 2, 2, 4 }, 
            { 1, 2, 2, 2 }, { 2, 4, 4, 4 }, { 2, 4, 2, 4 }, { 2, 2, 4, 4 }, { 2, 3, 4, 4 }, { 2, 4, 6, 6 }, { 4, 1, 4, 3 }, { 2, 6, 4, 7 }, 
            { 4, 8, 8, 12 }, { 3, 4, 11, 11 }, { 5, 5, 5, 11 }, { 5, 7, 15, 3 }, { 1, 10, 1, 2 }, { 5, 9, 17, 2 }, { 3, 3, 17, 9 }, 
            { 3, 3, 15, 15 }, { 4, 17, 17, 19 }, { 2, 17, 7, 34 }, { 4, 4, 11, 16 }, { 6, 6, 11, 30 }, { 8, 8, 7, 22 }, { 10, 19, 28, 33 }, 
            { 8, 22, 8, 12 }, { 3, 3, 4, 11 }, { 7, 21, 1, 19 }, { 5, 19, 15, 23 }, { 13, 2, 42, 23 }, { 17, 10, 10, 19 }, 
            { 17, 14, 29, 11 }, { 13, 14, 44, 59 }, { 12, 12, 39, 22 }, { 6, 6, 46, 2 }, { 17, 29, 49, 24 }, { 4, 13, 48, 42 }, 
            { 20, 40, 43, 10 }, { 19, 18, 34, 20 } };

        public static readonly int[,] RequiredBlocksInGroup2 = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, 
            { 0, 0, 2, 2 }, { 0, 0, 0, 0 }, { 0, 0, 4, 1 }, { 0, 2, 2, 2 }, { 0, 2, 4, 4 }, { 2, 1, 2, 2 }, { 0, 4, 4, 8 }, { 2, 2, 6, 4 }, 
            { 0, 1, 4, 4 }, { 1, 5, 5, 5 }, { 1, 5, 7, 7 }, { 1, 3, 2, 13 }, { 5, 1, 15, 17 }, { 1, 4, 1, 19 }, { 4, 11, 4, 16 }, 
            { 5, 13, 5, 10 }, { 4, 0, 6, 6 }, { 7, 0, 16, 0 }, { 5, 14, 14, 14 }, { 4, 14, 16, 2 }, { 4, 13, 22, 13 }, { 2, 4, 6, 4 }, 
            { 4, 3, 26, 28 }, { 10, 23, 31, 31 }, { 7, 7, 37, 26 }, { 10, 10, 25, 25 }, { 3, 29, 1, 28 }, { 0, 23, 35, 35 }, 
            { 1, 21, 19, 46 }, { 6, 23, 7, 1 }, { 7, 26, 14, 41 }, { 14, 34, 10, 64 }, { 4, 14, 10, 46 }, { 18, 32, 14, 32 }, 
            { 4, 7, 22, 67 }, { 6, 31, 34, 61 } };

        public static readonly int[,] RequiredCodeWordsInBlocksGroup1 = { { 19, 16, 13, 9 }, { 34, 28, 22, 16 }, { 55, 44, 17, 13 }, 
            { 80, 32, 24, 9 }, { 108, 43, 15, 11 }, { 68, 27, 19, 15 }, { 78, 31, 14, 13 }, { 97, 38, 18, 14 }, { 116, 36, 16, 12 }, 
            { 68, 43, 19, 15 }, { 81, 50, 22, 12 }, { 92, 36, 20, 14 }, { 107, 37, 20, 11 }, { 115, 40, 16, 12 }, { 87, 41, 24, 12 }, 
            { 98, 45, 19, 15 }, { 107, 46, 22, 14 }, { 120, 43, 22, 14 }, { 113, 44, 21, 13 }, { 107, 41, 24, 15 }, { 116, 42, 22, 16 }, 
            { 111, 46, 24, 13 }, { 121, 47, 24, 15 }, { 117, 45, 24, 16 }, { 106, 47, 24, 15 }, { 114, 46, 22, 16 }, { 122, 45, 23, 15 }, 
            { 117, 45, 24, 15 }, { 116, 45, 23, 15 }, { 115, 47, 24, 15 }, { 115, 46, 24, 15 }, { 115, 46, 24, 15 }, { 115, 46, 24, 15 }, 
            { 115, 46, 24, 16 }, { 121, 47, 24, 15 }, { 121, 47, 24, 15 }, { 122, 46, 24, 15 }, { 122, 46, 24, 15 }, { 117, 47, 24, 15 }, 
            { 118, 47, 24, 15 } };

        public static readonly int[,] RequiredCodeWordsInBlocksGroup2 = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, 
            { 0, 0, 16, 12 }, { 0, 0, 0, 0 }, { 0, 0, 15, 14 }, { 0, 39, 19, 15 }, { 0, 37, 17, 13 }, { 69, 44, 20, 16 }, { 0, 51, 23, 13 }, 
            { 93, 37, 21, 15 }, { 0, 38, 21, 12 }, { 116, 41, 17, 13 }, { 88, 42, 25, 13 }, { 99, 46, 20, 16 }, { 108, 47, 23, 15 }, 
            { 121, 44, 23, 15 }, { 114, 45, 22, 14 }, { 108, 42, 25, 16 }, { 117, 0, 23, 17 }, { 112, 0, 25, 0 }, { 122, 48, 25, 16 }, 
            { 118, 46, 25, 17 }, { 107, 48, 25, 16 }, { 115, 47, 23, 17 }, { 123, 46, 24, 16 }, { 118, 46, 25, 16 }, { 117, 46, 24, 16 }, 
            { 116, 48, 25, 16 }, { 116, 47, 25, 16 }, { 0, 47, 25, 16 }, { 116, 47, 25, 16 }, { 116, 47, 25, 17 }, { 122, 48, 25, 16 }, 
            { 122, 48, 25, 16 }, { 123, 47, 25, 16 }, { 123, 47, 25, 16 }, { 118, 48, 25, 16 }, { 119, 48, 25, 16 } };

        public static readonly int[,] CodewordTable = { { 19, 16, 13, 9 }, { 34, 28, 22, 16 }, { 55, 44, 34, 26 }, { 80, 64, 48, 36 }, 
            { 108, 86, 62, 46 }, { 136, 108, 76, 60 }, { 156, 124, 88, 66 }, { 194, 154, 110, 86 }, { 232, 182, 132, 100 }, 
            { 274, 216, 154, 122 }, { 324, 254, 180, 140 }, { 370, 290, 206, 158 }, { 428, 334, 244, 180 }, { 461, 365, 261, 197 }, 
            { 523, 415, 295, 223 }, { 589, 453, 325, 253 }, { 647, 507, 367, 283 }, { 721, 563, 397, 313 }, { 795, 627, 445, 341 }, 
            { 861, 669, 485, 385 }, { 932, 714, 512, 406 }, { 1006, 782, 568, 442 }, { 1094, 860, 614, 464 }, { 1174, 914, 664, 514 }, 
            { 1276, 1000, 718, 538 }, { 1370, 1062, 754, 596 }, { 1468, 1128, 808, 628 }, { 1531, 1193, 871, 661 }, 
            { 1631, 1267, 911, 701 }, { 1735, 1373, 985, 745 }, { 1843, 1455, 1033, 793 }, { 1955, 1541, 1115, 845 }, 
            { 2071, 1631, 1171, 901 }, { 2191, 1725, 1231, 961 }, { 2306, 1812, 1286, 986 }, { 2434, 1914, 1354, 1054 }, 
            { 2566, 1992, 1426, 1096 }, { 2702, 2102, 1502, 1142 }, { 2812, 2216, 1582, 1222}, { 2956, 2334, 1666, 1276 } };

        public static readonly int[] RemainderBitsRequired = { 0, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 
            4, 4, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0 };

        public static readonly AlignmentCordArray[] AlignmentCords = { null, new AlignmentCordArray(6, 18), 
            new AlignmentCordArray(6, 22), new AlignmentCordArray(6, 26), new AlignmentCordArray(6, 30), new AlignmentCordArray(6, 34), 
            new AlignmentCordArray(6, 22, 38), new AlignmentCordArray(6, 22, 42), new AlignmentCordArray(6, 26, 46), 
            new AlignmentCordArray(6, 28, 50), new AlignmentCordArray(6, 30, 54), new AlignmentCordArray(6, 32, 58), 
            new AlignmentCordArray(6, 34, 62), new AlignmentCordArray(6, 26, 46, 66), new AlignmentCordArray(6, 26, 48, 70), 
            new AlignmentCordArray(6, 30, 56, 82), new AlignmentCordArray(6, 30, 58, 86), new AlignmentCordArray(6, 34, 62, 90), 
            new AlignmentCordArray(6, 28, 50, 72, 94), new AlignmentCordArray(6, 26, 50, 74, 98),
            new AlignmentCordArray(6, 30, 54, 78, 102), new AlignmentCordArray(6, 28, 54, 80, 106), 
            new AlignmentCordArray(6, 32, 58, 84, 110), new AlignmentCordArray(6, 30, 58, 86, 114), 
            new AlignmentCordArray(6, 34, 62, 90, 118), new AlignmentCordArray(6, 26, 50, 74, 98, 122), 
            new AlignmentCordArray(6, 30, 54, 78, 102, 126), new AlignmentCordArray(6, 26, 52, 78, 104, 130), 
            new AlignmentCordArray(6, 30, 56, 82, 108, 134), new AlignmentCordArray(6, 34, 60, 86, 112, 138), 
            new AlignmentCordArray(6, 30, 58, 86, 114, 142), new AlignmentCordArray(6, 34, 62, 90, 118, 146), 
            new AlignmentCordArray(6, 30, 54, 78, 102, 126, 150), new AlignmentCordArray(6, 24, 50, 76, 102, 128, 154), 
            new AlignmentCordArray(6, 28, 54, 80, 106, 132, 158), new AlignmentCordArray(6, 32, 58, 84, 110, 136, 162), 
            new AlignmentCordArray(6, 26, 54, 82, 110, 138, 166), new AlignmentCordArray(6, 30, 58, 86, 114, 142, 170) };
    }
}
