using System.Collections.Generic;

namespace Nubank.Contract
{
    public interface IResponse<T> where T : IData
    {
        IList<string> Violations { get; set; }

        /// <summary>
        /// Format to display on Output
        /// </summary>
        /// <returns></returns>
        object ToResposeFormat();
    }
}