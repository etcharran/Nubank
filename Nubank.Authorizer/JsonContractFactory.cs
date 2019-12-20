using System.Linq;
using System.Text.Json;
using Nubank.Contract;
using Nubank.Tools;

namespace Nubank.Authorizer
{
    public class JsonContractFactory
    {
        /// <summary>
        /// Converts the Json to the Contract matching by the first key found
        /// <para>{'firstkey': {}} </para>
        /// <para>'firstkey' being the data contract name</para>
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IData ToContract(string document)
        {
            try
            {
                // Generate Json Iterator
                var iterator = JsonDocument.Parse(document).RootElement.EnumerateObject();
                
                // First Iterator Position
                iterator.MoveNext();

                // Get DataType by the name matched from the json document's first key
                var dataType = ReflectionTools.GetAllImplementations<IData>()
                        .Where(d => (string)d.GetField("name").GetValue(null) == iterator.Current.Name).First();

                // Convert the json document to the DataType retrieved
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                return JsonSerializer.Deserialize(iterator.Current.Value.GetRawText(), dataType, options) as IData;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("There is no matching Data in the Contracts. Provide supported operations.", ex);
            }
        }
    }
}