using System.Collections.Generic;
using Models;

namespace Services.Common.Abstractions
{
    public interface IRequestCounterService
    {
        void Notice(string path);

        IDictionary<string, RequestData> Get();
    }
}