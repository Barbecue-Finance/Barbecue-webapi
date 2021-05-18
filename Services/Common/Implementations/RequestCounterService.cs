using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Services.Common.Abstractions;

namespace Services.Common.Implementations
{
    public class RequestCounterService : IRequestCounterService
    {
        private readonly Dictionary<string, RequestData> _dictionary;

        public RequestCounterService()
        {
            _dictionary = new();
        }

        public void Notice(string path)
        {
            var nowDateString = DateTime.Now.ToString("yy.MM.dd HH:mm:ss");
            if (!_dictionary.ContainsKey(path))
            {
                _dictionary.Add(path, new RequestData(nowDateString, 0));
            }

            var requestData = _dictionary[path];

            requestData.Amount++;
            requestData.LastRequest = nowDateString;
        }

        public IDictionary<string, RequestData> Get()
        {
            return _dictionary.OrderByDescending(r =>
                r.Value.Amount
            ).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}