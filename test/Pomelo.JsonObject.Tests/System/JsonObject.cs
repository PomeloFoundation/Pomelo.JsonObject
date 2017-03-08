using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Pomelo.JsonObject.Tests.System
{

    public class JsonObjectTest
    {

        protected T Cast <T>(string serialized)
        {
            var dataParam = Expression.Parameter(typeof(string), "data");
            var body = Expression.Block(Expression.Convert(dataParam, typeof(T)));
            var run = Expression.Lambda(body, dataParam).Compile();
            return (T) run.DynamicInvoke(serialized);
        }

        [Fact]
        public void TestJsonObject()
        {
            const string jsonStr = @"{""a"":""b""}";
            var jsonObj = Cast<JsonObject<Dictionary<string, string>>>(jsonStr);
            Assert.True(jsonObj.Object.ContainsKey("a"));
            Assert.Equal("b", jsonObj.Object["a"]);
            Assert.Equal(jsonStr, jsonObj.Json);
        }
    }

}
