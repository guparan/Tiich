using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiController.WordExtraction;
using System.Collections.Generic;

namespace CodedUITestProject1
{
    [TestClass]
    public class WordExtractionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            HomeMadeExtraction ext = new HomeMadeExtraction();
            string text = "cours de tennis, et de guitare, 99888 et penis intérersidérale. DU CUL";

            List<string> res = ext.Extract(text); 
        }
    }
}
