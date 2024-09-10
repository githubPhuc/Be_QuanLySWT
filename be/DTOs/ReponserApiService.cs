using Newtonsoft.Json;
namespace be.DTOs
{
    public class ReponserApiService<T>
    {
      
            public string Message { get; set; }
            public int? Count { get; set; }

            public bool _isIgnoreNullData;
            private object _data;
            public object Data
            {
                get; set;
            }

            private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            public ReponserApiService(bool isIgnoreNullData = true)
            {
                this._isIgnoreNullData = isIgnoreNullData;
            }
        

    }
}
