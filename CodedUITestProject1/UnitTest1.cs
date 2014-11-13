using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiController.Tags;

namespace CodedUITestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ThesaurusApi()
        {
            ThesaurusAltervista th = new ThesaurusAltervista();
            th.GetTags("guitare"); 
        }
    }
}
