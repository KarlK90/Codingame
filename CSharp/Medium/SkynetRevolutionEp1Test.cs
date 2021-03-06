﻿using NUnit.Framework;

namespace Codingame.Medium
{
    [TestFixture]
    public class SkynetRevolutionEp1Test_SimpleTest
    {
        [Test]
        public void SimpleTest()
        {
            SkynetRevolutionEp1.Main(new[] { "3 2 1", "1 2", "1 0", "2", "1" });
        }

        [Test]
        public void SeveralPaths()
        {
            SkynetRevolutionEp1.Main(new[] { "4 4 1", "1 3", "2 3", "0 1", "0 2", "3", "0", "2" });
        }

        [Test]
        public void Star()
        {
            SkynetRevolutionEp1.Main(new[] { "12 23 1", "11 6", "0 9", "1 2", "0 1", "10 1", "11 5", "2 3", "4 5", "8 9", "6 7", "7 8", "0 6", "3 4", "0 2", "11 7", "0 8", "0 4", "9 10", "0 5", "0 7", "0 3", "0 10", "5 6", "0", "11" });
        }

        [Test]
        public void TripleStar()
        {
            SkynetRevolutionEp1.Main(new[] { "38 79 3", "28 36", "0 2", "3 34", "29 21", "37 35", "28 32", "0 10", "37 2", "4 5", "13 14", "34 35", "27 19", "28 34", "30 31", "18 26", "0 9", "7 8", "18 24", "18 23", "0 5", "16 17", "29 30", "10 11", "0 12", "15 16", "0 11", "0 17", "18 22", "23 24", "0 7", "35 23", "22 23", "1 2", "0 13", "18 27", "25 26", "32 33", "28 31", "24 25", "28 35", "21 22", "4 33", "28 29", "36 22", "18 25", "37 23", "18 21", "5 6", "19 20", "0 14", "35 36", "9 10", "0 6", "20 21", "0 3", "33 34", "14 15", "28 33", "11 12", "12 13", "17 1", "18 19", "36 29", "0 4", "0 15", "0 1", "18 20", "2 3", "0 16", "8 9", "0 8", "26 27", "28 30", "3 4", "31 32", "6 7", "37 1", "37 24", "35 2", "0", "18", "28", "37" });
        }

        [Test]
        public void StarAltValidator()
        {
            SkynetRevolutionEp1.Main(new[] { "12 23 1", "4 9", "4 3 ", "4 2", "2 3", "2 11", "2 7", "7 11", "7 1", "1 11", "1 10", "10 11", "10 0", "0 11", "0 8", "8 11", "8 6", "6 11", "6 5", "5 11", "5 9", "9 11", "9 3", "3 11 ", "11", "4", "3", "11" });
        }

        [Test]
        public void TripleStarAltValidator()
        {
            SkynetRevolutionEp1.Main(new[] { "38 79 3", "5 11", "5 29", "29 11", "29 0", "0 11", "0 23", "23 11", "23 1", "1 11", "1 22", "22 11", "22 14", "14 11", "14 3", "3 11", "3 4", "4 11", "4 25", "25 11", "25 26", "26 11", "26 35", "35 11", "35 6", "6 11", "6 20", "20 11", "20 9", "9 11", "9 12", "12 11", "12 18", "18 11", "18 5", "20 37", "9 37", "9 36", "12 21", "18 17", "37 36", "36 8", "36 21", "21 8", "21 17", "17 8", "17 7", "7 8", "7 30", "30 8", "30 19", "19 8", "19 33", "33 8", "33 28", "28 8", "28 36", "34 27", "34 31", "34 33", "31 27", "31 32", "31 28", "32 27", "32 13", "32 36", "32 37", "13 27", "13 24", "13 37", "24 27", "24 15", "15 27", "15 16", "16 27", "16 10", "10 27", "10 2", "2 27", "2 34", "11", "8", "27", "37", "36", "32", "13", "27" });
        }

    }
}