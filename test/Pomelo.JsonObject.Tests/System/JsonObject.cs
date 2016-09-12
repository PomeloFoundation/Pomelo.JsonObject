using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pomelo.JsonObject.Tests.System
{

    [TestClass]
    public class JsonObjectTest
    {

        protected T Cast <T>(string serialized)
        {
            var dataParam = Expression.Parameter(typeof(string), "data");
            var body = Expression.Block(Expression.Convert(dataParam, typeof(T)));
            var run = Expression.Lambda(body, dataParam).Compile();
            return (T) run.DynamicInvoke(serialized);
        }

        [TestMethod]
        public void TestJsonObject()
        {
            const string jsonStr = @"{""a"":""b""}";
            var jsonObj = Cast<JsonObject<Dictionary<string, string>>>(jsonStr);
            Assert.IsTrue(jsonObj.Object.ContainsKey("a"));
            Assert.AreEqual("b", jsonObj.Object["a"]);
            Assert.AreEqual(jsonStr, jsonObj.Json);
        }
    }

}
