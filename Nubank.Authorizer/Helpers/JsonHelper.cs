using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Nubank.Contract;
using Nubank.Tools.Exceptions;
using System.Linq;

namespace Nubank.Authorizer.Helpers
{
    public class JsonHelper
    {
        /// <summary>
        /// Converts the Json to the Contract matching by the first key found
        /// <para>{'firstkey': {}} </para>
        /// <para>'firstkey' being the data contract name</para>
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Data ToContract(string document)
        {
            try
            {
                var jObject = JObject.Parse(document);

                // Get DataType by the name matched from the json document's first key
                var dataType = ReflectionHelper.GetAllImplementations<IData>()
                        .Where(d => (string)d.GetField("name").GetValue(null) == jObject.First.Path).First();

                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
                };

                // Convert the json document to the DataType retrieved
                return JsonConvert.DeserializeObject(jObject.GetValue(jObject.First.Path).ToString(), dataType, settings) as Data;
            }
            catch (System.Exception ex)
            {
                throw new UnSupportedInputDataException(ex);
            }
        }

        /// <summary>
        /// Serialize the response through the ToResponseFormat
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static string Serialize(IResponse<Account> response)
        {
            return JsonConvert.SerializeObject(response.ToResposeFormat(), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}