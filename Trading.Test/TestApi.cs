using NUnit.Framework;
using System;
using System.Linq;
using Trading.Authen.Repository.Entity;
using Trading.Authen.Repository.UnitOfWork;

namespace Trading.Authen.Test
{

    public class TestApi
    {
        private  IUnitOfWork _unitOfWork;
        private  TradingDbAuthenContext _dbContext;
        [SetUp]
        public void Setup()
        {
            _dbContext = new TradingDbAuthenContext();
            _unitOfWork = new UnitOfWork(_dbContext);
        }
        [TestCase("5")]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        public void Test_Get_User(string name)
        {
            var users = _unitOfWork.RolesRepository.GetFirst();
            Assert.IsNull(users, "user is null");
        }
        [Test]
        public void Test_Get_User1()
        {
            var users = _unitOfWork.RolesRepository.GetFirst();
            Assert.IsNull(users);
            //var data =  Assert.CatchAsync(async () =>
            //{
            //   // throw new Exception("xcvcx");

            //});

        }
    }
}