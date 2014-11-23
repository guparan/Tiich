using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichDAL;
using TiichRepository.Interface;
using System.Security.Cryptography;
using Utils;
using TiichDAL;
using TiichRepository.Repository;

namespace CodedUITestProject1
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void AddWorkshopWithTags()
        {
            WorkshopRepository repo = new WorkshopRepository();

            Workshop ws = new Workshop();
            ws.Tag = new List<Tag>() { new Tag{ label = "test"} };

            repo.Add(ws, new ErrorHandler());
        }
    }
}
